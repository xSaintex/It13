using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewProd : Form
    {
        private string _productId;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewProd(string productId = "")
        {
            InitializeComponent();
            _productId = productId;
            this.AutoScroll = true;
            btncancel.Click += btncancel_Click;
            ApplyViewStyling();
            LoadProductData();
        }

        private void ApplyViewStyling()
        {
            btnhome.Visible = btnproductlist.Visible = btnadd.Visible = false;

            var lblInfo = new Label
            {
                Text = "Product details are displayed below.",
                Font = new Font("Poppins", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(77, 70)
            };
            mainpanel.Controls.Add(lblInfo);
            mainpanel.Controls.SetChildIndex(lblInfo, 1);

            foreach (var tb in new[] { guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4 })
            {
                tb.ReadOnly = true;
                tb.BorderColor = Color.FromArgb(200, 200, 200);
                tb.BackColor = Color.FromArgb(248, 248, 248);
                tb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                tb.Cursor = Cursors.Default;
            }
            guna2TextBox4.Multiline = true;
            guna2TextBox4.BorderRadius = 16;
            guna2TextBox4.TextAlign = HorizontalAlignment.Left;

            foreach (var cb in new[] { guna2ComboBox1, guna2ComboBox2, guna2ComboBox3 })
            {
                cb.Enabled = false;
                cb.BackColor = Color.FromArgb(248, 248, 248);
                cb.BorderColor = Color.FromArgb(200, 200, 200);
                cb.Font = new Font("Bahnschrift SemiCondensed", 11F);
            }

            foreach (Control c in mainpanel.Controls)
                if (c is Label lbl && lbl != label2)
                    lbl.Font = new Font("Bahnschrift SemiCondensed", 11F);

            btnaddprod.Visible = false;
            btncancel.Text = "Close";
            btncancel.FillColor = Color.FromArgb(108, 117, 125);
            btncancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btncancel.BorderRadius = 12;
        }

        private void LoadProductData()
        {
            try
            {
                if (string.IsNullOrEmpty(_productId))
                {
                    ShowErrorMessage("No product selected.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            p.ProductName,
                            p.product_description,
                            p.unit_cost,
                            p.selling_price,
                            p.Status,
                            c.CategoryName,
                            s.CompanyName,
                            p.product_number
                        FROM product_list p
                        LEFT JOIN categories c ON p.category_id = c.id
                        LEFT JOIN suppliers s ON p.supplier_id = s.id
                        WHERE p.product_number = @ProductNumber 
                           OR CONCAT('PRD-', p.ProdID) = @ProductNumber
                        ORDER BY p.created_at DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductNumber", _productId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load product details
                                guna2TextBox1.Text = reader["ProductName"] != DBNull.Value ?
                                    reader["ProductName"].ToString() : "N/A";

                                guna2TextBox4.Text = reader["product_description"] != DBNull.Value ?
                                    reader["product_description"].ToString() : "No description available.";

                                decimal unitCost = reader["unit_cost"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["unit_cost"]) : 0;
                                guna2TextBox2.Text = $"₱{unitCost:N2}";

                                decimal sellingPrice = reader["selling_price"] != DBNull.Value ?
                                    Convert.ToDecimal(reader["selling_price"]) : 0;
                                guna2TextBox3.Text = $"₱{sellingPrice:N2}";

                                // Populate comboboxes
                                LoadComboBoxData();

                                // Set selected values
                                string category = reader["CategoryName"] != DBNull.Value ?
                                    reader["CategoryName"].ToString() : "Uncategorized";
                                guna2ComboBox1.Text = category;

                                string supplier = reader["CompanyName"] != DBNull.Value ?
                                    reader["CompanyName"].ToString() : "No Supplier";
                                guna2ComboBox2.Text = supplier;

                                string status = reader["Status"] != DBNull.Value ?
                                    reader["Status"].ToString() : "In Stock";
                                guna2ComboBox3.Text = status;

                                // Update window title with product number
                                string productNumber = reader["product_number"] != DBNull.Value ?
                                    reader["product_number"].ToString() : _productId;
                                label2.Text = $"View Product Details - {productNumber}";
                            }
                            else
                            {
                                ShowErrorMessage($"Product with ID '{_productId}' not found in database.");
                                LoadSampleData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading product data: {ex.Message}");
                LoadSampleData();
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load categories
                    string categoryQuery = "SELECT CategoryName FROM categories WHERE Status = 'active' ORDER BY CategoryName";
                    using (SqlCommand cmd = new SqlCommand(categoryQuery, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        guna2ComboBox1.Items.Clear();
                        guna2ComboBox1.Items.Add("Uncategorized");
                        while (reader.Read())
                        {
                            guna2ComboBox1.Items.Add(reader["CategoryName"].ToString());
                        }
                    }

                    // Load suppliers
                    string supplierQuery = "SELECT CompanyName FROM suppliers WHERE Status = 'active' ORDER BY CompanyName";
                    using (SqlCommand cmd = new SqlCommand(supplierQuery, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        guna2ComboBox2.Items.Clear();
                        guna2ComboBox2.Items.Add("No Supplier");
                        while (reader.Read())
                        {
                            guna2ComboBox2.Items.Add(reader["CompanyName"].ToString());
                        }
                    }

                    // Load status options
                    guna2ComboBox3.Items.Clear();
                    guna2ComboBox3.Items.AddRange(new object[] {
                        "In Stock", "Low Stock", "Out of Stock", "Discontinued"
                    });
                }
            }
            catch (Exception ex)
            {
                // Fallback to static data if database fails
                guna2ComboBox1.Items.Clear();
                guna2ComboBox1.Items.AddRange(new object[] {
                    "Electronics", "Accessories", "Furniture", "Office Supplies", "Cables", "Audio", "Others", "Uncategorized"
                });

                guna2ComboBox2.Items.Clear();
                guna2ComboBox2.Items.AddRange(new object[] {
                    "TechSupply Co.", "Cable World", "Office Plus", "AudioTech", "Global Traders", "No Supplier"
                });

                guna2ComboBox3.Items.Clear();
                guna2ComboBox3.Items.AddRange(new object[] {
                    "In Stock", "Low Stock", "Out of Stock", "Discontinued"
                });

                Console.WriteLine($"Error loading combo box data: {ex.Message}");
            }
        }

        private void LoadSampleData()
        {
            // Fallback sample data if database fails
            guna2TextBox1.Text = "Product Not Found";
            guna2TextBox2.Text = "₱0.00";
            guna2TextBox3.Text = "₱0.00";
            guna2TextBox4.Text = "The requested product could not be loaded from the database.";

            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.AddRange(new object[] { "Electronics", "Accessories", "Furniture", "Others" });
            guna2ComboBox1.Text = "Others";

            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.AddRange(new object[] { "TechSupply Co.", "Cable World", "Office Plus" });
            guna2ComboBox2.Text = "No Supplier";

            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock" });
            guna2ComboBox3.Text = "Out of Stock";

            label2.Text = "View Product Details - Not Found";
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btncancel_Click(object sender, EventArgs e)
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