using System;
using System.Web.Mvc;
using Dnn.Flow.QuizLearn.Models;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Web.Mvc.Framework.Controllers;

namespace Dnn.Flow.QuizLearn.Models
{
    public class SettingsViewModel
    {
        public QuizLearnMode Mode { get; set; }

        public IEnumerable<SelectedListItem> AvailableModes
        {
            get
            {
                return new List<SelectedListItem>
                {
                    new SelectedListItem
                    {
                        Value = ((int)QuizLearnMode.Recommendation).ToString(),
                        Text = "Csak termék ajánló"
                    },
                    new SelectedListItem
                    {
                        Value = ((int)QuizLearnMode.LevelAssessment).ToString(),
                        Text = "Csak szintfelmérő"
                    },
                    new SelectedListItem
                    {
                        Value = ((int)QuizLearnMode.RecommendationWithLevelAssessment).ToString(),
                        Text = "Termék ajánló szintfelmérővel"
                    }
                }
            }
        }
    }
}