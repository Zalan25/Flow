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

            btnRemove.Location = new Point(175, 7);
            btnRemove.Size = new Size(68, 30);
            txtAltAnswer.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            txtAltAnswer.Location = new Point(6, 8);
            txtAltAnswer.Size = new Size(160, 26);

            btnRemove.Text = "Törlés";
            btnRemove.Location = new Point(174, 7);
            btnRemove.Size = new Size(62, 28);
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
            int buttonWidth = 68;
            int rightPadding = 6;
            int gap = 8;

            btnRemove.Width = 62;
            btnRemove.Left = 174;

            txtAltAnswer.Left = 6;
            txtAltAnswer.Width = 160;
        }

    }
}
