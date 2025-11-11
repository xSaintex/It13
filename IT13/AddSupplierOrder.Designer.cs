using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class AddSupplierOrder
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
            // linkBack REMOVED
            lblCompany = new Label();
            cmbCompany = new Guna2ComboBox();
            lblOrderDate = new Label();
            dateOrder = new Guna2DateTimePicker();
            lblPayment = new Label();
            cmbPayment = new Guna2ComboBox();
            lblEstDate = new Label();
            dateEstimated = new Guna2DateTimePicker();
            pnlItems = new Guna2Panel();
            lblItemTable = new Label();
            txtSearchProduct = new Guna2TextBox();
            btnAddProduct = new Guna2Button();
            dgvItems = new Guna2DataGridView();
            colProdName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colSellPrice = new DataGridViewTextBoxColumn();
            colAvail = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            lblSubtotal = new Label();
            lblSubtotalVal = new Label();
            lblDiscount = new Label();
            numDiscount = new Guna2NumericUpDown();
            lblShipping = new Label();
            numShipping = new Guna2NumericUpDown();
            lblTotal = new Label();
            lblTotalVal = new Label();
            btnCancel = new Guna2Button();
            btnSave = new Guna2Button();

            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            tabControl.SuspendLayout();
            pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShipping).BeginInit();
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

            // TAB CONTROL (Now starts at top)
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
            // COMPANY NAME
            lblCompany.Text = "Company Name *";
            lblCompany.Location = new Point(40, y);
            cmbCompany.Location = new Point(40, y + 25);
            cmbCompany.Size = new Size(460, 36); // ← 460px

            // ORDER DATE
            lblOrderDate.Text = "Order Date *";
            lblOrderDate.Location = new Point(520, y);
            dateOrder.Location = new Point(520, y + 25);
            dateOrder.Size = new Size(250, 36);
            dateOrder.FillColor = Color.White;

            // PAYMENT TERMS
            lblPayment.Text = "Payment Terms *";
            lblPayment.Location = new Point(40, y + 80);
            cmbPayment.Location = new Point(40, y + 105);
            cmbPayment.Size = new Size(460, 36); // ← FIXED: now 460px

            // ESTIMATED DATE
            lblEstDate.Text = "Estimated Date *";
            lblEstDate.Location = new Point(520, y + 80);
            dateEstimated.Location = new Point(520, y + 105);
            dateEstimated.Size = new Size(250, 36);
            dateEstimated.FillColor = Color.White;

            tabInfo.Controls.AddRange(new Control[]
            {
            lblCompany, cmbCompany,
            lblOrderDate, dateOrder,
            lblPayment, cmbPayment,
            lblEstDate, dateEstimated
            });

            // ITEM PANEL
            pnlItems.Location = new Point(77, 260);
            pnlItems.Size = new Size(1458, 380);
            pnlItems.FillColor = Color.WhiteSmoke;

            lblItemTable.Text = "Item Table";
            lblItemTable.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblItemTable.Location = new Point(20, 15);

            txtSearchProduct.Location = new Point(20, 50);
            txtSearchProduct.Size = new Size(300, 36);
            txtSearchProduct.PlaceholderText = "Search by product name...";

            btnAddProduct.Location = new Point(1180, 50);
            btnAddProduct.Size = new Size(150, 36);
            btnAddProduct.Text = "+ Add Product";

            dgvItems.Location = new Point(20, 100);
            dgvItems.Size = new Size(1418, 200);
            dgvItems.AllowUserToAddRows = false;
            dgvItems.ColumnHeadersHeight = 40;
            dgvItems.GridColor = Color.FromArgb(231, 229, 255);

            colProdName.HeaderText = "PRODUCT NAME"; colProdName.Width = 400;
            colQty.HeaderText = "QUANTITY"; colQty.Width = 120;
            colSellPrice.HeaderText = "SELLING PRICE"; colSellPrice.Width = 150;
            colAvail.HeaderText = "AVAILABLE QUANTITY"; colAvail.Width = 150;
            colSubtotal.HeaderText = "SUBTOTAL"; colSubtotal.Width = 150;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colProdName, colQty, colSellPrice, colAvail, colSubtotal });

            pnlItems.Controls.AddRange(new Control[] { lblItemTable, txtSearchProduct, btnAddProduct, dgvItems });
            contentPanel.Controls.Add(pnlItems);

            // TOTALS
            int ty = 660;
            lblSubtotal.Text = "SubTotal:"; lblSubtotal.Location = new Point(1000, ty); lblSubtotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubtotalVal.Text = "₱0.00"; lblSubtotalVal.Location = new Point(1200, ty);
            lblDiscount.Text = "Discount (%):"; lblDiscount.Location = new Point(1000, ty + 40);
            numDiscount.Location = new Point(1200, ty + 40); numDiscount.Size = new Size(100, 30);
            lblShipping.Text = "Shipping Fee:"; lblShipping.Location = new Point(1000, ty + 80);
            numShipping.Location = new Point(1200, ty + 80); numShipping.Size = new Size(100, 30);
            lblTotal.Text = "Total:"; lblTotal.Location = new Point(1000, ty + 130); lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalVal.Text = "₱0.00"; lblTotalVal.Location = new Point(1200, ty + 130); lblTotalVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold); lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255);

            contentPanel.Controls.AddRange(new Control[] { lblSubtotal, lblSubtotalVal, lblDiscount, numDiscount, lblShipping, numShipping, lblTotal, lblTotalVal });

            // BOTTOM PANEL
            bottomPanel.Location = new Point(0, 800);
            bottomPanel.Size = new Size(1602, 78);
            bottomPanel.BackColor = Color.White;
            bottomPanel.BorderStyle = DashStyle.Solid;
            bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            btnCancel.Location = new Point(1200, 20);
            btnCancel.Size = new Size(120, 40);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            btnSave.Location = new Point(1330, 20);
            btnSave.Size = new Size(180, 40);
            btnSave.Text = "Save Supplier Order";
            btnSave.Click += btnSave_Click;

            bottomPanel.Controls.Add(btnCancel);
            bottomPanel.Controls.Add(btnSave);

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainPanel);
            Name = "AddSupplierOrder";

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShipping).EndInit();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel;
        private Guna2Panel contentPanel;
        private Guna2Panel bottomPanel;
        private Guna2TabControl tabControl;
        private TabPage tabInfo, tabAddress;
        // private LinkLabel linkBack; → REMOVED
        private Label lblCompany, lblOrderDate, lblPayment, lblEstDate;
        private Guna2ComboBox cmbCompany, cmbPayment;
        private Guna2DateTimePicker dateOrder, dateEstimated;
        private Guna2Panel pnlItems;
        private Label lblItemTable;
        private Guna2TextBox txtSearchProduct;
        private Guna2Button btnAddProduct;
        private Guna2DataGridView dgvItems;
        private DataGridViewTextBoxColumn colProdName, colQty, colSellPrice, colAvail, colSubtotal;
        private Label lblSubtotal, lblSubtotalVal, lblDiscount, lblShipping, lblTotal, lblTotalVal;
        private Guna2NumericUpDown numDiscount, numShipping;
        private Guna2Button btnCancel, btnSave;
    }
}