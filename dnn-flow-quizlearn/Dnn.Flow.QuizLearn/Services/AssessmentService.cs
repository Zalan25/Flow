using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;
using Dnn.Flow.QuizLearn.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (c1CorrectCount >= 3 && b2CorrectCount >= 3 && b1CorrectCount >= 3)
            {
                return 5; // C1
            }

            if (b2CorrectCount >= 3 && b1CorrectCount >= 3)
            {
                return 4; // B2
            }
            
            if (b1CorrectCount >= 3)
            {
                return 3; // B1
            }

            if (a2CorrectCount >= 3)
            {
                return 2; // A2
            }

            return 1; // A1
        }

        public QuestionViewModel GetQuestionForAssessment(int sessionId, int questionNumber)
        {
            var session = _repository.GetAssessmentSessionById(sessionId);

            if (session == null)
            {
                return null;
            }

            var questions = _repository.GetSessionQuestions(sessionId).ToList();

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
                QuestionTypeId = question.QuestionTypeId,
                QuestionNumber = questionNumber,
                TotalQuestions = questions.Count,
                QuestionText = question.QuestionText,
                LevelName = null,
                Answers = answers
            };
        }

        public void SaveTextAnswer(int moduleId, int sessionId, int questionId, string textAnswer)
        {
            _repository.AddTextAnswer(moduleId, sessionId, questionId, textAnswer);
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

        // Többválaszos kérdés mentése
        public void SaveSingleChoiceAnswer(int moduleId, int sessionId, int questionId, int answerId)
        {
            _repository.AddSingleChoiceAnswer(moduleId, sessionId, questionId, answerId);
        }

        public void SaveMultipleChoiceAnswer(int moduleId, int sessionId, int questionId, IEnumerable<int> answerIds)
        {
            var testAttemptAnswerId = _repository.StartMultipleChoiceAnswer(
                moduleId,
                sessionId,
                questionId
            );

            foreach (var answerId in answerIds)
            {
                _repository.AddMultipleChoiceAnswerOption(
                    moduleId,
                    testAttemptAnswerId,
                    answerId
                );
            }

            _repository.GradeMultipleChoiceAnswer(moduleId, testAttemptAnswerId);
        }


        //public void SaveTextAnswer(int moduleId, int sessionId, int questionId, string textAnswer)
        //{
        //    _repository.AddTextAnswer(moduleId, sessionId, questionId, textAnswer);
        //}

        //Eredmény kiszámítása a szintfelmérő teszt után

        private int GetRequiredCorrectCount(string levelName, int questionCount)
        {
            if (questionCount <= 0)
            {
                return int.MaxValue;
            }

            switch (levelName)
            {
                case "A1":
                case "A2":
                case "B1":
                    return Math.Max(1, questionCount - 2);

                case "B2":
                    return Math.Max(1, questionCount - 1);

                case "C1":
                    return Math.Max(1, (int)Math.Ceiling(questionCount / 2.0));

                default:
                    return int.MaxValue;
            }
        }
        private int DetermineFinalLevel(
            int a1Correct,
            int a1Count,
            int a2Correct,
            int a2Count,
            int b1Correct,
            int b1Count,
            int b2Correct,
            int b2Count,
            int c1Correct,
            int c1Count)
        {
            bool passedA1 = a1Correct >= GetRequiredCorrectCount("A1", a1Count);
            bool passedA2 = a2Correct >= GetRequiredCorrectCount("A2", a2Count);
            bool passedB1 = b1Correct >= GetRequiredCorrectCount("B1", b1Count);
            bool passedB2 = b2Correct >= GetRequiredCorrectCount("B2", b2Count);
            bool passedC1 = c1Correct >= GetRequiredCorrectCount("C1", c1Count);

            if (passedA1 && passedA2 && passedB1 && passedB2 && passedC1)
            {
                return 5; // C1
            }

            if (passedA1 && passedA2 && passedB1 && passedB2)
            {
                return 4; // B2
            }

            if (passedA1 && passedA2 && passedB1)
            {
                return 3; // B1
            }

            if (passedA1 && passedA2)
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

            _repository.CompleteTestAttempt(
                moduleId,
                sessionId,
                totalScore,
                a1Correct,
                a2Correct,
                b1Correct,
                b2Correct,
                c1Correct,
                finalLevelId
            );

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

        public int PrepareAssessmentSessionTest(int moduleId, int sessionId, int languageId)
        {
            return _repository.PrepareAssessmentSessionTest(moduleId, sessionId, languageId);
        }
    }
}