using System;
using System.Collections;
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
    public partial class Upload : Form
    {
        public User LoggedInUser { get; set; }
        Context _context = new Context();

        public Upload()
        {
            InitializeComponent();
        }

        private void Upload_Load(object sender, EventArgs e)
        {
            foreach(var assettype in _context.Types.Include("ACL").Include("MetaDataTypes").Include("Documents"))
            {
                if (LoggedInUser.ACL.AccessLevel == 10)
                {
                    comboBox1.Items.Add(assettype.Name);
                }
                if (LoggedInUser.ACL.AccessLevel == assettype.ACL.AccessLevel)
                {
                    comboBox1.Items.Add(assettype.Name);
                }
            }
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //make sure user selected the correct one
                FileName.Text = openFileDialog.SafeFileName;
            }
        }

        //checks if string contains letters
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x1 = 44 , y1 = 178, x2 = 159, y2 = 178;
            x1 = 30;
            y1 = 0;
            x2 = 145;
            y2 = 0;

            int c = groupBox1.Controls.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                groupBox1.Controls.Remove(groupBox1.Controls[i]);
            }
            foreach(var i in _context.Types.Include("MetaDataTypes").Where(x => x.Name == comboBox1.Text))
            {
                foreach(var z in i.MetaDataTypes)
                {
                    Label lb = new Label()
                    {
                        Location = new Point(x1, y1),
                        Text = z.FieldName,                        
                    };
                    y1 += 30;
                    groupBox1.Controls.Add(lb);
                    TextBox tb = new TextBox()
                    {
                        Location = new Point(x2, y2),
                        Size = new Size(251,28)
                    };
                    y2 += 30;
                    groupBox1.Controls.Add(tb);
                }
            }

        }
    }
}
