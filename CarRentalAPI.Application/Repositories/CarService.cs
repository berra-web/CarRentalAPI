using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Data.CarRentalDbContext;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Application.Repositories
{
    public class CarService : ICarService
    {

        private readonly CarRentalContext _context;

        public CarService(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        //public async Task UpdateCarAsync(int id, CarDto carDto)
        //{
        //    var car = await GetCarByIdAsync(id);
        //    if (car != null)
        //    {
        //        car.RegistrationNumber = carDto.RegistrationNumber;
        //        car.Category = carDto.Category;
        //        car.BaseDailyRent = carDto.BaseDailyRent;
        //        car.BaseKmPrice = carDto.BaseKmPrice;
        //        car.CurrentMileage = carDto.CurrentMileage;

        //        _context.Cars.Update(car);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
        {
            return await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetRentedCarsAsync()
        {
            return await _context.Cars.Where(c => !c.IsAvailable).ToListAsync();
        }
       

        public async Task AddCarAsync(CarDto carDto)
        {
            if (!await IsRegistrationNumberUniqueAsync(carDto.RegistrationNumber))
            {
                throw new InvalidOperationException("A car with this registration number already exists. Please use a different registration number.");
            }

            var car = new Car
            {
                RegistrationNumber = carDto.RegistrationNumber,
                Category = carDto.Category,
                BaseDailyRent = carDto.BaseDailyRent,
                BaseKmPrice = carDto.BaseKmPrice,
                CurrentMileage = carDto.CurrentMileage,
                IsAvailable = true
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsRegistrationNumberUniqueAsync(string registrationNumber)
        {
            return !await _context.Cars.AnyAsync(c => c.RegistrationNumber == registrationNumber);
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
