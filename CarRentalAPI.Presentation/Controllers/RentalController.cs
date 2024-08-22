using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("register-rental")]
        public async Task<IActionResult> RegisterRental(RentalDto rentalDto)
        {
            try
            {
                await _rentalService.RegisterRentalAsync(rentalDto);
                return Ok("Rental registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Returnera ett 400 Bad Request svar med ett användarvänligt meddelande
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("register-return")]
        public async Task<IActionResult> RegisterReturn(ReturnRentalDto returnDto)
        {
            try
            {
                int rentalId = await _rentalService.RegisterReturnAsync(returnDto); // Vänta på att få tillbaka rentalId
                return Ok(new { rentalId }); // Returnera rentalId i svaret
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Returnera ett användarvänligt felmeddelande
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request."); // Returnera ett generiskt felmeddelande
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentalsAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            try
            {
                var rental = await _rentalService.GetRentalByIdAsync(id);
                return Ok(rental);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Om uthyrningen inte hittas, returnera 404 Not Found
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            try
            {
                var rental = await _rentalService.GetRentalByIdAsync(id);
                if (rental == null)
                {
                    return NotFound($"Rental with ID {id} not found.");
                }

                await _rentalService.DeleteRentalAsync(id);
                return Ok($"Rental with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the rental: {ex.Message}");
            }
        }
    }
}
