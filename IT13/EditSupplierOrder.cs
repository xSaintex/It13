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
        private List<ProductRow> products = new List<ProductRow>();

        public EditSupplierOrder(string orderId)
        {
            _orderId = orderId;
            InitializeComponent();
            SetupCombos();
            LoadData();
            dateOrder.Value = DateTime.Today;
            dateEstimated.Value = DateTime.Today.AddDays(7);
            btnAddProduct.Click += (s, e) => OpenProductModal();
            numDiscount.ValueChanged += (s, e) => RecalculateTotals();
            numShipping.ValueChanged += (s, e) => RecalculateTotals();
        }

        private void SetupCombos()
        {
            cmbCompany.Items.AddRange(new[] { "Select company", "Incio", "TechCorp", "Global Supplies" });
            cmbPayment.Items.AddRange(new[] { "Select payment terms", "Net 30", "Net 15", "COD" });
        }

        private void LoadData()
        {
            txtId.Text = _orderId;
            cmbCompany.SelectedIndex = 1;
            cmbPayment.SelectedIndex = 1;
            dateOrder.Value = new DateTime(2025, 11, 10);
            dateEstimated.Value = new DateTime(2025, 11, 17);

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

        private void RecalculateTotals()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                if (r.Tag is ProductRow p) subtotal += p.Qty * p.Price;
            }
            lblSubtotalVal.Text = $"₱{subtotal:F2}";

            decimal discount = subtotal * (numDiscount.Value / 100m);
            decimal shipping = numShipping.Value;
            decimal total = subtotal - discount + shipping;

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
            if (cmbCompany.SelectedIndex <= 0 || cmbPayment.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select company and payment terms.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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