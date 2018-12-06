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
    public partial class NewUser : Form
    {
        public User LoggedInUser { get; set; }
        Context _context = new Context();
        public NewUser()
        {
            InitializeComponent();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            foreach (var accessLvll in _context.Access)
            {
                comboBox1.Items.Add(accessLvll.AccessLevel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User AddUser = new User();
            AddUser.Name = textBox3.Text;
            AddUser.Password = Crypto.Hash(textBox4.Text);
            Access AddAccess = new Access()
            {
                AccessLevel = Convert.ToInt32(comboBox1.SelectedItem)
            };
            AddUser.ACL = AddAccess;
            _context.Users.Add(AddUser);
            _context.SaveChanges();

            //Login
        }
    }
}
