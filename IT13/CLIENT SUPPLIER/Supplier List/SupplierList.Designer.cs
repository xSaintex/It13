using Guna.UI2.WinForms;
using System.Windows.Forms;
using System.Drawing;

namespace IT13
{
    partial class SupplierList
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
                BackColor = Color.FromArgb(12, 57, 101),
                ForeColor = Color.White,
                Font = new Font("Poppins", 12F, FontStyle.Bold),
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
            dgvSuppliers = new Guna2DataGridView();
            colID = new DataGridViewCheckBoxColumn();
            colCompany = new DataGridViewTextBoxColumn();
            colContact = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colPayment = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();
            Export = new Guna2ComboBox();
            btnAddSupplier = new Guna2Button();
            Filter = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            txtSearch = new Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dgvSuppliers)).BeginInit();
            this.SuspendLayout();

            // === LAYOUT IDENTICAL TO CustomerList ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnAddSupplier);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);

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
            SetupPaginationButton(btn7, "7", 131 ); SetupPaginationButton(btn6, "6", 1288);
            SetupPaginationButton(btn5, "5", 1258); SetupPaginationButton(btn4, "4", 1228);
            SetupPaginationButton(btn3, "3", 1198); SetupPaginationButton(btn2, "2", 1168);
            SetupPaginationButton(btn1, "1", 1138); SetupPaginationButton(btnlessthan, "<", 1108);

            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvSuppliers);

            dgvSuppliers.AllowUserToAddRows = false;
            dgvSuppliers.AllowUserToResizeColumns = false;
            dgvSuppliers.AllowUserToResizeRows = false;
            dgvSuppliers.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvSuppliers.ColumnHeadersHeight = 40;
            dgvSuppliers.GridColor = Color.FromArgb(231, 229, 255);
            dgvSuppliers.Location = new Point(22, 27);
            dgvSuppliers.Size = new Size(1412, 662);
            dgvSuppliers.RowHeadersVisible = false;
            dgvSuppliers.CellPainting += dgvSuppliers_CellPainting;
            dgvSuppliers.CellClick += dgvSuppliers_CellClick;
            dgvSuppliers.MouseDown += (s, e) => dgvSuppliers.ClearSelection();

            colID.HeaderText = "ID"; colID.Name = "colID";
            colCompany.HeaderText = "Company"; colCompany.Name = "colCompany";
            colContact.HeaderText = "Contact"; colContact.Name = "colContact";
            colPhone.HeaderText = "Phone"; colPhone.Name = "colPhone";
            colEmail.HeaderText = "Email"; colEmail.Name = "colEmail";
            colPayment.HeaderText = "Payment"; colPayment.Name = "colPayment";
            colStatus.HeaderText = "Status"; colStatus.Name = "colStatus";
            colActions.HeaderText = "Actions"; colActions.Name = "colActions";
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvSuppliers.Columns.AddRange(new DataGridViewColumn[] {
                colID, colCompany, colContact, colPhone, colEmail, colPayment, colStatus, colActions });

            Export.Location = new Point(1400, 45); Export.Size = new Size(128, 48);
            Export.Font = new Font("Poppins", 10F); Export.BorderRadius = 8; Export.FillColor = Color.White;

            btnAddSupplier.Location = new Point(1202, 45); btnAddSupplier.Size = new Size(190, 40);
            btnAddSupplier.Text = "✚ Add Supplier";
            btnAddSupplier.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnAddSupplier.FillColor = Color.FromArgb(0, 123, 255);
            btnAddSupplier.ForeColor = Color.White;
            btnAddSupplier.BorderRadius = 8;
            btnAddSupplier.Click += btnAddSupplier_Click;

            Filter.Location = new Point(1065, 45); Filter.Size = new Size(128, 48);
            Filter.Font = new Font("Poppins", 10F); Filter.BorderRadius = 8; Filter.FillColor = Color.White;

            btnSearch.Location = new Point(537, 41); btnSearch.Size = new Size(103, 48);
            btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSearch.BorderRadius = 5;
            btnSearch.Click += btnSearch_Click;  // NOW EXISTS!

            txtSearch.Location = new Point(94, 41); txtSearch.Size = new Size(437, 48);
            txtSearch.PlaceholderText = "Search company, email, or phone...";
            txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            txtSearch.Font = new Font("Poppins", 10.5F);
            txtSearch.ForeColor = Color.Black;
            txtSearch.BorderRadius = 12;

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "SupplierList";
            this.Text = "Supplier List";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false); guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvSuppliers)).EndInit();
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
        private Guna2DataGridView dgvSuppliers;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colCompany, colContact, colPhone, colEmail, colPayment, colStatus, colActions;
        private Guna2ComboBox Export, Filter;
        private Guna2Button btnAddSupplier, btnSearch;
        private Guna2TextBox txtSearch;
    }
}