using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddSupplierOrder : Form
    {
        private readonly List<ProductRow> products = new List<ProductRow>();
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public AddSupplierOrder()
        {
            InitializeComponent();
            SetupCombos();
            SetupButtonStyles();
            SetupDataGridViewStyle();

            dateOrder.Value = DateTime.Today;
            dateEstimated.Value = dateOrder.Value.AddDays(7);

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
        }

        private void SetupDataGridViewStyle()
        {
            // Remove "Available Quantity" column if it exists
            if (dgvItems.Columns.Contains("AVAILABLE QUANTITY") || dgvItems.Columns.Count > 4)
            {
                for (int i = dgvItems.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvItems.Columns[i].HeaderText.Contains("AVAILABLE") ||
                        dgvItems.Columns[i].Name.Contains("Available"))
                    {
                        dgvItems.Columns.RemoveAt(i);
                        break;
                    }
                }
            }

            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvItems.DefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvItems.BackgroundColor = Color.White;
            dgvItems.RowHeadersVisible = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.RowTemplate.Height = 40;
            dgvItems.ReadOnly = false;

            foreach (DataGridViewColumn col in dgvItems.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dgvItems.Columns.Count > 1)
            {
                dgvItems.Columns[1].ReadOnly = false;
            }

            dgvItems.DefaultCellStyle.BackColor = Color.White;
            dgvItems.DefaultCellStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvItems.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvItems.DefaultCellStyle.Padding = new Padding(5);
            dgvItems.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvItems.GridColor = Color.FromArgb(231, 229, 255);
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvItems.CellEndEdit += dgvItems_CellEndEdit;
            dgvItems.EditingControlShowing += dgvItems_EditingControlShowing;
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItems.CurrentCell.ColumnIndex == 1)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= Quantity_KeyPress;
                    tb.KeyPress += Quantity_KeyPress;
                }
            }
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = dgvItems.Rows[e.RowIndex];
                if (row.Tag is ProductRow product)
                {
                    if (int.TryParse(row.Cells[1].Value?.ToString(), out int newQty) && newQty > 0)
                    {
                        product.Qty = newQty;
                        decimal subtotal = product.Qty * product.Price;
                        row.Cells[3].Value = $"₱{subtotal:F2}";
                        RecalculateTotals();
                    }
                    else
                    {
                        row.Cells[1].Value = product.Qty;
                        MessageBox.Show("Please enter a valid quantity (greater than 0).", "Invalid Quantity",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void SetupCombos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

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

            cmbCompany.SelectedIndex = 0;
            cmbPayment.SelectedIndex = 0;
            cmbCountry.SelectedIndex = 0;
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
            int i = dgvItems.Rows.Add(p.Name, p.Qty, $"₱{p.Price:F2}", $"₱{p.Qty * p.Price:F2}");
            dgvItems.Rows[i].Tag = p;
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
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get supplier ID
                            long supplierId = GetSupplierId(cmbCompany.Text, connection, transaction);
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

                            // Insert supplier order
                            string orderQuery = @"
                                INSERT INTO supplier_orders (
                                    SupplierID, OrderDate, EstimatedDate, BillingAddress, ShippingAddress,
                                    SubTotal, Tax, ShippingFee, Discount, Total, Status, approval_status
                                ) VALUES (
                                    @SupplierID, @OrderDate, @EstimatedDate, @BillingAddress, @ShippingAddress,
                                    @SubTotal, @Tax, @ShippingFee, @Discount, @Total, @Status, @ApprovalStatus
                                );
                                SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                            long orderId;
                            using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection, transaction))
                            {
                                string billingAddress = $"{txtAddr1.Text}, {txtAddr2.Text}, {txtCity.Text}, {txtState.Text}, {txtPostal.Text}, {cmbCountry.Text}";

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
                                orderCommand.Parameters.AddWithValue("@Status", "Pending");
                                orderCommand.Parameters.AddWithValue("@ApprovalStatus", "pending");

                                orderId = Convert.ToInt64(orderCommand.ExecuteScalar());
                            }

                            // Insert order items with detailed logging
                            int itemsInserted = 0;
                            List<string> failedProducts = new List<string>();

                            foreach (DataGridViewRow row in dgvItems.Rows)
                            {
                                if (row.Tag is ProductRow product)
                                {
                                    // Get product ID
                                    long productId = GetProductId(product.Name, connection, transaction);

                                    if (productId > 0)
                                    {
                                        string itemQuery = @"
                                            INSERT INTO supporderitem (SupOrderID, ProdID, Qty, TotalCost)
                                            VALUES (@SupOrderID, @ProdID, @Qty, @TotalCost)";

                                        using (SqlCommand itemCommand = new SqlCommand(itemQuery, connection, transaction))
                                        {
                                            itemCommand.Parameters.AddWithValue("@SupOrderID", orderId);
                                            itemCommand.Parameters.AddWithValue("@ProdID", productId);
                                            itemCommand.Parameters.AddWithValue("@Qty", product.Qty);
                                            itemCommand.Parameters.AddWithValue("@TotalCost", product.Qty * product.Price);
                                            itemCommand.ExecuteNonQuery();
                                            itemsInserted++;
                                        }
                                    }
                                    else
                                    {
                                        failedProducts.Add(product.Name);
                                    }
                                }
                            }

                            // Check if all items were inserted
                            if (failedProducts.Count > 0)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Failed to find the following products in database:\n{string.Join("\n", failedProducts)}\n\nOrder not saved.",
                                    "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (itemsInserted == 0)
                            {
                                transaction.Rollback();
                                MessageBox.Show("No items were inserted. Please check your products.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Commit transaction
                            transaction.Commit();

                            MessageBox.Show($"Supplier order SO-{orderId:D5} created successfully with {itemsInserted} item(s)!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving order: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetSupplierId(string companyName, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT id FROM suppliers WHERE CompanyName = @CompanyName AND Status = 'active'";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@CompanyName", companyName);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt64(result) : 0;
            }
        }

        private long GetProductId(string productName, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT ProdID FROM product_list WHERE ProductName = @ProductName";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
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