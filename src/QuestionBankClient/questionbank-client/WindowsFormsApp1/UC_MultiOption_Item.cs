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
    
    public partial class UC_MultiOption_Item : UserControl
    {
        public string OptionText { get => txtOption.Text; set => txtOption.Text = value; }
        public bool IsCorrect { get => rbCorrect.Checked; set => rbCorrect.Checked = value; }

        public UC_MultiOption_Item() { InitializeComponent(); }

        private void UC_MultiOption_Item_Load(object sender, EventArgs e)
        {

        }
    }
}
