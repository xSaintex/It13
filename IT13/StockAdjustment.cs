using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class StockAdjustment : Form
    {
        private readonly Image _editIcon;
        private readonly Image _viewIcon;

        public StockAdjustment()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(16, 16));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(16, 16));

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
            Filter.Items.Add("Approved");
            Filter.Items.Add("Rejected");
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

        private void txtboxsearch_TextChanged(object sender, EventArgs e) { }

        private void datagridviewadjustment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Paint Checkbox + ID
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = Convert.ToBoolean(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 8, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = datagridviewadjustment.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                var textSize = e.Graphics.MeasureString(idText, e.CellStyle.Font);
                var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);

                e.Graphics.DrawString(idText, e.CellStyle.Font, Brushes.Black, textRect);
                e.Handled = true;
            }

            // Paint Edit + View Icons
            if (e.ColumnIndex == datagridviewadjustment.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 40) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 16) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 16, 16);
                e.Graphics.DrawImage(_viewIcon, x + 20, y, 16, 16);
                e.Handled = true;
            }
        }

        private void datagridviewadjustment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Handle Checkbox Click
            if (e.ColumnIndex == 0)
            {
                var row = datagridviewadjustment.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                datagridviewadjustment.InvalidateCell(0, e.RowIndex);
                return;
            }

            // Handle Actions (Edit/View)
            if (e.ColumnIndex == datagridviewadjustment.Columns["colActions"].Index)
            {
                var cellRect = datagridviewadjustment.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewadjustment.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconX = (cellRect.Width - 40) / 2;

                string adjustmentId = datagridviewadjustment.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + 16)
                {
                    OpenEditAdjustment(adjustmentId);
                }
                else if (clickX >= iconX + 20 && clickX < iconX + 36)
                {
                    OpenViewAdjustment(adjustmentId);
                }
            }
        }

        private void OpenEditAdjustment(string adjustmentId)
        {
            Form1 parent = this.ParentForm as Form1;
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
            Form1 parent = this.ParentForm as Form1;
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
            Form1 parent = this.ParentForm as Form1;
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
            row.Cells[0].Tag = id; // Store actual ID in Tag
        }
    }
}