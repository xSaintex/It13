namespace IT13
{
    partial class SupplierOrderList
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
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
            dgvOrders = new Guna.UI2.WinForms.Guna2DataGridView();
            colID = new DataGridViewCheckBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colSupplier = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            Export = new Guna.UI2.WinForms.Guna2ComboBox();
            btnAddOrder = new Guna.UI2.WinForms.Guna2Button();
            Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnSearch = new Guna.UI2.WinForms.Guna2Button();
            txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // MAIN PANEL
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnAddOrder);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);
            // PAGINATION PANEL
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(btngreaterthan);
            guna2Panel1.Controls.Add(btn9); guna2Panel1.Controls.Add(btn8); guna2Panel1.Controls.Add(btn7);
            guna2Panel1.Controls.Add(btn6); guna2Panel1.Controls.Add(btn5); guna2Panel1.Controls.Add(btn4);
            guna2Panel1.Controls.Add(btn3); guna2Panel1.Controls.Add(btn2); guna2Panel1.Controls.Add(btn1);
            guna2Panel1.Controls.Add(btnlessthan);
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            label1.Location = new Point(16, 8);
            label1.Text = "Showing 1-10 of 100";
            // PAGINATION BUTTONS
            SetupPaginationButton(btngreaterthan, ">", 1408);
            SetupPaginationButton(btn9, "9", 1378); SetupPaginationButton(btn8, "8", 1348);
            SetupPaginationButton(btn7, "7", 1318); SetupPaginationButton(btn6, "6", 1288);
            SetupPaginationButton(btn5, "5", 1258); SetupPaginationButton(btn4, "4", 1228);
            SetupPaginationButton(btn3, "3", 1198); SetupPaginationButton(btn2, "2", 1168);
            SetupPaginationButton(btn1, "1", 1138); SetupPaginationButton(btnlessthan, "<", 1108);
            // DATAGRIDVIEW PANEL
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvOrders);
            // DATAGRIDVIEW
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToResizeColumns = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvOrders.ColumnHeadersHeight = 40;
            dgvOrders.GridColor = Color.FromArgb(231, 229, 255);
            dgvOrders.Location = new Point(22, 27);
            dgvOrders.Size = new Size(1412, 662);
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
            dgvOrders.CellClick += dgvOrders_CellClick;
            // COLUMNS
            colID.HeaderText = "ID"; colID.Name = "colID"; colID.Width = 140;
            colDate.HeaderText = "Date"; colDate.Name = "colDate"; colDate.Width = 130;
            colSupplier.HeaderText = "Supplier"; colSupplier.Name = "colSupplier"; colSupplier.Width = 300;
            colTotal.HeaderText = "Total"; colTotal.Name = "colTotal"; colTotal.Width = 130;
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus"; colStatus.Width = 120;
            colActions.HeaderText = "Actions"; colActions.Name = "colActions"; colActions.Width = 120;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns.AddRange(new DataGridViewColumn[]
            {
                colID, colDate, colSupplier, colTotal, colStatus, colActions
            });
            // EXPORT
            Export.Location = new Point(1400, 45);
            Export.Size = new Size(128, 36);
            Export.DropDownStyle = ComboBoxStyle.DropDownList;
            Export.Font = new Font("Segoe UI", 10F);
            Export.ItemHeight = 30;
            // ADD ORDER
            btnAddOrder.Location = new Point(1235, 45);
            btnAddOrder.Size = new Size(155, 36);
            btnAddOrder.Text = "+ Add Order";
            btnAddOrder.FillColor = Color.FromArgb(0, 123, 255);
            btnAddOrder.ForeColor = Color.White;
            btnAddOrder.BorderRadius = 8;
            btnAddOrder.Click += btnAddOrder_Click;
            // FILTER
            Filter.Location = new Point(1100, 45);
            Filter.Size = new Size(128, 36);
            Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            Filter.Font = new Font("Segoe UI", 10F);
            Filter.ItemHeight = 30;
            // SEARCH
            btnSearch.Location = new Point(537, 41);
            btnSearch.Size = new Size(103, 40);
            btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.BorderRadius = 8;
            btnSearch.Click += btnSearch_Click;
            txtSearch.Location = new Point(94, 41);
            txtSearch.PlaceholderText = "Search supplier or order ID...";
            txtSearch.Size = new Size(437, 40);
            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "SupplierOrderList";
            Load += SupplierOrderList_Load;
            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
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
        // CONTROLS
        private Guna.UI2.WinForms.Guna2ShadowPanel mainpanel;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvOrders;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colSupplier;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2ComboBox Export;
        private Guna.UI2.WinForms.Guna2Button btnAddOrder;
        private Guna.UI2.WinForms.Guna2ComboBox Filter;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private void SupplierOrderList_Load(object sender, EventArgs e)
        {
            // You can add initialization logic here if needed
        }
    }
}