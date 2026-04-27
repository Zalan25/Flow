using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services.Interfaces;
using System;
using System.Collections.Generic;
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
        public IEnumerable<string> GetRecommendedSkus(
            int moduleId,
            int languageId,
            int questionLevelId,
            IEnumerable<int> selectedSkillTypeIds,
            int paceTypeId,
            int? secondaryLanguageId)
        {
            var selectedSkus = new List<string>();

            if (selectedSkillTypeIds == null)
            {
                return selectedSkus;
            }

            var focusSkillTypeId = selectedSkillTypeIds
                .Distinct()
                .FirstOrDefault();

            if (focusSkillTypeId <= 0)
            {
                return selectedSkus;
            }

            var compositionRules = _repository.GetBundleCompositionRules(
                moduleId,
                focusSkillTypeId,
                paceTypeId
            ).ToList();

            if (!compositionRules.Any())
            {
                return selectedSkus;
            }

            var targetProductCount = compositionRules.Sum(x => x.ProductCount);

            var focusRule = compositionRules
                .FirstOrDefault(x => x.ProductSkillTypeId == focusSkillTypeId);

            if (focusRule != null && focusRule.ProductCount < 2)
            {
                focusRule.ProductCount = 2;
            }

            foreach (var compositionRule in compositionRules
                .Where(x => x.ProductCount > 0)
                .OrderBy(x => x.Priority))
            {
                var matchingRules = GetMatchingRules(
                    moduleId,
                    languageId,
                    questionLevelId,
                    compositionRule.ProductSkillTypeId,
                    paceTypeId,
                    secondaryLanguageId
                );

                var skus = matchingRules
                    .Where(x => !string.IsNullOrWhiteSpace(x.HotcakesProductSKU))
                    .OrderBy(x => x.Priority)
                    .Select(x => x.HotcakesProductSKU)
                    .Distinct()
                    .Where(x => !selectedSkus.Contains(x))
                    .Take(compositionRule.ProductCount);

                selectedSkus.AddRange(skus);
            }

            if (selectedSkus.Count < targetProductCount)
            {
                foreach (var compositionRule in compositionRules
                    .OrderBy(x => x.Priority))
                {
                    if (selectedSkus.Count >= targetProductCount)
                    {
                        break;
                    }

                    var matchingRules = GetMatchingRules(
                        moduleId,
                        languageId,
                        questionLevelId,
                        compositionRule.ProductSkillTypeId,
                        paceTypeId,
                        secondaryLanguageId
                    );

                    var extraSkus = matchingRules
                        .Where(x => !string.IsNullOrWhiteSpace(x.HotcakesProductSKU))
                        .OrderBy(x => x.Priority)
                        .Select(x => x.HotcakesProductSKU)
                        .Distinct()
                        .Where(x => !selectedSkus.Contains(x))
                        .Take(targetProductCount - selectedSkus.Count);

                    selectedSkus.AddRange(extraSkus);
                }
            }

            return selectedSkus
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .Take(targetProductCount)
                .ToList();
        }
    }
}
