using Ardalis.GuardClauses;
using SpeedyAir.ly.SharedKernel;

namespace SpeedyAir.ly.Domain
{
    public class Flight
    {
        #region Public Members
        
        public int Id { get; private set; }

        public string Departure { get; private set; } = null!;

        public string Arrival { get; private set; } = null!;

        public int Day { get; private set; }

        public int CurrentLoad { get; private set; }

        #endregion Public Members

        #region Public Constructors

        public Flight(int id, string departure, string arrival, int day)
        {
            SetId(id);
            SetDeparture(departure);
            SetArrival(arrival);
            SetDay(day);
        }

        #endregion Public Constructors

        #region Set Methods

        public void SetId(int id)
        {
            Id = Guard.Against.NegativeOrZero(id, nameof(Id));
        }

        public void SetDeparture(string departure)
        {
            Departure = Guard.Against.NullOrEmpty(departure, nameof(Departure));
        }

        public void SetArrival(string arrival)
        {
            Arrival = Guard.Against.NullOrEmpty(arrival, nameof(arrival));
        }

        public void SetDay(int day)
        {
            Day = Guard.Against.NegativeOrZero(day, nameof(day));
        }

        public void IncrementCurrentLoad()
        {
            Guard.Against.InvalidInput(CurrentLoad, nameof(CurrentLoad), x => x < Constants.CurrentLoadLimit,
                $"Current load: {CurrentLoad} cannot be increase beyond {Constants.CurrentLoadLimit}");

            CurrentLoad++;
        }

        #endregion Set Methods

        public override string ToString()
        {
            return $"Flight: {Id}, departure: {Departure}, arrival: {Arrival}, day: {Day}";
        }
    }
}