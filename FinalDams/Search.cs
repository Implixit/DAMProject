using FinalDams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dams
{
    public partial class Search : Form
    {
        public User LoggedInUser { get; set; }
        Context _context = new Context();
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }
        public void search()
        {
            foreach (var i in _context.Documents.Include("AssetType").Include("MetaDataValues").Where
                (x => x.AssetType.ACL.AccessLevel == LoggedInUser.ACL.AccessLevel | LoggedInUser.ACL.AccessLevel == 10 & x.AssetType.Name.Contains(textBox1.Text) |
                x.AssetType.ACL.AccessLevel == LoggedInUser.ACL.AccessLevel | LoggedInUser.ACL.AccessLevel == 10 & x.Path.Contains(textBox1.Text) |
                x.AssetType.ACL.AccessLevel == LoggedInUser.ACL.AccessLevel | LoggedInUser.ACL.AccessLevel == 10 & x.UploadDate.ToString().Contains(textBox1.Text)))
            {
                Data firstname = _context.Data.Where(x => x.MetaType.FieldName == "First Name" & x.Document.ID == i.ID).FirstOrDefault();
                Data lastname = _context.Data.Where(x => x.MetaType.FieldName == "Last Name" & x.Document.ID == i.ID).FirstOrDefault();
                listBox1.Items.Add($"Name: {i.Path} Asset Type:{i.AssetType.Name}, {firstname.MetaValue}, {lastname.MetaValue}");
            }
        }
        private void Search_Load(object sender, EventArgs e)
        {

            foreach (var assettype in _context.Types.Include("ACL").Include("MetaDataTypes").Include("Documents"))
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
            //listBox1.Items.Clear();
            //foreach (var i in _context.Documents.Include("AssetType").Include("MetaDataValues").Where(x => x.AssetType.ACL.AccessLevel == LoggedInUser.ACL.AccessLevel | LoggedInUser.ACL.AccessLevel == 10))
            //{
            //    text = $"Name: {i.Path} Asset Type:{i.AssetType.Name}";
            //    listBox1.Items.Add(text);
            //}
        }
    }
}
