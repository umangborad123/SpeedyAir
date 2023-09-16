namespace SpeedyAir.ly.Domain
{
    public class Flight
    {
        public int Id { get; set; }

        public string Departure { get; set; } = null!;

        public string Arrival { get; set; } = null!;

        public int Day { get; set; }

        public int CurrentLoad { get; set; } = 0;

        public override string ToString()
        {
            return $"Flight: {Id}, departure: {Departure}, arrival: {Arrival}, day: {Day}";
        }
    }
}