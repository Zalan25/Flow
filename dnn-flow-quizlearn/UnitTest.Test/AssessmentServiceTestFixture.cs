using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using Dnn.Flow.QuizLearn.Services;
using Dnn.Flow.QuizLearn.Data;
using Dnn.Flow.QuizLearn.Models;

namespace Dnn.Flow.QuizLearn.Unittest
{
    // Az osztályt a TestFixture jelöli
    [TestFixture]
    public class AssessmentServiceTestFixture
    {
        // 1. Teszt: A publikus DetermineFinalLevel tesztelése TestCase-ekkel
        // A Test és a TestCase attribútumok a metódus fölött vannak!
        [Test]
        [TestCase(0, 0, 0, 0, 0, 1)] // Minden 0 -> A1 szint (alapértelmezett)
        [TestCase(0, 3, 0, 0, 0, 2)] // 3 db A2 jó -> A2 szint
        [TestCase(0, 0, 3, 0, 0, 3)] // 3 db B1 jó -> B1 szint
        [TestCase(0, 0, 3, 3, 0, 4)] // 3 db B1 és 3 db B2 jó -> B2 szint
        [TestCase(0, 0, 3, 3, 3, 5)] // 3 db B1, B2 és C1 jó -> C1 szint
        public void TestDetermineFinalLevel(int a1, int a2, int b1, int b2, int c1, int expectedLevel)
        {
            // Arrange
            var repositoryMock = new Mock<IQuizLearnDataRepository>();
            var assessmentService = new AssessmentService(repositoryMock.Object);

            // Act
            var actualResult = assessmentService.DetermineFinalLevel(a1, a2, b1, b2, c1);

            // Assert
            Assert.AreEqual(expectedLevel, actualResult);
        }

        // 2. Teszt: Eredmény kiszámítása (CalculateResult) - HappyPath (Sikeres lefutás B2 szinttel)
        [Test]
        public void TestCalculateResult_HappyPath_ReturnsB2()
        {
            // Arrange
            int moduleId = 1;
            int sessionId = 100;
            int expectedLevelId = 4; // B2

            var repositoryMock = new Mock<IQuizLearnDataRepository>(MockBehavior.Strict);

            var mockAnswers = new List<AttemptAnswerSummaryInfo>
            {
                new AttemptAnswerSummaryInfo { EarnedPoints = 15, QuestionLevelId = 3, IsCorrect = true },
                new AttemptAnswerSummaryInfo { EarnedPoints = 10, QuestionLevelId = 3, IsCorrect = true },
                new AttemptAnswerSummaryInfo { EarnedPoints = 10, QuestionLevelId = 3, IsCorrect = true }
            };

            repositoryMock
                .Setup(m => m.GetAttemptAnswerSummary(moduleId, sessionId))
                .Returns(mockAnswers);

            repositoryMock
                .Setup(m => m.CompleteAssessmentSession(moduleId, sessionId, expectedLevelId))
                .Returns(1);

            var assessmentService = new AssessmentService(repositoryMock.Object);

            // Act
            var actualResult = assessmentService.CalculateResult(moduleId, sessionId);

            // Assert
            Assert.AreEqual(sessionId, actualResult.SessionId);
            Assert.AreEqual(35, actualResult.TotalScore);
            Assert.AreEqual(expectedLevelId, actualResult.FinalLevelId);
            Assert.AreEqual("B2", actualResult.FinalLevelName);

            repositoryMock.Verify(m => m.GetAttemptAnswerSummary(moduleId, sessionId), Times.Once);
            repositoryMock.Verify(m => m.CompleteAssessmentSession(moduleId, sessionId, expectedLevelId), Times.Once);
        }

        // 3. Teszt: Session indítása (StartAssessmentSession) - HappyPath több skill mentésével
        [Test]
        public void TestStartAssessmentSession_HappyPath()
        {
            // Arrange
            var repositoryMock = new Mock<IQuizLearnDataRepository>(MockBehavior.Strict);

            var sessionInfo = new AssessmentSessionInfo { ModuleId = 10 };
            var selectedSkillTypeIds = new List<int> { 5, 8 };
            int expectedSessionId = 999;

            repositoryMock
                .Setup(m => m.AddAssessmentSession(sessionInfo))
                .Returns(expectedSessionId);

            repositoryMock
                .Setup(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 5));

            repositoryMock
                .Setup(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 8));

            var assessmentService = new AssessmentService(repositoryMock.Object);

            // Act
            var actualResult = assessmentService.StartAssessmentSession(sessionInfo, selectedSkillTypeIds);

            // Assert
            Assert.AreEqual(expectedSessionId, actualResult);

            repositoryMock.Verify(m => m.AddAssessmentSession(sessionInfo), Times.Once);
            repositoryMock.Verify(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 5), Times.Once);
            repositoryMock.Verify(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 8), Times.Once);
        }
    }
}