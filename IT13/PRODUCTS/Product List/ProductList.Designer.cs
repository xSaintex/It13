// ProductList.Designer.cs - Updated Column Headers
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();

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

            // MAIN PANEL
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
            guna2Panel1.TabIndex = 6;

            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            label1.Location = new Point(17, 9);
            label1.Name = "label1";
            label1.Size = new Size(165, 18);
            label1.Text = "Showing 1-10 of 100";

            // PAGINATION BUTTONS
            SetupPaginationButton(btngreaterthan, ">", 1391);
            SetupPaginationButton(btn9, "9", 1361);
            SetupPaginationButton(btn8, "8", 1331);
            SetupPaginationButton(btn7, "7", 1301);
            SetupPaginationButton(btn6, "6", 1271);
            SetupPaginationButton(btn5, "5", 1241);
            SetupPaginationButton(btn4, "4", 1211);
            SetupPaginationButton(btn3, "3", 1181);
            SetupPaginationButton(btn2, "2", 1151);
            SetupPaginationButton(btn1, "1", 1121);
            SetupPaginationButton(btnlessthan, "<", 1091);

            // DATAGRIDVIEW PANEL
            guna2ShadowPanel1.BackColor = Color.Transparent;
            guna2ShadowPanel1.Controls.Add(datagridviewinventory);
            guna2ShadowPanel1.FillColor = Color.White;
            guna2ShadowPanel1.Location = new Point(77, 104);
            guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            guna2ShadowPanel1.Radius = 5;
            guna2ShadowPanel1.ShadowColor = Color.Black;
            guna2ShadowPanel1.Size = new Size(1458, 716);
            guna2ShadowPanel1.TabIndex = 5;

            // DATAGRIDVIEW
            datagridviewinventory.AllowUserToAddRows = false;
            datagridviewinventory.AllowUserToResizeColumns = false;
            datagridviewinventory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            datagridviewinventory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Tahoma", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            datagridviewinventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            datagridviewinventory.ColumnHeadersHeight = 40;
            datagridviewinventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            datagridviewinventory.DefaultCellStyle = dataGridViewCellStyle3;
            datagridviewinventory.GridColor = Color.FromArgb(231, 229, 255);
            datagridviewinventory.Location = new Point(31, 35);
            datagridviewinventory.Name = "datagridviewinventory";
            datagridviewinventory.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            datagridviewinventory.RowHeadersVisible = false;
            datagridviewinventory.Size = new Size(1412, 662);
            datagridviewinventory.TabIndex = 0;
            datagridviewinventory.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            datagridviewinventory.ThemeStyle.AlternatingRowsStyle.Font = null;
            datagridviewinventory.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            datagridviewinventory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            datagridviewinventory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            datagridviewinventory.ThemeStyle.BackColor = Color.White;
            datagridviewinventory.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            datagridviewinventory.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            datagridviewinventory.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            datagridviewinventory.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            datagridviewinventory.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            datagridviewinventory.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            datagridviewinventory.ThemeStyle.HeaderStyle.Height = 40;
            datagridviewinventory.ThemeStyle.ReadOnly = false;
            datagridviewinventory.ThemeStyle.RowsStyle.BackColor = Color.White;
            datagridviewinventory.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            datagridviewinventory.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            datagridviewinventory.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            datagridviewinventory.ThemeStyle.RowsStyle.Height = 29;
            datagridviewinventory.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            datagridviewinventory.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            datagridviewinventory.CellContentClick += datagridviewinventory_CellContentClick;

            // COLUMNS - Updated Headers
            Column1.HeaderText = "PID";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";

            Column2.HeaderText = "PRODUCT NAME";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";

            Column3.HeaderText = "CATEGORY";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";

            Column4.HeaderText = "UNIT COST";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";

            Column5.HeaderText = "SELLING PRICE";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";

            Column6.HeaderText = "PRIMARY SUPPLIER";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";

            Column7.HeaderText = "STATUS";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";

            Column8.HeaderText = "ACTIONS";
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";

            // EXPORT COMBOBOX
            CombExport.BackColor = Color.Transparent;
            CombExport.BorderRadius = 5;
            CombExport.CustomizableEdges = customizableEdges1;
            CombExport.DrawMode = DrawMode.OwnerDrawFixed;
            CombExport.DropDownStyle = ComboBoxStyle.DropDownList;
            CombExport.Font = new Font("Tahoma", 10.2F);
            CombExport.ForeColor = Color.Silver;
            CombExport.ItemHeight = 30;
            CombExport.Location = new Point(1378, 45);
            CombExport.Name = "CombExport";
            CombExport.ShadowDecoration.CustomizableEdges = customizableEdges2;
            CombExport.Size = new Size(157, 36);
            CombExport.TabIndex = 7;

            // ADD PRODUCT BUTTON
            btnaddstock.BorderRadius = 5;
            btnaddstock.CustomizableEdges = customizableEdges3;
            btnaddstock.Font = new Font("Tahoma", 9F);
            btnaddstock.ForeColor = Color.White;
            btnaddstock.Location = new Point(1215, 45);
            btnaddstock.Name = "btnaddstock";
            btnaddstock.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnaddstock.Size = new Size(137, 36);
            btnaddstock.TabIndex = 3;
            btnaddstock.Text = "+Add Product";

            // FILTER COMBOBOX
            ComBoxFilters.BackColor = Color.Transparent;
            ComBoxFilters.BorderRadius = 5;
            ComBoxFilters.DrawMode = DrawMode.OwnerDrawFixed;
            ComBoxFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            ComBoxFilters.Font = new Font("Tahoma", 10.2F);
            ComBoxFilters.ForeColor = Color.Silver;
            ComBoxFilters.ItemHeight = 30;
            ComBoxFilters.Location = new Point(1072, 45);
            ComBoxFilters.Name = "ComBoxFilters";
            ComBoxFilters.Size = new Size(128, 36);
            ComBoxFilters.TabIndex = 2;

            // SEARCH BUTTON
            btnsearch.BorderRadius = 5;
            btnsearch.Font = new Font("Tahoma", 9F);
            btnsearch.ForeColor = Color.White;
            btnsearch.Location = new Point(520, 41);
            btnsearch.Name = "btnsearch";
            btnsearch.Size = new Size(103, 40);
            btnsearch.TabIndex = 1;
            btnsearch.Text = "Search";

            // SEARCH TEXTBOX
            txtboxsearch.BackColor = Color.White;
            txtboxsearch.BorderColor = Color.LightGray;
            txtboxsearch.BorderRadius = 5;
            txtboxsearch.Font = new Font("Segoe UI", 9F);
            txtboxsearch.Location = new Point(77, 41);
            txtboxsearch.Margin = new Padding(3, 4, 3, 4);
            txtboxsearch.Name = "txtboxsearch";
            txtboxsearch.PlaceholderText = "Search for products";
            txtboxsearch.Size = new Size(437, 40);
            txtboxsearch.TabIndex = 0;

            // FORM
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1906, 1033);
            Controls.Add(mainpanel);
            Name = "ProductList";
            Text = "ProductList";

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
            btn.Location = new Point(left, 6);
            btn.Size = new Size(30, 28);
            btn.FillColor = Color.WhiteSmoke;
            btn.ForeColor = Color.Black;
            btn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn.BorderRadius = 8;
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