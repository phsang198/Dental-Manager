using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagementWinForm
{
    class Appointments
    {
        public void RecordAppointment(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void DeleteAppointment(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public void UpdateAppointment(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public DataSet ShowAppointment(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = query;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}
