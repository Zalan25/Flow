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
        public UC_QuestionCard ActiveCard { get; set; }
        // --- ADATTAGOK / VÁLTOZÓK ---
        private int qCounter = 1;
        private UC_QuestionCard selectedIndex = null; // Az éppen szerkesztett kártya

        // --- KONSTRUKTOR ---
        public UC_TypeSelector()
        {
            InitializeComponent();
        }

        

        

        // --- KÉRDÉSEK KEZELÉSE (Létrehozás, Kiválasztás, Betöltés) ---

        private void CreateNewQuestionCard(string type, string defaultText)
        {
            UC_QuestionCard newCard = new UC_QuestionCard();

            // Itt javítottuk: QuestionType helyett Data.UI_TypeKey
            newCard.Data.UI_TypeKey = type;
            newCard.Data.QuestionText = defaultText;

            flpQuestionList.Controls.Add(newCard);
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
            ActiveCard = card; // Megjegyezzük, hogy őt szerkesztjük
            pnlright.Controls.Clear(); // Kiürítjük a jobb oldalt

            // Megnézzük, milyen típusú a kártya, és betöltjük a megfelelő panelt az ADATOKKAL EGYÜTT
            if (card.Data.UI_TypeKey == "tf")
            {
                var tf = new UC_TF_settings { Dock = DockStyle.Fill };
                tf.QuestionText = card.Data.QuestionText;
                tf.Points = card.Data.Points.ToString();
                pnlright.Controls.Add(tf);
            }
            else if (card.Data.UI_TypeKey == "Short")
            {
                var shortAns = new UC_Shortans_settings { Dock = DockStyle.Fill };
                shortAns.QuestionText = card.Data.QuestionText;
                shortAns.Points = card.Data.Points.ToString();
                pnlright.Controls.Add(shortAns);
            }
            else if (card.Data.UI_TypeKey == "Multi")
            {
                var multi = new UC_Multi_settings { Dock = DockStyle.Fill };
                multi.QuestionText = card.Data.QuestionText;
                multi.Points = card.Data.Points.ToString();
                pnlright.Controls.Add(multi);
            }
        }

        private void LoadRightSettings(string type)
        {
            pnlright.Controls.Clear();

            if (type == "Short")
            {
                UC_Shortans_settings settings = new UC_Shortans_settings { Dock = DockStyle.Fill };

                // 1. Betöltjük a validációs adatokat
                settings.MaxCharacters = selectedIndex.Data.MaxCharacters.ToString();
                // Itt a cmbTextType-ot is beállíthatod, ha elmentetted

                // 2. Visszatöltjük az ÖSSZES alternatív választ (nem csak az elsőt!)
                var flp = settings.Controls.Find("flpAnswers", true).FirstOrDefault() as FlowLayoutPanel;
                if (flp != null)
                {
                    flp.Controls.Clear();
                    foreach (var ans in selectedIndex.Data.Answers)
                    {
                        var item = new UC_ShortAnswer_Item
                        {
                            Width = flp.Width - 20,
                            AnswerText = ans.AnswerText
                        };
                        flp.Controls.Add(item);
                    }
                }

                pnlright.Controls.Add(settings);
            }
            else if (type == "tf")
            {
                UC_TF_settings settings = new UC_TF_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;

                // Visszaállítjuk a RadioButton állapotát (melyik volt a helyes?)
                var correctAnswer = selectedIndex.Data.Answers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer != null)
                {
                    // Ha az "Igaz" volt a helyes, bepipáljuk az rbTrue-t
                    var rbTrue = settings.Controls.Find("rbTrue", true).FirstOrDefault() as RadioButton;
                    if (rbTrue != null) rbTrue.Checked = (correctAnswer.AnswerText == "Igaz");
                }

                pnlright.Controls.Add(settings);
            }
            else if (type == "Multi")
            {
                UC_Multi_settings settings = new UC_Multi_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;

                // Visszatöltjük a korábban felvett feleletválasztós opciókat
                var flp = settings.Controls.Find("flpOptions", true).FirstOrDefault() as FlowLayoutPanel;
                if (flp != null)
                {
                    flp.Controls.Clear();
                    foreach (var ans in selectedIndex.Data.Answers)
                    {
                        var item = new UC_MultiOption_Item
                        {
                            Width = flp.Width - 20,
                            OptionText = ans.AnswerText,
                            IsCorrect = ans.IsCorrect
                        };
                        flp.Controls.Add(item);
                    }
                }

                pnlright.Controls.Add(settings);
            }
        }

        // --- ESEMÉNYKEZELŐK (Gombok) ---

        private void btnShort_Click(object sender, EventArgs e)
        {
            CreateNewQuestionCard("Short", "Új rövid válaszos kérdés...");
        }

        private void btnTF_Click(object sender, EventArgs e)
        {
            CreateNewQuestionCard("tf", "Új igaz/hamis kérdés...");
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            CreateNewQuestionCard("Multi", "Új feleletválasztós kérdés...");
        }

        

        // --- SEGÉDFÜGGVÉNYEK (Layout, Újraszámozás) ---

        private void ResizeCards()
        {
            flpQuestionList.SuspendLayout();
            foreach (Control ctrl in flpQuestionList.Controls)
            {
                if (ctrl is UC_QuestionCard card)
                {
                    card.Width = flpQuestionList.ClientSize.Width - 20;
                }
            }
            flpQuestionList.ResumeLayout();
        }

        private void RenumberQuestions()
        {
            qCounter = 1;
            foreach (Control ctrl in flpQuestionList.Controls)
            {
                if (ctrl is UC_QuestionCard card)
                {
                    // Itt javítottuk: card.QuestionText helyett card.Data.QuestionText
                    card.UpdateDisplay(qCounter++, card.Data.QuestionText);
                }
            }
        }
        //meglévő kérdőívek betöltése a modellből
        public void LoadQuestionsFromModel(List<Question> questions)
        {
            flpQuestionList.Controls.Clear();
            qCounter = 1; // Újraindítjuk a sorszámozást

            foreach (var qData in questions)
            {
                UC_QuestionCard card = new UC_QuestionCard();
                card.Data = qData; // Átadjuk az adatokat a kártyának

                // Kiszámoljuk az összefoglaló szöveget a kártyára
                string summary = $"[{qData.UI_TypeKey.ToUpper()}] ({qData.Points} pont)\n{qData.QuestionText}";

                flpQuestionList.Controls.Add(card);
                card.Width = flpQuestionList.ClientSize.Width - 25;
                card.UpdateDisplay(qCounter++, summary);

                // Bekötjük az eseményeket, hogy újra lehessen kattintani a szerkesztéshez
                card.Click += (s, ev) => SelectCard(card);
                foreach (Control child in card.Controls) child.Click += (s, ev) => SelectCard(card);
            }
        }

        
        public void ShowQuestionList()
        {
            pnlCenter.Controls.Clear();
            pnlCenter.Controls.Add(flpQuestionList);
            flpQuestionList.Dock = DockStyle.Fill;
        }

        // Ezt a nevet fogja hívni a középső választó
        public void HandleNewQuestionSelection(string type, string text)
        {
            CreateNewQuestionCard(type, text);
        }

        // --- DESIGNER METÓDUSOK ---
        private void flpQuestionList_Resize(object sender, EventArgs e) { ResizeCards(); }
        private void UC_TypeSelector_Load(object sender, EventArgs e) { CenterControls(); }
        private void CenterControls() { btnAdd.Left = (pnlCenter.Width - btnAdd.Width) / 2; }
        private void UC_TypeSelector_Load_1(object sender, EventArgs e) { }
        private void pnlright_Paint(object sender, PaintEventArgs e) { }
        private void flpQuestionList_Paint(object sender, PaintEventArgs e) { }
        private void btnnewquestion_Click(object sender, EventArgs e) { }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            // 1. Ellenőrizzük, van-e kijelölt kártya
            if (selectedIndex == null || pnlright.Controls.Count == 0) return;

            var currentUC = pnlright.Controls[0];

            // Alaphelyzetbe állítjuk a kártya válaszait
            selectedIndex.Data.Answers.Clear();
            string cardSummary = "";

            // 2. Adatok begyűjtése a panelekről
            if (currentUC is UC_Shortans_settings s)
            {
                // ELMENTJÜK A KÉRDÉST ÉS A PONTOT
                selectedIndex.Data.QuestionText = s.QuestionText;
                if (int.TryParse(s.Points, out int pts)) selectedIndex.Data.Points = pts;

                var answers = s.GetAnswers();
                selectedIndex.Data.Answers.AddRange(answers);

                cardSummary = $"[RÖVID VÁLASZ] ({s.Points} pont)\n" +
                              $"Q: {s.QuestionText}\n" +
                              $"Válaszok: {string.Join(", ", answers.Select(a => a.AnswerText))}";
            }
            else if (currentUC is UC_TF_settings tf)
            {
                // ELMENTJÜK A KÉRDÉST
                selectedIndex.Data.QuestionText = tf.QuestionText;

                selectedIndex.Data.Answers.Add(new Answer { AnswerText = "Igaz", IsCorrect = tf.IsTrueSelected, AnswerOrder = 1 });
                selectedIndex.Data.Answers.Add(new Answer { AnswerText = "Hamis", IsCorrect = !tf.IsTrueSelected, AnswerOrder = 2 });

                cardSummary = $"[IGAZ-HAMIS]\n" +
                              $"Q: {tf.QuestionText}\n" +
                              $"Helyes: {(tf.IsTrueSelected ? "IGAZ" : "HAMIS")}";
            }
            else if (currentUC is UC_Multi_settings multi)
            {
                // ELMENTJÜK A KÉRDÉST
                selectedIndex.Data.QuestionText = multi.QuestionText;
                var answers = multi.GetAnswers();
                selectedIndex.Data.Answers.AddRange(answers);

                string opts = string.Join(", ", answers.Select(a => (a.IsCorrect ? "✔" : "☐") + a.AnswerText));
                cardSummary = $"[MULTI]\nQ: {multi.QuestionText}\n{opts}";
            }

            // 3. FRISSÍTJÜK A KÁRTYÁT
            int pos = flpQuestionList.Controls.GetChildIndex(selectedIndex) + 1;
            selectedIndex.UpdateDisplay(pos, cardSummary);


        }

        // Ez a metódus fogja átmenteni az adatokat a panelről a kártyára
        public void SaveCurrentCard()
        {
            if (ActiveCard == null) return;

            var currentPanel = pnlright.Controls.OfType<UserControl>().FirstOrDefault();

            if (currentPanel is UC_TF_settings tf)
            {
                ActiveCard.Data.QuestionText = tf.QuestionText;
                ActiveCard.Data.Points = int.TryParse(tf.Points, out int p) ? p : 1;
                // Ha van Answer lista, azt is itt adhatod át: ActiveCard.Data.Answers = tf.GetAnswers();
            }
            else if (currentPanel is UC_Shortans_settings shortAns)
            {
                ActiveCard.Data.QuestionText = shortAns.QuestionText;
                ActiveCard.Data.Points = int.TryParse(shortAns.Points, out int p) ? p : 1;
            }
            else if (currentPanel is UC_Multi_settings multi)
            {
                ActiveCard.Data.QuestionText = multi.QuestionText;
                ActiveCard.Data.Points = int.TryParse(multi.Points, out int p) ? p : 1;
            }

            // Frissítjük a kártya szövegét a listában, hogy azonnal látszódjon a változás
            string summary = $"[{ActiveCard.Data.UI_TypeKey.ToUpper()}] ({ActiveCard.Data.Points} pont)\n{ActiveCard.Data.QuestionText}";

            int order = flpQuestionList.Controls.IndexOf(ActiveCard) + 1;
            ActiveCard.UpdateDisplay(order, summary);
        }
        // --- PUBLIKUS MŰVELETEK (Törlés) ---
        // Ez végzi a tényleges törlést
        public void DeleteCurrentCard()
        {
            if (ActiveCard != null)
            {
                // Kártya törlése a listából
                flpQuestionList.Controls.Remove(ActiveCard);
                ActiveCard.Dispose();
                ActiveCard = null;

                // Jobb oldali beállító panel kiürítése (hiszen töröltük a kérdést)
                pnlright.Controls.Clear();

                // Sorszámok újraosztása
                RefreshCardNumbering();
            }
        }

        // Ez a metódus rakja rendbe a sorszámokat törlés vagy új hozzáadása után
        public void RefreshCardNumbering()
        {
            int order = 1;
            foreach (UC_QuestionCard card in flpQuestionList.Controls.OfType<UC_QuestionCard>())
            {
                string summary = $"[{card.Data.UI_TypeKey.ToUpper()}] ({card.Data.Points} pont)\n{card.Data.QuestionText}";
                card.UpdateDisplay(order++, summary);
            }
        }
    }
}