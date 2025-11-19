using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class OrderList : Form
    {
        private readonly Image _editIcon;
        private readonly Image _viewIcon;
        private readonly Image _deleteIcon;

        public OrderList()
        {
            InitializeComponent();

            // Load icons from Resources (16x16)
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(16, 16));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(16, 16));
            _deleteIcon = new Bitmap(Properties.Resources.delete_icon, new Size(16, 16));

            // Ensure both buttons remain visually identical at runtime
            ForceUniformButton(btnAddCustomer);
            ForceUniformButton(btnAddSupplier);

            SetupFilterComboBox();
            SetupExportComboBox();
            LoadSampleData();
        }

        // Ensures identical visual/layout properties for a Guna2Button
        private void ForceUniformButton(Guna2Button b)
        {
            if (b == null) return;

            // geometry & text
            b.Size = new Size(170, 40);
            b.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            b.TextOffset = new Point(0, 0);
            b.Padding = new Padding(0);
            b.Margin = new Padding(0);

            // visuals
            b.FillColor = Color.FromArgb(0, 123, 255);
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            b.BorderRadius = 8;
            b.AutoRoundedCorners = false;
            b.UseTransparentBackground = false;
            b.Image = null;
            b.ImageAlign = HorizontalAlignment.Center;
            b.ImageOffset = new Point(0, 0);
            b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b.RightToLeft = RightToLeft.No;

            // ensure edges are uniformly customizable (prevents asymmetric rounding)
            b.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges
            {
                BottomLeft = true,
                BottomRight = true,
                TopLeft = true,
                TopRight = true
            };

            // hover/pressed appearance (make them identical)
            b.HoverState.FillColor = Color.FromArgb(0, 105, 220);
            b.HoverState.ForeColor = Color.White;
            b.PressedColor = Color.FromArgb(0, 90, 190);

            // force z-order and layout update
            b.BringToFront();
            b.Invalidate();
        }

        private void SetupFilterComboBox()
        {
            cmbFilters.Items.Clear();
            cmbFilters.Items.Add("Filters");
            cmbFilters.Items.Add("All");
            cmbFilters.Items.Add("Customer");
            cmbFilters.Items.Add("Supplier");
            cmbFilters.Items.Add("Pending");
            cmbFilters.Items.Add("Completed");
            cmbFilters.SelectedIndex = 0;
            cmbFilters.ForeColor = Color.Gray;

            cmbFilters.SelectedIndexChanged += (s, e) =>
            {
                cmbFilters.ForeColor = cmbFilters.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            };
        }

        private void SetupExportComboBox()
        {
            cmbExport.Items.Clear();
            cmbExport.Items.Add("Export Data");
            cmbExport.Items.Add("Excel");
            cmbExport.Items.Add("PDF");
            cmbExport.Items.Add("CSV");
            cmbExport.SelectedIndex = 0;
            cmbExport.ForeColor = Color.Gray;

            cmbExport.SelectedIndexChanged += (s, e) =>
            {
                cmbExport.ForeColor = cmbExport.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            };
        }

        private void datagridvieworders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // === CHECKBOX + ID ===
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = Convert.ToBoolean(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 8, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                string idText = datagridvieworders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                var textSize = e.Graphics.MeasureString(idText, e.CellStyle.Font);
                var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);
                e.Graphics.DrawString(idText, e.CellStyle.Font, Brushes.Black, textRect);
                e.Handled = true;
            }

            // === ACTIONS: Edit | View | Delete ===
            if (e.ColumnIndex == datagridvieworders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 60) / 2; // 3 icons = 48px + spacing
                int y = e.CellBounds.Y + (e.CellBounds.Height - 16) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, 16, 16);
                e.Graphics.DrawImage(_viewIcon, x + 20, y, 16, 16);
                e.Graphics.DrawImage(_deleteIcon, x + 40, y, 16, 16);
                e.Handled = true;
            }
        }

        private void datagridvieworders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // === CHECKBOX TOGGLE ===
            if (e.ColumnIndex == 0)
            {
                var row = datagridvieworders.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                datagridvieworders.InvalidateCell(0, e.RowIndex);
                return;
            }

            // === ACTIONS: Edit | View | Delete ===
            if (e.ColumnIndex == datagridvieworders.Columns["colActions"].Index)
            {
                var cellRect = datagridvieworders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridvieworders.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconX = (cellRect.Width - 60) / 2;

                string orderId = datagridvieworders.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + 16)
                {
                    MessageBox.Show($"Edit Order: {orderId}");
                }
                else if (clickX >= iconX + 20 && clickX < iconX + 36)
                {
                    MessageBox.Show($"View Order: {orderId}");
                }
                else if (clickX >= iconX + 40 && clickX < iconX + 56)
                {
                    MessageBox.Show($"Delete Order: {orderId}");
                }
            }
        }

        private void LoadSampleData()
        {
            AddRow("ORD001", "Customer", "Incio", "1 pcs.", "₱290.00", "Pending", "2025-10-22 00:00:00");
            AddRow("ORD002", "Customer", "Incio", "1 pcs.", "₱2,000.00", "Pending", "2025-10-16 00:00:00");
            AddRow("ORD003", "Supplier", "Incio", "2 pcs.", "₱2,200.00", "Pending", "2025-10-17 00:00:00");
        }

        private void AddRow(string id, string orderType, string company, string qty, string total, string status, string date)
        {
            int idx = datagridvieworders.Rows.Add(false, orderType, company, qty, total, status, date, null);
            var row = datagridvieworders.Rows[idx];
            row.Cells[0].Tag = id;
            row.Cells[5].Style.ForeColor = Color.FromArgb(0, 123, 255);
            row.Cells[5].Style.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        }
    }
}
