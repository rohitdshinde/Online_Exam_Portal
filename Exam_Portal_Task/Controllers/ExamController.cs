using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;
namespace Exam_Portal_Task.Controllers
{
    public class ExamController : Controller
    {
        Examdb_newEntities db;
        public ExamController()
        {
            db=new Examdb_newEntities();
        }
        // GET: Exam
        public ActionResult Index()
        {

            ViewBag.topics = GetTopics();
            return View();
        }
        [HttpPost]
        public ActionResult Index(int topic_id)
        {
            int student_id = Convert.ToInt32(Session["student_id"].ToString());

            tblTopic t = db.tblTopics.Find(topic_id);

            List<QuestionModel> lst = GetExamQuestions(topic_id,5);
            ExamModel em = new ExamModel() 
            { 
                student_id= student_id,
                topic_id = t.Topic_id, 
                topic_name = t.Topic_name, 
                questions=lst,
             start_date=DateTime.Now,
             start_time=DateTime.Now.ToLongTimeString()

            };
            Session["exam"] = em;
           // Session["questions"] = lst;
            return RedirectToAction("StartExam");
        }

        public ActionResult StartExam()
        {
            ExamModel em = (ExamModel)Session["exam"];
         //   List<QuestionModel> lst = (List<QuestionModel>)Session["questions"];
            return View(em);
        }
        public List<QuestionModel> GetExamQuestions(int topic_id,int size)
        {
            List<QuestionModel> questions = new List<QuestionModel>();
            var query = from t in db.tblTopics.ToList()
                        join c in db.tblTopic_contents.ToList() on t.Topic_id equals c.Topic_id
                        join q in db.tblContent_questions.ToList() on c.Content_id equals q.Content_id
                        where t.Topic_id.Equals(topic_id)
                        select new
                        {
                            topic_id = t.Topic_id,
                            topic_name = t.Topic_name,
                            content_id = c.Content_id,
                            content_name = c.Content_name,
                            question_id = q.Question_id,
                            question = q.Question,
                            option1 = q.Option_1,
                            option2 = q.Option_2,
                            option3 = q.Option_3,
                            option4 = q.Option_4,
                            correct_option_number = q.Correct_option_number
                        };

            foreach (var q in query)
            {
                QuestionModel m = new QuestionModel()
                {
                    content_id = q.content_id,
                    content_name = q.content_name,
                    option1 = q.option1,
                    option2 = q.option2,
                    option3 = q.option3,
                    option4 = q.option4,
                    question = q.question,
                    question_id = q.question_id,
                    topic_id = q.topic_id,
                    topic_name = q.topic_name,
                    correct_option_number = (int)q.correct_option_number

                };

                questions.Add(m);
            }
            //int end = questions.Count - 1;
            List<QuestionModel> lst = new List<QuestionModel>();
            while (lst.Count != size)
            {

                Random r=new Random();
                int i = r.Next(0, questions.Count - 1);
                QuestionModel qm = questions[i];

                if (lst.IndexOf(qm) == -1)
                {
                    lst.Add(qm);
                }

            }

            return lst;

        }
        public List<SelectListItem>   GetTopics()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(tblTopic t in db.tblTopics.ToList())
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = t.Topic_name,
                    Value = t.Topic_id.ToString()
                };
                lst.Add(s);
            }
            return lst;
        }


        public string SubmitExam(List<QuestionModel> questions)
        {
            ExamModel em = (ExamModel)Session["exam"];

           
            List<tblStudent_exam_questions> lst = new List<tblStudent_exam_questions>();
            foreach(QuestionModel q in questions)
            {
             //   tblContent_questions qs = db.tblContent_questions.Find(q.question_id);
                tblStudent_exam_questions sd = new tblStudent_exam_questions()
                {
                     Question_id=q.question_id,
                      Submited_option_number=q.submitted_option_number
                };
                lst.Add(sd);
            }
            tblStudent_exams m = new tblStudent_exams()
            {
                Student_id = em.student_id,
                Exam_date = em.start_date,
                Start_time = em.start_time,
                Topic_id = em.topic_id,
                End_time = DateTime.Now.ToLongTimeString(),
                 tblStudent_exam_questions=lst

            };

            db.tblStudent_exams.Add(m);
            db.SaveChanges();
            return "Exam Submitted Successfully";
        }
    }
}