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
    public partial class AddRental : Form
    {
        private readonly List<RentalItem> rentalItems = new List<RentalItem>();
        private readonly Dictionary<string, long> productNameToIdMap = new Dictionary<string, long>();
        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public AddRental()
        {
            InitializeComponent();

            numServiceFee.Maximum = 999999;
            numDiscount.Maximum = 100;
            numServiceFee_Addr.Maximum = 999999;
            numDiscount_Addr.Maximum = 100;

            SetupControls();
            WireEvents();
            RecalculateTotals();
            ShowPanel(pnlRentalInfo, pnlAddress);
        }

        private void SetupControls()
        {
            cmbBookingType.Items.AddRange(new[] { "Daily", "Weekly", "Monthly", "Event", "Long Term" });
            cmbPaymentTerms.Items.AddRange(new[] { "Full Payment", "50% Downpayment", "Installment", "Cash on Delivery" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Confirmed", "Ongoing", "Completed", "Cancelled" });

            dtpScheduledDate.Value = DateTime.Today;
            dtpReturnDate.Value = DateTime.Today.AddDays(7);

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

        private long GetOrCreateCustomer(SqlConnection conn, SqlTransaction transaction)
        {
            // Check if customer already exists by phone number or email
            string checkCustomerSql = @"
                SELECT CustID FROM customers 
                WHERE (PhoneNo = @PhoneNo OR Email = @Email) 
                OR (FirstName + ' ' + LastName = @CustomerName)";

            using (SqlCommand cmd = new SqlCommand(checkCustomerSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PhoneNo", txtContactNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt64(result);
                }
            }

            // Create new customer
            string insertCustomerSql = @"
                INSERT INTO customers (
                    Title, FirstName, LastName, CompanyName, Email, 
                    PhoneNo, ContactPerson, ContactDetail, Country, City, 
                    ZipCode, Add1, Add2, Status, created_at, updated_at
                ) VALUES (
                    @Title, @FirstName, @LastName, @CompanyName, @Email,
                    @PhoneNo, @ContactPerson, @ContactDetail, @Country, @City,
                    @ZipCode, @Add1, @Add2, @Status, GETDATE(), GETDATE()
                );
                SELECT SCOPE_IDENTITY();";

            // Parse name into first and last name
            string[] nameParts = txtCustomerName.Text.Trim().Split(' ');
            string firstName = nameParts.Length > 0 ? nameParts[0] : txtCustomerName.Text.Trim();
            string lastName = nameParts.Length > 1 ? nameParts[1] : "";

            using (SqlCommand cmd = new SqlCommand(insertCustomerSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Title", "Mr.");
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@CompanyName", txtCustomerName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@PhoneNo", txtContactNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactDetail", txtContactPerson.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", "Philippines");
                cmd.Parameters.AddWithValue("@City", "N/A");
                cmd.Parameters.AddWithValue("@ZipCode", "0000");
                cmd.Parameters.AddWithValue("@Add1", txtBillingAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Add2", txtShippingAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", "Active");

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        private string InsertRental(SqlConnection conn, SqlTransaction transaction, long customerId)
        {
            decimal subtotal = rentalItems.Sum(x => x.Subtotal);
            decimal discountAmount = subtotal * (numDiscount.Value / 100m);
            decimal total = subtotal - discountAmount + numServiceFee.Value;

            string insertRentalSql = @"
                INSERT INTO rentals (
                    CustomerID, 
                    contact_person, 
                    contact_number,
                    email,
                    booking_type, 
                    payment_terms, 
                    status, 
                    scheduled_date, 
                    return_date,
                    billing_address,
                    shipping_address,
                    discount,
                    service_fee,
                    subtotal,
                    total,
                    created_at,
                    updated_at
                ) VALUES (
                    @CustomerID, 
                    @ContactPerson, 
                    @ContactNumber,
                    @Email,
                    @BookingType, 
                    @PaymentTerms, 
                    @Status, 
                    @ScheduledDate, 
                    @ReturnDate,
                    @BillingAddress,
                    @ShippingAddress,
                    @Discount,
                    @ServiceFee,
                    @Subtotal,
                    @Total,
                    GETDATE(),
                    GETDATE()
                );
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(insertRentalSql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
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

                long rentalId = Convert.ToInt64(cmd.ExecuteScalar());
                return "RENT-" + rentalId.ToString().PadLeft(6, '0');
            }
        }

        private void InsertRentalItems(SqlConnection conn, SqlTransaction transaction, string rentalId)
        {
            long rentalDbId = Convert.ToInt64(rentalId.Replace("RENT-", ""));

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

                using (SqlCommand cmd = new SqlCommand(insertItemSql, conn, transaction))
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

        private void UpdateProductStock(SqlConnection conn, SqlTransaction transaction)
        {
            foreach (var item in rentalItems)
            {
                if (productNameToIdMap.ContainsKey(item.ProductName))
                {
                    long productId = productNameToIdMap[item.ProductName];

                    // Update stock - find active stock items and reduce quantity
                    string updateStockSql = @"
                        UPDATE TOP(1) stock_items 
                        SET qty = qty - @Quantity,
                            updated_at = GETDATE()
                        WHERE ProductID = @ProductID 
                        AND Status = 'active' 
                        AND qty >= @Quantity";

                    using (SqlCommand cmd = new SqlCommand(updateStockSql, conn, transaction))
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
                            // Get or create customer
                            long customerId = GetOrCreateCustomer(conn, transaction);

                            // Insert rental record
                            string rentalId = InsertRental(conn, transaction, customerId);

                            // Insert rental items
                            InsertRentalItems(conn, transaction, rentalId);

                            // Update product stock (if applicable)
                            UpdateProductStock(conn, transaction);

                            // Commit transaction
                            transaction.Commit();

                            MessageBox.Show($"Rental {rentalId} has been created successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            GoBackToRentalList();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error saving rental: {ex.Message}", "Database Error",
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