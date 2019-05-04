using CrystalDecisions.CrystalReports.Engine;
using Smart_School.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smart_School.Controllers
{

    

    public class ParentController : Controller
    {

        public ActionResult exportReportNews()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();

            ReportDocument rd = new ReportDocument();
            var List = ent.News.ToList();
            List<NewsViewModel> NewsList = new List<NewsViewModel>();
            foreach (var a in List)
            {
                NewsViewModel n = new NewsViewModel();
                n.Id = a.Id;
                n.Description = a.Description;
                n.Title = a.Title;
                n.Date = Convert.ToDateTime(a.Date);
                NewsList.Add(n);
            }
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport3.rpt"));
            rd.SetDataSource(NewsList);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "NewsList.pdf");

            }
            catch
            {
                throw;
            }
        }

        public ActionResult exportReportEvents()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();

            ReportDocument rd = new ReportDocument();
            var List = ent.Events.ToList();
            List<EventViewModel> EventList = new List<EventViewModel>();
            foreach (var a in List)
            {
                EventViewModel n = new EventViewModel();

                n.Description = a.Description;

                n.Date = Convert.ToDateTime(a.Date);
                EventList.Add(n);
            }
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport4.rpt"));
            rd.SetDataSource(EventList);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "EventList.pdf");

            }
            catch
            {
                throw;
            }
        }
        // GET: Parent
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

        public ActionResult ViewNews()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var List = ent.News.ToList();
            List<NewsViewModel> NewsList = new List<NewsViewModel>();
            foreach (var a in List)
            {
                NewsViewModel n = new NewsViewModel();
                n.Description = a.Description;
                n.Title = a.Title;
                n.Date = Convert.ToDateTime(a.Date);
                NewsList.Add(n);
            }
            return View(NewsList);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(ParentViewModel collection)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var Students = ent.Parents.ToList();
            foreach (var a in Students)
            {
                if (collection.UserName == a.UserName && collection.Password == a.Password)
                {
                    HelperClass.personLogged = "Parent";
                    HelperClass.LoginUserId = a.Id;
                    return RedirectToAction("Account");
                }
            }
            string result = "Ivalid Login Attempt!";
            return RedirectToAction("Login", "Parent", new { Message = result });
        }

        // GET: Parent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Parent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parent/Create
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

        // GET: Parent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Parent/Edit/5
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

        // GET: Parent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parent/Delete/5
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
