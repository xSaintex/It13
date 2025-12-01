using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class UserAdmins
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
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            this.mainpanel = new Guna2ShadowPanel();
            this.guna2Panel1 = new Guna2Panel();
            this.label1 = new Label();
            this.btngreaterthan = new Guna2Button(); this.btn9 = new Guna2Button();
            this.btn8 = new Guna2Button(); this.btn7 = new Guna2Button(); this.btn6 = new Guna2Button();
            this.btn5 = new Guna2Button(); this.btn4 = new Guna2Button(); this.btn3 = new Guna2Button();
            this.btn2 = new Guna2Button(); this.btn1 = new Guna2Button(); this.btnlessthan = new Guna2Button();

            this.guna2ShadowPanel1 = new Guna2ShadowPanel();
            this.dgvUsers = new Guna2DataGridView();

            this.colID = new DataGridViewCheckBoxColumn();
            this.colUsername = new DataGridViewTextBoxColumn();
            this.colFullName = new DataGridViewTextBoxColumn();
            this.colEmail = new DataGridViewTextBoxColumn();
            this.colRole = new DataGridViewTextBoxColumn();
            this.colActions = new DataGridViewTextBoxColumn();

            this.btnAddUser = new Guna2Button();
            this.btnSearch = new Guna2Button();
            this.txtSearch = new Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            this.SuspendLayout();

            // mainpanel
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(btnAddUser);
            mainpanel.Controls.Add(btnSearch);
            mainpanel.Controls.Add(txtSearch);

            // Pagination Panel
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.AddRange(new Control[] { btngreaterthan, btn9, btn8, btn7, btn6, btn5, btn4, btn3, btn2, btn1, btnlessthan });

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

            // Grid Panel
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Controls.Add(dgvUsers);

            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToResizeColumns = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvUsers.ColumnHeadersHeight = 40;
            dgvUsers.GridColor = Color.FromArgb(231, 229, 255);
            dgvUsers.Location = new Point(22, 27);
            dgvUsers.Size = new Size(1412, 662);
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.CellPainting += dgvUsers_CellPainting;
            dgvUsers.CellClick += dgvUsers_CellClick;

            // Columns
            colID.HeaderText = "ID"; colID.Name = "colID";
            colUsername.HeaderText = "Username"; colUsername.Name = "colUsername";
            colFullName.HeaderText = "Full Name"; colFullName.Name = "colFullName";
            colEmail.HeaderText = "Email"; colEmail.Name = "colEmail";
            colRole.HeaderText = "Role"; colRole.Name = "colRole";
            colActions.HeaderText = "Actions"; colActions.Name = "colActions";
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUsers.Columns.AddRange(new DataGridViewColumn[] {
                colID, colUsername, colFullName, colEmail, colRole, colActions
            });

            // Top Controls
            btnAddUser.Location = new Point(1330, 45);
            btnAddUser.Size = new Size(200, 38);
            btnAddUser.Text = "✚ Add User";
            btnAddUser.FillColor = Color.FromArgb(0, 123, 255);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.BorderRadius = 8;
            btnAddUser.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnAddUser.Click += btnAddUserAdmins_Click;

            btnSearch.Location = new Point(537, 41);
            btnSearch.Size = new Size(103, 48);
            btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.BorderRadius = 5;
            btnSearch.Font = new Font("Poppins", 10F, FontStyle.Bold);

            txtSearch.Location = new Point(94, 41);
            txtSearch.Size = new Size(437, 48);
            txtSearch.PlaceholderText = "Search users...";
            txtSearch.Font = new Font("Poppins", 10.5F);
            txtSearch.BorderRadius = 12;
            txtSearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            txtSearch.TextChanged += txtSearch_TextChanged;

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "UserList";
            this.Text = "User List";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false); guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
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
        private Guna2DataGridView dgvUsers;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colUsername, colFullName, colEmail, colRole, colActions;
        private Guna2Button btnAddUser, btnSearch;
        private Guna2TextBox txtSearch;
    }
}