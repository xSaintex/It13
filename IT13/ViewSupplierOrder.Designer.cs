// ---------------------------------------------------------------------
// ViewSupplierOrder.designer.cs
// READ-ONLY | Tighter 80px gap | Full consistency
// ---------------------------------------------------------------------
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

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

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new Guna2ShadowPanel();
            this.scrollPanel = new Guna2Panel();
            this.contentPanel = new Guna2Panel();
            this.bottomPanel = new Guna2Panel();
            this.label1 = new Label();

            // SUPPLIER INFO
            this.lblCompany = new Label();
            this.txtCompany = new Guna2TextBox();
            this.lblOrderDate = new Label();
            this.txtOrderDate = new Guna2TextBox();
            this.lblPayment = new Label();
            this.txtPayment = new Guna2TextBox();
            this.lblEstDate = new Label();
            this.txtEstDate = new Guna2TextBox();

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
            this.txtCountry = new Guna2TextBox();

            // ITEM TABLE
            this.pnlItems = new Guna2Panel();
            this.lblItemTable = new Label();
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
            this.txtDiscount = new Guna2TextBox();
            this.lblShipping = new Label();
            this.txtShipping = new Guna2TextBox();
            this.lblTotal = new Label();
            this.lblTotalVal = new Label();

            // BUTTON
            this.btnClose = new Guna2Button();

            this.mainPanel.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
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
            this.label1.Text = "View Supplier Order";
            this.label1.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            this.label1.Location = new Point(77, 20);
            this.label1.AutoSize = true;

            // === SUPPLIER INFO (Tighter 80px gap) ===
            int y = 100;

            this.lblCompany.Text = "Company Name";
            this.lblCompany.Location = new Point(77, y);
            this.lblCompany.Font = new Font("Tahoma", 10.2F);
            this.lblCompany.AutoSize = true;

            this.txtCompany.Location = new Point(77, y + 25);
            this.txtCompany.Size = new Size(460, 36);
            this.txtCompany.BorderRadius = 5;
            this.txtCompany.ReadOnly = true;

            this.lblOrderDate.Text = "Order Date";
            this.lblOrderDate.Location = new Point(600, y);
            this.lblOrderDate.Font = new Font("Tahoma", 10.2F);
            this.lblOrderDate.AutoSize = true;

            this.txtOrderDate.Location = new Point(600, y + 25);
            this.txtOrderDate.Size = new Size(250, 36);
            this.txtOrderDate.ReadOnly = true;

            this.lblPayment.Text = "Payment Terms";
            this.lblPayment.Location = new Point(77, y + 80);
            this.lblPayment.Font = new Font("Tahoma", 10.2F);
            this.lblPayment.AutoSize = true;

            this.txtPayment.Location = new Point(77, y + 105);
            this.txtPayment.Size = new Size(460, 36);
            this.txtPayment.ReadOnly = true;

            this.lblEstDate.Text = "Estimated Date";
            this.lblEstDate.Location = new Point(600, y + 80);
            this.lblEstDate.Font = new Font("Tahoma", 10.2F);
            this.lblEstDate.AutoSize = true;

            this.txtEstDate.Location = new Point(600, y + 105);
            this.txtEstDate.Size = new Size(250, 36);
            this.txtEstDate.ReadOnly = true;

            // === ADDRESS FIELDS ===
            int ay = y + 180;

            this.lblAddr1.Text = "Address Line 1";
            this.lblAddr1.Location = new Point(77, ay);
            this.lblAddr1.Font = new Font("Tahoma", 10.2F);
            this.lblAddr1.AutoSize = true;

            this.txtAddr1.Location = new Point(77, ay + 25);
            this.txtAddr1.Size = new Size(1300, 36);
            this.txtAddr1.BorderRadius = 5;
            this.txtAddr1.ReadOnly = true;

            this.lblAddr2.Text = "Address Line 2";
            this.lblAddr2.Location = new Point(77, ay + 80);
            this.lblAddr2.Font = new Font("Tahoma", 10.2F);
            this.lblAddr2.AutoSize = true;

            this.txtAddr2.Location = new Point(77, ay + 105);
            this.txtAddr2.Size = new Size(1300, 36);
            this.txtAddr2.BorderRadius = 5;
            this.txtAddr2.ReadOnly = true;

            this.lblCity.Text = "City / District";
            this.lblCity.Location = new Point(77, ay + 160);
            this.lblCity.Font = new Font("Tahoma", 10.2F);
            this.lblCity.AutoSize = true;

            this.txtCity.Location = new Point(77, ay + 185);
            this.txtCity.Size = new Size(400, 36);
            this.txtCity.BorderRadius = 5;
            this.txtCity.ReadOnly = true;

            this.lblState.Text = "State / Province";
            this.lblState.Location = new Point(497, ay + 160);
            this.lblState.Font = new Font("Tahoma", 10.2F);
            this.lblState.AutoSize = true;

            this.txtState.Location = new Point(497, ay + 185);
            this.txtState.Size = new Size(400, 36);
            this.txtState.BorderRadius = 5;
            this.txtState.ReadOnly = true;

            this.lblPostal.Text = "Postal Code";
            this.lblPostal.Location = new Point(917, ay + 160);
            this.lblPostal.Font = new Font("Tahoma", 10.2F);
            this.lblPostal.AutoSize = true;

            this.txtPostal.Location = new Point(917, ay + 185);
            this.txtPostal.Size = new Size(200, 36);
            this.txtPostal.BorderRadius = 5;
            this.txtPostal.ReadOnly = true;

            this.lblCountry.Text = "Country";
            this.lblCountry.Location = new Point(1137, ay + 160);
            this.lblCountry.Font = new Font("Tahoma", 10.2F);
            this.lblCountry.AutoSize = true;

            this.txtCountry.Location = new Point(1137, ay + 185);
            this.txtCountry.Size = new Size(240, 36);
            this.txtCountry.BorderRadius = 5;
            this.txtCountry.ReadOnly = true;

            // === ITEM PANEL ===
            this.pnlItems.Location = new Point(77, ay + 260);
            this.pnlItems.Size = new Size(1458, 380);
            this.pnlItems.FillColor = Color.WhiteSmoke;

            this.lblItemTable.Text = "Item Table";
            this.lblItemTable.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblItemTable.Location = new Point(20, 15);
            this.lblItemTable.AutoSize = true;

            this.dgvItems.Location = new Point(20, 60);
            this.dgvItems.Size = new Size(1418, 280);
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.ReadOnly = true;
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
            this.lblSubtotal.Text = "SubTotal:"; this.lblSubtotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold); this.lblSubtotal.Location = new Point(1000, ty); this.lblSubtotal.AutoSize = true;
            this.lblSubtotalVal.Text = "₱0.00"; this.lblSubtotalVal.Location = new Point(1200, ty); this.lblSubtotalVal.AutoSize = true;

            this.lblDiscount.Text = "Discount (%):"; this.lblDiscount.Location = new Point(1000, ty + 40); this.lblDiscount.AutoSize = true;
            this.txtDiscount.Location = new Point(1200, ty + 40); this.txtDiscount.Size = new Size(100, 30); this.txtDiscount.ReadOnly = true;

            this.lblShipping.Text = "Shipping Fee:"; this.lblShipping.Location = new Point(1000, ty + 80); this.lblShipping.AutoSize = true;
            this.txtShipping.Location = new Point(1200, ty + 80); this.txtShipping.Size = new Size(100, 30); this.txtShipping.ReadOnly = true;

            this.lblTotal.Text = "Total:"; this.lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold); this.lblTotal.Location = new Point(1000, ty + 130); this.lblTotal.AutoSize = true;
            this.lblTotalVal.Text = "₱0.00"; this.lblTotalVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold); this.lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255); this.lblTotalVal.Location = new Point(1200, ty + 130); this.lblTotalVal.AutoSize = true;

            // === BOTTOM PANEL ===
            this.bottomPanel.Location = new Point(0, 800);
            this.bottomPanel.Size = new Size(1602, 78);
            this.bottomPanel.BackColor = Color.White;
            this.bottomPanel.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(1330, 20);
            this.btnClose.Size = new Size(180, 40);
            this.btnClose.BorderRadius = 8;

            // === ADD CONTROLS ===
            this.contentPanel.Controls.AddRange(new Control[] {
                this.label1,
                this.lblCompany, this.txtCompany,
                this.lblOrderDate, this.txtOrderDate,
                this.lblPayment, this.txtPayment,
                this.lblEstDate, this.txtEstDate,
                this.lblAddr1, this.txtAddr1,
                this.lblAddr2, this.txtAddr2,
                this.lblCity, this.txtCity,
                this.lblState, this.txtState,
                this.lblPostal, this.txtPostal,
                this.lblCountry, this.txtCountry,
                this.pnlItems,
                this.lblSubtotal, this.lblSubtotalVal,
                this.lblDiscount, this.txtDiscount,
                this.lblShipping, this.txtShipping,
                this.lblTotal, this.lblTotalVal
            });

            this.pnlItems.Controls.AddRange(new Control[] {
                this.lblItemTable, this.dgvItems
            });

            this.bottomPanel.Controls.Add(this.btnClose);

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainPanel);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "View Supplier Order";

            this.mainPanel.ResumeLayout(false);
            this.scrollPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel;
        private Guna2Panel contentPanel;
        private Guna2Panel bottomPanel;
        private Label label1;

        private Label lblCompany; private Guna2TextBox txtCompany;
        private Label lblOrderDate; private Guna2TextBox txtOrderDate;
        private Label lblPayment; private Guna2TextBox txtPayment;
        private Label lblEstDate; private Guna2TextBox txtEstDate;

        private Label lblAddr1; private Guna2TextBox txtAddr1;
        private Label lblAddr2; private Guna2TextBox txtAddr2;
        private Label lblCity; private Guna2TextBox txtCity;
        private Label lblState; private Guna2TextBox txtState;
        private Label lblPostal; private Guna2TextBox txtPostal;
        private Label lblCountry; private Guna2TextBox txtCountry;

        private Guna2Panel pnlItems;
        private Label lblItemTable;
        private Guna2DataGridView dgvItems;
        private DataGridViewTextBoxColumn colProdName, colQty, colSellPrice, colAvail, colSubtotal;

        private Label lblSubtotal, lblSubtotalVal, lblDiscount, lblShipping, lblTotal, lblTotalVal;
        private Guna2TextBox txtDiscount, txtShipping;

        private Guna2Button btnClose;
    }
}