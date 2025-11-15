// ---------------------------------------------------------------------
// SupplierList.cs - 100% COPY OF CustomerList.cs (Only renamed + new data)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class SupplierList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public SupplierList()
        {
            InitializeComponent();

            // Load icons (make sure you have edit_icon.png & view_icon.png in Resources)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // DataGridView Settings
            dgvSuppliers.ReadOnly = true;
            dgvSuppliers.AllowUserToAddRows = false;
            dgvSuppliers.AllowUserToDeleteRows = false;
            dgvSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSuppliers.MultiSelect = false;

            foreach (DataGridViewColumn col in dgvSuppliers.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvSuppliers.EnableHeadersVisualStyles = false;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvSuppliers.ColumnHeadersDefaultCellStyle.BackColor;
            dgvSuppliers.DefaultCellStyle.SelectionBackColor =
                dgvSuppliers.DefaultCellStyle.BackColor;
            dgvSuppliers.DefaultCellStyle.SelectionForeColor =
                dgvSuppliers.DefaultCellStyle.ForeColor;
            dgvSuppliers.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvSuppliers.RowTemplate.Height = 45;
            dgvSuppliers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var colID = dgvSuppliers.Columns["colID"];
            colID.MinimumWidth = 160;
            colID.Width = 160;
            colID.FillWeight = 8;

            dgvSuppliers.Columns["colCompany"].FillWeight = 28;
            dgvSuppliers.Columns["colContact"].FillWeight = 18;
            dgvSuppliers.Columns["colPhone"].FillWeight = 16;
            dgvSuppliers.Columns["colEmail"].FillWeight = 22;
            dgvSuppliers.Columns["colPayment"].FillWeight = 12;
            dgvSuppliers.Columns["colStatus"].FillWeight = 12;
            dgvSuppliers.Columns["colActions"].FillWeight = 14;

            dgvSuppliers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvSuppliers.Columns["colContact"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colEmail"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colPayment"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.Columns["colStatus"].DefaultCellStyle.Alignment =
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
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
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
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        #endregion

        #region Sample Data (DIFFERENT FROM CUSTOMERS)
        private void LoadSampleData()
        {
            AddRow("SUP-001", "PrimeTech Distributors", "Mark Lim", "+63 917 555 1234", "mark@primetech.com", "Net 30", "Active");
            AddRow("SUP-002", "Global Supplies Inc.", "Sarah Go", "+63 922 888 9999", "sarah@global.com", "Net 45", "Active");
            AddRow("SUP-003", "Metro Trading Co.", "John Tan", "+63 905 333 4444", "john@metro.com", "Cash", "Inactive");
            AddRow("SUP-004", "Asia Pacific Traders", "Lisa Wong", "+63 918 777 8888", "lisa@apt.com.ph", "Net 60", "Active");
            AddRow("SUP-005", "Sunrise Enterprises", "David Cruz", "+63 927 111 2222", "david@sunrise.ph", "Net 30", "Active");
            AddRow("SUP-006", "Evergreen Imports", "Anna Reyes", "+63 939 222 3333", "anna@evergreen.ph", "Cash", "Active");
            AddRow("SUP-007", "Nexus Distribution", "Paul Santos", "+63 956 777 8888", "paul@nexus.com", "Net 15", "Inactive");
        }

        private void AddRow(string id, string company, string contact, string phone,
                           string email, string payment, string status)
        {
            int idx = dgvSuppliers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            var row = dgvSuppliers.Rows[idx];
            row.Cells["colID"].Tag = id;
            row.Height = 45;
        }
        #endregion

        #region Header Check State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (row.Visible)
                {
                    visibleCount++;
                    if ((bool)row.Cells["colID"].Value) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                (checkedCount == 0) ? false : (checkedCount == visibleCount) ? true : (bool?)null;

            dgvSuppliers.InvalidateCell(dgvSuppliers.Columns["colID"].Index, -1);
        }
        #endregion

        #region Cell Painting (Same as CustomerList)
        private void dgvSuppliers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                string headerText = "ID";
                var headerFont = dgvSuppliers.ColumnHeadersDefaultCellStyle.Font;
                var textSize = e.Graphics.MeasureString(headerText, headerFont);
                var textRect = new Rectangle(e.CellBounds.X + 30, e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35, e.CellBounds.Height);
                e.Graphics.DrawString(headerText, headerFont, Brushes.White, textRect);
                e.Handled = true;
                return;
            }

            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = dgvSuppliers.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    var textSize = e.Graphics.MeasureString(idText, new Font("Segoe UI", 11F));
                    var textRect = new Rectangle(e.CellBounds.X + 30, e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                        e.CellBounds.Width - 35, e.CellBounds.Height);
                    e.Graphics.DrawString(idText, new Font("Segoe UI", 11F), Brushes.Black, textRect);
                }
                e.Handled = true;
                return;
            }

            if (e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index)
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
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;

            if (e.RowIndex == -1 && e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                var cellRect = dgvSuppliers.GetCellDisplayRectangle(e.ColumnIndex, -1, false);
                var mousePos = dgvSuppliers.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 24)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvSuppliers.Rows)
                        if (row.Visible) row.Cells["colID"].Value = newState;
                    UpdateHeaderCheckState();
                    dgvSuppliers.InvalidateColumn(dgvSuppliers.Columns["colID"].Index);
                }
                return;
            }

            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvSuppliers.Columns["colID"].Index)
            {
                var row = dgvSuppliers.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells["colID"].Value ?? false);
                row.Cells["colID"].Value = !cur;
                dgvSuppliers.InvalidateCell(dgvSuppliers.Columns["colID"].Index, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }

            if (e.ColumnIndex == dgvSuppliers.Columns["colActions"].Index)
            {
                var cellRect = dgvSuppliers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvSuppliers.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                const int iconSize = 24;
                const int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string supId = dgvSuppliers.Rows[e.RowIndex].Cells["colID"].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditSupplier(supId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewSupplier(supId);
            }
        }
        #endregion

        #region Navigation
        private void OpenEditSupplier(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Supplier";
            var form = new EditSupplierList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewSupplier(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Supplier: {id}";
            var form = new ViewSupplierList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Supplier";
            var form = new AddSupplierList
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
            foreach (DataGridViewRow row in dgvSuppliers.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colID"].Tag?.ToString().ToLower() ?? "";
                string company = row.Cells["colCompany"].Value?.ToString().ToLower() ?? "";
                string phone = row.Cells["colPhone"].Value?.ToString().ToLower() ?? "";
                string email = row.Cells["colEmail"].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) ||
                             id.Contains(filter) ||
                             company.Contains(filter) ||
                             phone.Contains(filter) ||
                             email.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }
        #endregion
    }
}