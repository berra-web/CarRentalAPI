namespace CarRentalAPI.Domain.Models
{
    public class Customer
    {
        public string Id { get; set; }  // Motsvarar PersonId i CustomerDto
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

}
