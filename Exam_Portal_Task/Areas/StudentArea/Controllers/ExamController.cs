using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class ExamController : Controller
    {
        // GET: StudentArea/Exam
        public ActionResult Index()
        {
            return View();
        }
    }
}