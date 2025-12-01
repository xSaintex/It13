// CustomerOrderList.Designer.cs
namespace IT13
{
    partial class CustomerOrderList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(12, 57, 101),
                ForeColor = Color.White,
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            this.mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new Label();
            this.btngreaterthan = new Guna.UI2.WinForms.Guna2Button();
            this.btn9 = new Guna.UI2.WinForms.Guna2Button();
            this.btn8 = new Guna.UI2.WinForms.Guna2Button();
            this.btn7 = new Guna.UI2.WinForms.Guna2Button();
            this.btn6 = new Guna.UI2.WinForms.Guna2Button();
            this.btn5 = new Guna.UI2.WinForms.Guna2Button();
            this.btn4 = new Guna.UI2.WinForms.Guna2Button();
            this.btn3 = new Guna.UI2.WinForms.Guna2Button();
            this.btn2 = new Guna.UI2.WinForms.Guna2Button();
            this.btn1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnlessthan = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.dgvOrders = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colID = new DataGridViewCheckBoxColumn();
            this.colCompany = new DataGridViewTextBoxColumn();
            this.colQty = new DataGridViewTextBoxColumn();
            this.colTotalCost = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colDeliveryDate = new DataGridViewTextBoxColumn();
            this.colActions = new DataGridViewTextBoxColumn();
            this.Export = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnAddOrder = new Guna.UI2.WinForms.Guna2Button();
            this.Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();

