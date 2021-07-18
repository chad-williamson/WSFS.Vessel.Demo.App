using System;

namespace Models
{
    public class VesselLocation
    {
        public long Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }
    }

}
