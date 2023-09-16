using System.Text;
using Newtonsoft.Json;
using SpeedyAir.ly.Application.Interface;
using SpeedyAir.ly.Domain;

namespace SpeedyAir.ly.Application
{
    public class FlightScheduler : IFlightScheduler
    {
        private const int FlightCapacity = 20;

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
                new()
                {
                    Id = 1, Departure = "YUL", Arrival = "YYZ", Day = 1
                },
                new()
                {
                    Id = 2, Departure = "YUL", Arrival = "YYC", Day = 1
                },
                new()
                {
                    Id = 3, Departure = "YUL", Arrival = "YVR", Day = 1
                },
                new()
                {
                    Id = 4, Departure = "YUL", Arrival = "YYZ", Day = 2
                },
                new()
                {
                    Id = 5, Departure = "YUL", Arrival = "YYC", Day = 2
                },
                new()
                {
                    Id = 6, Departure = "YUL", Arrival = "YVR", Day = 2
                },
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
                    Orders.Add(new Order()
                    {
                        Id = keyValuePair.Key,
                        Destination = keyValuePair.Value["destination"]
                    });
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

                    order.Flight = possibleFlight;
                    possibleFlight.CurrentLoad++;
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