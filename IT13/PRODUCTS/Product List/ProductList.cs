// ProductList.cs - Updated for New Column Structure
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public partial class ProductList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false; // true = all checked, false = none, null = mixed

        public ProductList()
        {
            InitializeComponent();

            // Load icons (resized)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            // Setup UI
            SetupFilterComboBox();
            SetupExportComboBox();
            ConfigureDataGridView();
            LoadSampleData();
            UpdateHeaderCheckState();
        }

        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.AddRange(new[] { "Filter", "All", "In Stock", "Low Stock", "Out of Stock" });
            ComBoxFilters.SelectedIndex = 0;
            ComBoxFilters.ForeColor = Color.Gray;
            ComBoxFilters.SelectedIndexChanged += (s, e) =>
            {
                ComBoxFilters.ForeColor = ComBoxFilters.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplyFilter();
            };
        }

        private void SetupExportComboBox()
        {
            CombExport.Items.Clear();
            CombExport.Items.AddRange(new[] { "Export", "Excel", "PDF", "CSV" });
            CombExport.SelectedIndex = 0;
            CombExport.ForeColor = Color.Gray;
            CombExport.SelectedIndexChanged += (s, e) =>
                CombExport.ForeColor = CombExport.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }
        #endregion

        #region DataGridView Configuration
        private void ConfigureDataGridView()
        {
            // Clear and add columns
            datagridviewinventory.Columns.Clear();
            datagridviewinventory.Columns.AddRange(new DataGridViewColumn[]
            {
                Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8
            });

            // Grid settings
            datagridviewinventory.ReadOnly = true;
            datagridviewinventory.AllowUserToAddRows = false;
            datagridviewinventory.AllowUserToDeleteRows = false;
            datagridviewinventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewinventory.MultiSelect = false;
            datagridviewinventory.EnableHeadersVisualStyles = false;
            datagridviewinventory.RowTemplate.Height = 45;
            datagridviewinventory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            datagridviewinventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in datagridviewinventory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Selection style
            datagridviewinventory.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridviewinventory.ColumnHeadersDefaultCellStyle.BackColor;
            datagridviewinventory.DefaultCellStyle.SelectionBackColor =
                datagridviewinventory.DefaultCellStyle.BackColor;
            datagridviewinventory.DefaultCellStyle.SelectionForeColor =
                datagridviewinventory.DefaultCellStyle.ForeColor;
            datagridviewinventory.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // Column weights - Adjusted for consistent spacing
            datagridviewinventory.Columns["Column1"].MinimumWidth = 160; // PID with checkbox
            datagridviewinventory.Columns["Column1"].Width = 160;
            datagridviewinventory.Columns["Column1"].FillWeight = 10;
            datagridviewinventory.Columns["Column2"].FillWeight = 20; // Product Name
            datagridviewinventory.Columns["Column3"].FillWeight = 15; // Category
            datagridviewinventory.Columns["Column4"].FillWeight = 15; // Unit Cost
            datagridviewinventory.Columns["Column5"].FillWeight = 15; // Selling Price
            datagridviewinventory.Columns["Column6"].FillWeight = 20; // Primary Supplier
            datagridviewinventory.Columns["Column7"].FillWeight = 15; // Status
            datagridviewinventory.Columns["Column8"].FillWeight = 15; // Actions

            datagridviewinventory.Columns["Column2"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            datagridviewinventory.Columns["Column8"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Events
            datagridviewinventory.CellPainting += Datagridviewinventory_CellPainting;
            datagridviewinventory.CellClick += Datagridviewinventory_CellClick;
            txtboxsearch.TextChanged += Txtboxsearch_TextChanged;
            btnaddstock.Click += Btnaddstock_Click;
        }
        #endregion

        #region Sample Data
        private int _nextPid = 6; // Counter for next PID

        private void LoadSampleData()
        {
            datagridviewinventory.Rows.Clear();
            // Updated to match new column structure: PID, Name, Category, Unit Cost, Selling Price, Supplier, Status, Actions
            AddRow("PRD-001", "Wireless Mouse", "Electronics", "₱250.00", "₱350.00", "TechSupply Co.", "In Stock");
            AddRow("PRD-002", "USB-C Cable", "Accessories", "₱150.00", "₱250.00", "Cable World", "In Stock");
            AddRow("PRD-003", "Laptop Stand", "Furniture", "₱800.00", "₱1,200.00", "Office Plus", "Low Stock");
            AddRow("PRD-004", "HDMI Cable 2m", "Accessories", "₱300.00", "₱450.00", "Cable World", "Out of Stock");
            AddRow("PRD-005", "Bluetooth Speaker", "Electronics", "₱1,200.00", "₱1,800.00", "AudioTech", "In Stock");
        }

        private void AddRow(string pid, string name, string category, string unitCost, string sellingPrice, string supplier, string status)
        {
            int idx = datagridviewinventory.Rows.Add(false, name, category, unitCost, sellingPrice, supplier, status, null);
            var row = datagridviewinventory.Rows[idx];
            row.Cells[0].Tag = pid;
            row.Height = 45;
        }
        #endregion

        #region Add Product from AddProd Form
        /// <summary>
        /// Adds a product to the product list grid from the AddProd form
        /// </summary>
        public void AddProduct(AddProd.ProductItem productItem)
        {
            // Generate new PID
            string newPid = $"PRD-{_nextPid:D3}";
            _nextPid++;

            // Add the row to the grid
            AddRow(
                newPid,
                productItem.ProductName,
                productItem.Category,
                productItem.UnitCost,
                productItem.SellingPrice,
                productItem.PrimarySupplier,
                productItem.Status
            );

            // Update the UI
            UpdateHeaderCheckState();

            // Scroll to the top to show new product
            if (datagridviewinventory.Rows.Count > 0)
            {
                datagridviewinventory.FirstDisplayedScrollingRowIndex = 0;
            }
        }
        #endregion

        #region Checkbox Header State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;

            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false)) checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? (bool?)false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? true : (bool?)null;

            datagridviewinventory.InvalidateCell(0, -1); // Redraw header
        }
        #endregion

        #region Cell Painting
        private void Datagridviewinventory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // === HEADER: Checkbox + "PID" ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);

                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                string headerText = "PID";
                var headerFont = new Font("Tahoma", 10.2F, FontStyle.Bold);
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

            // === ROW: Checkbox + PID Text ===
            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string pid = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(pid))
                {
                    var textFont = new Font("Segoe UI", 11F);
                    var textSize = e.Graphics.MeasureString(pid, textFont);
                    var textRect = new Rectangle(
                        e.CellBounds.X + 30,
                        e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                        e.CellBounds.Width - 35,
                        e.CellBounds.Height);
                    e.Graphics.DrawString(pid, textFont, Brushes.Black, textRect);
                }
                e.Handled = true;
                return;
            }

            // === ACTION COLUMN: Edit + View Icons ===
            if (e.ColumnIndex == datagridviewinventory.Columns["Column8"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;

                // Align icons to the left side of the Actions column
                int x = e.CellBounds.X + 10; // 10px padding from left
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Handled = true;
            }
        }
        #endregion

        #region Cell Click
        private void Datagridviewinventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1 || e.ColumnIndex < 0) return;

            // === HEADER CHECKBOX CLICK ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                var headerRect = datagridviewinventory.GetCellDisplayRectangle(0, -1, false);
                var mousePos = datagridviewinventory.PointToClient(Cursor.Position);
                int clickX = mousePos.X - headerRect.X;

                if (clickX >= 0 && clickX <= 30)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault();
                    foreach (DataGridViewRow row in datagridviewinventory.Rows)
                    {
                        if (row.Visible && !row.IsNewRow)
                            row.Cells[0].Value = newState;
                    }
                    UpdateHeaderCheckState();
                    datagridviewinventory.InvalidateColumn(0);
                }
                return;
            }

            if (e.RowIndex < 0) return;

            // === ROW CHECKBOX TOGGLE ===
            if (e.ColumnIndex == 0)
            {
                var cellRect = datagridviewinventory.GetCellDisplayRectangle(0, e.RowIndex, false);
                var mousePos = datagridviewinventory.PointToClient(Cursor.Position);
                int clickX = mousePos.X - cellRect.X;

                if (clickX >= 0 && clickX <= 30)
                {
                    var row = datagridviewinventory.Rows[e.RowIndex];
                    bool current = (bool)(row.Cells[0].Value ?? false);
                    row.Cells[0].Value = !current;
                    datagridviewinventory.InvalidateCell(0, e.RowIndex);
                    UpdateHeaderCheckState();
                }
                return;
            }

            // === ACTION ICONS CLICK ===
            if (e.ColumnIndex == datagridviewinventory.Columns["Column8"].Index)
            {
                var cellRect = datagridviewinventory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewinventory.PointToClient(Cursor.Position);
                int clickX = mousePos.X - cellRect.X;

                int iconSize = 24;
                int gap = 16;
                int iconStartX = 10; // Aligned to left with 10px padding

                string productId = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= iconStartX && clickX < iconStartX + iconSize)
                    OpenEditProduct(productId);
                else if (clickX >= iconStartX + iconSize + gap && clickX < iconStartX + iconSize + gap + iconSize)
                    OpenViewProduct(productId);
            }
        }
        #endregion

        #region Navigation
        private void OpenEditProduct(string productId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Product";
            MessageBox.Show($"Edit: {productId}", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenViewProduct(string productId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Product Details";
            MessageBox.Show($"View: {productId}", "View Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Btnaddstock_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Product";

            var addForm = new AddProd
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

        #region Search & Filter
        private void ApplyFilter()
        {
            string selectedFilter = ComBoxFilters.SelectedItem?.ToString() ?? "Filter";

            // If "Filter" or "All" is selected, show all rows
            if (selectedFilter == "Filter" || selectedFilter == "All")
            {
                foreach (DataGridViewRow row in datagridviewinventory.Rows)
                {
                    if (!row.IsNewRow)
                        row.Visible = true;
                }
            }
            else
            {
                // Filter by status
                foreach (DataGridViewRow row in datagridviewinventory.Rows)
                {
                    if (row.IsNewRow) continue;

                    string status = row.Cells["Column7"].Value?.ToString() ?? "";
                    row.Visible = status.Equals(selectedFilter, StringComparison.OrdinalIgnoreCase);
                }
            }

            UpdateHeaderCheckState();
            UpdateRowCount();
        }

        private void UpdateRowCount()
        {
            int visibleCount = datagridviewinventory.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Visible && !r.IsNewRow);
            label1.Text = $"Showing {visibleCount} items";
        }

        private void Txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();

            // Get current filter selection
            string selectedFilter = ComBoxFilters.SelectedItem?.ToString() ?? "Filter";
            bool hasStatusFilter = selectedFilter != "Filter" && selectedFilter != "All";

            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.IsNewRow) continue;

                string pid = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells["Column2"].Value?.ToString().ToLower() ?? "";
                string category = row.Cells["Column3"].Value?.ToString().ToLower() ?? "";
                string status = row.Cells["Column7"].Value?.ToString() ?? "";

                // Check if matches search
                bool matchesSearch = string.IsNullOrEmpty(filter) ||
                                     pid.Contains(filter) ||
                                     name.Contains(filter) ||
                                     category.Contains(filter);

                // Check if matches status filter
                bool matchesStatusFilter = !hasStatusFilter ||
                                          status.Equals(selectedFilter, StringComparison.OrdinalIgnoreCase);

                // Row is visible only if it matches both search AND filter
                row.Visible = matchesSearch && matchesStatusFilter;
            }
            UpdateHeaderCheckState();
            UpdateRowCount();
        }
        #endregion

        #region Unused Designer Events (Keep for Compatibility)
        private void datagridviewinventory_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void ComBoxFilters_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        #endregion
    }
}