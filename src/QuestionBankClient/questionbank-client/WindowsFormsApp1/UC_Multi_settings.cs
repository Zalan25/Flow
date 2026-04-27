using QuestionBankClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace QuestionBankClient
{
    public partial class UC_Multi_settings : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public UC_Multi_settings()
        {
            InitializeComponent();

            this.HandleCreated += (s, e) => {
                SetPlaceholder(txtQuestionText, "Milyen nyelven szeretnél tanulni?");
                SetPlaceholder(txtHint, "Ide írd a súgó szöveget...");
                SetPlaceholder(txtPoints, "Pont");
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

        public bool IsMandatory => chkMandatory != null && chkMandatory.Checked;

        // --- MŰVELETEK ---

        public List<Answer> GetAnswers()
        {
            var answers = new List<Answer>();
            int order = 1;

            foreach (UC_MultiOption_Item item in flpOptions.Controls.OfType<UC_MultiOption_Item>())
            {
                if (!string.IsNullOrWhiteSpace(item.OptionText))
                {
                    answers.Add(new Answer
                    {
                        AnswerText = item.OptionText,
                        IsCorrect = item.IsCorrect,
                        AnswerOrder = order++
                    });
                }
            }
            return answers;
        }

        private void SetPlaceholder(Control control, string text)
        {
            if (control != null && control.IsHandleCreated)
                SendMessage(control.Handle, 0x1501, 0, text);
        }

        private void btnAddOption_Click(object sender, EventArgs e)
        {
            var newItem = new UC_MultiOption_Item { Width = flpOptions.Width - 25 };
            flpOptions.Controls.Add(newItem);
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
    }
}