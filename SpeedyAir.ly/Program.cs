using SpeedyAir.ly.Application;
using SpeedyAir.ly.Application.Interface;

namespace SpeedyAir.ly;

internal class Program
{
    private static void Main(string[] args)
    {
        IFlightScheduler flightScheduler = new FlightScheduler();

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
}