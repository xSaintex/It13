using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace IT13
{
    public partial class SupplierOrderList : Form
    {
        private readonly Image _editIcon, _viewIcon, _deleteIcon;
        private bool? _headerCheckState = false;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public SupplierOrderList()
        {
            InitializeComponent();

            // Load icons
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            _deleteIcon = new Bitmap(Properties.Resources.delete_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // Grid settings
            dgvOrders.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.RowHeadersVisible = false;

            // Remove selection highlight
            dgvOrders.DefaultCellStyle.SelectionBackColor = dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor = dgvOrders.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvOrders.DefaultCellStyle.Font = new Font("Poppins", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Column weights & alignment
            dgvOrders.Columns["colID"].MinimumWidth = 160;
            dgvOrders.Columns["colID"].FillWeight = 10;
            dgvOrders.Columns["colDate"].FillWeight = 15;
            dgvOrders.Columns["colSupplier"].FillWeight = 35;
            dgvOrders.Columns["colTotal"].FillWeight = 15;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colActions"].FillWeight = 18;

            dgvOrders.Columns["colDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colTotal"].DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dgvOrders.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colSupplier"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadDataFromDatabase();
            UpdateHeaderCheckState();

            // Final anti-selection
            dgvOrders.ClearSelection();
            dgvOrders.CurrentCell = null;

            // Hook MouseDown
            dgvOrders.MouseDown += dgvOrders_MouseDown;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Pending", "Delivered", "Cancelled" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;

            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplyFiltersAndSearch();
            };
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            so.SupOrderID,
                            so.OrderDate,
                            s.CompanyName,
                            so.Total,
                            so.Status
                        FROM supplier_orders so
                        INNER JOIN suppliers s ON so.SupplierID = s.id
                        ORDER BY so.OrderDate DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dgvOrders.Rows.Clear();
                        while (reader.Read())
                        {
                            string orderId = $"SO-{Convert.ToInt64(reader["SupOrderID"]):D5}";
                            string date = Convert.ToDateTime(reader["OrderDate"]).ToString("yyyy-MM-dd");
                            string supplier = reader["CompanyName"].ToString();
                            string total = $"₱{Convert.ToDecimal(reader["Total"]):N2}";
                            string status = reader["Status"].ToString();

                            AddRow(orderId, date, supplier, total, status);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string id, string date, string supplier, string total, string status)
        {
            int idx = dgvOrders.Rows.Add(false, date, supplier, total, status, null);
            dgvOrders.Rows[idx].Cells[0].Tag = id;
            dgvOrders.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;

            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (!row.IsNewRow && row.Visible)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false))
                        checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? (bool?)false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? true : (bool?)null;

            dgvOrders.InvalidateCell(dgvOrders.Columns["colID"].Index, -1);
        }

        private void ApplyFiltersAndSearch()
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            string filterStatus = Filter.SelectedItem?.ToString() ?? "Filter";

            bool showAll = filterStatus == "All" || filterStatus == "Filter";

            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.IsNewRow) continue;

                string id = (row.Cells[0].Tag?.ToString() ?? "").ToLower();
                string supplier = (row.Cells["colSupplier"].Value?.ToString() ?? "").ToLower();
                string status = row.Cells["colStatus"].Value?.ToString() ?? "";

                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                     id.Contains(searchText) ||
                                     supplier.Contains(searchText);

                bool matchesFilter = showAll ||
                                     (filterStatus != "Filter" && status.Equals(filterStatus, StringComparison.OrdinalIgnoreCase));

                row.Visible = matchesSearch && matchesFilter;
            }

            UpdateHeaderCheckState();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // HEADER: Checkbox + "ID"
            if (e.RowIndex == -1 && e.ColumnIndex == dgvOrders.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                var checkRect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, checkRect);
                e.Graphics.DrawRectangle(Pens.Black, checkRect.X, checkRect.Y, 15, 15);

                if (_headerCheckState == true)
                {
                    using (Pen pen = new Pen(Color.Black, 2.5f))
                    {
                        e.Graphics.DrawLines(pen, new Point[] {
                            new Point(checkRect.X + 3, checkRect.Y + 8),
                            new Point(checkRect.X + 7, checkRect.Y + 12),
                            new Point(checkRect.X + 13, checkRect.Y + 5)
                        });
                    }
                }
                else if (_headerCheckState == null)
                {
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 3, checkRect.Y + 3, 10, 10);
                }

                TextRenderer.DrawText(e.Graphics, "ID",
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // ROW: Checkbox + ID Text
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, checkRect);
                e.Graphics.DrawRectangle(Pens.Black, checkRect.X, checkRect.Y, 15, 15);

                if (isChecked)
                {
                    using (Pen pen = new Pen(Color.Black, 2.5f))
                    {
                        e.Graphics.DrawLines(pen, new Point[] {
                            new Point(checkRect.X + 3, checkRect.Y + 8),
                            new Point(checkRect.X + 7, checkRect.Y + 12),
                            new Point(checkRect.X + 13, checkRect.Y + 5)
                        });
                    }
                }

                string idText = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    TextRenderer.DrawText(e.Graphics, idText,
                        new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
                e.Handled = true;
                return;
            }

            // ACTIONS COLUMN
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24, gap = 16;
                int totalWidth = iconSize * 3 + gap * 2;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Graphics.DrawImage(_deleteIcon, x + (iconSize + gap) * 2, y, iconSize, iconSize);
                e.Handled = true;
                return;
            }

            // STATUS BADGE
            if (e.ColumnIndex == dgvOrders.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status switch
                {
                    "Delivered" => Color.FromArgb(34, 197, 94),
                    "Pending" => Color.FromArgb(255, 159, 0),
                    "Cancelled" => Color.FromArgb(239, 68, 68),
                    _ => Color.Gray
                };

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                using (var font = new Font("Poppins", 10F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var sz = e.Graphics.MeasureString(status, font);
                    e.Graphics.DrawString(status, font, brush,
                        e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);
                }
                e.Handled = true;
            }
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

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == dgvOrders.Columns["colID"].Index)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    if (row.Visible && !row.IsNewRow)
                        row.Cells[0].Value = newState;
                }
                _headerCheckState = newState ? true : (bool?)false;
                dgvOrders.InvalidateCell(dgvOrders.Columns["colID"].Index, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colID"].Index)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvOrders.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int iconSize = 24, gap = 16, total = iconSize * 3 + gap * 2;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditOrder(id);
                else if (clickX >= startX + iconSize + gap && clickX < startX + (iconSize + gap) * 2)
                    OpenViewOrder(id);
                else if (clickX >= startX + (iconSize + gap) * 2 && clickX < startX + total)
                    DeleteOrder(id);
            }
        }

        private void dgvOrders_MouseDown(object sender, MouseEventArgs e)
        {
            dgvOrders.CurrentCell = null;
        }

        private void OpenEditOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Supplier Order";
            var f = new EditSupplierOrder(orderId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Order: {orderId}";
            var f = new ViewSupplierOrder(orderId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void DeleteOrder(string orderId)
        {
            if (MessageBox.Show($"Delete order {orderId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Extract numeric ID from format SO-00001
                    long orderIdNum = long.Parse(orderId.Replace("SO-", ""));

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Delete order items first
                        string deleteItemsQuery = "DELETE FROM supporderitem WHERE SupOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(deleteItemsQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderIdNum);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete order
                        string deleteOrderQuery = "DELETE FROM supplier_orders WHERE SupOrderID = @OrderID";
                        using (SqlCommand cmd = new SqlCommand(deleteOrderQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderIdNum);
                            cmd.ExecuteNonQuery();
                        }

                        // Remove from grid
                        for (int i = dgvOrders.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dgvOrders.Rows[i].Cells[0].Tag?.ToString() == orderId)
                            {
                                dgvOrders.Rows.RemoveAt(i);
                                break;
                            }
                        }

                        MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ApplyFiltersAndSearch();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting order: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Supplier Order";
            var f = new AddSupplierOrder { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }
    }
}