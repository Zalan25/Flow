using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Flow.QuizLearn.Models
{
    public class BundleCompositionRuleInfo
    {
        public int BundleCompositionRuleId { get; set; }
        public int ModuleId { get; set; }
        public int FocusSkillTypeId { get; set; }
        public int PaceTypeId { get; set; }
        public int ProductSkillTypeId { get; set; }
        public int ProductCount { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
    }
}