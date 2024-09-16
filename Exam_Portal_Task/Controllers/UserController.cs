using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;

namespace Exam_Portal_Task.Controllers
{
    public class UserController : Controller
    {
        Examdb_newEntities db;
        public UserController()
        {       
            db = new Examdb_newEntities();
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email_address,string password)
        {
            tblStudent_details sd = db.tblStudent_details.ToList().FirstOrDefault(e => e.Email_address.Equals(email_address));
            if (sd != null)
            {
                Session["student_id"] = sd.Student_id;
                Session["student"] = sd;
                Session.Timeout = 10;
                return Redirect("/Exam/");
            }
            else
            {
                ViewBag.msg = "Invalid Email Address";
                return View();

            }
        }
    }
}