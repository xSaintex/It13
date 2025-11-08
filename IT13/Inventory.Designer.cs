namespace IT13
{
    partial class Inventory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelTop = new Panel();
            dataGridView = new DataGridView();
            panelBottom = new Panel();
            btnLast = new Button();
            btnNext = new Button();
            lblPageInfo = new Label();
            btnPrev = new Button();
            btnFirst = new Button();
            btnExport = new Button();
            btnAddStock = new Button();
            cmbFilters = new ComboBox();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblTotal = new Label();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(dataGridView);
            panelTop.Controls.Add(panelBottom);
            panelTop.Controls.Add(btnExport);
            panelTop.Controls.Add(btnAddStock);
            panelTop.Controls.Add(cmbFilters);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(lblTotal);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1532, 1055);
            panelTop.TabIndex = 0;
            panelTop.Paint += panelTop_Paint;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.Location = new Point(564, 209);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(701, 406);
            dataGridView.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnLast);
            panelBottom.Controls.Add(btnNext);
            panelBottom.Controls.Add(lblPageInfo);
            panelBottom.Controls.Add(btnPrev);
            panelBottom.Controls.Add(btnFirst);
            panelBottom.Location = new Point(564, 621);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(701, 39);
            panelBottom.TabIndex = 2;
            // 
            // btnLast
            // 
            btnLast.FlatAppearance.BorderColor = Color.FromArgb(128, 87, 255);
            btnLast.FlatStyle = FlatStyle.Flat;
            btnLast.Location = new Point(1120, 18);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(50, 30);
            btnLast.TabIndex = 4;
            btnLast.Text = ">>";
            btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(128, 87, 255);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Location = new Point(1070, 18);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(50, 30);
            btnNext.TabIndex = 3;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // lblPageInfo
            // 
            lblPageInfo.AutoSize = true;
            lblPageInfo.Font = new Font("Segoe UI", 9F);
            lblPageInfo.Location = new Point(304, 8);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(154, 20);
            lblPageInfo.TabIndex = 2;
            lblPageInfo.Text = "Showing 1-10 of 1000";
            lblPageInfo.Click += lblPageInfo_Click;
            // 
            // btnPrev
            // 
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(128, 87, 255);
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Location = new Point(59, 3);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(50, 30);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "<";
            btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnFirst
            // 
            btnFirst.FlatAppearance.BorderColor = Color.FromArgb(128, 87, 255);
            btnFirst.FlatStyle = FlatStyle.Flat;
            btnFirst.Location = new Point(3, 3);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(50, 30);
            btnFirst.TabIndex = 0;
            btnFirst.Text = "<<";
            btnFirst.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(128, 87, 255);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(1238, 123);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(70, 31);
            btnExport.TabIndex = 5;
            btnExport.Text = "Export Data ▼";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnAddStock
            // 
            btnAddStock.BackColor = Color.FromArgb(128, 87, 255);
            btnAddStock.FlatAppearance.BorderSize = 0;
            btnAddStock.FlatStyle = FlatStyle.Flat;
            btnAddStock.Font = new Font("Segoe UI", 9F);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Location = new Point(1172, 123);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(60, 31);
            btnAddStock.TabIndex = 4;
            btnAddStock.Text = "+ Add Stock";
            btnAddStock.UseVisualStyleBackColor = false;
            // 
            // cmbFilters
            // 
            cmbFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilters.FlatStyle = FlatStyle.Flat;
            cmbFilters.FormattingEnabled = true;
            cmbFilters.Items.AddRange(new object[] { "All", "Active", "Inactive", "Low Stock", "No Stock" });
            cmbFilters.Location = new Point(1006, 127);
            cmbFilters.Name = "cmbFilters";
            cmbFilters.Size = new Size(160, 28);
            cmbFilters.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(128, 87, 255);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(888, 129);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(70, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Location = new Point(623, 129);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search...";
            txtSearch.Size = new Size(250, 30);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F);
            lblTotal.ForeColor = Color.FromArgb(128, 87, 255);
            lblTotal.Location = new Point(562, 131);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(55, 23);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "12345";
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 252);
            ClientSize = new Size(1532, 1055);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1000, 600);
            Name = "Inventory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventory";
            Load += Inventory_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbFilters;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
    }
}