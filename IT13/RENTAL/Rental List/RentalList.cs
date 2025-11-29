using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class RentalList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public RentalList()
        {
            InitializeComponent();

            // Load icons with fallback
            _editIcon = new Bitmap(Properties.Resources.edit_icon ?? CreatePlaceholder(Color.FromArgb(255, 145, 0)), new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon ?? CreatePlaceholder(Color.FromArgb(0, 123, 255)), new Size(24, 24));

            SetupDataGridView();
            SetupFilterComboBox();
            SetupExportComboBox();
            LoadSampleData();
            UpdateHeaderCheckState();
            dgvRentals.ClearSelection();
        }

        private Image CreatePlaceholder(Color c)
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (var brush = new SolidBrush(c))
                    g.FillEllipse(brush, 4, 4, 16, 16);
            }
            return bmp;
        }

        private void SetupDataGridView()
        {
            dgvRentals.ReadOnly = true;
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvRentals.MultiSelect = false;
            dgvRentals.EnableHeadersVisualStyles = false;
            dgvRentals.DefaultCellStyle.SelectionBackColor = dgvRentals.DefaultCellStyle.BackColor;
            dgvRentals.DefaultCellStyle.SelectionForeColor = dgvRentals.DefaultCellStyle.ForeColor;
            dgvRentals.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvRentals.RowTemplate.Height = 45;
            dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvRentals.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Column weights & alignment
            dgvRentals.Columns["colCheck"].MinimumWidth = 180;
            dgvRentals.Columns["colCheck"].Width = 180;
            dgvRentals.Columns["colCheck"].FillWeight = 20;

            dgvRentals.Columns["colCustomerName"].FillWeight = 25;
            dgvRentals.Columns["colCustomerName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvRentals.Columns["colPhone"].FillWeight = 15;
            dgvRentals.Columns["colPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRentals.Columns["colAddress"].FillWeight = 30;
            dgvRentals.Columns["colAddress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRentals.Columns["colAddress"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvRentals.Columns["colItems"].FillWeight = 12;
            dgvRentals.Columns["colItems"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRentals.Columns["colTotal"].FillWeight = 15;
            dgvRentals.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvRentals.Columns["colStatus"].FillWeight = 13;
            dgvRentals.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRentals.Columns["colActions"].FillWeight = 15;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.AddRange(new[] { "Filter", "All", "Ongoing", "Completed", "Overdue", "Cancelled" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) =>
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.AddRange(new[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) =>
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadSampleData()
        {
            dgvRentals.Rows.Clear();
            AddRow("RENT-2025-001", "Juan Dela Cruz", "0917-123-4567", "123 Sampaguita St., Quezon City", 5, "₱45,000.00", "Ongoing");
            AddRow("RENT-2025-002", "Maria Clara", "0998-765-4321", "456 Rizal Ave., Manila", 8, "₱72,000.00", "Completed");
            AddRow("RENT-2025-003", "Jose Rizal", "0932-555-7890", "789 Bonifacio Global City, Taguig", 3, "₱28,500.00", "Overdue");
            AddRow("RENT-2025-004", "Andres Bonifacio", "0918-444-3210", "101 Mabini St., Pasig", 6, "₱58,000.00", "Ongoing");
        }

        private void AddRow(string rentId, string customer, string phone, string address, int items, string total, string status)
        {
            int idx = dgvRentals.Rows.Add(false, customer, phone, address, items, total, status, null);
            dgvRentals.Rows[idx].Cells["colCheck"].Tag = rentId;
            dgvRentals.Rows[idx].Height = 45;
        }

        // Called when returning from Add/Edit Rental
        public void RefreshData()
        {
            LoadSampleData(); // or your real LoadData()
            UpdateHeaderCheckState();
            dgvRentals.ClearSelection();
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvRentals.Rows)
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

            dgvRentals.InvalidateCell(0, -1); // Repaint header
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, float radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void dgvRentals_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header: Rent ID + Checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (_headerCheckState == true)
                {
                    using (var p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, r.X + 3, r.Y + 3, 10, 10);

                TextRenderer.DrawText(e.Graphics, "Rent ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                e.Handled = true;
                return;
            }

            // Row Checkbox + Rent ID
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (chk)
                {
                    using (var p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }

                string id = dgvRentals.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                e.Handled = true;
                return;
            }

            // Status Pill
            if (e.ColumnIndex == dgvRentals.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.ToLower() switch
                {
                    "ongoing" => Color.FromArgb(0, 123, 255),
                    "completed" => Color.FromArgb(34, 197, 94),
                    "overdue" => Color.FromArgb(220, 53, 69),
                    "cancelled" => Color.FromArgb(108, 117, 125),
                    _ => Color.Gray
                };

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                using (var font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var size = e.Graphics.MeasureString(status, font);
                    e.Graphics.DrawString(status, font, textBrush,
                        e.CellBounds.X + (e.CellBounds.Width - size.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - size.Height) / 2);
                }
                e.Handled = true;
                return;
            }

            // Action Icons (Edit & View)
            if (e.ColumnIndex == dgvRentals.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                const int size = 24, gap = 16;
                int total = size * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, size, size);
                e.Graphics.DrawImage(_viewIcon, x + size + gap, y, size, size);
                e.Handled = true;
            }
        }

        private void dgvRentals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRentals.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvRentals.Rows)
                    if (r.Visible && !r.IsNewRow)
                        r.Cells[0].Value = newState;

                _headerCheckState = newState ? true : (bool?)false;
                dgvRentals.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvRentals.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvRentals.Columns["colActions"].Index)
            {
                var cellRect = dgvRentals.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvRentals.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;

                const int size = 24, gap = 16, total = size * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string rentId = dgvRentals.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + size)
                    OpenEditRental(rentId);
                else if (clickX >= startX + size + gap && clickX < startX + total)
                    OpenViewRental(rentId);
            }
        }

        // FIXED: Now passes 'this' so Back button in AddRental works perfectly
        private void btnAddRental_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Add Rental";

            var addForm = new AddRental
            {
                Tag = this,                    // ← Critical: so back button knows where to return
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvRentals.Rows)
            {
                if (row.IsNewRow) continue;

                string id = row.Cells["colCheck"].Tag?.ToString().ToLower() ?? "";
                string customer = row.Cells["colCustomerName"].Value?.ToString().ToLower() ?? "";
                string phone = row.Cells["colPhone"].Value?.ToString().ToLower() ?? "";

                bool match = string.IsNullOrEmpty(filter) ||
                            id.Contains(filter) || customer.Contains(filter) || phone.Contains(filter);

                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }

        private void OpenEditRental(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Edit Rental";
            var form = new EditRental(id)
            {
                Tag = this,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewRental(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = $"View Rental: {id}";
            var form = new ViewRental(id)
            {
                Tag = this,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
    }
}