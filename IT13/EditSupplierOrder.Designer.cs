// ---------------------------------------------------------------------
// EditSupplierOrder.designer.cs
// UPDATED: Tighter 80px gap | Full consistency
// ---------------------------------------------------------------------
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class EditSupplierOrder
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

            // SUPPLIER INFO
            this.lblCompany = new Label();
            this.cmbCompany = new Guna2ComboBox();
            this.lblOrderDate = new Label();
            this.dateOrder = new Guna2DateTimePicker();
            this.lblPayment = new Label();
            this.cmbPayment = new Guna2ComboBox();
            this.lblEstDate = new Label();
            this.dateEstimated = new Guna2DateTimePicker();

            // ADDRESS
            this.lblAddr1 = new Label();
            this.txtAddr1 = new Guna2TextBox();
            this.lblAddr2 = new Label();
            this.txtAddr2 = new Guna2TextBox();
            this.lblCity = new Label();
            this.txtCity = new Guna2TextBox();
            this.lblState = new Label();
            this.txtState = new Guna2TextBox();
            this.lblPostal = new Label();
            this.txtPostal = new Guna2TextBox();
            this.lblCountry = new Label();
            this.cmbCountry = new Guna2ComboBox();

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
            this.contentPanel.AutoSize = false;

            // === HEADER ===
            this.label1.Text = "Edit Supplier Order";
            this.label1.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            this.label1.Location = new Point(77, 20);
            this.label1.AutoSize = true;

            this.label2.Text = "Fields marked with an asterisk (*) are required.";
            this.label2.ForeColor = Color.Red;
            this.label2.Font = new Font("Tahoma", 9F);
            this.label2.Location = new Point(84, 56);
            this.label2.AutoSize = true;

            // === SUPPLIER INFO (Tighter 80px gap) ===
            int y = 100;

            this.lblCompany.Text = "Company Name *";
            this.lblCompany.Location = new Point(77, y);
            this.lblCompany.Font = new Font("Tahoma", 10.2F);
            this.lblCompany.AutoSize = true;
            this.lblCompany.MaximumSize = new Size(500, 0);

            this.cmbCompany.Location = new Point(77, y + 25);
            this.cmbCompany.Size = new Size(460, 36);
            this.cmbCompany.BorderRadius = 5;
            this.cmbCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new Font("Segoe UI", 10F);
            this.cmbCompany.ForeColor = Color.FromArgb(68, 88, 112);

            this.lblOrderDate.Text = "Order Date *";
            this.lblOrderDate.Location = new Point(600, y);
            this.lblOrderDate.Font = new Font("Tahoma", 10.2F);
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.MaximumSize = new Size(300, 0);

            this.dateOrder.Location = new Point(600, y + 25);
            this.dateOrder.Size = new Size(250, 36);
            this.dateOrder.FillColor = Color.White;
            this.dateOrder.Format = DateTimePickerFormat.Custom;
            this.dateOrder.CustomFormat = "MMMM dd, yyyy";
            this.dateOrder.Font = new Font("Segoe UI", 9F);

            this.lblPayment.Text = "Payment Terms *";
            this.lblPayment.Location = new Point(77, y + 80);
            this.lblPayment.Font = new Font("Tahoma", 10.2F);
            this.lblPayment.AutoSize = true;
            this.lblPayment.MaximumSize = new Size(500, 0);

            this.cmbPayment.Location = new Point(77, y + 105);
            this.cmbPayment.Size = new Size(460, 36);
            this.cmbPayment.BorderRadius = 5;
            this.cmbPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPayment.Font = new Font("Segoe UI", 10F);
            this.cmbPayment.ForeColor = Color.FromArgb(68, 88, 112);

            this.lblEstDate.Text = "Estimated Date *";
            this.lblEstDate.Location = new Point(600, y + 80);
            this.lblEstDate.Font = new Font("Tahoma", 10.2F);
            this.lblEstDate.AutoSize = true;
            this.lblEstDate.MaximumSize = new Size(300, 0);

            this.dateEstimated.Location = new Point(600, y + 105);
            this.dateEstimated.Size = new Size(250, 36);
            this.dateEstimated.FillColor = Color.White;
            this.dateEstimated.Format = DateTimePickerFormat.Custom;
            this.dateEstimated.CustomFormat = "MMMM dd, yyyy";
            this.dateEstimated.Font = new Font("Segoe UI", 9F);

            // === ADDRESS FIELDS ===
            int ay = y + 180;

            this.lblAddr1.Text = "Address Line 1 *";
            this.lblAddr1.Location = new Point(77, ay);
            this.lblAddr1.Font = new Font("Tahoma", 10.2F);
            this.lblAddr1.AutoSize = true;
            this.lblAddr1.MaximumSize = new Size(600, 0);

            this.txtAddr1.Location = new Point(77, ay + 25);
            this.txtAddr1.Size = new Size(1300, 36);
            this.txtAddr1.BorderRadius = 5;
            this.txtAddr1.PlaceholderText = "Street, Building, Unit #";

            this.lblAddr2.Text = "Address Line 2";
            this.lblAddr2.Location = new Point(77, ay + 80);
            this.lblAddr2.Font = new Font("Tahoma", 10.2F);
            this.lblAddr2.AutoSize = true;
            this.lblAddr2.MaximumSize = new Size(600, 0);

            this.txtAddr2.Location = new Point(77, ay + 105);
            this.txtAddr2.Size = new Size(1300, 36);
            this.txtAddr2.BorderRadius = 5;
            this.txtAddr2.PlaceholderText = "Apartment, Suite, Floor (optional)";

            this.lblCity.Text = "City / District *";
            this.lblCity.Location = new Point(77, ay + 160);
            this.lblCity.Font = new Font("Tahoma", 10.2F);
            this.lblCity.AutoSize = true;
            this.lblCity.MaximumSize = new Size(400, 0);

            this.txtCity.Location = new Point(77, ay + 185);
            this.txtCity.Size = new Size(400, 36);
            this.txtCity.BorderRadius = 5;

            this.lblState.Text = "State / Province *";
            this.lblState.Location = new Point(497, ay + 160);
            this.lblState.Font = new Font("Tahoma", 10.2F);
            this.lblState.AutoSize = true;
            this.lblState.MaximumSize = new Size(400, 0);

            this.txtState.Location = new Point(497, ay + 185);
            this.txtState.Size = new Size(400, 36);
            this.txtState.BorderRadius = 5;

            this.lblPostal.Text = "Postal Code *";
            this.lblPostal.Location = new Point(917, ay + 160);
            this.lblPostal.Font = new Font("Tahoma", 10.2F);
            this.lblPostal.AutoSize = true;
            this.lblPostal.MaximumSize = new Size(200, 0);

            this.txtPostal.Location = new Point(917, ay + 185);
            this.txtPostal.Size = new Size(200, 36);
            this.txtPostal.BorderRadius = 5;
            this.txtPostal.MaxLength = 10;

            this.lblCountry.Text = "Country *";
            this.lblCountry.Location = new Point(1137, ay + 160);
            this.lblCountry.Font = new Font("Tahoma", 10.2F);
            this.lblCountry.AutoSize = true;
            this.lblCountry.MaximumSize = new Size(240, 0);

            this.cmbCountry.Location = new Point(1137, ay + 185);
            this.cmbCountry.Size = new Size(240, 36);
            this.cmbCountry.BorderRadius = 5;
            this.cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCountry.Font = new Font("Segoe UI", 10F);

            // === ITEM PANEL ===
            this.pnlItems.Location = new Point(77, ay + 260);
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

            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new Point(330, 50);
            this.btnSearch.Size = new Size(90, 36);
            this.btnSearch.BorderRadius = 5;

            this.btnAddProduct.Text = "+ Add Product";
            this.btnAddProduct.Location = new Point(1200, 50);
            this.btnAddProduct.Size = new Size(150, 36);
            this.btnAddProduct.BorderRadius = 5;

            this.dgvItems.Location = new Point(20, 100);
            this.dgvItems.Size = new Size(1418, 200);
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            this.dgvItems.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            this.dgvItems.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            this.colProdName.HeaderText = "PRODUCT NAME"; this.colProdName.Width = 400;
            this.colQty.HeaderText = "QUANTITY"; this.colQty.Width = 120;
            this.colSellPrice.HeaderText = "SELLING PRICE"; this.colSellPrice.Width = 150;
            this.colAvail.HeaderText = "AVAILABLE QUANTITY"; this.colAvail.Width = 150;
            this.colSubtotal.HeaderText = "SUBTOTAL"; this.colSubtotal.Width = 150;

            this.dgvItems.Columns.AddRange(new DataGridViewColumn[] {
                this.colProdName, this.colQty, this.colSellPrice, this.colAvail, this.colSubtotal
            });

            // === TOTALS ===
            int ty = ay + 660;
            this.lblSubtotal.Text = "SubTotal:";
            this.lblSubtotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSubtotal.Location = new Point(1000, ty);
            this.lblSubtotal.AutoSize = true;

            this.lblSubtotalVal.Text = "₱0.00";
            this.lblSubtotalVal.Location = new Point(1200, ty);
            this.lblSubtotalVal.AutoSize = true;

            this.lblDiscount.Text = "Discount (%):";
            this.lblDiscount.Location = new Point(1000, ty + 40);
            this.lblDiscount.AutoSize = true;

            this.numDiscount.Location = new Point(1200, ty + 40);
            this.numDiscount.Size = new Size(100, 30);

            this.lblShipping.Text = "Shipping Fee:";
            this.lblShipping.Location = new Point(1000, ty + 80);
            this.lblShipping.AutoSize = true;

            this.numShipping.Location = new Point(1200, ty + 80);
            this.numShipping.Size = new Size(100, 30);

            this.lblTotal.Text = "Total:";
            this.lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotal.Location = new Point(1000, ty + 130);
            this.lblTotal.AutoSize = true;

            this.lblTotalVal.Text = "₱0.00";
            this.lblTotalVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255);
            this.lblTotalVal.Location = new Point(1200, ty + 130);
            this.lblTotalVal.AutoSize = true;

            // === BOTTOM PANEL ===
            this.bottomPanel.Location = new Point(0, 800);
            this.bottomPanel.Size = new Size(1602, 78);
            this.bottomPanel.BackColor = Color.White;
            this.bottomPanel.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new Point(1200, 20);
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.BorderRadius = 8;

            this.btnSave.Text = "Update Supplier Order";
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
                this.lblAddr1, this.txtAddr1,
                this.lblAddr2, this.txtAddr2,
                this.lblCity, this.txtCity,
                this.lblState, this.txtState,
                this.lblPostal, this.txtPostal,
                this.lblCountry, this.cmbCountry,
                this.pnlItems,
                this.lblSubtotal, this.lblSubtotalVal,
                this.lblDiscount, this.numDiscount,
                this.lblShipping, this.numShipping,
                this.lblTotal, this.lblTotalVal
            });

            this.pnlItems.Controls.AddRange(new Control[] {
                this.lblItemTable, this.txtSearchProduct, this.btnSearch, this.btnAddProduct, this.dgvItems
            });

            this.bottomPanel.Controls.AddRange(new Control[] { this.btnCancel, this.btnSave });

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainPanel);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "Edit Supplier Order";

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

        private Label lblAddr1;
        private Guna2TextBox txtAddr1;
        private Label lblAddr2;
        private Guna2TextBox txtAddr2;
        private Label lblCity;
        private Guna2TextBox txtCity;
        private Label lblState;
        private Guna2TextBox txtState;
        private Label lblPostal;
        private Guna2TextBox txtPostal;
        private Label lblCountry;
        private Guna2ComboBox cmbCountry;

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