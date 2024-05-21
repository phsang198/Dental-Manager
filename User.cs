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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        
        void populate()
        {
            Users user = new Users();
            string query = "select * from UserTbl";

            DataSet ds = user.ShowUser(query);
            UserDGV.DataSource = ds.Tables[0];
            UserDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UserDGV.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UserDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "" ) return;

            string query = "insert into UserTbl values(N'" + UnameTb.Text + "', '" + PasswordTb.Text + "','" + PhoneTb.Text + "')";
            Users user = new Users();
            try
            {
                user.AddUser(query);
                MessageBox.Show("User Succesfully Added");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            populate();
        }

        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PasswordTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            if (key == 0)
            {
                MessageBox.Show("Select The User To Delete");
            }
            else
            {
                try
                {
                    string query = "Delete from UserTbl where UId=" + key + "";

                    user.DeleteUser(query);
                    MessageBox.Show("User Succesfully Deleted");
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
            Users user = new Users();
            if (key == 0)
            {
                MessageBox.Show("Select The User");
            }
            else
            {
                try
                {
                    string query = "Update UserTbl set Uname = '" + UnameTb.Text + "',Upass='" + PasswordTb.Text + "',Phone='" + PhoneTb.Text + "' where Uid=" + key + " ";

                    user.DeleteUser(query);
                    MessageBox.Show("User Succesfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
