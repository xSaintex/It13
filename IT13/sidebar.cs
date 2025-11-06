using System;
using System.Drawing;
using System.Windows.Forms;
namespace IT13
{
    public partial class Sidebar : UserControl
    {
        private Button activeButton;
        public Sidebar()
        {
            InitializeComponent();
            ConfigureButtons();
            ActivateButton(btnDashboard);
        }
        private void ConfigureButtons()
        {
            // Button texts with icons
            btnDashboard.Text = " 📊 Dashboard";
            btnInventory.Text = " 📦 Inventory";
            btnProducts.Text = " 🏷️ Products";
            btnOrders.Text = " 📋 Orders";
            btnSales.Text = " 💎 Sales";
            btnStockAdjustments.Text = " 🔄 Stock Adjustments";
            btnCustomers.Text = " 👥 Customers";
            btnSuppliers.Text = " 🏭 Suppliers";
            btnDeliveries.Text = " 🚚 Deliveries";
            btnReturns.Text = " ↩️ Returns";
            btnRental.Text = " 📅 Rental";
            btnEmployees.Text = " 👤 Employees";
            btnUsers.Text = " 👨‍💼 Users";
            btnReports.Text = " 📊 Reports";
            btnHelp.Text = " ❓ Help";
            // Tags for routing
            btnDashboard.Tag = "Dashboard";
            btnInventory.Tag = "Inventory";
            btnProducts.Tag = "Products";
            btnOrders.Tag = "Orders";
            btnSales.Tag = "Sales";
            btnStockAdjustments.Tag = "Stock Adjustments";
            btnCustomers.Tag = "Customers";
            btnSuppliers.Tag = "Suppliers";
            btnDeliveries.Tag = "Deliveries";
            btnReturns.Tag = "Returns";
            btnRental.Tag = "Rental";
            btnEmployees.Tag = "Employees";
            btnUsers.Tag = "Users";
            btnReports.Tag = "Reports";
            btnHelp.Tag = "Help";
            // Common style
            Button[] buttons = {
                btnDashboard, btnInventory, btnProducts, btnOrders, btnSales,
                btnStockAdjustments, btnCustomers, btnSuppliers, btnDeliveries,
                btnReturns, btnRental, btnEmployees, btnUsers, btnReports, btnHelp
            };
            foreach (var btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(15, 0, 0, 0);
                btn.Font = new Font("Segoe UI", 9.5F);
                btn.ForeColor = Color.FromArgb(80, 80, 80);
                btn.BackColor = Color.White;
                btn.Cursor = Cursors.Hand;
                btn.Height = 45;
                // Hover effect
                btn.MouseEnter += (s, e) =>
                {
                    if (btn != activeButton)
                        btn.BackColor = Color.FromArgb(245, 247, 250);
                };
                btn.MouseLeave += (s, e) =>
                {
                    if (btn != activeButton)
                        btn.BackColor = Color.White;
                };
                btn.Click += SidebarButton_Click;
            }
        }
        private void SidebarButton_Click(object sender, EventArgs e)
        {
            ActivateButton((Button)sender);
            string section = ((Button)sender).Tag.ToString();
            SidebarItemClicked?.Invoke(this, section);
        }
        private void ActivateButton(Button btn)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.White;
                activeButton.ForeColor = Color.FromArgb(80, 80, 80);
                activeButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            }
            activeButton = btn;
            activeButton.BackColor = Color.FromArgb(230, 240, 255);
            activeButton.ForeColor = Color.FromArgb(0, 89, 179);
            activeButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            // Handle inventory click
        }
        public event EventHandler<string> SidebarItemClicked;
    }
}