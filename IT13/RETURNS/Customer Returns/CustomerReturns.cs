using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class CustomerReturns : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;

        public CustomerReturns()
        {
            InitializeComponent();

            // Load icons (safe — won't crash even if missing)
            _editIcon = new Bitmap(Properties.Resources.edit_icon ?? CreatePlaceholder(Color.FromArgb(255, 145, 0)), new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon ?? CreatePlaceholder(Color.FromArgb(0, 123, 255)), new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // EXACT SAME SETTINGS AS SUPPLIERLIST
            dgvReturns.ReadOnly = true;
            dgvReturns.AllowUserToAddRows = false;
            dgvReturns.AllowUserToDeleteRows = false;
            dgvReturns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReturns.MultiSelect = false;

            foreach (DataGridViewColumn col in dgvReturns.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvReturns.EnableHeadersVisualStyles = false;
            dgvReturns.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvReturns.ColumnHeadersDefaultCellStyle.BackColor;

            // THIS REMOVES ROW HIGHLIGHT COMPLETELY
            dgvReturns.DefaultCellStyle.SelectionBackColor = dgvReturns.DefaultCellStyle.BackColor;
            dgvReturns.DefaultCellStyle.SelectionForeColor = dgvReturns.DefaultCellStyle.ForeColor;

            dgvReturns.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvReturns.RowTemplate.Height = 45;
            dgvReturns.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvReturns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var colCheck = dgvReturns.Columns["colCheck"];
            colCheck.MinimumWidth = 160;
            colCheck.Width = 160;
            colCheck.FillWeight = 8;

            dgvReturns.Columns["colCustomerID"].FillWeight = 16;
            dgvReturns.Columns["colCustomerName"].FillWeight = 28; dgvReturns.Columns["colCustomerName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvReturns.Columns["colQty"].FillWeight = 10;
            dgvReturns.Columns["colTotal"].FillWeight = 14;
            dgvReturns.Columns["colStatus"].FillWeight = 12;
            dgvReturns.Columns["colReturnDate"].FillWeight = 14;
            dgvReturns.Columns["colActions"].FillWeight = 14;

            dgvReturns.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReturns.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReturns.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReturns.Columns["colReturnDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            LoadSampleData();
            UpdateHeaderCheckState();
        }

        private Image CreatePlaceholder(Color c)
        {
            var b = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(b)) g.Clear(Color.Transparent);
            return b;
        }

        #region ComboBox Setup
        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter"); Filter.Items.Add("All"); Filter.Items.Add("Pending"); Filter.Items.Add("Approved"); Filter.Items.Add("Rejected");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;
            Filter.SelectedIndexChanged += (s, e) => Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export"); Export.Items.Add("Excel"); Export.Items.Add("PDF"); Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Black;
            Export.SelectedIndexChanged += (s, e) => Export.ForeColor = Export.SelectedIndex == 0 ? Color.Black : Color.FromArgb(68, 88, 112);
        }
        #endregion

        #region Sample Data
        private void LoadSampleData()
        {
            AddRow("RET-2025-001", "CUS-001", "John Doe", 5, "₱25,000.00", "Pending", "2025-11-18");
            AddRow("RET-2025-002", "CUS-003", "Jane Smith", 2, "₱18,000.00", "Approved", "2025-11-15");
            AddRow("RET-2025-003", "CUS-005", "Mike Tan", 1, "₱8,000.00", "Rejected", "2025-11-10");
            AddRow("RET-2025-004", "CUS-007", "Sarah Lee", 3, "₱32,000.00", "Pending", "2025-11-17");
        }
        private void AddRow(string retId, string cusId, string name, int qty, string total, string status, string date)
        {
            int idx = dgvReturns.Rows.Add(false, cusId, name, qty, total, status, date, null);
            dgvReturns.Rows[idx].Cells["colCheck"].Tag = retId;
            dgvReturns.Rows[idx].Height = 45;
        }
        #endregion

        #region Header Check State
        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvReturns.Rows)
            {
                if (row.Visible)
                {
                    visibleCount++;
                    if ((bool)row.Cells["colCheck"].Value) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                (checkedCount == 0) ? false : (checkedCount == visibleCount) ? true : (bool?)null;
            dgvReturns.InvalidateCell(dgvReturns.Columns["colCheck"].Index, -1);
        }
        #endregion

        #region Cell Painting — 100% IDENTICAL TO SUPPLIERLIST
        private void dgvReturns_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == dgvReturns.Columns["colCheck"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);
                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                string headerText = "Return ID";  // ← ONLY DIFFERENCE
                var headerFont = dgvReturns.ColumnHeadersDefaultCellStyle.Font;
                var textSize = e.Graphics.MeasureString(headerText, headerFont);
                var textRect = new Rectangle(e.CellBounds.X + 30, e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35, e.CellBounds.Height);
                e.Graphics.DrawString(headerText, headerFont, Brushes.White, textRect);
                e.Handled = true;
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReturns.Columns["colCheck"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect, isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = dgvReturns.Rows[e.RowIndex].Cells["colCheck"].Tag?.ToString() ?? "";
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

            if (e.ColumnIndex == dgvReturns.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                const int iconSize = 24, gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, iconSize, iconSize);
                e.Graphics.DrawImage(_viewIcon, x + iconSize + gap, y, iconSize, iconSize);
                e.Handled = true;
            }
        }
        #endregion

        #region Cell Click — 100% IDENTICAL
        private void dgvReturns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < -1) return;

            if (e.RowIndex == -1 && e.ColumnIndex == dgvReturns.Columns["colCheck"].Index)
            {
                var cellRect = dgvReturns.GetCellDisplayRectangle(e.ColumnIndex, -1, false);
                var mousePos = dgvReturns.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                if (clickX >= 8 && clickX <= 24)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvReturns.Rows)
                        if (row.Visible) row.Cells["colCheck"].Value = newState;
                    UpdateHeaderCheckState();
                    dgvReturns.InvalidateColumn(dgvReturns.Columns["colCheck"].Index);
                }
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReturns.Columns["colCheck"].Index)
            {
                var row = dgvReturns.Rows[e.RowIndex];
                bool cur = (bool)(row.Cells["colCheck"].Value ?? false);
                row.Cells["colCheck"].Value = !cur;
                dgvReturns.InvalidateCell(dgvReturns.Columns["colCheck"].Index, e.RowIndex);
                UpdateHeaderCheckState();
                return;
            }

            if (e.ColumnIndex == dgvReturns.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                var cellRect = dgvReturns.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvReturns.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                const int iconSize = 24, gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;
                string retId = dgvReturns.Rows[e.RowIndex].Cells["colCheck"].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditReturn(retId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewReturn(retId);
            }
        }
        #endregion

        #region Navigation & Search
        private void btnAddReturn_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Add Customer Return";

            var addForm = new AddCustomerReturns
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // THIS LINE IS THE KEY — tells Add form how to return to THIS exact page
            addForm.Tag = this;

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvReturns.Rows)
            {
                if (row.IsNewRow) continue;
                string id = row.Cells["colCheck"].Tag?.ToString().ToLower() ?? "";
                string cusId = row.Cells["colCustomerID"].Value?.ToString().ToLower() ?? "";
                string name = row.Cells["colCustomerName"].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) || id.Contains(filter) || cusId.Contains(filter) || name.Contains(filter);
                row.Visible = match;
            }
            UpdateHeaderCheckState();
        }

        private void OpenEditReturn(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Customer Return";
            var form = new EditCustomerReturns(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewReturn(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Return: {id}";
            var form = new ViewCustomerReturns(id) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }
        #endregion
    }
}