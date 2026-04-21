using System;

namespace Dnn.Flow.QuizLearn.Models
{

    public class PaceTypeInfo
    {
        public int PaceTypeId { get; set; }
        public int PaceKey { get; set; }
        public string PaceName { get; set; }
        public int MinProducts { get; set; }
        public int MaxProducts { get; set; }
        public bool IsActive { get; set; }
    }
}
