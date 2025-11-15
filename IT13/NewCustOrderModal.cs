using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class NewCustOrderModal : Form
    {
        // Property to hold selected products
        public List<OrderProduct> SelectedProducts { get; private set; }

        public NewCustOrderModal()
        {
            InitializeComponent();

            // Wire up event handlers
            btnCancel.Click += BtnCancel_Click;
            btnAddSelected.Click += BtnAddSelected_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Initialize the list
            SelectedProducts = new List<OrderProduct>();

            // Load sample products
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Add sample products to the DataGridView
            // Replace this with your actual database query
            dgvProducts.Rows.Add(false, "Product A", "1", "150.00", "50");
            dgvProducts.Rows.Add(false, "Product B", "1", "200.00", "30");
            dgvProducts.Rows.Add(false, "Product C", "1", "100.00", "100");
            dgvProducts.Rows.Add(false, "Product D", "1", "350.00", "20");
            dgvProducts.Rows.Add(false, "Product E", "1", "175.00", "75");
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.IsNewRow) continue;

                string productName = row.Cells["ProductName"].Value?.ToString().ToLower() ?? "";
                row.Visible = productName.Contains(searchText);
            }
        }

        private void BtnAddSelected_Click(object sender, EventArgs e)
        {
            // Clear previous selections
            SelectedProducts.Clear();

            // Check if any products are selected
            bool hasSelection = false;

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.IsNewRow) continue;

                // Check if the checkbox is checked
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value ?? false);

                if (isSelected)
                {
                    hasSelection = true;

                    // Validate quantity
                    int quantity;
                    if (!int.TryParse(row.Cells["QTY"].Value?.ToString(), out quantity) || quantity <= 0)
                    {
                        MessageBox.Show($"Please enter a valid quantity for {row.Cells["ProductName"].Value}",
                            "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if quantity exceeds available stock
                    int available = int.Parse(row.Cells["Available"].Value.ToString());
                    if (quantity > available)
                    {
                        MessageBox.Show($"Quantity for {row.Cells["ProductName"].Value} exceeds available stock ({available})",
                            "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create product object
                    OrderProduct product = new OrderProduct
                    {
                        Name = row.Cells["ProductName"].Value.ToString(),
                        Quantity = quantity,
                        Price = decimal.Parse(row.Cells["Price"].Value.ToString()),
                        AvailableStock = available
                    };

                    SelectedProducts.Add(product);
                }
            }

            if (!hasSelection)
            {
                MessageBox.Show("Please select at least one product.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Set dialog result to OK and close
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Set DialogResult to Cancel and close the form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Helper class to hold product information
    public class OrderProduct
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
    }
}