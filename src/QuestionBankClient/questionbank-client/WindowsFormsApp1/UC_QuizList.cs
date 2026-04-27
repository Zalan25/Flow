using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuestionBankClient
{
    public partial class UC_QuizList : UserControl
    {
        public UC_QuizList()
        {
            InitializeComponent();
            LoadQuizzesFromDatabase();
        }

        private Quiz FetchFullQuiz(int testId)
        {
            Quiz quiz = new Quiz { TestId = testId };

            try
            {
                using (SqlConnection conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    // 1. Teszt fejléc adatai
                    string sqlHeader = "SELECT TestName, Characterization FROM dbo.lm_tests WHERE TestId = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlHeader, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", testId);
                        using (var r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                quiz.Title = r.GetString(0);
                                quiz.Description = r.IsDBNull(1) ? "" : r.GetString(1);
                            }
                        }
                    }

                    // 2. Kérdések adatai (Összekötve a kapcsolótáblával)
                    string sqlQuestions = @"SELECT q.QuestionId, q.QuestionText, q.Points, q.QuestionTypeId 
                                  FROM dbo.lm_questions q
                                  JOIN dbo.lm_test_questions tq ON q.QuestionId = tq.QuestionId
                                  WHERE tq.TestId = @id ORDER BY tq.QuestionOrder";

                    using (SqlCommand cmd = new SqlCommand(sqlQuestions, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", testId);
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
                                // Visszaalakítjuk az ID-t a mi kulcsainkra
                                int typeId = r.GetInt32(3);
                                if (typeId == 1) q.UI_TypeKey = "Multi";
                                else if (typeId == 2) q.UI_TypeKey = "tf";
                                else if (typeId == 3) q.UI_TypeKey = "Short";

                                quiz.Questions.Add(q);
                            }
                        }
                    }

                    // 3. Válaszok kiolvasása minden kérdéshez
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
                return quiz;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a betöltéskor: " + ex.Message);
                return null;
            }
        }

        private void LoadQuizzesFromDatabase()
        {
            flpQuizzes.Controls.Clear();

            try
            {
                using (SqlConnection conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    // Lekérjük a tesztek ID-jét, Nevét és Leírását az SQL-ből
                    string sql = "SELECT TestId, TestName, Characterization FROM dbo.lm_tests WHERE IsActive = 1 ORDER BY TestId DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int testId = reader.GetInt32(0);
                            string testName = reader.GetString(1);
                            string desc = reader.IsDBNull(2) ? "" : reader.GetString(2);

                            // Létrehozunk egy szép kártyát minden tesztnek
                            Button btnQuiz = new Button
                            {
                                Text = $"{testName}\n(ID: {testId}) - {desc}",
                                Width = flpQuizzes.Width - 25,
                                Height = 80,
                                BackColor = Color.FromArgb(235, 245, 255),
                                FlatStyle = FlatStyle.Flat,
                                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                Cursor = Cursors.Hand,
                                Tag = testId // Eltároljuk az ID-t a gombban, hogy később tudjuk, melyiket akarjuk megnyitni
                            };

                            btnQuiz.FlatAppearance.BorderSize = 0;

                            // Ha rákattintanak egy tesztre
                            btnQuiz.Click += (s, e) => {
                                int id = (int)((Button)s).Tag;
                                Quiz loadedQuiz = FetchFullQuiz(id); // Ez a metódus olvassa ki az SQL-ből

                                if (loadedQuiz != null)
                                {
                                    Form1 main = (Form1)this.ParentForm;
                                    main.OpenExistingQuiz(loadedQuiz);
                                }
                            };

                            flpQuizzes.Controls.Add(btnQuiz);
                        }
                    }
                }

                if (flpQuizzes.Controls.Count == 0)
                {
                    Label lblEmpty = new Label { Text = "Még nincsenek elmentett kérdőíveid.", AutoSize = true, Font = new Font("Segoe UI", 12) };
                    flpQuizzes.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a kérdőívek betöltésekor: " + ex.Message, "Adatbázis hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
