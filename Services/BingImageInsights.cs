using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZplusBot.Services
{
    public class BingImageInsights
    {
        public VisuallysimilarImage[] visuallySimilarImages { get; set; }
    }

    public class VisuallysimilarImage
    {
        public string name { get; set; }
        public string webSearchUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string contentSize { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string imageInsightsToken { get; set; }
      //  public Insightssourcessummary insightsSourcesSummary { get; set; }
        public string imageId { get; set; }
        public string accentColor { get; set; }
    }

   
}