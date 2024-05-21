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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminLoginBtn_Click(object sender, EventArgs e)
        {
            if(AdminPass.Text == "")
            {
                MessageBox.Show("Enter The Admin Password");
            }
            else if(AdminPass.Text == "password")
            {
                User U = new User();
                U.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Password.  Contact The Admin");
            }
        }

        private void Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (AdminPass.Text == "")
                {
                    MessageBox.Show("Enter The Admin Password");
                }
                else if (AdminPass.Text == "password")
                {
                    User U = new User();
                    U.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Password.  Contact The Admin");
                }
            }    
        }
    }
}
