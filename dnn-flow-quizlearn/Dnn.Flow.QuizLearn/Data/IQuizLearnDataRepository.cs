using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Data
{
    public interface IQuizLearnDataRepository
    {
        // Lekérdezések
        IEnumerable<LanguageInfo> GetAllActiveLanguages();
        IEnumerable<QuestionLevelInfo> GetAllQuestionLevels();
        IEnumerable<SkillTypeInfo> GetAllSkillTypes();
        IEnumerable<PaceTypeInfo> GetAllPaceTypes();
        AssessmentModeInfo GetAssessmentModeByKey(string modeKey);


        // Sessions
        int AddAssessmentSession(AssessmentSessionInfo sessionInfo);
        int AddAssessmentSessionSkill(int moduleId, int assessmentSessionId, int skillTypeId);
        int CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId);

        // Szabályok
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

        // Eredmények
        int AddRecommendationResult(RecommendationResultInfo resultInfo);
        int AddRecommendationResultItem(RecommendationResultItemInfo itemInfo);



        // Szintfelmérő kérdések
        AssessmentSessionInfo GetAssessmentSessionById(int assessmentSessionId);
        IEnumerable<QuestionInfo> GetQuestionsForAssessment(int moduleId,int languageId);
        IEnumerable<AnswerInfo> GetAnswersByQuestionId(int moduleId,int questionId);
        int StartTestAttempt(int moduleId, int assessmentSessionId, int testId);
        int AddTestAttemptAnswer(int moduleId, int assessmentSessionId, int questionId, int answerId);
        IEnumerable<AttemptAnswerSummaryInfo> GetAttemptAnswerSummary(int moduleId, int assessmentSessionId);

    }
}