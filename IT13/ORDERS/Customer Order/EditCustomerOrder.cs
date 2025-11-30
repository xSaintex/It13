// EditCustomerOrder.cs
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditCustomerOrder : Form
    {
        private readonly List<ProductRow> products = new List<ProductRow>();
        private readonly string orderId;

        public EditCustomerOrder(string orderId)
        {
            this.orderId = orderId;
            InitializeComponent();
            SetupCombos();
            SetupButtonStyles();

            btnAddProduct.Click += (s, e) => OpenProductModal();
            btnSearch.Click += (s, e) => SearchProducts();
            numDiscount.ValueChanged += (s, e) => RecalculateTotals();
            numShipping.ValueChanged += (s, e) => RecalculateTotals();
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Address click → popup input
            txtBillingAddress.Click += (s, e) => ShowAddressDialog("Billing Address", ref txtBillingAddress);
            txtShippingAddress.Click += (s, e) => ShowAddressDialog("Shipping Address", ref txtShippingAddress);
            txtBillingAddress.ReadOnly = true;
            txtShippingAddress.ReadOnly = true;
            txtBillingAddress.Cursor = Cursors.Hand;
            txtShippingAddress.Cursor = Cursors.Hand;

            LoadOrderData();
        }

        private void ShowAddressDialog(string title, ref Guna2TextBox textbox)
        {
            string current = textbox.Text.Trim();
            string result = Microsoft.VisualBasic.Interaction.InputBox(
                $"Enter {title}:\n\n(You can paste multi-line address here)",
                title,
                current,
                -1, -1);
            if (result != null)
                textbox.Text = result.Trim();
        }

        private void SetupCombos()
        {
            cmbCompany.Items.AddRange(new[] { "Select company", "TechNova Corp", "Global Retail Inc.", "Prime Distributors", "Metro Traders Ltd.", "Alpha Solutions" });
            cmbPayment.Items.AddRange(new[] { "Select payment terms", "Net 30", "Net 15", "COD", "Prepaid" });
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

        private void LoadOrderData()
        {
            // Simulate loading data for orderId (replace with real data later)
            label1.Text = $"Edit Customer Order: {orderId}";
            cmbCompany.SelectedIndex = 1;
            cmbPayment.SelectedIndex = 1;
            dateOrder.Value = DateTime.Today.AddDays(-3);
            dateEstimated.Value = DateTime.Today.AddDays(4);
            txtBillingAddress.Text = "123 Business St., Makati City\nMetro Manila, Philippines 1200";
            txtShippingAddress.Text = "456 Delivery Ave., Quezon City\nMetro Manila, Philippines 1100";

            // Sample products
            var p1 = new ProductRow { Name = "Laptop Pro 15", Qty = 2, Price = 75000m, Available = 10 };
            var p2 = new ProductRow { Name = "Wireless Mouse", Qty = 5, Price = 1500m, Available = 50 };
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
            MessageBox.Show($"Customer order {orderId} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnToList();
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToList();

        private bool ValidateForm()
        {
            if (cmbCompany.SelectedIndex <= 0 || cmbPayment.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select company and payment terms.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBillingAddress.Text))
            {
                MessageBox.Show("Please enter Billing Address.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ReturnToList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Customer Orders";
            var list = new CustomerOrderList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(list);
            list.Show();
        }
    }
}