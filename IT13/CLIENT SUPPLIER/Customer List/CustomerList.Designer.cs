using Guna.UI2.WinForms;

namespace IT13
{
    partial class CustomerList
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
                BackColor = Color.FromArgb(12, 57, 101),       // EXACT same as StockAdjustment
                ForeColor = Color.White,
                Font = new Font("Poppins", 12F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            label1 = new System.Windows.Forms.Label();
            btngreaterthan = new Guna.UI2.WinForms.Guna2Button();
            btn9 = new Guna.UI2.WinForms.Guna2Button(); btn8 = new Guna.UI2.WinForms.Guna2Button(); btn7 = new Guna.UI2.WinForms.Guna2Button();
            btn6 = new Guna.UI2.WinForms.Guna2Button(); btn5 = new Guna.UI2.WinForms.Guna2Button(); btn4 = new Guna.UI2.WinForms.Guna2Button();
            btn3 = new Guna.UI2.WinForms.Guna2Button(); btn2 = new Guna.UI2.WinForms.Guna2Button(); btn1 = new Guna.UI2.WinForms.Guna2Button();
            btnlessthan = new Guna.UI2.WinForms.Guna2Button();
            guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            dgvCustomers = new Guna.UI2.WinForms.Guna2DataGridView();
            colID = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colActions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Export = new Guna.UI2.WinForms.Guna2ComboBox();
            btnAddCustomer = new Guna.UI2.WinForms.Guna2Button();
            Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnSearch = new Guna.UI2.WinForms.Guna2Button();
            txtSearch = new Guna.UI2.WinForms.Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dgvCustomers)).BeginInit();
            this.SuspendLayout();

            // mainpanel
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnAddCustomer);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);

            // Pagination panel
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(btngreaterthan);
            guna2Panel1.Controls.Add(btn9); guna2Panel1.Controls.Add(btn8); guna2Panel1.Controls.Add(btn7);
            guna2Panel1.Controls.Add(btn6); guna2Panel1.Controls.Add(btn5); guna2Panel1.Controls.Add(btn4);
            guna2Panel1.Controls.Add(btn3); guna2Panel1.Controls.Add(btn2); guna2Panel1.Controls.Add(btn1);
            guna2Panel1.Controls.Add(btnlessthan);

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

            // Grid panel
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvCustomers);

            // dgvCustomers
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToResizeColumns = false;
            dgvCustomers.AllowUserToResizeRows = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvCustomers.ColumnHeadersHeight = 40;
            dgvCustomers.GridColor = Color.FromArgb(231, 229, 255);
            dgvCustomers.Location = new Point(22, 27);
            dgvCustomers.Size = new Size(1412, 662);
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.CellPainting += dgvCustomers_CellPainting;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.MouseDown += (s, e) => dgvCustomers.ClearSelection();

            colID.HeaderText = "ID"; colID.Name = "colID";
            colCompany.HeaderText = "Company"; colCompany.Name = "colCompany";
            colContact.HeaderText = "Contact"; colContact.Name = "colContact";
            colPhone.HeaderText = "Phone"; colPhone.Name = "colPhone";
            colEmail.HeaderText = "Email"; colEmail.Name = "colEmail";
            colPayment.HeaderText = "Payment"; colPayment.Name = "colPayment";
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus";
            colActions.HeaderText = "Actions"; colActions.Name = "colActions";
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] {
                colID, colCompany, colContact, colPhone, colEmail, colPayment, colStatus, colActions });

            // Top controls
            Export.Location = new Point(1400, 45); Export.Size = new Size(128, 48);
            Export.Font = new Font("Poppins", 10F); Export.BorderRadius = 8; Export.FillColor = Color.White;

            btnAddCustomer.Location = new Point(1202, 45); btnAddCustomer.Size = new Size(190, 40);
            btnAddCustomer.Text = "✚ Add Customer";
            btnAddCustomer.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnAddCustomer.FillColor = Color.FromArgb(0, 123, 255);
            btnAddCustomer.ForeColor = Color.White;
            btnAddCustomer.BorderRadius = 8;
            btnAddCustomer.Click += btnAddCustomer_Click;

            Filter.Location = new Point(1065, 45); Filter.Size = new Size(128, 48);
            Filter.Font = new Font("Poppins", 10F); Filter.BorderRadius = 8; Filter.FillColor = Color.White;

            btnSearch.Location = new Point(537, 41); btnSearch.Size = new Size(103, 48);
            btnSearch.Text = "Search"; btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White; btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSearch.BorderRadius = 5;

            txtSearch.Location = new Point(94, 41); txtSearch.Size = new Size(437, 48);
            txtSearch.PlaceholderText = "Search company, email, or phone...";
            txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            txtSearch.Font = new Font("Poppins", 10.5F);
            txtSearch.ForeColor = Color.Black;
            txtSearch.BorderRadius = 12;
            txtSearch.TextChanged += txtSearch_TextChanged;

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "CustomerList";
            this.Text = "Customer List";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false); guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvCustomers)).EndInit();
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

        private Guna2ShadowPanel mainpanel;
        private Guna2Panel guna2Panel1;
        private Label label1;
        private Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna2ShadowPanel guna2ShadowPanel1;
        private Guna2DataGridView dgvCustomers;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colCompany, colContact, colPhone, colEmail, colPayment, colStatus, colActions;
        private Guna2ComboBox Export;
        private Guna2Button btnAddCustomer;
        private Guna2ComboBox Filter;
        private Guna2Button btnSearch;
        private Guna2TextBox txtSearch;
    }
}