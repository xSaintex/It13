using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ProdRentalModal : Form
    {
        public string SelectedProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal RentalPrice { get; private set; }
        public int AvailableQuantity { get; private set; }
        public string Subtotal => $"₱{(Quantity * RentalPrice):N2}";

        public ProdRentalModal()
        {
            InitializeComponent();
            LoadProducts();
            UpdateSubtotal();
        }

        private void LoadProducts()
        {
            var products = new[]
            {
                "LED Wall 3x5m|15000",
                "Sound System JBL Pro|25000",
                "Stage Lights Set (8pcs)|18000",
                "Projector 5000 Lumens|8000",
                "Fog Machine|5000"
            };

            foreach (var p in products)
            {
                var parts = p.Split('|');
                cmbProductName.Items.Add(new ProductItem { Name = parts[0], Price = decimal.Parse(parts[1]) });
            }

            cmbProductName.DisplayMember = "Name";
            if (cmbProductName.Items.Count > 0)
                cmbProductName.SelectedIndex = 0;
        }

        private void UpdateSubtotal()
        {
            if (int.TryParse(txtQuantity.Text, out int qty) && qty > 0 &&
                decimal.TryParse(txtRentalPrice.Text, out decimal price))
            {
                decimal total = qty * price;
                lblSubtotalValue.Text = $"₱{total:N2}";
            }
            else
            {
                lblSubtotalValue.Text = "₱0.00";
            }
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductName.SelectedItem is ProductItem item)
            {
                txtRentalPrice.Text = item.Price.ToString("N0");
                txtAvailableQty.Text = new Random().Next(5, 20).ToString();
                UpdateSubtotal();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e) => UpdateSubtotal();
        private void txtRentalPrice_TextChanged(object sender, EventArgs e) => UpdateSubtotal();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProductName.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAvailableQty.Text, out int avail) || qty > avail)
            {
                MessageBox.Show($"Only {avail} unit(s) available.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtRentalPrice.Text.Replace(",", ""), out decimal price) || price <= 0)
            {
                MessageBox.Show("Enter a valid rental price.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = (ProductItem)cmbProductName.SelectedItem;
            SelectedProductName = item.Name;
            Quantity = qty;
            RentalPrice = price;
            AvailableQuantity = avail;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    // Helper class to store product name + price
    public class ProductItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public override string ToString() => Name;
    }
}