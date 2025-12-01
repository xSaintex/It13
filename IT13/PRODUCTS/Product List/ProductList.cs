using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace IT13
{
    public partial class ProductList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ProductList()
        {
            InitializeComponent();

            // Load icons (make sure you have edit_icon and view_icon in Resources)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();
            ConfigureDataGridView();
            LoadProductData();
            UpdateHeaderCheckState();
            datagridviewinventory.ClearSelection();
        }

        private void SetupFilterComboBox()
        {
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.AddRange(new object[] { "Filter", "All", "In Stock", "Low Stock", "Out of Stock" });
            ComBoxFilters.SelectedIndex = 0;
            ComBoxFilters.ForeColor = Color.Gray;

            ComBoxFilters.SelectedIndexChanged += (s, e) =>
            {
                ComBoxFilters.ForeColor = ComBoxFilters.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplyFilter();
            };
        }

        private void SetupExportComboBox()
        {
            CombExport.Items.Clear();
            CombExport.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            CombExport.SelectedIndex = 0;
            CombExport.ForeColor = Color.Gray;
        }

        private void ConfigureDataGridView()
        {
            datagridviewinventory.ReadOnly = true;
            datagridviewinventory.AllowUserToAddRows = false;
            datagridviewinventory.AllowUserToDeleteRows = false;
            datagridviewinventory.AllowUserToResizeColumns = false;
            datagridviewinventory.AllowUserToResizeRows = false;
            datagridviewinventory.RowHeadersVisible = false;
            datagridviewinventory.EnableHeadersVisualStyles = false;
            datagridviewinventory.SelectionMode = DataGridViewSelectionMode.CellSelect;
            datagridviewinventory.MultiSelect = false;
            datagridviewinventory.DefaultCellStyle.SelectionBackColor = datagridviewinventory.DefaultCellStyle.BackColor;
            datagridviewinventory.DefaultCellStyle.SelectionForeColor = datagridviewinventory.DefaultCellStyle.ForeColor;
            datagridviewinventory.RowTemplate.Height = 45;
            datagridviewinventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in datagridviewinventory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            datagridviewinventory.Columns["Column1"].FillWeight = 10f;
            datagridviewinventory.Columns["Column2"].FillWeight = 30f;
            datagridviewinventory.Columns["Column3"].FillWeight = 15f;
            datagridviewinventory.Columns["Column4"].FillWeight = 12f;
            datagridviewinventory.Columns["Column5"].FillWeight = 12f;
            datagridviewinventory.Columns["Column6"].FillWeight = 18f;
            datagridviewinventory.Columns["Column7"].FillWeight = 12f;
            datagridviewinventory.Columns["Column8"].FillWeight = 14f;
            datagridviewinventory.Columns["Column8"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            datagridviewinventory.CellPainting += datagridviewinventory_CellPainting;
            datagridviewinventory.CellClick += datagridviewinventory_CellClick;
            txtboxsearch.TextChanged += Txtboxsearch_TextChanged;
            btnaddstock.Click += Btnaddstock_Click;
        }

        private void LoadProductData()
        {
            try
            {
                datagridviewinventory.Rows.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            p.ProdID,
                            p.product_number,
                            p.ProductName,
                            c.CategoryName,
                            p.unit_cost,
                            p.selling_price,
                            s.CompanyName,
                            p.Status
                        FROM product_list p
                        LEFT JOIN categories c ON p.category_id = c.id
                        LEFT JOIN suppliers s ON p.supplier_id = s.id
                        ORDER BY p.created_at DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string pid = reader["product_number"] != DBNull.Value ? reader["product_number"].ToString() : $"PRD-{reader["ProdID"]}";
                            string name = reader["ProductName"].ToString();
                            string category = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : "Uncategorized";
                            decimal unitCost = reader["unit_cost"] != DBNull.Value ? Convert.ToDecimal(reader["unit_cost"]) : 0;
                            decimal sellingPrice = reader["selling_price"] != DBNull.Value ? Convert.ToDecimal(reader["selling_price"]) : 0;
                            string supplier = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : "No Supplier";
                            string status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "In Stock";

                            int idx = datagridviewinventory.Rows.Add(
                                false,
                                name,
                                category,
                                $"₱{unitCost:N2}",
                                $"₱{sellingPrice:N2}",
                                supplier,
                                status,
                                null
                            );
                            datagridviewinventory.Rows[idx].Cells[0].Tag = pid;
                            datagridviewinventory.Rows[idx].Height = 45;
                        }
                    }
                }
                UpdateRowCount();
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
            AddRow("PRD-001", "Wireless Mouse", "Electronics", "₱250.00", "₱350.00", "TechSupply Co.", "In Stock");
            AddRow("PRD-002", "USB-C Cable", "Accessories", "₱150.00", "₱250.00", "Cable World", "In Stock");
            AddRow("PRD-003", "Laptop Stand", "Furniture", "₱800.00", "₱1,200.00", "Office Plus", "Low Stock");
            AddRow("PRD-004", "HDMI Cable 2m", "Accessories", "₱300.00", "₱450.00", "Cable World", "Out of Stock");
            AddRow("PRD-005", "Bluetooth Speaker", "Electronics", "₱1,200.00", "₱1,800.00", "AudioTech", "In Stock");
        }

        private void AddRow(string pid, string name, string category, string unitCost, string sellingPrice, string supplier, string status)
        {
            int idx = datagridviewinventory.Rows.Add(false, name, category, unitCost, sellingPrice, supplier, status, null);
            datagridviewinventory.Rows[idx].Cells[0].Tag = pid;
            datagridviewinventory.Rows[idx].Height = 45;
        }

        public void AddProduct(AddProd.ProductItem productItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get category ID
                    long categoryId = GetOrCreateCategoryId(productItem.Category, connection);

                    // Get supplier ID
                    long supplierId = GetOrCreateSupplierId(productItem.PrimarySupplier, connection);

                    // Generate product number
                    string productNumber = GenerateProductNumber(connection);

                    // Insert product
                    string query = @"
                        INSERT INTO product_list (
                            ProductName, category_id, supplier_id, Status, product_number, 
                            product_description, unit_cost, selling_price
                        ) VALUES (
                            @ProductName, @CategoryId, @SupplierId, @Status, @ProductNumber,
                            @Description, @UnitCost, @SellingPrice
                        )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productItem.ProductName);
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        command.Parameters.AddWithValue("@Status", productItem.Status);
                        command.Parameters.AddWithValue("@ProductNumber", productNumber);
                        command.Parameters.AddWithValue("@Description", productItem.Description ?? "");
                        command.Parameters.AddWithValue("@UnitCost", decimal.Parse(productItem.UnitCost.Replace("₱", "")));
                        command.Parameters.AddWithValue("@SellingPrice", decimal.Parse(productItem.SellingPrice.Replace("₱", "")));

                        command.ExecuteNonQuery();
                    }
                }

                // Reload data to show the new product
                LoadProductData();
                UpdateHeaderCheckState();

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string GenerateProductNumber(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM product_list";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar()) + 1;
                return $"PRD-{count:D3}";
            }
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false)) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? (bool?)false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? true : (bool?)null;
            datagridviewinventory.InvalidateCell(0, -1);
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void datagridviewinventory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header Checkbox + PID
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (_headerCheckState == true)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5)
                        });
                }
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, r.X + 3, r.Y + 3, 10, 10);

                TextRenderer.DrawText(e.Graphics, "PID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // Row Checkbox + PID
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (chk)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5)
                        });
                }

                string pid = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(pid))
                    TextRenderer.DrawText(e.Graphics, pid, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // Status Badge
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status switch
                {
                    "In Stock" => Color.FromArgb(34, 197, 94),
                    "Low Stock" => Color.FromArgb(255, 159, 0),
                    "Out of Stock" => Color.FromArgb(239, 68, 68),
                    _ => Color.Gray
                };

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var br = new SolidBrush(bg))
                    e.Graphics.FillPath(br, path);

                using (var f = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
                using (var br = new SolidBrush(Color.White))
                {
                    var sz = e.Graphics.MeasureString(status, f);
                    e.Graphics.DrawString(status, f, br,
                        e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);
                }
                e.Handled = true;
                return;
            }

            // Actions Column - Edit & View Icons
            if (e.ColumnIndex == 7 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);
                e.Handled = true;
            }
        }

        private void datagridviewinventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            datagridviewinventory.CurrentCell = null;

            if (e.RowIndex < 0 && e.ColumnIndex < 0) return;

            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow row in datagridviewinventory.Rows)
                    if (row.Visible && !row.IsNewRow)
                        row.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                datagridviewinventory.InvalidateCell(0, -1);
                return;
            }

            // Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = datagridviewinventory.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            // Action Icons: Edit or View
            if (e.ColumnIndex == 7 && e.RowIndex >= 0)
            {
                var cellRect = datagridviewinventory.GetCellDisplayRectangle(7, e.RowIndex, false);
                var pt = datagridviewinventory.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;

                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;

                string pid = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                {
                    OpenEditProduct(pid);
                }
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                {
                    OpenViewProduct(pid);
                }
            }
        }

        private void OpenEditProduct(string pid)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Edit Product";

            var editForm = new EditProd(pid)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(editForm);
            editForm.Show();
        }

        private void OpenViewProduct(string pid)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "View Product Details";

            var viewForm = new ViewProd(pid)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(viewForm);
            viewForm.Show();
        }

        private void Btnaddstock_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Add Product";

            var addForm = new AddProd
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void ApplyFilter()
        {
            string filter = ComBoxFilters.SelectedItem?.ToString() ?? "Filter";
            bool showAll = filter == "Filter" || filter == "All";

            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.IsNewRow) continue;
                string status = row.Cells[6].Value?.ToString() ?? "";
                row.Visible = showAll || status.Equals(filter, StringComparison.OrdinalIgnoreCase);
            }
            UpdateHeaderCheckState();
            UpdateRowCount();
        }

        private void UpdateRowCount()
        {
            int visible = datagridviewinventory.Rows.Cast<DataGridViewRow>().Count(r => r.Visible && !r.IsNewRow);
            label1.Text = $"Showing {visible} items";
        }

        private void Txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtboxsearch.Text.Trim().ToLower();
            string statusFilter = ComBoxFilters.SelectedItem?.ToString() ?? "Filter";
            bool hasStatusFilter = statusFilter != "Filter" && statusFilter != "All";

            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.IsNewRow) continue;

                string pid = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells[1].Value?.ToString().ToLower() ?? "";
                string category = row.Cells[2].Value?.ToString().ToLower() ?? "";
                string status = row.Cells[6].Value?.ToString() ?? "";

                bool matchSearch = string.IsNullOrEmpty(search) ||
                    pid.Contains(search) || name.Contains(search) || category.Contains(search);
                bool matchStatus = !hasStatusFilter || status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase);

                row.Visible = matchSearch && matchStatus;
            }
            UpdateHeaderCheckState();
            UpdateRowCount();
        }
    }
}