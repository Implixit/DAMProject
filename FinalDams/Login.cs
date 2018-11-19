using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace FinalDams
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = check_login();

            if (user != null)
            {
                MainPage mainPage = new MainPage();
                mainPage.LoggedInUser = user;
                this.Hide();
                mainPage.ShowDialog();
                this.Close();
            }
        }

        private User check_login()
        {
            string username = userTB.Text;
            string password = passTB.Text;

            using (Context _context = new Context())
            {

                User u = _context.Users.Include("ACL").FirstOrDefault(x => x.Name == username);

                if (u != null)
                {
                    var decrypt = Crypto.VerifyHashedPassword(u.Password, password);
                    if (decrypt)
                    {
                        return u;
                    }
                    else
                    {
                        MessageBox.Show("Password incorrect");
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
