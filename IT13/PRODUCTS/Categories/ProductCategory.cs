// ---------------------------------------------------------------------
// ProductCategory.cs - FINAL (Matches CustomerList + Header Select All)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
namespace IT13
{
    public partial class ProductCategory : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false; // true = all, false = none, null = mixed
        public ProductCategory()
        {
            InitializeComponent();
            // === BIGGER ICONS ===
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();
            // === GRID SETTINGS ===
            datagridviewcategory.ReadOnly = true;
            datagridviewcategory.AllowUserToAddRows = false;
            datagridviewcategory.AllowUserToDeleteRows = false;
            datagridviewcategory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewcategory.MultiSelect = false;
            foreach (DataGridViewColumn col in datagridviewcategory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            datagridviewcategory.EnableHeadersVisualStyles = false;
            datagridviewcategory.ColumnHeadersDefaultCellStyle.SelectionBackColor =
            datagridviewcategory.ColumnHeadersDefaultCellStyle.BackColor;
            datagridviewcategory.DefaultCellStyle.SelectionBackColor =
            datagridviewcategory.DefaultCellStyle.BackColor;
            datagridviewcategory.DefaultCellStyle.SelectionForeColor =
            datagridviewcategory.DefaultCellStyle.ForeColor;
            datagridviewcategory.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridviewcategory.RowTemplate.Height = 45;
            datagridviewcategory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // === COLUMN LAYOUT ===
            datagridviewcategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Fix ID column width
            datagridviewcategory.Columns["colID"].MinimumWidth = 160;
            datagridviewcategory.Columns["colID"].Width = 160;
            datagridviewcategory.Columns["colID"].FillWeight = 10;
            datagridviewcategory.Columns["colName"].FillWeight = 48;
            datagridviewcategory.Columns["colDate"].FillWeight = 15;
            datagridviewcategory.Columns["colStatus"].FillWeight = 12;
            datagridviewcategory.Columns["colActions"].FillWeight = 15;
            datagridviewcategory.Columns["colName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            datagridviewcategory.Columns["colDate"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            datagridviewcategory.Columns["colStatus"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            LoadSampleData();
            UpdateHeaderCheckState();
        }
        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Active");
            Filter.Items.Add("Inactive");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;
            Filter.SelectedIndexChanged += (s, e) =>
            Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }
        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export");
            Export.Items.Add("Excel");
            Export.Items.Add("PDF");
            Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Black;
            Export.SelectedIndexChanged += (s, e) =>
            Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }
        #endregion
        #region Sample Data
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
            var row = datagridviewcategory.Rows[idx];
            row.Cells[0].Tag = id;
            row.Height = 45;
        }
        #endregion
        #region Header Checkbox State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in datagridviewcategory.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)row.Cells[0].Value) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
            checkedCount == 0 ? false :
            checkedCount == visibleCount ? true : (bool?)null;
            datagridviewcategory.InvalidateCell(0, -1); // Repaint header
        }
        #endregion
        #region Cell Painting (Header + Row + Icons)
        private void datagridviewcategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // === HEADER CHECKBOX + "ID" TEXT ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);
                // Draw "ID" header text
                string headerText = "ID";
                var headerFont = datagridviewcategory.ColumnHeadersDefaultCellStyle.Font;
                var textSize = e.Graphics.MeasureString(headerText, headerFont);
                var textRect = new Rectangle(
                e.CellBounds.X + 30,
                e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                e.CellBounds.Width - 35,
                e.CellBounds.Height);
                e.Graphics.DrawString(headerText, headerFont, Brushes.White, textRect);
                e.Handled = true;
                return;
            }
            if (e.RowIndex < 0) return;
            // === ROW: Checkbox + ID Text ===
            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                isChecked ? ButtonState.Checked : ButtonState.Normal);
                string idText = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    var textSize = e.Graphics.MeasureString(idText, new Font("Segoe UI", 11F));
                    var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);
                    e.Graphics.DrawString(idText, new Font("Segoe UI", 11F), Brushes.Black, textRect);
                }
                e.Handled = true;
                return;
            }
            // === ACTIONS: Edit + View Icons ===
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }
        #endregion
        #region Cell Click (Header + Row + Icons)
        private void datagridviewcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;
            // === HEADER CHECKBOX CLICK ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                var cellRect = datagridviewcategory.GetCellDisplayRectangle(0, -1, false);
                var mousePos = datagridviewcategory.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 24) // Checkbox area
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in datagridviewcategory.Rows)
                    {
                        if (row.Visible && !row.IsNewRow)
                            row.Cells[0].Value = newState;
                    }
                    UpdateHeaderCheckState();
                    datagridviewcategory.InvalidateColumn(0);
                }
                return;
            }
            if (e.RowIndex < 0) return;
            // === ROW CHECKBOX TOGGLE ===
            if (e.ColumnIndex == 0)
            {
                var row = datagridviewcategory.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !cur;
                datagridviewcategory.InvalidateCell(0, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }
            // === ACTION ICONS ===
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                var cellRect = datagridviewcategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewcategory.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string categoryId = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag.ToString();
                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditCategory(categoryId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewProdCategory(categoryId);
            }
        }
        #endregion
        #region Navigation
        private void OpenEditCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Category";
            var editForm = new EditCategory(categoryId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(editForm);
            editForm.Show();
        }
        private void OpenViewProdCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Category Details";
            var viewForm = new ViewProdCategory(categoryId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(viewForm);
            viewForm.Show();
        }
        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Category";
            var addForm = new AddCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }
        #endregion
        #region Search (Updates Header Checkbox)
        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridviewcategory.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells["colName"].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) ||
                id.Contains(filter) ||
                name.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState(); // Critical!
        }
        #endregion
    }
}