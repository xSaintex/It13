using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewUser : Form
    {
        public ViewUser(string userId)
        {
            InitializeComponent();
            LoadUserData(userId);
        }

        private void LoadUserData(string userId)
        {
            lblHeader.Text = "View User";

            // Simulated data
            lblEmployeeValue.Text = "Maria Johnson";
            lblRoleValue.Text = "Administrator";
            lblUsernameValue.Text = "maria.johnson";
            lblEmailValue.Text = "maria@company.com";
        }

        private void btnClose_Click(object sender, EventArgs e)
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
    }
}