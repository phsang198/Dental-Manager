using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentalClinicManagementWinForm
{
    public partial class Treatment : Form
    {
        public Treatment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TreatNameTb.Text == "" || TreatCost.Text == "" || TreatDesc.Text == "") return;

            string query = "insert into TreatmentTbl values(N'" + TreatNameTb.Text + "', " + TreatCost.Text + ",N'" + TreatDesc.Text + "')";
            Treatments Treat = new Treatments();
            try
            {
                Treat.AddTreatment(query);
                MessageBox.Show("Treatment Succesfully Added");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        void filter()
        {
            MyPatient Pat = new MyPatient();
            string query = "select * from TreatmentTbl where TreatName like N'%" + SearchTb.Text + "%'";

            DataSet ds = Pat.ShowPatient(query);
            TreatmentDGV.DataSource = ds.Tables[0];
            TreatmentDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TreatmentDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TreatmentDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        void populate()
        {
            Treatments Treat = new Treatments();
            string query = "select * from TreatmentTbl";

            DataSet ds = Treat.ShowTreatment(query);
            TreatmentDGV.DataSource = ds.Tables[0];
            TreatmentDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TreatmentDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TreatmentDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void Treatment_Load(object sender, EventArgs e)
        {
            populate();
        }

        int key = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            Treatments Treat = new Treatments();
            if (key == 0)
            {
                MessageBox.Show("Select The Treatment");
            }
            else
            {
                try
                {
                    string query = "Update TreatmentTbl set TreatName = '" + TreatNameTb.Text + "',TreatCost=" + TreatCost.Text + ",TreatDesc='" + TreatDesc.Text + "' where TreatId=" + key + " ";

                    Treat.DeleteTreatment(query);
                    MessageBox.Show("Treatment Succesfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void TreatmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TreatNameTb.Text = TreatmentDGV.SelectedRows[0].Cells[1].Value.ToString();
            TreatCost.Text = TreatmentDGV.SelectedRows[0].Cells[2].Value.ToString();
            TreatDesc.Text = TreatmentDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (TreatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TreatmentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Treatments Treat = new Treatments();
            if (key == 0)
            {
                MessageBox.Show("Select The Treatment");
            }
            else
            {
                try
                {
                    string query = "Delete from TreatmentTbl where TreatId=" + key + "";

                    Treat.DeleteTreatment(query);
                    MessageBox.Show("Treatment Succesfully Deleted");
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

        private void label4_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Show();
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
