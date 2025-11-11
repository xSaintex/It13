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
        }

        private void LoadData(string orderId)
        {
            // Simulate loading
            txtCompany.Text = "Incio";
            txtOrderDate.Text = "2025-11-10";
            txtPayment.Text = "Net 30";
            txtEstDate.Text = "2025-11-17";
            txtDiscount.Text = "5";
            txtShipping.Text = "500";

            dgvItems.Rows.Add("HikVision Camera", 2, "₱2,000.00", 6, "₱4,000.00");
            dgvItems.Rows.Add("Logitech Mouse", 1, "₱200.00", 1, "₱200.00");

            decimal subtotal = 4200m;
            decimal discount = subtotal * 0.05m;
            decimal total = subtotal - discount + 500m;

            lblSubtotalVal.Text = $"₱{subtotal:F2}";
            lblTotalVal.Text = $"₱{total:F2}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Supplier Orders";
            var list = new SupplierOrderList
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