using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddProd : Form
    {
        public AddProd()
        {
            InitializeComponent();

            // Enable scrolling for the form
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 0);

            // Style breadcrumb buttons
            StyleBreadcrumbButtons();

            // Wire up click events
            btnhome.Click += btnhome_Click;
            btnproductlist.Click += btnproductlist_Click;
            btnadd.Click += btnadd_Click;
            btncancel.Click += btncancel_Click;
            btnaddprod.Click += btnaddprod_Click;

            // Apply border radius to action buttons
            ApplyButtonStyles();
        }

        private void ApplyButtonStyles()
        {
            // Set border radius to 5 for action buttons
            btncancel.BorderRadius = 5;
            btnaddprod.BorderRadius = 5;
        }

        private void StyleBreadcrumbButtons()
        {
            // Style the home button with icon
            StyleBreadcrumbButton(btnhome, "Home", true);

            // Style the product list button
            StyleBreadcrumbButton(btnproductlist, "Product List", false);

            // Style the add button (this would be the active/current page)
            StyleBreadcrumbButton(btnadd, "Add Product", false);
        }

        private void StyleBreadcrumbButton(Guna.UI2.WinForms.Guna2Button btn, string text, bool showHomeIcon = false)
        {
            btn.Text = showHomeIcon ? "" : text;
            btn.BorderRadius = 0;
            btn.BorderThickness = 0;
            btn.FillColor = Color.Transparent;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = HorizontalAlignment.Left;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            btn.DisabledState.BorderColor = Color.Transparent;
            btn.DisabledState.FillColor = Color.Transparent;

            // Set icon for home button
            if (showHomeIcon)
            {
                btn.Text = "🏠 Home";
                btn.ImageAlign = HorizontalAlignment.Left;
                btn.ImageSize = new Size(20, 20);
            }

            // Different color for the last breadcrumb (current page)
            if (btn == btnadd)
            {
                btn.ForeColor = Color.FromArgb(94, 148, 255); // Blue color for active
                btn.Checked = true;
            }
            else
            {
                btn.ForeColor = Color.FromArgb(125, 137, 149); // Gray for inactive
                btn.Checked = false;
            }

            // Hover state
            btn.HoverState.FillColor = Color.Transparent;
            btn.HoverState.ForeColor = Color.FromArgb(50, 50, 50);

            // Pressed state
            btn.PressedColor = Color.Transparent;
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            // Navigate to Home form
            // Example: 
            // Home homeForm = new Home();
            // homeForm.Show();
            // this.Hide();
        }

        private void btnproductlist_Click(object sender, EventArgs e)
        {
            // Navigate to ProductList form
            NavigateToProductList();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            // Already on Add Product page, so do nothing or refresh
            // This is the current active page
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            // Cancel button - navigate back to ProductList
            NavigateToProductList();
        }

        private void btnaddprod_Click(object sender, EventArgs e)
        {
            // Validate Product Name
            string productName = guna2TextBox1.Text.Trim();
            if (string.IsNullOrEmpty(productName) || productName == "Enter Product Name")
            {
                MessageBox.Show("Please enter a product name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Unit Cost
            string unitCostStr = guna2TextBox2.Text.Trim().Replace("₱", "").Replace(",", "").Trim();
            if (string.IsNullOrEmpty(unitCostStr) || unitCostStr == "0.00")
            {
                MessageBox.Show("Please enter a valid unit cost.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Selling Price
            string sellingPriceStr = guna2TextBox3.Text.Trim().Replace("₱", "").Replace(",", "").Trim();
            if (string.IsNullOrEmpty(sellingPriceStr) || sellingPriceStr == "0.00")
            {
                MessageBox.Show("Please enter a valid selling price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Category
            if (guna2ComboBox1.SelectedIndex < 0 || guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a product category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Supplier
            if (guna2ComboBox2.SelectedIndex < 0 || guna2ComboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a supplier company.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Status
            if (guna2ComboBox3.SelectedIndex < 0 || guna2ComboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a product status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get Description
            string description = guna2TextBox4.Text.Trim();
            if (string.IsNullOrEmpty(description) || description == "Enter product description....")
            {
                description = "";
            }

            // Confirm save
            DialogResult result = MessageBox.Show("Add this product to the product list?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            // Create product item
            var productItem = new ProductItem
            {
                ProductName = productName,
                Category = guna2ComboBox1.SelectedItem.ToString(),
                UnitCost = "₱" + unitCostStr,
                SellingPrice = "₱" + sellingPriceStr,
                PrimarySupplier = guna2ComboBox2.SelectedItem.ToString(),
                Status = guna2ComboBox3.SelectedItem.ToString(),
                Description = description
            };

            // Navigate to ProductList and add the product
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Product List";

                ProductList productListForm = new ProductList();

                // Add the product to the list
                productListForm.AddProduct(productItem);

                productListForm.TopLevel = false;
                productListForm.FormBorderStyle = FormBorderStyle.None;
                productListForm.Dock = DockStyle.Fill;

                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(productListForm);
                productListForm.Show();

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Helper class to hold product item data
        public class ProductItem
        {
            public string ProductName { get; set; }
            public string Category { get; set; }
            public string UnitCost { get; set; }
            public string SellingPrice { get; set; }
            public string PrimarySupplier { get; set; }
            public string Status { get; set; }
            public string Description { get; set; }
        }

        private void NavigateToProductList()
        {
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Product List";

                // Create ProductList form
                ProductList productListForm = new ProductList();
                productListForm.TopLevel = false;
                productListForm.FormBorderStyle = FormBorderStyle.None;
                productListForm.Dock = DockStyle.Fill;

                // Clear the content panel and add ProductList
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(productListForm);
                productListForm.Show();
            }
            else
            {
                // Fallback if parent form is not found
                this.Close();
            }
        }
    }
}