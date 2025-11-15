using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCustOrder : Form
    {
        private decimal subtotal = 0;
        private decimal discount = 0;
        private decimal shippingFee = 0;

        // Additional controls needed
        private Label lblSubtotalLabel;
        private Label lblSubtotalValue;
        private Label lblDiscountLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtDiscount;
        private Guna.UI2.WinForms.Guna2Button btnDiscountUp;
        private Guna.UI2.WinForms.Guna2Button btnDiscountDown;
        private Label lblShippingLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtShipping;
        private Guna.UI2.WinForms.Guna2Button btnShippingUp;
        private Guna.UI2.WinForms.Guna2Button btnShippingDown;
        private Label lblTotalLabel;
        private Label lblTotalValue;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSaveOrder;

        // Address tab controls
        private Guna.UI2.WinForms.Guna2ShadowPanel addressPanel;
        private Label lblBillingAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtBillingAddress;
        private Label lblShippingAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtShippingAddress;

        public AddCustOrder()
        {
            InitializeComponent();
            InitializeAdditionalControls();
            InitializeFormSettings();
            LoadInitialData();
            AttachEventHandlers();
        }

        private void InitializeAdditionalControls()
        {
            // Create the totals section at the bottom right (after the table)
            int startX = 972;
            int startY = 1280;

            // SubTotal Label
            lblSubtotalLabel = new Label();
            lblSubtotalLabel.Text = "SubTotal:";
            lblSubtotalLabel.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblSubtotalLabel.Location = new Point(startX, startY);
            lblSubtotalLabel.Size = new Size(100, 30);
            lblSubtotalLabel.TextAlign = ContentAlignment.MiddleLeft;

            // SubTotal Value
            lblSubtotalValue = new Label();
            lblSubtotalValue.Text = "₱0.00";
            lblSubtotalValue.Font = new Font("Tahoma", 10F);
            lblSubtotalValue.Location = new Point(startX + 190, startY);
            lblSubtotalValue.Size = new Size(150, 30);
            lblSubtotalValue.TextAlign = ContentAlignment.MiddleRight;

            // Discount Label
            lblDiscountLabel = new Label();
            lblDiscountLabel.Text = "Discount (%):";
            lblDiscountLabel.Font = new Font("Tahoma", 10F);
            lblDiscountLabel.Location = new Point(startX, startY + 40);
            lblDiscountLabel.Size = new Size(120, 30);
            lblDiscountLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Discount TextBox
            txtDiscount = new Guna.UI2.WinForms.Guna2TextBox();
            txtDiscount.Location = new Point(startX + 160, startY + 40);
            txtDiscount.Size = new Size(80, 36);
            txtDiscount.Text = "0";
            txtDiscount.TextAlign = HorizontalAlignment.Center;
            txtDiscount.BorderRadius = 5;
            txtDiscount.TextChanged += (s, e) => CalculateTotals();

            // Discount Up/Down buttons
            btnDiscountUp = new Guna.UI2.WinForms.Guna2Button();
            btnDiscountUp.Text = "▲";
            btnDiscountUp.Size = new Size(30, 18);
            btnDiscountUp.Location = new Point(startX + 245, startY + 40);
            btnDiscountUp.FillColor = Color.FromArgb(94, 148, 255);
            btnDiscountUp.Click += (s, e) => AdjustDiscount(1);

            btnDiscountDown = new Guna.UI2.WinForms.Guna2Button();
            btnDiscountDown.Text = "▼";
            btnDiscountDown.Size = new Size(30, 18);
            btnDiscountDown.Location = new Point(startX + 245, startY + 58);
            btnDiscountDown.FillColor = Color.FromArgb(94, 148, 255);
            btnDiscountDown.Click += (s, e) => AdjustDiscount(-1);

            // Shipping Fee Label
            lblShippingLabel = new Label();
            lblShippingLabel.Text = "Shipping Fee:";
            lblShippingLabel.Font = new Font("Tahoma", 10F);
            lblShippingLabel.Location = new Point(startX, startY + 85);
            lblShippingLabel.Size = new Size(120, 30);
            lblShippingLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Shipping TextBox
            txtShipping = new Guna.UI2.WinForms.Guna2TextBox();
            txtShipping.Location = new Point(startX + 160, startY + 85);
            txtShipping.Size = new Size(80, 36);
            txtShipping.Text = "0";
            txtShipping.TextAlign = HorizontalAlignment.Center;
            txtShipping.BorderRadius = 5;
            txtShipping.TextChanged += (s, e) => CalculateTotals();

            // Shipping Up/Down buttons
            btnShippingUp = new Guna.UI2.WinForms.Guna2Button();
            btnShippingUp.Text = "▲";
            btnShippingUp.Size = new Size(30, 18);
            btnShippingUp.Location = new Point(startX + 245, startY + 85);
            btnShippingUp.FillColor = Color.FromArgb(94, 148, 255);
            btnShippingUp.Click += (s, e) => AdjustShipping(10);

            btnShippingDown = new Guna.UI2.WinForms.Guna2Button();
            btnShippingDown.Text = "▼";
            btnShippingDown.Size = new Size(30, 18);
            btnShippingDown.Location = new Point(startX + 245, startY + 103);
            btnShippingDown.FillColor = Color.FromArgb(94, 148, 255);
            btnShippingDown.Click += (s, e) => AdjustShipping(-10);

            // Total Label
            lblTotalLabel = new Label();
            lblTotalLabel.Text = "Total:";
            lblTotalLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold);
            lblTotalLabel.Location = new Point(startX, startY + 135);
            lblTotalLabel.Size = new Size(100, 35);
            lblTotalLabel.TextAlign = ContentAlignment.MiddleLeft;

            // Total Value
            lblTotalValue = new Label();
            lblTotalValue.Text = "₱0.00";
            lblTotalValue.Font = new Font("Tahoma", 12F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(94, 148, 255);
            lblTotalValue.Location = new Point(startX + 190, startY + 135);
            lblTotalValue.Size = new Size(150, 35);
            lblTotalValue.TextAlign = ContentAlignment.MiddleRight;

            // Cancel Button
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnCancel.Text = "Cancel";
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.Size = new Size(120, 50);
            btnCancel.Location = new Point(1163, 1620);
            btnCancel.BorderRadius = 5;
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.Click += BtnCancel_Click;

            // Save Order Button
            btnSaveOrder = new Guna.UI2.WinForms.Guna2Button();
            btnSaveOrder.Text = "Save Customer Order";
            btnSaveOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSaveOrder.Size = new Size(200, 50);
            btnSaveOrder.Location = new Point(1289, 1620);
            btnSaveOrder.BorderRadius = 5;
            btnSaveOrder.FillColor = Color.FromArgb(0, 123, 255);
            btnSaveOrder.Click += BtnSaveOrder_Click;

            // Add all controls to main panel
            mainpanel.Controls.Add(lblSubtotalLabel);
            mainpanel.Controls.Add(lblSubtotalValue);
            mainpanel.Controls.Add(lblDiscountLabel);
            mainpanel.Controls.Add(txtDiscount);
            mainpanel.Controls.Add(btnDiscountUp);
            mainpanel.Controls.Add(btnDiscountDown);
            mainpanel.Controls.Add(lblShippingLabel);
            mainpanel.Controls.Add(txtShipping);
            mainpanel.Controls.Add(btnShippingUp);
            mainpanel.Controls.Add(btnShippingDown);
            mainpanel.Controls.Add(lblTotalLabel);
            mainpanel.Controls.Add(lblTotalValue);
            mainpanel.Controls.Add(btnCancel);
            mainpanel.Controls.Add(btnSaveOrder);

            // Create Address Panel
            CreateAddressPanel();
        }

        private void CreateAddressPanel()
        {
            addressPanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            addressPanel.BackColor = Color.Transparent;
            addressPanel.FillColor = Color.White;
            addressPanel.Location = new Point(102, 183);
            addressPanel.Name = "addressPanel";
            addressPanel.Radius = 8;
            addressPanel.ShadowColor = Color.Black;
            addressPanel.Size = new Size(1440, 350);
            addressPanel.Visible = false;

            // Billing Address Label
            lblBillingAddress = new Label();
            lblBillingAddress.Text = "Billing Address *";
            lblBillingAddress.Font = new Font("Tahoma", 10F);
            lblBillingAddress.Location = new Point(30, 30);
            lblBillingAddress.Size = new Size(140, 21);

            // Billing Address TextBox
            txtBillingAddress = new Guna.UI2.WinForms.Guna2TextBox();
            txtBillingAddress.BorderRadius = 5;
            txtBillingAddress.Location = new Point(30, 60);
            txtBillingAddress.Multiline = true;
            txtBillingAddress.Size = new Size(650, 120);
            txtBillingAddress.PlaceholderText = "Enter billing address";
            txtBillingAddress.Font = new Font("Segoe UI", 10F);

            // Shipping Address Label
            lblShippingAddress = new Label();
            lblShippingAddress.Text = "Shipping Address *";
            lblShippingAddress.Font = new Font("Tahoma", 10F);
            lblShippingAddress.Location = new Point(720, 30);
            lblShippingAddress.Size = new Size(160, 21);

            // Shipping Address TextBox
            txtShippingAddress = new Guna.UI2.WinForms.Guna2TextBox();
            txtShippingAddress.BorderRadius = 5;
            txtShippingAddress.Location = new Point(720, 60);
            txtShippingAddress.Multiline = true;
            txtShippingAddress.Size = new Size(680, 120);
            txtShippingAddress.PlaceholderText = "Enter shipping address";
            txtShippingAddress.Font = new Font("Segoe UI", 10F);

            // Add controls to address panel
            addressPanel.Controls.Add(lblBillingAddress);
            addressPanel.Controls.Add(txtBillingAddress);
            addressPanel.Controls.Add(lblShippingAddress);
            addressPanel.Controls.Add(txtShippingAddress);

            // Add address panel to main panel
            mainpanel.Controls.Add(addressPanel);
            addressPanel.BringToFront();
        }

        private void InitializeFormSettings()
        {
            // Enable scrolling for the form
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 2100);
            this.WindowState = FormWindowState.Maximized;

            // Set date fields with current date
            txtboxorder.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtboxorder.ReadOnly = true;

            // Configure delivery date as date picker style
            txtboxdelivery.PlaceholderText = "dd/mm/yyyy";

            // Format DataGridView
            dgvItems.AutoGenerateColumns = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;
            dgvItems.RowTemplate.Height = 40;

            // Update button styles to match the image
            guna2Button1.FillColor = Color.FromArgb(94, 148, 255);
            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.Black;
            guna2Button2.BorderColor = Color.FromArgb(213, 218, 223);
            guna2Button2.BorderThickness = 1;
        }

        private void LoadInitialData()
        {
            // Load payment terms
            comboxpayment.Items.Clear();
            comboxpayment.Items.AddRange(new string[] {
                "Select payment terms",
                "Cash on Delivery",
                "Net 30",
                "Net 60",
                "Credit Card",
                "Bank Transfer"
            });
            comboxpayment.SelectedIndex = 0;

            // Load sample companies
            comboxcompanyname.Items.Clear();
            comboxcompanyname.Items.AddRange(new string[] {
                "Select a company",
                "Company A",
                "Company B",
                "Company C",
                "Company D"
            });
            comboxcompanyname.SelectedIndex = 0;
        }

        private void AttachEventHandlers()
        {
            btnAddProduct.Click += BtnAddProduct_Click;
            guna2Button3.Click += BtnSearch_Click;
            comboxcompanyname.SelectedIndexChanged += ComboxCompanyName_SelectedIndexChanged;
            txtSearch.KeyPress += TxtSearch_KeyPress;

            // Tab switching
            guna2Button1.Click += BtnCustomerInfo_Click;
            guna2Button2.Click += BtnAddress_Click;
        }

        private void BtnCustomerInfo_Click(object sender, EventArgs e)
        {
            // Show Customer Information tab
            guna2ShadowPanel2.Visible = true;
            addressPanel.Visible = false;

            // Update button styles
            guna2Button1.FillColor = Color.FromArgb(94, 148, 255);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.BorderThickness = 0;

            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.Black;
            guna2Button2.BorderColor = Color.FromArgb(213, 218, 223);
            guna2Button2.BorderThickness = 1;
        }

        private void BtnAddress_Click(object sender, EventArgs e)
        {
            // Show Address tab
            guna2ShadowPanel2.Visible = false;
            addressPanel.Visible = true;

            // Update button styles
            guna2Button2.FillColor = Color.FromArgb(94, 148, 255);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.BorderThickness = 0;

            guna2Button1.FillColor = Color.White;
            guna2Button1.ForeColor = Color.Black;
            guna2Button1.BorderColor = Color.FromArgb(213, 218, 223);
            guna2Button1.BorderThickness = 1;
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            // Open the NewCustOrderModal form
            using (NewCustOrderModal modal = new NewCustOrderModal())
            {
                // Set the modal to be displayed in the center of the parent form
                modal.StartPosition = FormStartPosition.CenterParent;

                // Show the modal dialog
                DialogResult result = modal.ShowDialog(this);

                // If the user confirmed adding products in the modal
                if (result == DialogResult.OK)
                {
                    // Get the selected products from the modal and add them to dgvItems
                    AddProductsFromModal(modal);

                    // Recalculate totals after adding products
                    CalculateTotals();
                }
            }
        }

        private void AddProductsFromModal(NewCustOrderModal modal)
        {
            if (modal.SelectedProducts != null && modal.SelectedProducts.Count > 0)
            {
                foreach (var product in modal.SelectedProducts)
                {
                    int rowIndex = dgvItems.Rows.Add();
                    DataGridViewRow row = dgvItems.Rows[rowIndex];

                    row.Cells["ProductName"].Value = product.Name;
                    row.Cells["Quantity"].Value = product.Quantity.ToString();
                    row.Cells["SellingPrice"].Value = product.Price.ToString("0.00");
                    row.Cells["AvailableQuantity"].Value = product.AvailableStock.ToString();

                    // Calculate subtotal
                    decimal subtotal = product.Quantity * product.Price;
                    row.Cells["Subtotal"].Value = subtotal.ToString("0.00");
                }

                MessageBox.Show($"{modal.SelectedProducts.Count} product(s) added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddSampleProduct()
        {
            int rowIndex = dgvItems.Rows.Add();
            DataGridViewRow row = dgvItems.Rows[rowIndex];

            row.Cells["ProductName"].Value = "Sample Product";
            row.Cells["Quantity"].Value = "1";
            row.Cells["SellingPrice"].Value = "100.00";
            row.Cells["AvailableQuantity"].Value = "50";
            row.Cells["Subtotal"].Value = "100.00";

            CalculateTotals();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                string productName = row.Cells["ProductName"].Value?.ToString().ToLower() ?? "";
                row.Visible = productName.Contains(searchText);
            }
        }

        private void ComboxCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxcompanyname.SelectedIndex > 0)
            {
                string selectedCompany = comboxcompanyname.SelectedItem.ToString();
                LoadCompanyDetails(selectedCompany);
            }
            else
            {
                ClearContactFields();
            }
        }

        private void LoadCompanyDetails(string companyName)
        {
            // Simulate loading company data
            switch (companyName)
            {
                case "Company A":
                    txtboxperson.Text = "John Doe";
                    txtboxdetail.Text = "123-456-7890";
                    txtboxemail.Text = "john@companya.com";
                    comboxpayment.SelectedIndex = 1;
                    break;
                case "Company B":
                    txtboxperson.Text = "Jane Smith";
                    txtboxdetail.Text = "098-765-4321";
                    txtboxemail.Text = "jane@companyb.com";
                    comboxpayment.SelectedIndex = 2;
                    break;
                default:
                    ClearContactFields();
                    break;
            }
        }

        private void ClearContactFields()
        {
            txtboxperson.Clear();
            txtboxdetail.Clear();
            txtboxemail.Clear();
            comboxpayment.SelectedIndex = 0;
        }

        private void AdjustDiscount(decimal change)
        {
            decimal currentDiscount;
            if (decimal.TryParse(txtDiscount.Text, out currentDiscount))
            {
                currentDiscount += change;
                if (currentDiscount < 0) currentDiscount = 0;
                if (currentDiscount > 100) currentDiscount = 100;
                txtDiscount.Text = currentDiscount.ToString("0");
            }
        }

        private void AdjustShipping(decimal change)
        {
            decimal currentShipping;
            if (decimal.TryParse(txtShipping.Text, out currentShipping))
            {
                currentShipping += change;
                if (currentShipping < 0) currentShipping = 0;
                txtShipping.Text = currentShipping.ToString("0");
            }
        }

        private void CalculateTotals()
        {
            subtotal = 0;

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    decimal rowSubtotal;
                    string value = row.Cells["Subtotal"].Value.ToString().Replace(",", "");
                    if (decimal.TryParse(value, out rowSubtotal))
                    {
                        subtotal += rowSubtotal;
                    }
                }
            }

            // Parse discount and shipping
            decimal.TryParse(txtDiscount.Text, out discount);
            decimal.TryParse(txtShipping.Text, out shippingFee);

            // Calculate discount amount
            decimal discountAmount = subtotal * (discount / 100);
            decimal total = subtotal - discountAmount + shippingFee;

            // Update labels
            lblSubtotalValue.Text = "₱" + subtotal.ToString("N2");
            lblTotalValue.Text = "₱" + total.ToString("N2");
        }

        private bool ValidateForm()
        {
            if (comboxcompanyname.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a company name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboxcompanyname.Focus();
                return false;
            }

            if (comboxpayment.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select payment terms.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboxpayment.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtboxperson.Text))
            {
                MessageBox.Show("Please enter contact person.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxperson.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtboxdetail.Text))
            {
                MessageBox.Show("Please enter contact details.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxdetail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtboxemail.Text) || !IsValidEmail(txtboxemail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxemail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtboxdelivery.Text))
            {
                MessageBox.Show("Please enter delivery date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtboxdelivery.Focus();
                return false;
            }

            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one product to the order.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void BtnSaveOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                // Save order to database
                SaveOrderToDatabase();

                MessageBox.Show("Customer order saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Navigate back to CustOrder and refresh the table
                NavigateToCustOrder(refreshData: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel? All unsaved changes will be lost.",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Navigate back to CustOrder
                NavigateToCustOrder();
            }
        }

        private void NavigateToCustOrder(bool refreshData = false)
        {
            // Get the parent form (Form1)
            Form1 parentForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Customer Orders";

                // Create CustOrder form
                CustOrder custOrderForm = new CustOrder();
                custOrderForm.TopLevel = false;
                custOrderForm.FormBorderStyle = FormBorderStyle.None;
                custOrderForm.Dock = DockStyle.Fill;

                // If we need to refresh data, pass the saved order information
                if (refreshData)
                {
                    custOrderForm.AddNewOrderToTable(GetOrderData());
                }

                // Clear the content panel and add CustOrder
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(custOrderForm);
                custOrderForm.Show();
            }
            else
            {
                // Fallback: just close this form
                this.Close();
            }
        }

        private void SaveOrderToDatabase()
        {
            // TODO: Implement actual database save logic
            System.Threading.Thread.Sleep(500); // Simulate save delay
        }

        private OrderData GetOrderData()
        {
            // Calculate final totals
            decimal.TryParse(txtDiscount.Text, out discount);
            decimal.TryParse(txtShipping.Text, out shippingFee);
            decimal discountAmount = subtotal * (discount / 100);
            decimal total = subtotal - discountAmount + shippingFee;

            // Create order data object
            return new OrderData
            {
                OrderNumber = GenerateOrderNumber(),
                CompanyName = comboxcompanyname.SelectedItem.ToString(),
                ContactPerson = txtboxperson.Text,
                ContactDetails = txtboxdetail.Text,
                Email = txtboxemail.Text,
                OrderDate = txtboxorder.Text,
                DeliveryDate = txtboxdelivery.Text,
                PaymentTerms = comboxpayment.SelectedItem.ToString(),
                Subtotal = subtotal,
                Discount = discount,
                ShippingFee = shippingFee,
                Total = total,
                Status = "Pending",
                ItemCount = dgvItems.Rows.Count
            };
        }

        private string GenerateOrderNumber()
        {
            // Generate order number: ORD-YYYYMMDD-XXXX
            string date = DateTime.Now.ToString("yyyyMMdd");
            Random rand = new Random();
            string number = rand.Next(1000, 9999).ToString();
            return $"ORD-{date}-{number}";
        }

        // Order data class
        public class OrderData
        {
            public string OrderNumber { get; set; }
            public string CompanyName { get; set; }
            public string ContactPerson { get; set; }
            public string ContactDetails { get; set; }
            public string Email { get; set; }
            public string OrderDate { get; set; }
            public string DeliveryDate { get; set; }
            public string PaymentTerms { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Discount { get; set; }
            public decimal ShippingFee { get; set; }
            public decimal Total { get; set; }
            public string Status { get; set; }
            public int ItemCount { get; set; }
        }

        private void ClearForm()
        {
            comboxcompanyname.SelectedIndex = 0;
            comboxpayment.SelectedIndex = 0;
            txtboxperson.Clear();
            txtboxdetail.Clear();
            txtboxemail.Clear();
            txtboxorder.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtboxdelivery.Clear();
            txtBillingAddress.Clear();
            txtShippingAddress.Clear();
            dgvItems.Rows.Clear();
            txtSearch.Clear();
            txtDiscount.Text = "0";
            txtShipping.Text = "0";
            CalculateTotals();

            // Switch back to Customer Information tab
            BtnCustomerInfo_Click(null, null);
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void comboxcompanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboxCompanyName_SelectedIndexChanged(sender, e);
        }
    }
}