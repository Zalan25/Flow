using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using client_web_api.Models;

namespace client_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly string _connectionString;

        public QuizController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 1. VÉGPONT: Teszt
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return Ok(new { Success = true, Message = "Siker! Az API csatlakozott az adatbázishoz." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // 2. VÉGPONT: Az összes aktív kérdőív listázása (ID, Név, Leírás)
        [HttpGet("list")]
        public IActionResult GetQuizzes()
        {
            var quizzes = new List<Quiz>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT TestId, TestName, Characterization FROM dbo.lm_tests WHERE IsActive = 1 ORDER BY TestId DESC";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            quizzes.Add(new Quiz
                            {
                                TestId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                            });
                        }
                    }
                }
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 3. VÉGPONT: Egyetlen teljes kérdőív betöltése kérdésekkel és válaszokkal
        [HttpGet("{id}")]
        public IActionResult GetFullQuiz(int id)
        {
            Quiz quiz = new Quiz { TestId = id };
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Fejléc
                    string sqlHeader = "SELECT TestName, Characterization FROM dbo.lm_tests WHERE TestId = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlHeader, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                quiz.Title = r.GetString(0);
                                quiz.Description = r.IsDBNull(1) ? "" : r.GetString(1);
                            }
                            else return NotFound();
                        }
                    }

                    // Kérdések
                    string sqlQuestions = @"SELECT q.QuestionId, q.QuestionText, q.Points, q.QuestionTypeId 
                                          FROM dbo.lm_questions q
                                          JOIN dbo.lm_test_questions tq ON q.QuestionId = tq.QuestionId
                                          WHERE tq.TestId = @id ORDER BY tq.QuestionOrder";
                    using (SqlCommand cmd = new SqlCommand(sqlQuestions, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                var q = new Question
                                {
                                    QuestionId = r.GetInt32(0),
                                    QuestionText = r.GetString(1),
                                    Points = r.GetInt32(2)
                                };
                                int typeId = r.GetInt32(3);
                                if (typeId == 1) q.UI_TypeKey = "Multi";
                                else if (typeId == 2) q.UI_TypeKey = "tf";
                                else if (typeId == 3) q.UI_TypeKey = "Short";
                                quiz.Questions.Add(q);
                            }
                        }
                    }

                    // Válaszok
                    foreach (var q in quiz.Questions)
                    {
                        string sqlAns = "SELECT AnswerText, IsCorrect, AnswerOrder FROM dbo.lm_answers WHERE QuestionId = @qid";
                        using (SqlCommand cmd = new SqlCommand(sqlAns, conn))
                        {
                            cmd.Parameters.AddWithValue("@qid", q.QuestionId);
                            using (var r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    q.Answers.Add(new Answer
                                    {
                                        AnswerText = r.GetString(0),
                                        IsCorrect = r.GetBoolean(1),
                                        AnswerOrder = r.GetInt32(2)
                                    });
                                }
                            }
                        }
                    }
                }
                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 4. VÉGPONT: Kérdőív törlése
        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string sqlAnswers = @"DELETE a FROM dbo.lm_answers a
                                                  JOIN dbo.lm_test_questions tq ON a.QuestionId = tq.QuestionId
                                                  WHERE tq.TestId = @TestId";
                            using (SqlCommand cmd = new SqlCommand(sqlAnswers, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@TestId", id);
                                cmd.ExecuteNonQuery();
                            }

                            string sqlTestQuestions = "DELETE FROM dbo.lm_test_questions WHERE TestId = @TestId";
                            using (SqlCommand cmd = new SqlCommand(sqlTestQuestions, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@TestId", id);
                                cmd.ExecuteNonQuery();
                            }

                            string sqlTest = "DELETE FROM dbo.lm_tests WHERE TestId = @TestId";
                            using (SqlCommand cmd = new SqlCommand(sqlTest, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@TestId", id);
                                cmd.ExecuteNonQuery();
                            }

                            trans.Commit();
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            return StatusCode(500, "Tranzakciós hiba: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 5. VÉGPONT: Teljes kérdőív mentése (INSERT vagy UPDATE)
        [HttpPost("save")]
        public IActionResult SaveQuiz([FromBody] Quiz activeQuiz)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            int testId = activeQuiz.TestId;

                            // 1. Fejléc UPDATE vagy INSERT
                            if (testId > 0)
                            {
                                string sql = "UPDATE dbo.lm_tests SET TestName = @name, Characterization = @desc WHERE TestId = @id";
                                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(activeQuiz.Title) ? "Névtelen teszt" : activeQuiz.Title);
                                    cmd.Parameters.AddWithValue("@desc", activeQuiz.Description ?? "");
                                    cmd.Parameters.AddWithValue("@id", testId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string sql = @"INSERT INTO dbo.lm_tests (ModuleId, TestName, Characterization, LanguageId, TotalPoints, IsRandom, IsActive)
                                               OUTPUT INSERTED.TestId
                                               VALUES (1, @name, @desc, 1, 0, 0, 1)";
                                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(activeQuiz.Title) ? "Névtelen teszt" : activeQuiz.Title);
                                    cmd.Parameters.AddWithValue("@desc", activeQuiz.Description ?? "");
                                    testId = (int)cmd.ExecuteScalar();
                                }
                            }

                            // 2. Régi kérdések törlése (ha frissítés)
                            if (activeQuiz.TestId > 0)
                            {
                                string delSql = "DELETE FROM dbo.lm_test_questions WHERE TestId = @tid";
                                using (SqlCommand cmdDel = new SqlCommand(delSql, conn, trans))
                                {
                                    cmdDel.Parameters.AddWithValue("@tid", testId);
                                    cmdDel.ExecuteNonQuery();
                                }
                            }

                            // 3. Új kérdések beszúrása
                            int order = 1;
                            foreach (var data in activeQuiz.Questions)
                            {
                                int qTypeId = 1;
                                if (data.UI_TypeKey == "tf") qTypeId = 2;
                                else if (data.UI_TypeKey == "Short") qTypeId = 3;

                                int questionId;
                                string sqlQ = @"INSERT INTO dbo.lm_questions (ModuleId, QuestionText, LanguageId, QuestionLevelId, QuestionTypeId, SkillTypeId, Points, IsActive)
                                                OUTPUT INSERTED.QuestionId
                                                VALUES (1, @text, 1, 1, @typeId, 1, @pts, 1)";
                                using (SqlCommand cmdQ = new SqlCommand(sqlQ, conn, trans))
                                {
                                    cmdQ.Parameters.AddWithValue("@text", string.IsNullOrWhiteSpace(data.QuestionText) ? "Névtelen kérdés" : data.QuestionText);
                                    cmdQ.Parameters.AddWithValue("@typeId", qTypeId);
                                    cmdQ.Parameters.AddWithValue("@pts", data.Points);
                                    questionId = (int)cmdQ.ExecuteScalar();
                                }

                                string sqlL = "INSERT INTO dbo.lm_test_questions (ModuleId, TestId, QuestionId, QuestionOrder) VALUES (1, @tid, @qid, @ord)";
                                using (SqlCommand cmdL = new SqlCommand(sqlL, conn, trans))
                                {
                                    cmdL.Parameters.AddWithValue("@tid", testId);
                                    cmdL.Parameters.AddWithValue("@qid", questionId);
                                    cmdL.Parameters.AddWithValue("@ord", order++);
                                    cmdL.ExecuteNonQuery();
                                }

                                if (data.Answers != null && data.Answers.Count > 0)
                                {
                                    foreach (var ans in data.Answers)
                                    {
                                        string sqlA = "INSERT INTO dbo.lm_answers (ModuleId, QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (1, @qid, @txt, @isc, @ord)";
                                        using (SqlCommand cmdA = new SqlCommand(sqlA, conn, trans))
                                        {
                                            cmdA.Parameters.AddWithValue("@qid", questionId);
                                            cmdA.Parameters.AddWithValue("@txt", ans.AnswerText);
                                            cmdA.Parameters.AddWithValue("@isc", ans.IsCorrect);
                                            cmdA.Parameters.AddWithValue("@ord", ans.AnswerOrder);
                                            cmdA.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            trans.Commit();
                            return Ok(new { Success = true, TestId = testId });
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            return StatusCode(500, "Mentési hiba: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Adatbázis hiba: " + ex.Message);
            }
        }
    }
}