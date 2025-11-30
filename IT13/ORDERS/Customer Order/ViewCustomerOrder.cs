// ViewCustomerOrder.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Linq;

namespace IT13
{
    public partial class ViewCustomerOrder : Form
    {
        private readonly string _orderId;
        private DataGridViewRow[] allItems; // To store original rows for filtering

        public ViewCustomerOrder(string orderId)
        {
            _orderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            InitializeComponent();

            btnCancel.Click += btnCancel_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearchProduct.TextChanged += txtSearchProduct_TextChanged;

            LoadOrderData();
            MakeReadOnlyButKeepSearchActive();
            CacheAllItems(); // Save original data for search
        }

        private void MakeReadOnlyButKeepSearchActive()
        {
            foreach (Control c in contentPanel.Controls)
            {
                if (c is Guna2TextBox tb && c != txtSearchProduct)
                    tb.Enabled = false;
                if (c is Guna2ComboBox || c is Guna2DateTimePicker || c is Guna2NumericUpDown)
                    c.Enabled = false;
                if (c is Guna2Button btn && btn != btnSearch && btn != btnCancel)
                    btn.Enabled = false;
            }

            // Keep search controls enabled
            txtSearchProduct.Enabled = true;
            btnSearch.Enabled = true;

            // Hide Add Product button
            btnAddProduct.Visible = false;
            btnAddProduct.Enabled = false;

            btnSave.Visible = false;
            btnCancel.Text = "Close";
            btnCancel.FillColor = Color.FromArgb(0, 123, 255);           // Bootstrap Primary Blue
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverState.FillColor = Color.FromArgb(0, 105, 235); // Slightly darker on hover
            btnCancel.BorderColor = Color.FromArgb(0, 123, 255);
            btnCancel.BorderThickness = 1;
        }

        private void LoadOrderData()
        {
            label1.Text = $"View Order: {_orderId}";

            cmbCompany.Text = "TechNova Corp";
            cmbPayment.Text = "Net 30";
            dateOrder.Value = DateTime.Today.AddDays(-5);
            dateEstimated.Value = DateTime.Today.AddDays(2);

            txtBillingAddress.Text =
                "789 Corporate Tower, Ayala Ave\r\nMakati City, Metro Manila 1226\r\nPhilippines";

            txtShippingAddress.Text =
                "456 Warehouse Lane, Pasay City\r\nMetro Manila 1300\r\nPhilippines";

            numDiscount.Minimum = 0; numDiscount.Maximum = 100; numDiscount.Value = 10;
            numShipping.Minimum = 0; numShipping.Maximum = 1000000; numShipping.Value = 1200;

            dgvItems.Rows.Clear();
            dgvItems.Rows.Add("Gaming Laptop X1", 1, "₱98,500.00", 8, "₱98,500.00");
            dgvItems.Rows.Add("USB-C Hub 7-in-1", 3, "₱2,800.00", 25, "₱8,400.00");
            dgvItems.Rows.Add("Wireless Keyboard", 2, "₱3,200.00", 40, "₱6,400.00");
            dgvItems.Rows.Add("27\" 4K Monitor", 4, "₱28,900.00", 15, "₱115,600.00");
            dgvItems.Rows.Add("Mechanical Keyboard RGB", 5, "₱5,800.00", 30, "₱29,000.00");

            lblSubtotalVal.Text = "₱351,800.00";
            lblTotalVal.Text = "₱318,338.00";
        }

        private void CacheAllItems()
        {
            allItems = new DataGridViewRow[dgvItems.Rows.Count];
            for (int i = 0; i < dgvItems.Rows.Count; i++)
                allItems[i] = dgvItems.Rows[i];
        }

        private void PerformSearch()
        {
            string search = txtSearchProduct.Text.Trim().ToLower();

            dgvItems.Rows.Clear();

            if (string.IsNullOrEmpty(search))
            {
                // Show all
                foreach (var row in allItems)
                    dgvItems.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value,
                                      row.Cells[3].Value, row.Cells[4].Value);
            }
            else
            {
                var filtered = allItems.Where(r =>
                    r.Cells[0].Value?.ToString().ToLower().Contains(search) == true
                ).ToArray();

                foreach (var row in filtered)
                    dgvItems.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value,
                                      row.Cells[3].Value, row.Cells[4].Value);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            // Optional: Live search as user types
            PerformSearch();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Customer Orders";

            var list = new CustomerOrderList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(list);
            list.Show();
        }
    }
}