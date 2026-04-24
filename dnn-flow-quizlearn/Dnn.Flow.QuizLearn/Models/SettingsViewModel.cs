using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using System.Web.WebPages.Html;
using Dnn.Flow.QuizLearn.Models;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.Mvc.Framework.Controllers;

namespace Dnn.Flow.QuizLearn.Models
{
    public class SettingsViewModel
    {
        public QuizLearnMode Mode { get; set; }

        public IEnumerable<SelectListItem> AvailableModes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = ((int)QuizLearnMode.Recommendation).ToString(),
                        Text = "Csak termék ajánló"
                    },
                    new SelectListItem
                    {
                        Value = ((int)QuizLearnMode.LevelAssessment).ToString(),
                        Text = "Csak szintfelmérő"
                    },
                    new SelectListItem
                    {
                        Value = ((int)QuizLearnMode.RecommendationWithLevelAssessment).ToString(),
                        Text = "Termék ajánló szintfelmérővel"
                    }
                };
            }
        }
    }
}