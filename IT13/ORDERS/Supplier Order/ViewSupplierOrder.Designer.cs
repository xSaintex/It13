// ---------------------------------------------------------------------
// ViewSupplierOrder.designer.cs
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

            this.lblCompany = new Label(); this.txtCompany = new Guna2TextBox();
            this.lblOrderDate = new Label(); this.txtOrderDate = new Guna2TextBox();
            this.lblPayment = new Label(); this.txtPayment = new Guna2TextBox();
            this.lblEstDate = new Label(); this.txtEstDate = new Guna2TextBox();

            this.lblAddr1 = new Label(); this.txtAddr1 = new Guna2TextBox();
            this.lblAddr2 = new Label(); this.txtAddr2 = new Guna2TextBox();
            this.lblCity = new Label(); this.txtCity = new Guna2TextBox();
            this.lblState = new Label(); this.txtState = new Guna2TextBox();
            this.lblPostal = new Label(); this.txtPostal = new Guna2TextBox();
            this.lblCountry = new Label(); this.txtCountry = new Guna2TextBox();

            this.pnlItems = new Guna2Panel();
            this.lblItemTable = new Label();
            this.dgvItems = new Guna2DataGridView();
            this.colProdName = new DataGridViewTextBoxColumn();
            this.colQty = new DataGridViewTextBoxColumn();
            this.colSellPrice = new DataGridViewTextBoxColumn();
            this.colAvail = new DataGridViewTextBoxColumn();
            this.colSubtotal = new DataGridViewTextBoxColumn();

            this.lblSubtotal = new Label(); this.lblSubtotalVal = new Label();
            this.lblDiscount = new Label(); this.txtDiscount = new Guna2TextBox();
            this.lblShipping = new Label(); this.txtShipping = new Guna2TextBox();
            this.lblTotal = new Label(); this.lblTotalVal = new Label();

            this.btnClose = new Guna2Button();

            this.mainPanel.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();

            this.mainPanel.Location = new Point(300, 88);
            this.mainPanel.Size = new Size(1602, 878);
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.Radius = 8;
            this.mainPanel.Controls.Add(this.scrollPanel);
            this.mainPanel.Controls.Add(this.bottomPanel);

            this.scrollPanel.Location = new Point(0, 0);
            this.scrollPanel.Size = new Size(1602, 800);
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.contentPanel);

            this.contentPanel.Location = new Point(0, 0);
            this.contentPanel.Size = new Size(1458, 1350);

            this.label1.Text = "View Supplier Order";
            this.label1.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            this.label1.Location = new Point(77, 20);
            this.label1.AutoSize = true;

            int y = 100;

            this.lblCompany.Text = "Company Name"; this.lblCompany.Location = new Point(77, y); this.lblCompany.Font = new Font("Poppins", 10.2F); this.lblCompany.ForeColor = Color.Black; this.lblCompany.AutoSize = true;
            this.txtCompany.Location = new Point(77, y + 25); this.txtCompany.Size = new Size(460, 36); this.txtCompany.BorderRadius = 5; this.txtCompany.ReadOnly = true; this.txtCompany.Font = new Font("Poppins", 10F); this.txtCompany.ForeColor = Color.Black;

            this.lblOrderDate.Text = "Order Date"; this.lblOrderDate.Location = new Point(600, y); this.lblOrderDate.Font = new Font("Poppins", 10.2F); this.lblOrderDate.ForeColor = Color.Black; this.lblOrderDate.AutoSize = true;
            this.txtOrderDate.Location = new Point(600, y + 25); this.txtOrderDate.Size = new Size(250, 36); this.txtOrderDate.ReadOnly = true; this.txtOrderDate.Font = new Font("Poppins", 10F); this.txtOrderDate.ForeColor = Color.Black;

            this.lblPayment.Text = "Payment Terms"; this.lblPayment.Location = new Point(77, y + 80); this.lblPayment.Font = new Font("Poppins", 10.2F); this.lblPayment.ForeColor = Color.Black; this.lblPayment.AutoSize = true;
            this.txtPayment.Location = new Point(77, y + 105); this.txtPayment.Size = new Size(460, 36); this.txtPayment.ReadOnly = true; this.txtPayment.Font = new Font("Poppins", 10F); this.txtPayment.ForeColor = Color.Black;

            this.lblEstDate.Text = "Estimated Date"; this.lblEstDate.Location = new Point(600, y + 80); this.lblEstDate.Font = new Font("Poppins", 10.2F); this.lblEstDate.ForeColor = Color.Black; this.lblEstDate.AutoSize = true;
            this.txtEstDate.Location = new Point(600, y + 105); this.txtEstDate.Size = new Size(250, 36); this.txtEstDate.ReadOnly = true; this.txtEstDate.Font = new Font("Poppins", 10F); this.txtEstDate.ForeColor = Color.Black;

            int ay = y + 180;

            this.lblAddr1.Text = "Address Line 1"; this.lblAddr1.Location = new Point(77, ay); this.lblAddr1.Font = new Font("Poppins", 10.2F); this.lblAddr1.ForeColor = Color.Black; this.lblAddr1.AutoSize = true;
            this.txtAddr1.Location = new Point(77, ay + 25); this.txtAddr1.Size = new Size(1300, 36); this.txtAddr1.BorderRadius = 5; this.txtAddr1.ReadOnly = true; this.txtAddr1.Font = new Font("Poppins", 10F); this.txtAddr1.ForeColor = Color.Black;

            this.lblAddr2.Text = "Address Line 2"; this.lblAddr2.Location = new Point(77, ay + 80); this.lblAddr2.Font = new Font("Poppins", 10.2F); this.lblAddr2.ForeColor = Color.Black; this.lblAddr2.AutoSize = true;
            this.txtAddr2.Location = new Point(77, ay + 105); this.txtAddr2.Size = new Size(1300, 36); this.txtAddr2.BorderRadius = 5; this.txtAddr2.ReadOnly = true; this.txtAddr2.Font = new Font("Poppins", 10F); this.txtAddr2.ForeColor = Color.Black;

            this.lblCity.Text = "City / District"; this.lblCity.Location = new Point(77, ay + 160); this.lblCity.Font = new Font("Poppins", 10.2F); this.lblCity.ForeColor = Color.Black; this.lblCity.AutoSize = true;
            this.txtCity.Location = new Point(77, ay + 185); this.txtCity.Size = new Size(400, 36); this.txtCity.BorderRadius = 5; this.txtCity.ReadOnly = true; this.txtCity.Font = new Font("Poppins", 10F); this.txtCity.ForeColor = Color.Black;

            this.lblState.Text = "State / Province"; this.lblState.Location = new Point(497, ay + 160); this.lblState.Font = new Font("Poppins", 10.2F); this.lblState.ForeColor = Color.Black; this.lblState.AutoSize = true;
            this.txtState.Location = new Point(497, ay + 185); this.txtState.Size = new Size(400, 36); this.txtState.BorderRadius = 5; this.txtState.ReadOnly = true; this.txtState.Font = new Font("Poppins", 10F); this.txtState.ForeColor = Color.Black;

            this.lblPostal.Text = "Postal Code"; this.lblPostal.Location = new Point(917, ay + 160); this.lblPostal.Font = new Font("Poppins", 10.2F); this.lblPostal.ForeColor = Color.Black; this.lblPostal.AutoSize = true;
            this.txtPostal.Location = new Point(917, ay + 185); this.txtPostal.Size = new Size(200, 36); this.txtPostal.BorderRadius = 5; this.txtPostal.ReadOnly = true; this.txtPostal.Font = new Font("Poppins", 10F); this.txtPostal.ForeColor = Color.Black;

            this.lblCountry.Text = "Country"; this.lblCountry.Location = new Point(1137, ay + 160); this.lblCountry.Font = new Font("Poppins", 10.2F); this.lblCountry.ForeColor = Color.Black; this.lblCountry.AutoSize = true;
            this.txtCountry.Location = new Point(1137, ay + 185); this.txtCountry.Size = new Size(240, 36); this.txtCountry.BorderRadius = 5; this.txtCountry.ReadOnly = true; this.txtCountry.Font = new Font("Poppins", 10F); this.txtCountry.ForeColor = Color.Black;

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
            this.dgvItems.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(12, 57, 101);
            this.dgvItems.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            this.dgvItems.ThemeStyle.HeaderStyle.Font = new Font("Poppins", 10F, FontStyle.Bold);

            this.colProdName.HeaderText = "PRODUCT NAME"; this.colProdName.Width = 400;
            this.colQty.HeaderText = "QUANTITY"; this.colQty.Width = 120;
            this.colSellPrice.HeaderText = "SELLING PRICE"; this.colSellPrice.Width = 150;
            this.colAvail.HeaderText = "AVAILABLE QUANTITY"; this.colAvail.Width = 150;
            this.colSubtotal.HeaderText = "SUBTOTAL"; this.colSubtotal.Width = 150;
            this.dgvItems.Columns.AddRange(new DataGridViewColumn[] { colProdName, colQty, colSellPrice, colAvail, colSubtotal });

            int ty = ay + 660;

            this.lblSubtotal.Text = "SubTotal:"; this.lblSubtotal.Font = new Font("Poppins", 10F, FontStyle.Bold); this.lblSubtotal.ForeColor = Color.Black; this.lblSubtotal.Location = new Point(1000, ty); this.lblSubtotal.AutoSize = true;
            this.lblSubtotalVal.Text = "₱0.00"; this.lblSubtotalVal.Font = new Font("Poppins", 10F); this.lblSubtotalVal.ForeColor = Color.Black; this.lblSubtotalVal.Location = new Point(1200, ty); this.lblSubtotalVal.AutoSize = true;

            this.lblDiscount.Text = "Discount (%):"; this.lblDiscount.Font = new Font("Poppins", 10F); this.lblDiscount.ForeColor = Color.Black; this.lblDiscount.Location = new Point(1000, ty + 40); this.lblDiscount.AutoSize = true;
            this.txtDiscount.Location = new Point(1200, ty + 40); this.txtDiscount.Size = new Size(100, 30); this.txtDiscount.ReadOnly = true; this.txtDiscount.Font = new Font("Poppins", 10F); this.txtDiscount.ForeColor = Color.Black;

            this.lblShipping.Text = "Shipping Fee:"; this.lblShipping.Font = new Font("Poppins", 10F); this.lblShipping.ForeColor = Color.Black; this.lblShipping.Location = new Point(1000, ty + 80); this.lblShipping.AutoSize = true;
            this.txtShipping.Location = new Point(1200, ty + 80); this.txtShipping.Size = new Size(100, 30); this.txtShipping.ReadOnly = true; this.txtShipping.Font = new Font("Poppins", 10F); this.txtShipping.ForeColor = Color.Black;

            this.lblTotal.Text = "Total:"; this.lblTotal.Font = new Font("Poppins", 12F, FontStyle.Bold); this.lblTotal.ForeColor = Color.Black; this.lblTotal.Location = new Point(1000, ty + 130); this.lblTotal.AutoSize = true;
            this.lblTotalVal.Text = "₱0.00"; this.lblTotalVal.Font = new Font("Poppins", 12F, FontStyle.Bold); this.lblTotalVal.ForeColor = Color.FromArgb(0, 123, 255); this.lblTotalVal.Location = new Point(1200, ty + 130); this.lblTotalVal.AutoSize = true;

            this.bottomPanel.Location = new Point(0, 800);
            this.bottomPanel.Size = new Size(1602, 78);
            this.bottomPanel.BackColor = Color.White;
            this.bottomPanel.BorderColor = Color.FromArgb(231, 229, 255);

            this.btnClose.Text = "Close"; this.btnClose.Location = new Point(1330, 20); this.btnClose.Size = new Size(180, 40); this.btnClose.BorderRadius = 8; 

            this.contentPanel.Controls.AddRange(new Control[] {
                label1, lblCompany, txtCompany, lblOrderDate, txtOrderDate, lblPayment, txtPayment, lblEstDate, txtEstDate,
                lblAddr1, txtAddr1, lblAddr2, txtAddr2, lblCity, txtCity, lblState, txtState, lblPostal, txtPostal, lblCountry, txtCountry,
                pnlItems, lblSubtotal, lblSubtotalVal, lblDiscount, txtDiscount, lblShipping, txtShipping, lblTotal, lblTotalVal
            });
            this.pnlItems.Controls.AddRange(new Control[] { lblItemTable, dgvItems });
            this.bottomPanel.Controls.Add(btnClose);

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainPanel);
            this.Text = "View Supplier Order";

            this.mainPanel.ResumeLayout(false);
            this.scrollPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvItems)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel, contentPanel, bottomPanel, pnlItems;
        private Label label1, lblCompany, lblOrderDate, lblPayment, lblEstDate, lblAddr1, lblAddr2, lblCity, lblState, lblPostal, lblCountry, lblItemTable;
        private Label lblSubtotal, lblSubtotalVal, lblDiscount, lblShipping, lblTotal, lblTotalVal;
        private Guna2TextBox txtCompany, txtOrderDate, txtPayment, txtEstDate, txtAddr1, txtAddr2, txtCity, txtState, txtPostal, txtCountry, txtDiscount, txtShipping;
        private Guna2DataGridView dgvItems;
        private DataGridViewTextBoxColumn colProdName, colQty, colSellPrice, colAvail, colSubtotal;
        private Guna2Button btnClose;
    }
}