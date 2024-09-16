using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Portal_Task.Models;

namespace Exam_Portal_Task.Areas.StudentArea.Controllers
{
    public class ExamDetailController : Controller
    {
        Examdb_newEntities db;

        public ExamDetailController()
        {
            db = new Examdb_newEntities();
        }

        public ActionResult Index()
        {
            List<ExamModel> lst = GetExams();
            return View(lst);
        }

        public ActionResult ViewExam(int id)
        {
            tblStudent_exams e = db.tblStudent_exams.Find(id);
            List<tblStudent_exam_questions> questionlist = db.tblStudent_exam_questions.ToList().Where(ep => ep.Exam_id.Equals(id)).ToList();

            List<QuestionModel> questions = new List<QuestionModel>();
            int correct_count = 0, wrong_count = 0;

            foreach (tblStudent_exam_questions q in questionlist)
            {
                tblContent_questions qs = db.tblContent_questions.Find(q.Question_id);
                tblTopic_contents content = db.tblTopic_contents.Find(qs.Content_id);
                tblTopic topic = db.tblTopics.Find(content.Topic_id);
                string status = "Wrong";

                if (q.Submited_option_number == qs.Correct_option_number)
                {
                    status = "Correct";
                    correct_count++;
                }
                else
                {
                    wrong_count++;
                }

                QuestionModel qm = new QuestionModel()
                {
                    question_id = (int)q.Question_id,
                    submitted_option_number = (int)q.Submited_option_number,
                    question = qs.Question,
                    option1 = qs.Option_1,
                    option2 = qs.Option_2,
                    option3 = qs.Option_3,
                    option4 = qs.Option_4,
                    correct_option_number = (int)qs.Correct_option_number,
                    content_id = content.Content_id,
                    content_name = content.Content_name,
                    topic_id = topic.Topic_id,
                    topic_name = topic.Topic_name,
                    status = status
                };

                questions.Add(qm);
            }

            tblStudent_details sd = db.tblStudent_details.Find(e.Student_id);
            tblTopic t = db.tblTopics.Find(e.Topic_id);

            ExamModel em = new ExamModel()
            {
                exam_id = e.Exam_id,
                end_time = e.End_time,
                start_date = (DateTime)e.Exam_date,
                start_time = e.Start_time,
                student_id = (int)e.Student_id,
                topic_id = (int)e.Topic_id,
                questions = questions,
                student_name = sd.Student_name,
                topic_name = t.Topic_name
            };

            ViewBag.correct_count = correct_count;
            ViewBag.wrong_count = wrong_count;

           
            var contentPercentages = em.questions
                .GroupBy(q => q.content_name)
                .Select(group => new ContentPercentageModel
                {
                    ContentName = group.Key,
                    TotalQuestions = group.Count(),
                    CorrectQuestions = group.Count(q => q.status == "Correct"),
                    Percentage = group.Count(q => q.status == "Correct") * 100.0 / group.Count()
                })
                .ToList();

            
            ViewBag.ContentPercentages = contentPercentages;

            return View(em);
        }

        public List<ExamModel> GetExams()
        {
            List<ExamModel> exams = new List<ExamModel>();
            List<tblStudent_exams> lst = db.tblStudent_exams.ToList();

            foreach (tblStudent_exams e in lst)
            {
                ExamModel em = new ExamModel()
                {
                    exam_id = e.Exam_id,
                    topic_id = (int)e.Topic_id,
                    start_date = (DateTime)e.Exam_date,
                    end_time = e.End_time,
                    start_time = e.Start_time,
                    student_id = (int)e.Student_id,
                    student_name = e.tblStudent_details.Student_name,
                    topic_name = e.tblTopic.Topic_name
                };
                exams.Add(em);
            }

            return exams;
        }
    }
}