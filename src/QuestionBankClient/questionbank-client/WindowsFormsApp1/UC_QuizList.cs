using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json;

namespace QuestionBankClient
{
    public partial class UC_QuizList : UserControl
    {
        public UC_QuizList()
        {
            InitializeComponent();
            LoadQuizzesFromDatabase();
        }

        // Kérdőív részletes letöltése az API-ból
        private async Task<Quiz> FetchFullQuiz(int testId)
        {
            try
            {
                return await DatabaseService.Client.GetFromJsonAsync<Quiz>($"api/quiz/{testId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a letöltéskor: " + ex.Message);
                return null;
            }
        }

        // Kérdőívek listájának lekérése az API-ból
        private async void LoadQuizzesFromDatabase()
        {
            flpQuizzes.Controls.Clear();

            try
            {
                var quizzes = await DatabaseService.Client.GetFromJsonAsync<List<Quiz>>("api/quiz/list");

                if (quizzes == null || quizzes.Count == 0)
                {
                    Label lblEmpty = new Label { Text = "Még nincsenek elmentett kérdőíveid.", AutoSize = true, Font = new Font("Segoe UI", 12) };
                    flpQuizzes.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var quiz in quizzes)
                {
                    Panel pnlRow = new Panel
                    {
                        Width = flpQuizzes.Width - 25,
                        Height = 80,
                        Margin = new Padding(0, 0, 0, 10)
                    };

                    Button btnDelete = new Button
                    {
                        Text = "Törlés",
                        Width = 100,
                        Dock = DockStyle.Right,
                        BackColor = Color.Crimson,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Cursor = Cursors.Hand,
                        Tag = quiz.TestId
                    };
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.Click += async (s, e) => {
                        int id = (int)((Button)s).Tag;
                        await DeleteQuizFromDatabase(id);
                    };

                    Button btnQuiz = new Button
                    {
                        Text = $"{quiz.Title}\n(ID: {quiz.TestId}) - {quiz.Description}",
                        Dock = DockStyle.Fill,
                        BackColor = Color.FromArgb(235, 245, 255),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand,
                        Tag = quiz.TestId
                    };
                    btnQuiz.FlatAppearance.BorderSize = 0;
                    btnQuiz.Click += async (s, e) => {
                        int id = (int)((Button)s).Tag;
                        Quiz loadedQuiz = await FetchFullQuiz(id);

                        if (loadedQuiz != null)
                        {
                            Form1 main = (Form1)this.ParentForm;
                            main.OpenExistingQuiz(loadedQuiz);
                        }
                    };

                    pnlRow.Controls.Add(btnDelete);
                    pnlRow.Controls.Add(btnQuiz);
                    flpQuizzes.Controls.Add(pnlRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a kérdőívek betöltésekor: " + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kérdőív törlése API-n keresztül
        public async Task DeleteQuizFromDatabase(int testId)
        {
            var confirmResult = MessageBox.Show("Biztosan törölni szeretnéd ezt a kérdőívet?",
                                                "Kérdőív törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                HttpResponseMessage response = await DatabaseService.Client.DeleteAsync($"api/quiz/{testId}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("A kérdőív sikeresen törölve lett!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQuizzesFromDatabase();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Szerver hiba a törléskor:\n" + error, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült törölni a kérdőívet:\n" + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Üres események a Designer kompatibilitás miatt
        private void flpQuizzes_Paint(object sender, PaintEventArgs e) { }
        private void UC_QuizList_Load(object sender, EventArgs e) { }
    }
}