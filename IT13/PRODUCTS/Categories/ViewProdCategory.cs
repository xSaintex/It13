// ViewProdCategory.cs
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IT13
{
    public partial class ViewProdCategory : Form
    {
        private readonly string _categoryId;

        public ViewProdCategory(string categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            txtId.Text = _categoryId;
            txtName.Text = "CCTV";
            txtDate.Text = "2025-10-11";
            txtStatus.Text = "Active";
        }

        private void btnBack_Click(object sender, EventArgs e)
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