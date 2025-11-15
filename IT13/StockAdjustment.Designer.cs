namespace IT13
{
    partial class StockAdjustment
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10.2F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            label1 = new Label();
            btngreaterthan = new Guna.UI2.WinForms.Guna2Button();
            btn9 = new Guna.UI2.WinForms.Guna2Button();
            btn8 = new Guna.UI2.WinForms.Guna2Button();
            btn7 = new Guna.UI2.WinForms.Guna2Button();
            btn6 = new Guna.UI2.WinForms.Guna2Button();
            btn5 = new Guna.UI2.WinForms.Guna2Button();
            btn4 = new Guna.UI2.WinForms.Guna2Button();
            btn3 = new Guna.UI2.WinForms.Guna2Button();
            btn2 = new Guna.UI2.WinForms.Guna2Button();
            btn1 = new Guna.UI2.WinForms.Guna2Button();
            btnlessthan = new Guna.UI2.WinForms.Guna2Button();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            datagridviewadjustment = new Guna.UI2.WinForms.Guna2DataGridView();
            colID = new DataGridViewCheckBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colProductName = new DataGridViewTextBoxColumn();
            colAdjustmentType = new DataGridViewTextBoxColumn();
            colPhysicalCount = new DataGridViewTextBoxColumn();
            colRequestedBy = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            Export = new Guna.UI2.WinForms.Guna2ComboBox();
            btnaddadjustment = new Guna.UI2.WinForms.Guna2Button();
            Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnsearch = new Guna.UI2.WinForms.Guna2Button();
            txtboxsearch = new Guna.UI2.WinForms.Guna2TextBox();
            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridviewadjustment).BeginInit();
            SuspendLayout();
            // MAIN PANEL
            mainpanel.BackColor = Color.Transparent;
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnaddadjustment);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnsearch);
            mainpanel.Controls.Add(txtboxsearch);
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 8;
            mainpanel.ShadowColor = Color.Black;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.TabIndex = 1;
            // PAGINATION PANEL
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(btngreaterthan);
            guna2Panel1.Controls.Add(btn9);
            guna2Panel1.Controls.Add(btn8);
            guna2Panel1.Controls.Add(btn7);
            guna2Panel1.Controls.Add(btn6);
            guna2Panel1.Controls.Add(btn5);
            guna2Panel1.Controls.Add(btn4);
            guna2Panel1.Controls.Add(btn3);
            guna2Panel1.Controls.Add(btn2);
            guna2Panel1.Controls.Add(btn1);
            guna2Panel1.Controls.Add(btnlessthan);
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.TabIndex = 7;
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            label1.Location = new Point(16, 8);
            label1.Text = "Showing 1-10 of 100";
            // PAGINATION BUTTONS
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
            // DATAGRIDVIEW PANEL
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(datagridviewadjustment);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.TabIndex = 5;
            // DATAGRIDVIEW
            datagridviewadjustment.AllowUserToAddRows = false;
            datagridviewadjustment.AllowUserToResizeColumns = false;
            datagridviewadjustment.AllowUserToResizeRows = false;
            datagridviewadjustment.ColumnHeadersDefaultCellStyle = headerStyle;
            datagridviewadjustment.ColumnHeadersHeight = 40;
            datagridviewadjustment.GridColor = Color.FromArgb(231, 229, 255);
            datagridviewadjustment.Location = new Point(22, 27);
            datagridviewadjustment.Name = "datagridviewadjustment";
            datagridviewadjustment.RowHeadersVisible = false;
            datagridviewadjustment.Size = new Size(1412, 662);
            datagridviewadjustment.TabIndex = 0;
            datagridviewadjustment.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            datagridviewadjustment.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            datagridviewadjustment.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            datagridviewadjustment.CellPainting += datagridviewadjustment_CellPainting;
            datagridviewadjustment.CellClick += datagridviewadjustment_CellClick;
            // COLUMNS
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 140;
            colDate.HeaderText = "Date"; colDate.Name = "colDate"; colDate.Width = 130;
            colProductName.HeaderText = "Product Name"; colProductName.Name = "colProductName"; colProductName.Width = 220;
            colAdjustmentType.HeaderText = "Adjust Type"; colAdjustmentType.Name = "colAdjustmentType"; colAdjustmentType.Width = 150;
            colPhysicalCount.HeaderText = "Phys Count"; colPhysicalCount.Name = "colPhysicalCount"; colPhysicalCount.Width = 130;
            colRequestedBy.HeaderText = "Requested By"; colRequestedBy.Name = "colRequestedBy"; colRequestedBy.Width = 180;
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus"; colStatus.Width = 120;
            colActions.HeaderText = "Actions"; colActions.Name = "colActions"; colActions.Width = 100;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridviewadjustment.Columns.AddRange(new DataGridViewColumn[]
            {
                colID, colDate, colProductName, colAdjustmentType,
                colPhysicalCount, colRequestedBy, colStatus, colActions
            });
            // EXPORT COMBOBOX
            Export.Location = new Point(1400, 45);
            Export.Size = new Size(128, 36);
            Export.DropDownStyle = ComboBoxStyle.DropDownList;
            Export.Font = new Font("Segoe UI", 10F);
            Export.ItemHeight = 30;
            Export.TabIndex = 4;
            // ADD ADJUSTMENT BUTTON
            btnaddadjustment.Location = new Point(1235, 45);
            btnaddadjustment.Size = new Size(155, 36);
            btnaddadjustment.Text = "+Add Adjustment";
            btnaddadjustment.Click += btnaddadjustment_Click;
            btnaddadjustment.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnaddadjustment.Padding = new Padding(4, 0, 4, 0);
            btnaddadjustment.FillColor = Color.FromArgb(0, 123, 255);
            btnaddadjustment.ForeColor = Color.White;
            btnaddadjustment.BorderRadius = 8;
            // FILTER COMBOBOX
            Filter.Location = new Point(1100, 45);
            Filter.Size = new Size(128, 36);
            Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            Filter.Font = new Font("Segoe UI", 10F);
            Filter.ItemHeight = 30;
            Filter.TabIndex = 2;
            // SEARCH BUTTON
            btnsearch.Location = new Point(537, 41);
            btnsearch.Size = new Size(103, 40);
            btnsearch.Text = "Search";
            btnsearch.FillColor = Color.FromArgb(0, 123, 255);
            btnsearch.ForeColor = Color.White;
            btnsearch.BorderRadius = 8;
            txtboxsearch.Location = new Point(94, 41);
            txtboxsearch.PlaceholderText = "Search Adjustment";
            txtboxsearch.Size = new Size(437, 40);
            txtboxsearch.TextChanged += txtboxsearch_TextChanged;
            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "StockAdjustment";
            Text = "Stock Adjustment";
            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagridviewadjustment).EndInit();
            ResumeLayout(false);
        }
        private void SetupPaginationButton(Guna.UI2.WinForms.Guna2Button btn, string text, int left)
        {
            btn.Text = text;
            btn.Location = new Point(left, 5);
            btn.Size = new Size(36, 32);
            btn.FillColor = Color.FromArgb(248, 248, 248);
            btn.ForeColor = Color.Black;
            btn.BorderRadius = 8;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.HoverState.FillColor = Color.FromArgb(235, 235, 235);
            btn.DisabledState.FillColor = Color.FromArgb(220, 220, 220);
            btn.DisabledState.ForeColor = Color.Gray;
        }
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
        private Guna.UI2.WinForms.Guna2DataGridView datagridviewadjustment;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colAdjustmentType;
        private DataGridViewTextBoxColumn colPhysicalCount;
        private DataGridViewTextBoxColumn colRequestedBy;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2ComboBox Export;
        private Guna.UI2.WinForms.Guna2Button btnaddadjustment;
        private Guna.UI2.WinForms.Guna2ComboBox Filter;
        private Guna.UI2.WinForms.Guna2Button btnsearch;
        private Guna.UI2.WinForms.Guna2TextBox txtboxsearch;
    }
}