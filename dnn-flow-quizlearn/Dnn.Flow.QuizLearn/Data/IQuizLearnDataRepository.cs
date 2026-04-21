using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Data
{
    public interface IQuizLearnDataRepository
    {
        // Lookups
        IEnumerable<LanguageInfo> GetAllActiveLanguages();
        IEnumerable<QuestionLevelInfo> GetAllQuestionLevels();
        IEnumerable<SkillTypeInfo> GetAllSkillTypes();
        IEnumerable<PaceTypeInfo> GetAllPaceTypes();
        AssessmentModeInfo GetAssessmentModeByKey(string modeKey);

        // Sessions
        int AddAssessmentSession(AssessmentSessionInfo sessionInfo);
        int AddAssessmentSessionSkill(int moduleId, int assessmentSessionId, int skillTypeId);
        bool CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId);

        // Recommendation rules
        IEnumerable<RecommendationRuleInfo> FindExactRecommendationRules(
            int moduleId,
            int languageId,
            int questionLevelId,
            int skillTypeId,
            int paceTypeId,
            int? secondaryLanguageId);

        IEnumerable<RecommendationRuleInfo> FindFallbackRecommendationRules(
            int moduleId,
            int languageId,
            int questionLevelId,
            int skillTypeId,
            int? secondaryLanguageId);

        IEnumerable<RecommendationRuleInfo> FindGeneralRecommendationRules(
            int moduleId,
            int languageId);

        // Recommendation results
        int AddRecommendationResult(RecommendationResultInfo resultInfo);
        int AddRecommendationResultItem(RecommendationResultItemInfo itemInfo);


    }
}