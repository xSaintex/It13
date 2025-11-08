using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class Form1 : Form
    {
        private inven inventoryForm;
        private ProductList productListForm;

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
            navBar1.PageTitle = "Dashboard";
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

            // Sidebar Item Click handler
            sidebar1.SidebarItemClicked += (s, ev) =>
            {
                if (ev.Section == "Products")
                {
                    // Just toggle dropdown, don't change content
                    // Dropdown is handled internally by sidebar
                }
                else if (ev.Section == "Product List") // This is the dropdown item under Products
                {
                    navBar1.PageTitle = "Product List";
                    LoadProductListForm();
                }
                else if (ev.Section == "Inventory")
                {
                    navBar1.PageTitle = "Inventory";
                    LoadInventoryForm();
                }
                else
                {
                    navBar1.PageTitle = ev.Section;
                    pnlContent.Controls.Clear();
                }
            };
        }

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
    }
}