using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditSupplierOrder : Form
    {
        private readonly string _orderId;
        private readonly List<ProductRow> products = new List<ProductRow>();
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private long _orderIdNum;
        private decimal _originalSubtotal = 0m;

        public EditSupplierOrder(string orderId)
        {
            _orderId = orderId;
            // Extract numeric ID from format SO-00001
            _orderIdNum = long.Parse(orderId.Replace("SO-", ""));

            InitializeComponent();
            SetupCombos();
            SetupButtonStyles();
            LoadData();

            btnAddProduct.Click += (s, e) => OpenProductModal();
            btnSearch.Click += (s, e) => SearchProducts();
            txtSearchProduct.TextChanged += (s, e) => SearchProducts(); // Real-time search
            numDiscount.ValueChanged += (s, e) => RecalculateTotals();
            numShipping.ValueChanged += (s, e) => RecalculateTotals();
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            txtPostal.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
            };

            // Add event handler for removing products from grid
            dgvItems.CellContentClick += dgvItems_CellContentClick;
        }

        private void SetupCombos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load suppliers
                    string supplierQuery = "SELECT CompanyName FROM suppliers WHERE Status = 'active'";
                    using (SqlCommand supplierCommand = new SqlCommand(supplierQuery, connection))
                    using (SqlDataReader supplierReader = supplierCommand.ExecuteReader())
                    {
                        cmbCompany.Items.Add("Select company");
                        while (supplierReader.Read())
                        {
                            cmbCompany.Items.Add(supplierReader["CompanyName"].ToString());
                        }
                    }

                    cmbPayment.Items.AddRange(new[] { "Select payment terms", "Net 30", "Net 15", "COD", "Net 60", "Due on receipt" });
                    cmbCountry.Items.AddRange(new[] { "Select country", "Philippines", "United States", "Canada", "Japan", "Germany", "China", "Singapore" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading combo data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCompany.Items.AddRange(new[] { "Select company", "Incio", "TechCorp", "Global Supplies" });
                cmbPayment.Items.AddRange(new[] { "Select payment terms", "Net 30", "Net 15", "COD" });
                cmbCountry.Items.AddRange(new[] { "Select country", "Philippines", "United States", "Canada", "Japan", "Germany" });
            }
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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load order details
                    string orderQuery = @"
                        SELECT 
                            so.OrderDate,
                            so.EstimatedDate,
                            so.BillingAddress,
                            so.ShippingAddress,
                            so.ShippingFee,
                            so.Discount,
                            so.SubTotal,
                            s.CompanyName
                        FROM supplier_orders so
                        INNER JOIN suppliers s ON so.SupplierID = s.id
                        WHERE so.SupOrderID = @OrderID";

                    using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection))
                    {
                        orderCommand.Parameters.AddWithValue("@OrderID", _orderIdNum);
                        using (SqlDataReader reader = orderCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dateOrder.Value = Convert.ToDateTime(reader["OrderDate"]);
                                dateEstimated.Value = Convert.ToDateTime(reader["EstimatedDate"]);

                                // Parse billing address
                                string billingAddr = reader["BillingAddress"].ToString();
                                ParseAddress(billingAddr);

                                // Set company
                                string companyName = reader["CompanyName"].ToString();
                                for (int i = 0; i < cmbCompany.Items.Count; i++)
                                {
                                    if (cmbCompany.Items[i].ToString() == companyName)
                                    {
                                        cmbCompany.SelectedIndex = i;
                                        break;
                                    }
                                }

                                numShipping.Value = Convert.ToDecimal(reader["ShippingFee"]);

                                // Get original subtotal and discount
                                _originalSubtotal = Convert.ToDecimal(reader["SubTotal"]);
                                decimal discountAmount = Convert.ToDecimal(reader["Discount"]);

                                // Calculate discount percentage from discount amount and subtotal
                                if (_originalSubtotal > 0)
                                {
                                    decimal discountPercentage = (discountAmount / _originalSubtotal) * 100;
                                    numDiscount.Value = Math.Round(discountPercentage, 2);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Order {_orderId} not found in database.", "Order Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // Load order items
                    string itemsQuery = @"
                        SELECT 
                            p.ProdID,
                            p.ProductName,
                            soi.Qty,
                            soi.TotalCost
                        FROM supporderitem soi
                        INNER JOIN product_list p ON soi.ProdID = p.ProdID
                        WHERE soi.SupOrderID = @OrderID";

                    using (SqlCommand itemsCommand = new SqlCommand(itemsQuery, connection))
                    {
                        itemsCommand.Parameters.AddWithValue("@OrderID", _orderIdNum);
                        using (SqlDataReader reader = itemsCommand.ExecuteReader())
                        {
                            products.Clear();
                            dgvItems.Rows.Clear();

                            int rowCount = 0;
                            while (reader.Read())
                            {
                                string productName = reader["ProductName"].ToString();
                                int qty = Convert.ToInt32(reader["Qty"]);
                                decimal totalCost = Convert.ToDecimal(reader["TotalCost"]);
                                decimal unitPrice = qty > 0 ? totalCost / qty : 0;

                                var product = new ProductRow
                                {
                                    Name = productName,
                                    Qty = qty,
                                    Price = unitPrice,
                                    Available = qty // Default to ordered quantity since StockQty column doesn't exist
                                };

                                products.Add(product);
                                AddProductToGrid(product);
                                rowCount++;
                            }

                            if (rowCount == 0)
                            {
                                MessageBox.Show($"No items found for order {_orderId}.", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    // Set payment terms (you may want to store this in the database)
                    // For now, defaulting to Net 30
                    if (cmbPayment.Items.Count > 1)
                        cmbPayment.SelectedIndex = 1;

                    RecalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order data: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ParseAddress(string address)
        {
            try
            {
                // Address format: "addr1, addr2, city, state, postal, country"
                string[] parts = address.Split(new[] { ", " }, StringSplitOptions.None);
                if (parts.Length >= 6)
                {
                    txtAddr1.Text = parts[0];
                    txtAddr2.Text = parts[1];
                    txtCity.Text = parts[2];
                    txtState.Text = parts[3];
                    txtPostal.Text = parts[4];

                    string country = parts[5];
                    for (int i = 0; i < cmbCountry.Items.Count; i++)
                    {
                        if (cmbCountry.Items[i].ToString() == country)
                        {
                            cmbCountry.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Default values if parsing fails
                cmbCountry.SelectedIndex = 1;
            }
        }

        private void OpenProductModal()
        {
            using (var modal = new SelectProductsModal())
            {
                if (modal.ShowDialog() == DialogResult.OK && modal.SelectedProducts != null)
                {
                    foreach (var p in modal.SelectedProducts)
                    {
                        // Check if product already exists
                        bool exists = false;
                        foreach (var existingProduct in products)
                        {
                            if (existingProduct.Name == p.Name)
                            {
                                existingProduct.Qty += p.Qty;
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            products.Add(p);
                        }
                    }

                    // Refresh grid
                    dgvItems.Rows.Clear();
                    foreach (var product in products)
                    {
                        AddProductToGrid(product);
                    }

                    RecalculateTotals();
                }
            }
        }

        private void AddProductToGrid(ProductRow p)
        {
            int i = dgvItems.Rows.Add(p.Name, p.Qty, $"₱{p.Price:F2}", $"₱{p.Qty * p.Price:F2}");
            dgvItems.Rows[i].Tag = p;
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If there's a delete button column, handle it here
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var result = MessageBox.Show("Remove this product from the order?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var product = dgvItems.Rows[e.RowIndex].Tag as ProductRow;
                    if (product != null)
                    {
                        products.Remove(product);
                        dgvItems.Rows.RemoveAt(e.RowIndex);
                        RecalculateTotals();
                    }
                }
            }
        }

        private void SearchProducts()
        {
            string query = txtSearchProduct.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                // Show all products if search is empty
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            // Filter products based on search query
            bool foundAny = false;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Tag is ProductRow product)
                {
                    // Search in product name
                    bool matches = product.Name.ToLower().Contains(query);
                    row.Visible = matches;

                    if (matches)
                        foundAny = true;
                }
            }

            if (!foundAny)
            {
                MessageBox.Show($"No products found matching '{txtSearchProduct.Text}'.",
                    "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get supplier ID
                    long supplierId = GetSupplierId(cmbCompany.Text, connection);
                    if (supplierId == 0)
                    {
                        MessageBox.Show("Selected supplier not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Calculate totals
                    decimal subtotal = 0m;
                    foreach (DataGridViewRow r in dgvItems.Rows)
                        if (r.Tag is ProductRow p) subtotal += p.Qty * p.Price;

                    decimal discount = subtotal * (numDiscount.Value / 100m);
                    decimal total = subtotal - discount + numShipping.Value;

                    // Update supplier order
                    string orderQuery = @"
                        UPDATE supplier_orders SET
                            SupplierID = @SupplierID,
                            OrderDate = @OrderDate,
                            EstimatedDate = @EstimatedDate,
                            BillingAddress = @BillingAddress,
                            ShippingAddress = @ShippingAddress,
                            SubTotal = @SubTotal,
                            Tax = @Tax,
                            ShippingFee = @ShippingFee,
                            Discount = @Discount,
                            Total = @Total,
                            updated_at = SYSUTCDATETIME()
                        WHERE SupOrderID = @OrderID";

                    using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection))
                    {
                        string billingAddress = $"{txtAddr1.Text}, {txtAddr2.Text}, {txtCity.Text}, {txtState.Text}, {txtPostal.Text}, {cmbCountry.Text}";

                        orderCommand.Parameters.AddWithValue("@OrderID", _orderIdNum);
                        orderCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                        orderCommand.Parameters.AddWithValue("@OrderDate", dateOrder.Value);
                        orderCommand.Parameters.AddWithValue("@EstimatedDate", dateEstimated.Value);
                        orderCommand.Parameters.AddWithValue("@BillingAddress", billingAddress);
                        orderCommand.Parameters.AddWithValue("@ShippingAddress", billingAddress);
                        orderCommand.Parameters.AddWithValue("@SubTotal", subtotal);
                        orderCommand.Parameters.AddWithValue("@Tax", 0);
                        orderCommand.Parameters.AddWithValue("@ShippingFee", numShipping.Value);
                        orderCommand.Parameters.AddWithValue("@Discount", discount);
                        orderCommand.Parameters.AddWithValue("@Total", total);

                        orderCommand.ExecuteNonQuery();
                    }

                    // Delete existing order items
                    string deleteItemsQuery = "DELETE FROM supporderitem WHERE SupOrderID = @OrderID";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteItemsQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@OrderID", _orderIdNum);
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Insert updated order items
                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        if (row.Tag is ProductRow product)
                        {
                            long productId = GetProductId(product.Name, connection);
                            if (productId > 0)
                            {
                                string itemQuery = @"
                                    INSERT INTO supporderitem (SupOrderID, ProdID, Qty, TotalCost)
                                    VALUES (@SupOrderID, @ProdID, @Qty, @TotalCost)";

                                using (SqlCommand itemCommand = new SqlCommand(itemQuery, connection))
                                {
                                    itemCommand.Parameters.AddWithValue("@SupOrderID", _orderIdNum);
                                    itemCommand.Parameters.AddWithValue("@ProdID", productId);
                                    itemCommand.Parameters.AddWithValue("@Qty", product.Qty);
                                    itemCommand.Parameters.AddWithValue("@TotalCost", product.Qty * product.Price);
                                    itemCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    MessageBox.Show($"Order {_orderId} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReturnToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetSupplierId(string companyName, SqlConnection connection)
        {
            string query = "SELECT id FROM suppliers WHERE CompanyName = @CompanyName AND Status = 'active'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CompanyName", companyName);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : 0;
            }
        }

        private long GetProductId(string productName, SqlConnection connection)
        {
            string query = "SELECT ProdID FROM product_list WHERE ProductName = @ProductName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : 0;
            }
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