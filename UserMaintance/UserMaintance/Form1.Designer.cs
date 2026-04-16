namespace UserMaintance
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listUsers = new ListBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            btnAdd = new Button();
            btnoutput = new Button();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // listUsers
            // 
            listUsers.FormattingEnabled = true;
            listUsers.ItemHeight = 15;
            listUsers.Location = new Point(14, 35);
            listUsers.Margin = new Padding(1);
            listUsers.Name = "listUsers";
            listUsers.Size = new Size(184, 259);
            listUsers.TabIndex = 0;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(270, 42);
            lblFullName.Margin = new Padding(1, 0, 1, 0);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(38, 15);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "label1";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(343, 42);
            txtFullName.Margin = new Padding(1);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(153, 23);
            txtFullName.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(270, 137);
            btnAdd.Margin = new Padding(1);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(225, 22);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "button1";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnoutput
            // 
            btnoutput.Location = new Point(397, 267);
            btnoutput.Name = "btnoutput";
            btnoutput.Size = new Size(75, 23);
            btnoutput.TabIndex = 6;
            btnoutput.Text = "button1";
            btnoutput.UseVisualStyleBackColor = true;
            btnoutput.Click += btnoutput_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(238, 271);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "button1";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 325);
            Controls.Add(btnDelete);
            Controls.Add(btnoutput);
            Controls.Add(btnAdd);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(listUsers);
            Margin = new Padding(1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listUsers;
        private Label lblFullName;
        private TextBox txtFullName;
        private Button btnAdd;
        private Button btnoutput;
        private Button btnDelete;
    }
}
