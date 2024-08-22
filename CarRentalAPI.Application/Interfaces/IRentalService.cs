using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;

namespace CarRentalAPI.Application.Interfaces
{
    public interface IRentalService
    {
        Task RegisterRentalAsync(RentalDto rentalDto);
        Task<int> RegisterReturnAsync(ReturnRentalDto returnDto);
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental> GetRentalByIdAsync(int rentalId);

        Task DeleteRentalAsync(int id);


    }


}
