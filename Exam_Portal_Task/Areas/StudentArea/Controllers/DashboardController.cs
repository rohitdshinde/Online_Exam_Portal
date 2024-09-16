using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class DashboardController : Controller
    {
        // GET: StudentArea/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}