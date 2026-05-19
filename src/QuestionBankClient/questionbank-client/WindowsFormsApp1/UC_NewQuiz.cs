using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
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
            ApplyDesign();
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
                //main.pnlbetamain.Controls.Clear();
                //UC_TypeSelector selector = new UC_TypeSelector { Dock = DockStyle.Fill };
                //main.pnlbetamain.Controls.Add(selector);
                main.OpenTypeSelector();
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

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color LightBackground = Color.FromArgb(244, 247, 251);
        private readonly Color PanelBackground = Color.FromArgb(235, 239, 246);
        private readonly Color BorderGray = Color.FromArgb(215, 220, 230);

        //Desgin
        private void ApplyDesign()
        {
            this.BackColor = LightBackground;

            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.BackColor = LightBackground;
            pnlMainContent.AutoScroll = false;
            pnlMainContent.Padding = new Padding(0);

            pnlStartCard.Anchor = AnchorStyles.None;
            pnlStartCard.BackColor = PanelBackground;
            pnlStartCard.Size = new Size(760, 430);
            pnlStartCard.Location = new Point(
                (pnlMainContent.Width - pnlStartCard.Width) / 2,
                125
            );

            lblTestName.Text = "Kérdőív címe";
            lblTestName.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblTestName.ForeColor = Color.Black;
            lblTestName.AutoSize = true;
            lblTestName.Location = new Point(58, 55);

            txtTestName.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            txtTestName.BorderStyle = BorderStyle.FixedSingle;
            txtTestName.Multiline = false;
            txtTestName.Size = new Size(640, 34);
            txtTestName.Location = new Point(58, 88);
            txtTestName.Text = string.IsNullOrWhiteSpace(txtTestName.Text) ? "" : txtTestName.Text;

            lblDescription.Text = "Leírása";
            lblDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblDescription.ForeColor = Color.Black;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(58, 145);

            txtDescription.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Multiline = false;
            txtDescription.Size = new Size(640, 34);
            txtDescription.Location = new Point(58, 178);

            btnAddQuestions.Text = "+ Kérdés hozzáadása";
            btnAddQuestions.Size = new Size(285, 50);
            btnAddQuestions.Location = new Point(
                (pnlStartCard.Width - btnAddQuestions.Width) / 2,
                300
            );
            btnAddQuestions.BackColor = DarkBlue;
            btnAddQuestions.ForeColor = Color.White;
            btnAddQuestions.FlatStyle = FlatStyle.Flat;
            btnAddQuestions.FlatAppearance.BorderSize = 0;
            btnAddQuestions.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnAddQuestions.Cursor = Cursors.Hand;

            pnlStartCard.Paint += DrawPanelBorder;

            this.Resize += UC_NewQuiz_Resize;
        }

        //Keret
        private void DrawPanelBorder(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel == null)
            {
                return;
            }

            using (Pen pen = new Pen(BorderGray, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        //Resize
        private void UC_NewQuiz_Resize(object sender, EventArgs e)
        {
            if (pnlMainContent == null || pnlStartCard == null)
            {
                return;
            }

            pnlStartCard.Location = new Point(
                (pnlMainContent.Width - pnlStartCard.Width) / 2,
                125
            );
        }
    }
}