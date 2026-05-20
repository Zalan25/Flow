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
            ApplyDesign();
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
                    Label lblEmpty = new Label();
                    lblEmpty.Text = "Még nincs elmentett kérdőíved.";
                    lblEmpty.AutoSize = true;
                    lblEmpty.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
                    lblEmpty.ForeColor = Color.FromArgb(80, 85, 100);
                    lblEmpty.Margin = new Padding(0, 10, 0, 0);

                    flpQuizzes.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var quiz in quizzes)
                {
                    Panel card = CreateQuizCard(quiz);
                    flpQuizzes.Controls.Add(card);
                    //Panel pnlRow = new Panel
                    //{
                    //    Width = flpQuizzes.Width - 25,
                    //    Height = 80,
                    //    Margin = new Padding(0, 0, 0, 10)
                    //};

                //    Button btnDelete = new Button
                //    {
                //        Text = "Törlés",
                //        Width = 100,
                //        Dock = DockStyle.Right,
                //        BackColor = Color.Crimson,
                //        ForeColor = Color.White,
                //        FlatStyle = FlatStyle.Flat,
                //        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                //        Cursor = Cursors.Hand,
                //        Tag = quiz.TestId
                //    };
                //    btnDelete.FlatAppearance.BorderSize = 0;
                //    btnDelete.Click += async (s, e) => {
                //        int id = (int)((Button)s).Tag;
                //        await DeleteQuizFromDatabase(id);
                //    };

                //    Button btnQuiz = new Button
                //    {
                //        Text = $"{quiz.Title}\n(ID: {quiz.TestId}) - {quiz.Description}",
                //        Dock = DockStyle.Fill,
                //        BackColor = Color.FromArgb(235, 245, 255),
                //        FlatStyle = FlatStyle.Flat,
                //        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                //        TextAlign = ContentAlignment.MiddleLeft,
                //        Cursor = Cursors.Hand,
                //        Tag = quiz.TestId
                //    };
                //    btnQuiz.FlatAppearance.BorderSize = 0;
                //    btnQuiz.Click += async (s, e) => {
                //        int id = (int)((Button)s).Tag;
                //        Quiz loadedQuiz = await FetchFullQuiz(id);

                //        if (loadedQuiz != null)
                //        {
                //            Form1 main = (Form1)this.ParentForm;
                //            main.OpenExistingQuiz(loadedQuiz);
                //        }
                //    };

                //    pnlRow.Controls.Add(btnDelete);
                //    pnlRow.Controls.Add(btnQuiz);
                //    flpQuizzes.Controls.Add(pnlRow);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a kérdőívek betöltésekor: " + ex.Message, "Hálózati hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color LightBackground = Color.FromArgb(244, 247, 251);
        private readonly Color CardBorder = Color.FromArgb(215, 220, 230);
        private readonly Color DeleteRed = Color.FromArgb(190, 65, 65);
        private Button btnNewQuiz;

        private void ApplyDesign()
        {
            this.BackColor = Theme.PageBackground;

            lblTitle.Text = "Kérdőíveim";
            lblTitle.Font = Theme.TitleFont;
            lblTitle.ForeColor = Theme.TextPurple;
            lblTitle.AutoSize = true;

            if (btnNewQuiz == null)
            {
                btnNewQuiz = new Button();
                btnNewQuiz.Name = "btnNewQuiz";

                btnNewQuiz.Click += (s, e) =>
                {
                    Form1 main = this.ParentForm as Form1;

                    if (main != null)
                    {
                        main.ShowNewQuiz();
                    }
                };

                this.Controls.Add(btnNewQuiz);
            }

            btnNewQuiz.Text = "+ Új kérdőív";
            btnNewQuiz.Size = new Size(230, 56);
            btnNewQuiz.Anchor = AnchorStyles.None;
            Theme.StylePrimaryButton(btnNewQuiz);
            btnNewQuiz.Cursor = Cursors.Hand;

            flpQuizzes.Anchor = AnchorStyles.None;
            flpQuizzes.BackColor = Theme.PageBackground;
            flpQuizzes.FlowDirection = FlowDirection.LeftToRight;
            flpQuizzes.WrapContents = true;
            flpQuizzes.AutoScroll = true;
            flpQuizzes.Padding = new Padding(0);

            this.Resize -= UC_QuizList_ResizeDesignOnly;
            this.Resize += UC_QuizList_ResizeDesignOnly;

            ApplyQuizListLayout();

            btnNewQuiz.BringToFront();
            lblTitle.BringToFront();
        }
        private void ApplyQuizListLayout()
        {
            if (lblTitle != null)
            {
                lblTitle.Location = new Point(40, 35);
            }

            if (btnNewQuiz != null)
            {
                btnNewQuiz.Location = new Point(
                    Math.Max(40, this.ClientSize.Width - btnNewQuiz.Width - 40),
                    35
                );

                btnNewQuiz.BringToFront();
            }

            if (flpQuizzes != null)
            {
                flpQuizzes.Location = new Point(40, 140);

                flpQuizzes.Size = new Size(
                    Math.Max(300, this.ClientSize.Width - 80),
                    Math.Max(250, this.ClientSize.Height - 180)
                );
            }

            if (lblTitle != null)
            {
                lblTitle.BringToFront();
            }

            if (btnNewQuiz != null)
            {
                btnNewQuiz.BringToFront();
            }
        }
        private void UC_QuizList_ResizeDesignOnly(object sender, EventArgs e)
        {
            ApplyQuizListLayout();
        }

        //Quiz kártya létrehozása
        private Panel CreateQuizCard(Quiz quiz)
        {
            Panel card = new Panel();
            card.Width = 330;
            card.Height = 225;
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 28, 28);
            card.Padding = new Padding(18);
            card.Tag = quiz.TestId;

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(CardBorder, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            Label title = new Label();
            title.Text = string.IsNullOrWhiteSpace(quiz.Title) ? "Névtelen kérdőív" : quiz.Title;
            title.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            title.ForeColor = DarkBlue;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.AutoSize = false;
            title.Location = new Point(20, 45);
            title.Size = new Size(card.Width - 40, 60);

            Button btnEdit = new Button();
            btnEdit.Text = "Szerkesztés";
            btnEdit.Size = new Size(140, 44);
            btnEdit.Location = new Point(18, 150);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.BackColor = Color.FromArgb(245, 247, 255);
            btnEdit.ForeColor = TextPurple;
            btnEdit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Tag = quiz.TestId;
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(105, 115, 165);
            btnEdit.FlatAppearance.BorderSize = 1;
            btnEdit.Click += async (s, e) =>
            {
                int id = (int)((Button)s).Tag;
                Quiz loadedQuiz = await FetchFullQuiz(id);

                if (loadedQuiz != null)
                {
                    Form1 main = (Form1)this.ParentForm;
                    main.OpenExistingQuiz(loadedQuiz);
                }
            };

            Button btnDelete = new Button();
            btnDelete.Text = "Törlés";
            btnDelete.Size = new Size(130, 44);
            btnDelete.Location = new Point(180, 150);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.BackColor = Color.FromArgb(255, 245, 245);
            btnDelete.ForeColor = DeleteRed;
            btnDelete.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Tag = quiz.TestId;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(235, 120, 120);
            btnDelete.FlatAppearance.BorderSize = 1;
            btnDelete.Click += async (s, e) =>
            {
                int id = (int)((Button)s).Tag;
                await DeleteQuizFromDatabase(id);
            };

            card.Controls.Add(title);
            card.Controls.Add(btnEdit);
            card.Controls.Add(btnDelete);

            return card;
        }

      
    }
}