using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditRental : Form
    {
        private readonly List<RentalItem> rentalItems = new List<RentalItem>();
        private readonly Dictionary<string, long> productNameToIdMap = new Dictionary<string, long>();
        private readonly Dictionary<string, int> originalQuantities = new Dictionary<string, int>();
        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly string rentalId;
        private long rentalDbId;
        private long customerId;

        public EditRental(string id = "RENT-001")
        {
            InitializeComponent();
            rentalId = id;
            rentalDbId = Convert.ToInt64(id.Replace("RENT-", ""));

            numServiceFee.Maximum = 999999;
            numDiscount.Maximum = 100;
            numServiceFee_Addr.Maximum = 999999;
            numDiscount_Addr.Maximum = 100;

            SetupControls();
            WireEvents();
            LoadExistingData();
            RecalculateTotals();
            ShowPanel(pnlRentalInfo, pnlAddress);
        }

        private void SetupControls()
        {
            cmbBookingType.Items.AddRange(new[] { "Daily", "Weekly", "Monthly", "Event", "Long Term" });
            cmbPaymentTerms.Items.AddRange(new[] { "Full Payment", "50% Downpayment", "Installment", "Cash on Delivery" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Confirmed", "Ongoing", "Completed", "Cancelled" });

            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 57, 101);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F);
            dgvItems.RowTemplate.Height = 50;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvItems.Columns.Count == 0)
            {
                dgvItems.Columns.Add("ProductName", "Product Name");
                dgvItems.Columns.Add("Quantity", "Qty");
                dgvItems.Columns.Add("Price", "Price");
                dgvItems.Columns.Add("Available", "Available");
                dgvItems.Columns.Add("Subtotal", "Subtotal");
            }
        }

        private void LoadExistingData()
        {
            lblHeader.Text = $"Edit Rental - {rentalId}";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load rental details
                    string rentalSql = @"
                        SELECT 
                            r.*,
                            c.CustID,
                            c.FirstName,
                            c.LastName,
                            c.CompanyName,
                            c.PhoneNo,
                            c.Email,
                            c.Add1 as Address
                        FROM rentals r
                        LEFT JOIN customers c ON r.CustomerID = c.CustID
                        WHERE r.RentalID = @RentalID";

                    using (SqlCommand cmd = new SqlCommand(rentalSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RentalID", rentalDbId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerId = Convert.ToInt64(reader["CustomerID"]);

                                // Get customer name (prefer CompanyName, fallback to FirstName + LastName)
                                string companyName = reader["CompanyName"]?.ToString();
                                string firstName = reader["FirstName"]?.ToString();
                                string lastName = reader["LastName"]?.ToString();

                                if (!string.IsNullOrWhiteSpace(companyName))
                                {
                                    txtCustomerName.Text = companyName;
                                }
                                else if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
                                {
                                    txtCustomerName.Text = $"{firstName} {lastName}".Trim();
                                }

                                txtContactPerson.Text = reader["contact_person"]?.ToString() ?? "";
                                txtContactNumber.Text = reader["contact_number"]?.ToString() ?? "";
                                txtEmail.Text = reader["email"]?.ToString() ?? "";

                                // Set combo box values
                                SetComboBoxValue(cmbBookingType, reader["booking_type"]?.ToString() ?? "Daily");
                                SetComboBoxValue(cmbPaymentTerms, reader["payment_terms"]?.ToString() ?? "Full Payment");
                                SetComboBoxValue(cmbStatus, reader["status"]?.ToString() ?? "pending");

                                // Set dates
                                if (reader["scheduled_date"] != DBNull.Value)
                                    dtpScheduledDate.Value = Convert.ToDateTime(reader["scheduled_date"]);
                                if (reader["return_date"] != DBNull.Value)
                                    dtpReturnDate.Value = Convert.ToDateTime(reader["return_date"]);

                                txtBillingAddress.Text = reader["billing_address"]?.ToString() ?? "";
                                txtShippingAddress.Text = reader["shipping_address"]?.ToString() ?? "";

                                numDiscount.Value = reader["discount"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["discount"]) : 0;
                                numServiceFee.Value = reader["service_fee"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["service_fee"]) : 0;
                            }
                        }
                    }

                    // Load rental items
                    string itemsSql = @"
                        SELECT 
                            ri.*,
                            p.ProdID,
                            p.ProductName,
                            COALESCE(SUM(si.qty), 0) as AvailableQty
                        FROM rental_items ri
                        LEFT JOIN product_list p ON ri.ProductID = p.ProdID
                        LEFT JOIN stock_items si ON p.ProdID = si.ProductID AND si.Status = 'active'
                        WHERE ri.RentalID = @RentalID
                        GROUP BY ri.RentalItemID, ri.ProductID, ri.quantity, 
                                 ri.rental_price, ri.subtotal, p.ProdID, p.ProductName";

                    using (SqlCommand cmd = new SqlCommand(itemsSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RentalID", rentalDbId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dgvItems.Rows.Clear();
                            rentalItems.Clear();
                            originalQuantities.Clear();

                            while (reader.Read())
                            {
                                string productName = reader["ProductName"]?.ToString() ?? "Unknown Product";
                                int quantity = Convert.ToInt32(reader["quantity"]);

                                var item = new RentalItem
                                {
                                    ProductName = productName,
                                    Quantity = quantity,
                                    RentalPrice = Convert.ToDecimal(reader["rental_price"]),
                                    AvailableQty = Convert.ToInt32(reader["AvailableQty"])
                                };

                                long productId = reader["ProdID"] != DBNull.Value ?
                                    Convert.ToInt64(reader["ProdID"]) : 0;

                                if (productId > 0)
                                {
                                    productNameToIdMap[productName] = productId;
                                }

                                // Store original quantity for stock restoration
                                originalQuantities[productName] = quantity;

                                rentalItems.Add(item);
                                dgvItems.Rows.Add(
                                    item.ProductName,
                                    item.Quantity,
                                    $"₱{item.RentalPrice:N2}",
                                    item.AvailableQty,
                                    $"₱{item.Subtotal:N2}"
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rental data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback to sample data
                LoadSampleData();
            }
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            comboBox.SelectedIndex = 0;
        }

        private void LoadSampleData()
        {
            txtCustomerName.Text = "Juan Dela Cruz";
            txtContactPerson.Text = "Juan";
            txtContactNumber.Text = "0917-123-4567";
            txtEmail.Text = "juan@example.com";
            cmbBookingType.SelectedIndex = 3;
            cmbPaymentTerms.SelectedIndex = 1;
            cmbStatus.SelectedIndex = 2;
            dtpScheduledDate.Value = DateTime.Today.AddDays(-2);
            dtpReturnDate.Value = DateTime.Today.AddDays(5);
            txtBillingAddress.Text = "123 Sampaguita St., Brgy. Holy Spirit, Quezon City";
            txtShippingAddress.Text = "Same as billing";
            numDiscount.Value = 10;
            numServiceFee.Value = 2500;

            var items = new[]
            {
                new RentalItem { ProductName = "LED Wall 10x20", Quantity = 2, RentalPrice = 45000m, AvailableQty = 8 },
                new RentalItem { ProductName = "Sound System", Quantity = 1, RentalPrice = 18000m, AvailableQty = 4 }
            };

            rentalItems.Clear();
            originalQuantities.Clear();
            rentalItems.AddRange(items);
            dgvItems.Rows.Clear();

            foreach (var item in items)
            {
                dgvItems.Rows.Add(
                    item.ProductName,
                    item.Quantity,
                    $"₱{item.RentalPrice:N2}",
                    item.AvailableQty,
                    $"₱{item.Subtotal:N2}"
                );
                originalQuantities[item.ProductName] = item.Quantity;
            }
        }

        private void WireEvents()
        {
            btnRentalInfo.Click += (s, e) => ShowPanel(pnlRentalInfo, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlRentalInfo);
            btnSaveRental.Click += (s, e) => SaveRental();
            lnkBack.LinkClicked += (s, e) => GoBackToRentalList();
            btnAddProduct.Click += btnAddProduct_Click;

            numDiscount.ValueChanged += (s, e) => SyncAndRecalculate(numDiscount, numDiscount_Addr);
            numServiceFee.ValueChanged += (s, e) => SyncAndRecalculate(numServiceFee, numServiceFee_Addr);
            numDiscount_Addr.ValueChanged += (s, e) => SyncAndRecalculate(numDiscount_Addr, numDiscount);
            numServiceFee_Addr.ValueChanged += (s, e) => SyncAndRecalculate(numServiceFee_Addr, numServiceFee);
        }

        private void SyncAndRecalculate(Guna2NumericUpDown source, Guna2NumericUpDown target)
        {
            if (source.Value != target.Value)
                target.Value = source.Value;
            RecalculateTotals();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var modal = new ProdRentalModal())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    var item = new RentalItem
                    {
                        ProductName = modal.SelectedProductName,
                        Quantity = modal.Quantity,
                        RentalPrice = modal.RentalPrice,
                        AvailableQty = modal.AvailableQuantity
                    };

                    if (modal.SelectedProductID.HasValue)
                    {
                        productNameToIdMap[item.ProductName] = modal.SelectedProductID.Value;
                    }

                    rentalItems.Add(item);
                    dgvItems.Rows.Add(
                        item.ProductName,
                        item.Quantity,
                        $"₱{item.RentalPrice:N2}",
                        item.AvailableQty,
                        $"₱{item.Subtotal:N2}"
                    );
                    RecalculateTotals();
                }
            }
        }

        private void RecalculateTotals()
        {
            decimal subtotal = rentalItems.Sum(x => x.Subtotal);
            decimal discountAmount = subtotal * (numDiscount.Value / 100m);
            decimal total = subtotal - discountAmount + numServiceFee.Value;

            string subText = $"₱{subtotal:N2}";
            string totalText = $"₱{total:N2}";

            lblSubtotalVal.Text = subText;
            lblTotalVal.Text = totalText;
            lblSubtotalVal_Addr.Text = subText;
            lblTotalVal_Addr.Text = totalText;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide)
        {
            hide.Visible = false;
            show.Visible = true;
            btnRentalInfo.FillColor = btnAddress.FillColor = Color.FromArgb(245, 245, 245);
            btnRentalInfo.ForeColor = btnAddress.ForeColor = Color.Black;

            if (show == pnlRentalInfo)
            {
                btnRentalInfo.FillColor = Color.FromArgb(0, 123, 255);
                btnRentalInfo.ForeColor = Color.White;
            }
            else
            {
                btnAddress.FillColor = Color.FromArgb(0, 123, 255);
                btnAddress.ForeColor = Color.White;
            }
            RecalculateTotals();
        }

        private void UpdateCustomer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Parse customer name
                    string[] nameParts = txtCustomerName.Text.Trim().Split(' ');
                    string firstName = nameParts.Length > 0 ? nameParts[0] : txtCustomerName.Text.Trim();
                    string lastName = nameParts.Length > 1 ? nameParts[1] : "";

                    string updateCustomerSql = @"
                        UPDATE customers 
                        SET 
                            CompanyName = @CompanyName,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            PhoneNo = @PhoneNo,
                            Email = @Email,
                            ContactPerson = @ContactPerson,
                            Add1 = @Address,
                            updated_at = GETDATE()
                        WHERE CustID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(updateCustomerSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@CompanyName", txtCustomerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtContactNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtBillingAddress.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRental()
        {
            decimal subtotal = rentalItems.Sum(x => x.Subtotal);
            decimal discountAmount = subtotal * (numDiscount.Value / 100m);
            decimal total = subtotal - discountAmount + numServiceFee.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updateRentalSql = @"
                        UPDATE rentals SET
                            contact_person = @ContactPerson,
                            contact_number = @ContactNumber,
                            email = @Email,
                            booking_type = @BookingType,
                            payment_terms = @PaymentTerms,
                            status = @Status,
                            scheduled_date = @ScheduledDate,
                            return_date = @ReturnDate,
                            billing_address = @BillingAddress,
                            shipping_address = @ShippingAddress,
                            discount = @Discount,
                            service_fee = @ServiceFee,
                            subtotal = @Subtotal,
                            total = @Total,
                            updated_at = GETDATE()
                        WHERE RentalID = @RentalID";

                    using (SqlCommand cmd = new SqlCommand(updateRentalSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RentalID", rentalDbId);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@BookingType", cmbBookingType.SelectedItem?.ToString() ?? "Daily");
                        cmd.Parameters.AddWithValue("@PaymentTerms", cmbPaymentTerms.SelectedItem?.ToString() ?? "Full Payment");
                        cmd.Parameters.AddWithValue("@Status", (cmbStatus.SelectedItem?.ToString() ?? "Pending").ToLower());
                        cmd.Parameters.AddWithValue("@ScheduledDate", dtpScheduledDate.Value);
                        cmd.Parameters.AddWithValue("@ReturnDate", dtpReturnDate.Value);
                        cmd.Parameters.AddWithValue("@BillingAddress", txtBillingAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@ShippingAddress", txtShippingAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Discount", numDiscount.Value);
                        cmd.Parameters.AddWithValue("@ServiceFee", numServiceFee.Value);
                        cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                        cmd.Parameters.AddWithValue("@Total", total);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating rental: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void UpdateRentalItems()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Delete existing rental items
                    string deleteItemsSql = "DELETE FROM rental_items WHERE RentalID = @RentalID";
                    using (SqlCommand cmd = new SqlCommand(deleteItemsSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@RentalID", rentalDbId);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert new rental items
                    foreach (var item in rentalItems)
                    {
                        string insertItemSql = @"
                            INSERT INTO rental_items (
                                RentalID,
                                ProductID,
                                quantity,
                                rental_price,
                                subtotal,
                                created_at,
                                updated_at
                            ) VALUES (
                                @RentalID,
                                @ProductID,
                                @Quantity,
                                @RentalPrice,
                                @Subtotal,
                                GETDATE(),
                                GETDATE()
                            )";

                        using (SqlCommand cmd = new SqlCommand(insertItemSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@RentalID", rentalDbId);
                            cmd.Parameters.AddWithValue("@ProductID",
                                productNameToIdMap.ContainsKey(item.ProductName) ?
                                productNameToIdMap[item.ProductName] : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmd.Parameters.AddWithValue("@RentalPrice", item.RentalPrice);
                            cmd.Parameters.AddWithValue("@Subtotal", item.Subtotal);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating rental items: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void UpdateProductStock()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // First, restore original quantities
                    foreach (var originalItem in originalQuantities)
                    {
                        string productName = originalItem.Key;
                        int originalQty = originalItem.Value;

                        if (productNameToIdMap.ContainsKey(productName))
                        {
                            long productId = productNameToIdMap[productName];

                            string restoreStockSql = @"
                                UPDATE TOP(1) stock_items 
                                SET qty = qty + @Quantity,
                                    updated_at = GETDATE()
                                WHERE ProductID = @ProductID 
                                AND Status = 'active'";

                            using (SqlCommand cmd = new SqlCommand(restoreStockSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                cmd.Parameters.AddWithValue("@Quantity", originalQty);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Then, deduct new quantities
                    foreach (var item in rentalItems)
                    {
                        if (productNameToIdMap.ContainsKey(item.ProductName))
                        {
                            long productId = productNameToIdMap[item.ProductName];

                            string updateStockSql = @"
                                UPDATE TOP(1) stock_items 
                                SET qty = qty - @Quantity,
                                    updated_at = GETDATE()
                                WHERE ProductID = @ProductID 
                                AND Status = 'active' 
                                AND qty >= @Quantity";

                            using (SqlCommand cmd = new SqlCommand(updateStockSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected == 0)
                                {
                                    throw new Exception($"Insufficient stock for product: {item.ProductName}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product stock: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void SaveRental()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContactNumber.Text))
            {
                MessageBox.Show("Contact number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNumber.Focus();
                return;
            }

            if (rentalItems.Count == 0)
            {
                MessageBox.Show("Please add at least one rental item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAddProduct.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Update customer information
                            UpdateCustomer();

                            // Update rental record
                            UpdateRental();

                            // Update rental items
                            UpdateRentalItems();

                            // Update product stock (restore old, deduct new)
                            UpdateProductStock();

                            transaction.Commit();

                            MessageBox.Show($"Rental {rentalId} has been updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            GoBackToRentalList();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error updating rental: {ex.Message}", "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoBackToRentalList()
        {
            var mainForm = this.ParentForm as Form1;
            if (mainForm == null)
            {
                this.Close();
                return;
            }

            var rentalList = mainForm.pnlContent.Controls.OfType<RentalList>().FirstOrDefault();
            if (rentalList != null)
            {
                mainForm.navBar1.PageTitle = "Rental List";
                rentalList.BringToFront();
                rentalList.RefreshData();
            }
            else
            {
                rentalList = new RentalList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                mainForm.pnlContent.Controls.Clear();
                mainForm.pnlContent.Controls.Add(rentalList);
                mainForm.navBar1.PageTitle = "Rental List";
                rentalList.Show();
                rentalList.RefreshData();
            }
            this.Close();
        }
    }
}