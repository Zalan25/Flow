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
            //this.Load += (s, e) => Form1_Resize(null, null);
            //Kinézet
            this.BackColor = LightBackground;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.MinimumSize = new Size(1200, 750);

            SetupBaseLayout();
            ShowQuizList();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        // --- A LÉNYEG: Erőszakos Elrendezés (Brute Force Layout) ---
        //private void Form1_Resize(object sender, EventArgs e)
        //{
        //    if (pnlHeader == null || pnlmain == null || pnlchoose == null || pnlbetamain == null) return;

        //    // 1. Fejléc fixen felül
        //    pnlHeader.Top = 0;
        //    pnlHeader.Left = 0;
        //    pnlHeader.Width = this.ClientSize.Width;

        //    // 2. Fő panel a fejléc alatt
        //    pnlmain.Top = pnlHeader.Height;
        //    pnlmain.Left = 0;
        //    pnlmain.Width = this.ClientSize.Width;
        //    pnlmain.Height = this.ClientSize.Height - pnlHeader.Height;

        //    // 3. Bal oldali menü fixen a bal szélen
        //    pnlchoose.Top = 0;
        //    pnlchoose.Left = 0;
        //    pnlchoose.Height = pnlmain.Height;

        //    // 4. A szürke tartalom kitölti a maradék helyet jobbra
        //    pnlbetamain.Top = 0;
        //    pnlbetamain.Left = pnlchoose.Width;
        //    pnlbetamain.Width = pnlmain.Width - pnlchoose.Width;
        //    pnlbetamain.Height = pnlmain.Height;

        //    // 5. A GOMBOK fixálása a jobb felső sarokba
        //    int marginRight = 20;
        //    if (btnFinalSave != null)
        //        btnFinalSave.Left = pnlHeader.Width - btnFinalSave.Width - marginRight;

            

        //    // 6. ÚJ: Bal oldali gombok egymás alá rendezése, hogy kicsi ablakban se tűnjenek el!
        //    if (lbledit != null && btnexisting != null && btnnew != null)
        //    {
        //        // A felirat alja + 50 pixel üres hely
        //        btnexisting.Top = lbledit.Bottom + 50;
        //        // A Kérdőíveim gomb alja + 30 pixel üres hely
        //        btnnew.Top = btnexisting.Bottom + 30;
        //    }
        //}

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
            //Form1_Resize(null, null);
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

            // --- ÚJ: NYELVI SZINTEK ELLENŐRZÉSE MENTÉS ELŐTT ---
            if (!ValidateQuizLevels(ActiveQuiz.Questions, out string missing))
            {
                DialogResult result = MessageBox.Show(
                    $"Figyelem! A kérdőívből hiányoznak a következő nyelvi szintek: {missing}.\n\nBiztosan el szeretnéd menteni így is?",
                    "Hiányzó nyelvi szintek",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Ha a felhasználó a "Nem" gombra kattint, megszakítjuk a mentést!
                if (result == DialogResult.No) return;
            }

            // 2. HTTP POST kérés küldése az API-nak (JSON formátumban)
            try
            {
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


        private bool ValidateQuizLevels(List<Question> questions, out string missingLevels)
        {
            // Elvárt szintek ID-jai (1=A1, 2=A2, 3=B1, 4=B2, 5=C1)
            var requiredLevels = new HashSet<int> { 1, 2, 3, 4, 5 };

            // Összegyűjtjük a kérdőívben aktuálisan szereplő szintek ID-jait
            var presentLevels = questions.Select(q => q.QuestionLevelId).ToHashSet();

            // Kiszűrjük, mik hiányoznak a mintából
            var missing = requiredLevels.Except(presentLevels).ToList();

            if (missing.Count > 0)
            {
                // Kikeresjük a DropdownData osztályból a szöveges neveket a kiíráshoz
                var allLevels = DropdownData.GetLevels();
                var missingNames = missing.Select(id => allLevels.FirstOrDefault(l => l.Key == id).Value ?? $"ID:{id}");

                missingLevels = string.Join(", ", missingNames);
                return false;
            }

            missingLevels = "";
            return true;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color LightBackground = Color.FromArgb(244, 247, 251);
        private readonly Color SidebarBackground = Color.White;
        private readonly Color ActiveMenuBackground = Color.FromArgb(230, 235, 248);
        private readonly Color BorderGray = Color.FromArgb(215, 220, 230);

        //Base setup
        private Panel pnlSidebar;
        private Panel pnlContent;

        private Button btnMenuQuizList;
        private Button btnMenuNewQuiz;
        private Label lblAppTitle;

        private void SetupBaseLayout()
        {
            this.Controls.Clear();

            pnlSidebar = new Panel();
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.BackColor = SidebarBackground;
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 300;
            pnlSidebar.Padding = new Padding(32, 36, 32, 32);

            pnlContent = new Panel();
            pnlContent.Name = "pnlContent";
            pnlContent.BackColor = LightBackground;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Padding = new Padding(60, 48, 42, 42);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);

            BuildSidebar();
        }

        //Baloldali Sidebar
        private void BuildSidebar()
        {
            lblAppTitle = new Label();
            lblAppTitle.Text = "Kérdőív\nszerkesztő";
            lblAppTitle.ForeColor = TextPurple;
            lblAppTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblAppTitle.AutoSize = false;
            lblAppTitle.Width = 230;
            lblAppTitle.Height = 120;
            lblAppTitle.Location = new Point(32, 36);

            btnMenuQuizList = CreateMenuButton("▭  Kérdőíveim");
            btnMenuQuizList.Location = new Point(32, 190);
            btnMenuQuizList.Click += (s, e) => ShowQuizList();

            btnMenuNewQuiz = CreateMenuButton("⊞  Új kérdőív");
            btnMenuNewQuiz.Location = new Point(32, 270);
            btnMenuNewQuiz.Click += (s, e) => ShowNewQuiz();

            pnlSidebar.Controls.Add(lblAppTitle);
            pnlSidebar.Controls.Add(btnMenuQuizList);
            pnlSidebar.Controls.Add(btnMenuNewQuiz);
        }

        private Button CreateMenuButton(string text)
        {
            var button = new Button();

            button.Text = text;
            button.Width = 230;
            button.Height = 58;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(18, 0, 0, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = SidebarBackground;
            button.ForeColor = TextPurple;
            button.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            return button;
        }

        //Képernyő váltás
        private void ClearContent()
        {
            pnlContent.Controls.Clear();
        }

        private void SetActiveMenu(Button activeButton)
        {
            btnMenuQuizList.BackColor = SidebarBackground;
            btnMenuNewQuiz.BackColor = SidebarBackground;

            activeButton.BackColor = ActiveMenuBackground;
        }

        private void ShowQuizList()
        {
            ClearContent();
            SetActiveMenu(btnMenuQuizList);
            // var quizList = new UC_QuizList(this);
            var quizList = new UC_QuizList();
            quizList.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(quizList);
        }

        public void ShowNewQuiz()
        {
            ClearContent();
            SetActiveMenu(btnMenuNewQuiz);
            //var newQuiz = new UC_NewQuiz(this);
            var newQuiz = new UC_NewQuiz();
            newQuiz.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(newQuiz);
        }
    }
}