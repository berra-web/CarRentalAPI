namespace CarRentalAPI.Domain.DTOs
{
    public class RentalDto
    {
        public int CarId { get; set; }
        public string CustomerId { get; set; } 
        public string CustomerName { get; set; } 
        public string CustomerPhoneNumber { get; set; } 
        public DateTime RentalStart { get; set; } 
        public double? StartMileage { get; set; }
        public double? Price { get; set; } 
        public int? DaysRented { get; set; }
    }

}
