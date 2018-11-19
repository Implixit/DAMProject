using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalDams
{
    public partial class MainPage : Form
    {
        public User LoggedInUser { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Search searchPage = new Search();
            //searchPage.LoggedInUser = LoggedInUser;
            //this.Hide();
            //searchPage.ShowDialog();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Upload uploadPage = new Upload();
            uploadPage.LoggedInUser = LoggedInUser;
            this.Hide();
            uploadPage.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                    "Confimation", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Login loginpage = new Login();
                this.Hide();
                loginpage.ShowDialog();
                this.Close();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            if (LoggedInUser.ACL.AccessLevel == 10)
            {
                button4.Visible = true;
                button4.Enabled = true;
            }
        }
    }
}
