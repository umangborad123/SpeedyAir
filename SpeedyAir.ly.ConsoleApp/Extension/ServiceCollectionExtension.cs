using Microsoft.Extensions.DependencyInjection;
using SpeedyAir.ly.Application;
using SpeedyAir.ly.Application.Interface;

namespace SpeedyAir.ly.ConsoleApp.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection service)
        {
            service.AddTransient<IFlightScheduler, FlightScheduler>();

            return service;
        }
    }
}