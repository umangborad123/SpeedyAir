using Newtonsoft.Json;
using SpeedyAir.ly.Application.Interface;
using SpeedyAir.ly.Domain;
using SpeedyAir.ly.SharedKernel;
using System.Text;

namespace SpeedyAir.ly.Application
{
    public class FlightScheduler : IFlightScheduler
    {
        private const int FlightCapacity = Constants.CurrentLoadLimit;

        public List<Flight> Flights { get; set; }

        public List<Order> Orders { get; set; }

        public FlightScheduler()
        {
            Flights = new List<Flight>();
            Orders = new List<Order>();
        }

        public void LoadFlights()
        {
            Flights.Clear();
            Flights.AddRange(new List<Flight>()
            {
                new(1, "YUL",  "YYZ", 1),
                new(2, "YUL",  "YYC", 1),
                new(3, "YUL",  "YVR", 1),
                new(4, "YUL",  "YYZ", 2),
                new(5, "YUL",  "YYC", 2),
                new(6, "YUL",  "YVR", 2)
            });
        }

        public void DisplayFlightSchedules()
        {
            Console.WriteLine("Flight Schedule:");
            Flights.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public void LoadOrders()
        {
            try
            {
                const string path = @"Resource/coding-assignment-orders.json";
                var json = File.ReadAllText(path);

                var orderDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json) ??
                                new Dictionary<string, Dictionary<string, string>>();

                Orders.Clear();

                foreach (var keyValuePair in orderDict)
                {
                    Orders.Add(new Order(keyValuePair.Key, keyValuePair.Value["destination"]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred in {nameof(LoadOrders)}");
                Console.WriteLine(e);
                throw;
            }
        }

        public void ScheduleOrdersToFlights()
        {
            var orderCount = 0;
            var flightCount = 0;

            var flightLookup = Flights.ToLookup(x => x.Arrival);

            foreach (var order in Orders)
            {
                var destination = order.Destination;
                if (!flightLookup.Contains(destination))
                {
                    continue;
                }

                var possibleFlights = flightLookup[destination];
                foreach (var possibleFlight in possibleFlights)
                {
                    if (possibleFlight.CurrentLoad >= FlightCapacity)
                    {
                        continue;
                    }

                    order.SetFlight(possibleFlight);
                    possibleFlight.IncrementCurrentLoad();

                    break;
                }
            }
        }

        public void DisplayOrders()
        {
            Console.WriteLine("Flight Itineraries:");
            Orders.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        public void DisplayFullFlightDetails()
        {
            Console.WriteLine("Flight Details:");
            foreach (var flight in Flights)
            {
                var sb = new StringBuilder();
                sb.Append(flight);
                sb.Append(", current load: ");
                sb.Append(flight.CurrentLoad);
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine();
        }
    }
}