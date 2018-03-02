using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimulatedDevice.Classes;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice.Services
{
    class FlightService
    {
        string _baseUrl = "https://appinnovationbackend.azurewebsites.net{0}";

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

        private Flight GetDummyFlight(string flightNumber, DateTime date)
        {
            return new Flight()
            {
                FlightNumber = flightNumber,
                Date = date,
                DepartureCity = "San Francisco, CA",
                DepartureAirportCode = "SFO",
                DepartureTime = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0),
                ArrivalAirportCode = "SEA",
                ArrivalCity = "Seattle, WA",
                ArrivalTime = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0),
            };
        }

        public async Task<List<BaggageItem>> GetBagsForFlight(string flightNumber)
        {
            List<BaggageItem> bagsForFlight = new List<BaggageItem>();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_baseUrl, "/api/getbaggageforflight?flightNumber=" + flightNumber));

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
