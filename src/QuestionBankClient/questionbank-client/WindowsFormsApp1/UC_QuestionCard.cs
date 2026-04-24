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
    public partial class UC_QuestionCard : UserControl
    {
        // Eltároljuk a kérdés adatait a kártyán belül is
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } // pl. "Short"
        public List<string> Answers { get; set; } = new List<string>();

        public UC_QuestionCard()
        {
            InitializeComponent();
        }

        // Függvény a kártya feliratainak frissítésére
        public void UpdateDisplay(int number, string text)
        {
            lblNumber.Text = number.ToString() + ". kérdés";
            lblText.Text = text;
        }

        private void pnlquestioncard_Paint(object sender, PaintEventArgs e)
        {

        }
        
    }
}
