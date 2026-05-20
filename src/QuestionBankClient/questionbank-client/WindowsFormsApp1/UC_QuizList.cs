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

                    
                    Panel pnlActions = new Panel
                    {
                        Dock = DockStyle.Right,
                        Width = 270, // Elegendő hely a két gombnak és a margóknak
                        BackColor = Color.FromArgb(235, 245, 255) // Ugyanaz a szín, mint a fő gombé, hogy egybeolvadjon
                    };

                    // --- TÖRÉS GOMB ÚJRATERVEZVE ---
                    Button btnDelete = new Button
                    {
                        Text = "Törlés",
                        Bounds = new Rectangle(160, 20, 90, 40), // (X, Y, Szélesség, Magasság) -> Y=20 miatt középre kerül
                        BackColor = Color.White,
                        ForeColor = Color.Crimson,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold), // Kisebb betűméret a modernségért
                        Cursor = Cursors.Hand,
                        Tag = quiz.TestId
                    };
                    btnDelete.FlatAppearance.BorderSize = 1; // Finom piros keret a Figma tervhez hasonlóan
                    btnDelete.FlatAppearance.BorderColor = Color.Crimson;
                    btnDelete.Click += async (s, e) => {
                        int id = (int)((Button)s).Tag;
                        await DeleteQuizFromDatabase(id);
                    };

                    // --- AKTIVÁLÓ GOMB ÚJRATERVEZVE ---
                    Button btnToggleActive = new Button
                    {
                        Text = quiz.IsActive ? "Deaktiválás" : "Aktiválás",
                        Bounds = new Rectangle(10, 20, 140, 40), // X=10 -> Így van 10px távolság a Törlés gombtól (10+140=150)
                        BackColor = quiz.IsActive ? Color.MediumSeaGreen : Color.White,
                        ForeColor = quiz.IsActive ? Color.White : Color.FromArgb(64, 64, 64),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Cursor = Cursors.Hand,
                        Tag = quiz.TestId
                    };
                    btnToggleActive.FlatAppearance.BorderSize = quiz.IsActive ? 0 : 1;
                    btnToggleActive.FlatAppearance.BorderColor = Color.Gray;
                    btnToggleActive.Click += async (s, e) => {
                        Button clickedButton = (Button)s;
                        int id = (int)clickedButton.Tag;
                        await ToggleQuizStatus(id, clickedButton);
                    };

                    // Hozzáadjuk a gombokat az akció panelhez
                    pnlActions.Controls.Add(btnToggleActive);
                    pnlActions.Controls.Add(btnDelete);

                    // --- FŐ GOMB (A kártya bal oldala) ---
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

                    // Hozzáadjuk a konténereket a sorhoz.
                    // Z-Order miatt ELŐSZÖR a jobb oldali panelt adjuk hozzá, utána a kitöltő (Fill) gombot!
                    pnlRow.Controls.Add(pnlActions);
                    pnlRow.Controls.Add(btnQuiz);

                    flpQuizzes.Controls.Add(pnlRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a kérdőívek betöltésekor: " + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- API hívás az állapotváltásra ---
        private async Task ToggleQuizStatus(int testId, Button btn)
        {
            btn.Enabled = false;
            try
            {
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
                        btn.FlatAppearance.BorderSize = 0; // Aktív állapotban nincs keret
                    }
                    else
                    {
                        btn.Text = "Aktiválás";
                        btn.BackColor = Color.White;
                        btn.ForeColor = Color.FromArgb(64, 64, 64);
                        btn.FlatAppearance.BorderSize = 1; // Inaktív állapotban finom szürke keret
                    }
                }
                else
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Nem sikerült átállítani az állapotot.\nOk: {errorDetails}", "Szerver Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hálózati hiba: " + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true;
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

        private void flpQuizzes_Resize(object sender, EventArgs e)
        {

        }

        
    }
}