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
    public partial class AddStock : Form
    {
        public AddStock()
        {
            InitializeComponent();
            // Enable scrolling for the form
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 0);

            // Style the breadcrumb buttons
            StyleBreadcrumbButton(btnhome, "Home", true); // true = show icon
            StyleBreadcrumbButton(btninventory, "Inventory", false);
            StyleBreadcrumbButton(btnadd, "Add stock", false);
        }

        private void StyleBreadcrumbButton(Guna.UI2.WinForms.Guna2Button btn, string text, bool showHomeIcon = false)
        {
            btn.Text = showHomeIcon ? "" : text; // Empty text if showing icon only
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
                // You can use Guna2 built-in image or set a custom image
                // Option 1: Use a home icon from resources or file
                // btn.Image = Properties.Resources.home_icon; // If you have it in resources

                // Option 2: Set image from file
                // btn.Image = Image.FromFile("path_to_home_icon.png");

                // For now, we'll use text as fallback
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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            // Search functionality
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Label click handler
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Label click handler
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Button click handler
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            // Navigate to Home form
            // Example: 
            // Home homeForm = new Home();
            // homeForm.Show();
            // this.Hide();
        }

        private void btninventory_Click(object sender, EventArgs e)
        {
            // Navigate to Inventory form
            // Example:
            // Inventory inventoryForm = new Inventory();
            // inventoryForm.Show();
            // this.Hide();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            // Already on Add Stock page, so do nothing or refresh
            // This is the current active page
        }
    }
}