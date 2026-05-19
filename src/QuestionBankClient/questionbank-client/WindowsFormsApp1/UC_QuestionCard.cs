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
            lblText.Text = string.IsNullOrWhiteSpace(displayContent)
    ? "Nincs megadva kérdésszöveg..."
    : displayContent;


            // AutoSize = False
            // Anchor = Top, Left, Right, Bottom (vagy Dock = Fill egy panelen belül)
        }

        //private void pnlquestioncard_Paint(object sender, PaintEventArgs e)
        //{
        //    Control paintControl = sender as Control;
        //    if (paintControl == null) return;


        //    using (Pen pen = new Pen(Color.Navy, 2))
        //    {
        //        e.Graphics.DrawRectangle(pen, 1, 1, paintControl.Width - 3, paintControl.Height - 3);
        //    }
        //}

        //Kinézet
        private readonly Color DarkBlue = Color.FromArgb(25, 52, 88);
        private readonly Color TextPurple = Color.FromArgb(48, 45, 91);
        private readonly Color CardBorder = Color.FromArgb(215, 220, 230);
        private readonly Color CardBackground = Color.White;
        private readonly Color SelectedBackground = Color.FromArgb(229, 237, 255);
        private readonly Color SelectedBorder = Color.FromArgb(25, 52, 88);

        private bool isSelected = false;
        private void ApplyDesign()
        {
            this.BackColor = CardBackground;
            this.Height = 150;
            this.Margin = new Padding(0, 0, 0, 18);
            this.Padding = new Padding(0);
            this.Cursor = Cursors.Hand;

            pnlquestioncard.Dock = DockStyle.Fill;
            pnlquestioncard.BackColor = CardBackground;
            pnlquestioncard.Padding = new Padding(18);
            pnlquestioncard.Cursor = Cursors.Hand;

            lblNumber.AutoSize = false;
            lblNumber.Location = new Point(18, 14);
            lblNumber.Size = new Size(260, 26);
            lblNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNumber.ForeColor = TextPurple;
            lblNumber.TextAlign = ContentAlignment.MiddleLeft;
            lblNumber.BackColor = CardBackground;
            lblNumber.Cursor = Cursors.Hand;

            lblText.AutoSize = false;
            lblText.Location = new Point(18, 48);
            lblText.Size = new Size(pnlquestioncard.Width - 36, 82);
            lblText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblText.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            lblText.ForeColor = DarkBlue;
            lblText.TextAlign = ContentAlignment.TopLeft;
            lblText.BackColor = CardBackground;
            lblText.Cursor = Cursors.Hand;

            this.Resize -= UC_QuestionCard_Resize;
            this.Resize += UC_QuestionCard_Resize;
        }

        private void pnlquestioncard_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = isSelected ? SelectedBorder : CardBorder;
            int borderWidth = isSelected ? 2 : 1;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(
                    pen,
                    borderWidth,
                    borderWidth,
                    pnlquestioncard.Width - borderWidth * 2 - 1,
                    pnlquestioncard.Height - borderWidth * 2 - 1
                );
            }
        }

        //Méret
        private void UC_QuestionCard_Resize(object sender, EventArgs e)
        {
            lblText.Size = new Size(pnlquestioncard.Width - 36, 82);
            pnlquestioncard.Invalidate();
        }

        //Jelölés
        public void SetSelected(bool selected)
        {
            isSelected = selected;

            Color background = selected ? SelectedBackground : CardBackground;

            this.BackColor = background;
            pnlquestioncard.BackColor = background;
            lblNumber.BackColor = background;
            lblText.BackColor = background;

            pnlquestioncard.Invalidate();
            this.Invalidate();
        }

        //Felirat
    }
}