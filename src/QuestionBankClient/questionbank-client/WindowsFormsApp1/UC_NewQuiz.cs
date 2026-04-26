using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuestionBankClient
{
    public partial class UC_NewQuiz : UserControl
    {
        public UC_NewQuiz()
        {
            InitializeComponent();
        }

        // --- NAVIGÁCIÓ (Az eredeti kódod, amit pótoltunk) ---

        private void btnAddQuestions_Click(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;

            // Panelek kezelése a váltáshoz
            mainForm.pnlchoose.Controls.Clear();
            mainForm.pnlchoose.SendToBack();

            mainForm.pnlbetamain.Controls.Clear();
            mainForm.pnlbetamain.SendToBack();

            mainForm.pnlmain.BringToFront();
            mainForm.pnlmain.Controls.Clear();

            // Típusválasztó betöltése
            UC_TypeSelector typeSelector = new UC_TypeSelector();
            typeSelector.Dock = DockStyle.Fill;
            mainForm.pnlmain.Controls.Add(typeSelector);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            pnlStartCard.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(pnlStartCard);
        }

        // --- ADATBÁZIS MENTÉS (A funkcionalitás) ---

        private void btnFinalSave_Click(object sender, EventArgs e)
        {
            // Megkeressük a kérdéseket tároló TypeSelectort
            Form1 mainForm = (Form1)this.ParentForm;
            var typeSelector = mainForm.pnlmain.Controls.OfType<UC_TypeSelector>().FirstOrDefault();

            if (typeSelector == null || string.IsNullOrWhiteSpace(txtTestName.Text))
            {
                MessageBox.Show("Hiba: Adj meg egy nevet és legalább egy kérdést!");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Teszt (lm_tests) mentése
                            int testId = SaveQuizHeader(conn, transaction);

                            // 2. Kérdések és válaszok mentése
                            var flp = typeSelector.Controls.Find("flpQuestionList", true).FirstOrDefault() as FlowLayoutPanel;
                            int order = 1;

                            foreach (UC_QuestionCard card in flp.Controls.OfType<UC_QuestionCard>())
                            {
                                int questionId = SaveQuestion(card.Data, conn, transaction);
                                LinkQuestionToTest(testId, questionId, order++, conn, transaction);
                                SaveAnswers(questionId, card.Data.Answers, conn, transaction);
                            }

                            transaction.Commit();
                            MessageBox.Show("A kérdőív sikeresen mentve!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }

        // --- SQL SEGÉDMETÓDUSOK ---

        private int SaveQuizHeader(SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"INSERT INTO dbo.lm_tests (ModuleId, TestName, Characterization, LanguageId, TotalPoints, IsRandom, IsActive) 
                           OUTPUT INSERTED.TestId
                           VALUES (1, @name, @desc, 1, 0, 0, 1)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@name", txtTestName.Text);
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                return (int)cmd.ExecuteScalar();
            }
        }

        private int SaveQuestion(Question data, SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"INSERT INTO dbo.lm_questions (ModuleId, QuestionText, LanguageId, QuestionLevelId, QuestionTypeId, SkillTypeId, Points, IsActive) 
                           OUTPUT INSERTED.QuestionId
                           VALUES (1, @text, 1, 1, 1, 1, @pts, 1)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@text", data.QuestionText);
                cmd.Parameters.AddWithValue("@pts", data.Points);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void LinkQuestionToTest(int testId, int questionId, int order, SqlConnection conn, SqlTransaction trans)
        {
            string sql = "INSERT INTO dbo.lm_test_questions (ModuleId, TestId, QuestionId, QuestionOrder) VALUES (1, @tid, @qid, @ord)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@tid", testId);
                cmd.Parameters.AddWithValue("@qid", questionId);
                cmd.Parameters.AddWithValue("@ord", order);
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveAnswers(int questionId, List<Answer> answers, SqlConnection conn, SqlTransaction trans)
        {
            foreach (var ans in answers)
            {
                string sql = "INSERT INTO dbo.lm_answers (ModuleId, QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (1, @qid, @txt, @isc, @ord)";
                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@qid", questionId);
                    cmd.Parameters.AddWithValue("@txt", ans.AnswerText);
                    cmd.Parameters.AddWithValue("@isc", ans.IsCorrect);
                    cmd.Parameters.AddWithValue("@ord", ans.AnswerOrder);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- ÜRES DESIGNER METÓDUSOK ---
        private void pnlHeader_Paint(object sender, PaintEventArgs e) { }
        private void pnlMainContent_Paint(object sender, PaintEventArgs e) { }
        private void lblDescription_Click(object sender, EventArgs e) { }
        private void txtDescription_TextChanged(object sender, EventArgs e) { }
        private void lblTestName_Click(object sender, EventArgs e) { }
        private void lblMainTitle_Click(object sender, EventArgs e) { }

        private void pnlMainContent_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}