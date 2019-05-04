using CrystalDecisions.CrystalReports.Engine;
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


        public ActionResult exportReportStudent()
        {
            SmartSchoolEntities1 db = new SmartSchoolEntities1();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport.rpt"));
            rd.SetDataSource(db.Students.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "StudentList.pdf");

            }
            catch
            {
                throw;
            }
        }

        public ActionResult exportReportParent()
        {
            SmartSchoolEntities1 db = new SmartSchoolEntities1();

            ReportDocument rd = new ReportDocument();
            var ParentList = db.Parents.ToList();

            List<ParentViewModel> List = new List<ParentViewModel>();
            foreach (var a in ParentList)
            {
                ParentViewModel par = new ParentViewModel();
                par.Name = a.Name;
                par.Contact = a.Contact;
                par.NIC = a.NIC;
                par.Email = a.Email;
                par.UserName = a.UserName;
                par.Password = a.Password;
                par.Id = a.Id;
                List.Add(par);
            }
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport1.rpt"));
            rd.SetDataSource(List);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "ParentList.pdf");

            }
            catch
            {
                throw;
            }
        }

        public ActionResult exportReportHostel()
        {
            SmartSchoolEntities1 db = new SmartSchoolEntities1();

            ReportDocument rd = new ReportDocument();
            var allHostels = db.Hostels.ToList();
            List<HostelViewModel> list = new List<HostelViewModel>();
            foreach (var a in allHostels)
            {
                HostelViewModel h = new HostelViewModel();
                h.HostelName = a.Name;
                h.HostelLocation = a.Location;
                h.Rent = Convert.ToInt32(a.Rent);
                h.Id = a.Id;
                list.Add(h);
            }
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(list);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "HostelList.pdf");

            }
            catch
            {
                throw;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult AddNews()
        {
            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddNews(NewsViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            News news = new News();
            news.Description = model.Description;
            news.Title = model.Title;
            news.Date = model.Date;

            ent.News.Add(news);
            ent.SaveChanges();
            return RedirectToAction("Account");

        }

        public ActionResult AddEvent()
        {
            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddEvent(EventViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            Event eve = new Event();
            eve.Description = model.Description;
            eve.Date = model.Date;

            ent.Events.Add(eve);
            ent.SaveChanges();
            return RedirectToAction("Account");

        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddStudent(StudentViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
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

        public ActionResult AllStudent()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var StudentList = ent.Students.ToList();

            List<StudentViewModel> List = new List<StudentViewModel>();
            foreach(var a in StudentList)
            {
                StudentViewModel stu = new StudentViewModel();
                stu.Name = a.Name;
                stu.Id = a.Id;
                stu.Contact = a.Contact;
                stu.RegisterationNumber = a.RegisterationNumber;
                stu.Email = a.Email;
                stu.UserName = a.UserName;
                stu.Password = a.Password;
                List.Add(stu);
            }

            return View(List);
        }

        public ActionResult UpdateStudent(int id)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var s = ent.Students.Where(x => x.Id == id).First();
            StudentViewModel student = new StudentViewModel();
            student.Name = s.Name;
            student.Contact = s.Contact;
            student.Email = s.Email;
            student.RegisterationNumber = s.RegisterationNumber;
            student.UserName = s.UserName;
            student.Password = s.Password;
            return View(student);
        }

        [HttpPost]
        public ActionResult UpdateStudent(int id, StudentViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            Student student = ent.Students.Where(x => x.Id == id).First();
            student.Name = model.Name;
            student.Contact = model.Contact;
            student.Email = model.Email;
            student.RegisterationNumber = model.RegisterationNumber;
            student.UserName = model.UserName;
            student.Password = model.Password;

            ent.SaveChanges();

            return RedirectToAction("AllStudent");
        }

        public ActionResult DeleteStudent(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteStudent(int id, StudentViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                SmartSchoolEntities1 ent = new SmartSchoolEntities1();
                var student = ent.Students.Where(x => x.Id == id).First();
                ent.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();

                return RedirectToAction("AllStudent");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AllParent()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var ParentList = ent.Parents.ToList();

            List<ParentViewModel> List = new List<ParentViewModel>();
            foreach (var a in ParentList)
            {
                ParentViewModel par = new ParentViewModel();
                par.Name = a.Name;
                par.Contact = a.Contact;
                par.NIC = a.NIC;
                par.Email = a.Email;
                par.UserName = a.UserName;
                par.Password = a.Password;
                par.Id = a.Id;
                List.Add(par);
            }

            return View(List);
        }

        public ActionResult AddParent()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
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
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
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


        public ActionResult UpdateParent(int id)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var p = ent.Parents.Where(x => x.Id == id).First();
            ParentViewModel parent = new ParentViewModel();
            parent.Name = p.Name;
            parent.Contact = p.Contact;
            parent.Email = p.Email;
            parent.UserName = p.UserName;
            parent.Password = p.Password;
            parent.NIC = p.NIC;
            return View(parent);
        }

        [HttpPost]
        public ActionResult UpdateParent(int id, ParentViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var parent = ent.Parents.Where(x => x.Id == id).First();
            parent.Name = model.Name;
            parent.Contact = model.Contact;
            parent.Email = model.Email;
            parent.UserName = model.UserName;
            parent.Password = model.Password;
            parent.NIC = model.NIC;

            ent.SaveChanges();

            return RedirectToAction("AllParent");
        }

        public ActionResult DeleteParent(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteParent(int id, ParentViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                SmartSchoolEntities1 ent = new SmartSchoolEntities1();
                var parent = ent.Parents.Where(x => x.Id == id).First();
                ent.Entry(parent).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();

                return RedirectToAction("AllParent");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddHostel()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddHostel(HostelViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            Hostel h = new Hostel();
            h.Name = model.HostelName;
            h.Location = model.HostelLocation;
            h.Rent = model.Rent;
            
            ent.Hostels.Add(h);
            ent.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AllHostel()
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var allHostels = ent.Hostels.ToList();
            List<HostelViewModel> list = new List<HostelViewModel>();
            foreach(var a in allHostels)
            {
                HostelViewModel h = new HostelViewModel();
                h.HostelName = a.Name;
                h.HostelLocation = a.Location;
                h.Rent =Convert.ToInt32(a.Rent);
                h.Id = a.Id;
                list.Add(h);
            }
            return View(list);
        }

        public ActionResult UpdateHostel(int id)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            var h = ent.Hostels.Where(x => x.Id == id).First();
            HostelViewModel hostel = new HostelViewModel();
            hostel.HostelName = h.Name;
            hostel.Rent =Convert.ToInt32(h.Rent);
            hostel.HostelLocation = h.Location;
            return View(hostel);
        }

        [HttpPost]
        public ActionResult UpdateHostel(int id,HostelViewModel model)
        {
            SmartSchoolEntities1 ent = new SmartSchoolEntities1();
            Hostel h = ent.Hostels.Where(x=>x.Id == id).First();
            h.Name = model.HostelName;
            h.Location = model.HostelLocation;
            h.Rent = model.Rent;

            
            ent.SaveChanges();

            return View();
        }

        public ActionResult DeleteHostel(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteHostel(int id, HostelViewModel collection)
        {
            try
            {
                // TODO: Add delete logic here
                SmartSchoolEntities1 ent = new SmartSchoolEntities1();
                var hostel = ent.Hostels.Where(x => x.Id == id).First();
                ent.Entry(hostel).State = System.Data.Entity.EntityState.Deleted;
                ent.SaveChanges();
                
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

       
    }
}
