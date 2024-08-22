using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // POST: api/car
        [HttpPost]
        public async Task<IActionResult> AddCar([FromForm] CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _carService.AddCarAsync(carDto);
                return Ok("Car added successfully.");
            }
            catch (InvalidOperationException)
            {
                // Returnera ett specifikt felmeddelande om dubblett
                return BadRequest(new { message = "A car with this registration number already exists. Please use a different registration number." });
            }
            catch (Exception)
            {
                // Returnera ett generiskt felmeddelande för andra undantag
                return StatusCode(500, new { message = "An unexpected error occurred while processing your request." });
            }
        }

        // GET: api/car
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        // GET: api/car/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound(new { message = "Car not found with the specified ID." });
                }

                return Ok(car);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while processing your request." });
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var cars = await _carService.GetAvailableCarsAsync();
            return Ok(cars);
        }

        [HttpGet("rented")]
        public async Task<IActionResult> GetRentedCars()
        {
            var cars = await _carService.GetRentedCarsAsync();
            return Ok(cars);
        }


        // DELETE: api/Car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found.");
                }

                await _carService.DeleteCarAsync(id);
                return Ok($"Car with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the car: {ex.Message}");
            }
        }
    }
}
