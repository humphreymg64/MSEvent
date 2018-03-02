using System;

namespace ContosoBaggage.Models
{
    public class ScanHistory
    {
        public ScanHistory()
        {
        }

        public DateTime Scanned { get; set; }

        public string ScannedLocation { get; set; }

        public string Message { get; set; }
    }
}