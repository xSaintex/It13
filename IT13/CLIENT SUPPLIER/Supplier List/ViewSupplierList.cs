using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewSupplierList : Form
    {
        private readonly string _supplierId;
        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewSupplierList(string supplierId)
        {
            _supplierId = supplierId;
            InitializeComponent();

            // Tab switching
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress, pnlRemarks);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther, pnlRemarks);
            btnRemarks.Click += (s, e) => ShowPanel(pnlRemarks, pnlOther, pnlAddress);

            // Back button
            btnBack.Click += (s, e) => CloseForm();

            // Copy address link
            lnkCopy.Click += (s, e) => CopyBillingToShipping();

            // Default tab
            ShowPanel(pnlOther, pnlAddress, pnlRemarks);

            LoadSupplierData();
            MakeFieldsReadOnly();
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = hide2.Visible = false;
            show.Visible = true;

            btnOther.FillColor = Color.WhiteSmoke; btnOther.ForeColor = Color.Black;
            btnAddress.FillColor = Color.WhiteSmoke; btnAddress.ForeColor = Color.Black;
            btnRemarks.FillColor = Color.WhiteSmoke; btnRemarks.ForeColor = Color.Black;

            if (show == pnlOther)
            {
                btnOther.FillColor = Color.FromArgb(0, 123, 255);
                btnOther.ForeColor = Color.White;
            }
            if (show == pnlAddress)
            {
                btnAddress.FillColor = Color.FromArgb(0, 123, 255);
                btnAddress.ForeColor = Color.White;
            }
            if (show == pnlRemarks)
            {
                btnRemarks.FillColor = Color.FromArgb(0, 123, 255);
                btnRemarks.ForeColor = Color.White;
            }
        }

        private void LoadSupplierData()
        {
            try
            {
                // Extract numeric ID from "SUP-001" format if needed
                string numericId = _supplierId;
                if (_supplierId.Contains("SUP-"))
                {
                    numericId = _supplierId.Replace("SUP-", "").TrimStart('0');
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            Title,
                            FirstName,
                            LastName,
                            CompanyName,
                            Email,
                            PhoneNo,
                            PaymentTerms,
                            ContactPerson,
                            ContactDetail,
                            Country,
                            City,
                            ZipCode,
                            Add1,
                            Add2,
                            Status,
                            created_at,
                            updated_at
                        FROM suppliers 
                        WHERE id = @SupplierId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", numericId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Basic Information
                                txtTitle.Text = reader["Title"]?.ToString() ?? "";
                                txtFName.Text = reader["FirstName"]?.ToString() ?? "";
                                txtLName.Text = reader["LastName"]?.ToString() ?? "";
                                txtCompany.Text = reader["CompanyName"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                                txtPhone.Text = reader["PhoneNo"]?.ToString() ?? "";
                                cmbPayment.Text = reader["PaymentTerms"]?.ToString() ?? "Net 30";

                                // Status with proper formatting
                                string status = reader["Status"]?.ToString() ?? "Active";
                                cmbStatus.Text = char.ToUpper(status[0]) + status.Substring(1).ToLower();

                                // Contact Details
                                txtContactPerson.Text = reader["ContactPerson"]?.ToString() ?? "";

                                // Parse ContactDetail
                                string contactDetail = reader["ContactDetail"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(contactDetail))
                                {
                                    txtContactNum.Text = ExtractContactNumber(contactDetail);
                                }
                                else
                                {
                                    txtContactNum.Text = "";
                                }

                                // Billing Address
                                string country = reader["Country"]?.ToString() ?? "Philippines";
                                string city = reader["City"]?.ToString() ?? "";
                                string zip = reader["ZipCode"]?.ToString() ?? "";
                                string add1 = reader["Add1"]?.ToString() ?? "";
                                string add2 = reader["Add2"]?.ToString() ?? "";

                                cmbBCountry.Text = country;
                                txtBCity.Text = city;
                                txtBZip.Text = zip;
                                txtBLine1.Text = add1;
                                txtBLine2.Text = add2;

                                // For view mode, assume shipping is same as billing
                                // If you have separate shipping fields in DB, update query accordingly
                                cmbSCountry.Text = country;
                                txtSCity.Text = city;
                                txtSZip.Text = zip;
                                txtSLine1.Text = add1;
                                txtSLine2.Text = add2;

                                // Remarks - You could add a remarks field to your suppliers table
                                // For now, using a default message
                                txtRemarks.Text = GenerateRemarks(
                                    reader["FirstName"]?.ToString(),
                                    reader["LastName"]?.ToString(),
                                    reader["CompanyName"]?.ToString(),
                                    reader["PaymentTerms"]?.ToString(),
                                    reader["created_at"]?.ToString()
                                );

                                // Update form title
                                UpdateFormTitle(
                                    reader["FirstName"]?.ToString(),
                                    reader["LastName"]?.ToString(),
                                    reader["CompanyName"]?.ToString()
                                );
                            }
                            else
                            {
                                MessageBox.Show($"Supplier with ID {_supplierId} not found.", "Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CloseForm();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData();
            }
        }

        private string ExtractContactNumber(string contactDetail)
        {
            // Simple extraction logic
            if (string.IsNullOrEmpty(contactDetail))
                return "";

            // If it looks like a phone number, return as-is
            if (contactDetail.Contains("+") || contactDetail.Replace(" ", "").All(char.IsDigit))
                return contactDetail;

            // Try to extract from JSON or formatted text
            if (contactDetail.Contains("\"Phone\":") || contactDetail.Contains("\"phone\":"))
            {
                try
                {
                    int start = contactDetail.IndexOf("\"Phone\":", StringComparison.OrdinalIgnoreCase);
                    if (start == -1) start = contactDetail.IndexOf("\"phone\":");

                    if (start != -1)
                    {
                        start += 8; // Length of "\"Phone\":"
                        int end = contactDetail.IndexOf(",", start);
                        if (end == -1) end = contactDetail.IndexOf("}", start);

                        if (end > start)
                        {
                            string phonePart = contactDetail.Substring(start, end - start);
                            return phonePart.Trim(' ', '"', '\'');
                        }
                    }
                }
                catch
                {
                    // If parsing fails, return the original
                }
            }

            return contactDetail;
        }

        private string GenerateRemarks(string firstName, string lastName, string company, string paymentTerms, string createdDate)
        {
            string fullName = "";
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                fullName = $"{firstName} {lastName}";
            else if (!string.IsNullOrEmpty(firstName))
                fullName = firstName;
            else if (!string.IsNullOrEmpty(lastName))
                fullName = lastName;

            string remarks = $"Supplier Information:\r\n";
            remarks += $"• Name: {fullName}\r\n";
            remarks += $"• Company: {company ?? "Not specified"}\r\n";
            remarks += $"• Payment Terms: {paymentTerms ?? "Net 30"}\r\n";

            if (!string.IsNullOrEmpty(createdDate))
            {
                if (DateTime.TryParse(createdDate, out DateTime created))
                {
                    remarks += $"• Registered: {created:MMMM dd, yyyy}\r\n";
                }
            }

            remarks += $"\r\nNotes:\r\n";
            remarks += $"• Primary contact method: Phone call or email\r\n";
            remarks += $"• Standard delivery: 3-5 business days\r\n";
            remarks += $"• Minimum order: ₱5,000\r\n";

            return remarks;
        }

        private void UpdateFormTitle(string firstName, string lastName, string company)
        {
            string displayName = "";

            if (!string.IsNullOrEmpty(company))
                displayName = company;
            else if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                displayName = $"{firstName} {lastName}".Trim();
            else
                displayName = _supplierId;

            this.Text = $"View Supplier: {displayName}";
        }

        private void LoadSampleData()
        {
            // Fallback sample data
            txtTitle.Text = "Ms.";
            txtFName.Text = "Sarah";
            txtLName.Text = "Lim";
            txtEmail.Text = "sarah@supplies.co";
            txtCompany.Text = "Metro Supplies Co.";
            txtPhone.Text = "+63 917 888 7777";
            cmbPayment.Text = "Net 30";
            cmbStatus.Text = "Active";

            txtContactPerson.Text = "Robert Tan";
            txtContactNum.Text = "+63 922 555 4321";

            cmbBCountry.Text = "Philippines";
            txtBCity.Text = "Pasig City";
            txtBZip.Text = "1605";
            txtBLine1.Text = "Ortigas Center";
            txtBLine2.Text = "San Antonio";

            cmbSCountry.Text = "Philippines";
            txtSCity.Text = "Mandaluyong City";
            txtSZip.Text = "1550";
            txtSLine1.Text = "Shaw Boulevard";
            txtSLine2.Text = "Greenfield District";

            txtRemarks.Text =
                "Reliable supplier for office supplies and equipment.\r\n" +
                "• Offers 5% discount for bulk orders over ₱50,000\r\n" +
                "• Contact Robert Tan for quotations and delivery schedule\r\n" +
                "• Preferred payment: Bank transfer within Net 30";
        }

        private void MakeFieldsReadOnly()
        {
            // Make all textboxes read-only
            txtTitle.ReadOnly = true;
            txtFName.ReadOnly = true;
            txtLName.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtCompany.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtContactPerson.ReadOnly = true;
            txtContactNum.ReadOnly = true;
            txtBCity.ReadOnly = true;
            txtBZip.ReadOnly = true;
            txtBLine1.ReadOnly = true;
            txtBLine2.ReadOnly = true;
            txtSCity.ReadOnly = true;
            txtSZip.ReadOnly = true;
            txtSLine1.ReadOnly = true;
            txtSLine2.ReadOnly = true;
            txtRemarks.ReadOnly = true;

            // Disable comboboxes
            cmbPayment.Enabled = false;
            cmbStatus.Enabled = false;
            cmbBCountry.Enabled = false;
            cmbSCountry.Enabled = false;

            // Disable copy link (view-only mode)
            lnkCopy.Enabled = false;
        }

        private void CopyBillingToShipping()
        {
            cmbSCountry.Text = cmbBCountry.Text;
            txtSCity.Text = txtBCity.Text;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                // Navigate back to supplier list
                parent.navBar1.PageTitle = "Suppliers";
                parent.pnlContent.Controls.Clear();

                var supplierList = new SupplierList()
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                parent.pnlContent.Controls.Add(supplierList);
                supplierList.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}