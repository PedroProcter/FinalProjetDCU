using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FaceRecognition;
using Data;


namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private FaceRec faceRec = new FaceRec();
        private Label face_id = new Label();
        private TableStudentsManager tableStudentsManager = new TableStudentsManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo x in devices) {
                comboBox1.Items.Add(x.Name);
            }

            if (comboBox1.Items.Count > 1) {
                comboBox1.SelectedIndex = 0;
            }*/
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void id_textbox_Enter(object sender, EventArgs e)
        {
            if (id_textbox.Text == "" || id_textbox.Text == " 0") { 
                id_textbox.Text = "";
                id_textbox.ForeColor = Color.FromArgb(244, 244, 244);
            }
        }

        private void id_textbox_Leave(object sender, EventArgs e)
        {
            if (id_textbox.Text == "") {
                id_textbox.Text = " 0";
                id_textbox.ForeColor = Color.FromArgb(140, 140, 140);
            }
        }

        private void name_texbox_Enter(object sender, EventArgs e)
        {
            if (name_texbox.Text == "" || name_texbox.Text == " Juan")
            {
                name_texbox.Text = "";
                name_texbox.ForeColor = Color.FromArgb(244, 244, 244);
            }
        }

        private void name_texbox_Leave(object sender, EventArgs e)
        {
            if (name_texbox.Text == "")
            {
                name_texbox.Text = " Juan";
                name_texbox.ForeColor = Color.FromArgb(140, 140, 140);
            }
        }

        private void email_textbox_Enter(object sender, EventArgs e)
        {
            if (email_textbox.Text == "" || email_textbox.Text == " example@email.com")
            {
                email_textbox.Text = "";
                email_textbox.ForeColor = Color.FromArgb(244, 244, 244);
            }
        }

        private void email_textbox_Leave(object sender, EventArgs e)
        {
            if (email_textbox.Text == "")
            {
                email_textbox.Text = " example@email.com";
                email_textbox.ForeColor = Color.FromArgb(140, 140, 140);
            }
        }

        private void phone_number_textbox_Enter(object sender, EventArgs e)
        {
            if (phone_number_textbox.Text == "" || phone_number_textbox.Text == " 123-456-7890")
            {
                phone_number_textbox.Text = "";
                phone_number_textbox.ForeColor = Color.FromArgb(244, 244, 244);
            }
        }

        private void phone_number_textbox_Leave(object sender, EventArgs e)
        {
            if (phone_number_textbox.Text == "")
            {
                phone_number_textbox.Text = " 123-456-7890";
                phone_number_textbox.ForeColor = Color.FromArgb(140, 140, 140);
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            faceRec.Save_IMAGE(id_textbox.Text);
            MessageBox.Show("Save Sucessful...");
        }

        private void capture_button_Click(object sender, EventArgs e)
        {
            if (capture_button.Text == "Capture")
            {
                faceRec.openCamera(pictureBox1, new PictureBox());
                capture_button.Text = "Capturing...";
            }
            
        }

        private void find_student_button_Click(object sender, EventArgs e)
        {
            if (!faceRec.isTrained)
            {
                faceRec.isTrained = true;
                find_student_button.Text = "Stop Searching...";
                timer1.Enabled = true;
            }
            else {
                faceRec.isTrained = false;
                find_student_button.Text = "Find Student";
                timer1.Enabled = false;

                Color gray = Color.FromArgb(140, 140, 140);

                id_textbox.ForeColor = gray;
                name_texbox.ForeColor = gray;
                email_textbox.ForeColor = gray;
                phone_number_textbox.ForeColor = gray;

                id_textbox.Text = " 0";
                name_texbox.Text = " Juan";
                email_textbox.Text = " example@email.com";
                phone_number_textbox.Text = " 123-456-7890";
                classification_label.Text = "UNKNOW";
            }
        }
        
        private void register_student_button_Click(object sender, EventArgs e)
        {
            String student_id = id_textbox.Text;
            faceRec.Save_IMAGE(student_id);
            tableStudentsManager.AddStudent(student_id, name_texbox.Text, email_textbox.Text, phone_number_textbox.Text);

            MessageBox.Show("Registrated Sucessfully...");
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (faceRec.isTrained == true) {
                faceRec.getPersonName(face_id);

                DataTable dataTable = tableStudentsManager.getStudents(face_id.Text);

                classification_label.Text = "UNKNOW";
                foreach (DataRow row in dataTable.Rows)
                 {
                    Color gray = Color.FromArgb(244, 244, 244);

                    id_textbox.ForeColor = gray;
                    name_texbox.ForeColor = gray;
                    email_textbox.ForeColor = gray;
                    phone_number_textbox.ForeColor = gray;

                    id_textbox.Text = row["StudentId"].ToString();
                    name_texbox.Text = row["Name"].ToString();
                    email_textbox.Text = row["Email"].ToString();
                    phone_number_textbox.Text = row["phone_number"].ToString();
                    classification_label.Text = "STUDENT";
                 }

                if (face_id.Text == "") {
                    id_textbox.Text = "";
                    name_texbox.Text = "";
                    email_textbox.Text = "";
                    phone_number_textbox.Text = "";
                }

            }
        }
    }
}
