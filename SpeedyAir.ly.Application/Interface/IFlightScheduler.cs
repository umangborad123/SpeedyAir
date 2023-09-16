namespace SpeedyAir.ly.Application.Interface
{
    public interface IFlightScheduler
    {
        void LoadFlights();

        void DisplayFlightSchedules();

        void LoadOrders();

        void ScheduleOrdersToFlights();

        void DisplayOrders();

        void DisplayFullFlightDetails();
    }
}