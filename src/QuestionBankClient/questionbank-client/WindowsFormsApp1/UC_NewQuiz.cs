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
    public partial class UC_NewQuiz : UserControl
    {
        public UC_NewQuiz()
        {
            InitializeComponent();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMainContent_Paint(object sender, PaintEventArgs e)
        {

        }
        // Start -> Típusválasztó UC váltás
        // Start -> Típusválasztó UC váltás
        private void btnAddQuestions_Click(object sender, EventArgs e)
        {
            // 1. Elérjük a Form1-et
            Form1 mainForm = (Form1)this.ParentForm;

            // 2. pnlchoose: Ürítés és háttérbe küldés
            mainForm.pnlchoose.Controls.Clear();
            mainForm.pnlchoose.SendToBack();
            // mainForm.pnlchoose.Visible = false; // Opcionális: el is rejtheted teljesen

            // 3. pnlbetamain: Ürítés és háttérbe küldés (ebből tűnik el a mostani UC_NewQuiz)
            mainForm.pnlbetamain.Controls.Clear();
            mainForm.pnlbetamain.SendToBack();
            // mainForm.pnlbetamain.Visible = false;

            // 4. pnlmain: Előtérbe hozás és ürítés
            mainForm.pnlmain.BringToFront();
            mainForm.pnlmain.Controls.Clear();

            // 5. UC_TypeSelector létrehozása és ráhelyezése a pnlmain-re
            UC_TypeSelector typeSelector = new UC_TypeSelector();
            typeSelector.Dock = DockStyle.Fill;
            mainForm.pnlmain.Controls.Add(typeSelector);


        }

        // Vissza a kezdő kártyához
        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlMainContent.Controls.Clear();

            // Beállítjuk a kártyát, hogy kitöltse a helyet
            pnlStartCard.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(pnlStartCard);

            
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTestName_Click(object sender, EventArgs e)
        {

        }

        private void pnlStartCard_Paint(object sender, PaintEventArgs e)
        {

        }
        // Vissza a főoldalra
        

        private void btnFinalSave_Click(object sender, EventArgs e)
        {

        }

        private void lblMainTitle_Click(object sender, EventArgs e)
        {

        }

        // Kezdőlap visszatöltése
        private void SetupStartPage()
        {

            pnlMainContent.Controls.Add(pnlStartCard);
        }
        
        
        // Inside Form1.cs
        public void EnableMyButton(bool enable)
        {
            
        }
        

        
    }
}
