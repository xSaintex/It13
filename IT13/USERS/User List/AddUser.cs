using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddUser : Form
    {
        private string _placeholder = "Start typing to search for an employee...";

        public AddUser()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            SetupRoleCombo();
            SetupEmployeeCombo();
            LoadEmployeeData();
        }

        private void SetupRoleCombo()
        {
            comboRole.Items.Clear();
            comboRole.Items.Add("Select a user role...");
            comboRole.Items.Add("Administrator");
            comboRole.Items.Add("Manager");
            comboRole.Items.Add("Cashier");
            comboRole.Items.Add("Staff");
            comboRole.Items.Add("IT Support");
            comboRole.SelectedIndex = 0;
        }

        private void SetupEmployeeCombo()
        {
            comboEmployee.DropDownStyle = ComboBoxStyle.DropDown;

            // DO NOT USE AutoCompleteMode/AutoCompleteSource → causes crash on Guna2ComboBox
            comboEmployee.AutoCompleteMode = AutoCompleteMode.None;
            comboEmployee.AutoCompleteSource = AutoCompleteSource.None;

            comboEmployee.Items.Clear();
            comboEmployee.Items.Add(_placeholder);
            comboEmployee.SelectedIndex = 0;
            comboEmployee.ForeColor = Color.Gray;

            // Manual filtering as you type (works perfectly )
            comboEmployee.KeyUp += ComboEmployee_KeyUp;
            comboEmployee.TextChanged += ComboEmployee_TextChanged;
            comboEmployee.GotFocus += ComboEmployee_GotFocus;
            comboEmployee.LostFocus += ComboEmployee_LostFocus;
        }

        private void LoadEmployeeData()
        {
            var employees = new[]
            {
             "Maria Johnson", "John Smith", "Emily Davis", "Michael Brown",
             "Sarah Wilson", "David Anderson", "Jessica Taylor", "Robert Thomas",
             "Olivia Martinez", "James Garcia"
         };

            foreach (var emp in employees)
                if (!comboEmployee.Items.Contains(emp))
                    comboEmployee.Items.Add(emp);
        }

        // Manual search/filter — works 100% with Guna2ComboBox
        private void ComboEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                return;

            string text = comboEmployee.Text.Trim();

            if (text == _placeholder || string.IsNullOrEmpty(text))
                return;

            comboEmployee.DroppedDown = true;

            // Save current selection
            string selected = comboEmployee.SelectedItem?.ToString();

            // Filter items
            comboEmployee.Items.Clear();
            comboEmployee.Items.Add(_placeholder);

            foreach (var item in new[] {
             "Maria Johnson", "John Smith", "Emily Davis", "Michael Brown",
             "Sarah Wilson", "David Anderson", "Jessica Taylor", "Robert Thomas",
             "Olivia Martinez", "James Garcia" })
            {
                if (item.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                    comboEmployee.Items.Add(item);
            }

            comboEmployee.Text = text;
            comboEmployee.SelectionStart = text.Length;
            comboEmployee.SelectionLength = 0;

            // Restore selection if possible
            if (!string.IsNullOrEmpty(selected) && comboEmployee.Items.Contains(selected))
                comboEmployee.SelectedItem = selected;
        }

        private void ComboEmployee_TextChanged(object sender, EventArgs e)
        {
            if (comboEmployee.Text == _placeholder || string.IsNullOrWhiteSpace(comboEmployee.Text))
                comboEmployee.ForeColor = Color.Gray;
            else
                comboEmployee.ForeColor = Color.Black;
        }

        private void ComboEmployee_GotFocus(object sender, EventArgs e)
        {
            if (comboEmployee.Text == _placeholder)
            {
                comboEmployee.Text = "";
                comboEmployee.ForeColor = Color.Black;
            }
        }

        private void ComboEmployee_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboEmployee.Text))
            {
                comboEmployee.Text = _placeholder;
                comboEmployee.ForeColor = Color.Gray;
            }
        }

        // Rest of your code (validation, save, cancel, eye icons) stays exactly the same
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnToUserList();
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToUserList();

        private bool ValidateForm()
        {
            if (comboEmployee.Text == _placeholder || string.IsNullOrWhiteSpace(comboEmployee.Text))
            {
                MessageBox.Show("Please select an employee.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (comboRole.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a role.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;
        }
    }
}