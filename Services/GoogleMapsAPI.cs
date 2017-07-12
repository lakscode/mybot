using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZplusBot.Services
{
    public class GoogleMapsAPI
    {
        public GoogleLocation[] results { get; set; }
    }

    public class GoogleLocation
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string vicinity { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
      //  public string viewport { get; set; }
    }

    public class Location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}