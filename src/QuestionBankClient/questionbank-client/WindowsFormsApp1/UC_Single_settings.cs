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
    public partial class UC_Single_settings : UserControl
    {
        public UC_Single_settings()
        {
            InitializeComponent();

            // Legördülők feltöltése a DropdownData szótárakból
            cmbLanguage.DataSource = new BindingSource(DropdownData.GetLanguages(), null);
            cmbLanguage.DisplayMember = "Value";
            cmbLanguage.ValueMember = "Key";

            cmbLevel.DataSource = new BindingSource(DropdownData.GetLevels(), null);
            cmbLevel.DisplayMember = "Value";
            cmbLevel.ValueMember = "Key";

            cmbSkill.DataSource = new BindingSource(DropdownData.GetSkills(), null);
            cmbSkill.DisplayMember = "Value";
            cmbSkill.ValueMember = "Key";

            // Bekötjük az "+ Új opció" gomb eseményét
            btnAddOption.Click += BtnAddOption_Click;
        }

        // --- PROPERTY-K AZ ADATOK KI- ÉS BEOLVASÁSÁHOZ ---
        public string QuestionText
        {
            get => txtQuestion.Text;
            set => txtQuestion.Text = value;
        }

        public string Points
        {
            get => numPoints.Value.ToString();
            set { if (decimal.TryParse(value, out decimal p)) numPoints.Value = p; }
        }

        public int SelectedLanguageId
        {
            get => cmbLanguage.SelectedValue != null ? (int)cmbLanguage.SelectedValue : 1;
            set => cmbLanguage.SelectedValue = value;
        }

        public int SelectedLevelId
        {
            get => cmbLevel.SelectedValue != null ? (int)cmbLevel.SelectedValue : 1;
            set => cmbLevel.SelectedValue = value;
        }

        public int SelectedSkillId
        {
            get => cmbSkill.SelectedValue != null ? (int)cmbSkill.SelectedValue : 1;
            set => cmbSkill.SelectedValue = value;
        }

        // --- ÚJ VÁLASZOPCIÓ HOZZÁADÁSA ---
        private void BtnAddOption_Click(object sender, EventArgs e)
        {
            var item = new UC_SingleOption_Item();
            item.Width = flpOptions.Width - 25; // -25 pixel, hogy elférjen a görgetősáv

            // OKOS CSOPORTOSÍTÁS: Ha ezt bejelölik, a többit kikapcsoljuk
            item.OptionSelected += (s, ev) =>
            {
                foreach (UC_SingleOption_Item otherItem in flpOptions.Controls.OfType<UC_SingleOption_Item>())
                {
                    if (otherItem != item)
                    {
                        otherItem.IsCorrect = false;
                    }
                }
            };

            flpOptions.Controls.Add(item);
        }

        // --- VÁLASZOK KIGYŰJTÉSE MENTÉSKOR ---
        public List<Answer> GetAnswers()
        {
            List<Answer> answers = new List<Answer>();
            int order = 1;
            foreach (UC_SingleOption_Item item in flpOptions.Controls.OfType<UC_SingleOption_Item>())
            {
                // Csak azokat mentjük, amikbe a tanár írt is szöveget
                if (!string.IsNullOrWhiteSpace(item.OptionText))
                {
                    answers.Add(new Answer
                    {
                        AnswerText = item.OptionText.Trim(),
                        IsCorrect = item.IsCorrect,
                        AnswerOrder = order++
                    });
                }
            }
            return answers;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null) parent.DeleteCurrentCard();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Megkeressük a szülő TypeSelector panelt
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null)
            {
                // Rászólunk, hogy mentse el a kártyát!
                parent.SaveCurrentCard();
                MessageBox.Show("A kérdés frissítve lett a listában!", "Sikeres mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UC_Single_settings_Load(object sender, EventArgs e)
        {

        }
    }
}