using System;
using System.Collections.Generic;

namespace ContosoBaggage.Models
{
    public class Baggage
    {
        public Baggage()
        {
        }

        public string Status { get; set; }

        public string FlightNo { get; set;  }

        public DateTime LastScanned { get; set; }

        public string LastKnownLocation { get; set; }

        public string Message { get; set; }

        List<ScanHistory> ScanHistory { get; set; }
    }
}