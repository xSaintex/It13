using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditCustomerList : Form
    {
        private readonly string _customerId;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True";

        public EditCustomerList(string customerId)
        {
            _customerId = customerId;
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => UpdateCustomer();
            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;
            LoadCustomerData();
        }

        private void SetupPhoneFields()
        {
            txtPhone.PlaceholderText = "+63 9XX XXX XXXX";
            txtContactNum.PlaceholderText = "+63 9XX XXX XXXX";
        }

        private void SetupNumberOnlyFields()
        {
            txtPhone.KeyPress += NumberOnly_KeyPress;
            txtContactNum.KeyPress += NumberOnly_KeyPress;
            txtBZip.KeyPress += NumberOnly_KeyPress;
            txtSZip.KeyPress += NumberOnly_KeyPress;
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
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
                    string query = @"SELECT 
                                        Title, FirstName, LastName, Email, CompanyName, PhoneNo,
                                        PaymentTerms, Status, ContactPerson, ContactDetail,
                                        Country, City, ZipCode, Add1, Add2
                                    FROM customers 
                                    WHERE CustID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _customerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Other Info
                                txtTitle.Text = reader["Title"]?.ToString() ?? "";
                                txtFName.Text = reader["FirstName"]?.ToString() ?? "";
                                txtLName.Text = reader["LastName"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                                txtCompany.Text = reader["CompanyName"]?.ToString() ?? "";
                                txtPhone.Text = reader["PhoneNo"]?.ToString() ?? "";

                                // Set Payment Terms
                                string payment = reader["PaymentTerms"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(payment))
                                {
                                    int idx = cmbPayment.FindStringExact(payment);
                                    if (idx >= 0) cmbPayment.SelectedIndex = idx;
                                }

                                // Set Status
                                string status = reader["Status"]?.ToString() ?? "Active";
                                cmbStatus.SelectedItem = status;

                                txtContactPerson.Text = reader["ContactPerson"]?.ToString() ?? "";
                                txtContactNum.Text = reader["ContactDetail"]?.ToString() ?? "";

                                // Address fields - using same for both Billing and Shipping
                                string country = reader["Country"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(country))
                                {
                                    int idx = cmbBCountry.FindStringExact(country);
                                    if (idx >= 0)
                                    {
                                        cmbBCountry.SelectedIndex = idx;
                                        cmbSCountry.SelectedIndex = idx;
                                    }
                                }

                                string city = reader["City"]?.ToString() ?? "";
                                txtBCity.Text = city;
                                txtSCity.Text = city;

                                string zip = reader["ZipCode"]?.ToString() ?? "";
                                txtBZip.Text = zip;
                                txtSZip.Text = zip;

                                string add1 = reader["Add1"]?.ToString() ?? "";
                                txtBLine1.Text = add1;
                                txtSLine1.Text = add1;

                                string add2 = reader["Add2"]?.ToString() ?? "";
                                txtBLine2.Text = add2;
                                txtSLine2.Text = add2;
                            }
                            else
                            {
                                MessageBox.Show("Customer not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseForm();
            }
        }

        private void LnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbSCountry.SelectedItem = cmbBCountry.SelectedItem;
            txtSCity.Text = txtBCity.Text;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
        }

        private void UpdateCustomer()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                string.IsNullOrWhiteSpace(txtLName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtCompany.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please fill in all required fields (First Name, Last Name, Email, Company, Phone).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE customers SET
                                        Title = @title,
                                        FirstName = @fname,
                                        LastName = @lname,
                                        Email = @email,
                                        CompanyName = @company,
                                        PhoneNo = @phone,
                                        PaymentTerms = @payment,
                                        Status = @status,
                                        ContactPerson = @contactPerson,
                                        ContactDetail = @contactDetail,
                                        Country = @country,
                                        City = @city,
                                        ZipCode = @zip,
                                        Add1 = @add1,
                                        Add2 = @add2
                                    WHERE CustID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Other Info
                        cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@fname", txtFName.Text.Trim());
                        cmd.Parameters.AddWithValue("@lname", txtLName.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@company", txtCompany.Text.Trim());
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@payment", cmbPayment.Text);
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                        cmd.Parameters.AddWithValue("@contactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@contactDetail", txtContactNum.Text.Trim());

                        // Address - Save from Billing Address fields
                        cmd.Parameters.AddWithValue("@country", cmbBCountry.Text);
                        cmd.Parameters.AddWithValue("@city", txtBCity.Text.Trim());
                        cmd.Parameters.AddWithValue("@zip", txtBZip.Text.Trim());
                        cmd.Parameters.AddWithValue("@add1", txtBLine1.Text.Trim());
                        cmd.Parameters.AddWithValue("@add2", txtBLine2.Text.Trim());

                        cmd.Parameters.AddWithValue("@id", _customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Customer {_customerId} updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CloseForm();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Customer List";
                parent.pnlContent.Controls.Clear();

                var customerListForm = new CustomerList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                parent.pnlContent.Controls.Add(customerListForm);
                customerListForm.Show();
            }
        }
    }
}