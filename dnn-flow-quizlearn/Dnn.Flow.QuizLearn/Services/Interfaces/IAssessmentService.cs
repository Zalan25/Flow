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
    }
}