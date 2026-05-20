using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QuestionBankClient
{
    public partial class UC_Shortans_settings : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public UC_Shortans_settings()
        {
            InitializeComponent();
            ApplyDesign();

            cmbSkill.DataSource = new BindingSource(DropdownData.GetSkills(), null);
            cmbSkill.DisplayMember = "Value";
            cmbSkill.ValueMember = "Key";

            // Nyelvek betöltése a legördülőbe
            cmbQuestionLanguage.DataSource = new BindingSource(DropdownData.GetLanguages(), null);
            cmbQuestionLanguage.DisplayMember = "Value"; // Amit a felhasználó lát (pl. Görög)
            cmbQuestionLanguage.ValueMember = "Key";     // Az ID, amit a gép lát (pl. 2)

            // Szintek betöltése a legördülőbe
            cmbQuestionLevel.DataSource = new BindingSource(DropdownData.GetLevels(), null);
            cmbQuestionLevel.DisplayMember = "Value";
            cmbQuestionLevel.ValueMember = "Key";

            panel1.Dock = DockStyle.Fill;

            this.HandleCreated += (s, e) => {
                SetPlaceholder(txtQuestionText, "pl. Mi az angol 'alma' szó?");
                
                SetPlaceholder(txtPoints, "1");
            };
            if (txtPoints != null) txtPoints.KeyPress += TxtPoints_KeyPress;
            FixScrollWidth();
        }

        // --- ADATOK ELÉRÉSE ---

        public string QuestionText
        {
            get => txtQuestionText?.Text ?? "";
            set { if (txtQuestionText != null) txtQuestionText.Text = value; }
        }

        public string Points
        {
            get => txtPoints?.Text ?? "1";
            set { if (txtPoints != null) txtPoints.Text = value; }
        }

        
        public int SelectedLanguageId
        {
            get => cmbQuestionLanguage.SelectedValue != null ? (int)cmbQuestionLanguage.SelectedValue : 1;
            set { if (cmbQuestionLanguage != null) cmbQuestionLanguage.SelectedValue = value; }
        }

        
        public int SelectedLevelId
        {
            get => cmbQuestionLevel.SelectedValue != null ? (int)cmbQuestionLevel.SelectedValue : 1;
            set { if (cmbQuestionLevel != null) cmbQuestionLevel.SelectedValue = value; }
        }


        public int SelectedSkillId
        {
            get => cmbSkill.SelectedValue != null ? (int)cmbSkill.SelectedValue : 1;
            set => cmbSkill.SelectedValue = value;
        }


        // --- MŰVELETEK ---

        public List<Answer> GetAnswers()
        {
            var list = new List<Answer>();
            int order = 1;
            foreach (UC_ShortAnswer_Item item in flpAnswers.Controls.OfType<UC_ShortAnswer_Item>())
            {
                if (!string.IsNullOrWhiteSpace(item.AnswerText))
                {
                    list.Add(new Answer
                    {
                        AnswerText = item.AnswerText,
                        IsCorrect = true,
                        AnswerOrder = order++
                    });
                }
            }
            return list;
        }

        private void SetPlaceholder(Control control, string text)
        {
            if (control != null && control.IsHandleCreated)
                SendMessage(control.Handle, 0x1501, 0, text);
        }

        private void btnAddAns_Click_1(object sender, EventArgs e)
        {
            // Új alternatíva hozzáadása a listához
            //var newItem = new UC_ShortAnswer_Item { Width = flpAnswers.Width - 25 };
            var newItem = new UC_ShortAnswer_Item
            {
                Width = flpAnswers.ClientSize.Width - 24
            };
            flpAnswers.Controls.Add(newItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null) parent.DeleteCurrentCard();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // Ez a metódus figyeli, hogy mit gépel a felhasználó
        private void TxtPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Megkeressük a szülő TypeSelector panelt
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null)
            {
                // Rászólunk, hogy mentse el a kártyát!
                parent.SaveCurrentCard();
                MessageBox.Show("A kérdés frissítve lett a listában!", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null) parent.DeleteCurrentCard();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Megkeressük a szülő TypeSelector panelt
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null)
            {
                // Rászólunk, hogy mentse el a kártyát!
                parent.SaveCurrentCard();
                MessageBox.Show("A kérdés frissítve lett a listában!", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // --- VÁLASZOK VISSZATÖLTÉSE ---
        public void SetAnswers(List<Answer> answers)
        {
            flpAnswers.Controls.Clear(); 

            if (answers == null) return;

            foreach (var ans in answers)
            {
                // Létrehozzuk a sort a meglévő adatból
                var item = new UC_ShortAnswer_Item();
                //item.Width = flpAnswers.Width - 25;
                item.Width = flpAnswers.ClientSize.Width - 24;

                // Visszatöltjük a szöveget
                item.AnswerText = ans.AnswerText;

                // Hozzáadjuk a listához
                flpAnswers.Controls.Add(item);
            }
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color SoftPanelBackground = Color.FromArgb(235, 239, 246);
        private readonly Color InputBorder = Color.FromArgb(200, 207, 220);
        private readonly Color DeleteRed = Color.FromArgb(190, 65, 65);
        private void ApplyDesign()
        {
            this.BackColor = SoftPanelBackground;

            this.BackColor = SoftPanelBackground;
            this.Size = new Size(360, 720);
            this.MinimumSize = new Size(360, 0);
            this.MaximumSize = new Size(360, 0);

            panel1.BackColor = SoftPanelBackground;
            panel1.Dock = DockStyle.Fill;
            panel1.AutoScroll = true;
            panel1.AutoScrollMinSize = new Size(0, 720);
            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
            panel1.Padding = new Padding(0);

            // Felső sor
            StyleLabel(label5, "Nyelv", 24, 18);
            StyleCombo(cmbQuestionLanguage, 24, 46, 140);

            StyleLabel(label4, "Szint", 204, 18);
            StyleCombo(cmbQuestionLevel, 200, 46, 140);

            // Második sor
            StyleLabel(label6, "Készség", 24, 100);
            StyleCombo(cmbSkill, 24, 128, 200);

            StyleLabel(lblrate, "Pontszám", 254, 100);
            StyleTextBox(txtPoints, 250, 128, 90, 30, false);

            // Kérdés
            StyleSectionLabel(label1, "Kérdés szövege", 24, 188);
            StyleTextBox(txtQuestionText, 24, 224, 316, 100, true);

            // Helyes válaszok
            StyleSectionLabel(label2, "Helyes válaszok", 24, 356);

            StyleSmallButton(btnAddAns, "+ Másik válasz", 210, 352);

            flpAnswers.Location = new Point(24, 405);
            flpAnswers.Size = new Size(316, 185);
            flpAnswers.BackColor = Color.White;
            flpAnswers.FlowDirection = FlowDirection.TopDown;
            flpAnswers.WrapContents = false;
            flpAnswers.AutoScroll = true;
            flpAnswers.Padding = new Padding(8);

            // Alsó gombok
            StyleDeleteButton(btnDelete, "Törlés", 24, 615);
            StylePrimaryButton(btnSave, "Mentés", 190, 615);

            // Rejtett / nem használt labelök
            lblBaseSettings.Visible = false;
            lblCheck.Visible = false;
            lblEval.Visible = false;
            label3.Visible = false;

            this.Resize -= UC_Shortans_settings_Resize;
            this.Resize += UC_Shortans_settings_Resize;
        }

        //Stílus segédfüggvények
        private void StyleLabel(Label label, string text, int x, int y)
        {
            label.Text = text;
            label.AutoSize = false;
            label.Location = new Point(x, y);
            label.Size = new Size(150, 24);
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(70, 76, 90);
            label.BackColor = SoftPanelBackground;
        }

        private void StyleSectionLabel(Label label, string text, int x, int y)
        {
            label.Text = text;
            label.AutoSize = false;
            label.Location = new Point(x, y);
            label.Size = new Size(220, 28);
            label.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label.ForeColor = TextPurple;
            label.BackColor = SoftPanelBackground;
        }

        private void StyleCombo(ComboBox combo, int x, int y, int width)
        {
            combo.Location = new Point(x, y);
            combo.Size = new Size(width, 32);
            combo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.BackColor = Color.White;
            combo.ForeColor = DarkBlue;
        }

        private void StyleTextBox(TextBox textBox, int x, int y, int width, int height, bool multiline)
        {
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, height);
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = DarkBlue;
            textBox.Multiline = multiline;
        }

        private void StyleSmallButton(Button button, string text, int x, int y)
        {
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(130, 34);
            button.BackColor = Color.White;
            button.ForeColor = TextPurple;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = InputBorder;
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void StylePrimaryButton(Button button, string text, int x, int y)
        {
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(150, 44);
            button.BackColor = DarkBlue;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void StyleDeleteButton(Button button, string text, int x, int y)
        {
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(150, 44);
            button.BackColor = Color.FromArgb(255, 245, 245);
            button.ForeColor = DeleteRed;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(235, 120, 120);
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        //Méret
        private void UC_Shortans_settings_Resize(object sender, EventArgs e)
        {
            // Direkt fix, keskeny layout: ne húzza szét vízszintesen.
            int contentWidth = 330;

            txtQuestionText.Width = contentWidth;
            flpAnswers.Width = contentWidth;

            btnAddAns.Left = 24 + contentWidth - btnAddAns.Width;

            btnDelete.Left = 24;
            btnSave.Left = 24 + contentWidth - btnSave.Width;

            btnDelete.Top = 615;
            btnSave.Top = 615;

            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
        }

        //SCroll
        private void FixScrollWidth()
        {
            this.Width = 360;
            this.MinimumSize = new Size(360, 0);
            this.MaximumSize = new Size(360, 0);

            panel1.Width = 360;
            panel1.AutoScroll = true;
            panel1.AutoScrollMinSize = new Size(0, 720);

            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
            panel1.HorizontalScroll.Maximum = 0;

            this.PerformLayout();
            panel1.PerformLayout();
        }
    }
}