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


        [HttpGet]
        public ActionResult Ping()
        {
            return Content("PING GET OK");
        }

        [HttpPost]
        public ActionResult PingPost()
        {
            System.Diagnostics.Debugger.Launch();
            return Content("PING POST OK");
        }
        public ItemController()
        {
            _lookupService = new LookupService();
            _assessmentService = new AssessmentService();
            _recommendationService = new RecommendationService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Start");
        }

        [HttpGet]
        public ActionResult Start()
        {
            var model = new AssessmentStartViewModel
            {
                ModuleId = ModuleContext.ModuleId,
                Languages = _lookupService.GetLanguages(),
                Levels = _lookupService.GetLevels(),
                Skills = _lookupService.GetSkills(),
                PaceTypes = _lookupService.GetPaceTypes(),
                SelectedSkillTypeIds = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(AssessmentStartViewModel model)
        {
            System.Diagnostics.Debugger.Launch();
            return Content("POST biztosan lefutott");
            if (model == null || !ModelState.IsValid)
            {
                model = model ?? new AssessmentStartViewModel();

                model.ModuleId = ModuleContext.ModuleId;
                model.Languages = _lookupService.GetLanguages();
                model.Levels = _lookupService.GetLevels();
                model.Skills = _lookupService.GetSkills();
                model.PaceTypes = _lookupService.GetPaceTypes();
                model.SelectedSkillTypeIds = model.SelectedSkillTypeIds ?? new List<int>();

                return View(model);
            }

            var selectedSkills = model.SelectedSkillTypeIds ?? new List<int>();

            if (!selectedSkills.Any())
            {
                model.ModuleId = ModuleContext.ModuleId;
                model.Languages = _lookupService.GetLanguages();
                model.Levels = _lookupService.GetLevels();
                model.Skills = _lookupService.GetSkills();
                model.PaceTypes = _lookupService.GetPaceTypes();

                ModelState.AddModelError("", "Legalább egy készséget ki kell választani.");
                return View(model);
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

            var sessionId = _assessmentService.StartAssessmentSession(sessionInfo, selectedSkills);

            return RedirectToAction("Recommendation", new
            {
                sessionId = sessionId,
                languageId = model.LanguageId,
                questionLevelId = model.SelectedLevelId ?? 1,
                skillTypeId = selectedSkills.First(),
                paceTypeId = model.PaceTypeId ?? 1,
                secondaryLanguageId = model.SecondaryLanguageId
            });

        }

        [HttpGet]
        public ActionResult Recommendation(
            int sessionId,
            int languageId,
            int? questionLevelId,
            int skillTypeId,
            int? paceTypeId,
            int? secondaryLanguageId)
        {
            if (!questionLevelId.HasValue || !paceTypeId.HasValue || skillTypeId <= 0)
            {
                return Content("Hiányzó recommendation paraméter.");
            }

            var rules = _recommendationService.GetMatchingRules(
                ModuleContext.ModuleId,
                languageId,
                questionLevelId.Value,
                skillTypeId,
                paceTypeId.Value,
                secondaryLanguageId);

            ViewBag.SessionId = sessionId;
            ViewBag.RuleCount = rules == null ? 0 : rules.Count();

            return View(rules);
        }
    }
}