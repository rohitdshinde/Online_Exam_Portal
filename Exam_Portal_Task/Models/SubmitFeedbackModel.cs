using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Portal_Task.Models
{
    public class SubmitFeedbackModel
    {
        
        public int StudentId { get; set; }

      
        public int TopicId { get; set; }

       
        public string Comments { get; set; }
    }
}