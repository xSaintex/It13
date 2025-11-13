// ---------------------------------------------------------------------
// inven.cs – FINAL FIXED (Fits 100% in pnlContent)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class inven : Form
    {
        private CheckBox headerCheckBox;

        public inven()
        {
            InitializeComponent();
            SetupResponsiveLayout(); // ← Auto-fit everything
            InitializeData();
        }

        private void SetupResponsiveLayout()
        {
            // === MAIN PANEL: FILL ENTIRE FORM ===
            mainpanel.Dock = DockStyle.Fill;
            mainpanel.Location = Point.Empty;
            mainpanel.Margin = new Padding(0);
            mainpanel.Padding = new Padding(20); // Inner spacing

            // === SEARCH BAR (Top Row) ===
            txtboxsearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtboxsearch.Left = 0;
            txtboxsearch.Top = 0;
            txtboxsearch.Width = mainpanel.ClientSize.Width - 600;

            btnsearchinv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnsearchinv.Top = 0;
            btnsearchinv.Left = txtboxsearch.Right + 10;

            ComBoxFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ComBoxFilters.Top = 0;
            ComBoxFilters.Left = btnsearchinv.Right + 10;

            btnaddstock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnaddstock.Top = 0;
            btnaddstock.Left = ComBoxFilters.Right + 10;

            ComBoxExporData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ComBoxExporData.Top = 0;
            ComBoxExporData.Left = btnaddstock.Right + 10;

            // === DATA GRID PANEL ===
            guna2ShadowPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            guna2ShadowPanel1.Top = 70;
            guna2ShadowPanel1.Left = 0;
            guna2ShadowPanel1.Width = mainpanel.ClientSize.Width;
            guna2ShadowPanel1.Height = mainpanel.ClientSize.Height - 150;

            datagridviewcustorder.Dock = DockStyle.Fill;
            datagridviewcustorder.Margin = new Padding(20);

            // === PAGINATION PANEL ===
            guna2Panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2Panel1.Height = 50;
            guna2Panel1.Left = 0;
            guna2Panel1.Width = mainpanel.ClientSize.Width;
            guna2Panel1.Top = mainpanel.ClientSize.Height - guna2Panel1.Height - 20;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetupResponsiveLayout();
        }

        private void InitializeData()
        {
            // ComboBox setup
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.Add("Filter");
            ComBoxFilters.SelectedIndex = 0;
            ComBoxFilters.ForeColor = Color.Black;

            ComBoxExporData.Items.Clear();
            ComBoxExporData.Items.Add("Export Data");
            ComBoxExporData.SelectedIndex = 0;
            ComBoxExporData.ForeColor = Color.Black;

            // Add checkbox column
            if (datagridviewcustorder.Columns.Count == 0 || 
                datagridviewcustorder.Columns[0] is not DataGridViewCheckBoxColumn)
            {
                var checkCol = new DataGridViewCheckBoxColumn
                {
                    HeaderText = "",
                    Name = "checkboxColumn",
                    Width = 50
                };
                datagridviewcustorder.Columns.Insert(0, checkCol);
            }

            // Sample data
            datagridviewcustorder.Rows.Add(false, "1", "cctv", "Dome Cameras", "43 pcs.", "₱200.00", "₱8,600.00", "Verizon", "Active");
            datagridviewcustorder.Rows.Add(false, "2", "cctv", "Bullet Cameras", "18 pcs.", "₱300.00", "₱5,400.00", "AT&T", "Active");

            // Header checkbox
            datagridviewcustorder.CellPainting += DataGridView_CellPainting;
            datagridviewcustorder.CellClick += DataGridView_CellClick;
            datagridviewcustorder.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;

            UpdateRowCount();
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                if (headerCheckBox == null)
                {
                    headerCheckBox = new CheckBox { Size = new Size(18, 18), BackColor = Color.Transparent };
                    headerCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;
                    datagridviewcustorder.Controls.Add(headerCheckBox);
                }
                var loc = new Point(
                    e.CellBounds.Left + (e.CellBounds.Width - 18) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - 18) / 2
                );
                headerCheckBox.Location = loc;
                e.Handled = true;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                headerCheckBox.Checked = !headerCheckBox.Checked;
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (datagridviewcustorder.IsCurrentCellDirty)
                datagridviewcustorder.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            datagridviewcustorder.EndEdit();
            foreach (DataGridViewRow row in datagridviewcustorder.Rows)
                if (!row.IsNewRow)
                    row.Cells[0].Value = headerCheckBox.Checked;
            datagridviewcustorder.RefreshEdit();
        }

        private void UpdateRowCount()
        {
            labelshow.Text = $"Showing {datagridviewcustorder.Rows.Count} items";
        }

        private void btnaddstock_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Add Stock";
                var addStock = new AddStock
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(addStock);
                addStock.Show();
            }
        }

        // Empty event stubs (keep if needed)
        private void txtboxsearch_TextChanged(object sender, EventArgs e) { }
        private void datagridviewinventory_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e) { }
    }
}