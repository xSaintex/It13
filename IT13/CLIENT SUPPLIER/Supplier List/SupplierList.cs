using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class SupplierList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public SupplierList()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            dgvSuppliers.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvSuppliers.MultiSelect = false;
            dgvSuppliers.ReadOnly = true;
            dgvSuppliers.AllowUserToAddRows = false;
            dgvSuppliers.AllowUserToDeleteRows = false;
            dgvSuppliers.RowHeadersVisible = false;
            dgvSuppliers.DefaultCellStyle.SelectionBackColor = dgvSuppliers.DefaultCellStyle.BackColor;
            dgvSuppliers.DefaultCellStyle.SelectionForeColor = dgvSuppliers.DefaultCellStyle.ForeColor;
            foreach (DataGridViewColumn c in dgvSuppliers.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvSuppliers.DefaultCellStyle.Font = new Font("Poppins", 11F);
            dgvSuppliers.RowTemplate.Height = 45;
            dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvSuppliers.Columns["colID"].MinimumWidth = 160;
            dgvSuppliers.Columns["colID"].FillWeight = 10;
            dgvSuppliers.Columns["colCompany"].FillWeight = 28;
            dgvSuppliers.Columns["colContact"].FillWeight = 18;
            dgvSuppliers.Columns["colPhone"].FillWeight = 16;
            dgvSuppliers.Columns["colEmail"].FillWeight = 22;
            dgvSuppliers.Columns["colPayment"].FillWeight = 12;
            dgvSuppliers.Columns["colStatus"].FillWeight = 12;
            dgvSuppliers.Columns["colActions"].FillWeight = 14;

            dgvSuppliers.Columns["colContact"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colPayment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
            UpdateHeaderCheckState();
            dgvSuppliers.ClearSelection();
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Active", "Inactive" });
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
            AddRow("SUP-001", "PrimeTech Distributors", "Mark Lim", "+63 917 555 1234", "mark@primetech.com", "Net 30", "Active");
            AddRow("SUP-002", "Global Supplies Inc.", "Sarah Go", "+63 922 888 9999", "sarah@global.com", "Net 45", "Active");
            AddRow("SUP-003", "Metro Trading Co.", "John Tan", "+63 905 333 4444", "john@metro.com", "Cash", "Inactive");
            AddRow("SUP-004", "Asia Pacific Traders", "Lisa Wong", "+63 918 777 8888", "lisa@apt.com.ph", "Net 60", "Active");
            AddRow("SUP-005", "Sunrise Enterprises", "David Cruz", "+63 927 111 2222", "david@sunrise.ph", "Net 30", "Active");
        }

        private void AddRow(string id, string company, string contact, string phone, string email, string payment, string status)
        {
            int idx = dgvSuppliers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            dgvSuppliers.Rows[idx].Cells[0].Tag = id;
            dgvSuppliers.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
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
            dgvSuppliers.InvalidateCell(0, -1);
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

        private void dgvSuppliers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                string id = dgvSuppliers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            if (e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);
                e.Handled = true; return;
            }

            if (e.ColumnIndex == dgvSuppliers.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status == "Active" ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);
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

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSuppliers.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvSuppliers.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvSuppliers.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvSuppliers.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index)
            {
                var cellRect = dgvSuppliers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvSuppliers.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvSuppliers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditSupplier(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewSupplier(id);
            }
        }

        // SEARCH BUTTON — NOW WORKS!
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string company = row.Cells["colCompany"].Value?.ToString().ToLower() ?? "";
                string phone = row.Cells["colPhone"].Value?.ToString().ToLower() ?? "";
                string email = row.Cells["colEmail"].Value?.ToString().ToLower() ?? "";

                bool match = string.IsNullOrEmpty(filter) ||
                             id.Contains(filter) ||
                             company.Contains(filter) ||
                             phone.Contains(filter) ||
                             email.Contains(filter);

                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }

        private void OpenEditSupplier(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Edit Supplier";
            var f = new EditSupplierList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void OpenViewSupplier(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = $"View Supplier: {id}";
            var f = new ViewSupplierList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Add Supplier";
            var f = new AddSupplierList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }
    }
}