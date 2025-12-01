using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ProdRentalModal : Form
    {
        public string SelectedProductName { get; private set; }
        public long? SelectedProductID { get; private set; }
        public int Quantity { get; private set; }
        public decimal RentalPrice { get; private set; }
        public int AvailableQuantity { get; private set; }
        public decimal SubtotalValue => Quantity * RentalPrice;
        public string Subtotal => $"₱{(Quantity * RentalPrice):N2}";

        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ProdRentalModal()
        {
            InitializeComponent();
            LoadProducts();
            UpdateSubtotal();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            p.ProdID,
                            p.ProductName,
                            p.selling_price,
                            COALESCE(SUM(si.qty), 0) as AvailableQty
                        FROM product_list p
                        LEFT JOIN stock_items si ON p.ProdID = si.ProductID AND si.Status = 'active'
                        WHERE p.Status = 'active'
                        GROUP BY p.ProdID, p.ProductName, p.selling_price
                        HAVING COALESCE(SUM(si.qty), 0) > 0
                        ORDER BY p.ProductName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbProductName.Items.Clear();
                            cmbProductName.Items.Add(new ProductItem { ProductID = 0, Name = "-- Select Product --", Price = 0, AvailableQty = 0 });

                            while (reader.Read())
                            {
                                var item = new ProductItem
                                {
                                    ProductID = Convert.ToInt64(reader["ProdID"]),
                                    Name = reader["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(reader["selling_price"]),
                                    AvailableQty = Convert.ToInt32(reader["AvailableQty"])
                                };
                                cmbProductName.Items.Add(item);
                            }
                        }
                    }
                }

                cmbProductName.DisplayMember = "Name";
                cmbProductName.ValueMember = "ProductID";

                if (cmbProductName.Items.Count > 1)
                {
                    cmbProductName.SelectedIndex = 1; // Skip the first "Select Product" item
                }
                else
                {
                    cmbProductName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleProducts();
            }
        }

        private void LoadSampleProducts()
        {
            cmbProductName.Items.Clear();
            var products = new[]
            {
                new ProductItem { ProductID = 1, Name = "LED Wall 10x20", Price = 45000, AvailableQty = 8 },
                new ProductItem { ProductID = 2, Name = "Sound System", Price = 18000, AvailableQty = 4 },
                new ProductItem { ProductID = 3, Name = "Stage Lights", Price = 25000, AvailableQty = 6 },
                new ProductItem { ProductID = 4, Name = "Projector HD", Price = 8000, AvailableQty = 12 },
                new ProductItem { ProductID = 5, Name = "Fog Machine", Price = 5000, AvailableQty = 15 }
            };

            foreach (var product in products)
            {
                cmbProductName.Items.Add(product);
            }

            if (cmbProductName.Items.Count > 0)
                cmbProductName.SelectedIndex = 0;
        }

        private void UpdateSubtotal()
        {
            try
            {
                if (int.TryParse(txtQuantity.Text, out int qty) && qty > 0 &&
                    decimal.TryParse(txtRentalPrice.Text.Replace(",", "").Replace("₱", "").Trim(), out decimal price))
                {
                    decimal total = qty * price;
                    lblSubtotalValue.Text = $"₱{total:N2}";

                    // Update border color based on quantity validation
                    if (int.TryParse(txtAvailableQty.Text, out int avail))
                    {
                        if (qty > avail)
                        {
                            txtQuantity.BorderColor = Color.Red;
                        }
                        else
                        {
                            txtQuantity.BorderColor = Color.FromArgb(200, 200, 200);
                        }
                    }
                }
                else
                {
                    lblSubtotalValue.Text = "₱0.00";
                    txtQuantity.BorderColor = Color.FromArgb(200, 200, 200);
                }
            }
            catch
            {
                lblSubtotalValue.Text = "₱0.00";
            }
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductName.SelectedItem is ProductItem item && item.ProductID > 0)
            {
                SelectedProductID = item.ProductID;
                SelectedProductName = item.Name;
                txtRentalPrice.Text = item.Price.ToString("N2");
                txtAvailableQty.Text = item.AvailableQty.ToString();

                // Reset quantity if exceeds available
                if (int.TryParse(txtQuantity.Text, out int currentQty) && currentQty > item.AvailableQty)
                {
                    txtQuantity.Text = "1";
                }

                UpdateSubtotal();
            }
            else
            {
                // Clear fields if "Select Product" is selected
                SelectedProductID = null;
                SelectedProductName = string.Empty;
                txtRentalPrice.Text = "0.00";
                txtAvailableQty.Text = "0";
                UpdateSubtotal();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            // Ensure only numbers
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                if (!int.TryParse(txtQuantity.Text, out int result) || result <= 0)
                {
                    // Remove non-numeric characters
                    string clean = string.Empty;
                    foreach (char c in txtQuantity.Text)
                    {
                        if (char.IsDigit(c))
                            clean += c;
                    }
                    txtQuantity.Text = clean;
                    txtQuantity.SelectionStart = txtQuantity.Text.Length;

                    if (string.IsNullOrEmpty(clean))
                        txtQuantity.Text = "1";
                }
            }
            else
            {
                txtQuantity.Text = "1";
            }

            UpdateSubtotal();
        }

        private void txtRentalPrice_TextChanged(object sender, EventArgs e)
        {
            // Format price as user types
            if (decimal.TryParse(txtRentalPrice.Text.Replace(",", "").Replace("₱", "").Trim(), out decimal price))
            {
                txtRentalPrice.TextChanged -= txtRentalPrice_TextChanged; // Prevent recursion
                txtRentalPrice.Text = price.ToString("N2");
                txtRentalPrice.SelectionStart = txtRentalPrice.Text.Length;
                txtRentalPrice.TextChanged += txtRentalPrice_TextChanged;
            }
            UpdateSubtotal();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validation
            if (cmbProductName.SelectedItem == null || !(cmbProductName.SelectedItem is ProductItem selectedItem))
            {
                MessageBox.Show("Please select a product.", "Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductName.Focus();
                return;
            }

            if (selectedItem.ProductID == 0) // "Select Product" item
            {
                MessageBox.Show("Please select a valid product.", "Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductName.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (minimum 1).", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }

            if (!int.TryParse(txtAvailableQty.Text, out int avail))
            {
                MessageBox.Show("Unable to determine available quantity.", "Stock Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (qty > avail)
            {
                MessageBox.Show($"Only {avail} unit(s) available. Please reduce quantity.",
                    "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }

            if (!decimal.TryParse(txtRentalPrice.Text.Replace(",", "").Replace("₱", "").Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid rental price.", "Invalid Price",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRentalPrice.Focus();
                txtRentalPrice.SelectAll();
                return;
            }

            // Set properties
            SelectedProductName = selectedItem.Name;
            SelectedProductID = selectedItem.ProductID;
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

        // Helper method for ComboBox item display
        private void cmbProductName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            var item = cmbProductName.Items[e.Index] as ProductItem;
            if (item != null)
            {
                using (var brush = new SolidBrush(e.ForeColor))
                {
                    string displayText;
                    if (item.ProductID == 0)
                    {
                        displayText = item.Name;
                        e.Graphics.DrawString(displayText, e.Font, brush, e.Bounds);
                    }
                    else
                    {
                        displayText = $"{item.Name} (₱{item.Price:N2} - Available: {item.AvailableQty})";

                        // Draw product name
                        e.Graphics.DrawString(displayText, e.Font, brush, e.Bounds);

                        // Draw available quantity in different color if low
                        if (item.AvailableQty < 5)
                        {
                            using (var lowStockBrush = new SolidBrush(Color.Red))
                            {
                                var availableText = $"Available: {item.AvailableQty}";
                                var size = e.Graphics.MeasureString(availableText, e.Font);
                                var availableRect = new RectangleF(e.Bounds.Right - size.Width - 5, e.Bounds.Top, size.Width, e.Bounds.Height);
                                e.Graphics.DrawString(availableText, e.Font, lowStockBrush, availableRect);
                            }
                        }
                    }
                }
            }

            e.DrawFocusRectangle();
        }
    }

    public class ProductItem
    {
        public long? ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableQty { get; set; }

        public override string ToString()
        {
            if (ProductID == 0)
                return Name;
            return $"{Name} (₱{Price:N2})";
        }
    }
}