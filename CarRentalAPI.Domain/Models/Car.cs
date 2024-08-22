namespace CarRentalAPI.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public CarCategory Category { get; set; }
        public double BaseDailyRent { get; set; }
        public double BaseKmPrice { get; set; }
        public double CurrentMileage { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

}
