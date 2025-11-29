using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class CustomerList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public CustomerList()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.RowHeadersVisible = false;

            dgvCustomers.DefaultCellStyle.SelectionBackColor = dgvCustomers.DefaultCellStyle.BackColor;
            dgvCustomers.DefaultCellStyle.SelectionForeColor = dgvCustomers.DefaultCellStyle.ForeColor;
            foreach (DataGridViewColumn c in dgvCustomers.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvCustomers.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvCustomers.RowTemplate.Height = 45;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.Columns["colID"].MinimumWidth = 160;
            dgvCustomers.Columns["colID"].FillWeight = 10;
            dgvCustomers.Columns["colCompany"].FillWeight = 28;
            dgvCustomers.Columns["colContact"].FillWeight = 18;
            dgvCustomers.Columns["colPhone"].FillWeight = 16;
            dgvCustomers.Columns["colEmail"].FillWeight = 22;
            dgvCustomers.Columns["colPayment"].FillWeight = 12;
            dgvCustomers.Columns["colStatus"].FillWeight = 12;
            dgvCustomers.Columns["colActions"].FillWeight = 14;

            dgvCustomers.Columns["colContact"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colPayment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
            UpdateHeaderCheckState();
            dgvCustomers.ClearSelection();
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
            AddRow("CUST-001", "ABC Corporation", "John Doe", "+63 905 123 4567", "john@abc.com", "Net 30", "Active");
            AddRow("CUST-002", "XYZ Trading", "Jane Smith", "+63 917 987 6543", "jane@xyz.com", "Cash", "Inactive");
            AddRow("CUST-003", "Global Mart", "Mike Tan", "+63 923 456 7890", "mike@global.com", "Net 60", "Active");
            AddRow("CUST-004", "Tech Depot", "Ana Cruz", "+63 912 345 6789", "ana@tech.com", "Net 15", "Active");
            AddRow("CUST-005", "Prime Supplies", "Luis Reyes", "+63 998 765 4321", "luis@prime.com", "Cash", "Inactive");
        }

        private void AddRow(string id, string company, string contact, string phone, string email, string payment, string status)
        {
            int idx = dgvCustomers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            dgvCustomers.Rows[idx].Cells[0].Tag = id;
            dgvCustomers.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvCustomers.Rows)
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
            dgvCustomers.InvalidateCell(0, -1);
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

        private void dgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                string id = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true; return;
            }

            // Actions column
            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index && e.RowIndex >= 0)
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
            if (e.ColumnIndex == dgvCustomers.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status == "Active" ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);
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

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCustomers.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvCustomers.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvCustomers.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvCustomers.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCustomers.Columns["colActions"].Index)
            {
                var cellRect = dgvCustomers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvCustomers.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditCustomer(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewCustomer(id);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string s = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow r in dgvCustomers.Rows)
            {
                if (r.IsNewRow) continue;
                string id = r.Cells[0].Tag?.ToString().ToLower() ?? "";
                string company = r.Cells["colCompany"].Value?.ToString().ToLower() ?? "";
                string phone = r.Cells["colPhone"].Value?.ToString().ToLower() ?? "";
                string email = r.Cells["colEmail"].Value?.ToString().ToLower() ?? "";
                r.Visible = string.IsNullOrEmpty(s) || id.Contains(s) || company.Contains(s) || phone.Contains(s) || email.Contains(s);
            }
            UpdateHeaderCheckState();
        }

        // Navigation methods unchanged (OpenEditCustomer, OpenViewCustomer, btnAddCustomer_Click)
        private void OpenEditCustomer(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Edit Customer";
            var f = new EditCustomerList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void OpenViewCustomer(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = $"View Customer: {id}";
            var f = new ViewCustomerList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;
            p.navBar1.PageTitle = "Add Customer";
            var f = new AddCustomerList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear(); p.pnlContent.Controls.Add(f); f.Show();
        }
    }
}