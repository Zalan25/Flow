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
    public partial class UC_Essay_settings : UserControl
    {
        public UC_Essay_settings()
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
        }

        // --- PROPERTY-K AZ ADATOK KI- ÉS BEOLVASÁSÁHOZ ---
        public string QuestionText
        {
            get => txtQuestion.Text;
            set => txtQuestion.Text = value;
        }

        public string SampleAnswer
        {
            get => txtSampleAnswer.Text;
            set => txtSampleAnswer.Text = value;
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

        // --- MINTA VÁLASZ KIGYŰJTÉSE MENTÉSKOR ---
        public List<Answer> GetAnswers()
        {
            List<Answer> answers = new List<Answer>();

            // Csak akkor mentünk "választ", ha a tanár írt be minta választ
            if (!string.IsNullOrWhiteSpace(SampleAnswer))
            {
                answers.Add(new Answer
                {
                    AnswerText = SampleAnswer.Trim(),
                    IsCorrect = true, // Az esszé minta válasza az adatbázisban "helyes" értéket kap
                    AnswerOrder = 1
                });
            }
            return answers;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var parent = this.Parent?.Parent as UC_TypeSelector;
            if (parent != null) parent.DeleteCurrentCard();
        }
    }
}
