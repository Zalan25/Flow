using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuestionBankClient
{
    public partial class UC_TF_settings : UserControl
    {
        // Windows API a láthatatlan "Ghost Text" (Placeholder) funkcióhoz
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public UC_TF_settings()
        {
            InitializeComponent();

            this.HandleCreated += (s, e) => {
                SetPlaceholder(txtQuestionText, "Írd be az Igaz-Hamis kérdést ide...");
                SetPlaceholder(txtPoints, "Pont");
            };
            if (txtPoints != null) txtPoints.KeyPress += TxtPoints_KeyPress;
        }

        // --- ADATOK ELÉRÉSE (A TypeSelector számára) ---

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

        // Később ezt is elmenthetjük az SQL-be, ha a Modelben felveszünk egy IsMandatory mezőt!
        public bool IsMandatory => chkMandatory != null && chkMandatory.Checked;

        public bool IsTrueSelected => rbTrue != null && rbTrue.Checked;

        // --- MŰVELETEK ---

        private void SetPlaceholder(Control control, string text)
        {
            if (control != null && control.IsHandleCreated)
                SendMessage(control.Handle, 0x1501, 0, text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null) parent.DeleteCurrentCard();
        }

        // Ha a későbbiekben az "Opció hozzáadása" gombnak szeretnél funkciót adni, itt teheted meg:
        private void btnAddOption_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Igaz-Hamis kérdésnél alapból csak két opció lehetséges.");
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