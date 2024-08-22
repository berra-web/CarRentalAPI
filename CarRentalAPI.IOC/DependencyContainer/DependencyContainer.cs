using CarRentalAPI.Application.Interfaces;
using CarRentalAPI.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalAPI.IOC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
