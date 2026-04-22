using System;

namespace Dnn.Flow.QuizLearn.Models
{
    public class RecommendationResultItemInfo
    {
        public int ModuleId { get; set; }
        public int RecommendationResultId { get; set; }
        public int ProductSKU { get; set; }
        public int SortOrder { get; set; }
        public string SourceRule { get; set; }
        public string SourceType { get; set; }
        public int Score { get; set; }
        public bool IsPrimary { get; set; }

    }
}
