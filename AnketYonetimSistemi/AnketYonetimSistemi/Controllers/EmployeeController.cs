using AnketYonetimSistemi.Current_Session;
using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnketYonetimSistemi.Controllers
{
    public class EmployeeController : Controller
    {
        SurveyManager sm=new SurveyManager();
        UserSurveyManager usm=new UserSurveyManager();
        // GET: Employee
        public ActionResult Index()
        {

            var liste1 = sm.List();

            foreach (var item in liste1)
            {
                if (item.IsCompletely == false)
                {
                    sm.Delete(item);
                }
            }


            List<Survey> liste=new List<Survey>();

            foreach (var item in usm.List().Where(x => x.UserId == CurrentSession.user.Id && x.IsPerCompletely==false))
            {
                foreach (var item2 in sm.List().Where(x => x.Id == item.SurveyId))
                {
                    if (item2.IsCompletely == true)
                    {
                        liste.Add(item2);
                    }

                }
            }

            return View(liste);
           
        }

    }
}