using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.KeyPreview = true; // Allow Enter key
            this.KeyDown += Login_KeyDown;
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "1234")
            {
                MessageBox.Show(
                    "Welcome back to Le Parisien Telecoms Portal!",
                    "Login Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // new MainDashboard().Show();
                // this.Hide();
            }
            else
            {
                MessageBox.Show(
                    "Invalid credentials. Try admin / 1234",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPass.Checked;
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(0, 80, 150);
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(0, 51, 102);
        }
    }
}
