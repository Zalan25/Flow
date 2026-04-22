using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Services.Interfaces
{
    public interface ILookupService
    {
        IEnumerable<LanguageInfo> GetLanguages();
        IEnumerable<QuestionLevelInfo> GetLevels();
        IEnumerable<SkillTypeInfo> GetSkills();
        IEnumerable<PaceTypeInfo> GetPaceTypes();
        AssessmentModeInfo GetAssessmentMode(string modeKey);
    }
}
