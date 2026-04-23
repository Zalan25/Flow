using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
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

        // DNN MVC always routes through Index — handle both GET and POST here
        public ActionResult Index()
        {
            if (Request.HttpMethod == "POST")
            {
                return HandleStartPost();
            }

            return View("Start", BuildStartViewModel());
        }

        private ActionResult HandleStartPost()
        {
            var model = new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,
                AssessmentModeId = int.TryParse(Request.Form["AssessmentModeId"], out int modeId) ? modeId : 0,
                LanguageId = int.TryParse(Request.Form["LanguageId"], out int langId) ? langId : 0,
                SecondaryLanguageId = int.TryParse(Request.Form["SecondaryLanguageId"], out int secLangId) ? (int?)secLangId : null,
                SelectedLevelId = int.TryParse(Request.Form["SelectedLevelId"], out int levelId) ? (int?)levelId : null,
                PaceTypeId = int.TryParse(Request.Form["PaceTypeId"], out int paceId) ? (int?)paceId : null,
                SelectedSkillTypeIds = Request.Form.GetValues("SelectedSkillTypeIds")
                                        ?.Select(x => int.TryParse(x, out int sid) ? sid : 0)
                                        .Where(x => x > 0)
                                        .ToList() ?? new List<int>()
            };

            if (!model.SelectedSkillTypeIds.Any())
            {
                var fresh = BuildStartViewModel();
                fresh.SelectedSkillTypeIds = model.SelectedSkillTypeIds;
                ModelState.AddModelError("", "Legalább egy készséget ki kell választani.");
                return View("Start", fresh);
            }

            var sessionInfo = new AssessmentSessionInfo
            {
                ModuleId = ModuleContext.ModuleId,
                AssessmentModeId = model.AssessmentModeId,
                LanguageId = model.LanguageId,
                SecondaryLanguageId = model.SecondaryLanguageId,
                SelectedLevelId = model.SelectedLevelId ?? 1,
                PaceTypeId = model.PaceTypeId ?? 1,
                UserId = null,
                NeedLevelTest = false,
                Status = "Started"
            };

            var sessionId = _assessmentService.StartAssessmentSession(sessionInfo, model.SelectedSkillTypeIds);

            var rules = _recommendationService.GetMatchingRules(
                ModuleContext.ModuleId,
                model.LanguageId,
                model.SelectedLevelId ?? 1,
                model.SelectedSkillTypeIds.First(),
                model.PaceTypeId ?? 1,
                model.SecondaryLanguageId);

            ViewBag.SessionId = sessionId;
            ViewBag.RuleCount = rules == null ? 0 : rules.Count();

            return View("Recommendation", rules);
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
    }
}