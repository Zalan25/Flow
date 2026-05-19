using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace QuestionBankClient
{
    public partial class FrmQuestionBank : Form
    {
        public List<Question> SelectedQuestions = new List<Question>();
        private List<Question> _allBankQuestions;

        public FrmQuestionBank()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                // API hívás az előbb megírt /api/quiz/bank végpontra
                _allBankQuestions = await DatabaseService.Client.GetFromJsonAsync<List<Question>>("api/quiz/bank");

                // BindingSource használata a listázáshoz
                dgvQuestions.DataSource = _allBankQuestions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a bank betöltésekor: " + ex.Message);
            }
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            // Végigmegyünk a táblázaton, és összeszedjük azokat, amiknél a 'chkSelect' pipa be van nyomva
            foreach (DataGridViewRow row in dgvQuestions.Rows)
            {
                if (Convert.ToBoolean(row.Cells["chkSelect"].Value) == true)
                {
                    SelectedQuestions.Add((Question)row.DataBoundItem);
                }
            }
            this.DialogResult = DialogResult.OK; // Jelzi a főablaknak, hogy vannak kijelölt kérdések
            this.Close();
        }
    }
}
