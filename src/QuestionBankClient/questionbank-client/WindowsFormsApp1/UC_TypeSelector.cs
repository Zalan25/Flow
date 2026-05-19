using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
            cmbRandomLanguage.DataSource = new BindingSource(DropdownData.GetLanguages(), null);
            cmbRandomLanguage.DisplayMember = "Value";
            cmbRandomLanguage.ValueMember = "Key";

            ApplyDesign();
        }


        // --- KÉRDÉSEK KEZELÉSE (Létrehozás, Kiválasztás, Betöltés) ---

        private void CreateNewQuestionCard(string type, string defaultText)
        {
            UC_QuestionCard newCard = new UC_QuestionCard();

            // Biztonsági ellenőrzés: ha a Data null lenne, létrehozzuk
            if (newCard.Data == null)
            {
                newCard.Data = new Question();
            }

            // Típus kulcs és alapértékek beállítása
            newCard.Data.UI_TypeKey = type;
            newCard.Data.QuestionText = defaultText;
            newCard.Data.Points = 1;

            // --- JAVÍTVA: Az adatbázis (SQL) Típus ID-k beállítása a szöveges kulcs alapján ---
            if (type == "Single") newCard.Data.QuestionTypeId = 1;
            else if (type == "Multi") newCard.Data.QuestionTypeId = 2;
            else if (type == "tf") newCard.Data.QuestionTypeId = 3;
            else if (type == "Essay") newCard.Data.QuestionTypeId = 4;
            else if (type == "Short") newCard.Data.QuestionTypeId = 5;

            flpQuestionList.Controls.Add(newCard);
            newCard.Width = flpQuestionList.ClientSize.Width - 10;

            newCard.UpdateDisplay(qCounter++, defaultText);

            // JAVÍTVA: Csak EGYSZER kötjük be a kattintást, a központi rekurzív metódussal!
            AssignClickToAll(newCard, newCard);

            // JAVÍTVA: Csak EGYSZER hívjuk meg a kijelölést!
            SelectCard(newCard);
        }

        private void SelectCard(UC_QuestionCard card)
        {
            if (card == null || card.Data == null) return;

            // Beállítjuk aktívnak a kártyát
            this.ActiveCard = card;
            this.selectedIndex = card;

            // Vizuális visszajelzés (Színek frissítése)
            //foreach (UC_QuestionCard c in flpQuestionList.Controls.OfType<UC_QuestionCard>())
            //{
            //    c.BackColor = System.Drawing.SystemColors.Control; // Eredeti szín
            //}
            //card.BackColor = System.Drawing.Color.LightBlue; // Kijelölt szín
            foreach (UC_QuestionCard c in flpQuestionList.Controls.OfType<UC_QuestionCard>())
            {
                c.SetSelected(false);
            }

            card.SetSelected(true);

            // Hibakereső üzenet (Ha betölt, de mégsem nyílik le a panel, ez megmondja miért)
            if (string.IsNullOrEmpty(card.Data.UI_TypeKey))
            {
                MessageBox.Show("Hiba: Ennek a kérdésnek nincs típusa beállítva!");
                return;
            }

            // Jobb oldali panel betöltése
            LoadRightSettings(card.Data.UI_TypeKey);
        }

        private void AssignClickToAll(Control parent, UC_QuestionCard card)
        {
            // Rákötjük a kattintást az aktuális elemre
            parent.Click += (s, e) => {
                SelectCard(card);
            };

            // Végigmegyünk az összes gyerek-elemen (Label, Panel stb.), és azokra is rákötjük
            foreach (Control child in parent.Controls)
            {
                AssignClickToAll(child, card);
            }
        }

        private void LoadRightSettings(string type)
        {
            pnlright.Controls.Clear();

            // --- ÚJ FEJLÉC (CÍM) DINAMIKUS LÉTREHOZÁSA ---
            Label lblHeader = new Label();
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(40, 50, 80);
            lblHeader.Dock = DockStyle.Top; // Odaragasztjuk a panel tetejére
            lblHeader.Height = 60;
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Beállítjuk a szöveget a típus alapján
            if (type == "Single") lblHeader.Text = "Egy helyes válaszos kérdés";
            else if (type == "Multi") lblHeader.Text = "Több helyes válaszos kérdés";
            else if (type == "tf") lblHeader.Text = "Igaz / Hamis kérdés";
            else if (type == "Essay") lblHeader.Text = "Esszé / Kifejtős kérdés";
            else if (type == "Short") lblHeader.Text = "Rövid válasz / Fordítás";
            else lblHeader.Text = "Kérdés beállításai";

            // Először a fejlécet adjuk a panelhez
            pnlright.Controls.Add(lblHeader);

            // --- PANELEK BETÖLTÉSE ---
            if (type == "Single")
            {
                var settings = new UC_Single_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;
                settings.Points = selectedIndex.Data.Points.ToString();
                settings.SelectedLanguageId = selectedIndex.Data.LanguageId;
                settings.SelectedLevelId = selectedIndex.Data.QuestionLevelId;
                settings.SelectedSkillId = selectedIndex.Data.SkillTypeId;
                settings.SetAnswers(selectedIndex.Data.Answers);

                pnlright.Controls.Add(settings);
                settings.BringToFront(); // Ez garantálja, hogy a fejléc alatt töltse ki a helyet
            }
            else if (type == "Multi")
            {
                var settings = new UC_Multi_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;
                settings.Points = selectedIndex.Data.Points.ToString();
                settings.SelectedLanguageId = selectedIndex.Data.LanguageId;
                settings.SelectedLevelId = selectedIndex.Data.QuestionLevelId;
                settings.SelectedSkillId = selectedIndex.Data.SkillTypeId;
                settings.SetAnswers(selectedIndex.Data.Answers);

                pnlright.Controls.Add(settings);
                settings.BringToFront();
            }
            else if (type == "tf")
            {
                var settings = new UC_TF_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;
                settings.Points = selectedIndex.Data.Points.ToString();
                settings.SelectedLanguageId = selectedIndex.Data.LanguageId;
                settings.SelectedLevelId = selectedIndex.Data.QuestionLevelId;
                settings.SelectedSkillId = selectedIndex.Data.SkillTypeId;
                settings.SetAnswers(selectedIndex.Data.Answers);

                pnlright.Controls.Add(settings);
                settings.BringToFront();
            }
            else if (type == "Essay")
            {
                var settings = new UC_Essay_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;
                settings.Points = selectedIndex.Data.Points.ToString();
                settings.SelectedLanguageId = selectedIndex.Data.LanguageId;
                settings.SelectedLevelId = selectedIndex.Data.QuestionLevelId;
                settings.SelectedSkillId = selectedIndex.Data.SkillTypeId;
                settings.SetAnswers(selectedIndex.Data.Answers);

                if (selectedIndex.Data.Answers != null && selectedIndex.Data.Answers.Count > 0)
                {
                    settings.SampleAnswer = selectedIndex.Data.Answers[0].AnswerText;
                }

                pnlright.Controls.Add(settings);
                settings.BringToFront();
            }
            else if (type == "Short")
            {
                var settings = new UC_Shortans_settings { Dock = DockStyle.Fill };
                settings.QuestionText = selectedIndex.Data.QuestionText;
                settings.Points = selectedIndex.Data.Points.ToString();
                settings.SelectedLanguageId = selectedIndex.Data.LanguageId;
                settings.SelectedLevelId = selectedIndex.Data.QuestionLevelId;
                settings.SelectedSkillId = selectedIndex.Data.SkillTypeId;
                settings.SetAnswers(selectedIndex.Data.Answers);

                pnlright.Controls.Add(settings);
                settings.BringToFront();
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

        private void button2_Click(object sender, EventArgs e)
        {
            CreateNewQuestionCard("Essay", "Írd ide az esszé kérdést...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewQuestionCard("Single", "Írd ide a kérdést (Egy helyes válasz)...");
        }


        // --- SEGÉDFÜGGVÉNYEK (Layout, Újraszámozás) ---

        private void ResizeCards()
        {
            //flpQuestionList.SuspendLayout();
            //foreach (Control ctrl in flpQuestionList.Controls)
            //{
            //    if (ctrl is UC_QuestionCard card)
            //    {
            //        card.Width = flpQuestionList.ClientSize.Width - 20;
            //    }
            //}
            //flpQuestionList.ResumeLayout();
            if (flpQuestionList == null)
            {
                return;
            }

            flpQuestionList.SuspendLayout();

            foreach (Control ctrl in flpQuestionList.Controls)
            {
                if (ctrl is UC_QuestionCard card)
                {
                    card.Width = flpQuestionList.ClientSize.Width - 42;
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
                    card.UpdateDisplay(qCounter++, card.Data.QuestionText);
                }
            }
        }

        // Meglévő kérdőívek betöltése a modellből
        public void LoadQuestionsFromModel(List<Question> questions)
        {
            flpQuestionList.Controls.Clear();
            qCounter = 1;

            foreach (var qData in questions)
            {
                UC_QuestionCard card = new UC_QuestionCard();
                card.Data = qData; // Átadjuk az adatokat a kártyának

                // --- ID átfordítása UI Kulcsra (Ezek az adatbázis ID-k) ---
                if (qData.QuestionTypeId == 1) qData.UI_TypeKey = "Single";
                else if (qData.QuestionTypeId == 2) qData.UI_TypeKey = "Multi";
                else if (qData.QuestionTypeId == 3) qData.UI_TypeKey = "tf";
                else if (qData.QuestionTypeId == 4) qData.UI_TypeKey = "Essay";
                else if (qData.QuestionTypeId == 5) qData.UI_TypeKey = "Short";
                else if (string.IsNullOrEmpty(qData.UI_TypeKey)) qData.UI_TypeKey = "Ismeretlen";

                // Kártya szövegének összeállítása
                string typeKey = qData.UI_TypeKey.ToUpper();
                string qText = qData.QuestionText != null ? qData.QuestionText : "Nincs szöveg";
                string summary = $"[{typeKey}] ({qData.Points} pont)\n{qText}";

                // Kártya hozzáadása a listához
                flpQuestionList.Controls.Add(card);
                card.Width = flpQuestionList.ClientSize.Width - 25;
                card.UpdateDisplay(qCounter++, summary);

                // --- ITT KÖTJÜK BE A KATTINTÁST A MEGLÉVŐ KÁRTYÁKRA ---
                AssignClickToAll(card, card);
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
            if (currentPanel == null) return;

            ActiveCard.Data.Answers.Clear();

            if (currentPanel is UC_Single_settings single)
            {
                ActiveCard.Data.QuestionText = single.QuestionText;
                ActiveCard.Data.Points = int.TryParse(single.Points, out int p) ? p : 1;
                ActiveCard.Data.LanguageId = single.SelectedLanguageId;
                ActiveCard.Data.QuestionLevelId = single.SelectedLevelId;
                ActiveCard.Data.SkillTypeId = single.SelectedSkillId;
                ActiveCard.Data.Answers.AddRange(single.GetAnswers());
            }
            else if (currentPanel is UC_Multi_settings multi)
            {
                ActiveCard.Data.QuestionText = multi.QuestionText;
                ActiveCard.Data.Points = int.TryParse(multi.Points, out int p) ? p : 1;
                ActiveCard.Data.LanguageId = multi.SelectedLanguageId;
                ActiveCard.Data.QuestionLevelId = multi.SelectedLevelId;
                ActiveCard.Data.SkillTypeId = multi.SelectedSkillId;
                ActiveCard.Data.Answers.AddRange(multi.GetAnswers());
            }
            else if (currentPanel is UC_TF_settings tf)
            {
                ActiveCard.Data.QuestionText = tf.QuestionText;
                ActiveCard.Data.Points = int.TryParse(tf.Points, out int p) ? p : 1;
                ActiveCard.Data.LanguageId = tf.SelectedLanguageId;
                ActiveCard.Data.QuestionLevelId = tf.SelectedLevelId;
                ActiveCard.Data.SkillTypeId = tf.SelectedSkillId;
                ActiveCard.Data.Answers.Add(new Answer { AnswerText = "Igaz", IsCorrect = tf.IsTrueSelected, AnswerOrder = 1 });
                ActiveCard.Data.Answers.Add(new Answer { AnswerText = "Hamis", IsCorrect = !tf.IsTrueSelected, AnswerOrder = 2 });
            }
            else if (currentPanel is UC_Essay_settings essay)
            {
                ActiveCard.Data.QuestionText = essay.QuestionText;
                ActiveCard.Data.Points = int.TryParse(essay.Points, out int p) ? p : 1;
                ActiveCard.Data.LanguageId = essay.SelectedLanguageId;
                ActiveCard.Data.QuestionLevelId = essay.SelectedLevelId;
                ActiveCard.Data.SkillTypeId = essay.SelectedSkillId;
                ActiveCard.Data.Answers.AddRange(essay.GetAnswers());
            }
            else if (currentPanel is UC_Shortans_settings trans)
            {
                ActiveCard.Data.QuestionText = trans.QuestionText;
                ActiveCard.Data.Points = int.TryParse(trans.Points, out int p) ? p : 1;
                ActiveCard.Data.LanguageId = trans.SelectedLanguageId;
                ActiveCard.Data.QuestionLevelId = trans.SelectedLevelId;
                ActiveCard.Data.SkillTypeId = trans.SelectedSkillId;

                // Ha a régi Shortans panelednek van GetAnswers() metódusa, akkor azt használd itt:
                ActiveCard.Data.Answers.AddRange(trans.GetAnswers()); 
            }

            // Biztonságos kártyaszöveg-generálás (a korábbi null-hibák elkerülése végett)
            string typeKey = ActiveCard.Data.UI_TypeKey != null ? ActiveCard.Data.UI_TypeKey.ToUpper() : "ISMERETLEN";
            string qText = ActiveCard.Data.QuestionText != null ? ActiveCard.Data.QuestionText : "Nincs szöveg";
            string summary = $"[{typeKey}] ({ActiveCard.Data.Points} pont)\n{qText}";

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

        private async void btnAddRandom_Click(object sender, EventArgs e)
        {
            // Megnézzük, milyen nyelvet és hány kérdést választott a felhasználó
            if (cmbRandomLanguage.SelectedValue == null) return;

            int langId = (int)cmbRandomLanguage.SelectedValue;
            int count = (int)numRandomCount.Value;

            try
            {
                // Gomb letiltása, amíg tölt
                btnAddRandom.Enabled = false;
                btnAddRandom.Text = "Töltés...";

                // API hívás a random végpontra
                string url = $"api/quiz/random?langId={langId}&count={count}";

                HttpResponseMessage response = await DatabaseService.Client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Szerver hiba ({response.StatusCode}):\n{errorContent}", "API Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var randomQuestions = await response.Content.ReadFromJsonAsync<List<Question>>();

                if (randomQuestions != null && randomQuestions.Count > 0)
                {
                    // 1. Összeszedjük a JELENLEG a felületen lévő kérdéseket
                    var currentQuestions = new List<Question>();
                    foreach (UC_QuestionCard card in flpQuestionList.Controls.OfType<UC_QuestionCard>())
                    {
                        currentQuestions.Add(card.Data);
                    }

                    // 2. Készítünk egy jövőbeli, kombinált listát a vizsgálathoz
                    var combinedQuestions = new List<Question>(currentQuestions);
                    combinedQuestions.AddRange(randomQuestions);

                    // --- ÚJ: SZINT-ELLENŐRZÉS A BEDOBÁS ELŐTT ---
                    var requiredLevels = new HashSet<int> { 1, 2, 3, 4, 5 }; // A1 - C1
                    var presentLevels = combinedQuestions.Select(q => q.QuestionLevelId).ToHashSet();
                    var missing = requiredLevels.Except(presentLevels).ToList();

                    if (missing.Count > 0)
                    {
                        // Kikeresjük a DropdownData-ból a hiányzó szintek neveit
                        var allLevels = DropdownData.GetLevels();
                        var missingNames = missing.Select(id => allLevels.FirstOrDefault(l => l.Key == id).Value ?? $"ID:{id}");
                        string missingLevelsString = string.Join(", ", missingNames);

                        DialogResult result = MessageBox.Show(
                            $"Ha bedobjuk ezt a {randomQuestions.Count} db kérdést, a kérdőívből (összesítve) még mindig hiányozni fognak a következő szintek: {missingLevelsString}.\n\nBiztosan hozzá szeretnéd adni őket?",
                            "Hiányos nyelvi szintek", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            return; // Felhasználó meggondolta magát, nem adjuk hozzá a kérdéseket
                        }
                    }

                    // 3. Ha idáig eljutott (mert minden szint megvan, vagy rányomott az Igen-re):
                    LoadQuestionsFromModel(combinedQuestions); // Újrarajzoljuk a teljes listát a jövőbeli listával

                    // Visszajelzés
                    string msg = randomQuestions.Count < count
                        ? $"Csak {randomQuestions.Count} db kérdést találtunk az adatbázisban, ezeket betöltöttük."
                        : $"Sikeresen bedobtunk {randomQuestions.Count} db véletlenszerű kérdést!";

                    MessageBox.Show(msg, "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nem találtunk kérdést ezen a nyelven az adatbázisban.", "Nincs találat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a random kérdések betöltésekor:\n" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Gomb visszaállítása
                btnAddRandom.Enabled = true;
                btnAddRandom.Text = "Random Kérdések Bedobása";
            }
        }

        private void cmbRandomLanguage_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void btnOpenBank_Click(object sender, EventArgs e)
        {
            // Megnyitjuk a kérdésbankot mint dialógusablakot
            using (FrmQuestionBank bankForm = new FrmQuestionBank())
            {
                // Ha a felhasználó a "Hozzáadás" gombra nyomott az ablakban
                if (bankForm.ShowDialog() == DialogResult.OK)
                {
                    if (bankForm.SelectedQuestions != null && bankForm.SelectedQuestions.Count > 0)
                    {
                        // 1. Összeszedjük a már meglévőket
                        var currentQuestions = new List<Question>();
                        foreach (UC_QuestionCard card in flpQuestionList.Controls.OfType<UC_QuestionCard>())
                        {
                            currentQuestions.Add(card.Data);
                        }

                        // 2. Hozzáfűzzük a bankból kiválasztottakat
                        currentQuestions.AddRange(bankForm.SelectedQuestions);

                        // 3. Újrarajzoljuk az egészet
                        LoadQuestionsFromModel(currentQuestions);

                        MessageBox.Show($"{bankForm.SelectedQuestions.Count} db kérdést sikeresen hozzáadtunk a kérdőívhez!",
                                        "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color LightBackground = Color.FromArgb(244, 247, 251);
        private readonly Color PanelBackground = Color.White;
        private readonly Color SoftPanelBackground = Color.FromArgb(235, 239, 246);
        private readonly Color BorderGray = Color.FromArgb(215, 220, 230);

        private void ApplyDesign()
        {
            this.BackColor = LightBackground;

            // Fő elrendezés
            pnlleft.Dock = DockStyle.Left;
            pnlleft.Width = 330;
            pnlleft.BackColor = PanelBackground;
            pnlleft.Padding = new Padding(22, 24, 22, 24);

            pnlright.Dock = DockStyle.Right;
            pnlright.Width = 430;
            pnlright.BackColor = SoftPanelBackground;
            pnlright.Padding = new Padding(20, 22, 20, 20);

            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.BackColor = LightBackground;
            pnlCenter.Padding = new Padding(28, 24, 28, 24);

            // Nagyon fontos: Dock sorrend miatt újrarendezzük a kontrollokat
            this.Controls.SetChildIndex(pnlleft, 2);
            this.Controls.SetChildIndex(pnlright, 1);
            this.Controls.SetChildIndex(pnlCenter, 0);

            // Bal oldali cím
            lblTypeHeader.Text = "Kérdéstípusok";
            lblTypeHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTypeHeader.ForeColor = TextPurple;
            lblTypeHeader.AutoSize = false;
            lblTypeHeader.Location = new Point(22, 24);
            lblTypeHeader.Size = new Size(280, 48);

            // Kérdéstípus gombok
            StyleTypeButton(btnShort, "Rövid válasz", 90);
            StyleTypeButton(btnMulti, "Több helyes válasz", 155);
            StyleTypeButton(btnTF, "Igaz / hamis", 220);
            StyleTypeButton(button1, "Egy helyes válasz", 285);
            StyleTypeButton(button2, "Esszé / kifejtős", 350);

            // Random kérdés blokk
            label1.Text = "Random kérdések";
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.ForeColor = TextPurple;
            label1.AutoSize = false;
            label1.Location = new Point(22, 445);
            label1.Size = new Size(280, 38);

            label2.Text = "Darabszám";
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label2.ForeColor = Color.FromArgb(75, 80, 95);
            label2.AutoSize = false;
            label2.Location = new Point(22, 495);
            label2.Size = new Size(260, 24);

            numRandomCount.Location = new Point(22, 522);
            numRandomCount.Size = new Size(260, 32);
            numRandomCount.Font = new Font("Segoe UI", 11F);

            label3.Text = "Nyelv";
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label3.ForeColor = Color.FromArgb(75, 80, 95);
            label3.AutoSize = false;
            label3.Location = new Point(22, 565);
            label3.Size = new Size(260, 24);

            cmbRandomLanguage.Location = new Point(22, 592);
            cmbRandomLanguage.Size = new Size(260, 32);
            cmbRandomLanguage.Font = new Font("Segoe UI", 11F);

            StyleSecondaryButton(btnAddRandom, "Random kérdések hozzáadása", 640);
            StyleSecondaryButton(btnOpenBank, "Kérdésbank megnyitása", 710);

            // Középső kérdéslista
            flpQuestionList.Dock = DockStyle.Fill;
            flpQuestionList.BackColor = Color.White;
            flpQuestionList.FlowDirection = FlowDirection.TopDown;
            flpQuestionList.WrapContents = false;
            flpQuestionList.AutoScroll = true;
            flpQuestionList.Padding = new Padding(18);

            // Jobb oldali alapállapot
            pnlright.Controls.Clear();

            Label emptySettings = new Label();
            emptySettings.Text = "Válassz ki egy kérdést a beállítások szerkesztéséhez.";
            emptySettings.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
            emptySettings.ForeColor = Color.FromArgb(75, 80, 95);
            emptySettings.AutoSize = false;
            emptySettings.TextAlign = ContentAlignment.MiddleCenter;
            emptySettings.Dock = DockStyle.Fill;

            pnlright.Controls.Add(emptySettings);

            this.Resize += UC_TypeSelector_Resize;
        }

        //Gombok
        private void StyleTypeButton(Button button, string text, int top)
        {
            button.Text = text;
            button.Location = new Point(22, top);
            button.Size = new Size(260, 52);
            button.BackColor = Color.FromArgb(245, 247, 255);
            button.ForeColor = TextPurple;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = BorderGray;
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(16, 0, 0, 0);
            button.Cursor = Cursors.Hand;
        }

        private void StyleSecondaryButton(Button button, string text, int top)
        {
            button.Text = text;
            button.Location = new Point(22, top);
            button.Size = new Size(260, 50);
            button.BackColor = DarkBlue;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        //Méret
        private void UC_TypeSelector_Resize(object sender, EventArgs e)
        {
            ResizeCards();
        }
    }
}