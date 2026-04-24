using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankClient
{
    internal class Models
    {
    }
    public class Quiz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }

    public class Question
    {
        public string QuestionText { get; set; }
        public string Type { get; set; } // pl. "Short"
        public List<string> CorrectAnswers { get; set; } = new List<string>();
    }
}
