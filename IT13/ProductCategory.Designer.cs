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
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold), // Bigger header
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
            ((System.ComponentModel.ISupportInitialize)datagridviewcategory).BeginInit();
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
            guna2ShadowPanel1.Controls.Add(datagridviewcategory);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.TabIndex = 5;

            // DATAGRIDVIEW
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
            datagridviewcategory.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            datagridviewcategory.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            datagridviewcategory.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            datagridviewcategory.CellPainting += datagridviewcategory_CellPainting;
            datagridviewcategory.CellClick += datagridviewcategory_CellClick;

            // COLUMNS – WIDER & CLEAN
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

            datagridviewcategory.Columns.AddRange(new DataGridViewColumn[]
            {
    colID, colName, colDate, colStatus, colActions
            });

            // EXPORT COMBOBOX – SHIFTED RIGHT
            Export.Location = new Point(1400, 45);
            Export.Size = new Size(128, 36);
            Export.DropDownStyle = ComboBoxStyle.DropDownList;
            Export.Font = new Font("Segoe UI", 10F);
            Export.ItemHeight = 30;
            Export.TabIndex = 4;

            // ADD CATEGORY BUTTON
            btnaddcategory.Location = new Point(1235, 45);
            btnaddcategory.Size = new Size(155, 36);
            btnaddcategory.Text = "+Add Category";
            btnaddcategory.Click += btnaddcategory_Click;
            btnaddcategory.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnaddcategory.Padding = new Padding(4, 0, 4, 0);
            btnaddcategory.FillColor = Color.FromArgb(0, 123, 255);
            btnaddcategory.ForeColor = Color.White;
            btnaddcategory.BorderRadius = 8;

            // FILTER COMBOBOX
            Filter.Location = new Point(1100, 45);
            Filter.Size = new Size(128, 36);
            Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            Filter.Font = new Font("Segoe UI", 10F);
            Filter.ItemHeight = 30;
            Filter.TabIndex = 2;

            // SEARCH BUTTON
            btnsearchcat.Location = new Point(537, 41);
            btnsearchcat.Size = new Size(103, 40);
            btnsearchcat.Text = "Search";
            btnsearchcat.FillColor = Color.FromArgb(0, 123, 255);
            btnsearchcat.ForeColor = Color.White;
            btnsearchcat.BorderRadius = 8;

            txtboxsearch.Location = new Point(94, 41);
            txtboxsearch.PlaceholderText = "Search Category";
            txtboxsearch.Size = new Size(437, 40);
            txtboxsearch.TextChanged += txtboxsearch_TextChanged;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "ProductCategory";
            Text = "Product Category";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagridviewcategory).EndInit();
            ResumeLayout(false);
        }

        // PAGINATION BUTTONS (via SetupPaginationButton)
        private void SetupPaginationButton(Guna.UI2.WinForms.Guna2Button btn, string text, int left)
        {
            btn.Text = text;
            btn.Location = new Point(left, 5);
            btn.Size = new Size(30, 28);
            btn.FillColor = Color.WhiteSmoke;
            btn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn.BorderRadius = 8;   
        }

        // CONTROL DECLARATIONS
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