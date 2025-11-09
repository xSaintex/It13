// AddCategory.cs
using System;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();

            // Initialize fields
            txtName.Text = "";
            txtStatus.Text = "Active";
            datePicker.Value = DateTime.Today;  // Default: today
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDate = datePicker.Value.ToString("MM/dd/yyyy");

            MessageBox.Show($"Category added successfully!\n" +
                          $"Name: {txtName.Text}\n" +
                          $"Date: {selectedDate}",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReturnToList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToList();
        }

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Product Categories";
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(new ProductCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            });
            parent.pnlContent.Controls[0].Show();
        }
    }
}