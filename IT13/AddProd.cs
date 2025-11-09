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
            StyleBreadcrumbButton(btnadd, "Add stock", false);
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
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            // Already on Add Product page, so do nothing or refresh
            // This is the current active page
        }
    }
}