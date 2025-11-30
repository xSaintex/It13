// ---------------------------------------------------------------------
// ViewSupplierOrder.cs
// ---------------------------------------------------------------------
using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewSupplierOrder : Form
    {
        public ViewSupplierOrder(string orderId)
        {
            InitializeComponent();
            LoadData(orderId);
            btnClose.Click += btnClose_Click;
        }

        private void LoadData(string orderId)
        {
            txtCompany.Text = "Incio";
            txtOrderDate.Text = "November 10, 2025";
            txtPayment.Text = "Net 30";
            txtEstDate.Text = "November 17, 2025";

            txtAddr1.Text = "123 Main St., Unit 456";
            txtAddr2.Text = "Bldg 7, Makati";
            txtCity.Text = "Makati City";
            txtState.Text = "Metro Manila";
            txtPostal.Text = "1229";
            txtCountry.Text = "Philippines";

            dgvItems.Rows.Add("HikVision Camera", 2, "₱2,000.00", 6, "₱4,000.00");
            dgvItems.Rows.Add("Logitech Mouse", 1, "₱200.00", 1, "₱200.00");

            decimal subtotal = 4200m;
            decimal discount = subtotal * 0.05m;
            decimal total = subtotal - discount + 500m;

            lblSubtotalVal.Text = $"₱{subtotal:F2}";
            txtDiscount.Text = "5";
            txtShipping.Text = "500";
            lblTotalVal.Text = $"₱{total:F2}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            if (this.Tag is OrderList)
            {
                parent.navBar1.PageTitle = "Orders";
                var list = new OrderList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(list);
                list.Show();
            }
            else
            {
                parent.navBar1.PageTitle = "Supplier Orders";
                var list = new SupplierOrderList { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(list);
                list.Show();
            }
        }
    }
}