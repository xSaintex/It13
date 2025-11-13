// ---------------------------------------------------------------------
// Form1.cs - FULL & FINAL
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
namespace IT13
{
    public partial class Form1 : Form
    {
        private inven inventoryForm;
        private ProductList productListForm;
        private ProductCategory productCategoryForm;
        private CustOrder custOrderForm;
        private StockAdjustment stockAdjustmentForm;
        private OrderList orderListForm;
        private SupplierOrderList supplierOrderListForm;
        private CustomerList customerListForm; // ← Field added
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // NAVBAR setup
            navBar1.Dock = DockStyle.None;
            navBar1.Width = 1190;
            navBar1.Height = 70;
            navBar1.Left = 260;
            navBar1.Top = 0;
            navBar1.Padding = new Padding(20, 0, 30, 0);
            navBar1.PageTitle = "Dashboard"; // ← Default
            navBar1.UserName = "John Doe";
            // SIDEBAR setup
            sidebar1.Dock = DockStyle.Left;
            sidebar1.Width = 260;
            sidebar1.Height = this.ClientSize.Height;
            sidebar1.BringToFront();
            // CONTENT PANEL setup
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Left = 260;
            pnlContent.Top = 70;
            pnlContent.Width = this.ClientSize.Width - 260;
            pnlContent.Height = this.ClientSize.Height - 70;
            // AUTO RESIZE handler
            this.Resize += (s, ev) =>
            {
                sidebar1.Height = this.ClientSize.Height;
                navBar1.Width = this.ClientSize.Width - 260;
                navBar1.Left = 260;
                pnlContent.Width = this.ClientSize.Width - 260;
                pnlContent.Height = this.ClientSize.Height - navBar1.Height;
            };
            // SIDEBAR ITEM CLICK HANDLER
            sidebar1.SidebarItemClicked += (s, ev) =>
            {
                if (ev.Section == "Products")
                {
                    navBar1.PageTitle = "Products";
                }
                else if (ev.Section == "Product List")
                {
                    navBar1.PageTitle = "Product List";
                    LoadProductListForm();
                }
                else if (ev.Section == "Product Categories")
                {
                    navBar1.PageTitle = "Product Categories";
                    LoadProductCategoryForm();
                }
                else if (ev.Section == "Inventory")
                {
                    navBar1.PageTitle = "Inventory";
                    LoadInventoryForm();
                }
                else if (ev.Section == "Stock Adjustments")
                {
                    navBar1.PageTitle = "Stock Adjustments";
                    LoadStockAdjustmentForm();
                }
                else if (ev.Section == "Order List")
                {
                    navBar1.PageTitle = "Order List";
                    LoadOrderListForm();
                }
                else if (ev.Section == "Supplier Order")
                {
                    navBar1.PageTitle = "Supplier Order";
                    LoadSupplierOrderListForm();
                }
                else if (ev.Section == "Customer Order")
                {
                    navBar1.PageTitle = "Customer Order";
                    LoadCustomerOrderForm();
                }
                else if (ev.Section == "Customer List") // ← ONLY THIS OPENS CUSTOMER LIST
                {
                    navBar1.PageTitle = "Customer List";
                    LoadCustomerListForm();
                }
                else if (ev.Section == "Orders")
                {
                    // Just toggle dropdown
                }
                else
                {
                    navBar1.PageTitle = ev.Section;
                    pnlContent.Controls.Clear();
                }
            };
            // NO AUTO-LOAD ON F5 → Dashboard or blank
        }
        #region Load Form Methods
        private void LoadInventoryForm()
        {
            pnlContent.Controls.Clear();
            inventoryForm = new inven
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(inventoryForm);
            inventoryForm.Show();
        }
        private void LoadProductListForm()
        {
            pnlContent.Controls.Clear();
            productListForm = new ProductList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(productListForm);
            productListForm.Show();
        }
        private void LoadProductCategoryForm()
        {
            pnlContent.Controls.Clear();
            productCategoryForm = new ProductCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(productCategoryForm);
            productCategoryForm.Show();
        }
        private void LoadCustomerOrderForm()
        {
            pnlContent.Controls.Clear();
            custOrderForm = new CustOrder
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(custOrderForm);
            custOrderForm.Show();
        }
        private void LoadStockAdjustmentForm()
        {
            pnlContent.Controls.Clear();
            stockAdjustmentForm = new StockAdjustment
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(stockAdjustmentForm);
            stockAdjustmentForm.Show();
        }
        private void LoadOrderListForm()
        {
            pnlContent.Controls.Clear();
            orderListForm = new OrderList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(orderListForm);
            orderListForm.Show();
        }
        private void LoadSupplierOrderListForm()
        {
            pnlContent.Controls.Clear();
            supplierOrderListForm = new SupplierOrderList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(supplierOrderListForm);
            supplierOrderListForm.Show();
        }
        // NEW: Load Customer List
        public void LoadCustomerListForm()
        {
            pnlContent.Controls.Clear();
            customerListForm = new CustomerList
            {
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(customerListForm);
            customerListForm.Show();
        }
        #endregion
    }
}