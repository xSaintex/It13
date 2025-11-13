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
            this.KeyPreview = true;
            this.KeyDown += Login_KeyDown;
        }

        #region Custom Paint for Full Cover Image
        private void PicLogo_Paint(object sender, PaintEventArgs e)
        {
            if (picLogo.Image == null) return;

            var img = picLogo.Image;
            var pb = sender as PictureBox;
            var g = e.Graphics;

            // === ZOOM OUT: Use Min instead of Max ===
            float scaleX = (float)pb.Width / img.Width;
            float scaleY = (float)pb.Height / img.Height;
            float scale = Math.Min(scaleX, scaleY);  // ← ZOOM OUT (was Max)

            // Optional: Add padding to make it even more zoomed out
            float paddingFactor = 0.85f; // 1.0 = full fit, 0.85 = 15% smaller (more zoomed out)
            scale *= paddingFactor;

            float newWidth = img.Width * scale;
            float newHeight = img.Height * scale;

            float x = (pb.Width - newWidth) / 2;
            float y = (pb.Height - newHeight) / 2;

            g.DrawImage(img, x, y, newWidth, newHeight);
        }
        #endregion

        #region Event Handlers
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
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
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;  // This tells Program.cs to open Form1
                this.Close();                         // Close Login form
            }
            else
            {
                MessageBox.Show(
                    "Invalid credentials. Try admin / 1234",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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
        #endregion
    }
}