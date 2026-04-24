using System.Collections.Generic;
using System.Linq;
using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services.Interfaces;

namespace Dnn.Flow.QuizLearn.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IQuizLearnDataRepository _repository;
        public AssessmentService()
        {
            _repository = new SqlDataProvider();
        }

        public int StartAssessmentSession(AssessmentSessionInfo sessionInfo, IEnumerable<int> selectedSkillTypeIds)
        {
            var sessionId = _repository.AddAssessmentSession(sessionInfo);

            foreach (var skillTypeId in selectedSkillTypeIds)
            {
                _repository.AddAssessmentSessionSkill(
                    sessionInfo.ModuleId,
                    sessionId,
                    skillTypeId
                );
            }

            return sessionId;
        }

        public int CompleteAssessmentSession(int moduleId, int assessmentSessionId, int? finalLevelId)
        {
            return _repository.CompleteAssessmentSession(moduleId, assessmentSessionId, finalLevelId);
        }

        public int? DetermineFinalLevel(int a1CorrectCount, int a2CorrectCount, int b1CorrectCount, int b2CorrectCount, int c1CorrectCount)
        {
            if (a1CorrectCount < 3)
            {
                return 1; // A1
            }

            if (a2CorrectCount < 3)
            {
                return 2; // A2
            }

            if (b1CorrectCount < 3)
            {
                return 3; // B1
            }

            if (b2CorrectCount < 3)
            {
                return 4; // B2
            }

            if (c1CorrectCount < 3)
            {
                return 4; // C1
            }
            return 5;
        }

        public QuestionViewModel GetQuestionForAssessment(int sessionId, int questionNumber)
        {
            var session = _repository.GetAssessmentSessionById(sessionId);

            if (session == null)
            {
                return null;
            }

            var questions = _repository.GetQuestionsForAssessment(session.ModuleId, session.LanguageId).ToList();

            if (!questions.Any())
            {
                return null;
            }

            if (questionNumber < 1 || questionNumber > questions.Count)
            {
                return null;
            }

            var question = questions[questionNumber - 1];

            var answers = _repository.GetAnswersByQuestionId(session.ModuleId, question.QuestionId)
                .Select(a => new AnswerOptionViewModel
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText
                })
                .ToList();

            return new QuestionViewModel
            {
                SessionId = sessionId,
                QuestionId = question.QuestionId,
                QuestionNumber = questionNumber,
                TotalQuestions = questions.Count,
                QuestionText = question.QuestionText,
                LevelName = null,
                Answers = answers
            };
        }
        public AssessmentSessionInfo GetAssessmentSessionById(int sessionId)
        {
            return _repository.GetAssessmentSessionById(sessionId);
        }
        public int StartTestAttempt(int moduleId, int assessmentSessionId, int testId)
        {
            return _repository.StartTestAttempt(moduleId, assessmentSessionId, testId);
        }
        public void SaveAnswer(int moduleId, int sessionId, int questionId, int answerId)
        {
             _repository.AddTestAttemptAnswer(moduleId, sessionId, questionId, answerId);
        }


        //Eredmény kiszámítása a szintfelmérő teszt után
        private int DetermineFinalLevel(
            int totalScore,
            int a1Correct,
            int a2Correct,
            int b1Correct,
            int b2Correct,
            int c1Correct)
        {
            if (totalScore >= 43 && totalScore <= 50 && b2Correct >= 2 && c1Correct >= 1)
            {
                return 5; // C1 közeli
            }

            if (totalScore >= 33 && totalScore <= 42 && b1Correct >= 3)
            {
                return 4; // B2
            }

            if (totalScore >= 21 && totalScore <= 32 && a2Correct >= 3)
            {
                return 3; // B1
            }

            if (totalScore >= 11 && totalScore <= 20 && a1Correct >= 3)
            {
                return 2; // A2
            }

            return 1; // A1
        }
        private string GetLevelName(int levelId)
        {
            switch (levelId)
            {
                case 1:
                    return "A1";
                case 2:
                    return "A2";
                case 3:
                    return "B1";
                case 4:
                    return "B2";
                case 5:
                    return "C1 közeli";
                default:
                    return "A1";
            }
        }

        public ResultViewModel CalculateResult(int moduleId, int sessionId)
        {
            var answers = _repository.GetAttemptAnswerSummary(moduleId, sessionId).ToList();

            if (!answers.Any())
            {
                return new ResultViewModel
                {
                    SessionId = sessionId,
                    TotalScore = 0,
                    FinalLevelId = 1,
                    FinalLevelName = "A1"
                };
            }

            var totalScore = answers.Sum(x => x.EarnedPoints);

            var a1Correct = answers.Count(x => x.QuestionLevelId == 1 && x.IsCorrect);
            var a2Correct = answers.Count(x => x.QuestionLevelId == 2 && x.IsCorrect);
            var b1Correct = answers.Count(x => x.QuestionLevelId == 3 && x.IsCorrect);
            var b2Correct = answers.Count(x => x.QuestionLevelId == 4 && x.IsCorrect);
            var c1Correct = answers.Count(x => x.QuestionLevelId == 5 && x.IsCorrect);

            var finalLevelId = DetermineFinalLevel(
                totalScore,
                a1Correct,
                a2Correct,
                b1Correct,
                b2Correct,
                c1Correct
            );

            _repository.CompleteAssessmentSession(moduleId, sessionId, finalLevelId);

            return new ResultViewModel
            {
                SessionId = sessionId,
                TotalScore = totalScore,
                FinalLevelId = finalLevelId,
                FinalLevelName = GetLevelName(finalLevelId),
                A1Correct = a1Correct,
                A2Correct = a2Correct,
                B1Correct = b1Correct,
                B2Correct = b2Correct,
                C1Correct = c1Correct
            };
        }
    }
}