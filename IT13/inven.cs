using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class inven : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false; // true = all, false = none, null = mixed
        private int _nextSid = 5; // Counter for next SID
        public inven()
        {
            InitializeComponent();
            // === BIGGER ICONS ===
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            ApplyRadiusToControls();
            SetupFilterComboBox();
            SetupExportComboBox();
            // === GRID SETTINGS ===
            datagridviewcustorder.ReadOnly = true;
            datagridviewcustorder.AllowUserToAddRows = false;
            datagridviewcustorder.AllowUserToDeleteRows = false;
            datagridviewcustorder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewcustorder.MultiSelect = false;
            foreach (DataGridViewColumn col in datagridviewcustorder.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            datagridviewcustorder.EnableHeadersVisualStyles = false;
            datagridviewcustorder.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridviewcustorder.ColumnHeadersDefaultCellStyle.BackColor;
            datagridviewcustorder.DefaultCellStyle.SelectionBackColor =
                datagridviewcustorder.DefaultCellStyle.BackColor;
            datagridviewcustorder.DefaultCellStyle.SelectionForeColor =
                datagridviewcustorder.DefaultCellStyle.ForeColor;
            datagridviewcustorder.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridviewcustorder.RowTemplate.Height = 45;
            datagridviewcustorder.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // === COLUMN LAYOUT ===
            datagridviewcustorder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Convert first column to checkbox + SID column
            datagridviewcustorder.Columns[0].MinimumWidth = 160;
            datagridviewcustorder.Columns[0].Width = 160;
            datagridviewcustorder.Columns[0].FillWeight = 10;
            datagridviewcustorder.Columns[1].FillWeight = 25; // Product Name
            datagridviewcustorder.Columns[2].FillWeight = 15; // Category
            datagridviewcustorder.Columns[3].FillWeight = 8; // QTY
            datagridviewcustorder.Columns[4].FillWeight = 12; // Unit Cost
            datagridviewcustorder.Columns[5].FillWeight = 12; // Total Cost
            datagridviewcustorder.Columns[6].FillWeight = 15; // Supplier
            datagridviewcustorder.Columns[7].FillWeight = 10; // Status
            datagridviewcustorder.Columns[8].FillWeight = 13; // Action
            datagridviewcustorder.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            datagridviewcustorder.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            LoadSampleData();
            UpdateHeaderCheckState();
            UpdateRowCount();
        }

        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.Add("Filter");
            ComBoxFilters.Items.Add("All");
            ComBoxFilters.Items.Add("Active");
            ComBoxFilters.Items.Add("Inactive");
            ComBoxFilters.SelectedIndex = 0;
            ComBoxFilters.ForeColor = Color.Gray;

            ComBoxFilters.SelectedIndexChanged += (s, e) =>
            {
                ComBoxFilters.ForeColor = ComBoxFilters.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                string filter = ComBoxFilters.SelectedItem?.ToString().ToLower() ?? "filter";
                CurrencyManager cm = (CurrencyManager)BindingContext[datagridviewcustorder.DataSource];
                if (cm != null) cm.SuspendBinding();

                foreach (DataGridViewRow row in datagridviewcustorder.Rows)
                {
                    if (row.IsNewRow) continue;

                    string status = row.Cells[7].Value?.ToString().ToLower() ?? "";
                    bool match = filter == "filter" || filter == "all" || status == filter;
                    row.Visible = match;
                }

                if (cm != null) cm.ResumeBinding();
                UpdateHeaderCheckState();
                UpdateRowCount();
            };
        }

        private void SetupExportComboBox()
        {
            ComBoxExporData.Items.Clear();
            ComBoxExporData.Items.Add("Export Data");
            ComBoxExporData.Items.Add("Excel");
            ComBoxExporData.Items.Add("PDF");
            ComBoxExporData.Items.Add("CSV");
            ComBoxExporData.SelectedIndex = 0;
            ComBoxExporData.ForeColor = Color.Black;
            ComBoxExporData.SelectedIndexChanged += (s, e) =>
                ComBoxExporData.ForeColor = ComBoxExporData.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }
        #endregion

        #region Sample Data
        private void LoadSampleData()
        {
            datagridviewcustorder.Rows.Clear();
            AddRow("1", "Dome Cameras", "cctv", "43 pcs.", "₱200.00", "₱8,600.00", "Verizon Communications Inc.", "Active");
            AddRow("2", "Bullet Cameras", "cctv", "18 pcs.", "₱300.00", "₱5,400.00", "AT&T Inc.", "Active");
            AddRow("3", "kisweksiwe", "siriejir", "18 pcs.", "₱400.00", "₱7,200.00", "AT&T Inc.", "Active");
            AddRow("4", "krokro", "cdsr", "10 pcs.", "₱400.00", "₱4,000.00", "AT&T Inc.", "Active");
            // Added Inactive row for testing filter
            AddRow("5", "Test Camera", "cctv", "10 pcs.", "₱500.00", "₱5,000.00", "Test Supplier", "Inactive");
        }

        private void AddRow(string sid, string name, string category, string qty, string unitCost, string totalCost, string supplier, string status)
        {
            int idx = datagridviewcustorder.Rows.Add(false, name, category, qty, unitCost, totalCost, supplier, status, null);
            var row = datagridviewcustorder.Rows[idx];
            row.Cells[0].Tag = sid; // Store SID in Tag
            row.Height = 45;
        }
        #endregion

        #region Add Stock Items from AddStock Form
        /// <summary>
        /// Adds multiple stock items to the inventory grid from the AddStock form
        /// </summary>
        public void AddStockItems(List<AddStock.StockItem> stockItems)
        {
            foreach (var item in stockItems)
            {
                // Generate new SID
                string newSid = _nextSid.ToString();
                _nextSid++;
                // Add the row to the grid
                AddRow(
                    newSid,
                    item.ProductName,
                    item.Category,
                    item.Quantity,
                    item.UnitCost,
                    item.TotalCost,
                    item.Supplier,
                    item.Status
                );
            }
            // Update the UI
            UpdateHeaderCheckState();
            UpdateRowCount();
            // Scroll to the top to show new items
            if (datagridviewcustorder.Rows.Count > 0)
            {
                datagridviewcustorder.FirstDisplayedScrollingRowIndex = 0;
            }
        }
        #endregion

        #region Header Checkbox State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in datagridviewcustorder.Rows)
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
            datagridviewcustorder.InvalidateCell(0, -1); // Repaint header
        }
        #endregion

        #region Cell Painting (Header + Row + Icons)
        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // === HEADER CHECKBOX + "SID" TEXT ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);
                // Draw "SID" header text
                string headerText = "SID";
                var headerFont = datagridviewcustorder.ColumnHeadersDefaultCellStyle.Font;
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
            // === ROW: Checkbox + SID Text ===
            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);
                string sidText = datagridviewcustorder.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(sidText))
                {
                    var textSize = e.Graphics.MeasureString(sidText, new Font("Segoe UI", 11F));
                    var textRect = new Rectangle(
                        e.CellBounds.X + 30,
                        e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                        e.CellBounds.Width - 35,
                        e.CellBounds.Height);
                    e.Graphics.DrawString(sidText, new Font("Segoe UI", 11F), Brushes.Black, textRect);
                }
                e.Handled = true;
                return;
            }
            // === ACTIONS: Edit + View Icons ===
            if (e.ColumnIndex == 8) // Column9 (Action column)
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
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;
            // === HEADER CHECKBOX CLICK ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                var cellRect = datagridviewcustorder.GetCellDisplayRectangle(0, -1, false);
                var mousePos = datagridviewcustorder.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 24) // Checkbox area
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in datagridviewcustorder.Rows)
                    {
                        if (row.Visible && !row.IsNewRow)
                            row.Cells[0].Value = newState;
                    }
                    UpdateHeaderCheckState();
                    datagridviewcustorder.InvalidateColumn(0);
                }
                return;
            }
            if (e.RowIndex < 0) return;
            // === ROW CHECKBOX TOGGLE ===
            if (e.ColumnIndex == 0)
            {
                var row = datagridviewcustorder.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !cur;
                datagridviewcustorder.InvalidateCell(0, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }
            // === ACTION ICONS ===
            if (e.ColumnIndex == 8) // Column9 (Action column)
            {
                var cellRect = datagridviewcustorder.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewcustorder.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string stockId = datagridviewcustorder.Rows[e.RowIndex].Cells[0].Tag.ToString();
                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditStock(stockId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewStock(stockId);
            }
        }
        #endregion

        #region Navigation
        private void OpenEditStock(string stockId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Stock";
            // TODO: Create EditStock form
            MessageBox.Show($"Edit Stock: {stockId}", "Navigate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenViewStock(string stockId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Stock Details";
            // TODO: Create ViewStock form
            MessageBox.Show($"View Stock: {stockId}", "Navigate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Search (Updates Header Checkbox)
        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();
            CurrencyManager cm = (CurrencyManager)BindingContext[datagridviewcustorder.DataSource];
            if (cm != null) cm.SuspendBinding();

            foreach (DataGridViewRow row in datagridviewcustorder.Rows)
            {
                if (row.IsNewRow) continue;

                string sid = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells[1].Value?.ToString().ToLower() ?? "";
                string category = row.Cells[2].Value?.ToString().ToLower() ?? "";

                bool match = string.IsNullOrEmpty(filter) ||
                             sid.Contains(filter) ||
                             name.Contains(filter) ||
                             category.Contains(filter);

                row.Visible = match;
            }

            if (cm != null) cm.ResumeBinding();
            UpdateHeaderCheckState();
            UpdateRowCount();
        }
        #endregion

        #region Update Row Count
        private void UpdateRowCount()
        {
            int visibleCount = datagridviewcustorder.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Visible && !r.IsNewRow);
            labelshow.Text = $"Showing {visibleCount} items";
        }
        #endregion

        #region ApplyRadiusToControls
        private void ApplyRadiusToControls()
        {
            txtboxsearch.BorderRadius = 5;
            btnsearchinv.BorderRadius = 5;
            ComBoxFilters.BorderRadius = 5;
            btnaddstock.BorderRadius = 5;
            ComBoxExporData.BorderRadius = 5;
            // Wire up events
            datagridviewcustorder.CellPainting += DataGridView_CellPainting;
            datagridviewcustorder.CellClick += DataGridView_CellClick;
        }
        #endregion

        #region Event Handlers
        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void datagridviewinventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnaddstock_Click(object sender, EventArgs e)
        {
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Add Stock";
                // Create AddStock form
                AddStock addStockForm = new AddStock();
                addStockForm.TopLevel = false;
                addStockForm.FormBorderStyle = FormBorderStyle.None;
                addStockForm.Dock = DockStyle.Fill;
                // Clear the content panel and add AddStock
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(addStockForm);
                addStockForm.Show();
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (datagridviewcustorder.IsCurrentCellDirty)
            {
                datagridviewcustorder.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion
    }
}