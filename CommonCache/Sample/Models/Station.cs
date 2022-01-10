using System;

namespace Sample.Models
{
    [Serializable]
    public class Station
    {
        public Guid Id { get; set; }
        public string StationCode { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public DateTime ActiveDate { get; set; }
        public string RecordOrigin { get; set; }
    }

    [Serializable]
    public class StationList {
        public Guid Id { get; set; }
        public Station[] Stations { get; set; }
        public DateTime CacheExpiry { get; set; }
    }

}
