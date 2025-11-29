// ProductCategory.cs 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IT13
{
    public partial class ProductCategory : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public ProductCategory()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // COMPLETELY DISABLE SELECTION - THIS IS THE KEY
            datagridviewcategory.SelectionMode = DataGridViewSelectionMode.CellSelect;
            datagridviewcategory.MultiSelect = false;
            datagridviewcategory.ReadOnly = true;
            datagridviewcategory.AllowUserToAddRows = false;
            datagridviewcategory.AllowUserToDeleteRows = false;
            datagridviewcategory.RowHeadersVisible = false;

            // Remove any selection highlight
            datagridviewcategory.DefaultCellStyle.SelectionBackColor = datagridviewcategory.DefaultCellStyle.BackColor;
            datagridviewcategory.DefaultCellStyle.SelectionForeColor = datagridviewcategory.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn col in datagridviewcategory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            datagridviewcategory.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            datagridviewcategory.RowTemplate.Height = 45;
            datagridviewcategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            datagridviewcategory.Columns["colID"].MinimumWidth = 160;
            datagridviewcategory.Columns["colID"].FillWeight = 10;
            datagridviewcategory.Columns["colName"].FillWeight = 48;
            datagridviewcategory.Columns["colDate"].FillWeight = 15;
            datagridviewcategory.Columns["colStatus"].FillWeight = 12;
            datagridviewcategory.Columns["colActions"].FillWeight = 15;

            datagridviewcategory.Columns["colName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            datagridviewcategory.Columns["colDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridviewcategory.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            LoadSampleData();
            UpdateHeaderCheckState();

            // FINAL FIX: Prevent any selection at all times
            datagridviewcategory.ClearSelection();
            datagridviewcategory.CurrentCell = null;
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
            AddRow("CAT-001", "High-Resolution CCTV Security Camera System", "2025-04-05", "Active");
            AddRow("CAT-002", "Wireless Bluetooth Speaker with Subwoofer", "2025-04-02", "Active");
            AddRow("CAT-003", "Dual Stereo Speaker Set for Home Theater", "2025-03-20", "Inactive");
            AddRow("CAT-004", "Portable Mini Speaker with LED Lights", "2025-03-15", "Active");
            AddRow("CAT-005", "Professional Studio Monitor Speakers", "2025-02-28", "Active");
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
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
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
                        new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }

                e.Handled = true;
                return;
            }

            // ACTIONS COLUMN
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                int iconSize = 24, gap = 16;
                int totalWidth = iconSize * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);

                e.Handled = true;
                return;
            }

            // STATUS BADGE
            if (e.ColumnIndex == datagridviewcategory.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.Equals("Active", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb(34, 197, 94)
                    : Color.FromArgb(239, 68, 68);

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

        private void datagridviewcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // BLOCK ALL SELECTION
            datagridviewcategory.CurrentCell = null;

            // ONLY ALLOW THESE THREE ACTIONS:

            // 1. Header checkbox
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

            // 2. Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = datagridviewcategory.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                UpdateHeaderCheckState();
                return;
            }

            // 3. Edit/View icons
            if (e.RowIndex >= 0 && e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                var cellRect = datagridviewcategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = datagridviewcategory.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;

                int iconSize = 24, gap = 16, total = iconSize * 2 + gap;
                int startX = (cellRect.Width - total) / 2;

                string id = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditCategory(id);
                else if (clickX >= startX + iconSize + gap && clickX < startX + total)
                    OpenViewProdCategory(id);
            }
        }

        // Prevent selection on mouse down
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

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtboxsearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridviewcategory.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells["colName"].Value?.ToString().ToLower() ?? "";
                row.Visible = string.IsNullOrEmpty(search) || id.Contains(search) || name.Contains(search);
            }
            UpdateHeaderCheckState();
        }
    }
}