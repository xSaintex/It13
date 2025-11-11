using Guna.UI2.WinForms;

namespace IT13
{
    partial class SelectProductsModal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            mainPanel = new Guna2ShadowPanel();
            txtSearch = new Guna2TextBox();
            dgvProducts = new Guna2DataGridView();
            colCheck = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colAvail = new DataGridViewTextBoxColumn();
            btnCancel = new Guna2Button();
            btnAddSelected = new Guna2Button();

            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();

            // FORM
            ClientSize = new Size(800, 600);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
            Controls.Add(mainPanel);

            // MAIN PANEL
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.Controls.Add(txtSearch);
            mainPanel.Controls.Add(dgvProducts);
            mainPanel.Controls.Add(btnCancel);
            mainPanel.Controls.Add(btnAddSelected);

            // SEARCH BOX (Now at very top)
            txtSearch.Location = new Point(30, 20);
            txtSearch.Size = new Size(300, 36);
            txtSearch.PlaceholderText = "Search products...";

            // DATA GRID
            dgvProducts.Location = new Point(30, 70);
            dgvProducts.Size = new Size(740, 460);
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ColumnHeadersHeight = 40;
            dgvProducts.GridColor = Color.FromArgb(231, 229, 255);

            colCheck.HeaderText = ""; colCheck.Width = 50;
            colName.HeaderText = "PRODUCT NAME"; colName.Width = 250;
            colQty.HeaderText = "QTY"; colQty.Width = 80;
            colPrice.HeaderText = "PRICE"; colPrice.Width = 120;
            colAvail.HeaderText = "AVAILABLE"; colAvail.Width = 100;

            dgvProducts.Columns.AddRange(new DataGridViewColumn[]
            {
                colCheck, colName, colQty, colPrice, colAvail
            });

            // BUTTONS (Match SupplierOrderList style)
            btnCancel.Location = new Point(540, 540);
            btnCancel.Size = new Size(100, 40);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            btnAddSelected.Location = new Point(650, 540);
            btnAddSelected.Size = new Size(120, 40);
            btnAddSelected.Text = "Add Selected";
            btnAddSelected.FillColor = Color.FromArgb(0, 123, 255);
            btnAddSelected.ForeColor = Color.White;
            btnAddSelected.BorderRadius = 8;
            btnAddSelected.Click += btnAddSelected_Click;

            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainPanel;
        private Guna2TextBox txtSearch;
        private Guna2DataGridView dgvProducts;
        private DataGridViewCheckBoxColumn colCheck;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colAvail;
        private Guna2Button btnCancel;
        private Guna2Button btnAddSelected;
    }
}