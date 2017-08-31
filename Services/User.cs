using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoplusBot.Services
{


    public class jsonResponseObj
    {
        public jsonDocuments documents { get; set; }
        public string errors { get; set; }
    }


    public class jsonDocuments
    {
        public string id { get; set; }
        public jsonDetectedLanguages detectedLanguages { get; set; }
    }

    public class jsonDetectedLanguages
    {
        public string name { get; set; }
        public string iso6391Name { get; set; }
        public string score { get; set; }
    }



    public class User
    {
    }



    public class UserDetailsresults
    {
        public UserDetails result { get; set; }
    }

    public class UserDetails
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string assetId { get; set; }

    }


    public class QnADetailsresults
    {
        public QnADetails result { get; set; }
    }

    public class QnADetails
    {
        public string id { get; set; }
        public string userName { get; set; }
        public QuestionNAnswers[] questionNAnswers { get; set; }
    }



    public class QuestionNAnswers
    {
        public string id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
    public class QnAResult
        {
        public string result { get; set; }
    }

}