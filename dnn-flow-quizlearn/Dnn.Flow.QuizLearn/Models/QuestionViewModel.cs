using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class QuestionViewModel
    {

        public int SessionId { get; set; }

        public int QuestionId { get; set; }

        public int QuestionNumber { get; set; }

        public int TotalQuestions { get; set; }

        public string QuestionText { get; set; }

        public string LevelName { get; set; }

        public List<AnswerOptionViewModel> Answers { get; set; }

        public int QuestionTypeId { get; set; }

        public bool AllowsMultipleAnswers
        {
            get
            {
                return QuestionTypeId == (int)QuestionType.MultipleChoice;
            }
        }

        public bool RequiresTextAnswer
        {
            get
            {
                return QuestionTypeId == (int)QuestionType.TextAnswer
                    || QuestionTypeId == (int)QuestionType.Translation;
            }
        }

    }
}