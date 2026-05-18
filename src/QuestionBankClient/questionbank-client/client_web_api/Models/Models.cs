using System;
using System.Collections.Generic;

namespace client_web_api.Models
{
    public class Quiz
    {
        public int TestId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int LanguageId { get; set; } = 1;
        public int TotalPoints { get; set; }
        public bool IsRandom { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Question> Questions { get; set; } = new List<Question>();
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int LanguageId { get; set; } = 1;
        public int QuestionLevelId { get; set; } = 1;
        public int QuestionTypeId { get; set; }
        public int SkillTypeId { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; } = true;

        // Bővített mezők a validációhoz (Kérdőjel hozzáadva!)
        public string? ValidationType { get; set; }
        public int? MaxCharacters { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();

        // Kérdőjel hozzáadva!
        public string? UI_TypeKey { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int AnswerOrder { get; set; }
    }
}