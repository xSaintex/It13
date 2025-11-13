using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class SupplierOrderList : Form
    {
        private readonly Image _editIcon;
        private readonly Image _viewIcon;
        private readonly Image _deleteIcon;

        public SupplierOrderList()
        {
            InitializeComponent();

            // === BIGGER ICONS: 24x24 ===
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            _deleteIcon = new Bitmap(Properties.Resources.delete_icon, new Size(24, 24));

            // Setup dropdowns
            SetupFilterComboBox();
            SetupExportComboBox();

            // === READ-ONLY + NO INTERACTION ===
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;

            // Disable sorting on headers
            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Disable header visual feedback (no click, no hover)
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvOrders.ColumnHeadersDefaultCellStyle.BackColor;

            // Remove row selection highlight
            dgvOrders.DefaultCellStyle.SelectionBackColor =
                dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor =
                dgvOrders.DefaultCellStyle.ForeColor;

            // === BIGGER FONT + TALLER ROWS ===
            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // === AUTO-FILL COLUMNS ===
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.Columns["colID"].FillWeight = 18;
            dgvOrders.Columns["colDate"].FillWeight = 15;
            dgvOrders.Columns["colSupplier"].FillWeight = 30;
            dgvOrders.Columns["colTotal"].FillWeight = 15;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colActions"].FillWeight = 10;

            dgvOrders.Columns["colSupplier"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Pending");
            Filter.Items.Add("Delivered");
            Filter.Items.Add("Cancelled");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;  
            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
            };
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
            {
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
            };
        }

        private void LoadSampleData()
        {
            AddRow("SO-2025-001", "2025-11-10", "ABC Supplies Inc.", "₱12,500.50", "Pending");
            AddRow("SO-2025-002", "2025-11-09", "XYZ Trading", "₱8,900.00", "Delivered");
            AddRow("SO-2025-003", "2025-11-08", "Global Mart", "₱15,675.75", "Pending");
            AddRow("SO-2025-004", "2025-11-07", "Tech Depot", "₱22,300.00", "Cancelled");
            AddRow("SO-2025-005", "2025-11-06", "Prime Supplies", "₱9,450.00", "Delivered");
            AddRow("SO-2025-006", "2025-11-05", "Metro Traders", "₱18,200.00", "Pending");
            AddRow("SO-2025-007", "2025-11-04", "Alpha Corp", "₱11,800.00", "Delivered");
            AddRow("SO-2025-008", "2025-11-03", "Beta Inc.", "₱7,500.00", "Pending");
            AddRow("SO-2025-009", "2025-11-02", "Gamma Ltd.", "₱14,300.00", "Delivered");
            AddRow("SO-2025-010", "2025-11-01", "Delta Co.", "₱10,100.00", "Cancelled");
        }

        private void AddRow(string id, string date, string supplier, string total, string status)
        {
            int idx = dgvOrders.Rows.Add(false, date, supplier, total, status, null);
            var row = dgvOrders.Rows[idx];
            row.Cells[0].Tag = id;
            row.Height = 45;
        }

        // === CELL PAINTING: ID + CHECKBOX | 3 BIG ICONS + GAP ===
        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Skip header

            // === colID: Checkbox + ID Text ===
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

            // === colActions: Edit + View + Delete (24x24, 16px gap) ===
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);

                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 3) + (gap * 2); // 24+16+24+16+24 = 108
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Graphics.DrawImage(_deleteIcon, x + (iconSize + gap) * 2, y, iconSize, iconSize);

                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        // === CELL CLICK: Only checkbox + 3 icons work ===
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Checkbox
            if (e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                dgvOrders.InvalidateCell(0, e.RowIndex);
                return;
            }

            // Actions: Edit, View, Delete
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvOrders.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;

                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 3) + (gap * 2);
                int iconX = (cellRect.Width - totalWidth) / 2;

                string orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditOrder(orderId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + (iconSize + gap) * 2)
                    OpenViewOrder(orderId);
                else if (clickX >= iconX + (iconSize + gap) * 2 && clickX < iconX + totalWidth)
                    DeleteOrder(orderId);
            }
        }

        private void OpenEditOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Supplier Order";
            var editForm = new EditSupplierOrder(orderId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(editForm);
            editForm.Show();
        }

        private void OpenViewOrder(string orderId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Order: {orderId}";
            var viewForm = new ViewSupplierOrder(orderId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(viewForm);
            viewForm.Show();
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
            var addForm = new AddSupplierOrder
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                bool match = string.IsNullOrEmpty(filter) ||
                    row.Cells["colSupplier"].Value?.ToString().ToLower().Contains(filter) == true ||
                    row.Cells[0].Tag?.ToString().ToLower().Contains(filter) == true;
                row.Visible = match;
            }
        }
    }
}