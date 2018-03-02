using SimulatedDevice.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimulatedDevice.Models;
using SimulatedDevice.Services;

namespace SimulatedDevice.DummyData
{
    class DummyData
    {
        public List<BaggageItem> CreateBags()
        {
            var bags = new List<BaggageItem>();

            for (int i = 0; i < 10; i++)
            {
                bags.Add(new BaggageItem()
                {
                    BaggageId = i.ToString(),
                    FlightNumber = string.Concat("FL123", i),
                });
            }

            return bags;
        }

        public List<Flight> CreateFlights()
        {
            var flights = new List<Flight>();

            for (int i = 0; i < 10; i++)
            {
                flights.Add(new Flight()
                {
                    FlightNumber = string.Concat("FL123", i),                    
                    Date = DateTime.Now.Date,
                    ArrivalAirportCode = "SEA",
                    ArrivalCity = "Seattle",
                    ArrivalTime = DateTime.Now.AddHours(2),
                    DepartureAirportCode = "SFO",
                    DepartureCity = "San Francisco",
                    DepartureTime = DateTime.Now,                   
                });
            }
            return flights;
        }
    }
}
