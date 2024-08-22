namespace CarRentalAPI.Domain.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string BookingNumber { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
        public double? StartMileage { get; set; }
        public double? EndMileage { get; set; }
        public double? Price { get; set; }
        public int? DaysRented { get; set; }
    }

}
