using CarRentalAPI.Domain.DTOs;
using CarRentalAPI.Domain.Models;

namespace CarRentalAPI.Application.Interfaces
{
    public interface ICarService
    {
        Task<Car> GetCarByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task AddCarAsync(CarDto carDto);  
        //Task UpdateCarAsync(int id, CarDto carDto);
        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<IEnumerable<Car>> GetRentedCarsAsync();

        Task DeleteCarAsync(int id);

    }
}
