using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class CustOrder : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false; // true = all, false = none, null = mixed
        private int _nextOrderId = 5; // Counter for next order ID

        public CustOrder()
        {
            InitializeComponent();
            // === BIGGER ICONS ===
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            // Apply rounded corners to controls
            ApplyRadiusToControls();
            // Setup ComboBoxes
            SetupFilterComboBox();
            SetupExportComboBox();
            // === GRID SETTINGS ===
            datagridviewinventory.ReadOnly = true;
            datagridviewinventory.AllowUserToAddRows = false;
            datagridviewinventory.AllowUserToDeleteRows = false;
            datagridviewinventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewinventory.MultiSelect = false;
            foreach (DataGridViewColumn col in datagridviewinventory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            datagridviewinventory.EnableHeadersVisualStyles = false;
            datagridviewinventory.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridviewinventory.ColumnHeadersDefaultCellStyle.BackColor;
            datagridviewinventory.DefaultCellStyle.SelectionBackColor =
                datagridviewinventory.DefaultCellStyle.BackColor;
            datagridviewinventory.DefaultCellStyle.SelectionForeColor =
                datagridviewinventory.DefaultCellStyle.ForeColor;
            datagridviewinventory.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridviewinventory.RowTemplate.Height = 45;
            datagridviewinventory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // === COLUMN LAYOUT ===
            datagridviewinventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Convert first column to checkbox + ID column
            datagridviewinventory.Columns[0].MinimumWidth = 160;
            datagridviewinventory.Columns[0].Width = 160;
            datagridviewinventory.Columns[0].FillWeight = 10; // ID
            datagridviewinventory.Columns[1].FillWeight = 25; // Company name
            datagridviewinventory.Columns[2].FillWeight = 15; // QTY
            datagridviewinventory.Columns[3].FillWeight = 12; // Total Cost
            datagridviewinventory.Columns[4].FillWeight = 12; // Status
            datagridviewinventory.Columns[5].FillWeight = 15; // Delivery Date
            datagridviewinventory.Columns[6].FillWeight = 13; // Actions
            datagridviewinventory.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            datagridviewinventory.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Load sample data
            LoadSampleData();
            UpdateHeaderCheckState();
            UpdateRowCount();
        }

        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.Add("Filter");
            ComBoxFilters.Items.Add("All Orders");
            ComBoxFilters.Items.Add("Pending");
            ComBoxFilters.Items.Add("Processing");
            ComBoxFilters.Items.Add("Completed");
            ComBoxFilters.Items.Add("Cancelled");
            ComBoxFilters.SelectedIndex = 0;
            ComBoxFilters.ForeColor = Color.Black;
            ComBoxFilters.SelectedIndexChanged += (s, e) =>
                ComBoxFilters.ForeColor = ComBoxFilters.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
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
            datagridviewinventory.Rows.Clear();
            AddRow("1", "Verizon Communications", "43", "₱8,600.00", "Pending", "2025-12-01");
            AddRow("2", "AT&T Inc.", "18", "₱5,400.00", "Processing", "2025-12-05");
            AddRow("3", "Comcast Corp.", "18", "₱7,200.00", "Completed", "2025-11-30");
            AddRow("4", "T-Mobile US", "10", "₱4,000.00", "Cancelled", "2025-12-10");
        }

        private void AddRow(string id, string companyName, string qty, string totalCost, string status, string deliveryDate)
        {
            int idx = datagridviewinventory.Rows.Add(false, companyName, qty, totalCost, status, deliveryDate, null);
            var row = datagridviewinventory.Rows[idx];
            row.Cells[0].Tag = id; // Store ID in Tag
            row.Height = 45;
        }
        #endregion

        #region Add Order Items from AddCustOrder Form
        public void AddNewOrderToTable(AddCustOrder.OrderData orderData)
        {
            string newId = _nextOrderId.ToString();
            _nextOrderId++;
            AddRow(
                newId,
                orderData.CompanyName,
                orderData.ItemCount.ToString(),
                orderData.Total.ToString("₱#,##0.00"),
                orderData.Status,
                orderData.DeliveryDate
            );
            UpdateHeaderCheckState();
            UpdateRowCount();
            if (datagridviewinventory.Rows.Count > 0)
            {
                datagridviewinventory.FirstDisplayedScrollingRowIndex = 0;
            }
        }
        #endregion

        #region Header Checkbox State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in datagridviewinventory.Rows)
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
            datagridviewinventory.InvalidateCell(0, -1); // Repaint header
        }
        #endregion

        #region Cell Painting (Header + Row + Icons)
        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                var headerFont = datagridviewinventory.ColumnHeadersDefaultCellStyle.Font;
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
                string idText = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
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
            if (e.ColumnIndex == 6) // Actions column
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
                var cellRect = datagridviewinventory.GetCellDisplayRectangle(0, -1, false);
                var mousePos = datagridviewinventory.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 24) // Checkbox area
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
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
                var row = datagridviewinventory.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !cur;
                datagridviewinventory.InvalidateCell(0, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }
            // === ACTION ICONS ===
            if (e.ColumnIndex == 6) // Actions column
            {
                var cellRect = datagridviewinventory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewinventory.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string orderId = datagridviewinventory.Rows[e.RowIndex].Cells[0].Tag.ToString();
                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditOrder(orderId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewOrder(orderId);
            }
        }
        #endregion

        #region Navigation
        private void OpenEditOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Customer Order";
            MessageBox.Show($"Edit Order: {orderId}", "Navigate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenViewOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Order Details";
            MessageBox.Show($"View Order: {orderId}", "Navigate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Search Functionality
        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridviewinventory.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string companyName = row.Cells[1].Value?.ToString().ToLower() ?? "";
                string status = row.Cells[4].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) ||
                             id.Contains(filter) ||
                             companyName.Contains(filter) ||
                             status.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
            UpdateRowCount();
        }

        private void txtboxsearch_Click(object sender, EventArgs e)
        {
            if (txtboxsearch.Text == txtboxsearch.PlaceholderText)
            {
                txtboxsearch.Text = "";
                txtboxsearch.ForeColor = Color.Black;
            }
            txtboxsearch.Visible = true; // Ensure textbox remains visible
        }

        private void txtboxsearch_Enter(object sender, EventArgs e)
        {
            if (txtboxsearch.Text == txtboxsearch.PlaceholderText)
            {
                txtboxsearch.Text = "";
                txtboxsearch.ForeColor = Color.Black;
            }
        }

        private void txtboxsearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxsearch.Text))
            {
                txtboxsearch.Text = txtboxsearch.PlaceholderText;
                txtboxsearch.ForeColor = Color.Gray;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            txtboxsearch_TextChanged(sender, e); // Trigger search logic
        }
        #endregion

        #region Update Row Count
        private void UpdateRowCount()
        {
            int visibleCount = datagridviewinventory.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Visible && !r.IsNewRow);
            labelshow.Text = $"Showing {visibleCount} items";
        }
        #endregion

        #region ApplyRadiusToControls
        private void ApplyRadiusToControls()
        {
            txtboxsearch.BorderRadius = 5;
            btnsearch.BorderRadius = 5;
            ComBoxFilters.BorderRadius = 5;
            btnaddorder.BorderRadius = 5;
            ComBoxExporData.BorderRadius = 5;
            // Wire up events
            datagridviewinventory.CellPainting += DataGridView_CellPainting;
            datagridviewinventory.CellClick += DataGridView_CellClick;
            datagridviewinventory.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
        }
        #endregion

        #region Event Handlers
        private void btnaddorder_Click(object sender, EventArgs e)
        {
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Add Customer Order";
                AddCustOrder addCustOrderForm = new AddCustOrder();
                addCustOrderForm.TopLevel = false;
                addCustOrderForm.FormBorderStyle = FormBorderStyle.None;
                addCustOrderForm.Dock = DockStyle.Fill;
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(addCustOrderForm);
                addCustOrderForm.Show();
            }
        }

        private void ComBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComBoxFilters.SelectedIndex > 0)
            {
                string filter = ComBoxFilters.SelectedItem.ToString();
                foreach (DataGridViewRow row in datagridviewinventory.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (filter == "All Orders")
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        string status = row.Cells[4].Value?.ToString() ?? "";
                        row.Visible = status.Equals(filter, StringComparison.OrdinalIgnoreCase);
                    }
                }
                UpdateHeaderCheckState();
                UpdateRowCount();
            }
        }

        private void ComBoxExporData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComBoxExporData.SelectedIndex > 0)
            {
                string exportOption = ComBoxExporData.SelectedItem.ToString();
                switch (exportOption)
                {
                    case "Excel":
                        ExportToExcel();
                        break;
                    case "PDF":
                        ExportToPDF();
                        break;
                    case "CSV":
                        ExportToCSV();
                        break;
                }
                ComBoxExporData.SelectedIndex = 0;
            }
        }

        private void ExportToExcel()
        {
            MessageBox.Show("Export to Excel functionality - Coming soon!", "Export",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportToPDF()
        {
            MessageBox.Show("Export to PDF functionality - Coming soon!", "Export",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportToCSV()
        {
            MessageBox.Show("Export to CSV functionality - Coming soon!", "Export",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (datagridviewinventory.IsCurrentCellDirty)
            {
                datagridviewinventory.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion
    }
}