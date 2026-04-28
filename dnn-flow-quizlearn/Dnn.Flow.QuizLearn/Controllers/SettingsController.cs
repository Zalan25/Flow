/*
' Copyright (c) 2026 Flow
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Collections;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Models;
using DotNetNuke.Entities.Modules;
using ValidateAntiForgeryTokenAttribute = DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryTokenAttribute;


namespace Dnn.Flow.QuizLearn.Controllers
{
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {

        public ActionResult Settings()
        {
            var modeValue = ModuleContext.Configuration.ModuleSettings["QuizLearnMode"] as string;

            var model = new SettingsViewModel
            {
                Mode = ParseQuizLearnMode(modeValue)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Settings(SettingsViewModel model)
        {
            var rawMode = Request.Form["Mode"];

            int parsedMode;
            QuizLearnMode mode;

            if (int.TryParse(rawMode, out parsedMode) &&
                Enum.IsDefined(typeof(QuizLearnMode), parsedMode))
            {
                mode = (QuizLearnMode)parsedMode;
            }
            else if (!Enum.TryParse(rawMode, true, out mode))
            {
                mode = QuizLearnMode.RecommendationWithLevelAssessment;
            }

            var moduleController = new ModuleController();

            moduleController.UpdateModuleSetting(
                ModuleContext.ModuleId,
                "QuizLearnMode",
                ((int)mode).ToString()
            );

            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(ModuleContext.ModuleId);

            TempData["SettingsSaved"] = true;

            return RedirectToAction("Settings");
        }
        private QuizLearnMode ParseQuizLearnMode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return QuizLearnMode.RecommendationWithLevelAssessment;
            }

            int parsedMode;

            if (int.TryParse(value, out parsedMode) &&
                Enum.IsDefined(typeof(QuizLearnMode), parsedMode))
            {
                return (QuizLearnMode)parsedMode;
            }

            QuizLearnMode parsedEnum;

            if (Enum.TryParse(value, true, out parsedEnum))
            {
                return parsedEnum;
            }

            return QuizLearnMode.RecommendationWithLevelAssessment;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult Settings()
        //{
        //    var settings = new Models.Settings();
        //    settings.Setting1 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Dnn.Flow.QuizLearn_Setting1", false);
        //    settings.Setting2 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("Dnn.Flow.QuizLearn_Setting2", System.DateTime.Now);

        //    return View(settings);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="supportsTokens"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateInput(false)]
        //[DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        //public ActionResult Settings(Models.Settings settings)
        //{
        //    ModuleContext.Configuration.ModuleSettings["Dnn.Flow.QuizLearn_Setting1"] = settings.Setting1.ToString();
        //    ModuleContext.Configuration.ModuleSettings["Dnn.Flow.QuizLearn_Setting2"] = settings.Setting2.ToUniversalTime().ToString("u");

        //    return RedirectToDefaultRoute();
        //}
    }
}