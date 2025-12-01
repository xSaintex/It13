using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Label = System.Windows.Forms.Label;

namespace IT13
{
    public partial class EditProd : Form
    {
        private readonly string _pid;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public EditProd(string productId = "")
        {
            _pid = productId;
            InitializeComponent();
            this.AutoScroll = true;

            btncancel.Click += btncancel_Click;
            btnaddprod.Click += btnaddprod_Click;

            ApplyModernStyling();

            label2.Text = "Edit Product";
            btnaddprod.Text = "Update Product";

            LoadComboBoxData();
            LoadProductData();
        }

        private void ApplyModernStyling()
        {
            // Hide breadcrumb buttons
            btnhome.Visible = btnproductlist.Visible = btnadd.Visible = false;

            // Red required text
            var lblRequired = new Label
            {
                Text = "Fields marked with (*) are required",
                Font = new Font("Poppins", 9F),
                ForeColor = Color.Red,
                AutoSize = true,
                Location = new Point(77, 70)
            };
            mainpanel.Controls.Add(lblRequired);
            mainpanel.Controls.SetChildIndex(lblRequired, 1);

            // TextBoxes
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

            guna2TextBox1.PlaceholderText = "Enter Product Name";
            guna2TextBox2.PlaceholderText = "0.00";
            guna2TextBox3.PlaceholderText = "0.00";
            guna2TextBox4.PlaceholderText = "Enter product description...";
            guna2TextBox4.TextAlign = HorizontalAlignment.Left;
            guna2TextBox4.Multiline = true;
            guna2TextBox4.BorderRadius = 16;

            // ComboBoxes
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

            // Labels (except header)
            foreach (Control c in mainpanel.Controls)
                if (c is Label lbl && lbl != label2)
                    lbl.Font = new Font("Bahnschrift SemiCondensed", 11F);

            // Buttons
            foreach (var btn in new[] { btnaddprod, btncancel })
            {
                btn.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
                btn.BorderRadius = 12;
                btn.ForeColor = Color.White;
                btn.FillColor = btn == btnaddprod ? Color.FromArgb(0, 123, 255) : Color.FromArgb(220, 53, 69);
                btn.HoverState.FillColor = btn == btnaddprod ? Color.FromArgb(0, 105, 230) : Color.FromArgb(200, 35, 51);
            }

            // Numbers only
            guna2TextBox2.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };
            guna2TextBox3.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };
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
                    guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock", "Discontinued" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading combo box data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Fallback to default items
                guna2ComboBox1.Items.AddRange(new object[] { "Electronics", "Accessories", "Furniture", "Office Supplies", "Cables", "Audio", "Others" });
                guna2ComboBox2.Items.AddRange(new object[] { "TechSupply Co.", "Cable World", "Office Plus", "AudioTech", "Global Traders" });
                guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock", "Discontinued" });
            }
        }

        private void LoadProductData()
        {
            if (string.IsNullOrEmpty(_pid))
            {
                MessageBox.Show("No product selected for editing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NavigateBack();
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get product data by product number or ID
                    string query = @"
                        SELECT 
                            p.ProductName,
                            c.CategoryName,
                            p.unit_cost,
                            p.selling_price,
                            s.CompanyName,
                            p.Status,
                            p.product_description
                        FROM product_list p
                        LEFT JOIN categories c ON p.category_id = c.id
                        LEFT JOIN suppliers s ON p.supplier_id = s.id
                        WHERE p.product_number = @ProductNumber OR p.ProdID = @ProdID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Try to parse as ID if it's numeric, otherwise use as product number
                        if (long.TryParse(_pid, out long productId))
                        {
                            command.Parameters.AddWithValue("@ProdID", productId);
                            command.Parameters.AddWithValue("@ProductNumber", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ProductNumber", _pid);
                            command.Parameters.AddWithValue("@ProdID", DBNull.Value);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate form fields with database data
                                guna2TextBox1.Text = reader["ProductName"].ToString();

                                string category = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : "";
                                if (!string.IsNullOrEmpty(category))
                                    guna2ComboBox1.Text = category;

                                decimal unitCost = reader["unit_cost"] != DBNull.Value ? Convert.ToDecimal(reader["unit_cost"]) : 0;
                                guna2TextBox2.Text = unitCost.ToString("N2");

                                decimal sellingPrice = reader["selling_price"] != DBNull.Value ? Convert.ToDecimal(reader["selling_price"]) : 0;
                                guna2TextBox3.Text = sellingPrice.ToString("N2");

                                string supplier = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : "";
                                if (!string.IsNullOrEmpty(supplier))
                                    guna2ComboBox2.Text = supplier;

                                string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "In Stock";
                                guna2ComboBox3.Text = status;

                                string description = reader["product_description"] != DBNull.Value ? reader["product_description"].ToString() : "";
                                guna2TextBox4.Text = description;
                            }
                            else
                            {
                                MessageBox.Show("Product not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                NavigateBack();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Load sample data as fallback
                LoadSampleData();
            }
        }

        private void LoadSampleData()
        {
            // Fallback sample data
            guna2TextBox1.Text = "Wireless Mouse";
            guna2TextBox2.Text = "250.00";
            guna2TextBox3.Text = "350.00";
            guna2TextBox4.Text = "Ergonomic wireless mouse with adjustable DPI.";

            if (guna2ComboBox1.Items.Count > 0) guna2ComboBox1.Text = "Electronics";
            if (guna2ComboBox2.Items.Count > 0) guna2ComboBox2.Text = "TechSupply Co.";
            if (guna2ComboBox3.Items.Count > 0) guna2ComboBox3.Text = "In Stock";
        }

        private void LimitDecimalPlaces(Guna.UI2.WinForms.Guna2TextBox tb)
        {
            if (tb.Text.Contains(".") && tb.Text.Split('.').Length > 2)
                tb.Text = tb.Text.Remove(tb.Text.LastIndexOf('.'));
            if (tb.Text.StartsWith(".")) tb.Text = "0" + tb.Text;
            tb.SelectionStart = tb.Text.Length;
        }

        private void btncancel_Click(object sender, EventArgs e) => NavigateBack();

        private void btnaddprod_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get category ID
                    long categoryId = GetOrCreateCategoryId(guna2ComboBox1.Text, connection);

                    // Get supplier ID
                    long supplierId = GetOrCreateSupplierId(guna2ComboBox2.Text, connection);

                    // Update product
                    string query = @"
                        UPDATE product_list 
                        SET 
                            ProductName = @ProductName,
                            category_id = @CategoryId,
                            supplier_id = @SupplierId,
                            Status = @Status,
                            product_description = @Description,
                            unit_cost = @UnitCost,
                            selling_price = @SellingPrice,
                            updated_at = GETDATE()
                        WHERE product_number = @ProductNumber OR ProdID = @ProdID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", guna2TextBox1.Text.Trim());
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        command.Parameters.AddWithValue("@Status", guna2ComboBox3.Text);
                        command.Parameters.AddWithValue("@Description", guna2TextBox4.Text.Trim());
                        command.Parameters.AddWithValue("@UnitCost", decimal.Parse(guna2TextBox2.Text.Trim()));
                        command.Parameters.AddWithValue("@SellingPrice", decimal.Parse(guna2TextBox3.Text.Trim()));

                        // Try to parse as ID if it's numeric, otherwise use as product number
                        if (long.TryParse(_pid, out long productId))
                        {
                            command.Parameters.AddWithValue("@ProdID", productId);
                            command.Parameters.AddWithValue("@ProductNumber", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ProductNumber", _pid);
                            command.Parameters.AddWithValue("@ProdID", DBNull.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NavigateBack();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update product. Product may have been deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetOrCreateCategoryId(string categoryName, SqlConnection connection)
        {
            string query = "SELECT id FROM categories WHERE CategoryName = @CategoryName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                object result = command.ExecuteScalar();
                if (result != null) return Convert.ToInt64(result);
            }

            // Create new category
            query = @"
                INSERT INTO categories (CategoryName, Date, Status) 
                VALUES (@CategoryName, GETDATE(), 'active');
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }

        private long GetOrCreateSupplierId(string supplierName, SqlConnection connection)
        {
            string query = "SELECT id FROM suppliers WHERE CompanyName = @CompanyName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CompanyName", supplierName);
                object result = command.ExecuteScalar();
                if (result != null) return Convert.ToInt64(result);
            }

            // Create new supplier
            query = @"
                INSERT INTO suppliers (CompanyName, Status) 
                VALUES (@CompanyName, 'active');
                SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CompanyName", supplierName);
                return Convert.ToInt64(command.ExecuteScalar());
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

            if (string.IsNullOrWhiteSpace(guna2ComboBox1.Text))
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(guna2ComboBox2.Text))
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(guna2ComboBox3.Text))
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox3.Focus();
                return false;
            }

            return true;
        }

        private void NavigateBack()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Product List";
                var list = new ProductList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(list);
                list.Show();
            }
        }
    }
}