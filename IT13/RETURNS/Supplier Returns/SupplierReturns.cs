using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class SupplierReturns : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public SupplierReturns()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon ?? CreatePlaceholder(Color.FromArgb(255, 145, 0)), new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon ?? CreatePlaceholder(Color.FromArgb(0, 123, 255)), new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            dgvReturns.ReadOnly = true;
            dgvReturns.AllowUserToAddRows = false;
            dgvReturns.AllowUserToDeleteRows = false;
            dgvReturns.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvReturns.MultiSelect = false;
            dgvReturns.EnableHeadersVisualStyles = false;
            dgvReturns.DefaultCellStyle.SelectionBackColor = dgvReturns.DefaultCellStyle.BackColor;
            dgvReturns.DefaultCellStyle.SelectionForeColor = dgvReturns.DefaultCellStyle.ForeColor;
            dgvReturns.DefaultCellStyle.Font = new Font("Poppins", 11F);
            dgvReturns.RowTemplate.Height = 45;
            dgvReturns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvReturns.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // COLUMN WIDTHS & ALIGNMENT
            dgvReturns.Columns["colCheck"].MinimumWidth = 180;
            dgvReturns.Columns["colCheck"].Width = 180;
            dgvReturns.Columns["colCheck"].FillWeight = 18;

            dgvReturns.Columns["colOrderID"].MinimumWidth = 180;
            dgvReturns.Columns["colOrderID"].Width = 180;
            dgvReturns.Columns["colOrderID"].FillWeight = 18;
            dgvReturns.Columns["colOrderID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns["colSupplierName"].FillWeight = 28;
            dgvReturns.Columns["colSupplierName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvReturns.Columns["colSupplierName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns["colQty"].FillWeight = 9;
            dgvReturns.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns["colTotal"].FillWeight = 14;
            dgvReturns.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvReturns.Columns["colStatus"].FillWeight = 13;
            dgvReturns.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns["colReturnDate"].FillWeight = 14;
            dgvReturns.Columns["colReturnDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns["colActions"].FillWeight = 16;

            LoadSampleData();
            UpdateHeaderCheckState();
            dgvReturns.ClearSelection();
        }

        private Image CreatePlaceholder(Color c)
        {
            var b = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(b)) g.Clear(Color.Transparent);
            return b;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter"); Filter.Items.Add("All"); Filter.Items.Add("Pending"); Filter.Items.Add("Approved"); Filter.Items.Add("Rejected");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) => Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export"); Export.Items.Add("Excel"); Export.Items.Add("PDF"); Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadSampleData()
        {
            AddRow("SRET-2025-001", "ORD-001", "TechSupply Co.", 10, "₱85,000.00", "Pending", "2025-11-20");
            AddRow("SRET-2025-002", "ORD-003", "Global Traders", 5, "₱42,500.00", "Approved", "2025-11-18");
            AddRow("SRET-2025-003", "ORD-005", "Office Plus", 3, "₱28,000.00", "Rejected", "2025-11-15");
            AddRow("SRET-2025-004", "ORD-007", "Cable World", 8, "₱96,000.00", "Pending", "2025-11-19");
        }

        private void AddRow(string retId, string orderId, string supplier, int qty, string total, string status, string date)
        {
            int idx = dgvReturns.Rows.Add(false, orderId, supplier, qty, total, status, date, null);
            dgvReturns.Rows[idx].Cells["colCheck"].Tag = retId;
            dgvReturns.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvReturns.Rows)
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
            dgvReturns.InvalidateCell(0, -1);
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

        private void dgvReturns_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                TextRenderer.DrawText(e.Graphics, "Return ID",
                    new Font("Poppins", 12F, FontStyle.Bold),
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
                string id = dgvReturns.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            if (e.ColumnIndex == dgvReturns.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.ToLower() switch
                {
                    "approved" => Color.FromArgb(34, 197, 94),
                    "pending" => Color.FromArgb(255, 193, 7),
                    "rejected" => Color.FromArgb(220, 53, 69),
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
                return;
            }

            if (e.ColumnIndex == dgvReturns.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                const int iconSize = 24, gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Handled = true;
            }
        }

        private void dgvReturns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvReturns.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvReturns.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvReturns.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvReturns.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReturns.Columns["colActions"].Index)
            {
                var cellRect = dgvReturns.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvReturns.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                const int iconSize = 24, gap = 16, total = iconSize * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string retId = dgvReturns.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditReturn(retId);
                else if (clickX >= startX + iconSize + gap && clickX < startX + total)
                    OpenViewReturn(retId);
            }
        }

        private void btnAddReturn_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Supplier Return";
            var addForm = new AddSupplierReturns
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            addForm.Tag = this;
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvReturns.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colCheck"].Tag?.ToString().ToLower() ?? "";
                string orderId = row.Cells["colOrderID"].Value?.ToString().ToLower() ?? "";
                string supplier = row.Cells["colSupplierName"].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) || id.Contains(filter) || orderId.Contains(filter) || supplier.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }

        private void OpenEditReturn(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Supplier Return";
            var form = new EditSupplierReturns(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewReturn(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Return: {id}";
            var form = new ViewSupplierReturns(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
    }
}