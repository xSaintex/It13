// ---------------------------------------------------------------------
// EditSupplierOrder.cs
// SAME LAYOUT AS ADD | LOGIC ONLY
// ---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditSupplierOrder : Form
    {
        private readonly string _orderId;
        private readonly List<ProductRow> products = new List<ProductRow>();

        public EditSupplierOrder(string orderId)
        {
            _orderId = orderId;
            InitializeComponent();
            SetupCombos();
            SetupButtonStyles();
            LoadData();

            btnAddProduct.Click += (s, e) => OpenProductModal();
            btnSearch.Click += (s, e) => SearchProducts();
            numDiscount.ValueChanged += (s, e) => RecalculateTotals();
            numShipping.ValueChanged += (s, e) => RecalculateTotals();
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            txtPostal.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
            };
        }

        private void SetupCombos()
        {
            cmbCompany.Items.AddRange(new[] { "Select company", "Incio", "TechCorp", "Global Supplies" });
            cmbPayment.Items.AddRange(new[] { "Select payment terms", "Net 30", "Net 15", "COD" });
            cmbCountry.Items.AddRange(new[] { "Select country", "Philippines", "United States", "Canada", "Japan", "Germany" });
        }

        private void SetupButtonStyles()
        {
            var primary = Color.FromArgb(0, 123, 255);
            var danger = Color.FromArgb(220, 53, 69);
            foreach (var btn in new[] { btnAddProduct, btnSave, btnSearch })
            {
                btn.FillColor = primary;
                btn.ForeColor = Color.White;
                btn.BorderRadius = 5;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            btnCancel.FillColor = danger;
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 5;
        }

        private void LoadData()
        {
            cmbCompany.SelectedIndex = 1;
            cmbPayment.SelectedIndex = 1;
            dateOrder.Value = new DateTime(2025, 11, 10);
            dateEstimated.Value = new DateTime(2025, 11, 17);

            txtAddr1.Text = "123 Main St., Unit 456";
            txtAddr2.Text = "Bldg 7, Makati";
            txtCity.Text = "Makati City";
            txtState.Text = "Metro Manila";
            txtPostal.Text = "1229";
            cmbCountry.SelectedIndex = 1;

            var p1 = new ProductRow { Name = "HikVision Camera", Qty = 2, Price = 2000.00m, Available = 6 };
            var p2 = new ProductRow { Name = "Logitech Mouse", Qty = 1, Price = 200.00m, Available = 1 };
            products.Add(p1); products.Add(p2);
            AddProductToGrid(p1); AddProductToGrid(p2);
            RecalculateTotals();
        }

        private void OpenProductModal()
        {
            using (var modal = new SelectProductsModal())
            {
                if (modal.ShowDialog() == DialogResult.OK && modal.SelectedProducts != null)
                {
                    foreach (var p in modal.SelectedProducts)
                    {
                        products.Add(p);
                        AddProductToGrid(p);
                    }
                    RecalculateTotals();
                }
            }
        }

        private void AddProductToGrid(ProductRow p)
        {
            int i = dgvItems.Rows.Add(p.Name, p.Qty, $"₱{p.Price:F2}", p.Available, $"₱{p.Qty * p.Price:F2}");
            dgvItems.Rows[i].Tag = p;
        }

        private void SearchProducts()
        {
            string query = txtSearchProduct.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a product name to search.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Searching for: \"{query}\"", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RecalculateTotals()
        {
            decimal subtotal = 0m;
            foreach (DataGridViewRow r in dgvItems.Rows)
                if (r.Tag is ProductRow p) subtotal += p.Qty * p.Price;

            lblSubtotalVal.Text = $"₱{subtotal:F2}";
            decimal discount = subtotal * (numDiscount.Value / 100m);
            decimal total = subtotal - discount + numShipping.Value;
            lblTotalVal.Text = $"₱{total:F2}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            MessageBox.Show($"Order {_orderId} updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnToList();
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToList();

        private bool ValidateForm()
        {
            if (cmbCompany.SelectedIndex <= 0 || cmbPayment.SelectedIndex <= 0 || cmbCountry.SelectedIndex <= 0)
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAddr1.Text) || string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtState.Text) || string.IsNullOrWhiteSpace(txtPostal.Text))
            {
                MessageBox.Show("Please complete address fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Add at least one product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ReturnToList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Supplier Orders";
            var list = new SupplierOrderList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(list);
            list.Show();
        }
    }
}