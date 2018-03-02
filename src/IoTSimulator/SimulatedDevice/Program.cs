namespace SimulatedDevice
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.Azure.Devices.Client;

    using Newtonsoft.Json;

    using SimulatedDevice.Classes;
    using SimulatedDevice.Services;
    using SimulatedDevice.Models;

    public class Program
    {
        // Create your IoTHub in the Azure portal and enter the URI below.
        private const string IotHubUri = "{iot hub hostname}";

        // Run the CreateDevideIdentity project and enter that value below.
        private const string DeviceKey = "{device key}";

        // Enter a name for the device.
        private const string DeviceId = "rfid-scanner";

        private static readonly Random Rand = new Random();
        private const double BagWeight = 35;

        private static DeviceClient _deviceClient;

        // TODO: Enter in the flight you want to follow: Valid entries include FL123#, where 
        // # is between 0 and 9.
        static string myFlightNumber = "FL1234";

        private static void Main(string[] args)
        {
            Console.WriteLine("Simulated RFID Scanner\n");
            // Connect to IOT Hub
            _deviceClient = DeviceClient.Create(IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey), TransportType.Mqtt);            

            Console.WriteLine("One moment, setting up the backend... \n");
            // Upload the flights
            CheckForExistingFlights().Wait();
            
            // Upload the bags
            CheckForExistingBags().Wait();

            // Need to run synchronously to preserve order of operations
            Console.WriteLine("Checking in the bags");
            CheckBagIn().Wait();
            ScanBagOntoPlane().Wait();
            ScanBagOffPlane().Wait();
            ScanBagOntoCarousel().Wait();

            Console.ReadLine();
        }

#region SetupDatabase
        // First, check for existing flights. If they don't exist, create them.
        // Second, If flights do exist, check for bags, and if they don't exist, create them.
        public static async Task CheckForExistingFlights()
        {
            var flights = await new FlightService().GetFlights();
                      
            if ((flights != null) && (flights.Count != 0))
            {
                return;          
            } else
            {
                // create flights            
                await InsertFlights();
            }
        }

        public static async Task InsertFlights()
        {
            var dummyFlights = new DummyData.DummyData().CreateFlights();
            var existingFlights = CosmosDataService.Instance("FlightCollection").GetFlights();

            // Get current list of flights in DB.
            // If they match the dummy data, do nothing. If they don't create the dummy data.         
            // This was done in case we want to let people create their own flights later.
            foreach (var f in dummyFlights)
            {
                // Check if dummy flight is already in DB
                var matches = existingFlights.Where(b => String.Equals(b.FlightNumber, f.FlightNumber)).ToList();
                
                // If not in DB, create it.
                if ((matches.Count == 0) || (matches == null))
                {
                    await CosmosDataService.Instance("FlightCollection").InsertItemAsync(f);
                } else
                {
                    return; 
                }
            }        
        }

        public static async Task CheckForExistingBags()
        {
            var flights = new DummyData.DummyData().CreateFlights();

            foreach (var f in flights)
            {
                // check for bags
                var bags = await new FlightService().GetBagsForFlight(f.FlightNumber);

                // if no bags, add them
                if ((bags == null) || (bags.Count == 0))
                {
                    await InsertBags(f);
                }
            }            
        }

        public static async Task InsertBags(Flight f)
        {
            // create bags
            for (int i = 0; i < 10; i++)
            {
                var bag = new BaggageItem()
                {
                    //Set properties on bags at checkin
                    FlightNumber = f.FlightNumber,
                    BaggageId = i.ToString(),
                    Weight = BagWeight + Rand.NextDouble() * 15,
                    Status = "Checked In",
                    LastScanned = DateTime.Now,
                    LastKnownLocation = "SFO",
                    Destination = "SEA",
                    Departure = "SFO"
                };

                await CosmosDataService.Instance("BagCollection").InsertItemAsync(bag);
            }
        }
#endregion SetupDatabase

