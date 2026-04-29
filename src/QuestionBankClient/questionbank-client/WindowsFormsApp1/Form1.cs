using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QuestionBankClient
{
    public partial class Form1 : Form
    {
        public Quiz ActiveQuiz = new Quiz();

        public Form1()
        {
            InitializeComponent();

            // Ez garantálja, hogy induláskor azonnal a helyére kerüljön minden
            this.Load += (s, e) => Form1_Resize(null, null);
        }

        private void Form1_Load(object sender, EventArgs e) { }

        // --- A LÉNYEG: Erőszakos Elrendezés (Brute Force Layout) ---
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (pnlHeader == null || pnlmain == null || pnlchoose == null || pnlbetamain == null) return;

            // 1. Fejléc fixen felül
            pnlHeader.Top = 0;
            pnlHeader.Left = 0;
            pnlHeader.Width = this.ClientSize.Width;

            // 2. Fő panel a fejléc alatt
            pnlmain.Top = pnlHeader.Height;
            pnlmain.Left = 0;
            pnlmain.Width = this.ClientSize.Width;
            pnlmain.Height = this.ClientSize.Height - pnlHeader.Height;

            // 3. Bal oldali menü fixen a bal szélen
            pnlchoose.Top = 0;
            pnlchoose.Left = 0;
            pnlchoose.Height = pnlmain.Height;

            // 4. A szürke tartalom kitölti a maradék helyet jobbra
            pnlbetamain.Top = 0;
            pnlbetamain.Left = pnlchoose.Width;
            pnlbetamain.Width = pnlmain.Width - pnlchoose.Width;
            pnlbetamain.Height = pnlmain.Height;

            // 5. A GOMBOK fixálása a jobb felső sarokba
            int marginRight = 20;
            if (btnFinalSave != null)
                btnFinalSave.Left = pnlHeader.Width - btnFinalSave.Width - marginRight;

            

            // 6. ÚJ: Bal oldali gombok egymás alá rendezése, hogy kicsi ablakban se tűnjenek el!
            if (lbledit != null && btnexisting != null && btnnew != null)
            {
                // A felirat alja + 50 pixel üres hely
                btnexisting.Top = lbledit.Bottom + 50;
                // A Kérdőíveim gomb alja + 30 pixel üres hely
                btnnew.Top = btnexisting.Bottom + 30;
            }
        }

        private void pnlmain_Paint(object sender, PaintEventArgs e) { }

        // --- NAVIGÁCIÓ ---

        private void btnnew_Click(object sender, EventArgs e)
        {
            ActiveQuiz = new Quiz();

            pnlbetamain.Controls.Clear();
            UC_NewQuiz uc = new UC_NewQuiz { Dock = DockStyle.Fill };
            pnlbetamain.Controls.Add(uc);

            lblMainTitle.Text = "Új kérdőív létrehozása";
            btnBack.Visible = true;

            // EZ A SOR KAPCSOLJA VISSZA A MENTÉS GOMBOT!
            if (btnFinalSave != null) btnFinalSave.Visible = true;
        }

        private void btnexisting_Click(object sender, EventArgs e)
        {
            // Töröljük a középső területet
            pnlbetamain.Controls.Clear();

            // Betöltjük a kérdőívek listáját
            UC_QuizList quizListPanel = new UC_QuizList { Dock = DockStyle.Fill };
            pnlbetamain.Controls.Add(quizListPanel);

            lblMainTitle.Text = "Mentett Kérdőíveim";
            btnBack.Visible = true;
            if (btnFinalSave != null) btnFinalSave.Visible = false;
        }

        public void btnBack_Click_1(object sender, EventArgs e)
        {
            // Mindent kitakarítunk
            pnlmain.Controls.Clear();
            pnlbetamain.Controls.Clear();

            // Visszatesszük az eredeti két panelt
            pnlmain.Controls.Add(pnlchoose);
            pnlmain.Controls.Add(pnlbetamain);

            // Alaphelyzetbe állítjuk a feliratokat és gombokat
            lblMainTitle.Text = "Saját kérdőív összeállítása";
            btnBack.Visible = false;
            if (btnFinalSave != null) btnFinalSave.Visible = false;

            // ELENGEDHETETLEN: Újra lefuttatjuk az elrendezést, hogy ne legyen üres a kép
            Form1_Resize(null, null);
            this.Refresh(); // Frissítjük a teljes ablakot, hogy ne "haljon meg"
        }

        // --- VÉGLEGES MENTÉS (SQL) ---

        private void btnFinalSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. KÉRDŐÍV FEJLÉC MENTÉSE (Ezt mindenképp megcsinálja, ha vannak kérdések, ha nincsenek!)
                            int testId = SaveQuizHeader(conn, transaction);

                            // 2. RÉGI KÉRDÉSEK TÖRLÉSE (Csak frissítésnél takarítunk)
                            if (ActiveQuiz.TestId > 0)
                            {
                                string delSql = "DELETE FROM dbo.lm_test_questions WHERE TestId = @tid";
                                using (SqlCommand cmdDel = new SqlCommand(delSql, conn, transaction))
                                {
                                    cmdDel.Parameters.AddWithValue("@tid", testId);
                                    cmdDel.ExecuteNonQuery();
                                }
                            }

                            // 3. KÉRDÉSEK MEGKERESÉSE ÉS MENTÉSE
                            // HIBA JAVÍTVA: A "this.Controls.Find" az EGÉSZ ablakban keres, bármelyik panelen is van!
                            var flpArray = this.Controls.Find("flpQuestionList", true);

                            if (flpArray.Length > 0)
                            {
                                var flp = flpArray[0] as FlowLayoutPanel;
                                if (flp != null && flp.Controls.OfType<UC_QuestionCard>().Any())
                                {
                                    int order = 1;
                                    foreach (UC_QuestionCard card in flp.Controls.OfType<UC_QuestionCard>())
                                    {
                                        // Biztonsági ellenőrzés
                                        if (card.Data == null) card.Data = new Question();
                                        if (card.Data.Points == 0) card.Data.Points = 1;

                                        // Kérdés mentése és összekötése
                                        int questionId = SaveQuestion(card.Data, conn, transaction);
                                        LinkQuestionToTest(testId, questionId, order++, conn, transaction);

                                        // Válaszok mentése
                                        if (card.Data.Answers != null && card.Data.Answers.Count > 0)
                                        {
                                            SaveAnswers(questionId, card.Data.Answers, conn, transaction);
                                        }
                                    }
                                }
                            }

                            // 4. MINDEN SIKERES -> MENTÉS VÉGLEGESÍTÉSE
                            transaction.Commit();

                            // Frissítjük a memóriában lévő azonosítót, ha esetleg új volt
                            ActiveQuiz.TestId = testId;

                            MessageBox.Show("A kérdőív sikeresen elmentve!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Visszatérés a főoldalra
                            this.btnBack_Click_1(null, null);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Hiba a mentés során: " + ex.Message, "Adatbázis hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült csatlakozni az adatbázishoz:\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int SaveQuizHeader(SqlConnection conn, SqlTransaction trans)
        {
            // HA EZ EGY MÁR LÉTEZŐ KÉRDŐÍV (Van azonosítója) -> UPDATE
            if (ActiveQuiz.TestId > 0)
            {
                string sql = @"UPDATE dbo.lm_tests
                       SET TestName = @name, Characterization = @desc
                       WHERE TestId = @id";

                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(ActiveQuiz.Title) ? "Névtelen teszt" : ActiveQuiz.Title);
                    cmd.Parameters.AddWithValue("@desc", ActiveQuiz.Description ?? "");
                    cmd.Parameters.AddWithValue("@id", ActiveQuiz.TestId);

                    cmd.ExecuteNonQuery(); // Frissítjük a sort
                }
                return ActiveQuiz.TestId; // Visszaadjuk a már meglévő ID-t
            }
            // HA EZ EGY TELJESEN ÚJ KÉRDŐÍV -> INSERT
            else
            {
                string sql = @"INSERT INTO dbo.lm_tests (ModuleId, TestName, Characterization, LanguageId, TotalPoints, IsRandom, IsActive)
                       OUTPUT INSERTED.TestId
                       VALUES (1, @name, @desc, 1, 0, 0, 1)";

                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(ActiveQuiz.Title) ? "Névtelen teszt" : ActiveQuiz.Title);
                    cmd.Parameters.AddWithValue("@desc", ActiveQuiz.Description ?? "");

                    return (int)cmd.ExecuteScalar(); // Visszaadjuk a frissen generált ID-t
                }
            }
        }

        private int SaveQuestion(Question data, SqlConnection conn, SqlTransaction trans)
        {
            int qTypeId = 1;
            if (data.UI_TypeKey == "tf") qTypeId = 2;
            else if (data.UI_TypeKey == "Short") qTypeId = 3;

            string sql = @"INSERT INTO dbo.lm_questions (ModuleId, QuestionText, LanguageId, QuestionLevelId, QuestionTypeId, SkillTypeId, Points, IsActive)
                           OUTPUT INSERTED.QuestionId
                           VALUES (1, @text, 1, 1, @typeId, 1, @pts, 1)";
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@text", string.IsNullOrWhiteSpace(data.QuestionText) ? "Névtelen kérdés" : data.QuestionText);
                cmd.Parameters.AddWithValue("@typeId", qTypeId);
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
        // létező quiz megnyitása a szerkesztéshez
        public void OpenExistingQuiz(Quiz loadedQuiz)
        {
            this.ActiveQuiz = loadedQuiz; // Beállítjuk az adatbázisból jött tesztet aktívnak

            pnlmain.Controls.Clear();
            UC_TypeSelector typeSelector = new UC_TypeSelector();
            typeSelector.Dock = DockStyle.Fill;
            pnlmain.Controls.Add(typeSelector);

            // Meghívunk egy új metódust a TypeSelectorban, ami kirajzolja a kártyákat
            typeSelector.LoadQuestionsFromModel(loadedQuiz.Questions);

            lblMainTitle.Text = "Kérdőív szerkesztése: " + loadedQuiz.Title;
            btnBack.Visible = true;
            btnFinalSave.Visible = true;
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

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}