using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewCustomerList : Form
    {
        private readonly string _customerId;
        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewCustomerList(string customerId)
        {
            _customerId = customerId;
            InitializeComponent();

            // Tab switching
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);

            // Back button
            btnBack.Click += (s, e) => CloseForm();

            // Copy address link
            lnkCopy.Click += (s, e) => CopyBillingToShipping();

            // Default: Show Other Details
            ShowPanel(pnlOther, pnlAddress);

            LoadCustomerData();
            MakeFieldsReadOnly();
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide)
        {
            show.Visible = true;
            hide.Visible = false;

            btnOther.FillColor = show == pnlOther ? Color.FromArgb(0, 123, 255) : Color.WhiteSmoke;
            btnAddress.FillColor = show == pnlAddress ? Color.FromArgb(0, 123, 255) : Color.WhiteSmoke;
            btnOther.ForeColor = show == pnlOther ? Color.White : Color.Black;
            btnAddress.ForeColor = show == pnlAddress ? Color.White : Color.Black;
        }

        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            Title,
                            FirstName,
                            LastName,
                            Email,
                            CompanyName,
                            PhoneNo,
                            PaymentTerms,
                            Status,
                            ContactPerson,
                            ContactDetail,
                            Country,
                            City,
                            ZipCode,
                            Add1,
                            Add2
                        FROM customers 
                        WHERE CustID = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", _customerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Basic Information
                                txtTitle.Text = reader["Title"]?.ToString() ?? "";
                                txtFName.Text = reader["FirstName"]?.ToString() ?? "";
                                txtLName.Text = reader["LastName"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                                txtCompany.Text = reader["CompanyName"]?.ToString() ?? "";
                                txtPhone.Text = reader["PhoneNo"]?.ToString() ?? "";
                                cmbPayment.Text = reader["PaymentTerms"]?.ToString() ?? "Cash";
                                cmbStatus.Text = reader["Status"]?.ToString() ?? "Active";

                                // Contact Details
                                txtContactPerson.Text = reader["ContactPerson"]?.ToString() ?? "";

                                // Parse ContactDetail JSON or use as-is
                                string contactDetail = reader["ContactDetail"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(contactDetail))
                                {
                                    // If ContactDetail contains JSON, extract phone number
                                    if (contactDetail.Contains("\"Phone\":") || contactDetail.Contains("Phone:"))
                                    {
                                        // Simple extraction - adjust based on your JSON format
                                        txtContactNum.Text = ExtractPhoneFromContactDetail(contactDetail);
                                    }
                                    else
                                    {
                                        txtContactNum.Text = contactDetail;
                                    }
                                }

                                // Billing Address (default address)
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

                                // For View mode, shipping address is same as billing unless you have separate shipping fields
                                // If your database has separate shipping fields, adjust the query and mapping here
                                cmbSCountry.Text = country;
                                txtSCity.Text = city;
                                txtSZip.Text = zip;
                                txtSLine1.Text = add1;
                                txtSLine2.Text = add2;

                                // Update form title with customer name
                                UpdateFormTitle(reader["FirstName"]?.ToString(), reader["LastName"]?.ToString());
                            }
                            else
                            {
                                MessageBox.Show($"Customer with ID {_customerId} not found.", "Not Found",
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
                LoadSampleData(); // Fallback to sample data
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData(); // Fallback to sample data
            }
        }

        private string ExtractPhoneFromContactDetail(string contactDetail)
        {
            // Simple extraction - adjust based on your actual ContactDetail format
            if (contactDetail.Contains("\"Phone\":"))
            {
                // JSON format: {"Phone":"+63 905 123 4567",...}
                try
                {
                    int start = contactDetail.IndexOf("\"Phone\":") + 8;
                    int end = contactDetail.IndexOf(",", start);
                    if (end == -1) end = contactDetail.IndexOf("}", start);
                    if (end > start)
                    {
                        string phonePart = contactDetail.Substring(start, end - start);
                        return phonePart.Trim(' ', '"', '\'');
                    }
                }
                catch
                {
                    return "";
                }
            }
            else if (contactDetail.Contains("Phone:"))
            {
                // Text format: Phone: +63 905 123 4567
                try
                {
                    int start = contactDetail.IndexOf("Phone:") + 6;
                    int end = contactDetail.IndexOf("\n", start);
                    if (end == -1) end = contactDetail.Length;
                    if (end > start)
                    {
                        return contactDetail.Substring(start, end - start).Trim();
                    }
                }
                catch
                {
                    return "";
                }
            }

            return contactDetail; // Return as-is if format not recognized
        }

        private void UpdateFormTitle(string firstName, string lastName)
        {
            string fullName = "";
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                fullName = $"{firstName} {lastName}";
            else if (!string.IsNullOrEmpty(firstName))
                fullName = firstName;
            else if (!string.IsNullOrEmpty(lastName))
                fullName = lastName;

            if (!string.IsNullOrEmpty(fullName))
                this.Text = $"View Customer: {fullName}";
        }

        private void LoadSampleData()
        {
            // Fallback sample data if database fails
            txtTitle.Text = "Mr.";
            txtFName.Text = "John";
            txtLName.Text = "Doe";
            txtEmail.Text = "john@abc.com";
            txtCompany.Text = "ABC Corporation";
            txtPhone.Text = "+63 905 123 4567";
            cmbPayment.Text = "Net 30";
            cmbStatus.Text = "Active";

            txtContactPerson.Text = "Maria Santos";
            txtContactNum.Text = "+63 923 456 7890";

            cmbBCountry.Text = "Philippines";
            txtBCity.Text = "Manila";
            txtBZip.Text = "1000";
            txtBLine1.Text = "123 Rizal Ave";
            txtBLine2.Text = "Sta. Cruz";

            cmbSCountry.Text = "Philippines";
            txtSCity.Text = "Quezon City";
            txtSZip.Text = "1100";
            txtSLine1.Text = "456 EDSA";
            txtSLine2.Text = "Cubao";
        }

        private void MakeFieldsReadOnly()
        {
            // Make all textboxes read-only
            txtTitle.ReadOnly = txtFName.ReadOnly = txtLName.ReadOnly = true;
            txtEmail.ReadOnly = txtCompany.ReadOnly = txtPhone.ReadOnly = true;
            txtContactPerson.ReadOnly = txtContactNum.ReadOnly = true;
            txtBCity.ReadOnly = txtBZip.ReadOnly = txtBLine1.ReadOnly = txtBLine2.ReadOnly = true;
            txtSCity.ReadOnly = txtSZip.ReadOnly = txtSLine1.ReadOnly = txtSLine2.ReadOnly = true;

            // Disable comboboxes
            cmbPayment.Enabled = false;
            cmbStatus.Enabled = false;
            cmbBCountry.Enabled = false;
            cmbSCountry.Enabled = false;

            // Disable copy link
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
                // Navigate back to customer list
                parent.navBar1.PageTitle = "Customers";
                parent.pnlContent.Controls.Clear();

                var customerList = new CustomerList()
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                parent.pnlContent.Controls.Add(customerList);
                customerList.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}