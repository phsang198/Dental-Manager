using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DentalClinicManagementWinForm
{
    class MyPatient
    {
        public void AddPatient(string query)
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
        public void DeletePatient(string query)
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
        public void UpdatePatient(string query)
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
        public DataSet ShowPatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection Con = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con ; 
            cmd.CommandText = query;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}
