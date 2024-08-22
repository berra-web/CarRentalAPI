using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarRentalAPI.Tests.Controllers
{
    public class RentalControllerTests
    {
        private readonly Mock<IRentalService> _rentalServiceMock;
        private readonly RentalController _controller;

        public RentalControllerTests()
        {
            _rentalServiceMock = new Mock<IRentalService>();
            _controller = new RentalController(_rentalServiceMock.Object);
        }

        [Fact]
        public async Task RegisterRental_ReturnsOkResult_WhenRentalIsRegistered()
        {
            // Arrange
            var rentalDto = new RentalDto
            {
                CarId = 1,
                CustomerId = "123456-7890",
                CustomerName = "Behrad HMB",
                CustomerPhoneNumber = "123456789",
                RentalStart = System.DateTime.Now,
                StartMileage = 1000,
                Price = null
            };

            _rentalServiceMock.Setup(service => service.RegisterRentalAsync(rentalDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterRental(rentalDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _rentalServiceMock.Verify(service => service.RegisterRentalAsync(rentalDto), Times.Once);
        }

        [Fact]
        public async Task RegisterReturn_ReturnsOkResult_WhenRentalIsReturned()
        {
            // Arrange
            var returnDto = new ReturnRentalDto
            {
                BookingNumber = "BN-12345678",
                EndMileage = 2000
            };

            _rentalServiceMock.Setup(service => service.RegisterReturnAsync(returnDto)).ReturnsAsync(1);

            // Act
            var result = await _controller.RegisterReturn(returnDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            _rentalServiceMock.Verify(service => service.RegisterReturnAsync(returnDto), Times.Once);
        }

        [Fact]
        public async Task GetRentalById_ReturnsNotFound_WhenRentalDoesNotExist()
        {
            // Arrange
            _rentalServiceMock.Setup(service => service.GetRentalByIdAsync(It.IsAny<int>())).ThrowsAsync(new System.ArgumentException("Rental not found"));

            // Act
            var result = await _controller.GetRentalById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
