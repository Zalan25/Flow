using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class AttemptAnswerSummaryInfo
    {
        public int QuestionId { get; set; }
        public int QuestionLevelId { get; set; }
        public int Points { get; set; }
        public bool IsCorrect { get; set; }
        public int EarnedPoints { get; set; }
    }
}