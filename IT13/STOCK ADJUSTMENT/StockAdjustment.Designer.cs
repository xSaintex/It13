namespace IT13
{
    partial class StockAdjustment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(12, 57, 101),
                ForeColor = Color.White,
                Font = new Font("Poppins", 12F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            this.mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
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
            this.dgvAdjustment = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdjustmentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhysicalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRequestedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Export = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnAddAdjustment = new Guna.UI2.WinForms.Guna2Button();
            this.Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();

            this.mainpanel.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustment)).BeginInit();
            this.SuspendLayout();

            // mainpanel
            this.mainpanel.Controls.Add(this.guna2Panel1);
            this.mainpanel.Controls.Add(this.guna2ShadowPanel1);
            this.mainpanel.Controls.Add(this.Export);
            this.mainpanel.Controls.Add(this.btnAddAdjustment);
            this.mainpanel.Controls.Add(this.Filter);
            this.mainpanel.Controls.Add(this.btnSearch);
            this.mainpanel.Controls.Add(this.txtSearch);
            this.mainpanel.FillColor = Color.White;
            this.mainpanel.Location = new Point(300, 88);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Radius = 8;
            this.mainpanel.Size = new Size(1602, 878);
            this.mainpanel.TabIndex = 1;

            // Pagination panel
            this.guna2Panel1.Location = new Point(77, 826);
            this.guna2Panel1.Size = new Size(1458, 36);
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

            this.label1.AutoSize = true;
            this.label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            this.label1.ForeColor = Color.Gray;
            this.label1.Location = new Point(16, 8);
            this.label1.Text = "Showing 1-10 of 100";

            SetupPaginationButton(btngreaterthan, ">", 1408);
            SetupPaginationButton(btn9, "9", 1378);
            SetupPaginationButton(btn8, "8", 1348);
            SetupPaginationButton(btn7, "7", 1318);
            SetupPaginationButton(btn6, "6", 1288);
            SetupPaginationButton(btn5, "5", 1258);
            SetupPaginationButton(btn4, "4", 1228);
            SetupPaginationButton(btn3, "3", 1198);
            SetupPaginationButton(btn2, "2", 1168);
            SetupPaginationButton(btn1, "1", 1138);
            SetupPaginationButton(btnlessthan, "<", 1108);

            // Grid panel
            this.guna2ShadowPanel1.FillColor = Color.White;
            this.guna2ShadowPanel1.Location = new Point(77, 104);
            this.guna2ShadowPanel1.Size = new Size(1458, 716);
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.Controls.Add(this.dgvAdjustment);

            // dgvAdjustment
            this.dgvAdjustment.AllowUserToAddRows = false;
            this.dgvAdjustment.AllowUserToResizeColumns = false;
            this.dgvAdjustment.AllowUserToResizeRows = false;
            this.dgvAdjustment.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvAdjustment.ColumnHeadersHeight = 40;
            this.dgvAdjustment.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvAdjustment.Location = new Point(22, 27);
            this.dgvAdjustment.Name = "dgvAdjustment";
            this.dgvAdjustment.RowHeadersVisible = false;
            this.dgvAdjustment.Size = new Size(1412, 662);
            this.dgvAdjustment.TabIndex = 0;

            this.dgvAdjustment.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgvAdjustment_CellPainting);
            this.dgvAdjustment.CellClick += new DataGridViewCellEventHandler(this.dgvAdjustment_CellClick);

            // Columns
            this.colID.HeaderText = "ID"; this.colID.Name = "colID";
            this.colDate.HeaderText = "Date"; this.colDate.Name = "colDate";
            this.colProductName.HeaderText = "Product Name"; this.colProductName.Name = "colProductName";
            this.colAdjustmentType.HeaderText = "Adjust Type"; this.colAdjustmentType.Name = "colAdjustmentType";
            this.colPhysicalCount.HeaderText = "PhyCount"; this.colPhysicalCount.Name = "colPhysicalCount";
            this.colRequestedBy.HeaderText = "Requested By"; this.colRequestedBy.Name = "colRequestedBy";
            this.colStatus.HeaderText = "Status"; this.colStatus.Name = "colStatus";
            this.colActions.HeaderText = "Actions"; this.colActions.Name = "colActions";
            this.colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvAdjustment.Columns.AddRange(new DataGridViewColumn[] {
                colID, colDate, colProductName, colAdjustmentType, colPhysicalCount, colRequestedBy, colStatus, colActions });

            // Top controls (exact copy of SupplierOrderList)
            this.Export.Location = new Point(1400, 45); this.Export.Size = new Size(128, 50);
            this.Export.Font = new Font("Poppins", 10F); this.Export.BorderRadius = 8; this.Export.FillColor = Color.White;

            this.btnAddAdjustment.Location = new Point(1202, 45); this.btnAddAdjustment.Size = new Size(190, 38);
            this.btnAddAdjustment.Text = "✚ Add Adjustment";
            this.btnAddAdjustment.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            this.btnAddAdjustment.FillColor = Color.FromArgb(0, 123, 255);
            this.btnAddAdjustment.ForeColor = Color.White;
            this.btnAddAdjustment.BorderRadius = 8;
            this.btnAddAdjustment.Click += new EventHandler(this.btnAddAdjustment_Click);

            this.Filter.Location = new Point(1065, 45); this.Filter.Size = new Size(128, 50);
            this.Filter.Font = new Font("Poppins", 10F); this.Filter.BorderRadius = 8; this.Filter.FillColor = Color.White;

            this.btnSearch.Location = new Point(537, 41); this.btnSearch.Size = new Size(103, 48);
            this.btnSearch.Text = "Search"; this.btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            this.btnSearch.ForeColor = Color.White; this.btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            this.btnSearch.BorderRadius = 5;

            this.txtSearch.Location = new Point(94, 41); this.txtSearch.Size = new Size(437, 48);
            this.txtSearch.PlaceholderText = "Search adjustment ID or product...";
            this.txtSearch.Font = new Font("Poppins", 10.5F); this.txtSearch.BorderRadius = 12;
            this.txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // Form
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainpanel);
            this.Name = "StockAdjustment";
            this.Text = "Stock Adjustment";

            this.mainpanel.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustment)).EndInit();
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

        // Controls
        private Guna.UI2.WinForms.Guna2ShadowPanel mainpanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label1;
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
        private Guna.UI2.WinForms.Guna2DataGridView dgvAdjustment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdjustmentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhysicalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequestedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2ComboBox Export;
        private Guna.UI2.WinForms.Guna2Button btnAddAdjustment;
        private Guna.UI2.WinForms.Guna2ComboBox Filter;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
    }
}