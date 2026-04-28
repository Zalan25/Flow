using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services.Interfaces;

namespace Dnn.Flow.QuizLearn.Services
{
    public class LookupService : ILookupService
    {
        private readonly IQuizLearnDataRepository _repository;

        public LookupService()
        {
            _repository = new SqlDataProvider();
        }

        public IEnumerable<LanguageInfo> GetLanguages()
        {
            return _repository.GetAllActiveLanguages();
        }

        public IEnumerable<QuestionLevelInfo> GetLevels()
        {
            return _repository.GetAllQuestionLevels();
        }

        public IEnumerable<SkillTypeInfo> GetSkills()
        {
            return _repository.GetAllSkillTypes();
        }

        public IEnumerable<PaceTypeInfo> GetPaceTypes()
        {
            return _repository.GetAllPaceTypes();
        }

        public AssessmentModeInfo GetAssessmentMode(string modeKey)
        {
            return _repository.GetAssessmentModeByKey(modeKey);
        }
    }
}