using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            // Simulate adding employee
            MessageBox.Show(
                $"Employee {txtFirstName.Text.Trim()} {txtLastName.Text.Trim()} has been added successfully!",
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