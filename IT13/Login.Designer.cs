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
            panelLeft = new Panel();
            lblLogo = new Label();
            panelRight = new Panel();
            linkForgot = new LinkLabel();
            chkShowPass = new CheckBox();
            chkRemember = new CheckBox();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            lblPassword = new Label();
            lblUsername = new Label();
            lblLogin = new Label();
            lblWelcome = new Label();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(10, 25, 70);
            panelLeft.Controls.Add(lblLogo);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(380, 521);
            panelLeft.TabIndex = 1;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Microsoft Sans Serif", 22F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(70, 220);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(216, 42);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "Le Parisien";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(linkForgot);
            panelRight.Controls.Add(chkShowPass);
            panelRight.Controls.Add(chkRemember);
            panelRight.Controls.Add(btnLogin);
            panelRight.Controls.Add(txtPassword);
            panelRight.Controls.Add(txtUsername);
            panelRight.Controls.Add(lblPassword);
            panelRight.Controls.Add(lblUsername);
            panelRight.Controls.Add(lblLogin);
            panelRight.Controls.Add(lblWelcome);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(380, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(504, 521);
            panelRight.TabIndex = 0;
            // 
            // linkForgot
            // 
            linkForgot.AutoSize = true;
            linkForgot.LinkColor = Color.FromArgb(0, 102, 204);
            linkForgot.Location = new Point(100, 430);
            linkForgot.Name = "linkForgot";
            linkForgot.Size = new Size(160, 20);
            linkForgot.TabIndex = 0;
            linkForgot.TabStop = true;
            linkForgot.Text = "Forgot your password?";
            linkForgot.VisitedLinkColor = Color.Purple;
            // 
            // chkShowPass
            // 
            chkShowPass.AutoSize = true;
            chkShowPass.Location = new Point(260, 320);
            chkShowPass.Name = "chkShowPass";
            chkShowPass.Size = new Size(67, 24);
            chkShowPass.TabIndex = 1;
            chkShowPass.Text = "Show";
            chkShowPass.CheckedChanged += chkShowPass_CheckedChanged;
            // 
            // chkRemember
            // 
            chkRemember.AutoSize = true;
            chkRemember.Location = new Point(100, 320);
            chkRemember.Name = "chkRemember";
            chkRemember.Size = new Size(154, 24);
            chkRemember.TabIndex = 2;
            chkRemember.Text = "Keep me signed in";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 51, 102);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(100, 370);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(280, 48);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "SIGN IN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(245, 245, 250);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(100, 270);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(245, 245, 250);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(100, 195);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 23);
            txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(100, 245);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(80, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(100, 170);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(87, 23);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Username";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogin.ForeColor = Color.FromArgb(0, 51, 102);
            lblLogin.Location = new Point(100, 110);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(251, 46);
            lblLogin.TabIndex = 6;
            lblLogin.Text = "Welcome Back";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 11F);
            lblWelcome.ForeColor = Color.FromArgb(80, 80, 80);
            lblWelcome.Location = new Point(100, 80);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(310, 25);
            lblWelcome.TabIndex = 7;
            lblWelcome.Text = "Sign in to your telecoms dashboard";
            // 
            // Login
            // 
            BackColor = Color.White;
            ClientSize = new Size(884, 521);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Le Parisien Telecoms – Secure Login";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ResumeLayout(false);
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
