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
        private void ApplyDesign()
        {
            this.Height = 44;
            this.BackColor = Color.White;
            this.Margin = new Padding(0, 0, 0, 8);

            txtAltAnswer.Location = new Point(8, 8);
            txtAltAnswer.Size = new Size(200, 28);
            txtAltAnswer.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            btnRemove.Text = "törlés";
            btnRemove.Location = new Point(220, 7);
            btnRemove.Size = new Size(80, 30);
            btnRemove.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnRemove.BackColor = Color.FromArgb(240, 240, 240);
            btnRemove.ForeColor = Color.Black;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.FlatAppearance.BorderSize = 1;

            this.Resize -= UC_ShortAnswer_Item_Resize;
            this.Resize += UC_ShortAnswer_Item_Resize;
        }

        private void UC_ShortAnswer_Item_Resize(object sender, EventArgs e)
        {
            int buttonWidth = 80;
            int rightPadding = 8;
            int gap = 10;

            btnRemove.Width = buttonWidth;
            btnRemove.Left = this.Width - buttonWidth - rightPadding;

            txtAltAnswer.Left = 8;
            txtAltAnswer.Width = btnRemove.Left - gap - txtAltAnswer.Left;
        }

    }
}
