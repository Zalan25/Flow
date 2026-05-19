using System.Windows.Forms;

namespace QuestionBankClient
{
    partial class FrmQuestionBank
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvQuestions = new System.Windows.Forms.DataGridView();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).BeginInit();
            this.SuspendLayout();

            // --- DataGridView (A lista) ---
            this.dgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuestions.Location = new System.Drawing.Point(30, 80);
            this.dgvQuestions.Name = "dgvQuestions";
            this.dgvQuestions.RowHeadersWidth = 82;
            this.dgvQuestions.Size = new System.Drawing.Size(900, 500);
            this.dgvQuestions.TabIndex = 0;
            // CheckBox oszlop hozzáadása (ez fontos, hogy lehessen pipálni)
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "chkSelect";
            chk.HeaderText = "Kijelöl";
            this.dgvQuestions.Columns.Add(chk);

            // --- Gomb ---
            this.btnAddSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSelected.Location = new System.Drawing.Point(730, 600);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(200, 60);
            this.btnAddSelected.TabIndex = 1;
            this.btnAddSelected.Text = "Hozzáadás";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);

            // --- Cím ---
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 37);
            this.lblTitle.Text = "Kérdésbank";

            // --- Form beállítások ---
            this.ClientSize = new System.Drawing.Size(960, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddSelected);
            this.Controls.Add(this.dgvQuestions);
            this.Name = "FrmQuestionBank";
            this.Text = "Kérdésbank - Válogatás";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvQuestions;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Label lblTitle;
    }
}