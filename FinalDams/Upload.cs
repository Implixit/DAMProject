using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        string FinishMessage; //message for leaving / after finish upload file
        public Upload()
        {
            InitializeComponent();
        }

        private void Upload_Load(object sender, EventArgs e)
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
            FinishMessage = "You are adding asset. Are you sure you want to leave?";
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            //Select File
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //make sure user selected the correct one
                FileName.Text = openFileDialog.SafeFileName;
            }
        }


        private void UploadButton_Click(object sender, EventArgs e)
        {
            #region Add item in confirm page + validation
            //Validation for File
            if (openFileDialog.FileName == "" | openFileDialog.FileName == null | openFileDialog.FileName == "openFileDialog1")
            {
                MessageBox.Show("Please select an asset to upload", "Error");
                return;
            }
            ////validation for File
            // if that file is exists in the folder 
            if (File.Exists(@"Assets\\" + openFileDialog.SafeFileName))
            {
                MessageBox.Show("There are exist file please upload other file.");
                return;
            }
            //need to select one of item
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Asset Type", "Error");
                return;
            }

            Confirm ConfirmInfomation = new Confirm();

            ConfirmInfomation.Filenamedata.Text = FileName.Text;

            string fileType = Path.GetExtension(openFileDialog.FileName);
            float sizea = new FileInfo(openFileDialog.FileName).Length;

            ConfirmInfomation.Filesizedata.Text = sizea + "KB";
            ConfirmInfomation.Filetypedata.Text = fileType;
            ConfirmInfomation.FileSelected.Text = openFileDialog.FileName;
            //location  1 = name of information 2 = information 
            int x1 = 125, y1 = 189, x2 = 310, y2 = 189;
            //item for controls in the groupbox1
            var Controls = groupBox1.Controls;
            foreach (Control ControlNumber in Controls)
            {

                if (ControlNumber is Label)
                {
                    Label lb = new Label()
                    {
                        Location = new Point(x1, y1),
                        Text = ControlNumber.Text,
                        Size = new Size(160, 24),
                        Font = new Font("Microsoft Sans Serif", 14)

                    };

                    y1 += 30;
                    ConfirmInfomation.Controls.Add(lb);
                }
                // informatioin user enter
                else
                {
                    Label information = new Label()
                    {
                        Location = new Point(x2, y2),
                        Text = ControlNumber.Text,
                        Font = new Font("Microsoft Sans Serif", 14)
                    };
                    y2 += 30;
                    ConfirmInfomation.Controls.Add(information);
                }
            }

            //validation for meta Tags
            foreach (Control CheckInformation in Controls)
            {
                //check textBox
                if (CheckInformation is TextBox)
                {
                    TextBox textBox = CheckInformation as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter " + CheckInformation.Name);
                        return;
                    }
                }
                // check combo box
                else if (CheckInformation is ComboBox)
                {
                    ComboBox comboBox = CheckInformation as ComboBox;
                    if (comboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please choose from " + CheckInformation.Name);
                        return;
                    }
                }
            }
            #endregion

            if (ConfirmInfomation.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;

                Document UploadFile = new Document();
                UploadFile.UploadDate = DateTime.Now.Date;
                UploadFile.UserID = _context.Users.Where(x => x.Name == LoggedInUser.Name & x.Password == LoggedInUser.Password).FirstOrDefault();
                UploadFile.AssetType = _context.Types.Where(x => x.Name == comboBox1.Text).FirstOrDefault();
                UploadFile.Path = openFileDialog.SafeFileName;
                #region CopyFile
                //create Folder
                Directory.CreateDirectory("Assets");
                //file that user choose
                string fileToCopy = openFileDialog.FileName;
                //where it store
                string destinationForFile = @"Assets\\";
                File.Copy(fileToCopy, destinationForFile + Path.GetFileName(fileToCopy));      
                #endregion
                _context.Documents.Add(UploadFile);
                _context.SaveChanges();

                //Date Box
                foreach (var i in _context.Types.Include("MetaDataTypes").Where(x => x.Name == comboBox1.Text))
                {
                    foreach (Control ControlNumber in Controls)
                    {
                        Data Uploaddata = new Data();

                        foreach (Meta z in i.MetaDataTypes.Where(x => x.FieldName == ControlNumber.Name))
                        {
                            Uploaddata.MetaType = _context.Meta.Where(q => q.FieldName == z.FieldName).FirstOrDefault();
                            Uploaddata.Document = _context.Documents.Where(x => x.ID == UploadFile.ID).FirstOrDefault();
                            Uploaddata.MetaValue = ControlNumber.Text;
                            _context.Data.Add(Uploaddata);
                        }
                    }
                }
                _context.SaveChanges();
                
                FinishMessage = "Asset added to the database successfully. Do you want to go home? YES back to Main Page / No add another Asset";
                this.Close();
            }

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //location for new label, textbox
            int x1 = 30, y1 = 0, x2 = 145, y2 = 0;


            int c = groupBox1.Controls.Count;
            //remove every things before add anythings
            for (int i = c - 1; i >= 0; i--)
            {
                groupBox1.Controls.Remove(groupBox1.Controls[i]);
            }
            #region Add information box 
            foreach (var i in _context.Types.Include("MetaDataTypes").Where(x => x.Name == comboBox1.Text))
            {
                foreach (var z in i.MetaDataTypes)
                {

                    Label lb = new Label()
                    {
                        Location = new Point(x1, y1),
                        Text = z.FieldName,
                    };
                    if (lb.Text != "Date")
                    {
                        y1 += 30;
                        groupBox1.Controls.Add(lb);
                    }
                    if (z.FieldType == "string" | z.FieldType == "int")
                    {
                        TextBox tb = new TextBox()
                        {
                            Location = new Point(x2, y2),
                            Name = z.FieldName,
                            Size = new Size(251, 28),
                        };
                        if (tb.Name == "NHI Number")
                        {
                            //make sure only number in the nhi number
                            tb.KeyPress += NHINumberBox_KeyPress;
                        }
                        groupBox1.Controls.Add(tb);
                    }
                    else if (z.FieldType == "bool")
                    {
                        ComboBox tb = new ComboBox()
                        {
                            Location = new Point(x2, y2),
                            Name = z.FieldName,
                            Size = new Size(251, 28),
                            DropDownStyle = ComboBoxStyle.DropDownList,
                        };
                        tb.Items.Add("True");
                        tb.Items.Add("False");
                        groupBox1.Controls.Add(tb);
                    }
                    else
                    {
                        DateTimePicker tb = new DateTimePicker()
                        {
                            //Show only day of today.
                            Format = DateTimePickerFormat.Short,
                            Location = new Point(x2, y2),
                            Size = new Size(251, 28),
                            Name = z.FieldName

                        };
                        if (lb.Text != "Date")
                        {
                            groupBox1.Controls.Add(tb);
                        }
                    }
                    if (lb.Text != "Date")
                    {
                        y2 += 30;
                    }
                }
            }
            #endregion
        }
        private void NHINumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only number in the number Box
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Number Only");
            }
        }

        private void Upload_FormClosed(object sender, FormClosedEventArgs e)
        {
            #region Closeing Window
            //Ask User after Upload file
            DialogResult result = MessageBox.Show(FinishMessage,
                "Confimation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //Go back to main Page
                MainPage mainPage = new MainPage();
                mainPage.LoggedInUser = LoggedInUser;
                this.Hide();
                mainPage.ShowDialog();
                this.Close();
            }
            else
            {
                //FileName.Text = "File Name";
                Upload uploadPage = new Upload();
                uploadPage.LoggedInUser = LoggedInUser;
                this.Hide();
                uploadPage.ShowDialog();
                this.Close();
                //openFileDialog.Reset();

            }
            #endregion
        }
    }
    }