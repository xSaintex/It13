using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditEmployee : Form
    {
        private readonly string _employeeId;

        public EditEmployee(string employeeId)
        {
            _employeeId = employeeId;
            InitializeComponent();
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            // Simulate loading data based on ID
            txtFirstName.Text = "Maria";
            txtLastName.Text = "Johnson";
            // In real app, fetch from database using _employeeId
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            MessageBox.Show(
                $"Employee {_employeeId} has been updated successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ReturnToList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToList();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            return true;
        }

        private void ReturnToList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Employees";
            parent.pnlContent.Controls.Clear();

            var listForm = new Employees
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Add(listForm);
            listForm.Show();
        }
    }
}