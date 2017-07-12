using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZplusBot.Services
{
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