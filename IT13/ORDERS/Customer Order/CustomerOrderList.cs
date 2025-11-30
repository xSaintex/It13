// CustomerOrderList.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IT13
{
    public partial class CustomerOrderList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public CustomerOrderList()
        {
            InitializeComponent();

            // Load icons (make sure these resources exist: edit_icon, view_icon)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // === EXACT SAME GRID SETTINGS AS SupplierOrderList ===
            dgvOrders.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.RowHeadersVisible = false;

            // Remove selection highlight completely
            dgvOrders.DefaultCellStyle.SelectionBackColor = dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor = dgvOrders.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvOrders.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Column weights & alignment
            dgvOrders.Columns["colID"].MinimumWidth = 160;
            dgvOrders.Columns["colID"].FillWeight = 12;
            dgvOrders.Columns["colCompany"].FillWeight = 30;
            dgvOrders.Columns["colQty"].FillWeight = 10;
            dgvOrders.Columns["colTotalCost"].FillWeight = 15;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colDeliveryDate"].FillWeight = 13;
            dgvOrders.Columns["colActions"].FillWeight = 18;

            dgvOrders.Columns["colCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colTotalCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colTotalCost"].DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dgvOrders.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colDeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
            UpdateHeaderCheckState();

            // Final anti-selection
            dgvOrders.ClearSelection();
            dgvOrders.CurrentCell = null;

            // Prevent accidental selection
            dgvOrders.MouseDown += dgvOrders_MouseDown;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Pending", "Delivered", "Cancelled", "Processing" });
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
            AddRow("CO-2025-001", "TechNova Corp", 48, "₱156,800.00", "Delivered", "2025-11-28");
            AddRow("CO-2025-002", "Global Retail Inc.", 120, "₱89,500.00", "Pending", "2025-12-05");
            AddRow("CO-2025-003", "Prime Distributors", 35, "₱210,300.50", "Processing", "2025-12-01");
            AddRow("CO-2025-004", "Metro Traders Ltd.", 80, "₱67,200.00", "Delivered", "2025-11-20");
            AddRow("CO-2025-005", "Alpha Solutions", 15, "₱445,900.00", "Cancelled", "2025-11-25");
        }

        private void AddRow(string id, string company, int qty, string total, string status, string deliveryDate)
        {
            int idx = dgvOrders.Rows.Add(false, company, qty, total, status, deliveryDate, null);
            dgvOrders.Rows[idx].Cells[0].Tag = id;
            dgvOrders.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvOrders.Rows)
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
            dgvOrders.InvalidateCell(0, -1);
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // HEADER: Checkbox + "ID"
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
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
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // ROW: Checkbox + ID Text
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
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
                        new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
                e.Handled = true;
                return;
            }

            // ACTIONS: Edit | View only
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24, gap = 20;
                int totalWidth = iconSize * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
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
                    "Processing" => Color.FromArgb(59, 130, 246),
                    "Cancelled" => Color.FromArgb(239, 68, 68),
                    _ => Color.Gray
                };

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                using (var font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
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

            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    if (row.Visible && !row.IsNewRow)
                        row.Cells[0].Value = newState;
                }
                _headerCheckState = newState ? true : (bool?)false;
                dgvOrders.InvalidateCell(0, -1);
                return;
            }

            // Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                UpdateHeaderCheckState();
                return;
            }

            // Actions: Edit | View
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvOrders.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int iconSize = 24, gap = 20, total = iconSize * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditOrder(id);
                else if (clickX >= startX + iconSize + gap && clickX < startX + total)
                    OpenViewOrder(id);
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
            parent.navBar1.PageTitle = "Edit Customer Order";
            var f = new EditCustomerOrder(orderId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Order: {orderId}";
            var f = new ViewCustomerOrder(orderId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Customer Order";
            var f = new AddCustomerOrder { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string company = row.Cells["colCompany"].Value?.ToString().ToLower() ?? "";
                row.Visible = string.IsNullOrEmpty(search) || id.Contains(search) || company.Contains(search);
            }
            UpdateHeaderCheckState();
        }
    }
}