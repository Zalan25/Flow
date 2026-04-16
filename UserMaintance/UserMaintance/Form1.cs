namespace UserMaintance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblFirstName.Text = Resource1.FirstName;
            lblLastName.Text = Resource1.LastName;
            btn.Add.Text = Resource1.Add;
        }
    }
}
