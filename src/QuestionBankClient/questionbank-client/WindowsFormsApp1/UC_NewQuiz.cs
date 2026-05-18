using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Json; // Új hálózati csomag

namespace QuestionBankClient
{
    public partial class UC_NewQuiz : UserControl
    {
        public UC_NewQuiz()
        {
            InitializeComponent();
        }

        // Átlépés a kérdések hozzáadásához
        private void btnAddQuestions_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Form1;
            if (main != null)
            {
                // Adatok mentése a memóriába
                main.ActiveQuiz.Title = txtTestName.Text;
                main.ActiveQuiz.Description = txtDescription.Text;

                // Váltás a TypeSelectorra
                main.pnlbetamain.Controls.Clear();
                UC_TypeSelector selector = new UC_TypeSelector { Dock = DockStyle.Fill };
                main.pnlbetamain.Controls.Add(selector);
            }
        }

        // Visszalépés a start kártyára
        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            pnlStartCard.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(pnlStartCard);
        }

        // Kérdőív mentése API-n keresztül
        private async void btnFinalSave_Click(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;
            var typeSelector = mainForm.pnlmain.Controls.OfType<UC_TypeSelector>().FirstOrDefault();

            if (string.IsNullOrWhiteSpace(txtTestName.Text))
            {
                MessageBox.Show("Hiba: Adj meg egy nevet a kérdőívnek!");
                return;
            }

            // Objektum frissítése a mezők alapján
            mainForm.ActiveQuiz.Title = txtTestName.Text;
            mainForm.ActiveQuiz.Description = txtDescription.Text;

            // Ha nyitva van a kártyalista, összeszedjük a kérdéseket
            if (typeSelector != null)
            {
                var flp = typeSelector.Controls.Find("flpQuestionList", true).FirstOrDefault() as FlowLayoutPanel;
                if (flp != null)
                {
                    mainForm.ActiveQuiz.Questions.Clear();
                    foreach (UC_QuestionCard card in flp.Controls.OfType<UC_QuestionCard>())
                    {
                        if (card.Data == null) card.Data = new Question();
                        mainForm.ActiveQuiz.Questions.Add(card.Data);
                    }
                }
            }

            try
            {
                // POST kérés az API mentés végpontjára
                HttpResponseMessage response = await DatabaseService.Client.PostAsJsonAsync("api/quiz/save", mainForm.ActiveQuiz);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("A kérdőív sikeresen mentve!");
                    mainForm.btnBack_Click_1(null, null); // Vissza a főoldalra
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Szerver hiba: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hálózati hiba: " + ex.Message);
            }
        }

        // Designer kompatibilitási üres metódusok
        private void pnlHeader_Paint(object sender, PaintEventArgs e) { }
        private void pnlMainContent_Paint(object sender, PaintEventArgs e) { }
        private void lblDescription_Click(object sender, EventArgs e) { }
        private void txtDescription_TextChanged(object sender, EventArgs e) { }
        private void lblTestName_Click(object sender, EventArgs e) { }
        private void lblMainTitle_Click(object sender, EventArgs e) { }
        private void pnlMainContent_Paint_1(object sender, PaintEventArgs e) { }
    }
}