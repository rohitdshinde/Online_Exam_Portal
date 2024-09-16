using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Portal_Task.Models
{
    public class QuestionModel
    {
        public int topic_id { get; set; }
        public int content_id { get; set; }
        public int question_id { get; set; }
        public string topic_name { get; set; }
        public string content_name { get; set; }
        public string question { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public int correct_option_number { get; set; }
        public int submitted_option_number { get; set; }
        public string status { get; set; }
    }
}