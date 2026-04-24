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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int qCount = 0;
        // Form1.cs-be a qCount mellé:
        public Quiz ActiveQuiz = new Quiz();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void pnlmain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnexisting_Click(object sender, EventArgs e)
        {

        }

        private void btnnew_Click(object sender, EventArgs e)
        {

            pnlbetamain.Controls.Clear();

            
            UC_NewQuiz uc = new UC_NewQuiz();

            
            uc.Dock = DockStyle.Fill;


            pnlbetamain.Controls.Add(uc);
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {

        }
        
    }
}
