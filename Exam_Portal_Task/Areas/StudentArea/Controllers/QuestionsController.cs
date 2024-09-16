using Exam_Portal_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class QuestionsController : Controller
    {
        Examdb_newEntities db;
        public QuestionsController()
        {
            db = new Examdb_newEntities();
        }
        public ActionResult Index()
        {
            ViewBag.topic = GetTopics();
            List<tblContent_questions> questions = db.tblContent_questions.ToList();
            return View(questions);
        }

        public List<SelectListItem> GetTopics()
        {
            List<SelectListItem>lst=new List<SelectListItem>();
            foreach(tblTopic t in db.tblTopics.ToList())
            {
                SelectListItem s = new SelectListItem() { Text=t.Topic_name, Value=t.Topic_id.ToString() };
                lst.Add(s); 
            }
            return lst;
        }

        public JsonResult GetTopicWiseContents(int id)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            List<tblTopic_contents> contents = db.tblTopic_contents.ToList().Where(e => e.Topic_id.Equals(id)).ToList();

            foreach (tblTopic_contents c in contents) 
            {
                SelectListItem s = new SelectListItem() { Text = c.Content_name, Value = c.Content_id.ToString() };
                lst.Add(s);
            }
            return Json(lst,JsonRequestBehavior.AllowGet);
        }

        public string AddQuestion(tblContent_questions q)
        {
            db.tblContent_questions.Add(q);
            db.SaveChanges();
            return "Question Added Successfully";
        }


       
    }
}