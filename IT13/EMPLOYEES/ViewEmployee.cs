using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewEmployee : Form
    {
        private readonly string _employeeId;

        public ViewEmployee(string employeeId)
        {
            _employeeId = employeeId;
            InitializeComponent();
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            txtId.Text = _employeeId;
            txtFirstName.Text = "Maria";
            txtLastName.Text = "Johnson";
            // In real app: load from DB using _employeeId
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ReturnToList();
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