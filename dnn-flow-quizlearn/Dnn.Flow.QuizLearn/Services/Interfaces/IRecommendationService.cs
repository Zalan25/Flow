using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Services.Interfaces
{
    public interface IRecommendationService
    {
        IEnumerable<RecommendationRuleInfo> GetMatchingRules(
        int moduleId,
        int languageId,
        int questionLevelId,
        int skillTypeId,
        int paceTypeId,
        int? secondaryLanguageId);

        int CreateRecommendationResult(RecommendationResultInfo resultInfo);

        int AddRecommendationResultItem(RecommendationResultItemInfo itemInfo);
        IEnumerable<string> GetRecommendedSkus(
        int moduleId,
        int languageId,
        int questionLevelId,
        IEnumerable<int> selectedSkillTypeIds,
        int paceTypeId,
        int? secondaryLanguageId);
    }
}
