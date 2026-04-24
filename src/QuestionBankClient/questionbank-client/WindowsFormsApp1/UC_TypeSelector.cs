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

    public partial class UC_TypeSelector : UserControl
    {
        public UC_TypeSelector()
        {
            InitializeComponent();
            this.Load += UC_TypeSelector_Load;
        }

        private void UC_TypeSelector_Load(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void CenterControls()
        {
            btnAdd.Left = (pnlCenter.Width - btnAdd.Width) / 2;
            btnAdd.Top = 20; // vagy ahova tenni akarod fentről
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlCenter.Controls.Clear();
            var uc = new UC_Typselector_mid { Dock = DockStyle.Fill };
            pnlCenter.Controls.Add(uc);
            
        }

        private void btnShort_Click(object sender, EventArgs e)
        {
            pnlright.Controls.Clear();
            UC_Typselector_mid mid = new UC_Typselector_mid { Dock = DockStyle.Fill };
            pnlright.Controls.Add(mid);
        }

        private void UC_TypeSelector_Load_1(object sender, EventArgs e)
        {

        }
    }
}
