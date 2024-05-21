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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        ConnectionString MyConnection = new ConnectionString();

        private void DashBoard_Load(object sender, EventArgs e)
        {
            SqlConnection Con = MyConnection.GetCon();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from appointmentTbl",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Pendinglbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from patientTbl", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Patientlbl.Text = dt1.Rows[0][0].ToString();
            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from userTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Userlbl.Text = dt2.Rows[0][0].ToString();
            SqlDataAdapter sda3 = new SqlDataAdapter("select min(ApDate) from AppointmentTbl", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Datelbl.Text = dt3.Rows[0][0].ToString();
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Show();
            this.Hide();
        }
    }
}
