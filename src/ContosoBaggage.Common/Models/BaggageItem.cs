using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ContosoBaggage.Common.Models
{
    /// <summary>
    /// Baggage item.
    /// </summary>
    public class BaggageItem : BaseModel
    {
        /// <summary>
        /// Gets or sets the baggage identifier.
        /// </summary>
        /// <value>The baggage identifier.</value>
        [JsonProperty("baggageId")]
        public string BaggageId { get; set; }

        /// <summary>
        /// Gets or sets the departure.
        /// </summary>
        /// <value>The departure.</value>
        [JsonProperty("departure")]
        public string Departure { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        [JsonProperty("destination")]
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>The device identifier.</value>
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>The flight number.</value>
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the scanned time.
        /// </summary>
        /// <value>The scanned time.</value>
        [JsonProperty("scannedTime")]
        public DateTime ScannedTime { get; set; }

        /// <summary>
        /// Gets or sets the scanner identifier.
        /// </summary>
        /// <value>The scanner identifier.</value>
        [JsonProperty("scannerId")]
        public string ScannerId { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        [JsonProperty("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the last scanned.
        /// </summary>
        /// <value>The last scanned.</value>
        [JsonProperty("lastScanned")]
        public DateTime LastScanned { get; set; }

        /// <summary>
        /// Gets or sets the last known location.
        /// </summary>
        /// <value>The last known location.</value>
        [JsonProperty("lastKnownLocation")]
        public string LastKnownLocation { get; set; }

        /// <summary>
        /// Gets or sets the carousel number.
        /// </summary>
        /// <value>The carousel number.</value>
        [JsonProperty("carouselNumber")]
        public int CarouselNumber { get; set; }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        [JsonIgnore]
        public string BagStatusIcon
        {
            get
            {
                switch (Status)
                {
                    case "Checked In":
                        return "checked.svg";
                    case "On Carousel":
                    default:
                        return "error.svg";
                }
            }
        }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        [JsonIgnore]
        public string BagStatusColor
        {
            get
            {
                switch (Status)
                {
                    case "Checked In":
                        return "checked.svg";
                    case "On Carousel":
                    default:
                        return "error.svg";
                }
            }
        }
    }
}
