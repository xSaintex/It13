// ---------------------------------------------------------------------
// ViewDelivery.designer.cs – FINAL 100% PERFECT & UNCLICKABLE
// ---------------------------------------------------------------------
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IT13
{
    partial class ViewDelivery
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            var mainPanel = new Guna2ShadowPanel();
            var scrollPanel = new Guna2Panel();
            var contentPanel = new Guna2Panel();
            var bottomPanel = new Guna2Panel();

            lblHeader = new Label();
            lblCreated = new Label();
            infoPanel = new Guna2Panel();
            txtCustomerOrder = new Label();
            txtDeliveryDate = new Label();
            txtEmployee = new Label();
            txtVehicle = new Label();
            txtPlate = new Label();
            txtLastAttempt = new Label();
            pnlStatusBadge = new Guna2Panel();
            lblStatusValue = new Label();

            btnAttempts = new Guna2Button();
            btnDetails = new Guna2Button();

            pnlAttempts = new Guna2ShadowPanel();
            var lblAttemptsHeader = new Label();
            btnAddAttempt = new Guna2Button();
            dgvAttempts = new DataGridView();

            pnlDetails = new Guna2ShadowPanel();
            var lblInfoHeader = new Label();
            var lblCustomerTitle = new Label();
            lblCustomerValue = new Label();
            var lblShippingTitle = new Label();
            lblShippingValue = new Label();

            btnBack = new Guna2Button();

            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            infoPanel.SuspendLayout();
            pnlAttempts.SuspendLayout();
            pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dgvAttempts)).BeginInit();
            this.SuspendLayout();

            // ===================== MAIN PANEL =====================
            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 878);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.ShadowDepth = 10;
            mainPanel.Controls.Add(scrollPanel);
            mainPanel.Controls.Add(bottomPanel);

            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1602, 800);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);
            contentPanel.Size = new Size(1458, 1100);

            // ===================== HEADER & INFO PANEL (unchanged) =====================
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 25);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            lblCreated.Font = new Font("Poppins", 9F);
            lblCreated.ForeColor = Color.Gray;
            lblCreated.Location = new Point(77, 65);
            lblCreated.AutoSize = true;

            infoPanel.Location = new Point(77, 100);
            infoPanel.Size = new Size(1380, 210);
            infoPanel.FillColor = Color.White;
            infoPanel.BorderColor = Color.FromArgb(220, 220, 220);
            infoPanel.BorderThickness = 1;
            infoPanel.BorderRadius = 12;

            int ix = 60, iy = 35, gapY = 70;
            int col1 = ix, col2 = 400, col3 = 750, col4 = 1100;

            var lbl1 = new Label(); lbl1.Text = "Customer Order"; lbl1.Font = new Font("Poppins", 10F); lbl1.Location = new Point(col1, iy);
            txtCustomerOrder.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtCustomerOrder.Location = new Point(col1, iy + 24); txtCustomerOrder.AutoSize = true;
            var lbl2 = new Label(); lbl2.Text = "Delivery Date"; lbl2.Font = new Font("Poppins", 10F); lbl2.Location = new Point(col2, iy);
            txtDeliveryDate.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtDeliveryDate.Location = new Point(col2, iy + 24); txtDeliveryDate.AutoSize = true;
            var lbl3 = new Label(); lbl3.Text = "Assigned Employee"; lbl3.Font = new Font("Poppins", 10F); lbl3.Location = new Point(col3, iy);
            txtEmployee.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtEmployee.Location = new Point(col3, iy + 24); txtEmployee.AutoSize = true;
            var lbl4 = new Label(); lbl4.Text = "Vehicle"; lbl4.Font = new Font("Poppins", 10F); lbl4.Location = new Point(col1, iy + gapY);
            txtVehicle.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtVehicle.Location = new Point(col1, iy + gapY + 24); txtVehicle.AutoSize = true;
            var lbl5 = new Label(); lbl5.Text = "Plate Number"; lbl5.Font = new Font("Poppins", 10F); lbl5.Location = new Point(col2, iy + gapY);
            txtPlate.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtPlate.Location = new Point(col2, iy + gapY + 24); txtPlate.AutoSize = true;
            var lbl6 = new Label(); lbl6.Text = "Last Attempt Date"; lbl6.Font = new Font("Poppins", 10F); lbl6.Location = new Point(col3, iy + gapY);
            txtLastAttempt.Font = new Font("Tahoma", 13F, FontStyle.Bold); txtLastAttempt.Location = new Point(col3, iy + gapY + 24); txtLastAttempt.AutoSize = true;

            var lblStatusTitle = new Label(); lblStatusTitle.Text = "Status"; lblStatusTitle.Font = new Font("Poppins", 10F); lblStatusTitle.Location = new Point(col4, iy + gapY);
            pnlStatusBadge.Size = new Size(110, 32);
            pnlStatusBadge.Location = new Point(col4, iy + gapY + 24);
            pnlStatusBadge.FillColor = Color.FromArgb(230, 255, 240);
            pnlStatusBadge.BorderRadius = 16;
            lblStatusValue.Font = new Font("Poppins", 10F, FontStyle.Bold);
            lblStatusValue.ForeColor = Color.FromArgb(0, 150, 80);
            lblStatusValue.Text = "Delivered";
            lblStatusValue.Dock = DockStyle.Fill;
            lblStatusValue.TextAlign = ContentAlignment.MiddleCenter;
            pnlStatusBadge.Controls.Add(lblStatusValue);

            infoPanel.Controls.AddRange(new Control[] {
                lbl1, txtCustomerOrder, lbl2, txtDeliveryDate, lbl3, txtEmployee,
                lbl4, txtVehicle, lbl5, txtPlate, lbl6, txtLastAttempt,
                lblStatusTitle, pnlStatusBadge
            });

            // ===================== TABS =====================
            int tabY = 340;
            btnAttempts.Text = "🚚 Delivery Attempts";
            btnAttempts.Location = new Point(77, tabY);
            btnAttempts.Size = new Size(180, 40);
            btnAttempts.FillColor = Color.FromArgb(0, 123, 255);
            btnAttempts.ForeColor = Color.White;
            btnAttempts.BorderRadius = 10;
            btnAttempts.Font = new Font("Tahoma", 8F, FontStyle.Regular);

            btnDetails.Text = "📦 Delivery Details";
            btnDetails.Location = new Point(270, tabY);
            btnDetails.Size = new Size(180, 40);
            btnDetails.FillColor = Color.FromArgb(240, 240, 240);
            btnDetails.ForeColor = Color.Black;
            btnDetails.BorderRadius = 10;
            btnDetails.Font = new Font("Tahoma", 8F, FontStyle.Regular);

            // ===================== ATTEMPTS PANEL =====================
            pnlAttempts.Location = new Point(77, tabY + 60);
            pnlAttempts.Size = new Size(1380, 380);
            pnlAttempts.FillColor = Color.FromArgb(248, 249, 252);
            pnlAttempts.Radius = 20;
            pnlAttempts.Visible = true;

            lblAttemptsHeader.Text = "Delivery Attempts";
            lblAttemptsHeader.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            lblAttemptsHeader.Location = new Point(35, 28);
            lblAttemptsHeader.AutoSize = true;

            btnAddAttempt.Text = "+ Add Attempt";
            btnAddAttempt.Location = new Point(1180, 22);
            btnAddAttempt.Size = new Size(160, 40);
            btnAddAttempt.FillColor = Color.FromArgb(0, 123, 255);
            btnAddAttempt.ForeColor = Color.White;
            btnAddAttempt.BorderRadius = 10;
            btnAddAttempt.Font = new Font("Poppins", 10F, FontStyle.Bold);

            // ===================== dgvAttempts – 100% UNCLICKABLE + NO SWAPPING + ALWAYS VISIBLE =====================
            dgvAttempts.Location = new Point(30, 70);
            dgvAttempts.Size = new Size(1320, 270);
            dgvAttempts.BackgroundColor = Color.White;
            dgvAttempts.BorderStyle = BorderStyle.None;
            dgvAttempts.RowTemplate.Height = 48;
            dgvAttempts.AllowUserToResizeRows = false;
            dgvAttempts.AllowUserToResizeColumns = false;
            dgvAttempts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAttempts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAttempts.ColumnHeadersHeight = 45;
            dgvAttempts.EnableHeadersVisualStyles = false;
            dgvAttempts.RowHeadersVisible = false;
            dgvAttempts.GridColor = Color.FromArgb(240, 240, 240);

            // Dark blue header
            dgvAttempts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 57, 101);
            dgvAttempts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAttempts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvAttempts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvAttempts.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvAttempts.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dgvAttempts.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvAttempts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(50, 50, 50);
            dgvAttempts.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);

            // TOTAL LOCKDOWN – NOTHING IS CLICKABLE OR MOVABLE
            dgvAttempts.ReadOnly = true;
            dgvAttempts.AllowUserToAddRows = false;
            dgvAttempts.AllowUserToDeleteRows = false;
            dgvAttempts.AllowUserToOrderColumns = false;     
            dgvAttempts.MultiSelect = false;
            dgvAttempts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAttempts.TabStop = false;                      
            dgvAttempts.Enabled = true;                       

            // FINAL DEFENSE – BLOCK ANY SELECTION OR FOCUS
            dgvAttempts.GotFocus += (s, e) => { ((DataGridView)s).ClearSelection(); ((DataGridView)s).CurrentCell = null; };
            dgvAttempts.MouseClick += (s, e) => { dgvAttempts.ClearSelection(); dgvAttempts.CurrentCell = null; };
            dgvAttempts.CellClick += (s, e) => { dgvAttempts.ClearSelection(); dgvAttempts.CurrentCell = null; };
            dgvAttempts.SelectionChanged += (s, e) => { dgvAttempts.ClearSelection(); dgvAttempts.CurrentCell = null; };
            dgvAttempts.CellMouseDown += (s, e) => { dgvAttempts.ClearSelection(); dgvAttempts.CurrentCell = null; };

            // Force no selection on load
            dgvAttempts.ClearSelection();
            dgvAttempts.CurrentCell = null;

            // Columns (only add once)
            if (dgvAttempts.Columns.Count == 0)
            {
                var colDate = new DataGridViewTextBoxColumn
                {
                    Name = "Date",
                    HeaderText = "Date",
                    FillWeight = 25,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                var colStatus = new DataGridViewTextBoxColumn
                {
                    Name = "Attempts",
                    HeaderText = "Attempts",
                    FillWeight = 22,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                var colRemarks = new DataGridViewTextBoxColumn
                {
                    Name = "Remarks",
                    HeaderText = "Remarks",
                    FillWeight = 53,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };

                dgvAttempts.Columns.Add(colDate);
                dgvAttempts.Columns.Add(colStatus);
                dgvAttempts.Columns.Add(colRemarks);

                colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colRemarks.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                colRemarks.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            // Colored status badge (only paints when there's data)
            dgvAttempts.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex != dgvAttempts.Columns["Attempts"].Index || e.Value == null) return;

                e.PaintBackground(e.CellBounds, true);
                string status = e.Value.ToString().Trim().ToLower();

                Color bg = status switch
                {
                    "delivered" => Color.FromArgb(34, 197, 94),
                    "failed" => Color.FromArgb(239, 68, 68),
                    "in transit" => Color.FromArgb(255, 193, 7),
                    "pending" => Color.FromArgb(108, 117, 125),
                    _ => Color.FromArgb(108, 117, 125)
                };

                var rect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 8, e.CellBounds.Width - 24, e.CellBounds.Height - 16);
                using var path = new GraphicsPath();
                float r = 12f;
                path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
                path.AddArc(rect.Right - r * 2, rect.Y, r * 2, r * 2, 270, 90);
                path.AddArc(rect.Right - r * 2, rect.Bottom - r * 2, r * 2, r * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - r * 2, r * 2, r * 2, 90, 90);
                path.CloseFigure();

                e.Graphics.FillPath(new SolidBrush(bg), path);

                using var brush = new SolidBrush(Color.White);
                using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                var sz = e.Graphics.MeasureString(e.Value.ToString(), font);
                e.Graphics.DrawString(e.Value.ToString(), font, brush,
                    e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);

                e.Handled = true;
            };

            // Add to panel (this line must stay exactly as you had it)
            pnlAttempts.Controls.AddRange(new Control[] { lblAttemptsHeader, btnAddAttempt, dgvAttempts });

            // ===================== REST OF FORM (unchanged) =====================
            pnlDetails.Location = new Point(77, tabY + 60);
            pnlDetails.Size = new Size(1380, 380);
            pnlDetails.FillColor = Color.FromArgb(248, 249, 252);
            pnlDetails.Radius = 20;
            pnlDetails.Visible = false;

            lblInfoHeader.Text = "Delivery Information";
            lblInfoHeader.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            lblInfoHeader.Location = new Point(35, 28);
            lblInfoHeader.AutoSize = true;

            lblCustomerTitle.Text = "Customer"; lblCustomerTitle.Font = new Font("Poppins", 10F); lblCustomerTitle.ForeColor = Color.Gray; lblCustomerTitle.Location = new Point(35, 85);
            lblCustomerValue.Font = new Font("Tahoma", 11F, FontStyle.Bold); lblCustomerValue.Location = new Point(35, 107); lblCustomerValue.AutoSize = true; lblCustomerValue.MaximumSize = new Size(1300, 0);
            lblShippingTitle.Text = "Shipping Address"; lblShippingTitle.Font = new Font("Poppins", 10F); lblShippingTitle.ForeColor = Color.Gray; lblShippingTitle.Location = new Point(35, 155);
            lblShippingValue.Font = new Font("Tahoma", 11F, FontStyle.Bold); lblShippingValue.Location = new Point(35, 177); lblShippingValue.AutoSize = true; lblShippingValue.MaximumSize = new Size(1300, 0);

            pnlDetails.Controls.AddRange(new Control[] { lblInfoHeader, lblCustomerTitle, lblCustomerValue, lblShippingTitle, lblShippingValue });

            bottomPanel.Location = new Point(0, 800);
            bottomPanel.Size = new Size(1602, 78);
            bottomPanel.BackColor = Color.White;
            btnBack.Text = "Back to List";
            btnBack.Location = new Point(1310, 20);
            btnBack.Size = new Size(170, 40);
            btnBack.FillColor = Color.FromArgb(100, 88, 255);
            btnBack.ForeColor = Color.White;
            btnBack.BorderRadius = 10;
            btnBack.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            bottomPanel.Controls.Add(btnBack);

            contentPanel.Controls.AddRange(new Control[] {
                lblHeader, lblCreated, infoPanel,
                btnAttempts, btnDetails,
                pnlAttempts, pnlDetails
            });

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainPanel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "ViewDelivery";

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            infoPanel.ResumeLayout(false); infoPanel.PerformLayout();
            pnlAttempts.ResumeLayout(false);
            pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvAttempts)).EndInit();
            this.ResumeLayout(false);
        }

        internal Label lblHeader, lblCreated;
        internal Guna2Panel infoPanel, pnlStatusBadge;
        internal Label txtCustomerOrder, txtDeliveryDate, txtEmployee, txtVehicle, txtPlate, txtLastAttempt, lblStatusValue;
        internal Label lblCustomerValue, lblShippingValue;
        internal Label lblCustomerTitle, lblShippingTitle;
        internal Guna2Button btnAttempts, btnDetails, btnAddAttempt, btnBack;
        internal Guna2ShadowPanel pnlAttempts, pnlDetails;
        internal DataGridView dgvAttempts;
    }
}