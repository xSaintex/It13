using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditUser : Form
    {
        private readonly string _userId;

        public EditUser(string userId)
        {
            _userId = userId;
            InitializeComponent();
            SetupRoleCombo();
            LoadUserData();
        }

        private void SetupRoleCombo()
        {
            comboRole.Items.Clear();
            comboRole.Items.Add("Administrator");
            comboRole.Items.Add("Manager");
            comboRole.Items.Add("Cashier");
            comboRole.Items.Add("Staff");
            comboRole.Items.Add("IT Support");
        }

        private void LoadUserData()
        {
            // SIMULATED DATA - Replace with real DB call
            lblHeader.Text = "Edit User";

            comboEmployee.Text = "Maria Johnson";
            comboEmployee.Enabled = false;  
            comboEmployee.ForeColor = Color.Black;

            comboRole.SelectedItem = "Administrator";
            txtUsername.Text = "maria.johnson";
            txtEmail.Text = "maria@company.com";

            // Password fields remain empty (optional to change)
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            MessageBox.Show("User updated successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReturnToUserList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToUserList();
        }

        private bool ValidateForm()
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Password validation (optional in EditUser)
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                if (txtPassword.Text.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return false;
                }
            }

            return true;
        }

        private void ReturnToUserList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Users";
            parent.pnlContent.Controls.Clear();

            var listForm = new UserList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Add(listForm);
            listForm.Show();
        }

        // Eye toggle
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnShowPassword.Image = txtPassword.UseSystemPasswordChar
                ? Properties.Resources.view_icon
                : Properties.Resources.view_icon;
        }

        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;
            btnShowConfirmPassword.Image = txtConfirmPassword.UseSystemPasswordChar
                ? Properties.Resources.view_icon
                : Properties.Resources.view_icon;
        }
    }
}