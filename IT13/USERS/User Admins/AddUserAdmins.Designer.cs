using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class AddUserAdmins
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainpanel = new Guna2ShadowPanel();
            lblHeader = new Label();
            lblRequired = new Label();
            lblEmployee = new Label();
            comboEmployee = new Guna2ComboBox();
            lblRole = new Label();
            comboRole = new Guna2ComboBox();
            lblUsername = new Label();
            txtUsername = new Guna2TextBox();
            lblEmail = new Label();
            txtEmail = new Guna2TextBox();
            lblPassword = new Label();
            txtPassword = new Guna2TextBox();
            btnShowPassword = new Guna2Button();
            lblPasswordHint = new Label();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new Guna2TextBox();
            btnShowConfirmPassword = new Guna2Button();
            btnCancel = new Guna2Button();
            btnSave = new Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.AddRange(new Control[] {
                lblHeader, lblRequired,
                lblEmployee, comboEmployee,
                lblRole, comboRole,
                lblUsername, txtUsername,
                lblEmail, txtEmail,
                lblPassword, txtPassword, btnShowPassword, lblPasswordHint,
                lblConfirmPassword, txtConfirmPassword, btnShowConfirmPassword,
                btnCancel, btnSave
            });

            // === HEADER ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "Create User";

            // === REQUIRED TEXT ===
            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Tahoma", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === EMPLOYEE ===
            lblEmployee.AutoSize = true;
            lblEmployee.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblEmployee.ForeColor = Color.Black;
            lblEmployee.Location = new Point(77, 110);
            lblEmployee.Text = "Employee *";

            comboEmployee.Location = new Point(77, 140);
            comboEmployee.Size = new Size(1275, 45);
            comboEmployee.DropDownStyle = ComboBoxStyle.DropDown;
            comboEmployee.BorderRadius = 8;
            comboEmployee.BorderColor = Color.FromArgb(200, 200, 200);
            comboEmployee.FillColor = Color.White;
            comboEmployee.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboEmployee.ForeColor = Color.Black;

            // === ROLE ===
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblRole.ForeColor = Color.Black;
            lblRole.Location = new Point(77, 210);
            lblRole.Text = "Role *";

            comboRole.Location = new Point(77, 240);
            comboRole.Size = new Size(600, 45);
            comboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRole.BorderRadius = 8;
            comboRole.BorderColor = Color.FromArgb(200, 200, 200);
            comboRole.FillColor = Color.White;
            comboRole.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboRole.ForeColor = Color.Black;

            // === USERNAME ===
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblUsername.ForeColor = Color.Black;
            lblUsername.Location = new Point(750, 210);
            lblUsername.Text = "Username *";

            txtUsername.Location = new Point(750, 240);
            txtUsername.Size = new Size(600, 48);
            txtUsername.BorderRadius = 8;
            txtUsername.BorderColor = Color.FromArgb(200, 200, 200);
            txtUsername.FillColor = Color.White;
            txtUsername.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtUsername.PlaceholderText = "Enter username (e.g., johndoe123)";
            txtUsername.PlaceholderForeColor = Color.Gray;
            txtUsername.ForeColor = Color.Black;
            txtUsername.MaxLength = 20;

            // === EMAIL ===
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblEmail.ForeColor = Color.Black;
            lblEmail.Location = new Point(77, 310);
            lblEmail.Text = "Email *";

            txtEmail.Location = new Point(77, 340);
            txtEmail.Size = new Size(600, 48);
            txtEmail.BorderRadius = 8;
            txtEmail.BorderColor = Color.FromArgb(200, 200, 200);
            txtEmail.FillColor = Color.White;
            txtEmail.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtEmail.PlaceholderText = "Enter email address (e.g., john@example.com)";
            txtEmail.PlaceholderForeColor = Color.Gray;
            txtEmail.ForeColor = Color.Black;

            // === PASSWORD ===
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblPassword.ForeColor = Color.Black;
            lblPassword.Location = new Point(750, 310);
            lblPassword.Text = "Password *";

            txtPassword.Location = new Point(750, 340);
            txtPassword.Size = new Size(550, 48);
            txtPassword.BorderRadius = 8;
            txtPassword.BorderColor = Color.FromArgb(200, 200, 200);
            txtPassword.FillColor = Color.White;
            txtPassword.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtPassword.PlaceholderText = "Enter a secure password";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.ForeColor = Color.Black;
            txtPassword.MaxLength = 8;

            btnShowPassword.Location = new Point(1300, 345);
            btnShowPassword.Size = new Size(40, 38);
            btnShowPassword.Image = Properties.Resources.view_icon;
            btnShowPassword.FillColor = Color.Transparent;
            btnShowPassword.BorderRadius = 8;
            btnShowPassword.Click += btnShowPassword_Click;

            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new Font("Tahoma", 9F);
            lblPasswordHint.ForeColor = Color.Gray;
            lblPasswordHint.Location = new Point(750, 395);
            lblPasswordHint.Text = "Password must be at least 8 characters";

            // === CONFIRM PASSWORD ===
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblConfirmPassword.ForeColor = Color.Black;
            lblConfirmPassword.Location = new Point(77, 450);
            lblConfirmPassword.Text = "Confirm Password *";

            txtConfirmPassword.Location = new Point(77, 480);
            txtConfirmPassword.Size = new Size(550, 48);
            txtConfirmPassword.BorderRadius = 8;
            txtConfirmPassword.BorderColor = Color.FromArgb(200, 200, 200);
            txtConfirmPassword.FillColor = Color.White;
            txtConfirmPassword.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtConfirmPassword.PlaceholderText = "Re-enter the password to confirm";
            txtConfirmPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.ForeColor = Color.Black;
            txtConfirmPassword.MaxLength = 8;

            btnShowConfirmPassword.Location = new Point(627, 485);
            btnShowConfirmPassword.Size = new Size(40, 38);
            btnShowConfirmPassword.Image = Properties.Resources.view_icon;
            btnShowConfirmPassword.FillColor = Color.Transparent;
            btnShowConfirmPassword.BorderRadius = 8;
            btnShowConfirmPassword.Click += btnShowConfirmPassword_Click;

            // === BUTTONS ===
            btnCancel.Location = new Point(1150, 780);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.Click += btnCancel_Click;

            btnSave.Location = new Point(1300, 780);
            btnSave.Size = new Size(160, 50);
            btnSave.Text = "Save";
            btnSave.FillColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.BorderRadius = 8;
            btnSave.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnSave.Click += btnSave_Click;

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "AddUser";
            this.Text = "Create User";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader, lblRequired, lblPasswordHint;
        private Label lblEmployee, lblRole, lblUsername, lblEmail, lblPassword, lblConfirmPassword;
        private Guna2ComboBox comboEmployee, comboRole;
        private Guna2TextBox txtUsername, txtEmail, txtPassword, txtConfirmPassword;
        private Guna2Button btnShowPassword, btnShowConfirmPassword;
        private Guna2Button btnCancel, btnSave;
    }
}