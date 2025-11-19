using Guna.UI2.WinForms;
namespace IT13
{
    partial class CustomerReturns
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
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
            colCustomerID = new DataGridViewTextBoxColumn();
            colCustomerName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colReturnDate = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            Export = new Guna2ComboBox();
            btnAddReturn = new Guna2Button();
            Filter = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            txtSearch = new Guna2TextBox();

            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1); mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export); mainpanel.Controls.Add(btnAddReturn);
            mainpanel.Controls.Add(Filter); mainpanel.Controls.Add(btnSearch); mainpanel.Controls.Add(txtSearch);

            guna2Panel1.Location = new Point(77, 826); guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.AddRange(new Control[] { btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan });
            label1.AutoSize = true; label1.Text = "Showing 1-10 of 100"; label1.Location = new Point(20, 9);
            label1.Font = new Font("Tahoma", 9F, FontStyle.Bold);

            SetupPaginationButton(btngreaterthan, ">", 1408); SetupPaginationButton(btn9, "9", 1378);
            SetupPaginationButton(btn8, "8", 1348); SetupPaginationButton(btn7, "7", 1318);
            SetupPaginationButton(btn6, "6", 1288); SetupPaginationButton(btn5, "5", 1258);
            SetupPaginationButton(btn4, "4", 1228); SetupPaginationButton(btn3, "3", 1198);
            SetupPaginationButton(btn2, "2", 1168); SetupPaginationButton(btn1, "1", 1138);
            SetupPaginationButton(btnlessthan, "<", 1108);

            guna2ShadowPanel1.FillColor = Color.White; guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5; guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvReturns);

            dgvReturns.AllowUserToAddRows = false; dgvReturns.AllowUserToResizeColumns = false;
            dgvReturns.AllowUserToResizeRows = false; dgvReturns.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvReturns.ColumnHeadersHeight = 40; dgvReturns.GridColor = Color.FromArgb(231, 229, 255);
            dgvReturns.Location = new Point(22, 27); dgvReturns.Size = new Size(1412, 662);
            dgvReturns.RowHeadersVisible = false;
            dgvReturns.CellPainting += dgvReturns_CellPainting;
            dgvReturns.CellClick += dgvReturns_CellClick;

            colCheck.HeaderText = "Return ID"; colCheck.Name = "colCheck";
            colCustomerID.HeaderText = "Customer ID"; colCustomerID.Name = "colCustomerID";
            colCustomerName.HeaderText = "Customer Name"; colCustomerName.Name = "colCustomerName";
            colQty.HeaderText = "QTY"; colQty.Name = "colQty";
            colTotal.HeaderText = "Total"; colTotal.Name = "colTotal";
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus";
            colReturnDate.HeaderText = "Return Date"; colReturnDate.Name = "colReturnDate";
            colActions.HeaderText = "Actions"; colActions.Name = "colActions";
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvReturns.Columns.AddRange(new DataGridViewColumn[]
            {
                colCheck, colCustomerID, colCustomerName, colQty, colTotal, colStatus, colReturnDate, colActions
            });

            Export.Location = new Point(1400, 45); Export.Size = new Size(128, 36); Export.DropDownStyle = ComboBoxStyle.DropDownList;
            btnAddReturn.Location = new Point(1235, 45); btnAddReturn.Size = new Size(155, 36);
            btnAddReturn.Text = "+ Add Return"; btnAddReturn.FillColor = Color.FromArgb(0, 123, 255);
            btnAddReturn.ForeColor = Color.White; btnAddReturn.BorderRadius = 8;
            btnAddReturn.Click += btnAddReturn_Click;

            Filter.Location = new Point(1100, 45); Filter.Size = new Size(128, 36); Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            btnSearch.Location = new Point(537, 41); btnSearch.Size = new Size(103, 40); btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255); btnSearch.ForeColor = Color.White; btnSearch.BorderRadius = 8;
            btnSearch.Click += btnSearch_Click;

            txtSearch.Location = new Point(94, 41); txtSearch.Size = new Size(437, 40);
            txtSearch.PlaceholderText = "Search return ID, customer ID or name...";

            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "CustomerReturns";
            Load += (s, e) => { };

            mainpanel.ResumeLayout(false); guna2Panel1.ResumeLayout(false); guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReturns).EndInit();
            ResumeLayout(false);
        }

        private void SetupPaginationButton(Guna2Button btn, string text, int left)
        {
            btn.Text = text; btn.Location = new Point(left, 5); btn.Size = new Size(36, 32);
            btn.FillColor = Color.FromArgb(248, 248, 248); btn.ForeColor = Color.Black;
            btn.BorderRadius = 8; btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.HoverState.FillColor = Color.FromArgb(235, 235, 235);
        }

        private Guna2ShadowPanel mainpanel, guna2ShadowPanel1;
        private Guna2Panel guna2Panel1;
        private Label label1;
        private Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna2DataGridView dgvReturns;
        private DataGridViewCheckBoxColumn colCheck;
        private DataGridViewTextBoxColumn colCustomerID, colCustomerName, colQty, colTotal, colStatus, colReturnDate, colActions;
        private Guna2ComboBox Export, Filter;
        private Guna2Button btnAddReturn, btnSearch;
        private Guna2TextBox txtSearch;
    }
}