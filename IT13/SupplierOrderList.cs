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
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(16, 16));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(16, 16));
            _deleteIcon = new Bitmap(Properties.Resources.delete_icon, new Size(16, 16));
            SetupFilterComboBox();
            SetupExportComboBox();
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
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
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
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) =>
            {
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
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
            row.Cells[0].Tag = id; // Store real ID
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Paint Checkbox + ID
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = Convert.ToBoolean(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 8, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                var textSize = e.Graphics.MeasureString(idText, e.CellStyle.Font);
                var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);
                e.Graphics.DrawString(idText, e.CellStyle.Font, Brushes.Black, textRect);
                e.Handled = true;
            }

            // Paint Edit + View + Delete Icons
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 60) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 16) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 16, 16);
                e.Graphics.DrawImage(_viewIcon, x + 20, y, 16, 16);
                e.Graphics.DrawImage(_deleteIcon, x + 40, y, 16, 16);
                e.Handled = true;
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Handle Checkbox
            if (e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                dgvOrders.InvalidateCell(0, e.RowIndex);
                return;
            }

            // Handle Actions
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvOrders.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconX = (cellRect.Width - 60) / 2;
                string orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + 16)
                {
                    OpenEditOrder(orderId);
                }
                else if (clickX >= iconX + 20 && clickX < iconX + 36)
                {
                    OpenViewOrder(orderId);
                }
                else if (clickX >= iconX + 40 && clickX < iconX + 56)
                {
                    DeleteOrder(orderId);
                }
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
                // Remove from DataGridView
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
            string filter = txtSearch.Text.Trim();
            var view = (dgvOrders.DataSource as DataTable)?.DefaultView;
            if (view != null)
            {
                if (string.IsNullOrEmpty(filter))
                    view.RowFilter = "";
                else
                    view.RowFilter = $"[Supplier] LIKE '%{filter}%' OR [Order ID] LIKE '%{filter}%'";
            }
        }
    }
}