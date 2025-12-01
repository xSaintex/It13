using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class OrderList : Form
    {
        private readonly Image _viewIcon;
        private readonly Image _deleteIcon;
        private bool? _headerCheckState = false;
        private readonly string _connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public OrderList()
        {
            InitializeComponent();
            _viewIcon = Properties.Resources.view_icon ?? CreatePlaceholder(Color.FromArgb(0, 123, 255));
            _deleteIcon = Properties.Resources.delete_icon ?? CreatePlaceholder(Color.FromArgb(220, 53, 69));
            SetupFilterComboBox();
            SetupExportComboBox();
            ConfigureDataGridView();

            // Add live search handler
            txtSearch.TextChanged += TxtSearch_TextChanged;

            LoadOrdersFromDatabase();
            ApplyVisibility(); // Apply initial visibility after loading
            UpdateHeaderCheckState();
            dgvOrders.ClearSelection();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyVisibility();
        }

        private void ConfigureDataGridView()
        {
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.DefaultCellStyle.SelectionBackColor = dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor = dgvOrders.DefaultCellStyle.ForeColor;
            dgvOrders.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvOrders.Columns["colCheck"].MinimumWidth = 180;
            dgvOrders.Columns["colCheck"].FillWeight = 20;
            dgvOrders.Columns["colOrderType"].FillWeight = 12;
            dgvOrders.Columns["colCompanyName"].FillWeight = 25;
            dgvOrders.Columns["colQty"].FillWeight = 8;
            dgvOrders.Columns["colTotal"].FillWeight = 12;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colEstDate"].FillWeight = 12;
            dgvOrders.Columns["colActions"].FillWeight = 10;
            dgvOrders.Columns["colOrderType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colCompanyName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colEstDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private Image CreatePlaceholder(Color color)
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
                g.Clear(Color.Transparent);
            return bmp;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All Orders", "Customer Orders", "Supplier Orders", "Delivered", "Pending", "Processing", "Cancelled" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplyVisibility();
            };
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV", "Print" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) =>
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadOrdersFromDatabase()
        {
            try
            {
                dgvOrders.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Load Customer Orders
                    string customerQuery = @"
                        SELECT
                            co.CusOrderID,
                            'Customer' AS OrderType,
                            c.CompanyName,
                            ISNULL(SUM(coi.Qly), 0) AS TotalQty,
                            co.Total,
                            co.Status,
                            co.DeliveryDate
                        FROM customer_orders co
                        LEFT JOIN customers c ON co.CustomerID = c.CustID
                        LEFT JOIN customerorderitems coi ON co.CusOrderID = coi.CusOrderID
                        GROUP BY co.CusOrderID, c.CompanyName, co.Total, co.Status, co.DeliveryDate";
                    using (SqlCommand cmd = new SqlCommand(customerQuery, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string orderId = "CO-" + reader["CusOrderID"].ToString();
                            string orderType = "Customer";
                            string company = reader["CompanyName"]?.ToString() ?? "N/A";
                            int qty = Convert.ToInt32(reader["TotalQty"]);
                            decimal total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : 0;
                            string status = reader["Status"]?.ToString() ?? "pending";
                            string date = reader["DeliveryDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DeliveryDate"]).ToString("yyyy-MM-dd")
                                : "N/A";
                            AddRow(orderId, orderType, company, qty, $"₱{total:N2}", status, date);
                        }
                    }
                    // Load Supplier Orders
                    string supplierQuery = @"
                        SELECT
                            so.SupOrderID,
                            'Supplier' AS OrderType,
                            s.CompanyName,
                            ISNULL(SUM(soi.Qty), 0) AS TotalQty,
                            so.Total,
                            so.Status,
                            so.EstimatedDate
                        FROM supplier_orders so
                        LEFT JOIN suppliers s ON so.SupplierID = s.id
                        LEFT JOIN supporderitem soi ON so.SupOrderID = soi.SupOrderID
                        GROUP BY so.SupOrderID, s.CompanyName, so.Total, so.Status, so.EstimatedDate";
                    using (SqlCommand cmd = new SqlCommand(supplierQuery, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string orderId = "SO-" + reader["SupOrderID"].ToString();
                            string orderType = "Supplier";
                            string company = reader["CompanyName"]?.ToString() ?? "N/A";
                            int qty = Convert.ToInt32(reader["TotalQty"]);
                            decimal total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : 0;
                            string status = reader["Status"]?.ToString() ?? "pending";
                            string date = reader["EstimatedDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["EstimatedDate"]).ToString("yyyy-MM-dd")
                                : "N/A";
                            AddRow(orderId, orderType, company, qty, $"₱{total:N2}", status, date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string orderId, string orderType, string company, int qty, string total, string status, string estDate)
        {
            int idx = dgvOrders.Rows.Add(false, orderType, company, qty, total, status, estDate, null);
            var row = dgvOrders.Rows[idx];
            row.Cells["colCheck"].Tag = orderId;
            row.Cells["colOrderType"].Tag = orderType;
            row.Height = 45;
        }

        private void ApplyVisibility()
        {
            string filterValue = Filter.SelectedIndex > 0 && Filter.SelectedIndex != 1 ? Filter.SelectedItem?.ToString() ?? "" : ""; // Treat "Filter" and "All Orders" as no filter
            string searchTerm = txtSearch.Text.Trim().ToLowerInvariant();

            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.IsNewRow) continue;

                // Filter logic
                string orderType = row.Cells["colOrderType"].Value?.ToString() ?? "";
                string status = row.Cells["colStatus"].Value?.ToString() ?? "";
                bool filterMatch = string.IsNullOrEmpty(filterValue) ||
                                   (filterValue == "Customer Orders" && orderType.Equals("Customer", StringComparison.OrdinalIgnoreCase)) ||
                                   (filterValue == "Supplier Orders" && orderType.Equals("Supplier", StringComparison.OrdinalIgnoreCase)) ||
                                   status.Equals(filterValue, StringComparison.OrdinalIgnoreCase);

                // Search logic (expanded to more columns for better functionality)
                string orderId = row.Cells["colCheck"].Tag?.ToString() ?? "";
                string company = row.Cells["colCompanyName"].Value?.ToString() ?? "";
                string qtyStr = row.Cells["colQty"].Value?.ToString() ?? "";
                string totalStr = row.Cells["colTotal"].Value?.ToString() ?? "";
                string estDate = row.Cells["colEstDate"].Value?.ToString() ?? "";

                bool searchMatch = string.IsNullOrEmpty(searchTerm) ||
                                   orderId.ToLowerInvariant().Contains(searchTerm) ||
                                   orderType.ToLowerInvariant().Contains(searchTerm) ||
                                   company.ToLowerInvariant().Contains(searchTerm) ||
                                   qtyStr.ToLowerInvariant().Contains(searchTerm) ||
                                   totalStr.ToLowerInvariant().Contains(searchTerm) ||
                                   status.ToLowerInvariant().Contains(searchTerm) ||
                                   estDate.ToLowerInvariant().Contains(searchTerm);

                row.Visible = filterMatch && searchMatch;
            }

            UpdateHeaderCheckState();
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells["colCheck"].Value ?? false)) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                               checkedCount == 0 ? false :
                               checkedCount == visibleCount ? true : (bool?)null;
            dgvOrders.InvalidateCell(0, -1);
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

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (_headerCheckState == true)
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new[] { new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5) });
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, r.X + 3, r.Y + 3, 10, 10);
                TextRenderer.DrawText(e.Graphics, "Order ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.Handled = true;
                return;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (chk)
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new[] { new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5) });
                string id = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id,
                        new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.Handled = true;
                return;
            }
            if (e.ColumnIndex == dgvOrders.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.ToLower() switch
                {
                    "delivered" or "completed" => Color.FromArgb(34, 197, 94),
                    "pending" => Color.FromArgb(255, 193, 7),
                    "processing" or "confirmed" => Color.FromArgb(0, 123, 255),
                    "cancelled" => Color.FromArgb(220, 53, 69),
                    _ => Color.Gray
                };
                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var br = new SolidBrush(bg))
                    e.Graphics.FillPath(br, path);
                using (var f = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
                using (var tb = new SolidBrush(Color.White))
                {
                    var sz = e.Graphics.MeasureString(status, f);
                    e.Graphics.DrawString(status, f, tb,
                        e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);
                }
                e.Handled = true;
                return;
            }
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int startX = e.CellBounds.X + (e.CellBounds.Width - 58) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;
                e.Graphics.DrawImage(_viewIcon, startX, y, 24, 24);
                e.Graphics.DrawImage(_deleteIcon, startX + 34, y, 24, 24);
                e.Handled = true;
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.CurrentCell = null;
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvOrders.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvOrders.InvalidateCell(0, -1);
                return;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point mouse = dgvOrders.PointToClient(Cursor.Position);
                int relativeX = mouse.X - cellRect.X;
                string orderId = dgvOrders.Rows[e.RowIndex].Cells["colCheck"].Tag?.ToString() ?? "";
                string orderType = dgvOrders.Rows[e.RowIndex].Cells["colOrderType"].Tag?.ToString() ?? "";
                var parent = this.ParentForm as Form1;
                if (parent == null) return;
                if (relativeX >= 17 && relativeX <= 41) // View
                {
                    Form viewForm = orderType == "Customer"
                        ? new ViewCustomerOrder(orderId)
                        : new ViewSupplierOrder(orderId);
                    parent.navBar1.PageTitle = orderType == "Customer"
                        ? $"View Customer Order: {orderId}"
                        : $"View Supplier Order: {orderId}";
                    viewForm.TopLevel = false;
                    viewForm.FormBorderStyle = FormBorderStyle.None;
                    viewForm.Dock = DockStyle.Fill;
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(viewForm);
                    viewForm.Show();
                }
                else if (relativeX >= 51 && relativeX <= 75) // Delete
                {
                    if (MessageBox.Show($"Delete order {orderId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteOrder(orderId, orderType);
                    }
                }
            }
        }

        private void DeleteOrder(string orderId, string orderType)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query;
                    long id = long.Parse(orderId.Split('-')[1]);
                    if (orderType == "Customer")
                    {
                        // Delete related records first
                        query = "DELETE FROM customerorderitems WHERE CusOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", id);
                            cmd.ExecuteNonQuery();
                        }
                        query = "DELETE FROM customer_orders WHERE CusOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else // Supplier
                    {
                        // Delete related records first
                        query = "DELETE FROM supporderitem WHERE SupOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", id);
                            cmd.ExecuteNonQuery();
                        }
                        query = "DELETE FROM supplier_orders WHERE SupOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrdersFromDatabase();
                ApplyVisibility(); // Reapply after reload
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting order: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomerOrder_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Customer Order";
            var f = new AddCustomerOrder { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddSupplierOrder_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Supplier Order";
            var f = new AddSupplierOrder { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyVisibility();
        }

        public void RefreshData()
        {
            LoadOrdersFromDatabase();
            ApplyVisibility();
        }
    }
}