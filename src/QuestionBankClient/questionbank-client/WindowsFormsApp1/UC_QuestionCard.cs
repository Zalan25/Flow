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
    public partial class UC_QuestionCard : UserControl
    {
        
        public Question Data { get; set; } = new Question();

        public UC_QuestionCard()
        {
            InitializeComponent();
            ApplyDesign();
        }

        // Frissítjük a kártya kinézetét
        // A 'number' a sorszám, a 'text' pedig a megjelenítendő kérdés
        public void UpdateDisplay(int number, string displayContent)
        {
            // A 'number' a sorszám (pl. 1. kérdés)
            lblNumber.Text = number.ToString() + ". kérdés";

            // A 'displayContent' mostantól a kérdést ÉS az összefoglalót is tartalmazza
            lblText.Text = string.IsNullOrWhiteSpace(displayContent) ? "Nincs megadva tartalom..." : displayContent;

            
            // AutoSize = False
            // Anchor = Top, Left, Right, Bottom (vagy Dock = Fill egy panelen belül)
        }

        private void pnlquestioncard_Paint(object sender, PaintEventArgs e)
        {
            Control paintControl = sender as Control;
            if (paintControl == null) return;

            
            using (Pen pen = new Pen(Color.Navy, 2))
            {
                e.Graphics.DrawRectangle(pen, 1, 1, paintControl.Width - 3, paintControl.Height - 3);
            }
        }

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color CardBorder = Color.FromArgb(215, 220, 230);
        private readonly Color CardBackground = Color.White;
        private readonly Color SoftBlue = Color.FromArgb(245, 247, 255);
        private readonly Color DeleteRed = Color.FromArgb(190, 65, 65);
        private void ApplyDesign()
        {
            this.BackColor = CardBackground;
            this.Height = 150;
            this.Margin = new Padding(0, 0, 0, 18);
            this.Padding = new Padding(18);

            this.Paint -= UC_QuestionCard_Paint;
            this.Paint += UC_QuestionCard_Paint;

            foreach (Control control in this.Controls)
            {
                control.BackColor = CardBackground;
            }

            StyleLabels();
            StyleButtons();
        }

        //Keret
        private void UC_QuestionCard_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(CardBorder, 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        //Felirat
    }
}