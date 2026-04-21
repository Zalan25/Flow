using System;
namespace Dnn.Flow.QuizLearn.Models
{
    public class AssessmentSessionInfo
    {
        public int ModuleId { get; set; }
        public int AssessmentModeId { get; set; }
        public int LanguageId { get; set; }

        public int? SecondaryLanguageId { get; set; }  
        public int? SelectedLevelId { get; set; }      
        public int? UserId { get; set; }               

        public int? PaceTypeId { get; set; }

        public bool NeedLevelTest { get; set; }
        public string Status { get; set; }
    }
}
