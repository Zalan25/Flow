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
            var newItem = new UC_ShortAnswer_Item { Width = flpAnswers.Width - 25 };
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
            // Csak számokat (0-9) és a Backspace-t (törlés) engedjük át
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ha betű, akkor "lenyeljük", nem jelenik meg!
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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}