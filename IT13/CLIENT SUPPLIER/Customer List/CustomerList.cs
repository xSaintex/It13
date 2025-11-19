// ---------------------------------------------------------------------
// CustomerList.cs - FINAL (MATCHES StockAdjustment + Designer)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // REQUIRED FOR GUNA2
namespace IT13
{
    public partial class CustomerList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public CustomerList()
        {
            InitializeComponent();
            // ----- BIGGER ICONS (24×24) -----
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));
            SetupFilterComboBox();
            SetupExportComboBox();
            // === DATAGRIDVIEW SETTINGS ===
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            foreach (DataGridViewColumn col in dgvCustomers.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor;
            dgvCustomers.DefaultCellStyle.SelectionBackColor =
                dgvCustomers.DefaultCellStyle.BackColor;
            dgvCustomers.DefaultCellStyle.SelectionForeColor =
                dgvCustomers.DefaultCellStyle.ForeColor;
            dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvCustomers.RowTemplate.Height = 45;
            dgvCustomers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // === AUTO-FILL ALL COLUMNS EXCEPT ID ===
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // === INCREASE ID COLUMN WIDTH (Fixed + Minimum) ===
            var colID = dgvCustomers.Columns["colID"];
            colID.MinimumWidth = 160; // Prevents shrinking
            colID.Width = 160; // Forces initial width
            colID.FillWeight = 8; // Lower weight so others take more space
            // === REST OF COLUMNS (Responsive) ===
            dgvCustomers.Columns["colCompany"].FillWeight = 28;
            dgvCustomers.Columns["colContact"].FillWeight = 18;
            dgvCustomers.Columns["colPhone"].FillWeight = 16;
            dgvCustomers.Columns["colEmail"].FillWeight = 22;
            dgvCustomers.Columns["colPayment"].FillWeight = 12;
            dgvCustomers.Columns["colStatus"].FillWeight = 12;
            dgvCustomers.Columns["colActions"].FillWeight = 14;
            // Wrap long text
            dgvCustomers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // center specific columns' cell text
            dgvCustomers.Columns["colContact"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colPayment"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colStatus"].DefaultCellStyle.Alignment =
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
        #region Sample Data
        private void LoadSampleData()
        {
            AddRow("CUST-001", "ABC Corporation", "John Doe", "+63 905 123 4567", "john@abc.com", "Net 30", "Active");
            AddRow("CUST-002", "XYZ Trading", "Jane Smith", "+63 917 987 6543", "jane@xyz.com", "Cash", "Inactive");
            AddRow("CUST-003", "Global Mart", "Mike Tan", "+63 923 456 7890", "mike@global.com", "Net 60", "Active");
            AddRow("CUST-004", "Tech Depot", "Ana Cruz", "+63 912 345 6789", "ana@tech.com", "Net 15", "Active");
            AddRow("CUST-005", "Prime Supplies", "Luis Reyes", "+63 998 765 4321", "luis@prime.com", "Cash", "Inactive");
        }
        private void AddRow(string id, string company, string contact, string phone,
                           string email, string payment, string status)
        {
            int idx = dgvCustomers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            var row = dgvCustomers.Rows[idx];
            row.Cells["colID"].Tag = id; // Store real ID in Tag
            row.Height = 45;
        }
        #endregion
        #region Header Check State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.Visible)
                {
                    visibleCount++;
                    if ((bool)row.Cells["colID"].Value) checkedCount++;
                }
            }
            if (visibleCount == 0)
            {
                _headerCheckState = false;
            }
            else
            {
                _headerCheckState = (checkedCount == 0) ? false : (checkedCount == visibleCount) ? true : (bool?)null;
            }
            dgvCustomers.InvalidateCell(dgvCustomers.Columns["colID"].Index, -1);
        }
        #endregion
        #region Cell Painting (Header Checkbox + Checkbox + ID + Icons)
        private void dgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // ----- HEADER CHECKBOX + TEXT (colID) -----
            if (e.RowIndex == -1 && e.ColumnIndex == dgvCustomers.Columns["colID"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                {
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                }
                else if (_headerCheckState == null)
                {
                    // Indeterminate state: filled square
                    e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(checkRect.X + 2, checkRect.Y + 2, 12, 12));
                }
                // Draw header text "ID" shifted right
                string headerText = "ID";
                var headerFont = dgvCustomers.ColumnHeadersDefaultCellStyle.Font;
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
            // ----- ROW CHECKBOX + ID (colID) -----
            if (e.ColumnIndex == dgvCustomers.Columns["colID"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);
                string idText = dgvCustomers.Rows[e.RowIndex].Cells["colID"].Tag?.ToString() ?? "";
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
            // ----- EDIT + VIEW ICONS (colActions) -----
            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index)
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
        #region Cell Click (Header Toggle + Checkbox toggle + Icon actions)
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;
            // ----- HEADER CHECKBOX TOGGLE -----
            if (e.RowIndex == -1 && e.ColumnIndex == dgvCustomers.Columns["colID"].Index)
            {
                var cellRect = dgvCustomers.GetCellDisplayRectangle(e.ColumnIndex, -1, false);
                var mousePos = dgvCustomers.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 8 + 16) // Clicked on checkbox area
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true); // Toggle: true/null -> false; false -> true
                    foreach (DataGridViewRow row in dgvCustomers.Rows)
                    {
                        if (row.Visible)
                        {
                            row.Cells["colID"].Value = newState;
                        }
                    }
                    UpdateHeaderCheckState();
                    dgvCustomers.InvalidateColumn(dgvCustomers.Columns["colID"].Index);
                }
                return;
            }
            if (e.RowIndex < 0) return;
            // ----- ROW CHECKBOX TOGGLE -----
            if (e.ColumnIndex == dgvCustomers.Columns["colID"].Index)
            {
                var row = dgvCustomers.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells["colID"].Value ?? false);
                row.Cells["colID"].Value = !cur;
                dgvCustomers.InvalidateCell(dgvCustomers.Columns["colID"].Index, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }
            // ----- ACTION ICONS -----
            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index)
            {
                var cellRect = dgvCustomers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvCustomers.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                const int iconSize = 24;
                const int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string custId = dgvCustomers.Rows[e.RowIndex].Cells["colID"].Tag.ToString();
                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditCustomer(custId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewCustomer(custId);
            }
        }
        #endregion
        #region Navigation (Edit / View / Add)
        private void OpenEditCustomer(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Customer";
            var form = new EditCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        private void OpenViewCustomer(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Customer: {id}";
            var form = new ViewCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Customer";
            var form = new AddCustomerList
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
            foreach (DataGridViewRow row in dgvCustomers.Rows)
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