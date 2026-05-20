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
    public partial class UC_ShortAnswer_Item : UserControl
    {
        public string AnswerText { get => txtAltAnswer.Text; set => txtAltAnswer.Text = value; }

        public UC_ShortAnswer_Item() 
        { 
            InitializeComponent();
            ApplyDesign();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Egyszerűen eltávolítja magát a listából
            this.Parent.Controls.Remove(this);
        }

        private void UC_ShortAnswer_Item_Load(object sender, EventArgs e)
        {

        }

        //Kinézet
        private Label lblCorrect;
        private void ApplyDesign()
        {
            this.AutoSize = false;
            this.Margin = new Padding(0, 0, 0, 10);
            this.Height = 42;
            this.BackColor = Color.White;
            this.Margin = new Padding(0, 0, 0, 8);

            if (lblCorrect == null)
            {
                lblCorrect = new Label();
                lblCorrect.Text = "✓";
                lblCorrect.AutoSize = false;
                lblCorrect.Size = new Size(18, 20);
                lblCorrect.Location = new Point(6, 11);
                lblCorrect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblCorrect.ForeColor = Color.SeaGreen;
                lblCorrect.BackColor = Color.White;
                this.Controls.Add(lblCorrect);
            }

            txtAltAnswer.Location = new Point(28, 8);
            txtAltAnswer.Size = new Size(150, 26);
            txtAltAnswer.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            btnRemove.Text = "Törlés";
            btnRemove.Location = new Point(188, 7);
            btnRemove.Size = new Size(56, 28);
            btnRemove.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnRemove.BackColor = Color.FromArgb(245, 245, 245);
            btnRemove.ForeColor = Color.Black;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 1;

            this.Resize -= UC_ShortAnswer_Item_Resize;
            this.Resize += UC_ShortAnswer_Item_Resize;

            UC_ShortAnswer_Item_Resize(this, EventArgs.Empty);
        }

        private void UC_ShortAnswer_Item_Resize(object sender, EventArgs e)
        {
            if (this.Width < 220)
                return;

            if (lblCorrect != null)
            {
                lblCorrect.Left = 6;
                lblCorrect.Top = 11;
            }

            btnRemove.Width = 58;
            btnRemove.Height = 28;
            btnRemove.Left = this.ClientSize.Width - btnRemove.Width - 6;
            btnRemove.Top = 7;

            txtAltAnswer.Left = 28;
            txtAltAnswer.Top = 8;
            txtAltAnswer.Width = btnRemove.Left - txtAltAnswer.Left - 10;
            txtAltAnswer.Height = 26;

            btnRemove.BringToFront();
        }

    }
}