            this.mainpanel.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // mainpanel
            // 
            this.mainpanel.BackColor = Color.Transparent;
            this.mainpanel.Controls.Add(this.guna2Panel1);
            this.mainpanel.Controls.Add(this.guna2ShadowPanel1);
            this.mainpanel.Controls.Add(this.Export);
            this.mainpanel.Controls.Add(this.btnAddOrder);
            this.mainpanel.Controls.Add(this.Filter);
            this.mainpanel.Controls.Add(this.btnSearch);
            this.mainpanel.Controls.Add(this.txtSearch);
            this.mainpanel.FillColor = Color.White;
            this.mainpanel.Location = new Point(300, 88);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Radius = 8;
            this.mainpanel.Size = new Size(1602, 878);
            this.mainpanel.TabIndex = 1;
            // 
            // guna2Panel1 (Pagination)
            // 
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.btngreaterthan);
            this.guna2Panel1.Controls.Add(this.btn9);
            this.guna2Panel1.Controls.Add(this.btn8);
            this.guna2Panel1.Controls.Add(this.btn7);
            this.guna2Panel1.Controls.Add(this.btn6);
            this.guna2Panel1.Controls.Add(this.btn5);
            this.guna2Panel1.Controls.Add(this.btn4);
            this.guna2Panel1.Controls.Add(this.btn3);
            this.guna2Panel1.Controls.Add(this.btn2);
            this.guna2Panel1.Controls.Add(this.btn1);
            this.guna2Panel1.Controls.Add(this.btnlessthan);
            this.guna2Panel1.Location = new Point(77, 826);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new Size(1458, 36);
            this.guna2Panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            this.label1.ForeColor = Color.Gray;
            this.label1.Location = new Point(16, 8);
            this.label1.Text = "Showing 1-10 of 100";
            // 
            // Pagination Buttons (same as supplier)
            // 
            this.SetupPaginationButton(this.btngreaterthan, ">", 1408);
            this.SetupPaginationButton(this.btn9, "9", 1378);
            this.SetupPaginationButton(this.btn8, "8", 1348);
            this.SetupPaginationButton(this.btn7, "7", 1318);
            this.SetupPaginationButton(this.btn6, "6", 1288);
            this.SetupPaginationButton(this.btn5, "5", 1258);
            this.SetupPaginationButton(this.btn4, "4", 1228);
            this.SetupPaginationButton(this.btn3, "3", 1198);
            this.SetupPaginationButton(this.btn2, "2", 1168);
            this.SetupPaginationButton(this.btn1, "1", 1138);
            this.SetupPaginationButton(this.btnlessthan, "<", 1108);
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.Controls.Add(this.dgvOrders);
            this.guna2ShadowPanel1.FillColor = Color.White;
            this.guna2ShadowPanel1.Location = new Point(77, 104);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.Size = new Size(1458, 716);
            this.guna2ShadowPanel1.TabIndex = 5;
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToResizeColumns = false;
            this.dgvOrders.AllowUserToResizeRows = false;
            this.dgvOrders.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvOrders.ColumnHeadersHeight = 40;
            this.dgvOrders.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvOrders.Location = new Point(22, 27);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.Size = new Size(1412, 662);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgvOrders_CellPainting);
            this.dgvOrders.CellClick += new DataGridViewCellEventHandler(this.dgvOrders_CellClick);
            // 
            // Columns
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colCompany.HeaderText = "Company Name";
            this.colCompany.Name = "colCompany";
            this.colQty.HeaderText = "QTY";
            this.colQty.Name = "colQty";
            this.colTotalCost.HeaderText = "Total Cost";
            this.colTotalCost.Name = "colTotalCost";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colDeliveryDate.HeaderText = "Delivery Date";
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colActions.HeaderText = "Actions";
            this.colActions.Name = "colActions";
            this.colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvOrders.Columns.AddRange(new DataGridViewColumn[] {
                this.colID, this.colCompany, this.colQty, this.colTotalCost, this.colStatus, this.colDeliveryDate, this.colActions});
            // 
            // Export, Add, Filter, Search (same positions)
            // 
            this.Export.Location = new Point(1400, 45);
            this.Export.Size = new Size(128, 48);
            this.Export.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Export.Font = new Font("Poppins", 10F);
            this.Export.ItemHeight = 36;
            this.Export.BorderRadius = 8;
            this.Export.BorderColor = Color.FromArgb(200, 200, 200);
            this.Export.FillColor = Color.White;

            this.btnAddOrder.Location = new Point(1202, 45);
            this.btnAddOrder.Size = new Size(190, 43);
            this.btnAddOrder.Text = "✚ Add Order";
            this.btnAddOrder.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            this.btnAddOrder.FillColor = Color.FromArgb(0, 123, 255);
            this.btnAddOrder.ForeColor = Color.White;
            this.btnAddOrder.BorderRadius = 8;
            this.btnAddOrder.Click += new EventHandler(this.btnAddOrder_Click);

            this.Filter.Location = new Point(1065, 45);
            this.Filter.Size = new Size(128, 48);
            this.Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Filter.Font = new Font("Poppins", 10F);
            this.Filter.ItemHeight = 36;
            this.Filter.BorderRadius = 8;
            this.Filter.BorderColor = Color.FromArgb(200, 200, 200);
            this.Filter.FillColor = Color.White;

            this.btnSearch.Location = new Point(537, 41);
            this.btnSearch.Size = new Size(103, 48);
            this.btnSearch.Text = "Search";
            this.btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            this.btnSearch.BorderRadius = 5;

            this.txtSearch.Location = new Point(94, 41);
            this.txtSearch.Size = new Size(437, 48);
            this.txtSearch.PlaceholderText = "Search company or order ID...";
            this.txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            this.txtSearch.Font = new Font("Poppins", 10.5F);
            this.txtSearch.BorderRadius = 12;
            this.txtSearch.BorderColor = Color.FromArgb(200, 200, 200);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);
            // 
            // CustomerOrderList
            // 
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainpanel);
            this.Name = "CustomerOrderList";
            this.Text = "Customer Order List";

            this.mainpanel.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
        }

        private void SetupPaginationButton(Guna.UI2.WinForms.Guna2Button btn, string text, int left)
        {
            btn.Text = text;
            btn.Location = new Point(left, 5);
            btn.Size = new Size(36, 32);
            btn.FillColor = Color.FromArgb(248, 248, 248);
            btn.ForeColor = Color.Black;
            btn.BorderRadius = 8;
            btn.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btn.HoverState.FillColor = Color.FromArgb(235, 235, 235);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel mainpanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btngreaterthan;
        private Guna.UI2.WinForms.Guna2Button btn9;
        private Guna.UI2.WinForms.Guna2Button btn8;
        private Guna.UI2.WinForms.Guna2Button btn7;
        private Guna.UI2.WinForms.Guna2Button btn6;
        private Guna.UI2.WinForms.Guna2Button btn5;
        private Guna.UI2.WinForms.Guna2Button btn4;
        private Guna.UI2.WinForms.Guna2Button btn3;
        private Guna.UI2.WinForms.Guna2Button btn2;
        private Guna.UI2.WinForms.Guna2Button btn1;
        private Guna.UI2.WinForms.Guna2Button btnlessthan;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvOrders;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colCompany;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colTotalCost;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colDeliveryDate;
        private DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2ComboBox Export;
        private Guna.UI2.WinForms.Guna2Button btnAddOrder;
        private Guna.UI2.WinForms.Guna2ComboBox Filter;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
    }
}