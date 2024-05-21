using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentalClinicManagementWinForm
{
    public partial class Appointment : Form
    {
        public Appointment()
        {
            InitializeComponent();
        }

        ConnectionString MyCon = new ConnectionString();

        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select PatName from PatientTbl ORDER BY PatName;", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatName", typeof(string));
            dt.Load(dr);
            PatientCb.ValueMember = "PatName";
            PatientCb.DataSource = dt;

            Con.Close();
        }
        private void fillTreatment()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TreatName from TreatmentTbl", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TreatName", typeof(string));
            dt.Load(dr);
            TreatmentCb.ValueMember = "TreatName";
            TreatmentCb.DataSource = dt;
            Con.Close();
        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            fillPatient();
            fillTreatment();
            populate();
        }

        void filter()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from AppointmentTbl where Patient like N'%" + SearchTb.Text + "%'";

            DataSet ds = Pat.ShowPatient(query);
            AppointmentDGV.DataSource = ds.Tables[0];

            AppointmentDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AppointmentDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AppointmentDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        void populate()
        {
            Appointments appointment = new Appointments();
            string query = "select * from AppointmentTbl";

            DataSet ds = appointment.ShowAppointment(query);
            AppointmentDGV.DataSource = ds.Tables[0];

            AppointmentDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AppointmentDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AppointmentDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (PatientCb.Text == "" || TreatmentCb.Text == "") return;

            string query = "insert into AppointmentTbl(Patient, Treatment, ApDate, ApTime) ";
            query += " SELECT N'" + PatientCb.Text + "',N'" + TreatmentCb.Text + "','" + Date.Value.ToString("yyyy-MM-dd") + "','" + Time.Value.TimeOfDay + "'";
            query += " where EXISTS ( SELECT 1 FROM PatientTbl WHERE PatName = N'" + PatientCb.Text + "')";
            query += " and EXISTS ( SELECT 1 FROM TreatmentTbl WHERE TreatName = N'" + TreatmentCb.Text + "')";
            Appointments appointment = new Appointments();
            try
            {
                appointment.RecordAppointment(query);
                //MessageBox.Show("Appointment Succesfully Recorded");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int key = 0;
        private void AppointmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatientCb.SelectedValue = AppointmentDGV.SelectedRows[0].Cells[1].Value.ToString();
            TreatmentCb.SelectedValue = AppointmentDGV.SelectedRows[0].Cells[2].Value.ToString();
            string pat = AppointmentDGV.SelectedRows[0].Cells[2].Value.ToString();

            if (pat == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AppointmentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Appointments appointment = new Appointments();
            if (key == 0)
            {
                MessageBox.Show("Select The Appointment To Cancel");
            }
            else
            {
                try
                {
                    string query = "Delete from AppointmentTbl where ApId=" + key + "";

                    appointment.DeleteAppointment(query);
                    MessageBox.Show("Appointment Succesfully Cancelled");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Appointments appointment = new Appointments();
            if (key == 0)
            {
                MessageBox.Show("Select The Appointment");
            }
            else
            {
                try
                {
                    string query = "Update AppointmentTbl set Patient = '" + PatientCb.SelectedValue.ToString() + "',Treatment='" + TreatmentCb.SelectedValue.ToString() + "',ApDate='" + Date.Value.ToString("yyyy-MM-dd") + "',ApTime='" + Time.Value.TimeOfDay + "' where ApId=" + key + " ";

                    appointment.DeleteAppointment(query);
                    MessageBox.Show("Appointment Succesfully Updated");
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

        private void label3_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Treatment treatment = new Treatment();
            treatment.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Prescription prescription = new Prescription();
            prescription.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            DashBoard dashboard = new DashBoard();
            dashboard.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
