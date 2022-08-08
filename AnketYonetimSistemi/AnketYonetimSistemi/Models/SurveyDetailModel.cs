using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnketYonetimSistemi.Models
{
    public class SurveyDetailModel
    {
        public int SurvId { get; set; }
        public string Title { get; set; }
        public bool isanonim { get; set; }
        public List<Question> questionliste =new List<Question>();
        public List<Response> responseliste =new List<Response>();


    }


}