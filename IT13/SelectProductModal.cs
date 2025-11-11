using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class SelectProductsModal : Form
    {
        public List<ProductRow> SelectedProducts { get; private set; } = new List<ProductRow>();

        public SelectProductsModal()
        {
            InitializeComponent();
            LoadSampleProducts();
        }

        private void LoadSampleProducts()
        {
            AddProduct("HikVision Camera", 10, 2000.00m);
            AddProduct("Logitech Mouse", 5, 200.00m);
            AddProduct("Dell Monitor", 3, 8000.00m);
        }

        private void AddProduct(string name, int available, decimal price)
        {
            int idx = dgvProducts.Rows.Add(false, name, 1, $"₱{price:F2}", available);
            dgvProducts.Rows[idx].Tag = new ProductRow { Name = name, Price = price, Available = available };
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            SelectedProducts.Clear();
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells[0].Value is bool checkedVal && checkedVal)
                {
                    var p = (ProductRow)row.Tag;
                    p.Qty = (int)row.Cells[2].Value;
                    SelectedProducts.Add(p);
                }
            }
            if (SelectedProducts.Count == 0)
            {
                MessageBox.Show("Select at least one product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}