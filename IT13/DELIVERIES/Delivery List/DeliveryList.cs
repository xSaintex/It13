using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class DeliveryList : Form
    {
        private readonly string connString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public DeliveryList()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();
            SetupDataGridView();
            LoadDeliveriesFromDatabase();
            UpdateHeaderCheckState();
            dgvDeliveries.ClearSelection();
            dgvDeliveries.CurrentCell = null;
            dgvDeliveries.MouseDown += (s, e) => dgvDeliveries.CurrentCell = null;
        }

        private void SetupDataGridView()
        {
            dgvDeliveries.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDeliveries.MultiSelect = false;
            dgvDeliveries.ReadOnly = true;
            dgvDeliveries.AllowUserToAddRows = false;
            dgvDeliveries.AllowUserToDeleteRows = false;
            dgvDeliveries.RowHeadersVisible = false;
            dgvDeliveries.DefaultCellStyle.SelectionBackColor = dgvDeliveries.DefaultCellStyle.BackColor;
            dgvDeliveries.DefaultCellStyle.SelectionForeColor = dgvDeliveries.DefaultCellStyle.ForeColor;
            foreach (DataGridViewColumn c in dgvDeliveries.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDeliveries.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvDeliveries.RowTemplate.Height = 45;
            dgvDeliveries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeliveries.Columns["colDeliveryID"].MinimumWidth = 160;
            dgvDeliveries.Columns["colDeliveryID"].FillWeight = 10;
            dgvDeliveries.Columns["colOrderID"].FillWeight = 18;
            dgvDeliveries.Columns["colCustomer"].FillWeight = 28;
            dgvDeliveries.Columns["colDeliveryDate"].FillWeight = 14;
            dgvDeliveries.Columns["colEmployee"].FillWeight = 16;
            dgvDeliveries.Columns["colVehicle"].FillWeight = 14;
            dgvDeliveries.Columns["colStatus"].FillWeight = 12;
            dgvDeliveries.Columns["colActions"].FillWeight = 14;
            dgvDeliveries.Columns["colDeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colEmployee"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colVehicle"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colCustomer"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Pending", "In Transit", "Delivered" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += Filter_SelectedIndexChanged;
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string filterValue = Filter.SelectedItem?.ToString();

            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                if (row.IsNewRow) continue;

                if (filterValue == "Filter" || filterValue == "All")
                {
                    row.Visible = true;
                }
                else
                {
                    string status = row.Cells["colStatus"].Value?.ToString() ?? "";
                    row.Visible = status.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
                }
            }
            UpdateHeaderCheckState();
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadDeliveriesFromDatabase()
        {
            dgvDeliveries.Rows.Clear();

            string query = @"
                SELECT 
                    d.DeliveryID,
                    co.CusOrderID,
                    c.CompanyName,
                    d.DeliveryDate,
                    (e.FirstName + ' ' + e.LastName) AS EmployeeName,
                    v.VehicleName,
                    v.LicensePlate,
                    d.Status,
                    co.shipping,
                    d.created_at
                FROM deliveries d
                LEFT JOIN customer_orders co ON d.CustOrderID = co.CusOrderID
                LEFT JOIN customers c ON co.CustomerID = c.CustID
                LEFT JOIN users u ON d.user_id = u.id
                LEFT JOIN employees e ON u.id = e.UserID
                LEFT JOIN vehicles v ON d.VehicleID = v.id
                ORDER BY d.DeliveryID DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string deliveryId = "DEL " + reader["DeliveryID"].ToString();
                            string orderId = "ORD-" + reader["CusOrderID"].ToString();
                            string customer = reader["CompanyName"]?.ToString() ?? "N/A";
                            string date = Convert.ToDateTime(reader["DeliveryDate"]).ToString("yyyy-MM-dd");
                            string employee = reader["EmployeeName"]?.ToString() ?? "Not Assigned";
                            string vehicle = reader["VehicleName"]?.ToString() ?? "-";
                            string status = reader["Status"]?.ToString() ?? "Pending";

                            int idx = dgvDeliveries.Rows.Add(false, orderId, customer, date, employee, vehicle, status, null);
                            dgvDeliveries.Rows[idx].Cells[0].Tag = deliveryId;
                            dgvDeliveries.Rows[idx].Tag = new
                            {
                                DeliveryID = reader["DeliveryID"],
                                LicensePlate = reader["LicensePlate"]?.ToString() ?? "N/A",
                                ShippingAddress = reader["shipping"]?.ToString() ?? "N/A",
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            };
                            dgvDeliveries.Rows[idx].Height = 45;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading deliveries: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false)) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                               checkedCount == 0 ? false :
                               checkedCount == visibleCount ? true : (bool?)null;
            dgvDeliveries.InvalidateCell(0, -1);
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

        private void dgvDeliveries_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
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
                TextRenderer.DrawText(e.Graphics, "ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

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
                string id = dgvDeliveries.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            if (e.ColumnIndex == dgvDeliveries.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);
                e.Handled = true; return;
            }

            if (e.ColumnIndex == dgvDeliveries.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status switch
                {
                    "Delivered" => Color.FromArgb(34, 197, 94),
                    "Pending" => Color.FromArgb(255, 159, 0),
                    "In Transit" => Color.FromArgb(255, 193, 7),
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
            }
        }

        private void dgvDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDeliveries.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvDeliveries.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvDeliveries.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvDeliveries.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvDeliveries.Columns["colActions"].Index)
            {
                var cellRect = dgvDeliveries.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvDeliveries.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;

                dynamic rowData = dgvDeliveries.Rows[e.RowIndex].Tag;
                long deliveryId = rowData.DeliveryID;

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditDelivery(deliveryId);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewDelivery(deliveryId);
            }
        }

        private void btnAddDelivery_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Add Delivery";
            var f = new AddDelivery { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenEditDelivery(long deliveryId)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Edit Delivery";
            var f = new EditDelivery(deliveryId.ToString()) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewDelivery(long deliveryId)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                dynamic rowData = row.Tag;
                if (rowData.DeliveryID == deliveryId)
                {
                    string deliveryIdStr = row.Cells[0].Tag?.ToString() ?? "";
                    p.navBar1.PageTitle = $"View Delivery: {deliveryIdStr}";

                    var view = new ViewDelivery
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,

                        CustomerOrderNo = row.Cells["colOrderID"].Value?.ToString() ?? "",
                        CustomerName = row.Cells["colCustomer"].Value?.ToString() ?? "",
                        DeliveryDate = DateTime.Parse(row.Cells["colDeliveryDate"].Value.ToString())
                                            .ToString("MMM dd, yyyy"),
                        Employee = row.Cells["colEmployee"].Value?.ToString() ?? "Not Assigned",
                        Vehicle = row.Cells["colVehicle"].Value?.ToString() ?? "-",
                        PlateNumber = rowData.LicensePlate ?? "N/A",
                        Status = row.Cells["colStatus"].Value?.ToString() ?? "Pending",
                        LastAttempt = GetLastAttempt(deliveryId),
                        CreatedDate = rowData.CreatedAt.ToString("MMM dd, yyyy hh:mm tt"),
                        ShippingAddress = rowData.ShippingAddress ?? "N/A"
                    };

                    p.pnlContent.Controls.Clear();
                    p.pnlContent.Controls.Add(view);
                    view.Show();
                    return;
                }
            }
        }

        private string GetLastAttempt(long deliveryId)
        {
            string query = @"SELECT TOP 1 AttemptDate, Status 
                           FROM delivery_attempts 
                           WHERE DeliveryID = @DeliveryID 
                           ORDER BY AttemptDate DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DeliveryID", deliveryId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime attemptDate = Convert.ToDateTime(reader["AttemptDate"]);
                                return attemptDate.ToString("MMM dd, yyyy hh:mm tt");
                            }
                        }
                    }
                }
            }
            catch { }

            return "No attempts yet";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string s = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow r in dgvDeliveries.Rows)
            {
                if (r.IsNewRow) continue;
                string id = r.Cells[0].Tag?.ToString().ToLower() ?? "";
                string order = r.Cells["colOrderID"].Value?.ToString().ToLower() ?? "";
                string cust = r.Cells["colCustomer"].Value?.ToString().ToLower() ?? "";
                r.Visible = string.IsNullOrEmpty(s) || id.Contains(s) || order.Contains(s) || cust.Contains(s);
            }
            UpdateHeaderCheckState();
        }

        public void RefreshData()
        {
            LoadDeliveriesFromDatabase();
            UpdateHeaderCheckState();
        }
    }
}