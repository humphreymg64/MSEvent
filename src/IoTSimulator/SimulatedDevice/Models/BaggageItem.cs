using Newtonsoft.Json;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice.Classes
{
    public class BaggageItem : BaseModel
    {

        [JsonProperty("baggageId")]
        public string BaggageId { get; set; }

        [JsonProperty("departure")]
        public string Departure { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("scannedTime")]
        public DateTime ScannedTime { get; set; }

        [JsonProperty("scannerId")]
        public string ScannerId { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lastScanned")]
        public DateTime LastScanned { get; set; }

        [JsonProperty("lastKnownLocation")]
        public string LastKnownLocation { get; set; }

        [JsonProperty("carouselNumber")]
        public int CarouselNumber { get; set; }
    }
}
