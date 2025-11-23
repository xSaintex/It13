using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class EditCustomerReturns
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
            mainPanel = new Guna2ShadowPanel();
            contentPanel = new Guna2Panel();
            lblHeader = new Label();
            lblRequired = new Label();
            lnkBack = new LinkLabel();
            btnCustomerOrder = new Guna2Button();
            btnAddress = new Guna2Button();
            btnReturns = new Guna2Button();
            pnlCustomerOrder = new Guna2ShadowPanel();
            pnlAddress = new Guna2ShadowPanel();
            pnlReturns = new Guna2ShadowPanel();

            lblOrderId = new Label(); cmbCustomerOrderID = new Guna2ComboBox();
            lblPaymentTerms = new Label(); cmbPaymentTerms = new Guna2ComboBox();
            lblStatus = new Label(); cmbStatus = new Guna2ComboBox();
            lblItems = new Label();
            dgvOrderItems = new Guna2DataGridView();
            colProduct = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();

            lblBillingTitle = new Label(); lblShippingTitle = new Label();
            txtBillingAddress = new Guna2TextBox();
            txtShippingAddress = new Guna2TextBox();

            lblReturnDate = new Label(); dtpReturnDate = new Guna2DateTimePicker();
            lblReturnType = new Label(); cmbReturnType = new Guna2ComboBox();
            lblReturnReason = new Label(); txtReturnReason = new Guna2TextBox();

            btnSaveCustomerOrder = new Guna2Button();
            btnSaveAddress = new Guna2Button();
            btnSaveReturns = new Guna2Button();

            lblTotalLabelCO = new Label(); lblTotalAmountCO = new Label();
            lblTotalLabelAddr = new Label(); lblTotalAmountAddr = new Label();
            lblTotalLabelRet = new Label(); lblTotalAmountRet = new Label();

            mainPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            pnlCustomerOrder.SuspendLayout();
            pnlAddress.SuspendLayout();
            pnlReturns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
            this.SuspendLayout();

            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 860);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 16;
            mainPanel.Controls.Add(contentPanel);

            contentPanel.Dock = DockStyle.Fill;
            contentPanel.AutoScroll = false;
            contentPanel.Padding = new Padding(30, 20, 30, 20);

            lblHeader.Text = "Edit Customer Return";
            lblHeader.Font = new Font("Tahoma", 20F, FontStyle.Bold);
            lblHeader.Location = new Point(50, 30);
            lblHeader.AutoSize = true;

            lblRequired.Text = "Fields marked with an asterisk (*) are required.";
            lblRequired.ForeColor = Color.FromArgb(220, 53, 69);
            lblRequired.Font = new Font("Poppins", 10F);
            lblRequired.Location = new Point(54, 73);
            lblRequired.AutoSize = true;

            lnkBack.Text = "← Back to Customer Returns";
            lnkBack.LinkColor = Color.FromArgb(0, 123, 255);
            lnkBack.Font = new Font("Poppins", 10F);
            lnkBack.Location = new Point(1280, 68);
            lnkBack.AutoSize = true;

            int tabY = 120;
            btnCustomerOrder.Text = "🧾 Customer Order Information";
            btnCustomerOrder.Size = new Size(250, 36);
            btnCustomerOrder.Location = new Point(54, tabY);
            btnCustomerOrder.FillColor = Color.FromArgb(0, 123, 255);
            btnCustomerOrder.ForeColor = Color.White;
            btnCustomerOrder.BorderRadius = 5;

            btnAddress.Text = "📍 Address";
            btnAddress.Size = new Size(170, 36);
            btnAddress.Location = new Point(315, tabY);
            btnAddress.FillColor = Color.FromArgb(245, 245, 245);
            btnAddress.ForeColor = Color.Black;
            btnAddress.BorderRadius = 5;

            btnReturns.Text = "📤 Returns";
            btnReturns.Size = new Size(170, 36);
            btnReturns.Location = new Point(495, tabY);
            btnReturns.FillColor = Color.FromArgb(245, 245, 245);
            btnReturns.ForeColor = Color.Black;
            btnReturns.BorderRadius = 5;

            int panelY = tabY + 70;

            // CUSTOMER ORDER PANEL
            pnlCustomerOrder.Location = new Point(50, panelY);
            pnlCustomerOrder.Size = new Size(1500, 650);
            pnlCustomerOrder.FillColor = Color.FromArgb(248, 249, 252);
            pnlCustomerOrder.Radius = 20;
            pnlCustomerOrder.Visible = true;

            int cy = 50;
            lblOrderId.Text = "Customer Order ID *";
            lblOrderId.Font = new Font("Poppins", 11F);
            lblOrderId.Location = new Point(40, cy);
            lblOrderId.Size = new Size(180, 28);

            cmbCustomerOrderID.Location = new Point(40, cy + 28);
            cmbCustomerOrderID.Size = new Size(420, 44);
            cmbCustomerOrderID.BorderRadius = 8;
            cmbCustomerOrderID.ForeColor = Color.Black;
            cmbCustomerOrderID.Font = new Font("Poppins", 10F);

            lblPaymentTerms.Text = "Payment *";
            lblPaymentTerms.Font = new Font("Poppins", 11F);
            lblPaymentTerms.Location = new Point(500, cy);

            cmbPaymentTerms.Location = new Point(500, cy + 28);
            cmbPaymentTerms.Size = new Size(320, 44);
            cmbPaymentTerms.BorderRadius = 8;
            cmbPaymentTerms.ForeColor = Color.Black;
            cmbPaymentTerms.Font = new Font("Poppins", 10F);

            lblStatus.Text = "Status *";
            lblStatus.Font = new Font("Poppins", 11F);
            lblStatus.Location = new Point(860, cy);

            cmbStatus.Location = new Point(860, cy + 28);
            cmbStatus.Size = new Size(280, 44);
            cmbStatus.BorderRadius = 8;
            cmbStatus.ForeColor = Color.Black;
            cmbStatus.Font = new Font("Poppins", 10F);

            lblItems.Text = "Order Items Details";
            lblItems.Font = new Font("Poppins", 13F, FontStyle.Bold);
            lblItems.Location = new Point(40, cy + 100);
            lblItems.Size = new Size(300, 30);

            dgvOrderItems.Location = new Point(40, cy + 135);
            dgvOrderItems.Size = new Size(1420, 320);
            dgvOrderItems.BackgroundColor = Color.White;
            dgvOrderItems.ReadOnly = true;
            dgvOrderItems.AllowUserToAddRows = false;
            dgvOrderItems.AllowUserToDeleteRows = false;
            dgvOrderItems.MultiSelect = false;
            dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderItems.EnableHeadersVisualStyles = false;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvOrderItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 10F, FontStyle.Bold);
            dgvOrderItems.DefaultCellStyle.Font = new Font("Poppins", 10F);
            dgvOrderItems.ColumnHeadersHeight = 50;
            dgvOrderItems.RowTemplate.Height = 45;
            dgvOrderItems.DefaultCellStyle.SelectionBackColor = dgvOrderItems.DefaultCellStyle.BackColor;
            dgvOrderItems.DefaultCellStyle.SelectionForeColor = dgvOrderItems.DefaultCellStyle.ForeColor;

            colProduct.HeaderText = "Product Name"; colProduct.Width = 700;
            colQty.HeaderText = "Qty"; colQty.Width = 120; colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPrice.HeaderText = "Unit Price"; colPrice.Width = 280;
            colTotal.HeaderText = "Total"; colTotal.Width = 280;
            dgvOrderItems.Columns.AddRange(colProduct, colQty, colPrice, colTotal);

            btnSaveCustomerOrder.Text = "Update";
            btnSaveCustomerOrder.Size = new Size(180, 44);
            btnSaveCustomerOrder.Location = new Point(40, 540);
            btnSaveCustomerOrder.FillColor = Color.FromArgb(0, 123, 255);
            btnSaveCustomerOrder.ForeColor = Color.White;
            btnSaveCustomerOrder.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSaveCustomerOrder.BorderRadius = 10;

            lblTotalLabelCO.Text = "Total Refunded:";
            lblTotalLabelCO.Font = new Font("Poppins", 11F);
            lblTotalLabelCO.Location = new Point(1100, 555);
            lblTotalLabelCO.AutoSize = true;

            lblTotalAmountCO.Text = "₱0.00";
            lblTotalAmountCO.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmountCO.ForeColor = Color.FromArgb(0, 123, 255);
            lblTotalAmountCO.Location = new Point(1240, 545);
            lblTotalAmountCO.AutoSize = true;

            pnlCustomerOrder.Controls.AddRange(new Control[]
            {
                lblOrderId, cmbCustomerOrderID, lblPaymentTerms, cmbPaymentTerms,
                lblStatus, cmbStatus, lblItems, dgvOrderItems,
                btnSaveCustomerOrder, lblTotalLabelCO, lblTotalAmountCO
            });

            // ADDRESS PANEL
            pnlAddress.Location = new Point(50, panelY);
            pnlAddress.Size = new Size(1500, 650);
            pnlAddress.FillColor = Color.FromArgb(248, 249, 252);
            pnlAddress.Radius = 20;
            pnlAddress.Visible = false;

            int ay = 60;
            lblBillingTitle.Text = "Billing Address *";
            lblBillingTitle.Font = new Font("Poppins", 12F, FontStyle.Bold);
            lblBillingTitle.Location = new Point(40, ay);
            lblBillingTitle.Size = new Size(300, 30);

            txtBillingAddress.Location = new Point(40, ay + 35);
            txtBillingAddress.Size = new Size(600, 180);
            txtBillingAddress.Multiline = true;
            txtBillingAddress.BorderRadius = 10;
            txtBillingAddress.ForeColor = Color.Black;
            txtBillingAddress.Font = new Font("Poppins", 10F);

            lblShippingTitle.Text = "Shipping Address *";
            lblShippingTitle.Font = new Font("Poppins", 12F, FontStyle.Bold);
            lblShippingTitle.Location = new Point(750, ay);
            lblShippingTitle.Size = new Size(300, 30);

            txtShippingAddress.Location = new Point(750, ay + 35);
            txtShippingAddress.Size = new Size(650, 180);
            txtShippingAddress.Multiline = true;
            txtShippingAddress.BorderRadius = 10;
            txtShippingAddress.ForeColor = Color.Black;
            txtShippingAddress.Font = new Font("Poppins", 10F);

            btnSaveAddress.Text = "Update";
            btnSaveAddress.Size = new Size(180, 44);
            btnSaveAddress.Location = new Point(40, 540);
            btnSaveAddress.FillColor = Color.FromArgb(0, 123, 255);
            btnSaveAddress.ForeColor = Color.White;
            btnSaveAddress.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSaveAddress.BorderRadius = 10;

            lblTotalLabelAddr.Text = "Total Refunded:";
            lblTotalLabelAddr.Font = new Font("Poppins", 11F);
            lblTotalLabelAddr.Location = new Point(1100, 555);
            lblTotalLabelAddr.AutoSize = true;

            lblTotalAmountAddr.Text = "₱0.00";
            lblTotalAmountAddr.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmountAddr.ForeColor = Color.FromArgb(0, 123, 255);
            lblTotalAmountAddr.Location = new Point(1240, 545);
            lblTotalAmountAddr.AutoSize = true;

            pnlAddress.Controls.AddRange(new Control[]
            {
                lblBillingTitle, lblShippingTitle,
                txtBillingAddress, txtShippingAddress,
                btnSaveAddress, lblTotalLabelAddr, lblTotalAmountAddr
            });

            // RETURNS PANEL
            pnlReturns.Location = new Point(50, panelY);
            pnlReturns.Size = new Size(1500, 650);
            pnlReturns.FillColor = Color.FromArgb(248, 249, 252);
            pnlReturns.Radius = 20;
            pnlReturns.Visible = false;

            int ry = 60;
            lblReturnDate.Text = "Return Date *";
            lblReturnDate.Font = new Font("Poppins", 11F);
            lblReturnDate.Location = new Point(40, ry);
            lblReturnDate.Size = new Size(150, 28);

            dtpReturnDate.Location = new Point(40, ry + 28);
            dtpReturnDate.Size = new Size(360, 44);
            dtpReturnDate.BorderRadius = 8;
            dtpReturnDate.FillColor = Color.White;

            lblReturnType.Text = "Return Type *";
            lblReturnType.Font = new Font("Poppins", 11F);
            lblReturnType.Location = new Point(440, ry);
            lblReturnType.Size = new Size(150, 28);

            cmbReturnType.Location = new Point(440, ry + 28);
            cmbReturnType.Size = new Size(400, 44);
            cmbReturnType.BorderRadius = 8;
            cmbReturnType.ForeColor = Color.Black;
            cmbReturnType.Font = new Font("Poppins", 10F);

            lblReturnReason.Text = "Return Reason *";
            lblReturnReason.Font = new Font("Poppins", 11F);
            lblReturnReason.Location = new Point(40, ry + 100);
            lblReturnReason.Size = new Size(150, 28);

            txtReturnReason.Location = new Point(40, ry + 128);
            txtReturnReason.Size = new Size(1420, 200);
            txtReturnReason.Multiline = true;
            txtReturnReason.BorderRadius = 10;
            txtReturnReason.ScrollBars = ScrollBars.Vertical;
            txtReturnReason.ForeColor = Color.Black;
            txtReturnReason.Font = new Font("Poppins", 10F);

            btnSaveReturns.Text = "Update";
            btnSaveReturns.Size = new Size(180, 44);
            btnSaveReturns.Location = new Point(40, 540);
            btnSaveReturns.FillColor = Color.FromArgb(0, 123, 255);
            btnSaveReturns.ForeColor = Color.White;
            btnSaveReturns.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSaveReturns.BorderRadius = 10;

            lblTotalLabelRet.Text = "Total Refunded:";
            lblTotalLabelRet.Font = new Font("Poppins", 11F);
            lblTotalLabelRet.Location = new Point(1100, 555);
            lblTotalLabelRet.AutoSize = true;

            lblTotalAmountRet.Text = "₱0.00";
            lblTotalAmountRet.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmountRet.ForeColor = Color.FromArgb(0, 123, 255);
            lblTotalAmountRet.Location = new Point(1240, 545);
            lblTotalAmountRet.AutoSize = true;

            pnlReturns.Controls.AddRange(new Control[]
            {
                lblReturnDate, dtpReturnDate, lblReturnType, cmbReturnType,
                lblReturnReason, txtReturnReason,
                btnSaveReturns, lblTotalLabelRet, lblTotalAmountRet
            });

            contentPanel.Controls.AddRange(new Control[]
            {
                lblHeader, lblRequired, lnkBack,
                btnCustomerOrder, btnAddress, btnReturns,
                pnlCustomerOrder, pnlAddress, pnlReturns
            });

            this.Controls.Add(mainPanel);
            this.ClientSize = new Size(1920, 1080);
            this.Text = "Edit Customer Return";
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
            pnlCustomerOrder.ResumeLayout(false);
            pnlAddress.ResumeLayout(false);
            pnlReturns.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainPanel, pnlCustomerOrder, pnlAddress, pnlReturns;
        private Guna2Panel contentPanel;
        private Label lblHeader, lblRequired;
        private LinkLabel lnkBack;
        private Guna2Button btnCustomerOrder, btnAddress, btnReturns;
        private Guna2Button btnSaveCustomerOrder, btnSaveAddress, btnSaveReturns;
        private Label lblOrderId, lblPaymentTerms, lblStatus, lblItems;
        private Guna2ComboBox cmbCustomerOrderID, cmbPaymentTerms, cmbStatus;
        private Guna2DataGridView dgvOrderItems;
        private DataGridViewTextBoxColumn colProduct, colQty, colPrice, colTotal;
        private Label lblBillingTitle, lblShippingTitle;
        private Guna2TextBox txtBillingAddress, txtShippingAddress;
        private Label lblReturnDate, lblReturnType, lblReturnReason;
        private Guna2DateTimePicker dtpReturnDate;
        private Guna2ComboBox cmbReturnType;
        private Guna2TextBox txtReturnReason;
        private Label lblTotalLabelCO, lblTotalAmountCO;
        private Label lblTotalLabelAddr, lblTotalAmountAddr;
        private Label lblTotalLabelRet, lblTotalAmountRet;
    }
}