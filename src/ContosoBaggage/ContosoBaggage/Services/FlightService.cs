using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ContosoBaggage.Common.Models;

namespace ContosoBaggage.Services
{
    /// <summary>
    /// Flight service.
    /// </summary>
    public class FlightService
    {
        /// <summary>
        /// The base URL.
        /// </summary>
        string _baseUrl = "https://appinnovationbackend.azurewebsites.net{0}";

        /// <summary>
        /// Gets the flights.
        /// </summary>
        /// <returns>The flights.</returns>
        public async Task<List<Flight>> GetFlights()
        {
            var flights = new List<Flight>();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_baseUrl, "/api/getflights"));

            try
            {
                HttpResponseMessage requestResult = await client.SendAsync(request);

                var responseText = await requestResult.Content.ReadAsStringAsync();

                flights = DeserializeResponse<List<Flight>>(responseText);
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
            
            return flights;
        }

        /// <summary>
        /// Gets the dummy flight.
        /// </summary>
        /// <returns>The dummy flight.</returns>
        /// <param name="flightNumber">Flight number.</param>
        /// <param name="date">Date.</param>
        private Flight GetDummyFlight(string flightNumber, DateTime date)
        {
            return new Flight()
            {
                FlightNumber = flightNumber,
                FlightDate = date,
                DepartureCity = "San Francisco, CA",
                DepartureAirportCode = "SFO",
                DepartureTime = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0),
                ArrivalAirportCode = "SEA",
                ArrivalCity = "Seattle, WA",
                ArrivalTime = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0),
            };
        }

        /// <summary>
        /// Gets the bags for flight.
        /// </summary>
        /// <returns>The bags for flight.</returns>
        /// <param name="flightNo">Flight no.</param>
        public async Task<List<BaggageItem>> GetBagsForFlight(string flightNo)
        {
            List<BaggageItem> bagsForFlight = new List<BaggageItem>();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_baseUrl, "/api/getbaggageforflight?flightNumber=" + flightNo));

            try
            {
                HttpResponseMessage requestResult = await client.SendAsync(request);

                var responseText = await requestResult.Content.ReadAsStringAsync();

                bagsForFlight = DeserializeResponse<List<BaggageItem>>(responseText);
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }

            return bagsForFlight;
        }

        /// <summary>
        /// Deserializes the response.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="jsonResponse">Json response.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static T DeserializeResponse<T>(string jsonResponse)
        {
            return DeserializeResponse<T>(jsonResponse, string.Empty);
        }

        /// <summary>
        /// Accepts a JSON string and deserializes it to a given object of type T
        /// </summary>
        /// <typeparam name="T">Type of the parameter to add</typeparam>
        /// <param name="jsonResponse">JSON data to deserialize</param>
        /// <param name="rootNode">Name of the root node (if any) to grab the data to deserialize</param>
        /// <returns></returns>
        private static T DeserializeResponse<T>(string jsonResponse, string rootNode)
        {
            var returnObject = Activator.CreateInstance<T>();

            if (!string.IsNullOrEmpty(rootNode)) jsonResponse = JObject.Parse(jsonResponse)[rootNode].ToString();
            returnObject = JsonConvert.DeserializeObject<T>(jsonResponse);

            return returnObject;
        }
    }
}
