using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class RecommendationRuleInfo
    {
        public int RecommendationRuleId { get; set; }
        public int ModuleId { get; set; }
        public int LanguageId { get; set; }
        public int QuestionLevelId { get; set; }
        public int SkillTypeId { get; set; }
        public int SecondaryLanguageId { get; set; }
        public int PaceTypeId { get; set; }
        public int Priority { get; set; }
        public string MatchMode { get; set; }
        public string HotcakesProductSKU { get; set; }
        public string HotcakesCategoryKey { get; set; }
        public string HotcakesTag { get; set; }
        public int MinProducts { get; set; }
        public int MaxProducts { get; set; }
    }
}