using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace IT13
{
    public partial class AddCustomerList : Form
    {
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        // Dictionary to store cities by country
        private Dictionary<string, string[]> citiesByCountry = new Dictionary<string, string[]>
        {
            { "Philippines", new string[] { "Manila", "Quezon City", "Makati", "Cebu City", "Davao City", "Taguig", "Pasig", "Caloocan", "Zamboanga City", "Antipolo" } },
            { "United States", new string[] { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" } },
            { "Canada", new string[] { "Toronto", "Montreal", "Vancouver", "Calgary", "Edmonton", "Ottawa", "Winnipeg", "Quebec City", "Hamilton", "Kitchener" } },
            { "United Kingdom", new string[] { "London", "Birmingham", "Manchester", "Glasgow", "Liverpool", "Leeds", "Sheffield", "Edinburgh", "Bristol", "Leicester" } },
            { "Australia", new string[] { "Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide", "Gold Coast", "Canberra", "Newcastle", "Wollongong", "Logan City" } },
            { "Japan", new string[] { "Tokyo", "Yokohama", "Osaka", "Nagoya", "Sapporo", "Fukuoka", "Kobe", "Kyoto", "Kawasaki", "Saitama" } },
            { "Singapore", new string[] { "Central Area", "Woodlands", "Tampines", "Jurong West", "Bedok", "Hougang", "Choa Chu Kang", "Yishun", "Sengkang", "Punggol" } }
        };

        // Dictionary to store zip codes by city
        private Dictionary<string, string> zipCodesByCity = new Dictionary<string, string>
        {
            // Philippines
            { "Manila", "1000" },
            { "Quezon City", "1100" },
            { "Makati", "1200" },
            { "Cebu City", "6000" },
            { "Davao City", "8000" },
            { "Taguig", "1630" },
            { "Pasig", "1600" },
            { "Caloocan", "1400" },
            { "Zamboanga City", "7000" },
            { "Antipolo", "1870" },
            
            // United States
            { "New York", "10001" },
            { "Los Angeles", "90001" },
            { "Chicago", "60601" },
            { "Houston", "77001" },
            { "Phoenix", "85001" },
            { "Philadelphia", "19101" },
            { "San Antonio", "78201" },
            { "San Diego", "92101" },
            { "Dallas", "75201" },
            { "San Jose", "95101" },
            
            // Canada
            { "Toronto", "M5H 2N2" },
            { "Montreal", "H2Y 1C6" },
            { "Vancouver", "V6B 1A1" },
            { "Calgary", "T2P 0Y5" },
            { "Edmonton", "T5J 0N3" },
            { "Ottawa", "K1A 0A6" },
            { "Winnipeg", "R3C 0A5" },
            { "Quebec City", "G1R 4S9" },
            { "Hamilton", "L8N 3Z1" },
            { "Kitchener", "N2G 1C5" },
            
            // United Kingdom
            { "London", "SW1A 1AA" },
            { "Birmingham", "B1 1AA" },
            { "Manchester", "M1 1AA" },
            { "Glasgow", "G1 1AA" },
            { "Liverpool", "L1 1AA" },
            { "Leeds", "LS1 1AA" },
            { "Sheffield", "S1 1AA" },
            { "Edinburgh", "EH1 1AA" },
            { "Bristol", "BS1 1AA" },
            { "Leicester", "LE1 1AA" },
            
            // Australia
            { "Sydney", "2000" },
            { "Melbourne", "3000" },
            { "Brisbane", "4000" },
            { "Perth", "6000" },
            { "Adelaide", "5000" },
            { "Gold Coast", "4217" },
            { "Canberra", "2600" },
            { "Newcastle", "2300" },
            { "Wollongong", "2500" },
            { "Logan City", "4114" },
            
            // Japan
            { "Tokyo", "100-0001" },
            { "Yokohama", "220-0001" },
            { "Osaka", "530-0001" },
            { "Nagoya", "450-0001" },
            { "Sapporo", "060-0001" },
            { "Fukuoka", "810-0001" },
            { "Kobe", "650-0001" },
            { "Kyoto", "600-8216" },
            { "Kawasaki", "210-0001" },
            { "Saitama", "330-0801" },
            
            // Singapore
            { "Central Area", "018956" },
            { "Woodlands", "730888" },
            { "Tampines", "520201" },
            { "Jurong West", "640681" },
            { "Bedok", "460201" },
            { "Hougang", "530201" },
            { "Choa Chu Kang", "680201" },
            { "Yishun", "760201" },
            { "Sengkang", "540201" },
            { "Punggol", "820201" }
        };

        public AddCustomerList()
        {
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();
            SetupComboBoxes();

            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => SaveCustomer();

            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;
        }

        private void SetupPhoneFields()
        {
            txtPhone.PlaceholderText = "09XX XXX XXXX";
            txtContactNum.PlaceholderText = "09XX XXX XXXX";
            txtPhone.MaxLength = 11;
            txtContactNum.MaxLength = 11;
        }

        private void SetupNumberOnlyFields()
        {
            txtPhone.KeyPress += PhoneNumber_KeyPress;
            txtContactNum.KeyPress += PhoneNumber_KeyPress;
            txtBZip.KeyPress += NumberOnly_KeyPress;
            txtSZip.KeyPress += NumberOnly_KeyPress;
        }

        private void SetupComboBoxes()
        {
            cmbTitle.Items.AddRange(new string[] { "Mr.", "Mrs.", "Ms.", "Other" });
            cmbTitle.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbPayment.Items.AddRange(new string[] { "Net 30", "Net 15", "Net 60", "Cash", "Due upon receipt" });

            string[] countries = { "Philippines", "United States", "Canada", "United Kingdom", "Australia", "Japan", "Singapore" };
            cmbBCountry.Items.AddRange(countries);
            cmbSCountry.Items.AddRange(countries);
            cmbBCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSCountry.DropDownStyle = ComboBoxStyle.DropDownList;

            // Setup city comboboxes as dropdowns
            cmbBCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSCity.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add event handlers for country selection change
            cmbBCountry.SelectedIndexChanged += CmbBCountry_SelectedIndexChanged;
            cmbSCountry.SelectedIndexChanged += CmbSCountry_SelectedIndexChanged;

            // Add event handlers for city selection change
            cmbBCity.SelectedIndexChanged += CmbBCity_SelectedIndexChanged;
            cmbSCity.SelectedIndexChanged += CmbSCity_SelectedIndexChanged;

            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;
        }

        private void CmbBCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCitiesForCountry(cmbBCountry, cmbBCity);
        }

        private void CmbSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCitiesForCountry(cmbSCountry, cmbSCity);
        }

        private void CmbBCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadZipCodeForCity(cmbBCity, txtBZip);
        }

        private void CmbSCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadZipCodeForCity(cmbSCity, txtSZip);
        }

        private void LoadCitiesForCountry(ComboBox countryCombo, ComboBox cityCombo)
        {
            cityCombo.Items.Clear();
            cityCombo.Text = "";

            if (countryCombo.SelectedItem != null)
            {
                string selectedCountry = countryCombo.SelectedItem.ToString();

                if (citiesByCountry.ContainsKey(selectedCountry))
                {
                    cityCombo.Items.AddRange(citiesByCountry[selectedCountry]);
                }
            }
        }

        private void LoadZipCodeForCity(ComboBox cityCombo, Guna2TextBox zipTextBox)
        {
            if (cityCombo.SelectedItem != null)
            {
                string selectedCity = cityCombo.SelectedItem.ToString();

                if (zipCodesByCity.ContainsKey(selectedCity))
                {
                    zipTextBox.Text = zipCodesByCity[selectedCity];
                }
                else
                {
                    zipTextBox.Text = "";
                }
            }
            else
            {
                zipTextBox.Text = "";
            }
        }

        private void PhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

            // Allow backspace, delete, etc.
            if (char.IsControl(e.KeyChar)) return;

            // Only allow digits
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Limit to 11 digits
            if (txt.Text.Length >= 11)
            {
                e.Handled = true;
            }
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

            // Allow backspace, delete, etc.
            if (char.IsControl(e.KeyChar)) return;

            // Allow digits, +, and space
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != ' ')
            {
                e.Handled = true;
                return;
            }

            // Only allow + at the beginning
            if (e.KeyChar == '+' && txt.TextLength > 0)
            {
                e.Handled = true;
                return;
            }

            // Prevent more than one +
            if (e.KeyChar == '+' && txt.Text.Contains("+"))
            {
                e.Handled = true;
            }
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

        private void LnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbSCountry.SelectedItem = cmbBCountry.SelectedItem;
            cmbSCity.SelectedItem = cmbBCity.SelectedItem;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
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

        // EMAIL VALIDATION
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // PHONE VALIDATION: EXACTLY 11 digits
        private bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            // Remove spaces and dashes
            string cleaned = Regex.Replace(phone.Trim(), @"[\s-]", "");

            // Must be exactly 11 digits starting with 09
            return Regex.IsMatch(cleaned, @"^09\d{9}$");
        }

        private void SaveCustomer()
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO customers 
                                    (Title, FirstName, LastName, CompanyName, Email, PaymentTerms, PhoneNo, 
                                     ContactPerson, ContactDetail, Country, City, ZipCode, Add1, Add2, Status)
                                    VALUES 
                                    (@Title, @FirstName, @LastName, @CompanyName, @Email, @PaymentTerms, @PhoneNo, 
                                     @ContactPerson, @ContactDetail, @Country, @City, @ZipCode, @Add1, @Add2, @Status)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", cmbTitle.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@FirstName", txtFName.Text.Trim());
                        command.Parameters.AddWithValue("@LastName", txtLName.Text.Trim());
                        command.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@PaymentTerms", cmbPayment.SelectedItem?.ToString() ?? "Net 30");
                        command.Parameters.AddWithValue("@PhoneNo", txtPhone.Text.Trim());
                        command.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        command.Parameters.AddWithValue("@ContactDetail", txtContactNum.Text.Trim());
                        command.Parameters.AddWithValue("@Country", cmbBCountry.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@City", cmbBCity.SelectedItem?.ToString() ?? "");
                        command.Parameters.AddWithValue("@ZipCode", txtBZip.Text.Trim());
                        command.Parameters.AddWithValue("@Add1", txtBLine1.Text.Trim());
                        command.Parameters.AddWithValue("@Add2", txtBLine2.Text.Trim());
                        command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem?.ToString() ?? "Active");

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer saved successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CloseForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save customer.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Title
            if (cmbTitle.SelectedItem == null) { MessageBox.Show("Please select a Title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbTitle.Focus(); return false; }

            // First Name
            if (string.IsNullOrWhiteSpace(txtFName.Text)) { MessageBox.Show("First Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtFName.Focus(); return false; }

            // Last Name
            if (string.IsNullOrWhiteSpace(txtLName.Text)) { MessageBox.Show("Last Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtLName.Focus(); return false; }

            // Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) { MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEmail.Focus(); return false; }
            if (!IsValidEmail(txtEmail.Text.Trim())) { MessageBox.Show("Please enter a valid email address.\nExample: john.doe@company.com", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEmail.Focus(); txtEmail.SelectAll(); return false; }

            // Company Name
            if (string.IsNullOrWhiteSpace(txtCompany.Text)) { MessageBox.Show("Company Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCompany.Focus(); return false; }

            // MAIN PHONE NUMBER - EXACTLY 11 DIGITS
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) { MessageBox.Show("Phone Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPhone.Focus(); return false; }
            if (!IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Invalid Phone Number!\nMust be exactly 11 digits starting with 09.\nExample: 09171234567", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                txtPhone.SelectAll();
                return false;
            }

            // Payment Terms
            if (cmbPayment.SelectedItem == null) { MessageBox.Show("Please select Payment Terms.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbPayment.Focus(); return false; }

            // Contact Person
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text)) { MessageBox.Show("Contact Person is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContactPerson.Focus(); return false; }

            // CONTACT NUMBER - ALSO EXACTLY 11 DIGITS
            if (string.IsNullOrWhiteSpace(txtContactNum.Text)) { MessageBox.Show("Contact Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtContactNum.Focus(); return false; }
            if (!IsValidPhoneNumber(txtContactNum.Text))
            {
                MessageBox.Show("Invalid Contact Number!\nMust be exactly 11 digits starting with 09.\nExample: 09171234567", "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNum.Focus();
                txtContactNum.SelectAll();
                return false;
            }

            // Billing Address
            if (cmbBCountry.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Billing Country is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbBCountry.Focus(); return false; }
            if (cmbBCity.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Billing City is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbBCity.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtBZip.Text)) { btnAddress.PerformClick(); MessageBox.Show("Billing Zip Code is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBZip.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtBLine1.Text)) { btnAddress.PerformClick(); MessageBox.Show("Billing Address Line 1 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBLine1.Focus(); return false; }

            // Shipping Address - NOT REQUIRED, user can leave it empty
            // No validation for shipping address

            return true;
        }
    }
}