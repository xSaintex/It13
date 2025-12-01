using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IT13
{
    public partial class AddProd : Form
    {
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public AddProd()
        {
            InitializeComponent();

            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 0);

            // Wire events
            btncancel.Click += btncancel_Click;
            btnaddprod.Click += btnaddprod_Click;

            // Apply all styling
            ApplyModernStyling();
            LoadComboBoxData();
        }

        private void ApplyModernStyling()
        {
            // === REMOVE BREADCRUMB BUTTONS FROM VIEW ===
            btnhome.Visible = false;
            btnproductlist.Visible = false;
            btnadd.Visible = false;

            // === TEXTBOXES ===
            foreach (var tb in new[] { guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4 })
            {
                tb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                tb.ForeColor = Color.Black;
                tb.BorderRadius = 12;
                tb.BorderThickness = 1;
                tb.BorderColor = Color.FromArgb(180, 180, 180);
                tb.FillColor = Color.White;
                tb.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
                tb.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
                tb.PlaceholderForeColor = Color.Gray;
            }

            // Product Name
            guna2TextBox1.PlaceholderText = "Enter Product Name";
            guna2TextBox1.Text = "";

            // Unit Cost & Selling Price - Numbers only + placeholder
            guna2TextBox2.PlaceholderText = "0.00";
            guna2TextBox3.PlaceholderText = "0.00";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";

            // Description - Left-top aligned
            guna2TextBox4.PlaceholderText = "Enter product description...";
            guna2TextBox4.Text = "";
            guna2TextBox4.TextAlign = HorizontalAlignment.Left;
            guna2TextBox4.BorderRadius = 16;
            guna2TextBox4.Multiline = true;

            // === COMBOBOXES ===
            foreach (var cb in new[] { guna2ComboBox1, guna2ComboBox2, guna2ComboBox3 })
            {
                cb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                cb.ForeColor = Color.Black;
                cb.BorderRadius = 12;
                cb.BorderThickness = 1;
                cb.BorderColor = Color.FromArgb(180, 180, 180);
                cb.FillColor = Color.White;
                cb.FocusedColor = Color.FromArgb(94, 148, 255);
            }

            // === LABELS (except header) ===
            foreach (Control c in mainpanel.Controls)
            {
                if (c is Label lbl && lbl != label2)
                {
                    lbl.Font = new Font("Bahnschrift SemiCondensed", 11F);
                    lbl.ForeColor = Color.Black;
                }
            }

            // === BUTTONS - Poppins + Modern Style ===
            foreach (var btn in new[] { btnaddprod, btncancel })
            {
                btn.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
                btn.BorderRadius = 12;
                btn.FillColor = btn == btnaddprod ? Color.FromArgb(0, 123, 255) : Color.FromArgb(220, 53, 69);
                btn.ForeColor = Color.White;
                btn.HoverState.FillColor = btn == btnaddprod
                    ? Color.FromArgb(0, 105, 230)
                    : Color.FromArgb(200, 35, 51);
            }

            // === NUMBERS ONLY FOR PRICE FIELDS ===
            guna2TextBox2.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };
            guna2TextBox3.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };

            // Allow only one decimal point
            guna2TextBox2.TextChanged += (s, e) => LimitDecimalPlaces(guna2TextBox2);
            guna2TextBox3.TextChanged += (s, e) => LimitDecimalPlaces(guna2TextBox3);
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load categories
                    string categoryQuery = "SELECT CategoryName FROM categories WHERE Status = 'active'";
                    using (SqlCommand categoryCommand = new SqlCommand(categoryQuery, connection))
                    using (SqlDataReader categoryReader = categoryCommand.ExecuteReader())
                    {
                        while (categoryReader.Read())
                        {
                            guna2ComboBox1.Items.Add(categoryReader["CategoryName"].ToString());
                        }
                    }

                    // Load suppliers
                    string supplierQuery = "SELECT CompanyName FROM suppliers WHERE Status = 'active'";
                    using (SqlCommand supplierCommand = new SqlCommand(supplierQuery, connection))
                    using (SqlDataReader supplierReader = supplierCommand.ExecuteReader())
                    {
                        while (supplierReader.Read())
                        {
                            guna2ComboBox2.Items.Add(supplierReader["CompanyName"].ToString());
                        }
                    }

                    // Load status options
                    guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading combo box data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Fallback to default items
                guna2ComboBox1.Items.AddRange(new object[] { "Electronics", "Accessories", "Furniture" });
                guna2ComboBox2.Items.AddRange(new object[] { "TechSupply Co.", "Cable World", "Office Plus", "AudioTech" });
                guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock" });
            }
        }

        private void LimitDecimalPlaces(Guna.UI2.WinForms.Guna2TextBox tb)
        {
            if (tb.Text.Contains(".") && tb.Text.Split('.').Length > 2)
                tb.Text = tb.Text.Remove(tb.Text.LastIndexOf('.'));
            if (tb.Text.StartsWith(".")) tb.Text = "0" + tb.Text;
            tb.SelectionStart = tb.Text.Length;
        }

        private void btncancel_Click(object sender, EventArgs e) => NavigateToProductList();

        private void btnaddprod_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string productName = guna2TextBox1.Text.Trim();
            decimal unitCost = decimal.Parse(guna2TextBox2.Text.Trim());
            decimal sellingPrice = decimal.Parse(guna2TextBox3.Text.Trim());
            string category = guna2ComboBox1.SelectedItem.ToString();
            string supplier = guna2ComboBox2.SelectedItem.ToString();
            string status = guna2ComboBox3.SelectedItem.ToString();
            string description = string.IsNullOrWhiteSpace(guna2TextBox4.Text) || guna2TextBox4.Text == "Enter product description..."
                ? "" : guna2TextBox4.Text.Trim();

            if (MessageBox.Show("Add this product to the product list?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var productItem = new ProductItem
            {
                ProductName = productName,
                Category = category,
                UnitCost = "₱" + unitCost.ToString("N2"),
                SellingPrice = "₱" + sellingPrice.ToString("N2"),
                PrimarySupplier = supplier,
                Status = status,
                Description = description
            };

            var parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Find existing ProductList form in pnlContent
                foreach (Control control in parentForm.pnlContent.Controls)
                {
                    if (control is ProductList productListForm)
                    {
                        productListForm.AddProduct(productItem);
                        NavigateToProductList();
                        return;
                    }
                }

                // If no existing ProductList found, create new one
                parentForm.navBar1.PageTitle = "Product List";
                var newProductListForm = new ProductList();
                newProductListForm.AddProduct(productItem);
                newProductListForm.TopLevel = false;
                newProductListForm.FormBorderStyle = FormBorderStyle.None;
                newProductListForm.Dock = DockStyle.Fill;

                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(newProductListForm);
                newProductListForm.Show();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter a product name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox1.Focus();
                return false;
            }

            if (!decimal.TryParse(guna2TextBox2.Text, out decimal unitCost) || unitCost <= 0)
            {
                MessageBox.Show("Please enter a valid unit cost.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox2.Focus();
                return false;
            }

            if (!decimal.TryParse(guna2TextBox3.Text, out decimal sellingPrice) || sellingPrice <= 0)
            {
                MessageBox.Show("Please enter a valid selling price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox3.Focus();
                return false;
            }

            if (guna2ComboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox1.Focus();
                return false;
            }

            if (guna2ComboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox2.Focus();
                return false;
            }

            if (guna2ComboBox3.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox3.Focus();
                return false;
            }

            return true;
        }

        public class ProductItem
        {
            public string ProductName { get; set; }
            public string Category { get; set; }
            public string UnitCost { get; set; }
            public string SellingPrice { get; set; }
            public string PrimarySupplier { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
        }

        private void NavigateToProductList()
        {
            var parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Product List";
                var productListForm = new ProductList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(productListForm);
                productListForm.Show();
            }
            else this.Close();
        }
    }
}