using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ContosoBaggage.Common.Models
{
    /// <summary>
    /// Flight.
    /// </summary>
    public class Flight : BaseModel
    {
        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>The flight number.</value>
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the flight date.
        /// </summary>
        /// <value>The flight date.</value>
        [JsonProperty("date")]
        public DateTime FlightDate { get; set; }

        /// <summary>
        /// Gets or sets the arrival airport code.
        /// </summary>
        /// <value>The arrival airport code.</value>
        [JsonProperty("arrivalAirportCode")]
        public string ArrivalAirportCode { get; set; }

        /// <summary>
        /// Gets or sets the arrival city.
        /// </summary>
        /// <value>The arrival city.</value>
        [JsonProperty("arrivalCity")]
        public string ArrivalCity { get; set; }

        /// <summary>
        /// Gets or sets the arrival time.
        /// </summary>
        /// <value>The arrival time.</value>
        [JsonProperty("arrivalTime")]
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the departure airport code.
        /// </summary>
        /// <value>The departure airport code.</value>
        [JsonProperty("departureAirportCode")]
        public string DepartureAirportCode { get; set; }

        /// <summary>
        /// Gets or sets the departure city.
        /// </summary>
        /// <value>The departure city.</value>
        [JsonProperty("departureCity")]
        public string DepartureCity { get; set; }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>The departure time.</value>
        [JsonProperty("departureTime")]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the boarding time.
        /// </summary>
        /// <value>The boarding time.</value>
        [JsonProperty("boardingTime")]
        public DateTime BoardingTime { get; set; }

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>The airline.</value>
        [JsonProperty("airline")]
        public string Airline { get; set; }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        [JsonProperty("flightStatus")]
        public string FlightStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the gate.
        /// </summary>
        /// <value>The gate.</value>
        [JsonProperty("gate")]
        public int Gate { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>The zone.</value>
        [JsonProperty("zone")]
        public int Zone { get; set; }

        /// <summary>
        /// Gets or sets the number of passengers.
        /// </summary>
        /// <value>The number of passengers.</value>
        [JsonProperty("numberOfPassengers")]
        public int NumberOfPassengers { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        [JsonIgnore]
        public string Duration
        {
            get {
                var duration = ArrivalTime - DepartureTime;
                return duration.ToString("%h") + "h " + duration.ToString("%m") + "m";
            }
        }

        /// <summary>
        /// Gets the flight status description.
        /// </summary>
        /// <value>The flight status description.</value>
        [JsonIgnore]
        public string FlightStatus
        {
            get
            {
                switch (FlightStatusCode)
                {
                    // awaiting take off
                    case "ATO":
                        return "AWAITING TAKE OFF";
                    // boarding
                    case "BOA":
                        return "BOARDING";
                    // At gate
                    case "AG":
                    default:
                        return "AT GATE";
                }
            }
        }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        [JsonIgnore]
        public string FlightStatusIcon
        {
            get
            {
                switch (FlightStatusCode)
                {
                    // awaiting take off
                    case "ATO":
                        return "awaiting-take-off.svg";
                    // boarding
                    case "BOA":
                        return "boarding.svg";
                    // At gate
                    case "AG":
                    default:
                        return "gate.svg";
                }
            }
        }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        [JsonIgnore]
        public string FlightStatusColor
        {
            get
            {
                switch (FlightStatusCode)
                {
                    // awaiting take off
                    case "ATO":
                        return "#1d7223";
                    // boarding
                    case "BOA":
                        return "#B00000";
                    // At gate
                    case "AG":
                    default:
                        return "#77767b";
                }
            }
        }
    }
}
