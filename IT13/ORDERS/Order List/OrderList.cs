using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class OrderList : Form
    {
        private readonly Image _viewIcon;
        private readonly Image _deleteIcon;
        private bool? _headerCheckState = false;

        public OrderList()
        {
            InitializeComponent();

            _viewIcon = Properties.Resources.view_icon ?? CreatePlaceholder(Color.FromArgb(0, 123, 255));
            _deleteIcon = Properties.Resources.delete_icon ?? CreatePlaceholder(Color.FromArgb(220, 53, 69));

            SetupFilterComboBox();
            SetupExportComboBox();

            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.DefaultCellStyle.SelectionBackColor = dgvOrders.DefaultCellStyle.BackColor;
            dgvOrders.DefaultCellStyle.SelectionForeColor = dgvOrders.DefaultCellStyle.ForeColor;
            dgvOrders.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvOrders.Columns["colCheck"].MinimumWidth = 180;
            dgvOrders.Columns["colCheck"].Width = 180;
            dgvOrders.Columns["colCheck"].FillWeight = 20;

            dgvOrders.Columns["colOrderType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colOrderType"].FillWeight = 12;
            dgvOrders.Columns["colCompanyName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colCompanyName"].FillWeight = 25;
            dgvOrders.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colQty"].FillWeight = 8;
            dgvOrders.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvOrders.Columns["colTotal"].FillWeight = 12;
            dgvOrders.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colStatus"].FillWeight = 12;
            dgvOrders.Columns["colEstDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns["colEstDate"].FillWeight = 12;
            dgvOrders.Columns["colActions"].FillWeight = 10;

            LoadSampleData();
            UpdateHeaderCheckState();
            dgvOrders.ClearSelection();
        }

        private Image CreatePlaceholder(Color c)
        {
            var b = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(b)) g.Clear(Color.Transparent);
            return b;
        }

        private void SetupFilterComboBox() { /* your code - unchanged */ }
        private void SetupExportComboBox() { /* your code - unchanged */ }

        private void LoadSampleData()
        {
            AddRow("CO-2025-001", "Customer", "TechNova Corp", 48, "₱156,800.00", "Delivered", "2025-11-28");
            AddRow("SO-2025-002", "Supplier", "Global Retail Inc.", 120, "₱89,500.00", "Pending", "2025-12-05");
            AddRow("CO-2025-003", "Customer", "Prime Distributors", 35, "₱210,000.00", "Processing", "2025-12-01");
            AddRow("SO-2025-004", "Supplier", "Metro Traders Ltd.", 80, "₱425,000.00", "Delivered", "2025-11-20");
            AddRow("CO-2025-005", "Customer", "Alpha Solutions", 15, "₱67,900.00", "Cancelled", "2025-11-25");
        }

        private void AddRow(string orderId, string orderType, string company, int qty, string total, string status, string estDate)
        {
            int idx = dgvOrders.Rows.Add(false, orderType, company, qty, total, status, estDate, null);
            var row = dgvOrders.Rows[idx];
            row.Cells["colCheck"].Tag = orderId;
            row.Cells["colOrderType"].Tag = orderType;
            row.Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells["colCheck"].Value ?? false)) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                checkedCount == 0 ? false :
                checkedCount == visibleCount ? true : (bool?)null;
            dgvOrders.InvalidateCell(0, -1);
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header checkbox + text
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (_headerCheckState == true)
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] { new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5) });
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, r.X + 3, r.Y + 3, 10, 10);

                TextRenderer.DrawText(e.Graphics, "Order ID", new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // Row checkbox + order id
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);
                if (chk)
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] { new Point(r.X + 3, r.Y + 8), new Point(r.X + 7, r.Y + 12), new Point(r.X + 13, r.Y + 5) });

                string id = dgvOrders.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id, new Font("Bahnschrift SemiCondensed", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.Handled = true;
                return;
            }

            // Status badge
            if (e.ColumnIndex == dgvOrders.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.ToLower() switch
                {
                    "delivered" => Color.FromArgb(34, 197, 94),
                    "pending" => Color.FromArgb(255, 193, 7),
                    "processing" => Color.FromArgb(0, 123, 255),
                    "cancelled" => Color.FromArgb(220, 53, 69),
                    _ => Color.Gray
                };
                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var br = new SolidBrush(bg))
                    e.Graphics.FillPath(br, path);
                using (var f = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
                using (var br = new SolidBrush(Color.White))
                {
                    var sz = e.Graphics.MeasureString(status, f);
                    e.Graphics.DrawString(status, f, br,
                        e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);
                }
                e.Handled = true;
                return;
            }

            // View + Delete icons
            if (e.ColumnIndex == dgvOrders.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int totalWidth = 58; // 24 + 10 + 24
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;

                e.Graphics.DrawImage(_viewIcon, startX, y, 24, 24);
                e.Graphics.DrawImage(_deleteIcon, startX + 34, y, 24, 24);

                e.Handled = true; // ← THIS IS WHAT YOUR RETURNLIST USES AND IT WORKS
            }
        }

        // EXACT SAME LOGIC AS YOUR WORKING ReturnList
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.CurrentCell = null;

            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvOrders.Rows)
                    if (r.Visible && !r.IsNewRow) r.Cells[0].Value = newState;
                _headerCheckState = newState ? true : (bool?)false;
                dgvOrders.InvalidateCell(0, -1);
                return;
            }

            // Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            // View or Delete icon
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOrders.Columns["colActions"].Index)
            {
                var cellRect = dgvOrders.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point mouse = dgvOrders.PointToClient(Cursor.Position);
                int relativeX = mouse.X - cellRect.X;

                string orderId = dgvOrders.Rows[e.RowIndex].Cells["colCheck"].Tag?.ToString() ?? "";
                string orderType = dgvOrders.Rows[e.RowIndex].Cells["colOrderType"].Tag?.ToString() ?? "";
                var parent = this.ParentForm as Form1;
                if (parent == null) return;

                if (relativeX >= 17 && relativeX <= 41) // View icon area
                {
                    parent.navBar1.PageTitle = $"View Order: {orderId}";
                    Form f = orderType == "Customer" ? new ViewCustomerOrder(orderId) : new ViewSupplierOrder(orderId);
                    f.Tag = this; f.TopLevel = false; f.FormBorderStyle = FormBorderStyle.None; f.Dock = DockStyle.Fill;
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(f);
                    f.Show();
                }
                else if (relativeX >= 51 && relativeX <= 75) // Delete icon area
                {
                    if (MessageBox.Show($"Delete {orderId}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvOrders.Rows.RemoveAt(e.RowIndex);
                        UpdateHeaderCheckState();
                    }
                }
            }
        }

        // Your other buttons unchanged methods (btnSearch_Click, btnAddCustomerOrder_Click, etc.)
        private void btnSearch_Click(object sender, EventArgs e) { /* your code */ }
        private void btnAddCustomerOrder_Click(object sender, EventArgs e) { /* your code */ }
        private void btnAddSupplierOrder_Click(object sender, EventArgs e) { /* your code */ }
    }
}