using CarRentalAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data.CarRentalDbContext
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konverterar enum-värden i CarCategory till strängar i databasen
            modelBuilder.Entity<Car>()
           .Property(c => c.Category)
           .HasConversion<string>();

            // Definierar relationen mellan Rental och Car (en bil kan ha många uthyrningar)
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId);

            // Definierar relationen mellan Rental och Customer (en kund kan ha många uthyrningar)
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);


            // Skapar databasen med fördefinierade bilar
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, RegistrationNumber = "ABC555", Category = CarCategory.SmallCar, BaseDailyRent = 200, BaseKmPrice = 2, CurrentMileage = 15000 },
                new Car { Id = 2, RegistrationNumber = "XYZ122", Category = CarCategory.Kombi, BaseDailyRent = 300, BaseKmPrice = 2.5, CurrentMileage = 25000 },
                new Car { Id = 3, RegistrationNumber = "LMN852", Category = CarCategory.Truck, BaseDailyRent = 500, BaseKmPrice = 3, CurrentMileage = 50000 }
            );

            // Skapar en unik index på RegistrationNumber för att säkerställa att varje bil har ett unikt registreringsnummer
            modelBuilder.Entity<Car>()
                .HasIndex(c => c.RegistrationNumber)
                .IsUnique();

            // Skapar databasen med en fördefinierad kund
            modelBuilder.Entity<Customer>()
                .HasData(new Customer
                {
                    Id = "900531-4176",
                    Name = "Behrad HMB",
                    PhoneNumber = "0736755551"
                }
            );
        }
    }
}
