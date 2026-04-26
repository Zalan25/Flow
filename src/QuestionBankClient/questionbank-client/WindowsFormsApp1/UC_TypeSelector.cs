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
        // --- ADATTAGOK / VÁLTOZÓK ---
        private int qCounter = 1;
        private UC_QuestionCard selectedIndex = null; // Az éppen szerkesztett kártya

        // --- KONSTRUKTOR ---
        public UC_TypeSelector()
        {
            InitializeComponent();
        }

        // --- PUBLIKUS MŰVELETEK (Törlés) ---

        public void DeleteCurrentCard()
        {
            if (selectedIndex != null)
            {
                var result = MessageBox.Show("Biztosan törölni szeretnéd ezt a kérdést?", "Törlés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    flpQuestionList.Controls.Remove(selectedIndex);
                    selectedIndex.Dispose();
                    selectedIndex = null;
                    pnlright.Controls.Clear();
                    RenumberQuestions();
                }
            }
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
            selectedIndex = card;

            foreach (Control c in flpQuestionList.Controls)
            {
                if (c is UC_QuestionCard) c.BackColor = Color.White;
            }
            card.BackColor = Color.FromArgb(235, 245, 255);

            // Itt javítottuk: card.QuestionType helyett card.Data.UI_TypeKey
            LoadRightSettings(card.Data.UI_TypeKey);
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

        // Ezt add hozzá az UC_TypeSelector.cs-hez
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
    }
}