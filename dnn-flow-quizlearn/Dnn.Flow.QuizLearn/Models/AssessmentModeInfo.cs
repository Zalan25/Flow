using System;

namespace Dnn.Flow.QuizLearn.Models
{
    public class AssessmentModeInfo
    {
        public int AssessmentModeId { get; set; }
        public string ModeKey { get; set; }
        public string ModeName { get; set; }
        public bool IsActive { get; set; }
    }
}
