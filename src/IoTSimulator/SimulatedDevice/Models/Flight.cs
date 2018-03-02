using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice.Models
{
    public class Flight : BaseModel
    {
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("arrivalAirportCode")]
        public string ArrivalAirportCode { get; set; }

        [JsonProperty("arrivalCity")]
        public string ArrivalCity { get; set; }

        [JsonProperty("arrivalTime")]
        public DateTime ArrivalTime { get; set; }

        [JsonProperty("departureAirportCode")]
        public string DepartureAirportCode { get; set; }

        [JsonProperty("departureCity")]
        public string DepartureCity { get; set; }

        [JsonProperty("departureTime")]
        public DateTime DepartureTime { get; set; }

        [JsonIgnore]
        public string Duration
        {
            get
            {
                var duration = ArrivalTime - DepartureTime;
                return duration.ToString("%h") + "h " + duration.ToString("%m") + "m";
            }
        }
    }
}
