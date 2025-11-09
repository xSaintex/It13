// EditCategory.cs
using System;
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
            txtDate.Text = "10/11/2025";
            comboStatus.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Category updated!", "Success");
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