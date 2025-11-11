using System;
using System.Globalization;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditCategory : Form
    {
        private readonly string _categoryId;

        public EditCategory(string categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            txtId.Text = _categoryId;
            txtName.Text = "CCTV";

            // Set date from string
            if (DateTime.TryParseExact("10/11/2025", "MM/dd/yyyy", null,
                DateTimeStyles.None, out DateTime dt))
            {
                datePicker.Value = dt;
            }
            else
            {
                datePicker.Value = DateTime.Today;
            }

            comboStatus.SelectedIndex = 0; // "Active"
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDate = datePicker.Value.ToString("MM/dd/yyyy");

            MessageBox.Show($"Category updated!\n" +
                          $"Name: {txtName.Text}\n" +
                          $"Date: {selectedDate}\n" +
                          $"Status: {comboStatus.Text}",
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