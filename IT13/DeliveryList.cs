// ---------------------------------------------------------------------
// DeliveryList.cs - FINAL (Add + Edit + View FULLY ENABLED)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace IT13
{
    public partial class DeliveryList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;
        public DeliveryList()
        {
            InitializeComponent();
            // Load icons (24x24)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();
            // DataGridView Settings
            dgvDeliveries.ReadOnly = true;
            dgvDeliveries.AllowUserToAddRows = false;
            dgvDeliveries.AllowUserToDeleteRows = false;
            dgvDeliveries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeliveries.MultiSelect = false;
            foreach (DataGridViewColumn col in dgvDeliveries.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvDeliveries.EnableHeadersVisualStyles = false;
            dgvDeliveries.ColumnHeadersDefaultCellStyle.SelectionBackColor =
            dgvDeliveries.ColumnHeadersDefaultCellStyle.BackColor;
            dgvDeliveries.DefaultCellStyle.SelectionBackColor =
            dgvDeliveries.DefaultCellStyle.BackColor;
            dgvDeliveries.DefaultCellStyle.SelectionForeColor =
            dgvDeliveries.DefaultCellStyle.ForeColor;
            dgvDeliveries.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvDeliveries.RowTemplate.Height = 45;
            dgvDeliveries.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvDeliveries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            var colID = dgvDeliveries.Columns["colDeliveryID"];
            colID.MinimumWidth = 160;
            colID.Width = 160;
            colID.FillWeight = 8;
            dgvDeliveries.Columns["colOrderID"].FillWeight = 20;
            dgvDeliveries.Columns["colCustomer"].FillWeight = 25;
            dgvDeliveries.Columns["colDeliveryDate"].FillWeight = 18;
            dgvDeliveries.Columns["colEmployee"].FillWeight = 18;
            dgvDeliveries.Columns["colVehicle"].FillWeight = 16;
            dgvDeliveries.Columns["colStatus"].FillWeight = 12;
            dgvDeliveries.Columns["colActions"].FillWeight = 14;
            dgvDeliveries.Columns["colCustomer"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDeliveries.Columns["colEmployee"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDeliveries.Columns["colDeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDeliveries.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            LoadSampleData();
            UpdateHeaderCheckState();
        }
        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Pending");
            Filter.Items.Add("In Transit");
            Filter.Items.Add("Delivered");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;
            Filter.SelectedIndexChanged += (sse, ee) =>
            Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export Data");
            Export.Items.Add("Excel");
            Export.Items.Add("PDF");
            Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Black;
            Export.DropDownWidth = 150;
            Export.SelectedIndexChanged += (sse, ee) =>
            Export.ForeColor = Export.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        #endregion
        #region Sample Data
        private void LoadSampleData()
        {
            AddRow("DEL-001", "ORD-1001", "ABC Corporation", "2025-11-18", "Juan Dela Cruz", "Toyota Hiace", "In Transit");
            AddRow("DEL-002", "ORD-1002", "XYZ Trading", "2025-11-19", "Maria Santos", "Mitsubishi L300", "Pending");
            AddRow("DEL-003", "ORD-1003", "Global Mart", "2025-11-20", "Pedro Reyes", "Isuzu Elf", "Delivered");
            AddRow("DEL-004", "ORD-1004", "Tech Depot", "2025-11-21", "Ana Lim", "Toyota Hiace", "In Transit");
            AddRow("DEL-005", "ORD-1005", "Prime Supplies", "2025-11-22", "Luis Tan", "Mitsubishi L300", "Pending");
        }
        private void AddRow(string id, string orderId, string customer, string date, string employee, string vehicle, string status)
        {
            int idx = dgvDeliveries.Rows.Add(false, orderId, customer, date, employee, vehicle, status, null);
            var row = dgvDeliveries.Rows[idx];
            row.Cells["colDeliveryID"].Tag = id;
            row.Height = 45;
        }
        #endregion
        #region Header Check State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                if (row.Visible)
                {
                    visibleCount++;
                    if ((bool)row.Cells["colDeliveryID"].Value) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
            checkedCount == 0 ? false :
            checkedCount == visibleCount ? true : (bool?)null;
            dgvDeliveries.InvalidateCell(dgvDeliveries.Columns["colDeliveryID"].Index, -1);
        }
        #endregion
        #region Cell Painting
        private void dgvDeliveries_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == dgvDeliveries.Columns["colDeliveryID"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);
                string headerText = "Delivery ID";
                var headerFont = dgvDeliveries.ColumnHeadersDefaultCellStyle.Font;
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
            if (e.ColumnIndex == dgvDeliveries.Columns["colDeliveryID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, isChecked ? ButtonState.Checked : ButtonState.Normal);
                string idText = dgvDeliveries.Rows[e.RowIndex].Cells["colDeliveryID"].Tag?.ToString() ?? "";
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
            if (e.ColumnIndex == dgvDeliveries.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                const int iconSize = 24;
                const int gap = 16;
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
        #region Cell Click
        private void dgvDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;
            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == dgvDeliveries.Columns["colDeliveryID"].Index)
            {
                var cellRect = dgvDeliveries.GetCellDisplayRectangle(e.ColumnIndex, -1, false);
                var mousePos = dgvDeliveries.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 8 + 16)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvDeliveries.Rows)
                        if (row.Visible) row.Cells["colDeliveryID"].Value = newState;
                    UpdateHeaderCheckState();
                    dgvDeliveries.InvalidateColumn(dgvDeliveries.Columns["colDeliveryID"].Index);
                }
                return;
            }
            if (e.RowIndex < 0) return;
            // Row checkbox
            if (e.ColumnIndex == dgvDeliveries.Columns["colDeliveryID"].Index)
            {
                var row = dgvDeliveries.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells["colDeliveryID"].Value ?? false);
                row.Cells["colDeliveryID"].Value = !cur;
                dgvDeliveries.InvalidateCell(dgvDeliveries.Columns["colDeliveryID"].Index, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }
            // Action icons
            if (e.ColumnIndex == dgvDeliveries.Columns["colActions"].Index)
            {
                var cellRect = dgvDeliveries.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvDeliveries.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                const int iconSize = 24;
                const int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string delId = dgvDeliveries.Rows[e.RowIndex].Cells["colDeliveryID"].Tag.ToString();
                if (clickX >= iconX && clickX < iconX + iconSize)
                {
                    OpenEditDelivery(delId); // UNCOMMENTED
                }
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                {
                    OpenViewDelivery(delId); // UNCOMMENTED
                }
            }
        }
        #endregion
        #region Navigation - FULLY ENABLED
        private void btnAddDelivery_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Delivery";
            parent.pnlContent.Controls.Clear();
            var form = new AddDelivery
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        private void OpenEditDelivery(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Delivery";
            parent.pnlContent.Controls.Clear();
            var form = new EditDelivery(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        private void OpenViewDelivery(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Delivery: {id}";
            parent.pnlContent.Controls.Clear();
            var form = new ViewDelivery(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        #endregion
        #region Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvDeliveries.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colDeliveryID"].Tag?.ToString().ToLower() ?? "";
                string orderId = row.Cells["colOrderID"].Value?.ToString().ToLower() ?? "";
                string customer = row.Cells["colCustomer"].Value?.ToString().ToLower() ?? "";
                string employee = row.Cells["colEmployee"].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) ||
                id.Contains(filter) ||
                orderId.Contains(filter) ||
                customer.Contains(filter) ||
                employee.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }
        #endregion
    }
}