using FinalDams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        string toolstriptext;
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
            //clears listbox so you don't add items already in the listbox
            listBox1.Items.Clear();
            //iterates through every document in database, that either has the same access level as the logged in user
            //or is an admin/patient. And the document has the same asset type as the one selected in combobox
            {
                foreach (var i in _context.Documents.Include("AssetType").Include("MetaDataValues").Where
                (x => x.AssetType.ACL.AccessLevel == LoggedInUser.ACL.AccessLevel & x.AssetType.Name.Contains(comboBox1.Text) | LoggedInUser.ACL.usertype == "Admin" & x.AssetType.Name.Contains(comboBox1.Text) | LoggedInUser.ACL.usertype == "Patient" & x.AssetType.Name.Contains(comboBox1.Text)))
                {
                    //finds the first name of the patient
                    Data firstname = _context.Data.Where(x => x.MetaType.FieldName == "First Name" & x.Document.ID == i.ID).FirstOrDefault();
                    //finds the first name of the patient
                    Data lastname = _context.Data.Where(x => x.MetaType.FieldName == "Last Name" & x.Document.ID == i.ID).FirstOrDefault();
                    //creates a new list of all the meta data for the document
                    List<Data> listdata = _context.Data.Where(x => x.Document.ID == i.ID).ToList();
                    //iterates through all the meta data
                    foreach (var o in listdata)
                    {
                        if (LoggedInUser.ACL.usertype != "Patient")
                        {   // searches the meta data fields for data entered
                            if (o.MetaValue.ToLower().Contains(textBox1.Text.ToLower()))
                            {
                                listBox1.Items.Add($"Name: {i.Path} Asset Type:{i.AssetType.Name} {firstname.MetaValue.Substring(0, 1)}.{lastname.MetaValue}");
                                break;
                            }
                        }
                        else
                        {   // only shows assets that are for the patient
                            if (o.MetaValue.ToLower().Contains(textBox1.Text.ToLower()) & firstname.MetaValue + lastname.MetaValue == LoggedInUser.Name)
                            {
                                listBox1.Items.Add($"Name: {i.Path} Asset Type:{i.AssetType.Name} {firstname.MetaValue.Substring(0, 1)}.{lastname.MetaValue}");
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void Search_Load(object sender, EventArgs e)
        {

            foreach (var assettype in _context.Types.Include("ACL").Include("MetaDataTypes").Include("Documents"))
            {
                if (LoggedInUser.ACL.usertype == "Admin" | LoggedInUser.ACL.usertype == "Patient")
                {
                    comboBox1.Items.Add(assettype.Name);
                }
                if (LoggedInUser.ACL.AccessLevel == assettype.ACL.AccessLevel)
                {
                    comboBox1.Items.Add(assettype.Name);
                }
            }
            search();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Search_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.LoggedInUser = LoggedInUser;
            this.Hide();
            this.Close();
            mainPage.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var document = _context.Documents.Include("MetaDataValues").Where(x => listBox1.SelectedItem.ToString().Contains(x.Path)).FirstOrDefault();
                toolstriptext = "";
                foreach (var status in document.MetaDataValues)
                {
                    toolstriptext += $"{status.MetaValue}, ";
                }
                toolStripStatusLabel1.Text = toolstriptext;

                pictureBox1.Visible = false;
                axWindowsMediaPlayer1.Visible = false;
                textBox2.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            search();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Document doc = _context.Documents.Where(x => listBox1.SelectedItem.ToString().Contains(x.Path)).FirstOrDefault();
            string openpath = $@"Assets\\{doc.Path}";
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = openpath
            };

            var fileStream = ofd.OpenFile();
            string ext = Path.GetExtension(doc.Path);
            if (ext == ".txt")
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    textBox2.Visible = true;
                    textBox2.Text = reader.ReadToEnd();
                }
            }
            else if (ext == ".png" | ext == ".jpg" | ext == ".gif")
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Image.FromFile(openpath);
            }
            else if (ext == ".mp4")
            {
                axWindowsMediaPlayer1.Visible = true;
                axWindowsMediaPlayer1.URL = openpath;
                axWindowsMediaPlayer1.settings.mute = true;
                axWindowsMediaPlayer1.uiMode = "None";
            }
        }
    }
}
