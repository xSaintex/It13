using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class ViewSupplierOrder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            mainPanel = new Guna2ShadowPanel();
            scrollPanel = new Guna2Panel();
            contentPanel = new Guna2Panel();
            bottomPanel = new Guna2Panel();
            tabControl = new Guna2TabControl();
            tabInfo = new TabPage();
            tabAddress = new TabPage();

            lblCompany = new Label();
            txtCompany = new Guna2TextBox();
            lblOrderDate = new Label();
            txtOrderDate = new Guna2TextBox();
            lblPayment = new Label();
            txtPayment = new Guna2TextBox();
            lblEstDate = new Label();
            txtEstDate = new Guna2TextBox();
            pnlItems = new Guna2Panel();
            lblItemTable = new Label();
            dgvItems = new Guna2DataGridView();
            colProdName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colSellPrice = new DataGridViewTextBoxColumn();
            colAvail = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            lblSubtotal = new Label();
            lblSubtotalVal = new Label();
            lblDiscount = new Label();
            txtDiscount = new Guna2TextBox();
            lblShipping = new Label();
            txtShipping = new Guna2TextBox();
            lblTotal = new Label();
            lblTotalVal = new Label();
            btnClose = new Guna2Button();

            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            tabControl.SuspendLayout();
            pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();

            // MAIN PANEL
            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 878);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.Controls.Add(scrollPanel);
            mainPanel.Controls.Add(bottomPanel);

            // SCROLL PANEL
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1602, 800);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);

            // CONTENT PANEL
            contentPanel.Location = new Point(0, 0);
            contentPanel.Size = new Size(1458, 1000);
            contentPanel.AutoSize = false;

            // TAB CONTROL
            tabControl.Location = new Point(77, 20);
            tabControl.Size = new Size(1458, 220);
            tabControl.TabMenuBackColor = Color.White;
            tabControl.TabButtonSelectedState.FillColor = Color.FromArgb(0, 123, 255);
            tabControl.TabMenuVisible = false;
            tabControl.Controls.Add(tabInfo);
            tabControl.Controls.Add(tabAddress);
            contentPanel.Controls.Add(tabControl);

            tabInfo.Text = "Supplier Information";
            tabAddress.Text = "Address";

            int y = 40;
            lblCompany.Text = "Company Name"; lblCompany.Location = new Point(40, y);
            txtCompany.Location = new Point(40, y + 25); txtCompany.Size = new Size(460, 36); txtCompany.ReadOnly = true;
            lblOrderDate.Text = "Order Date"; lblOrderDate.Location = new Point(520, y);
            txtOrderDate.Location = new Point(520, y + 25); txtOrderDate.Size = new Size(250, 36); txtOrderDate.ReadOnly = true;
            lblPayment.Text = "Payment Terms"; lblPayment.Location = new Point(40, y + 80);
            txtPayment.Location = new Point(40, y + 105); txtPayment.Size = new Size(460, 36); txtPayment.ReadOnly = true;
            lblEstDate.Text = "Estimated Date"; lblEstDate.Location = new Point(520, y + 80);
            txtEstDate.Location = new Point(520, y + 105); txtEstDate.Size = new Size(250, 36); txtEstDate.ReadOnly = true;

            tabInfo.Controls.AddRange(new Control[] { lblCompany, txtCompany, lblOrderDate, txtOrderDate, lblPayment, txtPayment, lblEstDate, txtEstDate });

            // ITEM PANEL
            pnlItems.Location = new Point(77, 260);
            pnlItems.Size = new Size(1458, 380);
            pnlItems.FillColor = Color.WhiteSmoke;

            lblItemTable.Text = "Item Table";
            lblItemTable.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblItemTable.Location = new Point(20, 15);

            dgvItems.Location = new Point(20, 60);
            dgvItems.Size = new Size(1418, 280);
            dgvItems.AllowUserToAddRows = false;
            dgvItems.ColumnHeadersHeight = 40;
            dgvItems.GridColor = Color.FromArgb(231, 229, 255);
            dgvItems.ReadOnly = true;

            colProdName.HeaderText = "PRODUCT NAME"; colProdName.Width = 400;
            colQty.HeaderText = "QUANTITY"; colQty.Width = 120;
            colSellPrice.HeaderText = "SELLING PRICE"; colSellPrice.Width = 150;
            colAvail.HeaderText = "AVAILABLE QUANTITY"; colAvail.Width = 150;
            colSubtotal.HeaderText = "SUBTOTAL"; colSubtotal.Width = 150;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colProdName, colQty, colSellPrice, colAvail, colSubtotal });

            pnlItems.Controls.AddRange(new Control[] { lblItemTable, dgvItems });
            contentPanel.Controls.Add(pnlItems);

            // TOTALS
            int ty = 660;
            lblSubtotal.Text = "SubTotal:"; lblSubtotal.Location = new Point(1000, ty); lblSubtotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubtotalVal.Text = "₱0.00"; lblSubtotalVal.Location = new Point(1200, ty);
            lblDiscount.Text = "Discount (%):"; lblDiscount.Location = new Point(1000, ty + 40);
            txtDiscount.Location = new Point(1200, ty + 40); txtDiscount.Size = new Size(100, 30); txtDiscount.ReadOnly = true;
            lblShipping.Text = "Shipping Fee:"; lblShipping.Location = new Point(1000, ty + 80);
            txtShipping.Location = new Point(1200, ty + 80); txtShipping.Size = new Size(100, 30); txtShipping.ReadOnly = true;
            lblTotal.Text = "Total:"; lblTotal.Location = new Point(1000, ty + 130); lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalVal.Text = "₱0.00"; lblTotalVal.Location = new Point(1200, ty + 130); lblTotalVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold); lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255);

            contentPanel.Controls.AddRange(new Control[] { lblSubtotal, lblSubtotalVal, lblDiscount, txtDiscount, lblShipping, txtShipping, lblTotal, lblTotalVal });

            // BOTTOM PANEL
            bottomPanel.Location = new Point(0, 800);
            bottomPanel.Size = new Size(1602, 78);
            bottomPanel.BackColor = Color.White;
            bottomPanel.BorderStyle = DashStyle.Solid;
            bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            btnClose.Location = new Point(1330, 20);
            btnClose.Size = new Size(180, 40);
            btnClose.Text = "Close";
            btnClose.Click += btnCancel_Click;

            bottomPanel.Controls.Add(btnClose);

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainPanel);
            Name = "ViewSupplierOrder";

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel;
        private Guna2Panel contentPanel;
        private Guna2Panel bottomPanel;
        private Guna2TabControl tabControl;
        private TabPage tabInfo, tabAddress;
        private Label lblCompany, lblOrderDate, lblPayment, lblEstDate;
        private Guna2TextBox txtCompany, txtOrderDate, txtPayment, txtEstDate;
        private Guna2Panel pnlItems;
        private Label lblItemTable;
        private Guna2DataGridView dgvItems;
        private DataGridViewTextBoxColumn colProdName, colQty, colSellPrice, colAvail, colSubtotal;
        private Label lblSubtotal, lblSubtotalVal, lblDiscount, lblShipping, lblTotal, lblTotalVal;
        private Guna2TextBox txtDiscount, txtShipping;
        private Guna2Button btnClose;
    }
}