// AddCustomerOrder.Designer.cs
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class AddCustomerOrder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new Guna2ShadowPanel();
            this.scrollPanel = new Guna2Panel();
            this.contentPanel = new Guna2Panel();
            this.bottomPanel = new Guna2Panel();
            this.label1 = new Label();
            this.label2 = new Label();

            // CUSTOMER INFO
            this.lblCompany = new Label();
            this.cmbCompany = new Guna2ComboBox();
            this.lblOrderDate = new Label();
            this.dateOrder = new Guna2DateTimePicker();
            this.lblPayment = new Label();
            this.cmbPayment = new Guna2ComboBox();
            this.lblEstDate = new Label();
            this.dateEstimated = new Guna2DateTimePicker();

            // ADDRESS (MessageBox Style)
            this.lblBillingAddress = new Label();
            this.txtBillingAddress = new Guna2TextBox();
            this.lblShippingAddress = new Label();
            this.txtShippingAddress = new Guna2TextBox();

            // ITEM TABLE
            this.pnlItems = new Guna2Panel();
            this.lblItemTable = new Label();
            this.txtSearchProduct = new Guna2TextBox();
            this.btnSearch = new Guna2Button();
            this.btnAddProduct = new Guna2Button();
            this.dgvItems = new Guna2DataGridView();
            this.colProdName = new DataGridViewTextBoxColumn();
            this.colQty = new DataGridViewTextBoxColumn();
            this.colSellPrice = new DataGridViewTextBoxColumn();
            this.colAvail = new DataGridViewTextBoxColumn();
            this.colSubtotal = new DataGridViewTextBoxColumn();

            // TOTALS
            this.lblSubtotal = new Label();
            this.lblSubtotalVal = new Label();
            this.lblDiscount = new Label();
            this.numDiscount = new Guna2NumericUpDown();
            this.lblShipping = new Label();
            this.numShipping = new Guna2NumericUpDown();
            this.lblTotal = new Label();
            this.lblTotalVal = new Label();

            // BUTTONS
            this.btnCancel = new Guna2Button();
            this.btnSave = new Guna2Button();

            this.mainPanel.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShipping)).BeginInit();
            this.SuspendLayout();

            // === MAIN PANEL ===
            this.mainPanel.Location = new Point(300, 88);
            this.mainPanel.Size = new Size(1602, 878);
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.Radius = 8;
            this.mainPanel.Controls.Add(this.scrollPanel);
            this.mainPanel.Controls.Add(this.bottomPanel);

            // === SCROLL PANEL ===
            this.scrollPanel.Location = new Point(0, 0);
            this.scrollPanel.Size = new Size(1602, 800);
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.contentPanel);

            // === CONTENT PANEL ===
            this.contentPanel.Location = new Point(0, 0);
            this.contentPanel.Size = new Size(1458, 1350);

            // === HEADER ===
            this.label1.Text = "New Customer Order";
            this.label1.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            this.label1.Location = new Point(77, 20);
            this.label1.AutoSize = true;

            this.label2.Text = "Fields marked with an asterisk (*) are required.";
            this.label2.ForeColor = Color.Red;
            this.label2.Font = new Font("Tahoma", 9F);
            this.label2.Location = new Point(80, 56);
            this.label2.AutoSize = true;

            // === CUSTOMER INFO ===
            int y = 100;
            this.lblCompany.Text = "Company Name *";
            this.lblCompany.Location = new Point(77, y);
            this.lblCompany.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblCompany.AutoSize = true;
            this.cmbCompany.Location = new Point(77, y + 25);
            this.cmbCompany.Size = new Size(460, 36);
            this.cmbCompany.BorderRadius = 5;
            this.cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.cmbCompany.ForeColor = Color.Black;

            this.lblOrderDate.Text = "Order Date *";
            this.lblOrderDate.Location = new Point(600, y);
            this.lblOrderDate.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblOrderDate.AutoSize = true;
            this.dateOrder.Location = new Point(600, y + 25);
            this.dateOrder.Size = new Size(250, 36);
            this.dateOrder.FillColor = Color.White;
            this.dateOrder.Format = DateTimePickerFormat.Custom;
            this.dateOrder.CustomFormat = "MMMM dd, yyyy";
            this.dateOrder.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.dateOrder.ForeColor = Color.Black; 

            this.lblPayment.Text = "Payment Terms *";
            this.lblPayment.Location = new Point(77, y + 80);
            this.lblPayment.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblPayment.AutoSize = true;
            this.cmbPayment.Location = new Point(77, y + 105);
            this.cmbPayment.Size = new Size(460, 36);
            this.cmbPayment.BorderRadius = 5;
            this.cmbPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPayment.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.cmbPayment.ForeColor = Color.Black;

            this.lblEstDate.Text = "Delivery Date *";
            this.lblEstDate.Location = new Point(600, y + 80);
            this.lblEstDate.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblEstDate.AutoSize = true;
            this.dateEstimated.Location = new Point(600, y + 105);
            this.dateEstimated.Size = new Size(250, 36);
            this.dateEstimated.FillColor = Color.White;
            this.dateEstimated.Format = DateTimePickerFormat.Custom;
            this.dateEstimated.CustomFormat = "MMMM dd, yyyy";
            this.dateEstimated.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.dateEstimated.ForeColor = Color.Black; 

            // === ADDRESS FIELDS (MessageBox Style) ===
            int ay = y + 180;
            this.lblBillingAddress.Text = "Billing Address *";
            this.lblBillingAddress.Location = new Point(77, ay);
            this.lblBillingAddress.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblBillingAddress.AutoSize = true;
            this.txtBillingAddress.Location = new Point(77, ay + 25);
            this.txtBillingAddress.Size = new Size(1300, 70);
            this.txtBillingAddress.Multiline = true;
            this.txtBillingAddress.BorderRadius = 8;
            this.txtBillingAddress.PlaceholderText = "Click to enter full billing address...";
            this.txtBillingAddress.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.txtBillingAddress.BackColor = Color.FromArgb(248, 250, 252);
            this.txtBillingAddress.ForeColor = Color.Black;

            this.lblShippingAddress.Text = "Shipping Address * (Leave blank to use Billing Address)";
            this.lblShippingAddress.Location = new Point(77, ay + 120);
            this.lblShippingAddress.Font = new Font("Bahnschrift SemiCondensed", 10.2F);
            this.lblShippingAddress.AutoSize = true;
            this.txtShippingAddress.Location = new Point(77, ay + 145);
            this.txtShippingAddress.Size = new Size(1300, 70);
            this.txtShippingAddress.Multiline = true;
            this.txtShippingAddress.BorderRadius = 8;
            this.txtShippingAddress.PlaceholderText = "Click to enter shipping address (optional)...";
            this.txtShippingAddress.Font = new Font("Bahnschrift SemiCondensed", 10F);
            this.txtShippingAddress.BackColor = Color.FromArgb(248, 250, 252);
            this.txtShippingAddress.ForeColor = Color.Black;

            // === ITEM PANEL ===
            this.pnlItems.Location = new Point(77, ay + 250);
            this.pnlItems.Size = new Size(1458, 380);
            this.pnlItems.FillColor = Color.WhiteSmoke;

            this.lblItemTable.Text = "Item Table";
            this.lblItemTable.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblItemTable.Location = new Point(20, 15);
            this.lblItemTable.AutoSize = true;

            this.txtSearchProduct.Location = new Point(20, 50);
            this.txtSearchProduct.Size = new Size(300, 36);
            this.txtSearchProduct.PlaceholderText = "Search by product name...";
            this.txtSearchProduct.BorderRadius = 5;
            this.txtSearchProduct.Font = new Font("Poppins", 10F);

            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new Point(330, 50);
            this.btnSearch.Size = new Size(90, 36);
            this.btnSearch.BorderRadius = 5;

            this.btnAddProduct.Text = "+ Add Product";
            this.btnAddProduct.Location = new Point(1200, 50);
            this.btnAddProduct.Size = new Size(150, 36);
            this.btnAddProduct.BorderRadius = 5;

            this.dgvItems.Location = new Point(20, 100);
            this.dgvItems.Size = new Size(1340, 200);
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(12, 57, 101);
            this.dgvItems.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            this.dgvItems.ThemeStyle.HeaderStyle.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);

            this.colProdName.HeaderText = "PRODUCT NAME"; this.colProdName.Width = 400;
            this.colQty.HeaderText = "QUANTITY"; this.colQty.Width = 120;
            this.colSellPrice.HeaderText = "SELLING PRICE"; this.colSellPrice.Width = 150;
            this.colAvail.HeaderText = "AVAILABLE QUANTITY"; this.colAvail.Width = 150;
            this.colSubtotal.HeaderText = "SUBTOTAL"; this.colSubtotal.Width = 150;

            this.dgvItems.Columns.AddRange(new DataGridViewColumn[] {
                this.colProdName, this.colQty, this.colSellPrice, this.colAvail, this.colSubtotal
            });

            // === TOTALS ===
            int ty = ay + 650;
            this.lblSubtotal.Text = "SubTotal:";
            this.lblSubtotal.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            this.lblSubtotal.Location = new Point(1000, ty);
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotalVal.Text = "₱0.00";
            this.lblSubtotalVal.Location = new Point(1200, ty);
            this.lblSubtotalVal.AutoSize = true;

            this.lblDiscount.Text = "Discount (%):";
            this.lblDiscount.Location = new Point(1000, ty + 40);
            this.numDiscount.Location = new Point(1200, ty + 40);
            this.numDiscount.Size = new Size(100, 30);

            this.lblShipping.Text = "Shipping Fee:";
            this.lblShipping.Location = new Point(1000, ty + 80);
            this.numShipping.Location = new Point(1200, ty + 80);
            this.numShipping.Size = new Size(100, 30);

            this.lblTotal.Text = "Total:";
            this.lblTotal.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.lblTotal.Location = new Point(1000, ty + 130);
            this.lblTotalVal.Text = "₱0.00";
            this.lblTotalVal.Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold);
            this.lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255);
            this.lblTotalVal.Location = new Point(1200, ty + 130);

            // === BOTTOM PANEL ===
            this.bottomPanel.Location = new Point(0, 800);
            this.bottomPanel.Size = new Size(1602, 78);
            this.bottomPanel.BackColor = Color.White;
            this.bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new Point(1200, 20);
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.BorderRadius = 8;

            this.btnSave.Text = "Save Customer Order";
            this.btnSave.Location = new Point(1330, 20);
            this.btnSave.Size = new Size(180, 40);
            this.btnSave.BorderRadius = 8;

            // === ADD CONTROLS ===
            this.contentPanel.Controls.AddRange(new Control[] {
                this.label1, this.label2,
                this.lblCompany, this.cmbCompany,
                this.lblOrderDate, this.dateOrder,
                this.lblPayment, this.cmbPayment,
                this.lblEstDate, this.dateEstimated,
                this.lblBillingAddress, this.txtBillingAddress,
                this.lblShippingAddress, this.txtShippingAddress,
                this.pnlItems,
                this.lblSubtotal, this.lblSubtotalVal,
                this.lblDiscount, this.numDiscount,
                this.lblShipping, this.numShipping,
                this.lblTotal, this.lblTotalVal
            });

            this.pnlItems.Controls.AddRange(new Control[] {
                this.lblItemTable, this.txtSearchProduct, this.btnSearch,
                this.btnAddProduct, this.dgvItems
            });

            this.bottomPanel.Controls.AddRange(new Control[] { this.btnCancel, this.btnSave });

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainPanel);
            this.Text = "Add Customer Order";

            this.mainPanel.ResumeLayout(false);
            this.scrollPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShipping)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel;
        private Guna2Panel contentPanel;
        private Guna2Panel bottomPanel;
        private Label label1;
        private Label label2;
        private Label lblCompany;
        private Guna2ComboBox cmbCompany;
        private Label lblOrderDate;
        private Guna2DateTimePicker dateOrder;
        private Label lblPayment;
        private Guna2ComboBox cmbPayment;
        private Label lblEstDate;
        private Guna2DateTimePicker dateEstimated;
        private Label lblBillingAddress;
        private Guna2TextBox txtBillingAddress;
        private Label lblShippingAddress;
        private Guna2TextBox txtShippingAddress;
        private Guna2Panel pnlItems;
        private Label lblItemTable;
        private Guna2TextBox txtSearchProduct;
        private Guna2Button btnSearch;
        private Guna2Button btnAddProduct;
        private Guna2DataGridView dgvItems;
        private DataGridViewTextBoxColumn colProdName;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colSellPrice;
        private DataGridViewTextBoxColumn colAvail;
        private DataGridViewTextBoxColumn colSubtotal;
        private Label lblSubtotal;
        private Label lblSubtotalVal;
        private Label lblDiscount;
        private Guna2NumericUpDown numDiscount;
        private Label lblShipping;
        private Guna2NumericUpDown numShipping;
        private Label lblTotal;
        private Label lblTotalVal;
        private Guna2Button btnCancel;
        private Guna2Button btnSave;
    }
}