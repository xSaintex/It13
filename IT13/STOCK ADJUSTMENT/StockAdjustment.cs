using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class StockAdjustment : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public StockAdjustment()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // EXACT SAME GRID SETTINGS AS SupplierOrderList
            dgvAdjustment.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvAdjustment.MultiSelect = false;
            dgvAdjustment.ReadOnly = true;
            dgvAdjustment.AllowUserToAddRows = false;
            dgvAdjustment.AllowUserToDeleteRows = false;
            dgvAdjustment.RowHeadersVisible = false;

            dgvAdjustment.DefaultCellStyle.SelectionBackColor = dgvAdjustment.DefaultCellStyle.BackColor;
            dgvAdjustment.DefaultCellStyle.SelectionForeColor = dgvAdjustment.DefaultCellStyle.ForeColor;
            foreach (DataGridViewColumn c in dgvAdjustment.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvAdjustment.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvAdjustment.RowTemplate.Height = 45;
            dgvAdjustment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Column weights & alignment
            dgvAdjustment.Columns["colID"].MinimumWidth = 160;
            dgvAdjustment.Columns["colID"].FillWeight = 10;
            dgvAdjustment.Columns["colDate"].FillWeight = 12;
            dgvAdjustment.Columns["colProductName"].FillWeight = 30;
            dgvAdjustment.Columns["colAdjustmentType"].FillWeight = 12;
            dgvAdjustment.Columns["colPhysicalCount"].FillWeight = 10;
            dgvAdjustment.Columns["colRequestedBy"].FillWeight = 18;
            dgvAdjustment.Columns["colStatus"].FillWeight = 12;
            dgvAdjustment.Columns["colActions"].FillWeight = 14;

            dgvAdjustment.Columns["colDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAdjustment.Columns["colAdjustmentType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAdjustment.Columns["colPhysicalCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAdjustment.Columns["colRequestedBy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAdjustment.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAdjustment.Columns["colProductName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
            UpdateHeaderCheckState();

            dgvAdjustment.ClearSelection();
            dgvAdjustment.CurrentCell = null;
            dgvAdjustment.MouseDown += dgvAdjustment_MouseDown;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Pending", "Approved", "Rejected" });
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
            AddRow("ADJ-2025-001", "2025-04-10", "CCTV Camera Pro", "Increase", "50", "John Doe", "Pending");
            AddRow("ADJ-2025-002", "2025-04-08", "Wireless Speaker", "Decrease", "20", "Jane Smith", "Approved");
            AddRow("ADJ-2025-003", "2025-04-05", "Dual Monitor", "Increase", "15", "Mike Tan", "Rejected");
        }

        private void AddRow(string id, string date, string product, string type, string count, string requestedBy, string status)
        {
            int idx = dgvAdjustment.Rows.Add(false, date, product, type, count, requestedBy, status, null);
            dgvAdjustment.Rows[idx].Cells[0].Tag = id;
            dgvAdjustment.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvAdjustment.Rows)
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
            dgvAdjustment.InvalidateCell(0, -1);
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

        private void dgvAdjustment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            // Row checkbox + ID text
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
                string id = dgvAdjustment.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            // Actions column (Edit | View)
            if (e.ColumnIndex == dgvAdjustment.Columns["colActions"].Index && e.RowIndex >= 0)
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
            if (e.ColumnIndex == dgvAdjustment.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status switch
                {
                    "Approved" => Color.FromArgb(34, 197, 94),
                    "Pending" => Color.FromArgb(255, 159, 0),
                    "Rejected" => Color.FromArgb(239, 68, 68),
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

        private void dgvAdjustment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAdjustment.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0) // header checkbox
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvAdjustment.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvAdjustment.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // row checkbox
            {
                var row = dgvAdjustment.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvAdjustment.Columns["colActions"].Index)
            {
                var cellRect = dgvAdjustment.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvAdjustment.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvAdjustment.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditAdjustment(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewAdjustment(id);
            }
        }

        private void dgvAdjustment_MouseDown(object sender, MouseEventArgs e) => dgvAdjustment.CurrentCell = null;

        private void OpenEditAdjustment(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Edit Stock Adjustment";
            var f = new EditStockAdjustment(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void OpenViewAdjustment(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = $"View Adjustment: {id}";
            var f = new ViewStockAdjustment(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void btnAddAdjustment_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Add Stock Adjustment";
            var f = new AddStockAdjustment { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string s = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow r in dgvAdjustment.Rows)
            {
                if (r.IsNewRow) continue;
                string id = r.Cells[0].Tag?.ToString().ToLower() ?? "";
                string prod = r.Cells["colProductName"].Value?.ToString().ToLower() ?? "";
                r.Visible = string.IsNullOrEmpty(s) || id.Contains(s) || prod.Contains(s);
            }
            UpdateHeaderCheckState();
        }
    }
}