using Dnn.Flow.QuizLearn.Models;
using System.Collections.Generic;
using System.Reflection;

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
        IEnumerable<BundleCompositionRuleInfo> GetBundleCompositionRules(
            int moduleId,
            int focusSkillTypeId,
            int paceTypeId);

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
        int AddTextAnswer(int moduleId, int assessmentSessionId, int questionId, string textAnswer);
        int AddSingleChoiceAnswer(int moduleId, int assessmentSessionId, int questionId, int selectedAnswerId);
        int StartMultipleChoiceAnswer(int moduleId, int assessmentSessionId, int questionId);
        int AddMultipleChoiceAnswerOption(int moduleId, int testAttemptAnswerId, int answerId);
        int GradeMultipleChoiceAnswer(int moduleId, int testAttemptAnswerId);

        void GenerateSessionQuestions(int moduleId, int sessionId, int languageId);
        IEnumerable<QuestionInfo> GetSessionQuestions(int sessionId);

        int CompleteTestAttempt(int moduleId, int assessmentSessionId, int totalScore, int a1Correct, int a2Correct, int b1Correct, int b2Correct, int c1Correct, int finalLevelId);

        int GetRandomActiveTestId(int moduleId, int languageId);
        int PrepareAssessmentSessionTest(int moduleId, int assessmentSessionId, int languageId);

    }
}