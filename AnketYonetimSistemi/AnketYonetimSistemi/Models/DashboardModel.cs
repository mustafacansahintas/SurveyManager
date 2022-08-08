using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnketYonetimSistemi.Models
{
    public class DashboardModel
    {
        public int TotalPerCount { get; set; }
        public int CompletePerCount { get; set; }
        public int QuestionCount { get; set; }
        public int TotalResponseCount { get; set; }
        public int MarkedResponseCount { get; set; }
     
        List<userinfo> liste=new List<userinfo>();

        public void usersurveyinfo(string Name,String Surname,int count)
        {
            userinfo userinfo=new userinfo();

            userinfo.Name = Name;
            userinfo.Surname = Surname; 
            userinfo.UserMarkedResponseCount = count;

            liste.Add(userinfo);
        }


    }

    public class userinfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UserMarkedResponseCount { get; set; }
    }

}