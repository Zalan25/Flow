using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class ResultViewModel
    {
        public int SessionId { get; set; }

        public int TotalScore { get; set; }

        public int Percentage { get; set; } 
        public int MaxScore { get; set; }
        public int FinalLevelId { get; set; }

        public string FinalLevelName { get; set; }

        public int A1Correct { get; set; }
        public int A2Correct { get; set; }
        public int B1Correct { get; set; }
        public int B2Correct { get; set; }
        public int C1Correct { get; set; }

        public int A1Total { get; set; }
        public int A2Total { get; set; }
        public int B1Total { get; set; }
        public int B2Total { get; set; }
        public int C1Total { get; set; }

    }
}