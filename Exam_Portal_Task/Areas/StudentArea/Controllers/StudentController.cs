using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;
namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class StudentController : Controller
    {
        // GET: StudentArea/Student
        Examdb_newEntities db=new Examdb_newEntities(); 
        public ActionResult Index()
        {
            List<tblStudent_details> lst=db.tblStudent_details.ToList();
            return View(lst);
        }
        [HttpPost]
        public string AddStudent(tblStudent_details st)
        {
            db.tblStudent_details.Add(st);
            db.SaveChanges();
            return "Student Added Successfully";
        }
    }
}