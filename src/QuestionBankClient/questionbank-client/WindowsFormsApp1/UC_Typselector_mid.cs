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

        // Segédfüggvény a szülő eléréséhez és a váltáshoz
        private void SelectTypeAndReturn(string type, string defaultText)
        {
            // Megkeressük az UC_TypeSelector-t (picker -> pnlCenter -> UC_TypeSelector)
            var parent = this.Parent.Parent as UC_TypeSelector;

            if (parent != null)
            {
                // 1. Először visszahozzuk a kérdéslistát a középső panelbe
                parent.ShowQuestionList();

                // 2. Létrehozzuk az új kártyát a választott típussal
                // Megjegyzés: a 'CreateNewQuestionCard' metódust public-ra vagy internal-ra kell állítanunk!
                parent.HandleNewQuestionSelection(type, defaultText);
            }
        }

        private void btnShort_Click(object sender, EventArgs e)
        {
            SelectTypeAndReturn("Short", "Új rövid válaszos kérdés...");
        }

        private void btnTF_Click(object sender, EventArgs e)
        {
            SelectTypeAndReturn("tf", "Új igaz/hamis kérdés...");
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            SelectTypeAndReturn("Multi", "Új feleletválasztós kérdés...");
        }

        // Üres metódusok a hiba elkerülésére
        private void btnLong_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}