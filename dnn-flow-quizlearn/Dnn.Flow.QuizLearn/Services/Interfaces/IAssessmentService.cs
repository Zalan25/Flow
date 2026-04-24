using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Services.Interfaces
{
    public interface IAssessmentService
    {
        int StartAssessmentSession(AssessmentSessionInfo sessionInfo, IEnumerable<int> selectedSkillTypeIds);

        int CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId);

        int? DetermineFinalLevel(int a1CorrectCount, int a2CorrectCount, int b1CorrectCount, int b2CorrectCount, int c1CorrectCount);
        void SaveAnswer(int moduleId,int sessionId, int questionId, int answerId);
        ResultViewModel CalculateResult(int moduleId, int sessionId);
        AssessmentSessionInfo GetAssessmentSessionById(int sessionId);
        void SaveSingleChoiceAnswer(int moduleId, int sessionId, int questionId, int answerId);
        void SaveMultipleChoiceAnswer(int moduleId, int sessionId, int questionId, IEnumerable<int> answerIds);
        void SaveTextAnswer(int moduleId, int sessionId, int questionId, string textAnswer);

    }
}