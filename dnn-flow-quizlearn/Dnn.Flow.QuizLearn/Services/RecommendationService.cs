using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services.Interfaces;
using System.Linq;

namespace Dnn.Flow.QuizLearn.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IQuizLearnDataRepository _repository;
        public RecommendationService()
        {
            _repository = new SqlDataProvider();
        }

        public IEnumerable<RecommendationRuleInfo> GetMatchingRules(int moduleId, int languageId, int questionLevelId, int skillTypeId, int paceTypeId, int? secondaryLanguageId)
        {
            var exactRules = _repository.FindExactRecommendationRules(
                moduleId,
                languageId,
                questionLevelId,
                skillTypeId,
                paceTypeId,
                secondaryLanguageId);

            if (exactRules != null && exactRules.Any())
            {
                return exactRules;
            }

            var fallbackRules = _repository.FindFallbackRecommendationRules(
                moduleId,
                languageId,
                questionLevelId,
                skillTypeId,
                secondaryLanguageId);

            if (fallbackRules != null && fallbackRules.Any())
            {
                return fallbackRules;
            }

            return _repository.FindGeneralRecommendationRules(
                moduleId,
                languageId);
        }

        public int CreateRecommendationResult(RecommendationResultInfo resultInfo)
        {
            return _repository.AddRecommendationResult(resultInfo);
        }

        public int AddRecommendationResultItem(RecommendationResultItemInfo itemInfo)
        {
            return _repository.AddRecommendationResultItem(itemInfo);
        }
    }
}
