using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class UserAdmins : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public UserAdmins()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            ConfigureDataGridView();
            LoadSampleData();
            UpdateHeaderCheckState();
            dgvUsers.ClearSelection();
            dgvUsers.CurrentCell = null;
            dgvUsers.MouseDown += (s, e) => dgvUsers.CurrentCell = null;
        }

        private void ConfigureDataGridView()
        {
            dgvUsers.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.DefaultCellStyle.SelectionBackColor = dgvUsers.DefaultCellStyle.BackColor;
            dgvUsers.DefaultCellStyle.SelectionForeColor = dgvUsers.DefaultCellStyle.ForeColor;
            dgvUsers.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvUsers.RowTemplate.Height = 45;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn c in dgvUsers.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Column weights
            dgvUsers.Columns["colID"].FillWeight = 15;
            dgvUsers.Columns["colUsername"].FillWeight = 20;
            dgvUsers.Columns["colFullName"].FillWeight = 25;
            dgvUsers.Columns["colEmail"].FillWeight = 25;
            dgvUsers.Columns["colRole"].FillWeight = 15;
            dgvUsers.Columns["colActions"].FillWeight = 15;

            // Alignment
            dgvUsers.Columns["colUsername"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsers.Columns["colFullName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsers.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUsers.Columns["colRole"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void LoadSampleData()
        {
            AddRow(1, "admin1", "John Doe", "john@example.com", "Administrator");
            AddRow(2, "admin2", "Sarah Wilson", "sarah@company.com", "Manager");
            AddRow(3, "admin3", "Jane Smith", "jane@company.com", "Staff");
            AddRow(4, "admin4", "Mike Brown", "mike@store.com", "Cashier");
            AddRow(5, "admin5", "David Lee", "david@it.com", "IT Support");
        }

        private void AddRow(int id, string username, string fullName, string email, string role)
        {
            int idx = dgvUsers.Rows.Add(false, username, fullName, email, role, null);
            dgvUsers.Rows[idx].Cells["colID"].Tag = id;
            dgvUsers.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvUsers.Rows)
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
            dgvUsers.InvalidateCell(0, -1);
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

        private void dgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                string id = dgvUsers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id,
                        new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                e.Handled = true; return;
            }

            // Actions Column: Edit + View Icons
            if (e.ColumnIndex == dgvUsers.Columns["colActions"].Index && e.RowIndex >= 0)
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

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUsers.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvUsers.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvUsers.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvUsers.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvUsers.Columns["colActions"].Index)
            {
                var cellRect = dgvUsers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvUsers.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string userId = dgvUsers.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditUserAdmins(userId);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewUserAdmins(userId);
            }
        }

        private void OpenEditUserAdmins(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit User";
            var f = new EditUserAdmins(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewUserAdmins(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View User: {id}";
            var f = new ViewUserAdmins(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddUserAdmins_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add User";
            var f = new AddUserAdmins { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colID"].Tag?.ToString() ?? "";
                string username = row.Cells["colUsername"].Value?.ToString().ToLower() ?? "";
                string fullName = row.Cells["colFullName"].Value?.ToString().ToLower() ?? "";
                string email = row.Cells["colEmail"].Value?.ToString().ToLower() ?? "";
                string role = row.Cells["colRole"].Value?.ToString().ToLower() ?? "";

                row.Visible = string.IsNullOrEmpty(search) ||
                              id.Contains(search) || username.Contains(search) || fullName.Contains(search) || email.Contains(search) || role.Contains(search);
            }
            UpdateHeaderCheckState();
        }
    }
}