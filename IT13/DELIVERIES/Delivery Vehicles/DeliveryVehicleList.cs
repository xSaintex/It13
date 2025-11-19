// ---------------------------------------------------------------------
// DeliveryVehicleList.cs - Same as CustomerList but NO Export + Edit only
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class DeliveryVehicleList : Form
    {
        private readonly Image _editIcon;
        private bool? _headerCheckState = false;

        public DeliveryVehicleList()
        {
            InitializeComponent();

            // Icons (24x24)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));

            SetupFilterComboBox();

            // === DATAGRIDVIEW SETTINGS ===
            dgvVehicles.ReadOnly = true;
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVehicles.MultiSelect = false;

            foreach (DataGridViewColumn col in dgvVehicles.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvVehicles.EnableHeadersVisualStyles = false;
            dgvVehicles.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvVehicles.ColumnHeadersDefaultCellStyle.BackColor;
            dgvVehicles.DefaultCellStyle.SelectionBackColor =
                dgvVehicles.DefaultCellStyle.BackColor;
            dgvVehicles.DefaultCellStyle.SelectionForeColor =
                dgvVehicles.DefaultCellStyle.ForeColor;
            dgvVehicles.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvVehicles.RowTemplate.Height = 45;
            dgvVehicles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Responsive columns
            dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ID column (checkbox + text)
            var colID = dgvVehicles.Columns["colID"];
            colID.MinimumWidth = 160;
            colID.Width = 160;
            colID.FillWeight = 10;

            // Fill weights
            dgvVehicles.Columns["colVehicleName"].FillWeight = 28;
            dgvVehicles.Columns["colPlateNumber"].FillWeight = 18;
            dgvVehicles.Columns["colStatus"].FillWeight = 12;
            dgvVehicles.Columns["colCreatedAt"].FillWeight = 15;
            dgvVehicles.Columns["colUpdatedAt"].FillWeight = 15;
            dgvVehicles.Columns["colActions"].FillWeight = 12;

            // Center alignment
            dgvVehicles.Columns["colPlateNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colCreatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colUpdatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        #endregion

        #region Sample Data
        private void LoadSampleData()
        {
            AddRow("VH-001", "Toyota Hiace", "NCR 1234", "Active", "2024-01-15 10:30", "2025-03-22 14:20");
            AddRow("VH-002", "Isuzu Elf", "NCR 5678", "Active", "2024-02-20 09:15", "2025-02-28 11:45");
            AddRow("VH-003", "Mitsubishi L300", "NCR 9012", "Inactive", "2024-03-10 13:00", "2024-12-01 08:30");
            AddRow("VH-004", "Fuso Canter", "NCR 3456", "Active", "2024-05-18 11:00", "2025-01-10 09:15");
            AddRow("VH-005", "Hyundai Porter", "NCR 7890", "Maintenance", "2024-07-22 14:30", "2025-03-01 16:45");
        }

        private void AddRow(string id, string name, string plate, string status, string created, string updated)
        {
            int idx = dgvVehicles.Rows.Add(false, name, plate, status, created, updated, null);
            var row = dgvVehicles.Rows[idx];
            row.Cells["colID"].Tag = id;
            row.Height = 45;
        }
        #endregion

        #region Header Checkbox Logic
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)row.Cells["colID"].Value) checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? true : (bool?)null;

            dgvVehicles.InvalidateCell(dgvVehicles.Columns["colID"].Index, -1);
        }
        #endregion

        #region Cell Painting (Header Checkbox + Row Checkbox + Edit Icon)
        private void dgvVehicles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header Checkbox + "ID" Text
            if (e.RowIndex == -1 && e.ColumnIndex == dgvVehicles.Columns["colID"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);

                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                // Draw "ID" text
                string headerText = "ID";
                var font = new Font("Tahoma", 10.2F, FontStyle.Bold);
                var size = e.Graphics.MeasureString(headerText, font);
                var textRect = new Rectangle(e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)size.Height) / 2,
                    e.CellBounds.Width - 35, e.CellBounds.Height);
                e.Graphics.DrawString(headerText, font, Brushes.White, textRect);
                e.Handled = true;
                return;
            }

            if (e.RowIndex < 0) return;

            // Row Checkbox + ID Text
            if (e.ColumnIndex == dgvVehicles.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = dgvVehicles.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    var textRect = new Rectangle(e.CellBounds.X + 30,
                        e.CellBounds.Y + (e.CellBounds.Height - 20) / 2,
                        e.CellBounds.Width - 35, e.CellBounds.Height);
                    e.Graphics.DrawString(idText, new Font("Segoe UI", 11F), Brushes.Black, textRect);
                }
                e.Handled = true;
                return;
            }

            // Actions: Only Edit Icon (centered)
            if (e.ColumnIndex == dgvVehicles.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 24) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 24, 24);
                e.Handled = true;
            }
        }
        #endregion

        #region Cell Click (Toggle Checkboxes + Edit Action)
        private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;

            // Header Checkbox Click
            if (e.RowIndex == -1 && e.ColumnIndex == dgvVehicles.Columns["colID"].Index)
            {
                var rect = dgvVehicles.GetCellDisplayRectangle(e.ColumnIndex, -1, false);
                var mouseX = dgvVehicles.PointToClient(MousePosition).X - rect.X;
                if (mouseX >= 8 && mouseX <= 24)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvVehicles.Rows)
                        if (row.Visible && !row.IsNewRow)
                            row.Cells["colID"].Value = newState;

                    UpdateHeaderCheckState();
                    dgvVehicles.InvalidateColumn(e.ColumnIndex);
                }
                return;
            }

            if (e.RowIndex < 0) return;

            // Row Checkbox Toggle
            if (e.ColumnIndex == dgvVehicles.Columns["colID"].Index)
            {
                var row = dgvVehicles.Rows[e.RowIndex];
                var rect = dgvVehicles.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mouseX = dgvVehicles.PointToClient(MousePosition).X - rect.X;
                if (mouseX >= 8 && mouseX <= 24)
                {
                    bool current = (bool)(row.Cells["colID"].Value ?? false);
                    row.Cells["colID"].Value = !current;
                    UpdateHeaderCheckState();
                    dgvVehicles.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
                return;
            }

            // Edit Icon Click
            if (e.ColumnIndex == dgvVehicles.Columns["colActions"].Index)
            {
                string vehicleId = dgvVehicles.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(vehicleId))
                    OpenEditVehicle(vehicleId);
            }
        }
        #endregion

        #region Navigation
        private void OpenEditVehicle(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Delivery Vehicle";
            var form = new EditVehicleList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Delivery Vehicle";
            var form = new AddVehicleList
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
            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colID"].Tag?.ToString().ToLower() ?? "";
                string name = row.Cells["colVehicleName"].Value?.ToString().ToLower() ?? "";
                string plate = row.Cells["colPlateNumber"].Value?.ToString().ToLower() ?? "";

                bool match = string.IsNullOrEmpty(filter) ||
                             id.Contains(filter) || name.Contains(filter) || plate.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }
        #endregion
    }
}