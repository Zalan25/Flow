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
        // Ez a "lelke" a kártyának: minden adat itt lakik (ID, Pont, Válaszok, stb.)
        public Question Data { get; set; } = new Question();

        public UC_QuestionCard()
        {
            InitializeComponent();
        }

        // Frissítjük a kártya kinézetét
        // A 'number' a sorszám, a 'text' pedig a megjelenítendő kérdés
        public void UpdateDisplay(int number, string displayContent)
        {
            // A 'number' a sorszám (pl. 1. kérdés)
            lblNumber.Text = number.ToString() + ". kérdés";

            // A 'displayContent' mostantól a kérdést ÉS az összefoglalót is tartalmazza
            lblText.Text = string.IsNullOrWhiteSpace(displayContent) ? "Nincs megadva tartalom..." : displayContent;

            // UI tipp: Állítsd be a lblText-et a designerben:
            // AutoSize = False
            // Anchor = Top, Left, Right, Bottom (vagy Dock = Fill egy panelen belül)
        }

        private void pnlquestioncard_Paint(object sender, PaintEventArgs e)
        {
            Control paintControl = sender as Control;
            if (paintControl == null) return;

            // Szép, sötétkék keret rajzolása (a 2 a vonalvastagságot jelenti)
            using (Pen pen = new Pen(Color.Navy, 2))
            {
                e.Graphics.DrawRectangle(pen, 1, 1, paintControl.Width - 3, paintControl.Height - 3);
            }
        }
    }
}