using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace QuestionBankClient
{
    public static class Theme
    {
        public static readonly Color DarkBlue = Color.FromArgb(25, 52, 88);        // #193458
        public static readonly Color TextPurple = Color.FromArgb(48, 45, 91);     // #302D5B
        public static readonly Color PageBackground = Color.FromArgb(244, 247, 251);
        public static readonly Color PanelBackground = Color.FromArgb(235, 239, 246);
        public static readonly Color ActiveMenuBackground = Color.FromArgb(230, 235, 248);
        public static readonly Color BorderGray = Color.FromArgb(215, 220, 230);
        public static readonly Color DeleteRed = Color.FromArgb(190, 65, 65);

        public static readonly Font TitleFont = new Font("Segoe UI", 24F, FontStyle.Bold);
        public static readonly Font SectionTitleFont = new Font("Segoe UI", 15F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font InputFont = new Font("Segoe UI", 11F, FontStyle.Regular);

        public static void StylePrimaryButton(Button button)
        {
            button.BackColor = DarkBlue;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleSecondaryButton(Button button)
        {
            button.BackColor = Color.White;
            button.ForeColor = TextPurple;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = BorderGray;
            button.FlatAppearance.BorderSize = 1;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleDeleteButton(Button button)
        {
            button.BackColor = Color.FromArgb(255, 245, 245);
            button.ForeColor = DeleteRed;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(235, 120, 120);
            button.FlatAppearance.BorderSize = 1;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleInput(TextBox textBox)
        {
            textBox.Font = InputFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = DarkBlue;
        }

        public static void StyleCombo(ComboBox comboBox)
        {
            comboBox.Font = InputFont;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = DarkBlue;
        }
    }
}