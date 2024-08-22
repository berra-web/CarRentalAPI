using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Application.Repositories;
using CarRentalAPI.Data.CarRentalDbContext;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CarRentalAPI.Tests.Services
{
    public class RentalServiceTests
    {
        private readonly CarRentalContext _context;
        private readonly ICustomerService _customerServiceMock;
        private readonly RentalService _rentalService;

        public RentalServiceTests()
        {
            var options = new DbContextOptionsBuilder<CarRentalContext>()
                .UseInMemoryDatabase(databaseName: "CarRentalTestDb")
                .Options;

            _context = new CarRentalContext(options);
            _customerServiceMock = new Mock<ICustomerService>().Object;
            _rentalService = new RentalService(_context, _customerServiceMock);
        }

        [Fact]
        public async Task RegisterReturnAsync_ShouldUpdateRental_WhenBookingNumberExists()
        {
            // Arrange
            var customer = new Customer
            {
                Id = "123456-7890",
                Name = "Behrad HMB",
                PhoneNumber = "123456789"
            };

            var car = new Car
            {
                Id = 1,
                RegistrationNumber = "ABC123",
                IsAvailable = false,
                Category = CarCategory.SmallCar,
                BaseDailyRent = 200,
                BaseKmPrice = 1.5,
                CurrentMileage = 10000
            };

            var rental = new Rental
            {
                Id = 1,
                BookingNumber = "BN-12345678",
                Car = car,
                Customer = customer,
                CustomerId = customer.Id, 
                RentalStart = DateTime.Now,
                StartMileage = car.CurrentMileage
            };

            await _context.Customers.AddAsync(customer);
            await _context.Cars.AddAsync(car);
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            var returnDto = new ReturnRentalDto
            {
                BookingNumber = "BN-12345678",
                EndMileage = 2000
            };

            // Act
            await _rentalService.RegisterReturnAsync(returnDto);

            // Assert
            var updatedRental = await _context.Rentals.FirstOrDefaultAsync(r => r.BookingNumber == "BN-12345678");
            Assert.NotNull(updatedRental);
            Assert.Equal(2000, updatedRental.EndMileage);
            Assert.True(updatedRental.Car.IsAvailable);
        }
    }
}
