using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuestionBankClient
{

    public partial class UC_TypeSelector : UserControl
    {
        private int qCounter = 1;
        private UC_QuestionCard selectedIndex = null; // Az éppen szerkesztett kártya

        public UC_TypeSelector()
        {
            InitializeComponent();
            // A Load eseményt a designer már bekötötte neked
        }

        // --- BAL OLDALI GOMBOK (Létrehozás) ---

        private void btnShort_Click(object sender, EventArgs e)
        {
            // 1. Létrehozunk egy új kártyát rövid válasz típussal
            CreateNewQuestionCard("Short", "Új rövid válaszos kérdés...");
        }

        // Ha van hosszú válasz gombod, annak ez lenne a kódja:
        

        // --- KÖZÉPSŐ LOGIKA ---

        private void CreateNewQuestionCard(string type, string defaultText)
        {
            UC_QuestionCard newCard = new UC_QuestionCard();
            newCard.QuestionType = type;

            // Először adjuk hozzá a listához
            flpQuestionList.Controls.Add(newCard);

            // UTÁNA állítsuk be a szélességet, hogy a panel aktuális méretéhez igazodjon
            // A -25 pixel kell, hogy a görgetősáv ne takarja el a szélét!
            newCard.Width = flpQuestionList.ClientSize.Width - 10;

            newCard.UpdateDisplay(qCounter++, defaultText);

            newCard.Click += (s, ev) => SelectCard(newCard);
            foreach (Control child in newCard.Controls)
            {
                child.Click += (s, ev) => SelectCard(newCard);
            }

            SelectCard(newCard);
        }

        private void SelectCard(UC_QuestionCard card)
        {
            selectedIndex = card;

            // Kijelölés színe (többi fehér, ez szürke)
            foreach (Control c in flpQuestionList.Controls)
            {
                if (c is UC_QuestionCard) c.BackColor = Color.White;
            }
            card.BackColor = Color.FromArgb(235, 245, 255); // Világoskék kijelölés

            // Betöltjük a jobb oldali panelt a kártya típusa alapján
            LoadRightSettings(card.QuestionType);
        }

        private void LoadRightSettings(string type)
        {
            pnlright.Controls.Clear();

            if (type == "Short")
            {
                UC_Shortans_settings settings = new UC_Shortans_settings { Dock = DockStyle.Fill };
                // Ha a kártyán már van szöveg, adjuk át a jobb oldalnak
                settings.CorrectAnswer = selectedIndex.QuestionText;
                pnlright.Controls.Add(settings);
            }
            // Itt jönnek majd a: else if (type == "Long") ...
        }

        // --- MENTÉS GOMB (Középen alul) ---

        // Ez a gomb frissíti a kártyát azzal, amit jobb oldalt beírtál
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (selectedIndex != null)
            {
                // Megkeressük az éppen aktív beállításokat a jobb oldalon
                var settings = pnlright.Controls.OfType<UC_Shortans_settings>().FirstOrDefault();

                if (settings != null)
                {
                    // Átmásoljuk az adatot a jobb oldalról a középső kártyára
                    selectedIndex.QuestionText = settings.CorrectAnswer;

                    // Frissítjük a kártya kinézetét (sorszám + szöveg)
                    int pos = flpQuestionList.Controls.GetChildIndex(selectedIndex) + 1;
                    selectedIndex.UpdateDisplay(pos, settings.CorrectAnswer);
                }
            }
        }
        private void ResizeCards()
        {
            // Megállítjuk a panel rajzolását, hogy ne villogjon, amíg átméretezünk
            flpQuestionList.SuspendLayout();

            foreach (Control ctrl in flpQuestionList.Controls)
            {
                if (ctrl is UC_QuestionCard card)
                {
                    // Beállítjuk a szélességet (hagyunk helyet a görgetősávnak is)
                    card.Width = flpQuestionList.ClientSize.Width - 20;
                }
            }

            flpQuestionList.ResumeLayout();
        }

        // --- DESIGNER ÜRES METÓDUSOK (Hogy ne legyen hiba) ---
        // Ezeket hagyd itt, ha a Designer.cs fájlban hivatkozás van rájuk!
        private void UC_TypeSelector_Load(object sender, EventArgs e) { CenterControls(); }
        private void CenterControls() { btnAdd.Left = (pnlCenter.Width - btnAdd.Width) / 2; }
        private void UC_TypeSelector_Load_1(object sender, EventArgs e) { }
        private void pnlright_Paint(object sender, PaintEventArgs e) { }
        private void flpQuestionList_Paint(object sender, PaintEventArgs e) { }

        private void flpQuestionList_Resize(object sender, EventArgs e)
        {
            ResizeCards();
        }

        private void btnnewquestion_Click(object sender, EventArgs e)
        {

        }
    }
}
