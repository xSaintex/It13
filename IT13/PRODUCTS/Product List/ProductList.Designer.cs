namespace IT13
{
    partial class ProductList
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
                Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                Font = new Font("Bahnschrift SemiCondensed", 11F),
                ForeColor = Color.Black,
                BackColor = Color.White,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black
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
            datagridviewinventory = new Guna.UI2.WinForms.Guna2DataGridView();
            Column1 = new DataGridViewCheckBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            CombExport = new Guna.UI2.WinForms.Guna2ComboBox();
            btnaddstock = new Guna.UI2.WinForms.Guna2Button();
            ComBoxFilters = new Guna.UI2.WinForms.Guna2ComboBox();
            btnsearch = new Guna.UI2.WinForms.Guna2Button();
            txtboxsearch = new Guna.UI2.WinForms.Guna2TextBox();

            mainpanel.SuspendLayout();
            guna2Panel1.SuspendLayout();
            guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridviewinventory).BeginInit();
            SuspendLayout();

            // mainpanel
            mainpanel.BackColor = Color.Transparent;
            mainpanel.Controls.Add(guna2Panel1);
            mainpanel.Controls.Add(guna2ShadowPanel1);
            mainpanel.Controls.Add(CombExport);
            mainpanel.Controls.Add(btnaddstock);
            mainpanel.Controls.Add(ComBoxFilters);
            mainpanel.Controls.Add(btnsearch);
            mainpanel.Controls.Add(txtboxsearch);
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(299, 92);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 8;
            mainpanel.ShadowColor = Color.Black;
            mainpanel.Size = new Size(1602, 871);
            mainpanel.TabIndex = 2;

            // Pagination Panel
            guna2Panel1.Location = new Point(77, 826);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.Size = new Size(1458, 36);
            guna2Panel1.TabIndex = 6;
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

            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 9F, FontStyle.Bold);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(16, 8);
            label1.Text = "Showing 1-10 of 100";

            // Pagination Buttons (exact same as StockAdjustment)
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

            // Grid Panel
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.Controls.Add(datagridviewinventory);

            // DataGridView
            // DataGridView
            datagridviewinventory.AllowUserToAddRows = false;
            datagridviewinventory.AllowUserToResizeColumns = false;
            datagridviewinventory.AllowUserToResizeRows = false;
            datagridviewinventory.ColumnHeadersDefaultCellStyle = headerStyle;
            datagridviewinventory.ColumnHeadersHeight = 40;
            datagridviewinventory.DefaultCellStyle = rowStyle;
            datagridviewinventory.GridColor = Color.FromArgb(231, 229, 255);
            datagridviewinventory.Location = new Point(22, 27);
            datagridviewinventory.Name = "datagridviewinventory";
            datagridviewinventory.RowHeadersVisible = false;
            datagridviewinventory.Size = new Size(1412, 662);
            datagridviewinventory.TabIndex = 0;

            // THESE 3 LINES ARE CRITICAL — PUT THEM HERE:
            datagridviewinventory.CellPainting += datagridviewinventory_CellPainting;
            datagridviewinventory.CellClick += datagridviewinventory_CellClick;
            datagridviewinventory.MouseDown += (s, e) => datagridviewinventory.ClearSelection();   // ← THIS ONE!

            // Columns - Title Case (NO ALL CAPS)
            Column1.HeaderText = "PID"; Column1.Name = "Column1";
            Column2.HeaderText = "Product Name"; Column2.Name = "Column2";
            Column3.HeaderText = "Category"; Column3.Name = "Column3";
            Column4.HeaderText = "Unit Cost"; Column4.Name = "Column4";
            Column5.HeaderText = "Selling Price"; Column5.Name = "Column5";
            Column6.HeaderText = "Primary Supplier"; Column6.Name = "Column6";
            Column7.HeaderText = "Status"; Column7.Name = "Column7";
            Column8.HeaderText = "Actions"; Column8.Name = "Column8";
            Column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            datagridviewinventory.Columns.AddRange(new DataGridViewColumn[] {
                Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8 });

            // Top Controls - EXACT SAME AS StockAdjustment
            txtboxsearch.Location = new Point(94, 41);
            txtboxsearch.Size = new Size(437, 48);
            txtboxsearch.PlaceholderText = "Search product name, PID or category...";
            txtboxsearch.PlaceholderForeColor = Color.FromArgb(80, 80, 80);
            txtboxsearch.BorderRadius = 12;
            txtboxsearch.Font = new Font("Poppins", 10.5F);

            btnsearch.Location = new Point(537, 41);
            btnsearch.Size = new Size(103, 48);
            btnsearch.Text = "Search";
            btnsearch.FillColor = Color.FromArgb(0, 123, 255);
            btnsearch.ForeColor = Color.White;
            btnsearch.BorderRadius = 5;
            btnsearch.Font = new Font("Poppins", 10F, FontStyle.Bold);

            ComBoxFilters.Location = new Point(1065, 45);
            ComBoxFilters.Size = new Size(128, 50);
            ComBoxFilters.BorderRadius = 8;
            ComBoxFilters.FillColor = Color.White;
            ComBoxFilters.Font = new Font("Poppins", 10F);

            btnaddstock.Location = new Point(1202, 45);
            btnaddstock.Size = new Size(190, 38);
            btnaddstock.Text = "✚ Add Product";
            btnaddstock.FillColor = Color.FromArgb(0, 123, 255);
            btnaddstock.ForeColor = Color.White;
            btnaddstock.BorderRadius = 8;
            btnaddstock.Font = new Font("Poppins", 10.5F, FontStyle.Bold);

            CombExport.Location = new Point(1400, 45);
            CombExport.Size = new Size(128, 50);
            CombExport.BorderRadius = 8;
            CombExport.FillColor = Color.White;
            CombExport.Font = new Font("Poppins", 10F);

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1906, 1033);
            Controls.Add(mainpanel);
            Name = "ProductList";
            Text = "Product List";

            mainpanel.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagridviewinventory).EndInit();
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
        private Guna.UI2.WinForms.Guna2DataGridView datagridviewinventory;
        private DataGridViewCheckBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private Guna.UI2.WinForms.Guna2ComboBox CombExport;
        private Guna.UI2.WinForms.Guna2Button btnaddstock;
        private Guna.UI2.WinForms.Guna2ComboBox ComBoxFilters;
        private Guna.UI2.WinForms.Guna2Button btnsearch;
        private Guna.UI2.WinForms.Guna2TextBox txtboxsearch;
    }
}