using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Domain.DTOs
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "PersonId is required.")]
        public string PersonId { get; set; } 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

}
