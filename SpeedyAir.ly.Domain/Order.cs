namespace SpeedyAir.ly.Domain
{
    public class Order
    {
        public string Id { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public Flight? Flight { get; set; }

        public override string ToString()
        {
            return Flight != null
                ? $"order: {Id}, flightNumber: {Flight.Id}, departure: {Flight.Departure}, arrival: {Flight.Arrival}, day: {Flight.Day}"
                : $"order: {Id}, flightNumber: not scheduled";
        }
    }
}