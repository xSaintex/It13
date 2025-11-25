using Guna.UI2.WinForms;

namespace IT13
{
    partial class ReturnList
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(12, 57, 101),
            ForeColor = Color.White,
            Font = new Font("Poppins", 12F, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainpanel = new Guna2ShadowPanel();
            guna2Panel1 = new Guna2Panel();
            label1 = new Label();
            btngreaterthan = new Guna2Button(); btn9 = new Guna2Button(); btn8 = new Guna2Button();
            btn7 = new Guna2Button(); btn6 = new Guna2Button(); btn5 = new Guna2Button();
            btn4 = new Guna2Button(); btn3 = new Guna2Button(); btn2 = new Guna2Button();
            btn1 = new Guna2Button(); btnlessthan = new Guna2Button();

            guna2ShadowPanel1 = new Guna2ShadowPanel();
            dgvReturns = new Guna2DataGridView();

            colCheck = new DataGridViewCheckBoxColumn();
            colOrderID = new DataGridViewTextBoxColumn();
            colReturnType = new DataGridViewTextBoxColumn();
            colCompanyName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colReturnDate = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();

            Export = new Guna2ComboBox();
            btnAddSupplierReturn = new Guna2Button();
            btnAddCustomerReturn = new Guna2Button();
            Filter = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            txtSearch = new Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReturns).BeginInit();
            this.SuspendLayout();

            // mainpanel
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnAddSupplierReturn);
            mainpanel.Controls.Add(btnAddCustomerReturn);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);

            // Pagination panel
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.AddRange(new Control[] {
                btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan });

            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(16, 8);
            label1.Text = "Showing 1-10 of 100";

            SetupPaginationButton(btngreaterthan, ">", 1408);
            SetupPaginationButton(btn9, "9", 1378); SetupPaginationButton(btn8, "8", 1348);
            SetupPaginationButton(btn7, "7", 1318); SetupPaginationButton(btn6, "6", 1288);
            SetupPaginationButton(btn5, "5", 1258); SetupPaginationButton(btn4, "4", 1228);
            SetupPaginationButton(btn3, "3", 1198); SetupPaginationButton(btn2, "2", 1168);
            SetupPaginationButton(btn1, "1", 1138); SetupPaginationButton(btnlessthan, "<", 1108);

            // DataGrid panel
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvReturns);

            dgvReturns.AllowUserToAddRows = false;
            dgvReturns.AllowUserToResizeColumns = false;
            dgvReturns.AllowUserToResizeRows = false;
            dgvReturns.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvReturns.ColumnHeadersHeight = 40;
            dgvReturns.GridColor = Color.FromArgb(231, 229, 255);
            dgvReturns.Location = new Point(22, 27);
            dgvReturns.Size = new Size(1412, 662);
            dgvReturns.RowHeadersVisible = false;
            dgvReturns.BackgroundColor = Color.White;
            dgvReturns.RowTemplate.Height = 45;
            dgvReturns.CellPainting += dgvReturns_CellPainting;
            dgvReturns.CellClick += dgvReturns_CellClick;

            // Columns
            colCheck.HeaderText = "Return ID"; colCheck.Name = "colCheck";
            colOrderID.HeaderText = "Order ID"; colOrderID.Name = "colOrderID";
            colReturnType.HeaderText = "Return Type"; colReturnType.Name = "colReturnType";
            colCompanyName.HeaderText = "Company Name"; colCompanyName.Name = "colCompanyName";
            colQty.HeaderText = "QTY"; colQty.Name = "colQty";
            colTotal.HeaderText = "Total"; colTotal.Name = "colTotal";
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus";
            colReturnDate.HeaderText = "Return Date"; colReturnDate.Name = "colReturnDate";
            colActions.HeaderText = "Actions"; colActions.Name = "colActions";
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns.AddRange(new DataGridViewColumn[] {
                colCheck, colOrderID, colReturnType, colCompanyName, colQty, colTotal, colStatus, colReturnDate, colActions
            });

            // Top controls
            Export.Location = new Point(1400, 45); Export.Size = new Size(128, 50);
            Export.Font = new Font("Poppins", 10F); Export.BorderRadius = 8; Export.FillColor = Color.White;

            btnAddSupplierReturn.Location = new Point(1200, 45); btnAddSupplierReturn.Size = new Size(190, 38);
            btnAddSupplierReturn.Text = "+ Supplier Return";
            btnAddSupplierReturn.FillColor = Color.FromArgb(0, 123, 255);
            btnAddSupplierReturn.ForeColor = Color.White;
            btnAddSupplierReturn.BorderRadius = 8;
            btnAddSupplierReturn.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnAddSupplierReturn.Click += btnAddSupplierReturn_Click;

            btnAddCustomerReturn.Location = new Point(990, 45); btnAddCustomerReturn.Size = new Size(200, 38);
            btnAddCustomerReturn.Text = "+ Customer Return";
            btnAddCustomerReturn.FillColor = Color.FromArgb(40, 167, 69);
            btnAddCustomerReturn.ForeColor = Color.White;
            btnAddCustomerReturn.BorderRadius = 8;
            btnAddCustomerReturn.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnAddCustomerReturn.Click += btnAddCustomerReturn_Click;

            Filter.Location = new Point(850, 45); Filter.Size = new Size(128, 50);
            Filter.Font = new Font("Poppins", 10F); Filter.BorderRadius = 8; Filter.FillColor = Color.White;

            btnSearch.Location = new Point(537, 41); btnSearch.Size = new Size(103, 48);
            btnSearch.Text = "Search"; btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White; btnSearch.BorderRadius = 5;
            btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSearch.Click += btnSearch_Click;

            txtSearch.Location = new Point(94, 41); txtSearch.Size = new Size(437, 48);
            txtSearch.PlaceholderText = "Search return ID, order ID or company name...";
            txtSearch.Font = new Font("Poppins", 10.5F);
            txtSearch.ForeColor = Color.Black;
            txtSearch.BorderRadius = 12;
            txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "ReturnList";
            this.Text = "Return List";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReturns).EndInit();
            this.ResumeLayout(false);
        }

        private void SetupPaginationButton(Guna2Button btn, string text, int left)
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

        private Guna2ShadowPanel mainpanel, guna2ShadowPanel1;
        private Guna2Panel guna2Panel1;
        private Label label1;
        private Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna2DataGridView dgvReturns;
        private DataGridViewCheckBoxColumn colCheck;
        private DataGridViewTextBoxColumn colOrderID, colReturnType, colCompanyName, colQty, colTotal, colStatus, colReturnDate, colActions;
        private Guna2ComboBox Export, Filter;
        private Guna2Button btnAddCustomerReturn, btnAddSupplierReturn, btnSearch;
        private Guna2TextBox txtSearch;
    }
}