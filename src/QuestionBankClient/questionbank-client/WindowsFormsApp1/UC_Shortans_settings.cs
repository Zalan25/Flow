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
    public partial class UC_Shortans_settings : UserControl
    {
        public UC_Shortans_settings()
        {
            InitializeComponent();
        }

        private void UC_Shortans_settings_Load(object sender, EventArgs e)
        {

        }

        public string CorrectAnswer
        {
            get => txtCorrectAnswer.Text;
            set => txtCorrectAnswer.Text = value;
        }

        private void btnAddAns_Click(object sender, EventArgs e)
        {
            var parent = (Form1)this.ParentForm;
            
            txtCorrectAnswer.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
