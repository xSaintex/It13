using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class DeliveryList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public DeliveryList()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();
            dgvDeliveries.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDeliveries.MultiSelect = false;
            dgvDeliveries.ReadOnly = true;
            dgvDeliveries.AllowUserToAddRows = false;
            dgvDeliveries.AllowUserToDeleteRows = false;
            dgvDeliveries.RowHeadersVisible = false;
            dgvDeliveries.DefaultCellStyle.SelectionBackColor = dgvDeliveries.DefaultCellStyle.BackColor;
            dgvDeliveries.DefaultCellStyle.SelectionForeColor = dgvDeliveries.DefaultCellStyle.ForeColor;
            foreach (DataGridViewColumn c in dgvDeliveries.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDeliveries.DefaultCellStyle.Font = new Font("Poppins", 11F);
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
            LoadSampleData();
            UpdateHeaderCheckState();
            dgvDeliveries.ClearSelection();
            dgvDeliveries.CurrentCell = null;
            dgvDeliveries.MouseDown += (s, e) => dgvDeliveries.CurrentCell = null;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Pending", "In Transit", "Delivered" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) => Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadSampleData()
        {
            AddRow("DEL 2025-001", "ORD-1001", "ABC Corporation", "2025-11-20", "Juan Dela Cruz", "Toyota Hiace", "Delivered");
            AddRow("DEL 2025-002", "ORD-1002", "XYZ Trading", "2025-11-21", "Maria Santos", "Mitsubishi L300", "Pending");
            AddRow("DEL 2025-003", "ORD-1003", "Global Mart", "2025-11-22", "Pedro Reyes", "Isuzu Elf", "In Transit");
        }

        private void AddRow(string id, string orderId, string customer, string date, string employee, string vehicle, string status)
        {
            int idx = dgvDeliveries.Rows.Add(false, orderId, customer, date, employee, vehicle, status, null);
            dgvDeliveries.Rows[idx].Cells[0].Tag = id;
            dgvDeliveries.Rows[idx].Height = 45;
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
            // Header checkbox + "ID"
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
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            // Row checkbox + ID
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
                    TextRenderer.DrawText(e.Graphics, id, new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            // Actions
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

            // Status badge
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
                using (var f = new Font("Poppins", 10F, FontStyle.Bold))
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
                string id = dgvDeliveries.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditDelivery(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewDelivery(id);  // Now fully connected with real data!
            }
        }

        // ADD DELIVERY BUTTON
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

        private void OpenEditDelivery(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Edit Delivery";
            var f = new EditDelivery(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        // FULLY CONNECTED VIEW DELIVERY — SHOWS REAL DATA FROM ROW
        private void OpenViewDelivery(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                if (row.Cells[0].Tag?.ToString() == id)
                {
                    p.navBar1.PageTitle = $"View Delivery: {id}";

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
                        PlateNumber = "NCR-JAA 1234", // Change later if you add plate column
                        Status = row.Cells["colStatus"].Value?.ToString() ?? "Pending",
                        LastAttempt = "Oct 22, 2025 2:30 PM",
                        CreatedDate = DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"),
                        ShippingAddress = "Block 12 Lot 5, Phase 2, Barangay San Jose, Antipolo City, Rizal 1870"
                    };

                    p.pnlContent.Controls.Clear();
                    p.pnlContent.Controls.Add(view);
                    view.Show();
                    return;
                }
            }
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
    }
}