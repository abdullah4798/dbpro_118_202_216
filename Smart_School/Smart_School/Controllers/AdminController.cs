using Smart_School.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Smart_School.Controllers
{
    public class AdminController : Controller
    {
        private const string V = "sohaibarif28@gmail.com";

        // GET: Admin
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddStudent(StudentViewModel model)
        {
            SmartSchoolEntities ent = new SmartSchoolEntities();
            Student s = new Student();

            s.Name = model.Name;
            s.Contact = model.Contact;
            s.RegisterationNumber = model.RegisterationNumber;
            s.Email = model.Email;
            s.UserName = model.UserName;
            s.Password = model.Password;

            using (MailMessage mm = new MailMessage(V, model.Email))
            {
                mm.Subject = "Hello There!";
                mm.Body = "UserName : " + model.UserName + " || Password : " + model.Password;
                
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("abdullahn804@gmail.com", "codinginside");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    
                }
            }



            

            ent.Students.Add(s);
            ent.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddParent()
        {
            SmartSchoolEntities ent = new SmartSchoolEntities();
            var listofStudents = ent.Students.ToList();

            List<string> dropdownList = new List<string>();

            foreach(var a in listofStudents)
            {
                dropdownList.Add(a.Name);
            }

            dropdownList.Sort();
            ViewBag.list = new SelectList(dropdownList);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddParent(ParentViewModel model)
        {
            SmartSchoolEntities ent = new SmartSchoolEntities();
            Parent s = new Parent();

            s.Name = model.Name;
            s.Contact = model.Contact;
            s.NIC = model.NIC;
            s.Email = model.Email;
            s.UserName = model.UserName;
            s.Password = model.Password;

            using (MailMessage mm = new MailMessage(V, model.Email))
            {
                mm.Subject = "Hello There!";
                mm.Body = "UserName : " + model.UserName + " || Password : " + model.Password;

                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("abdullahn804@gmail.com", "codinginside");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                }
            }

            var Students = ent.Students.ToList();
            foreach(var a in Students)
            {
                if(a.Name == model.StudentName)
                {
                    s.StudentId = a.Id;
                }
            }





            ent.Parents.Add(s);
            ent.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
