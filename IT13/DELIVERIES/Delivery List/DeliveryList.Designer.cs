using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class DeliveryList
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

            this.mainpanel = new Guna2ShadowPanel();
            this.guna2Panel1 = new Guna2Panel();
            this.label1 = new Label();
            this.btngreaterthan = new Guna2Button();
            this.btn9 = new Guna2Button();
            this.btn8 = new Guna2Button();
            this.btn7 = new Guna2Button();
            this.btn6 = new Guna2Button();
            this.btn5 = new Guna2Button();
            this.btn4 = new Guna2Button();
            this.btn3 = new Guna2Button();
            this.btn2 = new Guna2Button();
            this.btn1 = new Guna2Button();
            this.btnlessthan = new Guna2Button();
            this.guna2ShadowPanel1 = new Guna2ShadowPanel();
            this.dgvDeliveries = new Guna2DataGridView();

            this.colDeliveryID = new DataGridViewCheckBoxColumn();
            this.colOrderID = new DataGridViewTextBoxColumn();
            this.colCustomer = new DataGridViewTextBoxColumn();
            this.colDeliveryDate = new DataGridViewTextBoxColumn();
            this.colEmployee = new DataGridViewTextBoxColumn();
            this.colVehicle = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colActions = new DataGridViewTextBoxColumn();

            this.Export = new Guna2ComboBox();
            this.btnAddDelivery = new Guna2Button();
            this.Filter = new Guna2ComboBox();
            this.btnSearch = new Guna2Button();
            this.txtSearch = new Guna2TextBox();

            this.mainpanel.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).BeginInit();
            this.SuspendLayout();

            // mainpanel
            this.mainpanel.Controls.Add(this.guna2Panel1);
            this.mainpanel.Controls.Add(this.guna2ShadowPanel1);
            this.mainpanel.Controls.Add(this.Export);
            this.mainpanel.Controls.Add(this.btnAddDelivery);
            this.mainpanel.Controls.Add(this.Filter);
            this.mainpanel.Controls.Add(this.btnSearch);
            this.mainpanel.Controls.Add(this.txtSearch);
            this.mainpanel.FillColor = Color.White;
            this.mainpanel.Location = new Point(300, 88);
            this.mainpanel.Radius = 8;
            this.mainpanel.Size = new Size(1602, 878);

            // Pagination
            this.guna2Panel1.Location = new Point(77, 826);
            this.guna2Panel1.Size = new Size(1458, 36);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.btngreaterthan);
            this.guna2Panel1.Controls.Add(this.btn9); this.guna2Panel1.Controls.Add(this.btn8);
            this.guna2Panel1.Controls.Add(this.btn7); this.guna2Panel1.Controls.Add(this.btn6);
            this.guna2Panel1.Controls.Add(this.btn5); this.guna2Panel1.Controls.Add(this.btn4);
            this.guna2Panel1.Controls.Add(this.btn3); this.guna2Panel1.Controls.Add(this.btn2);
            this.guna2Panel1.Controls.Add(this.btn1); this.guna2Panel1.Controls.Add(this.btnlessthan);

            this.label1.AutoSize = true;
            this.label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            this.label1.ForeColor = Color.Gray;
            this.label1.Location = new Point(16, 8);
            this.label1.Text = "Showing 1-10 of 100";

            SetupPaginationButton(btngreaterthan, ">", 1408);
            SetupPaginationButton(btn9, "9", 1378); SetupPaginationButton(btn8, "8", 1348);
            SetupPaginationButton(btn7, "7", 1318); SetupPaginationButton(btn6, "6", 1288);
            SetupPaginationButton(btn5, "5", 1258); SetupPaginationButton(btn4, "4", 1228);
            SetupPaginationButton(btn3, "3", 1198); SetupPaginationButton(btn2, "2", 1168);
            SetupPaginationButton(btn1, "1", 1138); SetupPaginationButton(btnlessthan, "<", 1108);

            // Grid panel
            this.guna2ShadowPanel1.FillColor = Color.White;
            this.guna2ShadowPanel1.Location = new Point(77, 104);
            this.guna2ShadowPanel1.Size = new Size(1458, 716);
            this.guna2ShadowPanel1.Radius = 5;
            this.guna2ShadowPanel1.Controls.Add(this.dgvDeliveries);

            // dgvDeliveries
            this.dgvDeliveries.AllowUserToAddRows = false;
            this.dgvDeliveries.AllowUserToResizeColumns = false;
            this.dgvDeliveries.AllowUserToResizeRows = false;
            this.dgvDeliveries.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvDeliveries.ColumnHeadersHeight = 40;
            this.dgvDeliveries.GridColor = Color.FromArgb(231, 229, 255);
            this.dgvDeliveries.Location = new Point(22, 27);
            this.dgvDeliveries.Size = new Size(1412, 662);
            this.dgvDeliveries.RowHeadersVisible = false;
            this.dgvDeliveries.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgvDeliveries_CellPainting);
            this.dgvDeliveries.CellClick += new DataGridViewCellEventHandler(this.dgvDeliveries_CellClick);

            // Columns
            this.colDeliveryID.HeaderText = "ID"; this.colDeliveryID.Name = "colDeliveryID";
            this.colOrderID.HeaderText = "Order ID"; this.colOrderID.Name = "colOrderID";
            this.colCustomer.HeaderText = "Customer"; this.colCustomer.Name = "colCustomer";
            this.colDeliveryDate.HeaderText = "Delivery Date"; this.colDeliveryDate.Name = "colDeliveryDate";
            this.colEmployee.HeaderText = "Employee"; this.colEmployee.Name = "colEmployee";
            this.colVehicle.HeaderText = "Vehicle"; this.colVehicle.Name = "colVehicle";
            this.colStatus.HeaderText = "Status"; this.colStatus.Name = "colStatus";
            this.colActions.HeaderText = "Actions"; this.colActions.Name = "colActions";
            this.colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgvDeliveries.Columns.AddRange(new DataGridViewColumn[] {
                colDeliveryID, colOrderID, colCustomer, colDeliveryDate, colEmployee, colVehicle, colStatus, colActions });

            // Top controls
            this.Export.Location = new Point(1400, 45); this.Export.Size = new Size(128, 50);
            this.Export.Font = new Font("Poppins", 10F); this.Export.BorderRadius = 8; this.Export.FillColor = Color.White;

            this.btnAddDelivery.Location = new Point(1202, 45); this.btnAddDelivery.Size = new Size(190, 38);
            this.btnAddDelivery.Text = "✚ Add Delivery";
            this.btnAddDelivery.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            this.btnAddDelivery.FillColor = Color.FromArgb(0, 123, 255);
            this.btnAddDelivery.ForeColor = Color.White;
            this.btnAddDelivery.BorderRadius = 8;
            this.btnAddDelivery.Click += new EventHandler(this.btnAddDelivery_Click); // ← ADDED THIS LINE

            this.Filter.Location = new Point(1065, 45); this.Filter.Size = new Size(128, 50);
            this.Filter.Font = new Font("Poppins", 10F); this.Filter.BorderRadius = 8; this.Filter.FillColor = Color.White;

            this.btnSearch.Location = new Point(537, 41); this.btnSearch.Size = new Size(103, 48);
            this.btnSearch.Text = "Search"; this.btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            this.btnSearch.ForeColor = Color.White; this.btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            this.btnSearch.BorderRadius = 5;

            this.txtSearch.Location = new Point(94, 41); this.txtSearch.Size = new Size(437, 48);
            this.txtSearch.PlaceholderText = "Search delivery ID, order, customer...";
            this.txtSearch.Font = new Font("Poppins", 10.5F); this.txtSearch.BorderRadius = 12;
            this.txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(this.mainpanel);
            this.Name = "DeliveryList";
            this.Text = "Delivery List";

            this.mainpanel.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).EndInit();
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

        private Guna2ShadowPanel mainpanel;
        private Guna2Panel guna2Panel1;
        private Label label1;
        private Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna2ShadowPanel guna2ShadowPanel1;
        private Guna2DataGridView dgvDeliveries;
        private DataGridViewCheckBoxColumn colDeliveryID;
        private DataGridViewTextBoxColumn colOrderID, colCustomer, colDeliveryDate, colEmployee, colVehicle, colStatus, colActions;
        private Guna2ComboBox Export, Filter;
        private Guna2Button btnAddDelivery, btnSearch;
        private Guna2TextBox txtSearch;
    }
}