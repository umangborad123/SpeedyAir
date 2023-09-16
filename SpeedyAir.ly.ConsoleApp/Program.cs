using Microsoft.Extensions.DependencyInjection;
using SpeedyAir.ly.Application.Interface;
using SpeedyAir.ly.ConsoleApp.Extension;

namespace SpeedyAir.ly;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Set up the dependency injection container
            var serviceProvider = new ServiceCollection()
                .RegisterService()
                .BuildServiceProvider();

            var flightScheduler = serviceProvider.GetService<IFlightScheduler>()!;

            // User Story 1:
            flightScheduler.LoadFlights();
            flightScheduler.DisplayFlightSchedules();

            // User Story 2:
            flightScheduler.LoadOrders();
            flightScheduler.ScheduleOrdersToFlights();
            flightScheduler.DisplayOrders();

            // Display full flight details including current load
            //flightScheduler.DisplayFullFlightDetails();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}