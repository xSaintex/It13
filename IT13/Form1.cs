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
        private CustomerList customerListForm;
        private SupplierList supplierListForm;
        private DeliveryVehicleList deliveryVehicleListForm;
        private DeliveryList deliveryListForm;
        private CustomerReturns customerReturnsForm;
        private SupplierReturns supplierReturnsForm;
        private ReturnList returnListForm;       
        private RentalList rentalListForm;       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // === LAYOUT SETUP ===
            navBar1.Dock = DockStyle.None;
            navBar1.Left = 260;
            navBar1.Top = 0;
            navBar1.NavHeight = 80;
            navBar1.PageTitle = "Dashboard";
            navBar1.UserName = "Admin";
            sidebar1.Dock = DockStyle.Left;
            sidebar1.Width = 260;
            sidebar1.Height = this.ClientSize.Height;
            sidebar1.BringToFront();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Left = 260;
            pnlContent.Top = 70;
            pnlContent.Width = this.ClientSize.Width - 260;
            pnlContent.Height = this.ClientSize.Height - 70;

            // === RESIZE HANDLERS ===
            this.Resize += (s, ev) =>
            {
                navBar1.ApplySize();
                pnlContent.Left = 260;
                pnlContent.Top = navBar1.Height;
                pnlContent.Width = this.ClientSize.Width - 260;
                pnlContent.Height = this.ClientSize.Height - navBar1.Height;
            };
            this.Resize += (s, ev) =>
            {
                sidebar1.Height = this.ClientSize.Height;
                navBar1.Width = this.ClientSize.Width - 260;
                navBar1.Left = 260;
                pnlContent.Width = this.ClientSize.Width - 260;
                pnlContent.Height = this.ClientSize.Height - navBar1.Height;
            };

            // === SIDEBAR CLICK HANDLER ===
            sidebar1.SidebarItemClicked += (s, ev) =>
            {
                string section = ev.Section?.Trim() ?? "";
                switch (section)
                {
                    case "Dashboard":
                        navBar1.PageTitle = "Dashboard";
                        pnlContent.Controls.Clear();
                        break;
                    case "Product List":
                        navBar1.PageTitle = "Product List";
                        LoadProductListForm();
                        break;
                    case "Categories":
                        navBar1.PageTitle = "Categories";
                        LoadProductCategoryForm();
                        break;
                    case "Inventory":
                        navBar1.PageTitle = "Inventory";
                        LoadInventoryForm();
                        break;
                    case "Stock Adjustments":
                        navBar1.PageTitle = "Stock Adjustments";
                        LoadStockAdjustmentForm();
                        break;
                    case "Order List":
                        navBar1.PageTitle = "Order List";
                        LoadOrderListForm();
                        break;
                    case "Supplier Order":
                        navBar1.PageTitle = "Supplier Order";
                        LoadSupplierOrderListForm();
                        break;
                    case "Customer Order":
                        navBar1.PageTitle = "Customer Order";
                        LoadCustomerOrderForm();
                        break;
                    case "Customer List":
                        navBar1.PageTitle = "Customer List";
                        LoadCustomerListForm();
                        break;
                    case "Supplier List":
                        navBar1.PageTitle = "Supplier List";
                        LoadSupplierListForm();
                        break;
                    case "Delivery Vehicles":
                        navBar1.PageTitle = "Delivery Vehicles";
                        LoadDeliveryVehicleListForm();
                        break;
                    case "Delivery List":
                        navBar1.PageTitle = "Delivery List";
                        LoadDeliveryListForm();
                        break;
                    case "Customer Returns":
                        navBar1.PageTitle = "Customer Returns";
                        LoadCustomerReturnsForm();
                        break;
                    case "Supplier Returns":
                        navBar1.PageTitle = "Supplier Returns";
                        LoadSupplierReturnsForm();
                        break;
                    case "Returns List":
                        navBar1.PageTitle = "Returns List";
                        LoadReturnListForm();
                        break;
                    case "Rental List": 
                        navBar1.PageTitle = "Rental List";
                        LoadRentalListForm();
                        break;
                    default:
                        navBar1.PageTitle = section;
                        break;
                }
            };
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

        private void LoadCustomerListForm()
        {
            pnlContent.Controls.Clear();
            customerListForm = new CustomerList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(customerListForm);
            customerListForm.Show();
        }

        private void LoadSupplierListForm()
        {
            pnlContent.Controls.Clear();
            supplierListForm = new SupplierList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(supplierListForm);
            supplierListForm.Show();
        }

        private void LoadDeliveryVehicleListForm()
        {
            pnlContent.Controls.Clear();
            deliveryVehicleListForm = new DeliveryVehicleList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(deliveryVehicleListForm);
            deliveryVehicleListForm.Show();
        }

        private void LoadDeliveryListForm()
        {
            pnlContent.Controls.Clear();
            deliveryListForm = new DeliveryList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(deliveryListForm);
            deliveryListForm.Show();
        }

        private void LoadCustomerReturnsForm()
        {
            pnlContent.Controls.Clear();
            customerReturnsForm = new CustomerReturns
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(customerReturnsForm);
            customerReturnsForm.Show();
        }

        private void LoadSupplierReturnsForm()
        {
            pnlContent.Controls.Clear();
            supplierReturnsForm = new SupplierReturns
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(supplierReturnsForm);
            supplierReturnsForm.Show();
        }

        private void LoadReturnListForm()
        {
            pnlContent.Controls.Clear();
            returnListForm = new ReturnList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(returnListForm);
            returnListForm.Show();
        }

        private void LoadRentalListForm() 
        {
            pnlContent.Controls.Clear();
            rentalListForm = new RentalList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            pnlContent.Controls.Add(rentalListForm);
            rentalListForm.Show();
        }

        #endregion

        // === NAVIGATION HELPERS ===
        public void NavigateToCustomerList()
        {
            navBar1.PageTitle = "Customer List";
            LoadCustomerListForm();
        }

        public void NavigateToSupplierList()
        {
            navBar1.PageTitle = "Supplier List";
            LoadSupplierListForm();
        }

        public void NavigateToDeliveryVehicles()
        {
            navBar1.PageTitle = "Delivery Vehicles";
            LoadDeliveryVehicleListForm();
        }

        public void NavigateToDeliveryList()
        {
            navBar1.PageTitle = "Delivery List";
            LoadDeliveryListForm();
        }

        public void NavigateToCustomerReturns()
        {
            navBar1.PageTitle = "Customer Returns";
            LoadCustomerReturnsForm();
        }

        public void NavigateToSupplierReturns()
        {
            navBar1.PageTitle = "Supplier Returns";
            LoadSupplierReturnsForm();
        }

        public void NavigateToReturnList()
        {
            navBar1.PageTitle = "Returns List";
            LoadReturnListForm();
        }

        public void NavigateToRentalList() 
        {
            navBar1.PageTitle = "Rental List";
            LoadRentalListForm();
        }
    }
}