using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class ViewUser
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainpanel = new Guna2ShadowPanel();
            lblHeader = new Label();

            lblEmployee = new Label();
            lblEmployeeValue = new Label();

            lblRole = new Label();
            lblRoleValue = new Label();

            lblUsername = new Label();
            lblUsernameValue = new Label();

            lblEmail = new Label();
            lblEmailValue = new Label();

            btnClose = new Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.AddRange(new Control[] {
                lblHeader,
                lblEmployee, lblEmployeeValue,
                lblRole, lblRoleValue,
                lblUsername, lblUsernameValue,
                lblEmail, lblEmailValue,
                btnClose
            });

            // Header
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);
            lblHeader.Location = new Point(77, 40);
            lblHeader.Text = "View User";

            // Employee
            lblEmployee.AutoSize = true;
            lblEmployee.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblEmployee.ForeColor = Color.Black;
            lblEmployee.Location = new Point(77, 130);
            lblEmployee.Text = "Employee";

            lblEmployeeValue.AutoSize = true;
            lblEmployeeValue.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblEmployeeValue.ForeColor = Color.Black;
            lblEmployeeValue.Location = new Point(77, 160);
            lblEmployeeValue.Text = "";

            // Role
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblRole.ForeColor = Color.Black;
            lblRole.Location = new Point(77, 230);
            lblRole.Text = "Role";

            lblRoleValue.AutoSize = true;
            lblRoleValue.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblRoleValue.ForeColor = Color.Black;
            lblRoleValue.Location = new Point(77, 260);
            lblRoleValue.Text = "";

            // Username
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblUsername.ForeColor = Color.Black;
            lblUsername.Location = new Point(750, 130);
            lblUsername.Text = "Username";

            lblUsernameValue.AutoSize = true;
            lblUsernameValue.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblUsernameValue.ForeColor = Color.Black;
            lblUsernameValue.Location = new Point(750, 160);
            lblUsernameValue.Text = "";

            // Email
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblEmail.ForeColor = Color.Black;
            lblEmail.Location = new Point(750, 230);
            lblEmail.Text = "Email";

            lblEmailValue.AutoSize = true;
            lblEmailValue.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblEmailValue.ForeColor = Color.Black;
            lblEmailValue.Location = new Point(750, 260);
            lblEmailValue.Text = "";

            // Close Button
            btnClose.Location = new Point(1370, 780);
            btnClose.Size = new Size(160, 50);
            btnClose.Text = "Close";
            btnClose.FillColor = Color.FromArgb(0, 123, 255);
            btnClose.ForeColor = Color.White;
            btnClose.BorderRadius = 8;
            btnClose.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnClose.Click += btnClose_Click;

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "ViewUser";
            this.Text = "View User";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblEmployee, lblRole, lblUsername, lblEmail;
        private Label lblEmployeeValue, lblRoleValue, lblUsernameValue, lblEmailValue;
        private Guna2Button btnClose;
    }
}