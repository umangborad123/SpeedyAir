using Ardalis.GuardClauses;

namespace SpeedyAir.ly.Domain
{
    public class Order
    {
        #region Public Members

        public string Id { get; private set; } = null!;

        public string Destination { get; private set; } = null!;

        public Flight? Flight { get; private set; }

        #endregion Public Members

        #region Public Constructors

        public Order(string id, string destination)
        {
            SetId(id);
            SetDestination(destination);
        }

        public Order(string id, string destination, Flight? flight)
        {
            SetId(id);
            SetDestination(destination);
            SetFlight(flight);
        }

        #endregion Public Constructors

        #region Set Methods

        public void SetId(string id)
        {
            Id = Guard.Against.NullOrEmpty(id, nameof(Id));
        }

        public void SetDestination(string destination)
        {
            Destination = Guard.Against.NullOrEmpty(destination, nameof(Destination));
        }

        public void SetFlight(Flight? flight)
        {
            Flight = flight;
        }

        #endregion Set Methods

        public override string ToString()
        {
            return Flight != null
                ? $"order: {Id}, flightNumber: {Flight.Id}, departure: {Flight.Departure}, arrival: {Flight.Arrival}, day: {Flight.Day}"
                : $"order: {Id}, flightNumber: not scheduled";
        }
    }
}