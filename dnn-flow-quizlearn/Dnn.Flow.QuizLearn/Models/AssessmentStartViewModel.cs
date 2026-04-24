using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Models
{
    public class AssessmentStartViewModel
    {
        public int ModuleId { get; set; }

        public int AssessmentModeId { get; set; }
        public int LanguageId { get; set; }
        public int? SecondaryLanguageId { get; set; }
        public int? SelectedLevelId { get; set; }
        public int? PaceTypeId { get; set; }
        public bool NeedLevelTest { get; set; }
        public List<int> SelectedSkillTypeIds { get; set; }
        public QuizLearnMode ModuleMode { get; set; }

        public bool IsRecommendationOnlyMode
        {
            get
            {
                return ModuleMode == QuizLearnMode.Recommendation;
            }
        }

        public IEnumerable<LanguageInfo> Languages { get; set; }
        public IEnumerable<QuestionLevelInfo> Levels { get; set; }
        public IEnumerable<SkillTypeInfo> Skills { get; set; }
        public IEnumerable<PaceTypeInfo> PaceTypes { get; set; }
    }
}