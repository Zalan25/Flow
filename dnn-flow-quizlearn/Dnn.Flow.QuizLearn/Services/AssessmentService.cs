using System.Collections.Generic;
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
            var assessmentSessionId = _repository.AddAssessmentSession(sessionInfo);

            foreach (var skillTypeId in selectedSkillTypeIds)
            {
                _repository.AddAssessmentSessionSkill(
                    sessionInfo.ModuleId,
                    assessmentSessionId,
                    skillTypeId);
            }

            return assessmentSessionId;
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

        public AssessmentQuestionViewModel GetQuestionForAssessment(int sessionId, int questionNumber)
        {
            var session = _repository.GetAssessmentSessionById(sessionId);

            if (session == null)
            {
                return null;
            }

            var questions = _repository.GetQuestionsForAssessment(session.ModuleId, session.LanguageId)
                .ToList();

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

            return new AssessmentQuestionViewModel
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
    }
}