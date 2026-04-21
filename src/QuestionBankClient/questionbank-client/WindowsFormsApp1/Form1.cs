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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }
        // Kezdőlap visszatöltése
        private void SetupStartPage()
        {
            
            pnlMainContent.Controls.Add(pnlStartCard);
        }
        // Start -> Típusválasztó UC váltás
        private void btnAddQuestions_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            var uc = new UC_TypeSelector { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(uc);
            btnBack.Visible = true;
        }

        // Vissza a főoldalra
        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            SetupStartPage(); // start pnl visszatöltése
            btnBack.Visible = false;
        }
    }
}
