using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZplusBot.Services
{
    public class GoogleLatLng
    {
        public GoogleDetails[] results { get; set; }
    }

    public class GoogleDetails
    {
        public string formatted_address { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
        public Geometry geometry { get; set; }
    }

}