using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewSupplierOrder : Form
    {
        private readonly string _orderId;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewSupplierOrder(string orderId)
        {
            _orderId = orderId;
            InitializeComponent();
            LoadData();
            btnClose.Click += btnClose_Click;
            ApplyReadOnlyStyling();
        }

        private void LoadData()
        {
            try
            {
                if (string.IsNullOrEmpty(_orderId))
                {
                    ShowErrorMessage("No order selected.");
                    LoadSampleData();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Extract numeric ID from SO-XXXXX format
                    long orderIdNum;
                    if (_orderId.StartsWith("SO-"))
                    {
                        string idPart = _orderId.Substring(3);
                        if (!long.TryParse(idPart, out orderIdNum))
                        {
                            ShowErrorMessage($"Invalid order ID format: {_orderId}");
                            LoadSampleData();
                            return;
                        }
                    }
                    else
                    {
                        if (!long.TryParse(_orderId, out orderIdNum))
                        {
                            ShowErrorMessage($"Invalid order ID: {_orderId}");
                            LoadSampleData();
                            return;
                        }
                    }

                    // Query based on your actual database schema
                    string orderQuery = @"
                        SELECT 
                            so.SupOrderID,
                            so.OrderDate,
                            so.EstimatedDate,
                            so.SubTotal,
                            so.Tax,
                            so.ShippingFee,
                            so.Discount,
                            so.Total,
                            so.Status,
                            so.approval_status,
                            so.approval_reason,
                            s.CompanyName,
                            s.Title,
                            s.FirstName,
                            s.LastName,
                            s.Email,
                            s.PaymentTerms as SupplierPaymentTerms,
                            s.PhoneNo,
                            s.ContactPerson,
                            s.ContactDetail,
                            s.Country,
                            s.City,
                            s.ZipCode,
                            s.Add1 as Address1,
                            s.Add2 as Address2,
                            u1.user_name as CreatedBy,
                            u2.user_name as ApprovedBy
                        FROM supplier_orders so
                        INNER JOIN suppliers s ON so.SupplierID = s.id
                        LEFT JOIN users u1 ON so.created_by_user_id = u1.id
                        LEFT JOIN users u2 ON so.approved_by_user_id = u2.id
                        WHERE so.SupOrderID = @OrderID";

                    using (SqlCommand command = new SqlCommand(orderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderIdNum);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load order header information
                                txtCompany.Text = reader["CompanyName"] != DBNull.Value ?
                                    reader["CompanyName"].ToString() : "N/A";

                                txtOrderDate.Text = reader["OrderDate"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["OrderDate"]).ToString("MMMM dd, yyyy") : "N/A";

                                // Use supplier's payment terms
                                txtPayment.Text = reader["SupplierPaymentTerms"] != DBNull.Value ?
                                    reader["SupplierPaymentTerms"].ToString() : "Net 30";

                                txtEstDate.Text = reader["EstimatedDate"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["EstimatedDate"]).ToString("MMMM dd, yyyy") : "N/A";

                                // Load address information from suppliers table
                                txtAddr1.Text = reader["Address1"] != DBNull.Value ?
                                    reader["Address1"].ToString() : "";

                                txtAddr2.Text = reader["Address2"] != DBNull.Value ?
                                    reader["Address2"].ToString() : "";

                                txtCity.Text = reader["City"] != DBNull.Value ?
                                    reader["City"].ToString() : "";

                                // Note: Your suppliers table doesn't have a State column, only City and Country
                                txtState.Text = ""; // No state column in suppliers table

                                txtPostal.Text = reader["ZipCode"] != DBNull.Value ?
                                    reader["ZipCode"].ToString() : "";

                                txtCountry.Text = reader["Country"] != DBNull.Value ?
                                    reader["Country"].ToString() : "";

                                // Load financial information
                                decimal subtotal = reader["SubTotal"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["SubTotal"]) : 0;
                                lblSubtotalVal.Text = $"₱{subtotal:N2}";

                                decimal discount = reader["Discount"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["Discount"]) : 0;
                                txtDiscount.Text = $"{discount:N2}";

                                decimal shippingFee = reader["ShippingFee"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["ShippingFee"]) : 0;
                                txtShipping.Text = $"₱{shippingFee:N2}";

                                decimal total = reader["Total"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["Total"]) : 0;
                                lblTotalVal.Text = $"₱{total:N2}";

                                // Add tax information if available
                                decimal tax = reader["Tax"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["Tax"]) : 0;

                                // Create tax display if not already present
                                var lblTax = FindControl("lblTax") as Label;
                                var lblTaxVal = FindControl("lblTaxVal") as Label;

                                if (lblTax == null && tax > 0)
                                {
                                    lblTax = new Label
                                    {
                                        Name = "lblTax",
                                        Text = "Tax:",
                                        Font = new Font("Bahnschrift SemiCondensed", 10F),
                                        ForeColor = Color.Black,
                                        Location = new Point(1000, lblDiscount.Location.Y + 40),
                                        AutoSize = true
                                    };

                                    lblTaxVal = new Label
                                    {
                                        Name = "lblTaxVal",
                                        Text = $"₱{tax:N2}",
                                        Font = new Font("Bahnschrift SemiCondensed", 10F),
                                        ForeColor = Color.Black,
                                        Location = new Point(1200, lblTax.Location.Y),
                                        AutoSize = true
                                    };

                                    contentPanel.Controls.Add(lblTax);
                                    contentPanel.Controls.Add(lblTaxVal);

                                    // Adjust positions of other controls
                                    lblShipping.Location = new Point(1000, lblTax.Location.Y + 40);
                                    txtShipping.Location = new Point(1200, lblShipping.Location.Y);
                                    lblTotal.Location = new Point(1000, lblShipping.Location.Y + 50);
                                    lblTotalVal.Location = new Point(1200, lblTotal.Location.Y);
                                }
                                else if (lblTaxVal != null && tax > 0)
                                {
                                    lblTaxVal.Text = $"₱{tax:N2}";
                                }

                                // Update title with order ID and status
                                string status = reader["Status"] != DBNull.Value ?
                                    reader["Status"].ToString() : "N/A";
                                string approvalStatus = reader["approval_status"] != DBNull.Value ?
                                    reader["approval_status"].ToString() : "N/A";

                                label1.Text = $"View Supplier Order - {_orderId} ({status})";

                                // Add approval info if available
                                if (approvalStatus != "N/A")
                                {
                                    var lblApproval = FindControl("lblApproval") as Label;
                                    if (lblApproval == null)
                                    {
                                        lblApproval = new Label
                                        {
                                            Name = "lblApproval",
                                            Text = $"Approval: {approvalStatus}",
                                            Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold),
                                            ForeColor = approvalStatus == "approved" ? Color.Green :
                                                       approvalStatus == "pending" ? Color.Orange : Color.Red,
                                            Location = new Point(77, txtCountry.Location.Y + txtCountry.Height + 20),
                                            AutoSize = true
                                        };
                                        contentPanel.Controls.Add(lblApproval);
                                    }

                                    string createdBy = reader["CreatedBy"] != DBNull.Value ?
                                        reader["CreatedBy"].ToString() : "N/A";
                                    string approvedBy = reader["ApprovedBy"] != DBNull.Value ?
                                        reader["ApprovedBy"].ToString() : "N/A";

                                    var lblCreatedBy = FindControl("lblCreatedBy") as Label;
                                    if (lblCreatedBy == null && createdBy != "N/A")
                                    {
                                        lblCreatedBy = new Label
                                        {
                                            Name = "lblCreatedBy",
                                            Text = $"Created by: {createdBy}",
                                            Font = new Font("Bahnschrift SemiCondensed", 9F),
                                            ForeColor = Color.FromArgb(100, 100, 100),
                                            Location = new Point(77, lblApproval.Location.Y + 25),
                                            AutoSize = true
                                        };
                                        contentPanel.Controls.Add(lblCreatedBy);
                                    }

                                    if (approvedBy != "N/A" && approvalStatus == "approved")
                                    {
                                        var lblApprovedBy = FindControl("lblApprovedBy") as Label;
                                        if (lblApprovedBy == null)
                                        {
                                            lblApprovedBy = new Label
                                            {
                                                Name = "lblApprovedBy",
                                                Text = $"Approved by: {approvedBy}",
                                                Font = new Font("Bahnschrift SemiCondensed", 9F),
                                                ForeColor = Color.FromArgb(100, 100, 100),
                                                Location = new Point(77, lblCreatedBy != null ? lblCreatedBy.Location.Y + 25 : lblApproval.Location.Y + 25),
                                                AutoSize = true
                                            };
                                            contentPanel.Controls.Add(lblApprovedBy);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ShowErrorMessage($"Order with ID '{_orderId}' not found in database.");
                                LoadSampleData();
                                return;
                            }
                        }
                    }

                    // Load order items
                    LoadOrderItems(connection, orderIdNum);
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage($"Database error: {ex.Message}");
                LoadSampleData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading order data: {ex.Message}");
                LoadSampleData();
            }
        }

        private void LoadOrderItems(SqlConnection connection, long orderId)
        {
            try
            {
                dgvItems.Rows.Clear();

                // Simple query without available quantity column
                string itemsQuery = @"
                    SELECT 
                        p.ProductName,
                        soi.Qty as Quantity,
                        (soi.TotalCost / NULLIF(soi.Qty, 0)) as UnitPrice,
                        soi.TotalCost as Subtotal
                    FROM supporderitem soi
                    INNER JOIN product_list p ON soi.ProdID = p.ProdID
                    WHERE soi.SupOrderID = @OrderID
                    ORDER BY soi.SupItemID";

                using (SqlCommand command = new SqlCommand(itemsQuery, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader["ProductName"] != DBNull.Value ?
                                reader["ProductName"].ToString() : "N/A";

                            int quantity = reader["Quantity"] != DBNull.Value ?
                                Convert.ToInt32(reader["Quantity"]) : 0;

                            decimal unitPrice = 0;
                            if (reader["UnitPrice"] != DBNull.Value && !reader.IsDBNull(reader.GetOrdinal("UnitPrice")))
                            {
                                unitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                            }

                            decimal subtotal = reader["Subtotal"] != DBNull.Value ?
                                Convert.ToDecimal(reader["Subtotal"]) : 0;

                            // Add to DataGridView - we'll use 0 for available quantity since column doesn't exist
                            dgvItems.Rows.Add(
                                productName,
                                quantity,
                                $"₱{unitPrice:N2}",
                                0, // Placeholder since available quantity column doesn't exist
                                $"₱{subtotal:N2}"
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order items: {ex.Message}", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Add sample items as fallback
                dgvItems.Rows.Add("Sample Product 1", 2, "₱1,000.00", 0, "₱2,000.00");
                dgvItems.Rows.Add("Sample Product 2", 1, "₱500.00", 0, "₱500.00");
            }
        }

        private Control FindControl(string name)
        {
            foreach (Control control in contentPanel.Controls)
            {
                if (control.Name == name)
                    return control;
            }
            return null;
        }

        private void ApplyReadOnlyStyling()
        {
            // Style all textboxes as read-only
            foreach (Control control in contentPanel.Controls)
            {
                if (control is Guna2TextBox textBox)
                {
                    textBox.ReadOnly = true;
                    textBox.BorderColor = Color.FromArgb(200, 200, 200);
                    textBox.FillColor = Color.FromArgb(248, 248, 248);
                    textBox.Cursor = Cursors.Default;
                    textBox.BorderRadius = 5;
                }
            }

            // Style DataGridView
            dgvItems.ReadOnly = true;
            dgvItems.DefaultCellStyle.SelectionBackColor = dgvItems.DefaultCellStyle.BackColor;
            dgvItems.DefaultCellStyle.SelectionForeColor = dgvItems.DefaultCellStyle.ForeColor;
            dgvItems.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // Style close button
            btnClose.FillColor = Color.FromArgb(108, 117, 125);
            btnClose.HoverState.FillColor = Color.FromArgb(90, 100, 110);
            btnClose.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnClose.BorderRadius = 8;
        }

        private void LoadSampleData()
        {
            // Fallback sample data
            txtCompany.Text = "Sample Supplier";
            txtOrderDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            txtPayment.Text = "Net 30";
            txtEstDate.Text = DateTime.Now.AddDays(7).ToString("MMMM dd, yyyy");

            txtAddr1.Text = "123 Main Street";
            txtAddr2.Text = "Suite 100";
            txtCity.Text = "Sample City";
            txtState.Text = ""; // No state in your schema
            txtPostal.Text = "12345";
            txtCountry.Text = "Sample Country";

            // Add sample items
            dgvItems.Rows.Clear();
            dgvItems.Rows.Add("Sample Product 1", 2, "₱1,000.00", 0, "₱2,000.00");
            dgvItems.Rows.Add("Sample Product 2", 1, "₱500.00", 0, "₱500.00");

            decimal subtotal = 2500m;
            decimal discount = 100m;
            decimal shipping = 200m;
            decimal total = subtotal - discount + shipping;

            lblSubtotalVal.Text = $"₱{subtotal:F2}";
            txtDiscount.Text = $"{discount:F2}";
            txtShipping.Text = $"₱{shipping:F2}";
            lblTotalVal.Text = $"₱{total:F2}";

            label1.Text = $"View Supplier Order - {_orderId}";
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ReturnToList();
        }

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Supplier Orders";

            // Refresh the SupplierOrderList form
            var orderList = new SupplierOrderList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(orderList);
            orderList.Show();
        }
    }
}