using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;

namespace CarRentalAPI.Application.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto customerDto);
        Task<Customer> GetCustomerByIdAsync(string id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task DeleteCustomerAsync(string id);

    }

}
