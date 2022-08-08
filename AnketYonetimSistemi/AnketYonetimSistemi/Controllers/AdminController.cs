using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnketYonetimSistemi.Controllers
{
    public class AdminController : Controller
    {
        UserManager um=new UserManager();
        CompanyManager cm=new CompanyManager();
        SurveyManager sm=new SurveyManager();
       UserSurveyManager usm=new UserSurveyManager();
        // GET: Admin
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


            var liste = sm.List().Where(x => x.IsCompletely == true);

            return View(liste.ToList());

        }


        

        public ActionResult AllCompanies()
        {
            var liste = cm.List().ToList();

            return View(liste);
        }

        public ActionResult Create(Survey model,List<int> liste)
        {
              
            model.IsCompletely = false;

            if (model!=null)
            {
                   sm.Insert(model);
            }
                
            foreach (int item in liste)
            {
                UserSurvey us=new UserSurvey();

                us.UserId = item;

                us.SurveyId = model.Id;

                usm.Insert(us);
            }


            return View();
        }



    }
}