using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBankClient
{
    // A Kérdőív (Megfelel a dbo.lm_tests táblának)
    public class Quiz
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; } = 1; // Alapértelmezett: English (SQL szerint)
        public int TotalPoints { get; set; }
        public bool IsRandom { get; set; }
        public bool IsActive { get; set; } = true;

        // A kérdések listája a teszten belül
        public List<Question> Questions { get; set; } = new List<Question>();
    }

    // A Kérdés (Megfelel a dbo.lm_questions táblának)
    public class Question
    {
        public int QuestionId { get; set; } // SQL-ből jön (Identity)
        public string QuestionText { get; set; }
        public int LanguageId { get; set; } = 1;
        public int QuestionLevelId { get; set; } = 1; // A1, A2, stb.
        public int QuestionTypeId { get; set; } // 1: SingleChoice, stb.
        public int SkillTypeId { get; set; } // Vocabulary, Reading, stb.
        public int Points { get; set; }
        public bool IsActive { get; set; } = true;

        // Bővített mezők a validációhoz (amiket az UI-on hozzáadtunk)
        public string ValidationType { get; set; } // szöveg, szám, email
        public int? MaxCharacters { get; set; }

        // A kérdéshez tartozó válaszok listája
        public List<Answer> Answers { get; set; } = new List<Answer>();

        // Segédtulajdonság az UI-hoz (nem mentjük az SQL-be közvetlenül ebbe a táblába)
        public string UI_TypeKey { get; set; } // "Short", "tf", "Multi"
    }

    // A Válasz (Megfelel a dbo.lm_answers táblának)
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int AnswerOrder { get; set; }
    }

    
    
        public static class DropdownData
        {
            // Az SQL adatbázis lm_languages táblája alapján pontosítva
            public static List<KeyValuePair<int, string>> GetLanguages()
            {
                return new List<KeyValuePair<int, string>>() {
                new KeyValuePair<int, string>(1, "Angol"),
                new KeyValuePair<int, string>(2, "Görög"),
                new KeyValuePair<int, string>(3, "Kínai"),
                new KeyValuePair<int, string>(4, "Orosz"),
                new KeyValuePair<int, string>(5, "Japán"),
                new KeyValuePair<int, string>(6, "Török"),
                new KeyValuePair<int, string>(7, "Horvát"),
                new KeyValuePair<int, string>(8, "Portugál"),
                new KeyValuePair<int, string>(10, "Magyar")
            };
            }

                

            // Az SQL adatbázis lm_question_levels táblája alapján
            public static List<KeyValuePair<int, string>> GetLevels()
            {
                return new List<KeyValuePair<int, string>>() {
                new KeyValuePair<int, string>(1, "A1"),
                new KeyValuePair<int, string>(2, "A2"),
                new KeyValuePair<int, string>(3, "B1"),
                new KeyValuePair<int, string>(4, "B2"),
                new KeyValuePair<int, string>(5, "C1")
            };
            }

            public static Dictionary<int, string> GetSkills()
            {
            return new Dictionary<int, string>
            {
            { 1, "Vocabulary" },
            { 2, "Reading" },
            { 3, "Listening" },
            { 4, "Practice" },
            { 5, "SelfStudy" },
            { 6, "Book" }
    };
        }
    }
    
}