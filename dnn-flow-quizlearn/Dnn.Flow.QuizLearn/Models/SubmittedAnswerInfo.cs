using System;
using System.Collections.Generic;

namespace Dnn.Flow.QuizLearn.Models
{
    public class SubmittedAnswerInfo
    {
        public int ModuleId { get; set; }
        public int AssessmentSessionId { get; set; }
        public int QuestionId { get; set; }
        public List<int> SelectedAnswerIds { get; set; }
        public string TextAnswer { get; set; }
    }
}
