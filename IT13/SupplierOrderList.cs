// ---------------------------------------------------------------------
// SupplierOrderList.cs - FINAL (Header Checkbox Clickable + Left-Aligned)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class SupplierOrderList : Form
    {
        private readonly Image _editIcon, _viewIcon, _deleteIcon;
        private bool? _headerCheckState = false;

        public SupplierOrderList()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            _deleteIcon = new Bitmap(Properties.Resources.delete_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // Grid Settings
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;

            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvOrders.ColumnHeadersDefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionBackColor =
                dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor =
                dgvOrders.DefaultCellStyle.ForeColor;

            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvOrders.Columns["colID"].MinimumWidth = 160;
            dgvOrders.Columns["colID"].Width = 160;
            dgvOrders.Columns["colID"].FillWeight = 10;
            dgvOrders.Columns["colDate"].FillWeight = 15;
            dgvOrders.Columns["colSupplier"].FillWeight = 32;
            dgvOrders.Columns["colTotal"].FillWeight = 16;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colActions"].FillWeight = 15;

            dgvOrders.Columns["colSupplier"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
            UpdateHeaderCheckState();
        }

        #region ComboBoxes & Data
        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter"); Filter.Items.Add("All"); Filter.Items.Add("Pending");
            Filter.Items.Add("Delivered"); Filter.Items.Add("Cancelled");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;
            Filter.SelectedIndexChanged += (s, e) =>
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }

        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export"); Export.Items.Add("Excel"); Export.Items.Add("PDF"); Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Black;
            Export.SelectedIndexChanged += (s, e) =>
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }

        private void LoadSampleData()
        {
            AddRow("SO-2025-001", "2025-11-10", "ABC Supplies Inc.", "₱12,500.50", "Pending");
            AddRow("SO-2025-002", "2025-11-09", "XYZ Trading", "₱8,900.00", "Delivered");
            AddRow("SO-2025-003", "2025-11-08", "Global Mart", "₱15,675.75", "Pending");
            AddRow("SO-2025-004", "2025-11-07", "Tech Depot", "₱22,300.00", "Cancelled");
            AddRow("SO-2025-005", "2025-11-06", "Prime Supplies", "₱9,450.00", "Delivered");
        }

        private void AddRow(string id, string date, string supplier, string total, string status)
        {
            int idx = dgvOrders.Rows.Add(false, date, supplier, total, status, null);
            dgvOrders.Rows[idx].Cells[0].Tag = id;
            dgvOrders.Rows[idx].Height = 45;
        }
        #endregion

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvOrders.Rows)
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

            dgvOrders.InvalidateCell(0, -1);
        }

        #region Cell Painting — Header Checkbox LEFT-ALIGNED + Icons
        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // === HEADER CHECKBOX (Left-aligned, same as rows) ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);

                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);

                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                // Draw "ID" text next to checkbox
                string headerText = "ID";
                var headerFont = dgvOrders.ColumnHeadersDefaultCellStyle.Font;
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

            // === ROW CHECKBOX + ID ===
            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
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

            // === ACTIONS: Edit | View | Delete ===
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 3) + (gap * 2);
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Graphics.DrawImage(_deleteIcon, x + (iconSize + gap) * 2, y, iconSize, iconSize);

                e.Handled = true;
            }
        }
        #endregion

        #region Cell Click — Header Checkbox + Row Checkbox + Icons ALL WORKING
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1 || e.ColumnIndex < 0) return;

            // === HEADER CHECKBOX (now clickable!) ===
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(0, -1, false);
                var mousePos = dgvOrders.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;

                if (clickX >= 8 && clickX <= 24) // Checkbox hit area
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvOrders.Rows)
                        if (row.Visible && !row.IsNewRow)
                            row.Cells[0].Value = newState;

                    UpdateHeaderCheckState();
                }
                return;
            }

            if (e.RowIndex < 0) return;

            // === ROW CHECKBOX ===
            if (e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !cur;
                dgvOrders.InvalidateCell(0, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }

            // === ACTION ICONS (Edit | View | Delete) ===
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvOrders.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;

                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 3) + (gap * 2);
                int startX = (cellRect.Width - totalWidth) / 2;

                string orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= startX && clickX < startX + iconSize)
                    OpenEditOrder(orderId);
                else if (clickX >= startX + iconSize + gap && clickX < startX + (iconSize + gap) * 2)
                    OpenViewOrder(orderId);
                else if (clickX >= startX + (iconSize + gap) * 2 && clickX < startX + totalWidth)
                    DeleteOrder(orderId);
            }
        }
        #endregion

        #region Navigation & Actions
        private void OpenEditOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Supplier Order";
            var form = new EditSupplierOrder(orderId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Order: {orderId}";
            var form = new ViewSupplierOrder(orderId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void DeleteOrder(string orderId)
        {
            if (MessageBox.Show($"Delete order {orderId}?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < dgvOrders.Rows.Count; i++)
                {
                    if (dgvOrders.Rows[i].Cells[0].Tag?.ToString() == orderId)
                    {
                        dgvOrders.Rows.RemoveAt(i);
                        UpdateHeaderCheckState();
                        break;
                    }
                }
                MessageBox.Show("Order deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Supplier Order";
            var form = new AddSupplierOrder
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        #endregion

        #region Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells[0].Tag?.ToString().ToLower() ?? "";
                string supplier = row.Cells["colSupplier"].Value?.ToString().ToLower() ?? "";
                row.Visible = string.IsNullOrEmpty(filter) || id.Contains(filter) || supplier.Contains(filter);
            }
            UpdateHeaderCheckState();
        }
        #endregion
    }
}