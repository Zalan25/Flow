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

            // listbox beállítások
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

            var u = new User
            {
                FullName = txtFullName.Text,
            };
            users.Add(u);
        }
    }
}
