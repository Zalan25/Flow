using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services;
using DotNetNuke.Entities.Modules;
using ValidateAntiForgeryTokenAttribute = DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryTokenAttribute;

namespace Dnn.Flow.QuizLearn.Controllers
{
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {
        private readonly LookupService _lookupService;

        public SettingsController()
        {
            _lookupService = new LookupService();
        }

        public ActionResult Settings()
        {
            var modeValue = ModuleContext.Configuration.ModuleSettings["QuizLearnMode"] as string;

            QuizLearnMode mode;
            if (!Enum.TryParse(modeValue, out mode))
            {
                mode = QuizLearnMode.Recommendation;
            }

            var languages = _lookupService.GetLanguages().ToList();

            var activeLanguageIds = GetActiveAssessmentLanguageIds();

            if (!activeLanguageIds.Any())
            {
                activeLanguageIds = languages.Select(x => x.LanguageId).ToList();
            }

            var model = new SettingsViewModel
            {
                Mode = mode,
                ActiveAssessmentLanguageIds = activeLanguageIds,
                AvailableLanguages = languages.Select(x => new SelectListItem
                {
                    Value = x.LanguageId.ToString(),
                    Text = x.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(SettingsViewModel model)
        {
            var moduleController = new ModuleController();

            moduleController.UpdateModuleSetting(
                ModuleContext.ModuleId,
                "QuizLearnMode",
                ((int)model.Mode).ToString()
            );

            var activeLanguageIds = model.ActiveAssessmentLanguageIds ?? new List<int>();

            moduleController.UpdateModuleSetting(
                ModuleContext.ModuleId,
                "ActiveAssessmentLanguageIds",
                string.Join(",", activeLanguageIds.Distinct())
            );

            TempData["SettingsSaved"] = true;

            return RedirectToAction("Index", "Item");
        }

        private List<int> GetActiveAssessmentLanguageIds()
        {
            var value = ModuleContext.Configuration.ModuleSettings["ActiveAssessmentLanguageIds"] as string;

            if (string.IsNullOrWhiteSpace(value))
            {
                return new List<int>();
            }

            return value
                .Split(',')
                .Select(x =>
                {
                    int id;
                    return int.TryParse(x, out id) ? id : 0;
                })
                .Where(x => x > 0)
                .Distinct()
                .ToList();
        }
    }
}