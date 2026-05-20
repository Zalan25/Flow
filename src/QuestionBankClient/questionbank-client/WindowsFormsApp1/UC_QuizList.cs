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

                    // --- ÚJ GOMB: Aktiválás / Deaktiválás ---
                    Button btnToggleActive = new Button
                    {
                        // Ha IsActive igaz, akkor "Deaktiválás" a gomb szövege, különben "Aktiválás"
                        Text = quiz.IsActive ? "Deaktiválás" : "Aktiválás",
                        Width = 140,
                        Dock = DockStyle.Right, // Ez is jobbra dokkol, a Törlés gomb MELLETT fog megjelenni
                        // Zöld ha aktív (deaktiválható), Szürke ha inaktív (aktiválható)
                        BackColor = quiz.IsActive ? Color.MediumSeaGreen : Color.LightGray,
                        ForeColor = quiz.IsActive ? Color.White : Color.Black,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Cursor = Cursors.Hand,
                        Tag = quiz.TestId // Elrakjuk ide is az ID-t
                    };
                    btnToggleActive.FlatAppearance.BorderSize = 0;
                    btnToggleActive.Click += async (s, e) => {
                        // A kattintás esemény meghívja a segédmetódust, átadva a gombot magát és a teszt ID-ját
                        Button clickedButton = (Button)s;
                        int id = (int)clickedButton.Tag;
                        await ToggleQuizStatus(id, clickedButton);
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

                    // Hozzáadjuk a gombokat a panelhez. 
                    // A dokkolás miatt a sorrend fontos: ami előbb van hozzáadva, az kerül a legszélére.
                    pnlRow.Controls.Add(btnDelete);       // Legjobboldalra
                    pnlRow.Controls.Add(btnToggleActive); // A törlés mellé balra
                    pnlRow.Controls.Add(btnQuiz);         // Kitölti a maradékot balra

                    flpQuizzes.Controls.Add(pnlRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a kérdőívek betöltésekor: " + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- ÚJ METÓDUS: API hívás az állapotváltásra ---
        private async Task ToggleQuizStatus(int testId, Button btn)
        {
            btn.Enabled = false; // Kikapcsoljuk a gombot, amíg a hálózat dolgozik (ne lehessen spammelni)
            try
            {
                // Meghívjuk a korábban az API-ban megírt végpontot
                var response = await DatabaseService.Client.PostAsync($"api/quiz/toggle-active/{testId}", null);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Frissítjük a gomb külsejét a szerverről visszakapott új állapot alapján
                    if (jsonResponse.Contains("\"isActive\":true") || jsonResponse.Contains("\"isActive\": true"))
                    {
                        btn.Text = "Deaktiválás";
                        btn.BackColor = Color.MediumSeaGreen;
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.Text = "Aktiválás";
                        btn.BackColor = Color.LightGray;
                        btn.ForeColor = Color.Black;
                    }
                }
                else
                {
                    MessageBox.Show("Nem sikerült átállítani az állapotot.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hálózati hiba: " + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true; // Hálózat után visszakapcsoljuk a gombot
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