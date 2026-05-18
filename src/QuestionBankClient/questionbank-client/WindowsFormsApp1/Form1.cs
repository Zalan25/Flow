using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        // Végleges mentés hálózati API hívással
        private async void btnFinalSave_Click(object sender, EventArgs e)
        {
            // 1. Kártyák összeszedése a felületről
            var flpArray = this.Controls.Find("flpQuestionList", true);
            if (flpArray.Length > 0)
            {
                var flp = flpArray[0] as FlowLayoutPanel;
                if (flp != null)
                {
                    ActiveQuiz.Questions.Clear();
                    foreach (UC_QuestionCard card in flp.Controls.OfType<UC_QuestionCard>())
                    {
                        if (card.Data == null) card.Data = new Question();
                        if (card.Data.Points == 0) card.Data.Points = 1;
                        ActiveQuiz.Questions.Add(card.Data);
                    }
                }
            }

            try
            {
                // 2. HTTP POST kérés küldése az API-nak (JSON formátumban)
                HttpResponseMessage response = await DatabaseService.Client.PostAsJsonAsync("api/quiz/save", ActiveQuiz);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("A kérdőív sikeresen elmentve!", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnBack_Click_1(null, null); // Visszatérés a főoldalra
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Szerver hiba a mentéskor:\n" + error, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hálózati hiba (Nem elérhető az API):\n" + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}