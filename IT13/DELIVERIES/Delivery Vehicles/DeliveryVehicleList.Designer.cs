// ---------------------------------------------------------------------
// DeliveryVehicleList.designer.cs 
// ---------------------------------------------------------------------
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class DeliveryVehicleList
    {
        private DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(12, 57, 101),
            ForeColor = Color.White,
            Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        private System.ComponentModel.IContainer components = null;

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
            dgvVehicles = new Guna2DataGridView();

            colID = new DataGridViewCheckBoxColumn();
            colVehicleName = new DataGridViewTextBoxColumn();
            colPlateNumber = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();
            colUpdatedAt = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();

            btnAddVehicle = new Guna2Button();
            Filter = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            txtSearch = new Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            this.SuspendLayout();

            // ===================== MAIN PANEL =====================
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(btnAddVehicle);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);

            // ===================== PAGINATION =====================
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(btngreaterthan);
            guna2Panel1.Controls.Add(btn9); guna2Panel1.Controls.Add(btn8);
            guna2Panel1.Controls.Add(btn7); guna2Panel1.Controls.Add(btn6);
            guna2Panel1.Controls.Add(btn5); guna2Panel1.Controls.Add(btn4);
            guna2Panel1.Controls.Add(btn3); guna2Panel1.Controls.Add(btn2);
            guna2Panel1.Controls.Add(btn1); guna2Panel1.Controls.Add(btnlessthan);

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

            // ===================== GRID PANEL =====================
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(dgvVehicles);

            // ===================== DATAGRIDVIEW =====================
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToResizeColumns = false;
            dgvVehicles.AllowUserToResizeRows = false;
            dgvVehicles.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvVehicles.ColumnHeadersHeight = 40;
            dgvVehicles.GridColor = Color.FromArgb(231, 229, 255);
            dgvVehicles.Location = new Point(22, 27);
            dgvVehicles.Size = new Size(1412, 662);
            dgvVehicles.RowHeadersVisible = false;
            dgvVehicles.BackgroundColor = Color.White;
            dgvVehicles.RowTemplate.Height = 45;
            dgvVehicles.CellPainting += dgvVehicles_CellPainting;
            dgvVehicles.CellClick += dgvVehicles_CellClick;

            // ===================== COLUMNS =====================
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.Width = 160;
            colID.Resizable = DataGridViewTriState.False;

            colVehicleName.HeaderText = "Vehicle Name";
            colVehicleName.Name = "colVehicleName";
            colVehicleName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            colPlateNumber.HeaderText = "Plate Number";
            colPlateNumber.Name = "colPlateNumber";
            colPlateNumber.Width = 180;

            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.Width = 140;

            colCreatedAt.HeaderText = "Created At";
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.Width = 160;

            colUpdatedAt.HeaderText = "Updated At";
            colUpdatedAt.Name = "colUpdatedAt";
            colUpdatedAt.Width = 160;

            colActions.HeaderText = "Actions";
            colActions.Name = "colActions";
            colActions.Width = 120;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVehicles.Columns.AddRange(new DataGridViewColumn[]
            {
                colID, colVehicleName, colPlateNumber, colStatus, colCreatedAt, colUpdatedAt, colActions
            });

            // ===================== TOP CONTROLS =====================
            btnAddVehicle.Location = new Point(1340, 45);
            btnAddVehicle.Size = new Size(190, 38);
            btnAddVehicle.Text = "✚ Add Vehicle";
            btnAddVehicle.FillColor = Color.FromArgb(0, 123, 255);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.BorderRadius = 8;
            btnAddVehicle.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnAddVehicle.Click += btnAddVehicle_Click;

            Filter.Location = new Point(1203, 45);
            Filter.Size = new Size(128, 50);
            Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            Filter.Font = new Font("Poppins", 10F);
            Filter.BorderRadius = 8;
            Filter.FillColor = Color.White;

            btnSearch.Location = new Point(537, 41);
            btnSearch.Size = new Size(103, 48);
            btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.BorderRadius = 5;
            btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnSearch.Click += btnSearch_Click;

            txtSearch.Location = new Point(94, 41);
            txtSearch.Size = new Size(437, 48);
            txtSearch.PlaceholderText = "Search vehicle name, plate number...";
            txtSearch.Font = new Font("Poppins", 10.5F);
            txtSearch.BorderRadius = 12;
            txtSearch.ForeColor = Color.Black;
            txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);

            // ===================== FORM =====================
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "DeliveryVehicleList";
            this.Load += DeliveryVehicleList_Load;

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
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

        private void DeliveryVehicleList_Load(object sender, EventArgs e) { }

        // ===================== CONTROLS =====================
        private Guna2ShadowPanel mainpanel;
        private Guna2Panel guna2Panel1;
        private Label label1;
        private Guna2Button btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan;
        private Guna2ShadowPanel guna2ShadowPanel1;
        private Guna2DataGridView dgvVehicles;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colVehicleName, colPlateNumber, colStatus, colCreatedAt, colUpdatedAt, colActions;
        private Guna2Button btnAddVehicle;
        private Guna2ComboBox Filter;
        private Guna2Button btnSearch;
        private Guna2TextBox txtSearch;
    }
}