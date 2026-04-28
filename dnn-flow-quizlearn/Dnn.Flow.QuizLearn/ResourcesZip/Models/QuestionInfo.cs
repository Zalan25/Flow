using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class QuestionInfo
    {
        public int QuestionId { get; set; }

        public int ModuleId { get; set; }

        public string QuestionText { get; set; }

        public int LanguageId { get; set; }

        public int QuestionLevelId { get; set; }

        public int QuestionTypeId { get; set; }

        public int SkillTypeId { get; set; }

        public int Points { get; set; }

        public bool IsActive { get; set; }
    }
}