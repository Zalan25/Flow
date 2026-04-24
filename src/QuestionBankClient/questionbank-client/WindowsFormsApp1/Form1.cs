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
            ClearAnswerList();
            pnlMainContent.Controls.Clear();
            var uc = new UC_TypeSelector { Dock = DockStyle.Fill };
            pnlMainContent.Controls.Add(uc);
            btnBack.Visible = true;
        }
        // Inside Form1.cs
        public void EnableMyButton(bool enable)
        {
            this.btnBack.Enabled = enable;
        }
        //válaszok zárolása
        public void AddCorrectAnswer(string answer)
        {
            if (!string.IsNullOrWhiteSpace(answer))
            {
                lstAnswers.Items.Add(answer);
            }
        }

        public void ClearAnswerList()
        {
            lstAnswers.Items.Clear();
        }

        public void AddAnswer(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                lsta.Items.Add(text);
            }
        }

        // Vissza a főoldalra
        public void btnBack_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();
            SetupStartPage(); // start pnl visszatöltése
            btnBack.Visible = false;
        }
    }
}
