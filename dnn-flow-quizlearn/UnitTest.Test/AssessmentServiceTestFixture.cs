using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnn.Flow.QuizLearn.Controllers;

namespace Dnn.Flow.QuizLearn.UnitTest
{
    public class AssessmentServiceTestFixture
    {
        [Test]
        public void Test1()
        {
            public class AssessmentServiceTestFixture
        {
            // 1. Teszt: A publikus DetermineFinalLevel tesztelése TestCase-ekkel
            // Ez a függvény nem használ adatbázist, így nagyon jól tesztelhető a paraméteres TestCase-ekkel,
            [cite_start]// ahogy a leírás e-mail validálós példájában is szerepel[cite: 118, 119, 120, 121, 122, 123].
            [
                Test,
                TestCase(0, 0, 0, 0, 0, 1), // Minden 0 -> A1 szint (alapértelmezett)
                TestCase(0, 3, 0, 0, 0, 2), // 3 db A2 jó -> A2 szint
                TestCase(0, 0, 3, 0, 0, 3), // 3 db B1 jó -> B1 szint
                TestCase(0, 0, 3, 3, 0, 4), // 3 db B1 és 3 db B2 jó -> B2 szint
                TestCase(0, 0, 3, 3, 3, 5)  // 3 db B1, B2 és C1 jó -> C1 szint
            ]
            public void TestDetermineFinalLevel(int a1, int a2, int b1, int b2, int c1, int expectedLevel)
            {
                // Arrange
                [cite_start]// Itt nincs szükség adatbázis hívásra, egy üres (vagy null) mockkal is példányosíthatunk[cite: 100, 106].
                var repositoryMock = new Mock<IQuizLearnDataRepository>();
                var assessmentService = new AssessmentService(repositoryMock.Object);

                // Act
                [cite_start]// Meghívjuk a tesztelni kívánt funkciót[cite: 102, 108].
                var actualResult = assessmentService.DetermineFinalLevel(a1, a2, b1, b2, c1);

                // Assert
                [cite_start]// Összehasonlítjuk az elvárt eredményt a ténylegessel.
                Assert.AreEqual(expectedLevel, actualResult);
            }

            // 2. Teszt: Eredmény kiszámítása (CalculateResult) - HappyPath (Sikeres lefutás B2 szinttel)
            [cite_start]// Ebben a tesztben egy Mock objektumot hozunk létre Strict beállítással[cite: 235, 237, 247].
            [Test]
            public void TestCalculateResult_HappyPath_ReturnsB2()
            {
                // Arrange
                int moduleId = 1;
                int sessionId = 100;
                int expectedLevelId = 4; // B2

                [cite_start]// Mock objektum inicializálása szigorú viselkedéssel [cite: 247, 271]
                var repositoryMock = new Mock<IQuizLearnDataRepository>(MockBehavior.Strict);

                // Adatbázis válaszának előkészítése: a B2 eléréséhez >= 33 pont és >= 3 B1 helyes válasz kell
                var mockAnswers = new List<AttemptAnswerSummary>
            {
                new AttemptAnswerSummary { EarnedPoints = 15, QuestionLevelId = 3, IsCorrect = true }, // B1
                new AttemptAnswerSummary { EarnedPoints = 10, QuestionLevelId = 3, IsCorrect = true }, // B1
                new AttemptAnswerSummary { EarnedPoints = 10, QuestionLevelId = 3, IsCorrect = true }  // B1
            };

                [cite_start]// Setupoljuk az adatbázis függvényeket[cite: 238, 249, 273].
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

                [cite_start]// Ellenőrizzük, hogy a függvényhívás a megfelelő adatokkal pontosan egyszer megtörtént-e[cite: 244, 245, 259].
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

                [cite_start]// Session hozzáadásának előkészítése.
                repositoryMock
                    .Setup(m => m.AddAssessmentSession(sessionInfo))
                    .Returns(expectedSessionId);

                [cite_start]// A ciklus miatt minden skillre külön Setup kell, ha Strict a Mock[cite: 237, 247].
                repositoryMock
                    .Setup(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 5));

                repositoryMock
                    .Setup(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 8));

                var assessmentService = new AssessmentService(repositoryMock.Object);

                // Act
                var actualResult = assessmentService.StartAssessmentSession(sessionInfo, selectedSkillTypeIds);

                // Assert
                Assert.AreEqual(expectedSessionId, actualResult);

                [cite_start]// Verifikáció, hogy minden adatbázis mentés pontosan egyszer lefutott[cite: 244, 245, 259].
                repositoryMock.Verify(m => m.AddAssessmentSession(sessionInfo), Times.Once);
                repositoryMock.Verify(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 5), Times.Once);
                repositoryMock.Verify(m => m.AddAssessmentSessionSkill(10, expectedSessionId, 8), Times.Once);
            }
        }
    }
}
