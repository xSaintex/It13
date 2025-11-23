using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IT13
{
    public partial class ProductCategory : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ProductCategory()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();

            // COMPLETELY DISABLE SELECTION
            datagridviewcategory.SelectionMode = DataGridViewSelectionMode.CellSelect;
            datagridviewcategory.MultiSelect = false;
            datagridviewcategory.ReadOnly = true;
            datagridviewcategory.AllowUserToAddRows = false;
            datagridviewcategory.AllowUserToDeleteRows = false;
            datagridviewcategory.RowHeadersVisible = false;

            // Remove selection highlight
            datagridviewcategory.DefaultCellStyle.SelectionBackColor = datagridviewcategory.DefaultCellStyle.BackColor;
            datagridviewcategory.DefaultCellStyle.SelectionForeColor = datagridviewcategory.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn col in datagridviewcategory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            datagridviewcategory.DefaultCellStyle.Font = new Font("Poppins", 11F);
            datagridviewcategory.RowTemplate.Height = 45;

            // Use Fill mode with proper weights for better distribution
            datagridviewcategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Adjusted column weights for proper spacing
            datagridviewcategory.Columns["colID"].FillWeight = 18;
            datagridviewcategory.Columns["colName"].FillWeight = 30;
            datagridviewcategory.Columns["colDate"].FillWeight = 18;
            datagridviewcategory.Columns["colStatus"].FillWeight = 17;
            datagridviewcategory.Columns["colActions"].FillWeight = 17;

            // Set padding and alignment for all columns
            datagridviewcategory.Columns["colID"].DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);

            // Category Name - Center aligned
            datagridviewcategory.Columns["colName"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            datagridviewcategory.Columns["colName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridviewcategory.Columns["colName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Date - Center aligned
            datagridviewcategory.Columns["colDate"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            datagridviewcategory.Columns["colDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            datagridviewcategory.Columns["colStatus"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            datagridviewcategory.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            datagridviewcategory.Columns["colActions"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
            datagridviewcategory.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            LoadDataFromDatabase();
            UpdateHeaderCheckState();

            // Prevent any selection
            datagridviewcategory.ClearSelection();
            datagridviewcategory.CurrentCell = null;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Active", "Inactive" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;

            // Attach the event properly
            Filter.SelectedIndexChanged += Filter_SelectedIndexChanged;
            Filter.SelectedIndexChanged += (s, e) => Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        // NEW: Functional Filter Handler
        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Filter.SelectedIndex <= 0) return; // Ignore "Filter" placeholder

            ApplyFiltersAndSearch();
        }

        // Combined method to apply both Search + Filter
        private void ApplyFiltersAndSearch()
        {
            string searchText = txtboxsearch.Text.Trim().ToLower();
            string filterStatus = Filter.SelectedItem?.ToString();

            foreach (DataGridViewRow row in datagridviewcategory.Rows)
            {
                if (row.IsNewRow) continue;

                string id = row.Cells[0].Tag?.ToString() ?? "";
                string name = row.Cells["colName"].Value?.ToString() ?? "";
                string status = row.Cells["colStatus"].Value?.ToString() ?? "";

                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                    id.ToLower().Contains(searchText) ||
                                    name.ToLower().Contains(searchText);

                bool matchesFilter = string.IsNullOrEmpty(filterStatus) ||
                                    filterStatus == "Filter" ||
                                    filterStatus == "All" ||
                                    (filterStatus == "Active" && status.Equals("Active", StringComparison.OrdinalIgnoreCase)) ||
                                    (filterStatus == "Inactive" && status.Equals("Inactive", StringComparison.OrdinalIgnoreCase));

                // Show only if both conditions are satisfied
                row.Visible = matchesSearch && matchesFilter;
            }

            UpdateHeaderCheckState();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, CategoryName, Date, Status FROM categories ORDER BY id ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        datagridviewcategory.Rows.Clear();
                        while (reader.Read())
                        {
                            string id = "CAT-" + reader["id"].ToString().PadLeft(3, '0');
                            string name = reader["CategoryName"].ToString();
                            string date = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd");
                            string status = reader["Status"].ToString();
                            AddRow(id, name, date, status);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string id, string name, string date, string status)
        {
            int idx = datagridviewcategory.Rows.Add(false, name, date, status, null);
            datagridviewcategory.Rows[idx].Cells[0].Tag = id;
            datagridviewcategory.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in datagridviewcategory.Rows)
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

            datagridviewcategory.InvalidateCell(0, -1);
        }

        private void datagridviewcategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // HEADER CHECKBOX + "ID" TEXT
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
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // HEADER for Category Name - Centered
            if (e.RowIndex == -1 && e.ColumnIndex == datagridviewcategory.Columns["colName"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                TextRenderer.DrawText(e.Graphics, "Category Name",
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            // HEADER for Date - Centered
            if (e.RowIndex == -1 && e.ColumnIndex == datagridviewcategory.Columns["colDate"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                TextRenderer.DrawText(e.Graphics, "Date",
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            // HEADER for Status
            if (e.RowIndex == -1 && e.ColumnIndex == datagridviewcategory.Columns["colStatus"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                TextRenderer.DrawText(e.Graphics, "Status",
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 20, e.CellBounds.Y, e.CellBounds.Width - 20, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // HEADER for Actions
            if (e.RowIndex == -1 && e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                TextRenderer.DrawText(e.Graphics, "Actions",
                    new Font("Poppins", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 20, e.CellBounds.Y, e.CellBounds.Width - 20, e.CellBounds.Height),
                    Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // ROW CHECKBOX + ID TEXT
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

                string idText = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    TextRenderer.DrawText(e.Graphics, idText,
                        new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }
                e.Handled = true;
                return;
            }

            // CATEGORY NAME COLUMN - Centered
            if (e.RowIndex >= 0 && e.ColumnIndex == datagridviewcategory.Columns["colName"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                string nameText = e.Value?.ToString() ?? "";
                TextRenderer.DrawText(e.Graphics, nameText,
                    new Font("Poppins", 11F),
                    new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height),
                    Color.Black,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            // DATE COLUMN - Centered
            if (e.RowIndex >= 0 && e.ColumnIndex == datagridviewcategory.Columns["colDate"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                string dateText = e.Value?.ToString() ?? "";
                TextRenderer.DrawText(e.Graphics, dateText,
                    new Font("Poppins", 11F),
                    new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height),
                    Color.Black,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            // ACTIONS COLUMN - Left aligned with padding
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24, gap = 16;
                int x = e.CellBounds.X + 20; // Left aligned with 20px padding
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Handled = true;
                return;
            }

            // STATUS BADGE - Left aligned with padding
            if (e.ColumnIndex == datagridviewcategory.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.Equals("Active", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb(34, 197, 94)
                    : Color.FromArgb(239, 68, 68);

                // Measure text to create properly sized badge
                using (var font = new Font("Poppins", 10F, FontStyle.Bold))
                {
                    var sz = e.Graphics.MeasureString(status, font);
                    int badgeWidth = (int)sz.Width + 30;
                    int badgeHeight = e.CellBounds.Height - 16;

                    var rect = new Rectangle(e.CellBounds.X + 20, e.CellBounds.Y + 8, badgeWidth, badgeHeight);
                    using (var path = GetRoundedRect(rect, 10f))
                    using (var brush = new SolidBrush(bg))
                        e.Graphics.FillPath(brush, path);

                    using (var textBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(status, font, textBrush,
                            rect.X + (rect.Width - sz.Width) / 2,
                            rect.Y + (rect.Height - sz.Height) / 2);
                    }
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

        private void datagridviewcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            datagridviewcategory.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow row in datagridviewcategory.Rows)
                {
                    if (row.Visible && !row.IsNewRow)
                        row.Cells[0].Value = newState;
                }
                _headerCheckState = newState ? true : (bool?)false;
                datagridviewcategory.InvalidateCell(0, -1);
                return;
            }

            // Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = datagridviewcategory.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                UpdateHeaderCheckState();
                return;
            }

            // Edit/View icons - Updated for left alignment with 20px padding
            if (e.RowIndex >= 0 && e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                var cellRect = datagridviewcategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = datagridviewcategory.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;
                int iconSize = 24, gap = 16;
                int startX = 20; // Left aligned position with padding
                string id = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditCategory(id);
                else if (clickX >= startX + iconSize + gap && clickX < startX + iconSize + gap + iconSize)
                    OpenViewProdCategory(id);
            }
        }

        private void datagridviewcategory_MouseDown(object sender, MouseEventArgs e)
        {
            datagridviewcategory.CurrentCell = null;
        }

        private void OpenEditCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Category";
            var f = new EditCategory(categoryId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewProdCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Category Details";
            var f = new ViewProdCategory(categoryId) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Category";
            var f = new AddCategory() { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(f);
            f.Show();
        }

        // UPDATED: Search now reapplies filter too
        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        // Public method to refresh data
        public void RefreshData()
        {
            LoadDataFromDatabase();
            ApplyFiltersAndSearch(); // Reapply current filter + search after refresh
        }
    }
}