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
    public partial class UC_SingleOption_Item : UserControl
    {
        // Ezt az eseményt figyeli a szülő panel, hogy levegye a többi RadioButton-ről a jelölést
        public event EventHandler OptionSelected;

        public UC_SingleOption_Item()
        {
            InitializeComponent();

            // Ha rányomnak az X gombra, törli saját magát a listából
            btnDelete.Click += (s, e) => this.Dispose();

            // Ha rákattintanak a RadioButton-re, jelez a szülőnek
            rbCorrect.CheckedChanged += (s, e) =>
            {
                if (rbCorrect.Checked)
                {
                    OptionSelected?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public string OptionText
        {
            get => txtOption.Text;
            set => txtOption.Text = value;
        }

        public bool IsCorrect
        {
            get => rbCorrect.Checked;
            set => rbCorrect.Checked = value;
        }

        private void UC_SingleOption_Item_Load(object sender, EventArgs e)
        {

        }
    }
}
