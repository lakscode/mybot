using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZplusBot.Services
{
    public class BingWebInsights
    {
        public SimilarTextSearch[] similarTextSearch { get; set; }
    }

    public class SimilarTextSearch
    {
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
     //   public DateTime dateLastCrawled { get; set; }
        public string snippet { get; set; }
      
    }

   
}