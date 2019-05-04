using Smart_School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_School.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Login(string result)
        {
            ViewBag.Result = result;
            return View();
        }


        public ActionResult ViewNews()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var List = ent.News.ToList();
            List<NewsViewModel> NewsList = new List<NewsViewModel>();
            foreach(var a in List)
            {
                NewsViewModel n = new NewsViewModel();
                n.Description = a.Description;
                n.Title = a.Title;
                n.Date =Convert.ToDateTime(a.Date);
                NewsList.Add(n);
            }
            return View(NewsList);
        }

        public ActionResult ViewEvents()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var List = ent.Events.ToList();
            List<EventViewModel> EventList = new List<EventViewModel>();
            foreach (var a in List)
            {
                EventViewModel n = new EventViewModel();
                n.Description = a.Description;
                
                n.Date = Convert.ToDateTime(a.Date);
                EventList.Add(n);
            }
            return View(EventList);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(StudentViewModel collection)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var Students = ent.Students.ToList();
            foreach(var a in Students)
            {
                if(collection.UserName == a.UserName && collection.Password == a.Password)
                {
                    HelperClass.personLogged = "Student";
                    HelperClass.LoginUserId = a.Id;
                    return RedirectToAction("Account");
                }
            }
            string result = "Ivalid Login Attempt!";
            return RedirectToAction("Login", "Student", new { Message = result });
        }
        

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
