// ProductCategory.designer.cs
namespace IT13
{
    partial class ProductCategory
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
                BackColor = Color.FromArgb(12, 57, 101),
                ForeColor = Color.White,
                Font = new Font("Poppins", 12F, FontStyle.Bold),
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
            datagridviewcategory = new Guna.UI2.WinForms.Guna2DataGridView();

            colID = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();

            Export = new Guna.UI2.WinForms.Guna2ComboBox();
            btnaddcategory = new Guna.UI2.WinForms.Guna2Button();
            Filter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnsearchcat = new Guna.UI2.WinForms.Guna2Button();
            txtboxsearch = new Guna.UI2.WinForms.Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(datagridviewcategory)).BeginInit();
            SuspendLayout();

            // MAIN PANEL
            mainpanel.BackColor = Color.Transparent;
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(Export);
            mainpanel.Controls.Add(btnaddcategory);
            mainpanel.Controls.Add(Filter);
            mainpanel.Controls.Add(btnsearchcat);
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
            label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            label1.Location = new Point(16, 8);
            label1.ForeColor = Color.Gray;
            label1.Text = "Showing 1-10 of 100";

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

            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(datagridviewcategory);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.TabIndex = 5;

            datagridviewcategory.AllowUserToAddRows = false;
            datagridviewcategory.AllowUserToResizeColumns = false;
            datagridviewcategory.AllowUserToResizeRows = false;
            datagridviewcategory.ColumnHeadersDefaultCellStyle = headerStyle;
            datagridviewcategory.ColumnHeadersHeight = 40;
            datagridviewcategory.GridColor = Color.FromArgb(231, 229, 255);
            datagridviewcategory.Location = new Point(22, 27);
            datagridviewcategory.Name = "datagridviewcategory";
            datagridviewcategory.RowHeadersVisible = false;
            datagridviewcategory.Size = new Size(1412, 662);
            datagridviewcategory.TabIndex = 0;
            datagridviewcategory.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(12, 57, 101);
            datagridviewcategory.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            datagridviewcategory.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            datagridviewcategory.CellPainting += new DataGridViewCellPaintingEventHandler(datagridviewcategory_CellPainting);
            datagridviewcategory.CellClick += new DataGridViewCellEventHandler(datagridviewcategory_CellClick);

            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.Width = 160;
            colName.HeaderText = "Category Name";
            colName.Name = "colName";
            colName.Width = 500;
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.Width = 180;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.Width = 140;
            colActions.HeaderText = "Actions";
            colActions.Name = "colActions";
            colActions.Width = 120;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridviewcategory.Columns.AddRange(new DataGridViewColumn[] {
            colID, colName, colDate, colStatus, colActions});

            Export.Location = new Point(1400, 45);
            Export.Size = new Size(128, 48); // now 48px height
            Export.DropDownStyle = ComboBoxStyle.DropDownList;
            Export.Font = new Font("Poppins", 10F);
            Export.ItemHeight = 36;
            Export.BorderRadius = 8;
            Export.BorderColor = Color.FromArgb(200, 200, 200);
            Export.BorderThickness = 1;
            Export.FillColor = Color.White;
            Export.ForeColor = Color.FromArgb(50, 50, 50);
            Export.TabIndex = 4;

            btnaddcategory.Location = new Point(1202, 45);
            btnaddcategory.Size = new Size(190, 43); // now 48px height
            btnaddcategory.Text = "✚ Add Category";
            btnaddcategory.Click += new EventHandler(btnaddcategory_Click);
            btnaddcategory.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnaddcategory.Padding = new Padding(8, 0, 8, 0);
            btnaddcategory.FillColor = Color.FromArgb(0, 123, 255);
            btnaddcategory.ForeColor = Color.White;
            btnaddcategory.BorderRadius = 8; // slightly more rounded = looks expensive
            btnaddcategory.BorderThickness = 0;

            Filter.Location = new Point(1065, 45);
            Filter.Size = new Size(128, 48); // now 48px height
            Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            Filter.Font = new Font("Poppins", 10F);
            Filter.ItemHeight = 36;
            Filter.BorderRadius = 8;
            Filter.BorderColor = Color.FromArgb(200, 200, 200);
            Filter.BorderThickness = 1;
            Filter.FillColor = Color.White;
            Filter.ForeColor = Color.FromArgb(50, 50, 50);
            Filter.TabIndex = 2;

            btnsearchcat.Location = new Point(537, 41);
            btnsearchcat.Size = new Size(103, 48);
            btnsearchcat.Text = "Search";
            btnsearchcat.FillColor = Color.FromArgb(0, 123, 255);
            btnsearchcat.ForeColor = Color.White;
            btnsearchcat.Font = new Font("Poppins", 10F, FontStyle.Bold);
            btnsearchcat.BorderRadius = 5;

            txtboxsearch.Location = new Point(94, 41);
            txtboxsearch.PlaceholderText = "Search Category";
            txtboxsearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            txtboxsearch.Font = new Font("Poppins", 10.5F);
            txtboxsearch.Size = new Size(437, 48);
            txtboxsearch.BorderRadius = 12;
            txtboxsearch.BorderColor = Color.FromArgb(200, 200, 200);
            txtboxsearch.BorderThickness = 1;
            txtboxsearch.TextChanged += new EventHandler(txtboxsearch_TextChanged);

            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "ProductCategory";
            Text = "Product Category";
            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(datagridviewcategory)).EndInit();
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
            btn.Font = new Font("Poppins", 10F, FontStyle.Bold);
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
        private Guna.UI2.WinForms.Guna2DataGridView datagridviewcategory;
        private DataGridViewCheckBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colActions;
        private Guna.UI2.WinForms.Guna2ComboBox Export;
        private Guna.UI2.WinForms.Guna2Button btnaddcategory;
        private Guna.UI2.WinForms.Guna2ComboBox Filter;
        private Guna.UI2.WinForms.Guna2Button btnsearchcat;
        private Guna.UI2.WinForms.Guna2TextBox txtboxsearch;
    }
}