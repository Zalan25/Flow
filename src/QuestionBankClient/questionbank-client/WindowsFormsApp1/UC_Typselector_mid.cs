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
    public partial class UC_Typselector_mid : UserControl
    {
        public UC_Typselector_mid()
        {
            InitializeComponent();
        }

        private void btnShort_Click(object sender, EventArgs e)
        {
            // Megkeressük a panelt név alapján bárhol a szülő formon belül
            var panel = this.ParentForm.Controls.Find("pnlright", true).FirstOrDefault() as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                UC_Shortans_settings mid = new UC_Shortans_settings { Dock = DockStyle.Fill };
                panel.Controls.Add(mid);
            }
        }

        private void btnLong_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
