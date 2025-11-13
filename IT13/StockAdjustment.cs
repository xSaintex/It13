using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // REQUIRED FOR GUNA2

namespace IT13
{
    public partial class StockAdjustment : Form
    {
        private readonly Image _editIcon;
        private readonly Image _viewIcon;

        public StockAdjustment()
        {
            InitializeComponent();

            // BIGGER ICONS
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // === GUNA2DATAGRIDVIEW SETTINGS ===
            datagridviewadjustment.ReadOnly = true;
            datagridviewadjustment.AllowUserToAddRows = false;
            datagridviewadjustment.AllowUserToDeleteRows = false;
            datagridviewadjustment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewadjustment.MultiSelect = false;

            foreach (DataGridViewColumn col in datagridviewadjustment.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            datagridviewadjustment.EnableHeadersVisualStyles = false;
            datagridviewadjustment.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridviewadjustment.ColumnHeadersDefaultCellStyle.BackColor;

            datagridviewadjustment.DefaultCellStyle.SelectionBackColor =
                datagridviewadjustment.DefaultCellStyle.BackColor;
            datagridviewadjustment.DefaultCellStyle.SelectionForeColor =
                datagridviewadjustment.DefaultCellStyle.ForeColor;

            // BIGGER FONT + TALL ROWS
            datagridviewadjustment.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridviewadjustment.RowTemplate.Height = 45;
            datagridviewadjustment.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // AUTO-FILL COLUMNS
            datagridviewadjustment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridviewadjustment.Columns["colID"].FillWeight = 15;
            datagridviewadjustment.Columns["colDate"].FillWeight = 12;
            datagridviewadjustment.Columns["colProductName"].FillWeight = 28;
            datagridviewadjustment.Columns["colAdjustmentType"].FillWeight = 12;
            datagridviewadjustment.Columns["colPhysicalCount"].FillWeight = 10;
            datagridviewadjustment.Columns["colRequestedBy"].FillWeight = 18;
            datagridviewadjustment.Columns["colStatus"].FillWeight = 12;
            datagridviewadjustment.Columns["colActions"].FillWeight = 13;

            datagridviewadjustment.Columns["colProductName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Pending");
            Filter.Items.Add("Approved");
            Filter.Items.Add("Rejected");
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

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridviewadjustment.Rows)
            {
                bool match = string.IsNullOrEmpty(filter) ||
                    row.Cells["colProductName"].Value?.ToString().ToLower().Contains(filter) == true ||
                    row.Cells["colID"].Tag?.ToString().ToLower().Contains(filter) == true;
                row.Visible = match;
            }
        }

        private void datagridviewadjustment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = datagridviewadjustment.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
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

            if (e.ColumnIndex == datagridviewadjustment.Columns["colActions"].Index)
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

        private void datagridviewadjustment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                var row = datagridviewadjustment.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                datagridviewadjustment.InvalidateCell(0, e.RowIndex);
                return;
            }

            if (e.ColumnIndex == datagridviewadjustment.Columns["colActions"].Index)
            {
                var cellRect = datagridviewadjustment.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewadjustment.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;

                int iconSize = 24;
                int gap = 16;
                int totalWidth = (iconSize * 2) + gap;
                int iconX = (cellRect.Width - totalWidth) / 2;

                string adjustmentId = datagridviewadjustment.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + iconSize)
                    OpenEditAdjustment(adjustmentId);
                else if (clickX >= iconX + iconSize + gap && clickX < iconX + totalWidth)
                    OpenViewAdjustment(adjustmentId);
            }
        }

        private void OpenEditAdjustment(string adjustmentId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Stock Adjustment";
            var editForm = new EditStockAdjustment(adjustmentId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(editForm);
            editForm.Show();
        }

        private void OpenViewAdjustment(string adjustmentId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "View Stock Adjustment";
            var viewForm = new ViewStockAdjustment(adjustmentId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(viewForm);
            viewForm.Show();
        }

        private void btnaddadjustment_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Stock Adjustment";
            var addForm = new AddStockAdjustment
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void LoadSampleData()
        {
            AddRow("ADJ001", "2025-04-10", "CCTV Camera Pro", "Increase", "50", "John Doe", "Pending");
            AddRow("ADJ002", "2025-04-08", "Wireless Speaker", "Decrease", "20", "Jane Smith", "Approved");
            AddRow("ADJ003", "2025-04-05", "Dual Monitor", "Increase", "15", "Mike Tan", "Rejected");
        }

        private void AddRow(string id, string date, string productName, string adjType, string physicalCount, string requestedBy, string status)
        {
            int idx = datagridviewadjustment.Rows.Add(false, date, productName, adjType, physicalCount, requestedBy, status, null);
            var row = datagridviewadjustment.Rows[idx];
            row.Cells[0].Tag = id;
            row.Height = 45;
        }
    }
}