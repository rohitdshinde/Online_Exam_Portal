using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;
namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class Topic_ContentController : Controller
    {
        // GET: StudentArea/Topic_Content
        Examdb_newEntities db = new Examdb_newEntities();
        public ActionResult ComtentView()
        {
            return View();
        }
        public ActionResult AddTopicContent()
        {
            List<tblTopic_contents> lst = db.tblTopic_contents.ToList();
            return View(lst);
        }
        [HttpPost]
        public string AddTopicContent(tblTopic_contents tc)
        {
            db.tblTopic_contents.Add(tc);
            db.SaveChanges();
            return "Contents Added Successfully";
        }
    }

}