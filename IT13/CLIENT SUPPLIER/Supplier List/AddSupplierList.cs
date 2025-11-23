using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace IT13
{
    public partial class AddSupplierList : Form
    {
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        // Cities and Zip Codes
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

        private Dictionary<string, string> zipCodesByCity = new Dictionary<string, string>
        {
            // Philippines
            { "Manila", "1000" }, { "Quezon City", "1100" }, { "Makati", "1200" }, { "Cebu City", "6000" }, { "Davao City", "8000" },
            { "Taguig", "1630" }, { "Pasig", "1600" }, { "Caloocan", "1400" }, { "Zamboanga City", "7000" }, { "Antipolo", "1870" },
            // United States
            { "New York", "10001" }, { "Los Angeles", "90001" }, { "Chicago", "60601" }, { "Houston", "77001" }, { "Phoenix", "85001" },
            { "Philadelphia", "19101" }, { "San Antonio", "78201" }, { "San Diego", "92101" }, { "Dallas", "75201" }, { "San Jose", "95101" },
            // Canada
            { "Toronto", "M5H 2N2" }, { "Montreal", "H2Y 1C6" }, { "Vancouver", "V6B 1A1" }, { "Calgary", "T2P 0Y5" }, { "Edmonton", "T5J 0N3" },
            // United Kingdom
            { "London", "SW1A 1AA" }, { "Birmingham", "B1 1AA" }, { "Manchester", "M1 1AA" }, { "Glasgow", "G1 1AA" }, { "Liverpool", "L1 1AA" },
            // Australia
            { "Sydney", "2000" }, { "Melbourne", "3000" }, { "Brisbane", "4000" }, { "Perth", "6000" }, { "Adelaide", "5000" },
            // Japan
            { "Tokyo", "100-0001" }, { "Yokohama", "220-0001" }, { "Osaka", "530-0001" }, { "Nagoya", "450-0001" }, { "Sapporo", "060-0001" },
            // Singapore
            { "Central Area", "018956" }, { "Woodlands", "730888" }, { "Tampines", "520201" }, { "Jurong West", "640681" }, { "Bedok", "460201" }
        };

        public AddSupplierList()
        {
            InitializeComponent();
            SetupControls();
            WireEvents();
            ShowPanel(pnlOther, pnlAddress, pnlRemarks);
        }

        private void SetupControls()
        {
            // Phone fields
            txtPhone.PlaceholderText = "09XX XXX XXXX";
            txtContactNum.PlaceholderText = "09XX XXX XXXX";
            txtPhone.MaxLength = txtContactNum.MaxLength = 11;

            // Title ComboBox
            if (cmbTitle.Items.Count == 0)
                cmbTitle.Items.AddRange(new string[] { "Mr.", "Mrs.", "Ms.", "Other" });
            cmbTitle.DropDownStyle = ComboBoxStyle.DropDownList;

            // Payment & Status
            if (cmbPayment.Items.Count == 0)
                cmbPayment.Items.AddRange(new string[] { "Net 30", "Net 15", "Net 60", "Cash", "Due upon receipt" });
            cmbPayment.DropDownStyle = ComboBoxStyle.DropDownList;

            if (cmbStatus.Items.Count == 0)
                cmbStatus.Items.AddRange(new string[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;

            // Countries
            string[] countries = { "Philippines", "United States", "Canada", "United Kingdom", "Australia", "Japan", "Singapore" };
            cmbBCountry.Items.AddRange(countries);
            cmbSCountry.Items.AddRange(countries);
            cmbBCountry.DropDownStyle = cmbSCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBCity.DropDownStyle = cmbSCity.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void WireEvents()
        {
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress, pnlRemarks);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther, pnlRemarks);
            btnRemarks.Click += (s, e) => ShowPanel(pnlRemarks, pnlOther, pnlAddress);

            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => SaveSupplier();

            // Country -> City -> Zip
            cmbBCountry.SelectedIndexChanged += CmbBCountry_SelectedIndexChanged;
            cmbSCountry.SelectedIndexChanged += CmbSCountry_SelectedIndexChanged;
            cmbBCity.SelectedIndexChanged += CmbBCity_SelectedIndexChanged;
            cmbSCity.SelectedIndexChanged += CmbSCity_SelectedIndexChanged;

            // Fixed: Safe handling for Guna2TextBox
            txtPhone.KeyPress += PhoneNumber_KeyPress;
            txtContactNum.KeyPress += PhoneNumber_KeyPress;
            txtBZip.KeyPress += NumberOnly_KeyPress;
            txtSZip.KeyPress += NumberOnly_KeyPress;
        }

        private void CmbBCountry_SelectedIndexChanged(object sender, EventArgs e) => LoadCitiesForCountry(cmbBCountry, cmbBCity);
        private void CmbSCountry_SelectedIndexChanged(object sender, EventArgs e) => LoadCitiesForCountry(cmbSCountry, cmbSCity);
        private void CmbBCity_SelectedIndexChanged(object sender, EventArgs e) => LoadZipCodeForCity(cmbBCity, txtBZip);
        private void CmbSCity_SelectedIndexChanged(object sender, EventArgs e) => LoadZipCodeForCity(cmbSCity, txtSZip);

        private void LoadCitiesForCountry(Guna2ComboBox countryCombo, Guna2ComboBox cityCombo)
        {
            cityCombo.Items.Clear();
            cityCombo.Text = "";
            if (countryCombo.SelectedItem is string country && citiesByCountry.TryGetValue(country, out var cities))
                cityCombo.Items.AddRange(cities);
        }

        private void LoadZipCodeForCity(Guna2ComboBox cityCombo, Guna2TextBox zipBox)
        {
            zipBox.Text = cityCombo.SelectedItem is string city && zipCodesByCity.TryGetValue(city, out var zip)
                ? zip : "";
        }

        // FIXED: No more casting to System.Windows.Forms.TextBox
        private void PhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is not Guna2TextBox textBox) return;

            // Allow only digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            // Limit to 11 digits
            if (textBox.Text.Length >= 11 && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is not Guna2TextBox textBox) return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != ' ')
                e.Handled = true;

            if (e.KeyChar == '+' && textBox.SelectionStart != 0)
                e.Handled = true;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = hide2.Visible = false;
            show.Visible = true;

            btnOther.FillColor = btnAddress.FillColor = btnRemarks.FillColor = Color.WhiteSmoke;
            btnOther.ForeColor = btnAddress.ForeColor = btnRemarks.ForeColor = Color.Black;

            var activeBtn = show == pnlOther ? btnOther : show == pnlAddress ? btnAddress : btnRemarks;
            activeBtn.FillColor = Color.FromArgb(0, 123, 255);
            activeBtn.ForeColor = Color.White;
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
                parent.navBar1.PageTitle = "Supplier List";
                var supplierList = new SupplierList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(supplierList);
                supplierList.Show();
            }
            else
                this.Close();
        }

        private bool IsValidEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            string cleaned = Regex.Replace(phone.Trim(), @"[\s-]", "");
            return Regex.IsMatch(cleaned, @"^09\d{9}$");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFName.Text)) { MessageBox.Show("First Name is required."); txtFName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtLName.Text)) { MessageBox.Show("Last Name is required."); txtLName.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text)) { MessageBox.Show("Valid email is required."); txtEmail.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || !IsValidPhoneNumber(txtPhone.Text)) { MessageBox.Show("Phone must be 11 digits starting with 09."); txtPhone.Focus(); return false; }
            if (cmbPayment.SelectedItem == null) { MessageBox.Show("Select Payment Terms."); cmbPayment.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text)) { MessageBox.Show("Contact Person is required."); txtContactPerson.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtContactNum.Text) || !IsValidPhoneNumber(txtContactNum.Text)) { MessageBox.Show("Contact Number must be 11 digits starting with 09."); txtContactNum.Focus(); return false; }

            // Address validation
            if (cmbBCountry.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Billing Country required."); return false; }
            if (cmbBCity.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Billing City required."); return false; }
            if (string.IsNullOrWhiteSpace(txtBZip.Text)) { btnAddress.PerformClick(); MessageBox.Show("Billing Zip required."); return false; }
            if (string.IsNullOrWhiteSpace(txtBLine1.Text)) { btnAddress.PerformClick(); MessageBox.Show("Billing Address Line 1 required."); return false; }
            if (cmbSCountry.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Shipping Country required."); return false; }
            if (cmbSCity.SelectedItem == null) { btnAddress.PerformClick(); MessageBox.Show("Shipping City required."); return false; }
            if (string.IsNullOrWhiteSpace(txtSZip.Text)) { btnAddress.PerformClick(); MessageBox.Show("Shipping Zip required."); return false; }
            if (string.IsNullOrWhiteSpace(txtSLine1.Text)) { btnAddress.PerformClick(); MessageBox.Show("Shipping Address Line 1 required."); return false; }

            return true;
        }

        private void SaveSupplier()
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO suppliers
                        (Title, FirstName, LastName, CompanyName, Email, PaymentTerms, PhoneNo,
                         ContactPerson, ContactDetail, Country, City, ZipCode, Add1, Add2, Status, created_at, updated_at)
                        VALUES
                        (@Title, @FirstName, @LastName, @CompanyName, @Email, @PaymentTerms, @PhoneNo,
                         @ContactPerson, @ContactDetail, @Country, @City, @ZipCode, @Add1, @Add2, @Status, GETDATE(), GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", cmbTitle.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@FirstName", txtFName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", txtLName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@PaymentTerms", cmbPayment.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactDetail", txtContactNum.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", cmbBCountry.SelectedItem?.ToString() ?? "Philippines");
                        cmd.Parameters.AddWithValue("@City", cmbBCity.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@ZipCode", txtBZip.Text.Trim());
                        cmd.Parameters.AddWithValue("@Add1", txtBLine1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Add2", txtBLine2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Supplier saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CloseForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}