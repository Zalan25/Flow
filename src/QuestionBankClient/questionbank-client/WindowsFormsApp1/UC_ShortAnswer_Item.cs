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

        public UC_ShortAnswer_Item() { InitializeComponent(); }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Egyszerűen eltávolítja magát a listából
            this.Parent.Controls.Remove(this);
        }

        private void UC_ShortAnswer_Item_Load(object sender, EventArgs e)
        {

        }
    }
}
