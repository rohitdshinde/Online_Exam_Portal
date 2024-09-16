using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;

namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class TopicController : Controller
    {
        // GET: StudentArea/Topic
        Examdb_newEntities db=new Examdb_newEntities(); 
        public ActionResult Index()
        {
            List<tblTopic> lst = db.tblTopics.ToList();

            return View(lst);
        }
        public ActionResult AddTopics()
        {
            List<tblTopic> lst = db.tblTopics.ToList();

            return View(lst);

        }
        [HttpPost]
        public string AddTopics(tblTopic tp)
        {
            db.tblTopics.Add(tp);   
            db.SaveChanges();
            return "Topic Added Successfully";
        }
        public JsonResult GetTopic()
        {
            List<tblTopic> lst = new List<tblTopic>();
            foreach (tblTopic c in db.tblTopics.ToList())
            {
                tblTopic tc = new tblTopic()
                {
                    Topic_id = c.Topic_id,
                    Topic_name = c.Topic_name,
                };
                lst.Add(tc);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