#region CheckInBags
        // Check bag in at SFO Gate Check
        public static async Task CheckBagIn()
        {
            // Get the bags for the flight we're following
            var bagsForFlight = new FlightService().GetBagsForFlight(myFlightNumber).Result;

            if ((bagsForFlight == null) || (bagsForFlight.Count == 0)) 
            {
                Console.WriteLine("\n No bags found, try running the program again. \n");
            }
            else
            {
                // Use the existing bags and update them to the default state and send them to event hub
                foreach (var b in bagsForFlight)
                {
                    var index = 0;
                    Console.Write("Check In for bag: {0} flight: {1} \n", b.BaggageId, b.FlightNumber);

                    //Set properties on bags at checkin
                    bagsForFlight[index].Status = "Checked In";
                    bagsForFlight[index].LastScanned = DateTime.Now;
                    bagsForFlight[index].LastKnownLocation = "SFO";
                    bagsForFlight[index].Destination = "SEA";

                    var telemetryDataPoint = new
                    {
                        baggageId = bagsForFlight[index].BaggageId,
                        status = bagsForFlight[index].Status,
                        flightNumber = bagsForFlight[index].FlightNumber,
                        lastScanned = bagsForFlight[index].LastScanned,
                        lastKnownLocation = bagsForFlight[index].LastKnownLocation,
                        destination = bagsForFlight[index].Destination,
                        weight = bagsForFlight[index].Weight,
                        id = bagsForFlight[index].Id
                    };

                    // Encode the message
                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    // Send the message to IOT Hub
                    await _deviceClient.SendEventAsync(message);

                    // Wait so that they move through the process at a nice pace.
                    await Task.Delay(1000);

                    index++;
                }
            }

            return;
        }

        public async static Task ScanBagOntoPlane()
        {
            Console.WriteLine("\nRetrieving the bags from Cosmos... \n");
            
            // Wait 5 seconds for data to populate from first step
            await Task.Delay(5000);

            // Get the bags for the flight we're following
            var bagsForFlight = new FlightService().GetBagsForFlight(myFlightNumber).Result;

            if ((bagsForFlight != null) && (bagsForFlight.Count != 0))
            {
                var index = 0;

                // Use the existing bags and update them as they scan onto the plane
                foreach (var b in bagsForFlight)
                {
                    Console.Write("On plane for bag: {0} flight: {1} \n", b.BaggageId, b.FlightNumber);

                    //Update properties on bags that are being checked onto the place
                    bagsForFlight[index].Status = "On Plane";
                    bagsForFlight[index].LastScanned = DateTime.Now;
                    bagsForFlight[index].LastKnownLocation = "SFO";
                    bagsForFlight[index].Destination = "SEA";

                    var telemetryDataPoint = new
                    {
                        baggageId = bagsForFlight[index].BaggageId,
                        status = bagsForFlight[index].Status,
                        flightNumber = bagsForFlight[index].FlightNumber,
                        lastScanned = bagsForFlight[index].LastScanned,
                        lastKnownLocation = bagsForFlight[index].LastKnownLocation,
                        destination = bagsForFlight[index].Destination,
                        weight = bagsForFlight[index].Weight,
                        id = bagsForFlight[index].Id
                    };

                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    await _deviceClient.SendEventAsync(message);

                    await Task.Delay(1000);

                    index++;
                }
            }
            else
            {
                Console.WriteLine("No bags found in ScanBagOntoPlane(), please try again");            
            }

            return;
        }

        public async static Task ScanBagOffPlane()
        {
            Console.WriteLine("\nRetrieving the bags from Cosmos...\n");

            // Wait 5 seconds for data to make sure data is updated from previous step
            await Task.Delay(5000);

            // Get the bags for the flight we're following
            var bagsForFlight = new FlightService().GetBagsForFlight(myFlightNumber).Result;

            if ((bagsForFlight != null) && (bagsForFlight.Count != 0))
            {
                var index = 0;

                // Use the existing bags and update them as they scan onto the plane
                foreach (var b in bagsForFlight)
                {
                    Console.Write("Off plane for bag: {0} flight: {1} \n", b.BaggageId, b.FlightNumber);

                    //Update properties on bags that are being checked onto the place
                    bagsForFlight[index].Status = "Off Plane";
                    bagsForFlight[index].LastScanned = DateTime.Now;
                    bagsForFlight[index].LastKnownLocation = "SEA";
                    bagsForFlight[index].Destination = "SEA";

                    var telemetryDataPoint = new
                    {
                        baggageId = bagsForFlight[index].BaggageId,
                        status = bagsForFlight[index].Status,
                        flightNumber = bagsForFlight[index].FlightNumber,
                        lastScanned = bagsForFlight[index].LastScanned,
                        lastKnownLocation = bagsForFlight[index].LastKnownLocation,
                        destination = bagsForFlight[index].Destination,
                        weight = bagsForFlight[index].Weight,
                        id = bagsForFlight[index].Id
                    };

                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    await _deviceClient.SendEventAsync(message);

                    await Task.Delay(1000);

                    index++;
                }
            }
            else
            {
                Console.WriteLine("No bags found in ScanBagOffPlane(), please try again");
            }

            return;
        }

        public async static Task ScanBagOntoCarousel()
        {
            Console.WriteLine("\nRetrieving the bags from Cosmos...\n");

            // Wait 5 seconds for data to populate from first step
            await Task.Delay(5000);

            // Get the bags for the flight we're following
            var bagsForFlight = new FlightService().GetBagsForFlight(myFlightNumber).Result;

            if ((bagsForFlight != null) && (bagsForFlight.Count != 0))
            {
                var index = 0;

                // Use the existing bags and update them as they scan onto the plane
                foreach (var b in bagsForFlight)
                {
                    //Update properties on bags that are being checked onto the place
                    bagsForFlight[index].CarouselNumber = 7;
                    bagsForFlight[index].Status = "On Carousel";
                    bagsForFlight[index].LastScanned = DateTime.Now;
                    bagsForFlight[index].LastKnownLocation = "SEA";
                    bagsForFlight[index].Destination = "SEA";
                    
                    Console.Write("On Carousel number {0} for bag: {1} on flight {2} \n", b.CarouselNumber, b.BaggageId, b.FlightNumber);

                    var telemetryDataPoint = new
                    {
                        baggageId = bagsForFlight[index].BaggageId,
                        status = bagsForFlight[index].Status,
                        flightNumber = bagsForFlight[index].FlightNumber,
                        lastScanned = bagsForFlight[index].LastScanned,
                        lastKnownLocation = bagsForFlight[index].LastKnownLocation,
                        destination = bagsForFlight[index].Destination,
                        weight = bagsForFlight[index].Weight,
                        id = bagsForFlight[index].Id,
                        carouselNumber = bagsForFlight[index].CarouselNumber = 7
                };

                    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    await _deviceClient.SendEventAsync(message);

                    await Task.Delay(1000);

                    index++;
                }
            }
            else
            {
                Console.WriteLine("No bags found in ScanBagOntoCarousel(), please try again");
            }

            Console.WriteLine("\nfin.");
            return;
        }
    }
#endregion CheckInBags
}
