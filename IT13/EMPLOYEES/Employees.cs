using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class Employees : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public Employees()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            ConfigureDataGridView();
            LoadSampleData();
            UpdateHeaderCheckState();
            dgvEmployees.ClearSelection();
            dgvEmployees.CurrentCell = null;
            dgvEmployees.MouseDown += (s, e) => dgvEmployees.CurrentCell = null;
        }

        private void ConfigureDataGridView()
        {
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvEmployees.MultiSelect = false;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.AllowUserToDeleteRows = false;
            dgvEmployees.RowHeadersVisible = false;

            dgvEmployees.DefaultCellStyle.SelectionBackColor = dgvEmployees.DefaultCellStyle.BackColor;
            dgvEmployees.DefaultCellStyle.SelectionForeColor = dgvEmployees.DefaultCellStyle.ForeColor;
            dgvEmployees.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvEmployees.RowTemplate.Height = 45;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn c in dgvEmployees.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Column weights
            dgvEmployees.Columns["colID"].MinimumWidth = 160;
            dgvEmployees.Columns["colID"].FillWeight = 15;
            dgvEmployees.Columns["colFirstName"].FillWeight = 25;
            dgvEmployees.Columns["colLastName"].FillWeight = 25;
            dgvEmployees.Columns["colCreatedAt"].FillWeight = 20;
            dgvEmployees.Columns["colActions"].FillWeight = 15;

            // Alignment
            dgvEmployees.Columns["colFirstName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEmployees.Columns["colLastName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEmployees.Columns["colCreatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadSampleData()
        {
            AddRow(1, "Maria", "Johnson", "2025-10-11");
            AddRow(2, "John", "Smith", "2025-10-11");
            AddRow(3, "Emily", "Davis", "2025-10-11");
            AddRow(4, "Michael", "Brown", "2025-10-11");
            AddRow(5, "Sarah", "Wilson", "2025-10-11");
            AddRow(6, "David", "Anderson", "2025-10-11");
            AddRow(7, "Jessica", "Taylor", "2025-10-11");
            AddRow(8, "Robert", "Thomas", "2025-10-11");
            AddRow(9, "Olivia", "Martinez", "2025-10-11");
            AddRow(10, "James", "Garcia", "2025-10-11");
        }

        private void AddRow(int id, string firstName, string lastName, string createdAt)
        {
            int idx = dgvEmployees.Rows.Add(false, firstName, lastName, createdAt, null);
            dgvEmployees.Rows[idx].Cells["colID"].Tag = id;
            dgvEmployees.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvEmployees.Rows)
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
            dgvEmployees.InvalidateCell(0, -1);
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

        private void dgvEmployees_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header: Checkbox + "ID"
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

                TextRenderer.DrawText(e.Graphics, "ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.Handled = true; return;
            }

            // Row: Checkbox + ID Number
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

                string id = dgvEmployees.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id,
                        new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.Handled = true; return;
            }

            // Actions: Edit + View
            if (e.ColumnIndex == dgvEmployees.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);
                e.Handled = true;
            }
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmployees.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvEmployees.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvEmployees.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvEmployees.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvEmployees.Columns["colActions"].Index)
            {
                var cellRect = dgvEmployees.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvEmployees.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;

                string empId = dgvEmployees.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditEmployee(empId);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewEmployee(empId);
            }
        }

        private void OpenEditEmployee(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Employee";
            var f = new EditEmployee(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewEmployee(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Employee: {id}";
            var f = new ViewEmployee(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Employee";
            var f = new AddEmployee { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colID"].Tag?.ToString() ?? "";
                string first = row.Cells["colFirstName"].Value?.ToString().ToLower() ?? "";
                string last = row.Cells["colLastName"].Value?.ToString().ToLower() ?? "";
                row.Visible = string.IsNullOrEmpty(search) || id.Contains(search) || first.Contains(search) || last.Contains(search);
            }
            UpdateHeaderCheckState();
        }
    }
}