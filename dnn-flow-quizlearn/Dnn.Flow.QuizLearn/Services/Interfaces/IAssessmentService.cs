using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Services.Interfaces
{
    public interface IAssessmentService
    {
        int StartAssessmentSession(AssessmentSessionInfo sessionInfo, IEnumerable<int> selectedSkillTypeIds);

        int CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId);
        int GetRequiredCorrectCount(string levelName, int questionCount);

        int DetermineFinalLevel(int a1Correct, int a1Count, int a2Correct, int a2Count, int b1Correct, int b1Count, int b2Correct, int b2Count, int c1Correct, int c1Count);
        void SaveAnswer(int moduleId,int sessionId, int questionId, int answerId);
        ResultViewModel CalculateResult(int moduleId, int sessionId);
        AssessmentSessionInfo GetAssessmentSessionById(int sessionId);
        void SaveSingleChoiceAnswer(int moduleId, int sessionId, int questionId, int answerId);
        void SaveMultipleChoiceAnswer(int moduleId, int sessionId, int questionId, IEnumerable<int> answerIds);
        void SaveTextAnswer(int moduleId, int sessionId, int questionId, string textAnswer);
        int PrepareAssessmentSessionTest(int moduleId, int sessionId, int languageId);  


    }
}