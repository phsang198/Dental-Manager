using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentalClinicManagementWinForm
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || AddressTb.Text == "" || GenCb.Text == "" || AllergyTb.Text == "") return;


            string query = "insert into PatientTbl values(N'" + PatNameTb.Text + "', '" + PatPhoneTb.Text + "',N'" + AddressTb.Text + "', '" + DOBDate.Value.ToString("yyyy-MM-dd") + "', '" + GenCb.SelectedItem.ToString() + "',N'" + AllergyTb.Text + "')";
            MyPatient Pat = new MyPatient();
            try
            {
                Pat.AddPatient(query);
                MessageBox.Show("Patient Succesfully Added");
                populate();
            }catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            
        }
        void populate()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from PatientTbl";

            DataSet ds = Pat.ShowPatient(query);
            PatientDGV.DataSource = ds.Tables[0];
            PatientDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PatientDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PatientDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        void filter()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from PatientTbl where PatName like N'%"+ SearchTb.Text +"%'";

            DataSet ds = Pat.ShowPatient(query);
            PatientDGV.DataSource = ds.Tables[0];
            PatientDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PatientDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PatientDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        } 
        
        private void Patient_Load(object sender, EventArgs e)
        {
            populate();
        }

        int key = 0;
        String id = ""; 
        private void PatientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatNameTb.Text = PatientDGV.SelectedRows[0].Cells[1].Value.ToString();
            PatPhoneTb.Text = PatientDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddressTb.Text = PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            GenCb.SelectedItem = PatientDGV.SelectedRows[0].Cells[5].Value.ToString();
            AllergyTb.Text = PatientDGV.SelectedRows[0].Cells[6].Value.ToString();
            id = PatientDGV.SelectedRows[0].Cells[0].Value.ToString();
            if(PatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PatientDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if(key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    string query = "Delete from PatientTbl where PatId=" + key + "";

                    Pat.DeletePatient(query);
                    MessageBox.Show("Patient Succesfully Deleted");
                    populate();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyPatient Pat = new MyPatient();
            if (key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    string query = "Update PatientTbl set PatName = '" + PatNameTb.Text + "',PatPhone='"+PatPhoneTb.Text+ "',PatAddress='"+AddressTb.Text+ "',PatDOB='"+DOBDate.Value.Date.ToString("yyyy-MM-dd")+ "',PatGender='"+GenCb.SelectedItem.ToString()+"',PatAllergies ='"+AllergyTb.Text+"' where PatId="+key+";";

                    Pat.DeletePatient(query);
                    MessageBox.Show("Patient Succesfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Prescription prescription = new Prescription();
            prescription.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Treatment treatment = new Treatment();
            treatment.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Show();
            this.Hide();
        }

        static void CopyFileToFolder(string sourceFilePath, string destinationFolderPath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException("File nguồn không tồn tại.", sourceFilePath);
            }

            string fileName = Path.GetFileName(sourceFilePath);

            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }

            File.Copy(sourceFilePath, destinationFilePath, true);
        }
        static string GetExeFolderPath()
        {
            try
            {
                // Lấy đường dẫn của file exe đang thực thi
                string exePath = Assembly.GetExecutingAssembly().Location;

                // Lấy thư mục chứa file exe
                string exeFolderPath = Path.GetDirectoryName(exePath);

                return exeFolderPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

                return null;
            }
        }
        String ExePath = GetExeFolderPath(); 
        private void button4_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Patient");
                return; 
            }

            try
            {
                using (OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    openFileDialog.Title = "Thêm ảnh";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
                    openFileDialog.Multiselect = true;

                    DialogResult result = openFileDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Process selected files
                        foreach (string file in openFileDialog.FileNames)
                        {
                            CopyFileToFolder(file, ExePath + "\\data\\" + id); 
                        }

                        MessageBox.Show("Thêm ảnh thành công !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        static string GetFirstFileNameInFolder(string folderPath)
        {
            try
            {
                string[] fileNames = Directory.GetFiles(folderPath);

                if (fileNames.Length > 0)
                {
                    string firstFileName = fileNames.First();
                    return firstFileName;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        static void RunCommand(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    process.StandardInput.WriteLine(command);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    string result = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select The Patient");
                return;
            }
            try
            {
                string viewerExecutable = @"C:\Program Files (x86)\QuickPictureViewer\quick-picture-viewer.exe";
                string patientPath = GetFirstFileNameInFolder(ExePath + "\\data\\" + id);
                if (patientPath == null)
                {
                    MessageBox.Show("Image folder rỗng" );
                    return; 
                }
                String cmd = "\"" + viewerExecutable + "\" \"" + patientPath + "\"";
                RunCommand(cmd); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
