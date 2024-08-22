using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Data.CarRentalDbContext;
using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Application.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly CarRentalContext _context;

        public CustomerService(CarRentalContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            bool customerExists = await _context.Customers.AnyAsync(c => c.Id == customerDto.PersonId);
            if (customerExists)
            {
                throw new InvalidOperationException("A customer with this ID already exists. Please use a different ID.");
            }

            var customer = new Customer
            {
                Id = customerDto.PersonId,
                Name = customerDto.Name,
                PhoneNumber = customerDto.PhoneNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }


        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }


        public async Task DeleteCustomerAsync(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

    }

}
