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
using System.Linq;
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

            QuizLearnMode mode;

            if (!Enum.TryParse(modeValue, out mode))
            {
                mode = QuizLearnMode.RecommendationWithLevelAssessment;
            }

            var model = new SettingsViewModel
            {
                Mode = mode
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

            TempData["SettingsSaved"] = true;

            return RedirectToAction("Index", "Item");


        }


    }
}