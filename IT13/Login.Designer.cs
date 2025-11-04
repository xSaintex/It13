using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelLeft = new Panel();
            this.lblLogo = new Label();
            this.panelRight = new Panel();
            this.lblWelcome = new Label();
            this.lblLogin = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.chkShowPass = new CheckBox();
            this.chkRemember = new CheckBox();
            this.btnLogin = new Button();
            this.linkForgot = new LinkLabel();

            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();

            // ============== LEFT PANEL (BRANDING) ==============
            this.panelLeft.BackColor = Color.FromArgb(10, 25, 70);
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 380;
            this.panelLeft.Controls.Add(this.lblLogo);

            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new Font("Montserrat", 22F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Location = new Point(70, 220);
            this.lblLogo.Text = "Le Parisien";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // ============== RIGHT PANEL (LOGIN FORM) ==============
            this.panelRight.BackColor = Color.White;
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.Controls.Add(this.linkForgot);
            this.panelRight.Controls.Add(this.chkShowPass);
            this.panelRight.Controls.Add(this.chkRemember);
            this.panelRight.Controls.Add(this.btnLogin);
            this.panelRight.Controls.Add(this.txtPassword);
            this.panelRight.Controls.Add(this.txtUsername);
            this.panelRight.Controls.Add(this.lblPassword);
            this.panelRight.Controls.Add(this.lblUsername);
            this.panelRight.Controls.Add(this.lblLogin);
            this.panelRight.Controls.Add(this.lblWelcome);

            // Welcome & Title
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblWelcome.ForeColor = Color.FromArgb(80, 80, 80);
            this.lblWelcome.Location = new Point(100, 80);
            this.lblWelcome.Text = "Sign in to your telecoms dashboard";

            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblLogin.ForeColor = Color.FromArgb(0, 51, 102);
            this.lblLogin.Location = new Point(100, 110);
            this.lblLogin.Text = "Welcome Back";

            // Username
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F);
            this.lblUsername.Location = new Point(100, 170);
            this.lblUsername.Text = "Username";

            this.txtUsername.BorderStyle = BorderStyle.None;
            this.txtUsername.BackColor = Color.FromArgb(245, 245, 250);
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(100, 195);
            this.txtUsername.Size = new Size(280, 24);
            this.txtUsername.TabIndex = 0;

            // Password
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.Location = new Point(100, 245);
            this.lblPassword.Text = "Password";

            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.BackColor = Color.FromArgb(245, 245, 250);
            this.txtPassword.Font = new Font("Segoe UI", 10F);
            this.txtPassword.Location = new Point(100, 270);
            this.txtPassword.Size = new Size(280, 24);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TabIndex = 1;

            // Checkboxes
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new Point(100, 320);
            this.chkRemember.Text = "Keep me signed in";

            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new Point(260, 320);
            this.chkShowPass.Text = "Show";
            this.chkShowPass.CheckedChanged += chkShowPass_CheckedChanged;

            // Login Button
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.BackColor = Color.FromArgb(0, 51, 102);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnLogin.Location = new Point(100, 370);
            this.btnLogin.Size = new Size(280, 48);
            this.btnLogin.Text = "SIGN IN";
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.MouseEnter += btnLogin_MouseEnter;
            this.btnLogin.MouseLeave += btnLogin_MouseLeave;
            this.btnLogin.Click += btnLogin_Click;

            // Forgot Password
            this.linkForgot.AutoSize = true;
            this.linkForgot.Location = new Point(100, 430);
            this.linkForgot.Text = "Forgot your password?";
            this.linkForgot.LinkColor = Color.FromArgb(0, 102, 204);
            this.linkForgot.VisitedLinkColor = Color.Purple;

            // ============== FORM SETTINGS ==============
            this.ClientSize = new Size(880, 520);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Le Parisien Telecoms – Secure Login";
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);
            this.MaximizeBox = false;

            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private Panel panelRight;
        private Label lblLogo;
        private Label lblWelcome;
        private Label lblLogin;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkRemember;
        private CheckBox chkShowPass;
        private Button btnLogin;
        private LinkLabel linkForgot;
    }
}
