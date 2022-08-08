using AnketYonetimSistemi.Current_Session;
using AnketYonetimSistemi.Models;
using BLL;
using BLL.Result;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AnketYonetimSistemi.Controllers
{


    public class HomeController : Controller
    {
        UserManager um = new UserManager();
        CompanyManager cm = new CompanyManager();
        StatueModel statue = new StatueModel();
        SurveyManager sm = new SurveyManager();
        UserSurveyManager usm = new UserSurveyManager();
        UserResponseManager usrm = new UserResponseManager();
        QuestionManager qm = new QuestionManager();
        ResponseManager rm = new ResponseManager();

        public static List<SurveyDetailModel> liste = new List<SurveyDetailModel>();

       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<_User> bll = um.LoginUser(model);

                if (bll.Errors.Count > 0)
                {
                    bll.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<_User>("login", bll.Result);
                if (CurrentSession.user.IsManager)
                {
                    return RedirectToAction("Index", "Manager");
                }
                if (CurrentSession.user.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Employee");

            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Info(int id)
        {

            _User user = um.Find(x => x.Id == id);

            if (user != null)
            {
                return View(user);
            }

            return View();
        }

        public ActionResult UpdateUser(int id)
        {


            var user = um.Find(x => x.Id == id);

            ViewBag.CompanyId = new SelectList(cm.List(), "Id", "CompanyName");

            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(_User user)
        {


            if (ModelState.IsValid)
            {

                var liste = new List<_User>();


                foreach (var item in um.List().ToList())
                {
                    if (user.UserName == item.UserName || user.Password == item.Password)
                    {
                        if (user.Id != item.Id)
                        {
                            liste.Add(item);
                        }

                    }
                }
                if (liste.Count > 0)
                {
                    statue.key = false;
                    statue.title = "Güncelleme";

                    return RedirectToAction("Statue", "Home", statue);
                }

                _User User = um.Find(x => x.Id == user.Id);
                User.Name = user.Name;
                User.Surname = user.Surname;
                User.UserName = user.UserName;
                User.Password = user.Password;

                if (CurrentSession.user.IsAdmin)
                {
                    User.CompanyId = user.CompanyId;
                }

                um.Update(User);

                statue.key = true;
                statue.title = "Güncelleme";

                return RedirectToAction("Statue", "Home", statue);
            }

            ViewBag.CompanyId = new SelectList(cm.List(), "Id", "CompanyName");

            return View(user);
        }


        public ActionResult UpdateCompany(int id)
        {

            var company = cm.Find(x => x.Id == id);

            return View(company);
        }

        [HttpPost]
        public ActionResult UpdateCompany(Company company)
        {
            if (ModelState.IsValid)
            {

                var liste = new List<Company>();

                foreach (var item in cm.List())
                {
                    if (company.CompanyName == item.CompanyName)
                    {
                        liste.Add(item);

                    }
                }

                if (liste.Count > 0)
                {
                    statue.key = false;
                    statue.title = "Güncelleme";

                    return RedirectToAction("Statue", "Home", statue);
                }

                Company comp = cm.Find(x => x.Id == company.Id);
                comp.CompanyName = company.CompanyName;

                cm.Update(comp);

                statue.key = true;
                statue.title = "Güncelleme";
                return RedirectToAction("Statue", "Home", statue);

            }

            return View(company);

        }


        public ActionResult CompanyEmployees(int? id)
        {
            if (id == null)
            {
                var liste1 = new List<_User>();

                foreach (var item in um.List().ToList())
                {
                    if (!item.IsManager && !item.IsAdmin)
                    {
                        liste1.Add(item);
                    }
                }

                return View(liste1);
            }


            var liste2 = new List<_User>();

            foreach (var item in um.List().Where(x => x.company.Id == id).ToList())
            {
                if (!item.IsManager && !item.IsAdmin)
                {
                    liste2.Add(item);
                }
            }

            return View(liste2);
        }

        public ActionResult UserDelete(int id)
        {
            _User user = um.Find(u => u.Id == id);

            um.Delete(user);

            statue.key = true;
            statue.title = "Silme";

            return RedirectToAction("Statue", "Home", statue);
        }

        public ActionResult CompanyDelete(int id)
        {
            Company company = cm.Find(u => u.Id == id);

            cm.Delete(company);

            statue.key = true;
            statue.title = "Silme";

            return RedirectToAction("Statue", "Home", statue);
        }

        public ActionResult Statue(StatueModel model)
        {
            return View(model);
        }

        public ActionResult AddPersonel()
        {

            ViewBag.CompanyId = new SelectList(cm.List(), "Id", "CompanyName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPersonel(_User user)
        {
            user.Password = user.UserName;

            if (ModelState.IsValid)
            {


                foreach (var item in um.List().ToList())
                {
                    if (user.UserName == item.UserName)
                    {
                        statue.key = false;
                        statue.title = "Kayıt";

                        return RedirectToAction("Statue", "Home", statue);
                    }
                }

                if (user.CompanyId == 0 && !CurrentSession.user.IsAdmin)
                {
                    user.CompanyId = CurrentSession.user.CompanyId;
                }

                um.Insert(user);

                statue.key = true;
                statue.title = "Kayıt";

                return RedirectToAction("Statue", "Home", statue);


            }

            return View();
        }

        public ActionResult AddCompany()
        {
            ViewBag.CompanyId = new SelectList(cm.List(), "Id", "CompanyName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompany(Company comp)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in cm.List().ToList())
                {
                    if (comp.CompanyName == item.CompanyName)
                    {
                        statue.key = false;
                        statue.title = "Kayıt";

                        return RedirectToAction("Statue", "Home", statue);
                    }
                }

                cm.Insert(comp);

                statue.key = true;
                statue.title = "Kayıt";

                return RedirectToAction("Statue", "Home", statue);




            }
            return View();
        }

        public ActionResult CompletelySurvey(int? id)
        {

            List<Survey> surveyliste = new List<Survey>();

            List<_User> userliste = new List<_User>();

           

            if (id != null)
            {
                if (CurrentSession.user.IsAdmin)
                {
                    var list=usm.List().Where(x=> x.IsPerCompletely==true).ToList();

                    foreach (var item in list)
                    {
                        var survey = sm.Find(x=> x.Id==item.SurveyId);

                        if (survey.IsCompletely==true && !surveyliste.Contains(survey))
                        {
                          surveyliste.Add(survey);
                        }

                        
                    }

                    return View(surveyliste);

                }

                else if (CurrentSession.user.IsManager)
                {
                    var user = um.Find(x => x.Id == id);

                    foreach (var item in um.List().Where(x => x.CompanyId == user.CompanyId))
                    {
                        foreach (var item2 in usm.List().Where(x => x.UserId == item.Id && x.IsPerCompletely==true))
                        {
                            foreach (var item3 in sm.List().Where(x => x.Id == item2.SurveyId))
                            {
                                if (item3.IsCompletely == true && item3.IsAnonim == false)
                                {
                                    if (!surveyliste.Contains(item3))
                                    {
                                        surveyliste.Add(item3);
                                    }


                                }
                            }


                        }

                    }

                    return View(surveyliste);
                }

                foreach (var item in usm.List().Where(x => x.UserId == id && x.IsPerCompletely==true))
                {
                    foreach (var item2 in sm.List().Where(x => x.Id == item.SurveyId))
                    {
                        if (item2.IsCompletely == true)
                        {
                            surveyliste.Add(item2);
                        }

                    }
                }

                return View(surveyliste);

            }

            return View();

        }

        public ActionResult CreateSurvey()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSurvey(Survey model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            sm.Insert(model);


            TempData["survid"] = model.Id;

            return RedirectToAction("SurveyEmp", "Home");
        }

        public ActionResult SurveyDetail(int id,int userid)
        {
            SurveyDetailModel sdm = new SurveyDetailModel();

            var anket = sm.Find(x => x.Id == id);

            if (anket.IsCompletely == true)
            {
                sdm.SurvId = anket.Id;
                sdm.Title = anket.SurveyTitle;
                sdm.isanonim = anket.IsAnonim;

                foreach (var item in qm.List().Where(x => x.SurveyId == id))
                {

                    sdm.questionliste.Add(item);  

                    foreach (var item2 in rm.List().Where(x => x.QuestionId == item.Id))
                      {
                    
                      foreach (var item3 in usrm.List().Where(x => x.UserId == userid))
                      {
                    
                            if (item3.ResponseId==item2.Id)
                            {
                              sdm.responseliste.Add(item2);
                            }
                      }
                    }

                    

                }

            }

            return View(sdm);


        }


        public ActionResult SurveyEmp()
        {

            ViewBag.Id = new SelectList(cm.List().Where(x=>x.Id!=1), "Id", "CompanyName");

            return View();
        }


        [HttpPost]
        public ActionResult AddEmp(int id)
        {
            var liste = um.List().Where(x => x.CompanyId == id).ToList();

            return View(liste);
        }

        public ActionResult SurvQues(List<string> list, int id)
        {


            foreach (var item in list)
            {
                UserSurvey us = new UserSurvey();

                us.UserId = Convert.ToInt32(item);

                us.SurveyId = id;

                usm.Insert(us);

            }


            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddQuestion(int id, string text)
        {
            Question question = new Question();
            question.QuestionText = text;
            question.SurveyId = id;

            qm.Insert(question);

            var id_ = question.Id;

            return Json(id_, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteSurv(int id)
        {
            var anket = sm.Find(x => x.Id == id);

            sm.Delete(anket);

            return Json(id, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AddResponse(int id, string cevap)
        {
            Response response = new Response();

            response.QuestionId = id;
            response.Comment = cevap;
            response.IsMarked = false;

            rm.Insert(response);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SurveyFinish(int id)
        {

            var anket = sm.Find(x => x.Id == id);

            anket.IsCompletely = true;

            sm.Update(anket);

            string mesaj = "Anket Başarılı Bir Şekilde Oluşturulmuştur";


            return Json(mesaj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompletePerson(int id)
        {
            TempData["anketid"] = id;

            List<int> liste = new List<int>();
            List<_User> userlist = new List<_User>();


            if (CurrentSession.user.IsAdmin)
            {
                foreach (var item in usm.List().Where(x => x.SurveyId == id && x.IsPerCompletely==true).ToList())
                {
                    liste.Add(item.UserId);
                }
                if (liste != null)
                {
                    foreach (var item in liste)
                    {
                        var user = um.Find(x => x.Id == item);
                        userlist.Add(user);
                    }

                    return View(userlist);
                }
            }
            else if (CurrentSession.user.IsManager)
            {
                foreach (var item in usm.List().Where(x => x.SurveyId == id && x.IsPerCompletely==true).ToList())
                {
                    liste.Add(item.UserId);
                }

                if (liste != null)
                {
                    foreach (var item in liste)
                    {
                        var user = um.Find(x => x.Id == item);

                        if (user.CompanyId == CurrentSession.user.CompanyId)
                        {
                            userlist.Add(user);
                        }

                    }

                    return View(userlist);
                }

            }




            return View();
        }

        public ActionResult GetSurvey(int id)
        {
            SurveyDetailModel sdm = new SurveyDetailModel();

            var anket = sm.Find(x => x.Id == id);

            if (anket.IsCompletely == true)
            {
                sdm.SurvId = anket.Id;
                sdm.Title = anket.SurveyTitle;
                sdm.isanonim = anket.IsAnonim;

                foreach (var item in qm.List().Where(x => x.SurveyId == id))
                {

                    sdm.questionliste.Add(item);

                    foreach (var item2 in rm.List().Where(x => x.QuestionId == item.Id))
                    {

                        sdm.responseliste.Add(item2);


                    }

                }

            }



            return View(sdm);

        }

        public ActionResult Saveresponse(int id)
        {

            var response = rm.Find(x => x.Id == id);

            response.IsMarked = true;

            rm.Update(response);

            UserResponse usres = new UserResponse();

            usres.ResponseId = response.Id;
            usres.UserId = CurrentSession.user.Id;

            usrm.Insert(usres);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Removeresponse(int id)
        {

            var response = rm.Find(x => x.Id == id);

            response.IsMarked = true;

            rm.Update(response);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Persavesurvey(int id)
        {

            var usersurvey = usm.Find(x => x.SurveyId==id && x.UserId==CurrentSession.user.Id);

            usersurvey.IsPerCompletely= true;

            usm.Update(usersurvey);


            string statue;

            if (CurrentSession.user.IsAdmin)
            {
                statue = "Admin";
            }
            else if (CurrentSession.user.IsManager)
            {
                statue = "Manager";
            }
            else
            {
                statue = "Employee";
            }

            return Json(statue, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Dashboard(int id)
        {
            DashboardModel dbm=new DashboardModel();

            var survey=sm.Find(x => x.Id==id);

            var liste = usm.List(x => x.SurveyId == id && x.IsPerCompletely == true);
            var list = usm.List(x => x.SurveyId == id);

            var liste1 = qm.List(x => x.SurveyId == id);

                var liste2 = new List<Response>();
                var liste3 = new List<UserResponse>();

            foreach (var item1 in liste1)
            {
                foreach (var item2 in rm.List().Where(x=> x.QuestionId==item1.Id))
                {
                    liste2.Add(item2);
                }
            }

            var resplist = new List<Response>();

                foreach (var item1 in liste1)
                {
                    foreach (var item2 in rm.List(x => x.QuestionId==item1.Id && x.IsMarked==true))
                    {

                    resplist.Add(item2);
               
                    }
                }
                
            var resliste=new List<UserResponse>();

            foreach (var item1 in usm.List().Where(x=> x.SurveyId==id))
            {
                foreach (var item2 in resplist)
                {
                    foreach (var item in usrm.List(x=> x.UserId==item1.UserId && x.ResponseId==item2.Id))
                    {
                        resliste.Add(item);
                    }
                }
            }

                dbm.TotalPerCount=list.Count;
                dbm.CompletePerCount = liste.Count;
                dbm.TotalResponseCount=liste2.Count*list.Count;
                dbm.MarkedResponseCount=resliste.Count;

                

            return View(dbm);
        }

        public ActionResult AdminSurveyDetail(int id)
        {
            SurveyDetailModel sdm = new SurveyDetailModel();

            var anket = sm.Find(x => x.Id == id);

            if (anket.IsCompletely == true)
            {
                sdm.SurvId = anket.Id;
                sdm.Title = anket.SurveyTitle;
                sdm.isanonim = anket.IsAnonim;

                foreach (var item in qm.List().Where(x => x.SurveyId == id))
                {

                    sdm.questionliste.Add(item);

                    foreach (var item2 in rm.List().Where(x => x.QuestionId == item.Id))
                    {

                        sdm.responseliste.Add(item2);


                    }

                }

            }
            return View(sdm);
        }
    }
}