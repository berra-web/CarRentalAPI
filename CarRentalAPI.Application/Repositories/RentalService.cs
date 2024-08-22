using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Data.CarRentalDbContext;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Application.Repositories
{

    public class RentalService : IRentalService
    {
        private readonly CarRentalContext _context;
        private readonly ICustomerService _customerService;

        public RentalService(CarRentalContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }


        public async Task RegisterRentalAsync(RentalDto rentalDto)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == rentalDto.CarId);
                if (car == null)
                {
                    throw new ArgumentException("Car with the specified ID does not exist.");
                }

                if (!car.IsAvailable)
                {
                    throw new InvalidOperationException("The car is currently not available for rent.");
                }

                var customer = await _customerService.GetCustomerByIdAsync(rentalDto.CustomerId);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        Id = rentalDto.CustomerId,
                        Name = rentalDto.CustomerName,
                        PhoneNumber = rentalDto.CustomerPhoneNumber
                    };

                    await _customerService.AddCustomerAsync(new CustomerDto
                    {
                        PersonId = customer.Id,
                        Name = customer.Name,
                        PhoneNumber = customer.PhoneNumber
                    });
                }

                var newRental = new Rental
                {
                    CarId = rentalDto.CarId,
                    CustomerId = customer.Id,
                    RentalStart = rentalDto.RentalStart,
                    StartMileage = rentalDto.StartMileage,
                    Price = rentalDto.Price,
                    BookingNumber = $"BN-{Guid.NewGuid().ToString().Substring(0, 8)}" // Generera ett unikt bokningsnummer
                };

                car.IsAvailable = false; // Markera bilen som uthyrd

                _context.Rentals.Add(newRental);
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Rental could not be registered: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while processing your request.", ex);
            }
        }


        public async Task<int> RegisterReturnAsync(ReturnRentalDto returnDto)
        {
            var rental = await _context.Rentals.Include(r => r.Car)
                                               .FirstOrDefaultAsync(r => r.BookingNumber == returnDto.BookingNumber);

            if (rental == null)
            {
                throw new ArgumentException("Rental with the specified Booking Number does not exist.");
            }

            rental.RentalEnd = DateTime.Now;
            rental.EndMileage = returnDto.EndMileage;

            rental.Car.CurrentMileage = returnDto.EndMileage;
            rental.Car.IsAvailable = true;

            // Beräkna antal dagar
            rental.DaysRented = (rental.RentalEnd.Value - rental.RentalStart).Days;

            // Beräkna priset beroende på bilkategori
            double price = 0;
            var kilometersDriven = rental.EndMileage.HasValue && rental.StartMileage.HasValue
                ? rental.EndMileage.Value - rental.StartMileage.Value
                : 0;

            switch (rental.Car.Category)
            {
                case CarCategory.SmallCar:
                    price = rental.Car.BaseDailyRent * rental.DaysRented.Value;
                    break;
                case CarCategory.Kombi:
                    price = rental.Car.BaseDailyRent * rental.DaysRented.Value * 1.3 + rental.Car.BaseKmPrice * kilometersDriven;
                    break;
                case CarCategory.Truck:
                    price = rental.Car.BaseDailyRent * rental.DaysRented.Value * 1.5 + rental.Car.BaseKmPrice * kilometersDriven * 1.5;
                    break;
            }

            rental.Price = price; // Sätt det beräknade priset

            _context.Rentals.Update(rental);
            _context.Cars.Update(rental.Car);
            await _context.SaveChangesAsync();

            return rental.Id; // Returnera rentalId
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals.Include(r => r.Car).Include(r => r.Customer).ToListAsync();
        }

        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            var rental = await _context.Rentals.Include(r => r.Car).Include(r => r.Customer)
                                               .FirstOrDefaultAsync(r => r.Id == rentalId);
            if (rental == null)
            {
                throw new ArgumentException("Rental with the specified ID does not exist.");
            }

            return rental;
        }


        public async Task DeleteRentalAsync(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }

    }

}
