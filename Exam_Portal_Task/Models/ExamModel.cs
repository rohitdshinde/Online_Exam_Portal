using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Portal_Task.Models
{
    public class ExamModel
    {

        public int exam_id { get; set; }
        public int student_id {  get; set; }
        public string student_name {  get; set; }
        public int topic_id {  get; set; }
        public string topic_name { get; set;}
        public DateTime start_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }

        public List<QuestionModel> questions { get; set; }

    }
}