using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dnn.Flow.QuizLearn.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {
        private readonly LookupService _lookupService;
        private readonly AssessmentService _assessmentService;
        private readonly RecommendationService _recommendationService;

        public ItemController()
        {
            _lookupService = new LookupService();
            _assessmentService = new AssessmentService();
            _recommendationService = new RecommendationService();
        }

        public ActionResult Index()
        {
            if (Request.HttpMethod == "POST")
            {
                return HandleStartPost();
            }

            var mode = GetModuleMode();

            switch (mode)
            {
                case QuizLearnMode.LevelAssessment:
                    return StartAssessment();

                case QuizLearnMode.Recommendation:
                case QuizLearnMode.RecommendationWithLevelAssessment:
                default:
                    return Start();
            }
        }

        public ActionResult Start()
        {
            return View("Start", BuildStartViewModel());
        }

        public ActionResult StartAssessment()
        {
            ViewBag.ModuleMode = GetModuleMode();

            return View("StartAssessment");
        }

        private ActionResult HandleStartPost()
        {
            var moduleMode = GetModuleMode();

            var model = new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,

                AssessmentModeId = int.TryParse(Request.Form["AssessmentModeId"], out int modeId)
                    ? modeId
                    : 0,

                LanguageId = int.TryParse(Request.Form["LanguageId"], out int langId)
                    ? langId
                    : 0,

                SecondaryLanguageId = int.TryParse(Request.Form["SecondaryLanguageId"], out int secLangId)
                    ? (int?)secLangId
                    : null,

                SelectedLevelId = int.TryParse(Request.Form["SelectedLevelId"], out int levelId)
                    ? (int?)levelId
                    : null,

                PaceTypeId = int.TryParse(Request.Form["PaceTypeId"], out int paceId)
                    ? (int?)paceId
                    : null,

                SelectedSkillTypeIds = Request.Form.GetValues("SelectedSkillTypeIds")
                    ?.Select(x => int.TryParse(x, out int sid) ? sid : 0)
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList() ?? new List<int>()
            };

            if (model.LanguageId <= 0)
            {
                var fresh = BuildStartViewModel();
                ModelState.AddModelError("", "A nyelv kiválasztása kötelező.");
                return View("Start", fresh);
            }

            if (!model.SelectedSkillTypeIds.Any())
            {
                var fresh = BuildStartViewModel();
                fresh.SelectedSkillTypeIds = model.SelectedSkillTypeIds;
                ModelState.AddModelError("", "Legalább egy készséget ki kell választani.");
                return View("Start", fresh);
            }

            var needLevelTest =
                moduleMode == QuizLearnMode.LevelAssessment ||
                (moduleMode == QuizLearnMode.RecommendationWithLevelAssessment && !model.SelectedLevelId.HasValue);

            var sessionInfo = new AssessmentSessionInfo
            {
                ModuleId = ModuleContext.ModuleId,
                AssessmentModeId = model.AssessmentModeId,
                LanguageId = model.LanguageId,
                SecondaryLanguageId = model.SecondaryLanguageId,
                SelectedLevelId = model.SelectedLevelId ?? 1,
                PaceTypeId = model.PaceTypeId ?? 1,
                UserId = null,
                NeedLevelTest = needLevelTest,
                Status = "Started"
            };

            var sessionId = _assessmentService.StartAssessmentSession(
                sessionInfo,
                model.SelectedSkillTypeIds
            );

            if (needLevelTest)
            {
                ViewBag.SessionId = sessionId;
                ViewBag.LanguageId = model.LanguageId;
                ViewBag.SelectedSkillTypeIds = model.SelectedSkillTypeIds;

                return View("StartAssessment");
            }

            var rules = GetRecommendationRulesForAllSelectedSkills(model);

            ViewBag.SessionId = sessionId;
            ViewBag.RuleCount = rules.Count;

            return View("Recommendation", rules);
        }

        private List<RecommendationRuleInfo> GetRecommendationRulesForAllSelectedSkills(AssessmentStartViewModel model)
        {
            var result = new List<RecommendationRuleInfo>();

            foreach (var skillTypeId in model.SelectedSkillTypeIds)
            {
                var rulesForSkill = _recommendationService.GetMatchingRules(
                    ModuleContext.ModuleId,
                    model.LanguageId,
                    model.SelectedLevelId ?? 1,
                    skillTypeId,
                    model.PaceTypeId ?? 1,
                    model.SecondaryLanguageId
                ) as List<RecommendationRuleInfo>;

                if (rulesForSkill != null)
                {
                    result.AddRange(rulesForSkill);
                }
            }

            return result
                .GroupBy(x => x.RecommendationRuleId)
                .Select(g => g.First())
                .ToList();
        }

        private AssessmentStartViewModel BuildStartViewModel()
        {
            return new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,
                Languages = _lookupService.GetLanguages(),
                Levels = _lookupService.GetLevels(),
                Skills = _lookupService.GetSkills(),
                PaceTypes = _lookupService.GetPaceTypes(),
                SelectedSkillTypeIds = new List<int>()
            };
        }

        private QuizLearnMode GetModuleMode()
        {
            var modeValue = ModuleContext.Configuration.ModuleSettings["QuizLearnMode"] as string;

            int parsedMode;

            if (!int.TryParse(modeValue, out parsedMode))
            {
                return QuizLearnMode.RecommendationWithLevelAssessment;
            }

            if (!Enum.IsDefined(typeof(QuizLearnMode), parsedMode))
            {
                return QuizLearnMode.RecommendationWithLevelAssessment;
            }

            return (QuizLearnMode)parsedMode;
        }
    }
}