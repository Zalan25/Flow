using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.QuizLearn.Dnn.Flow.QuizLearn.Models
{
    public class AnswerInfo
    {
        public int AnswerId { get; set; }

        public int ModuleId { get; set; }

        public int QuestionId { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public int AnswerOrder { get; set; }
    }
}