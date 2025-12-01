using System;
using System.Data.SqlClient;
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
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public SupplierList()
        {
            InitializeComponent();

            // Load icons
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            // IMPORTANT: Wire up the TextChanged event for real-time search
            txtSearch.TextChanged += txtSearch_TextChanged;

            // Setup
            SetupDataGridView();
            SetupFilterComboBox();
            SetupExportComboBox();
            LoadDataFromDatabase();
            UpdateHeaderCheckState();
            dgvSuppliers.ClearSelection();
        }

        private void SetupDataGridView()
        {
            dgvSuppliers.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvSuppliers.MultiSelect = false;
            dgvSuppliers.ReadOnly = true;
            dgvSuppliers.AllowUserToAddRows = false;
            dgvSuppliers.AllowUserToDeleteRows = false;
            dgvSuppliers.RowHeadersVisible = false;
            dgvSuppliers.DefaultCellStyle.SelectionBackColor = dgvSuppliers.DefaultCellStyle.BackColor;
            dgvSuppliers.DefaultCellStyle.SelectionForeColor = dgvSuppliers.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn c in dgvSuppliers.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvSuppliers.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvSuppliers.RowTemplate.Height = 45;
            dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Fixed column weights for better alignment
            dgvSuppliers.Columns["colID"].MinimumWidth = 140;
            dgvSuppliers.Columns["colID"].FillWeight = 12;
            dgvSuppliers.Columns["colCompany"].FillWeight = 20;
            dgvSuppliers.Columns["colContact"].FillWeight = 20;
            dgvSuppliers.Columns["colPhone"].FillWeight = 15;
            dgvSuppliers.Columns["colEmail"].FillWeight = 22;
            dgvSuppliers.Columns["colPayment"].FillWeight = 10;
            dgvSuppliers.Columns["colStatus"].FillWeight = 10;
            dgvSuppliers.Columns["colActions"].FillWeight = 12;

            // Alignment - All columns centered
            dgvSuppliers.Columns["colID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSuppliers.Columns["colCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colContact"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colPayment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvSuppliers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvSuppliers.Columns["colContact"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.AddRange(new object[] { "Filter", "All", "Active", "Inactive" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;

            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplyFiltersAndSearch();
            };
        }

        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) =>
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT id, CompanyName, CONCAT(FirstName, ' ', LastName) AS FullName,
                               PhoneNo, Email, PaymentTerms, Status
                        FROM suppliers
                        ORDER BY id ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgvSuppliers.Rows.Clear();
                        while (reader.Read())
                        {
                            int rawId = Convert.ToInt32(reader["id"]);
                            string formattedId = "SUP-" + rawId.ToString().PadLeft(3, '0');
                            string company = reader["CompanyName"]?.ToString() ?? "";
                            string contact = reader["FullName"]?.ToString() ?? "";
                            string phone = reader["PhoneNo"]?.ToString() ?? "";
                            string email = reader["Email"]?.ToString() ?? "";
                            string payment = reader["PaymentTerms"]?.ToString() ?? "";
                            string status = reader["Status"]?.ToString() ?? "Inactive";

                            AddRow(formattedId, company, contact, phone, email, payment, status);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string id, string company, string contact, string phone, string email, string payment, string status)
        {
            // Column order: colID(checkbox), colCompany, colContact, colPhone, colEmail, colPayment, colStatus, colActions
            int idx = dgvSuppliers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            var row = dgvSuppliers.Rows[idx];
            row.Tag = id; // Store the formatted ID (e.g., "SUP-001") in the Tag property
            row.Height = 45;
        }

        private void ApplyFiltersAndSearch()
        {
            // Get search text and convert to lowercase for case-insensitive search
            string searchText = txtSearch.Text.Trim().ToLower();

            // Get filter status
            string filterStatus = Filter.SelectedItem?.ToString() ?? "Filter";
            bool showAll = filterStatus == "Filter" || filterStatus == "All";
            bool showActive = filterStatus == "Active";
            bool showInactive = filterStatus == "Inactive";

            // Loop through all rows
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (row.IsNewRow) continue;

                // Get the ID from Tag property
                string id = row.Tag?.ToString()?.ToLower() ?? "";

                // Get values from cells
                string company = row.Cells["colCompany"].Value?.ToString()?.ToLower() ?? "";
                string contact = row.Cells["colContact"].Value?.ToString()?.ToLower() ?? "";
                string phone = row.Cells["colPhone"].Value?.ToString()?.ToLower() ?? "";
                string email = row.Cells["colEmail"].Value?.ToString()?.ToLower() ?? "";
                string status = row.Cells["colStatus"].Value?.ToString()?.ToLower() ?? "";

                // Check if the search text matches any field
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                     id.Contains(searchText) ||
                                     company.Contains(searchText) ||
                                     contact.Contains(searchText) ||
                                     phone.Contains(searchText) ||
                                     email.Contains(searchText);

                // Check if status matches the filter
                bool matchesFilter = showAll ||
                                     (showActive && status == "active") ||
                                     (showInactive && status == "inactive");

                // Show row only if it matches both search and filter criteria
                row.Visible = matchesSearch && matchesFilter;
            }

            UpdateHeaderCheckState();
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;

            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if (Convert.ToBoolean(row.Cells["colID"].Value ?? false))
                        checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? (bool?)false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? (bool?)true : null;

            dgvSuppliers.InvalidateCell(dgvSuppliers.Columns["colID"].Index, -1);
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
            // Header Checkbox + "ID" label
            if (e.RowIndex == -1 && e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (_headerCheckState == true)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }
                else if (_headerCheckState == null)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), r.X + 3, r.Y + 3, 10, 10);
                }

                TextRenderer.DrawText(e.Graphics, "ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                e.Handled = true;
                return;
            }

            // Row Checkbox + ID Display
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = Convert.ToBoolean(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 14, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (isChecked)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }

                // Draw the ID from Tag property
                string id = dgvSuppliers.Rows[e.RowIndex].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                {
                    TextRenderer.DrawText(e.Graphics, id,
                        new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 40, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
                e.Handled = true;
                return;
            }

            // Action Buttons (Edit & View)
            if (e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);
                e.Handled = true;
                return;
            }

            // Status Badge
            if (e.ColumnIndex == dgvSuppliers.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "Inactive";
                Color bg = status.Equals("Active", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb(34, 197, 94)
                    : Color.FromArgb(239, 68, 68);

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 10,
                    e.CellBounds.Width - 20, e.CellBounds.Height - 20);

                using (var path = GetRoundedRect(rect, 8f))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                using (var font = new Font("Poppins", 10F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var size = e.Graphics.MeasureString(status, font);
                    float x = e.CellBounds.X + (e.CellBounds.Width - size.Width) / 2;
                    float y = e.CellBounds.Y + (e.CellBounds.Height - size.Height) / 2;
                    e.Graphics.DrawString(status, font, textBrush, x, y);
                }
                e.Handled = true;
            }
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSuppliers.CurrentCell = null;

            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow row in dgvSuppliers.Rows)
                {
                    if (row.Visible && !row.IsNewRow)
                        row.Cells["colID"].Value = newState;
                }
                _headerCheckState = newState ? true : (bool?)false;
                UpdateHeaderCheckState();
                return;
            }

            // Row checkbox
            if (e.ColumnIndex == dgvSuppliers.Columns["colID"].Index && e.RowIndex >= 0)
            {
                var row = dgvSuppliers.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells["colID"].Value ?? false);
                row.Cells["colID"].Value = !current;
                UpdateHeaderCheckState();
                return;
            }

            // Action buttons
            if (e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                var cellRect = dgvSuppliers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvSuppliers.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;

                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;
                string id = dgvSuppliers.Rows[e.RowIndex].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditSupplier(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewSupplier(id);
            }
        }

        // Real-time Search - triggers as you type
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        // Search button click - also triggers search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        // Navigation
        private void OpenEditSupplier(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = "Edit Supplier";
            var f = new EditSupplierList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewSupplier(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = $"View Supplier: {id}";
            var f = new ViewSupplierList(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = "Add Supplier";
            var f = new AddSupplierList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        public void RefreshData()
        {
            LoadDataFromDatabase();
            ApplyFiltersAndSearch();
        }
    }
}