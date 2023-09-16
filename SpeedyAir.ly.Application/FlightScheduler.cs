using Newtonsoft.Json;
using SpeedyAir.ly.Application.Interface;
using SpeedyAir.ly.Domain;
using SpeedyAir.ly.SharedKernel;
using System.Text;

namespace SpeedyAir.ly.Application
{
    public class FlightScheduler : IFlightScheduler
    {
        public List<Flight> Flights { get; set; }

        public List<Order> Orders { get; set; }

        public FlightScheduler()
        {
            Flights = new List<Flight>();
            Orders = new List<Order>();
        }

        /// <summary>
        /// Load flight information based on Scenario 1
        /// </summary>
        public void LoadFlights()
        {
            Flights.Clear();
            Flights.AddRange(new List<Flight>()
            {
                new(1, Constants.Montreal, Constants.Toronto, 1),
                new(2, Constants.Montreal, Constants.Calgary, 1),
                new(3, Constants.Montreal, Constants.Vancouver, 1),
                new(4, Constants.Montreal, Constants.Toronto, 2),
                new(5, Constants.Montreal, Constants.Calgary, 2),
                new(6, Constants.Montreal, Constants.Vancouver, 2)
            });
        }

        public void DisplayFlightSchedules()
        {
            Console.WriteLine("Flight Schedule:");
            Flights.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        /// <summary>
        /// Load orders from coding-assignment-orders.json file.
        /// Orders are loaded in priority order
        /// </summary>
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

        /// <summary>
        /// Schedule orders to flights based on priority.
        /// Each flight has a capacity of 20 boxes.
        /// </summary>
        public void ScheduleOrdersToFlights()
        {
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
                    if (possibleFlight.CurrentLoad >= Constants.CurrentLoadLimit)
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