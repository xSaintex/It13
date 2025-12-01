using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewProdCategory : Form
    {
        private readonly string _categoryId;
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewProdCategory(string categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            ApplyViewStyling();
            LoadCategoryData();
        }

        private void ApplyViewStyling()
        {
            // Set all textboxes as read-only
            txtId.ReadOnly = true;
            txtName.ReadOnly = true;
            txtDate.ReadOnly = true;
            txtStatus.ReadOnly = true;

            // Apply styling
            foreach (Guna2TextBox tb in new[] { txtId, txtName, txtDate, txtStatus })
            {
                tb.BorderColor = Color.FromArgb(200, 200, 200);
                tb.FillColor = Color.FromArgb(245, 245, 245);
                tb.Cursor = Cursors.Default;
                tb.BorderRadius = 12;
            }

            // Style status specifically
            txtStatus.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Bold);
            txtStatus.TextAlign = HorizontalAlignment.Center;

            // Update title
            lblTitle.Text = "View Category Details";
        }

        private void LoadCategoryData()
        {
            try
            {
                if (string.IsNullOrEmpty(_categoryId))
                {
                    ShowErrorMessage("No category selected.");
                    LoadSampleData();
                    return;
                }

                // Extract numeric ID from CAT-XXX format
                int numericId;
                if (_categoryId.StartsWith("CAT-"))
                {
                    string idPart = _categoryId.Substring(4);
                    if (!int.TryParse(idPart, out numericId))
                    {
                        ShowErrorMessage($"Invalid category ID format: {_categoryId}");
                        LoadSampleData();
                        return;
                    }
                }
                else
                {
                    // Try to parse directly as number
                    if (!int.TryParse(_categoryId, out numericId))
                    {
                        ShowErrorMessage($"Invalid category ID: {_categoryId}");
                        LoadSampleData();
                        return;
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to get category details
                    string query = @"
                        SELECT 
                            id,
                            CategoryName,
                            CONVERT(VARCHAR(10), Date, 120) as FormattedDate,
                            Status
                        FROM categories 
                        WHERE id = @CategoryId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", numericId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display category ID
                                txtId.Text = $"CAT-{reader["id"].ToString().PadLeft(3, '0')}";

                                // Display category name
                                txtName.Text = reader["CategoryName"] != DBNull.Value ?
                                    reader["CategoryName"].ToString() : "N/A";

                                // Display date
                                txtDate.Text = reader["FormattedDate"] != DBNull.Value ?
                                    reader["FormattedDate"].ToString() : "N/A";

                                // Display status with colored background
                                string status = reader["Status"] != DBNull.Value ?
                                    reader["Status"].ToString() : "Unknown";
                                txtStatus.Text = status;

                                // Set background color based on status
                                if (status.Equals("active", StringComparison.OrdinalIgnoreCase) ||
                                    status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                                {
                                    txtStatus.FillColor = Color.FromArgb(34, 197, 94); // Green for active
                                    txtStatus.ForeColor = Color.White;
                                }
                                else if (status.Equals("inactive", StringComparison.OrdinalIgnoreCase) ||
                                         status.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
                                {
                                    txtStatus.FillColor = Color.FromArgb(239, 68, 68); // Red for inactive
                                    txtStatus.ForeColor = Color.White;
                                }
                                else
                                {
                                    txtStatus.FillColor = Color.FromArgb(156, 163, 175); // Gray for unknown
                                    txtStatus.ForeColor = Color.White;
                                }

                                // Update window title with category ID
                                lblTitle.Text = $"View Category Details - {txtId.Text}";

                                // Load related products count
                                LoadRelatedProductsCount(connection, numericId);
                            }
                            else
                            {
                                ShowErrorMessage($"Category with ID '{_categoryId}' not found in database.");
                                LoadSampleData();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage($"Database error: {ex.Message}");
                LoadSampleData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading category data: {ex.Message}");
                LoadSampleData();
            }
        }

        private void LoadRelatedProductsCount(SqlConnection connection, int categoryId)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) as ProductCount 
                    FROM product_list 
                    WHERE category_id = @CategoryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        int productCount = Convert.ToInt32(result);

                        // Create label showing product count
                        var lblProductCount = new Label
                        {
                            Text = $"📦 Products in this category: {productCount}",
                            Font = new Font("Poppins", 10.5F, FontStyle.Regular),
                            ForeColor = Color.FromArgb(75, 85, 99),
                            AutoSize = true,
                            Location = new Point(77, 320) // Below the name field
                        };
                        mainpanel.Controls.Add(lblProductCount);
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently fail for product count - it's not critical
                Console.WriteLine($"Error loading product count: {ex.Message}");
            }
        }

        private void LoadSampleData()
        {
            // Fallback sample data
            txtId.Text = !string.IsNullOrEmpty(_categoryId) ? _categoryId : "CAT-000";
            txtName.Text = "Sample Category";
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtStatus.Text = "Active";
            txtStatus.FillColor = Color.FromArgb(34, 197, 94);
            txtStatus.ForeColor = Color.White;
            lblTitle.Text = "View Category Details - Sample";
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ReturnToList();
        }

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Product Categories";

            // Refresh the ProductCategory form to show updated data
            var categoryForm = new ProductCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(categoryForm);
            categoryForm.Show();
        }
    }
}