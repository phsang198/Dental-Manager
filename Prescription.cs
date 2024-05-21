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
    public partial class Prescription : Form
    {
        public Prescription()
        {
            InitializeComponent();
        }

        ConnectionString MyCon = new ConnectionString();

        private void fillPatient()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DISTINCT Patient from AppointmentTbl ORDER BY Patient;", Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Patient", typeof(string));
            dt.Load(dr);
            PatId.ValueMember = "Patient";
            PatId.DataSource = dt;
            Con.Close();
        }

        private void GetTreatment()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            string query = "select Treatment from AppointmentTbl where Patient= N'" + PatId.Text + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Treatment", typeof(string));
            dt.Load(dr);
            CBTreatment.DataSource = dt;
            CBTreatment.ValueMember = "Treatment";
            CBTreatment.DisplayMember = "Treatment";
            Con.Close();

        }

        private void GetPrice()
        {
            SqlConnection Con = MyCon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from TreatmentTbl where TreatName= N'" + CBTreatment.Text + "'", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TreatCostTb.Text = dr["TreatCost"].ToString();
            }
            Con.Close();
        }

        private void Prescription_Load(object sender, EventArgs e)
        {
            fillPatient();
            populate();
        }

        private void PatId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTreatment();
        }

        private void TreatmentTb_TextChanged(object sender, EventArgs e)
        {
            GetPrice();
        }

        void filter()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from PrescriptionTbl where PatName like N'%" + SearchTb.Text + "%'";

            DataSet ds = Pat.ShowPatient(query);
            PrescriptionDGV.DataSource = ds.Tables[0];
            PrescriptionDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PrescriptionDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PrescriptionDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        void populate()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from PrescriptionTbl";

            DataSet ds = Pat.ShowPatient(query);
            PrescriptionDGV.DataSource = ds.Tables[0];
            PrescriptionDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PrescriptionDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PrescriptionDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PatId.Text == "" || CBTreatment.Text == "" || TreatCostTb.Text == "" || MedicinesTb.Text == "" || QtyTb.Text == "") return;

            string query = "insert into PrescriptionTbl values(N'" + PatId.SelectedValue.ToString() + "',N'" + CBTreatment.Text + "'," + TreatCostTb.Text + ",N'"+MedicinesTb.Text+"','"+QtyTb.Text+"')";
            Prescriptions presc = new Prescriptions();
            try
            {
                presc.AddPrescription(query);
                MessageBox.Show("Prescription Succesfully Added");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int key = 0;
        private void PrescriptionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatId.SelectedValue = PrescriptionDGV.SelectedRows[0].Cells[1].Value.ToString();
            CBTreatment.Text = PrescriptionDGV.SelectedRows[0].Cells[2].Value.ToString();
            TreatCostTb.Text = PrescriptionDGV.SelectedRows[0].Cells[3].Value.ToString();
            MedicinesTb.Text = PrescriptionDGV.SelectedRows[0].Cells[4].Value.ToString();
            QtyTb.Text = PrescriptionDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CBTreatment.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PrescriptionDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Prescriptions presc = new Prescriptions();
            if (key == 0)
            {
                MessageBox.Show("Select The Prescription");
            }
            else
            {
                try
                {
                    string query = "Delete from PrescriptionTbl where PrescId=" + key + "";

                    presc.DeletePrescription(query);
                    MessageBox.Show("Prescription Succesfully Deleted");
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
        Bitmap bitmap;
        private void button3_Click(object sender, EventArgs e)
        {
            int height = PrescriptionDGV.Height;
            PrescriptionDGV.Height = PrescriptionDGV.RowCount * PrescriptionDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(PrescriptionDGV.Width, PrescriptionDGV.Height);
            PrescriptionDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, PrescriptionDGV.Width, PrescriptionDGV.Height));
            PrescriptionDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Treatment treatment = new Treatment();
            treatment.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Show();
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

        private void PatId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
