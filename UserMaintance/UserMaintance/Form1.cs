using System.ComponentModel;
using UserMaintance.Entities;

namespace UserMaintance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            // Localizáció beállítása
            lblFullName.Text = Resource1.FullName;
            btnAdd.Text = Resource1.Add;
            btnoutput.Text = Resource1.Output;
            btnDelete.Text = Resource1.Delete;

            // listbox beállítások
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";



        }

        private void btnoutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (var user in users)
                    {
                        sw.WriteLine($"{user.ID}\t{user.FullName}");
                    }
                }
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User
            {
                ID = Guid.NewGuid(),
                FullName = txtFullName.Text
            };
            users.Add(u);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var mbox = MessageBox.Show(Resource1.DeleteConformation, Resource1.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mbox == DialogResult.Yes)
            {
                if (listUsers.SelectedItem != null)
                {
                    
                        var user = (User)listUsers.SelectedItem;
                    users.Remove(user);
                }
            }
                
            
            
        }
    }
}
