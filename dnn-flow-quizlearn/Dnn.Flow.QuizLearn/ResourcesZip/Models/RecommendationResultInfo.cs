using System;

namespace Dnn.Flow.QuizLearn.Models
{
    public class RecommendationResultInfo
    {
        public int ModuleId { get; set; }
        public int AssessmentSessionId { get; set; }
        public string SourceType { get; set; }
        public int RecommendedLevelId { get; set; }
        public string RecommendationSummary { get; set; }
        public int MinProducts { get; set; }
        public int MaxProducts { get; set; }
    }
}
