using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIPOC.Models
{
    public class RideModel
    {
        public string UserName { get; set; }

        public DateTime RideTime { get; set; }

        public Address From { get; set; }

        public Address To { get; set; }

        public Address[] Route { get; set; }

        public int Seats { get; set; }

        public string Vehicle { get; set; }

        public string VehicleNumber { get; set; }
    }

    public class Address
    {
        public string Society { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public Cordinates LatLong { get; set; }
    }

    public class Cordinates
    {
        public string Longitute { get; set; }

        public string Latitude { get; set; }
    }
}