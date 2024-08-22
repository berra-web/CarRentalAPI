using CarRentalAPI.Domain.Models;

namespace CarRentalAPI.Domain.DTOs
{
    public class CarDto
    {
        public string RegistrationNumber { get; set; }
        public CarCategory Category { get; set; }
        public double BaseDailyRent { get; set; }
        public double BaseKmPrice { get; set; }
        public double CurrentMileage { get; set; }
    }
}
