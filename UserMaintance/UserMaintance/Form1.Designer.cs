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
            lblLastName = new Label();
            lblFirstName = new Label();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // listUsers
            // 
            listUsers.FormattingEnabled = true;
            listUsers.Location = new Point(41, 112);
            listUsers.Name = "listUsers";
            listUsers.Size = new Size(518, 820);
            listUsers.TabIndex = 0;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(770, 136);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(115, 48);
            lblLastName.TabIndex = 1;
            lblLastName.Text = "label1";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(770, 309);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(115, 48);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "label2";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(981, 136);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(431, 55);
            txtLastName.TabIndex = 3;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(981, 309);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(431, 55);
            txtFirstName.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(770, 438);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(642, 69);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "button1";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1455, 1040);
            Controls.Add(btnAdd);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(lblFirstName);
            Controls.Add(lblLastName);
            Controls.Add(listUsers);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listUsers;
        private Label lblLastName;
        private Label lblFirstName;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private Button btnAdd;
    }
}
