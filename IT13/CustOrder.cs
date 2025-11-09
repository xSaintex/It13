using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class CustOrder : Form
    {
        public CustOrder()
        {
            InitializeComponent();
            // Apply rounded corners to controls
            ApplyRoundedCorners(txtboxsearch, 5);
            ApplyRoundedCorners(btnsearch, 5);
            ApplyRoundedCorners(ComBoxFilters, 5);
            ApplyRoundedCorners(btnaddorder, 5);
            ApplyRoundedCorners(ComBoxExporData, 5);
            // Add text to ComboBoxes
            InitializeComboBoxes();
        }

        // Method to initialize ComboBox items
        private void InitializeComboBoxes()
        {
            // Add items to ComBoxFilters
            ComBoxFilters.Items.Add("Filters");
            ComBoxFilters.SelectedIndex = 0; // Set "Filters" as default selected
            // Add items to ComBoxExporData
            ComBoxExporData.Items.Add("Export Data");
            ComBoxExporData.SelectedIndex = 0; // Set "Export Data" as default selected
        }

        // Method to apply rounded corners to any control
        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnsearchinv_Click(object sender, EventArgs e)
        {
        }

        private void ComBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnaddstock_Click(object sender, EventArgs e)
        {
            // Navigate to Add Customer Order form
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Add Customer Order";

                // Create AddCustOrder form
                AddCustOrder addCustOrderForm = new AddCustOrder();
                addCustOrderForm.TopLevel = false;
                addCustOrderForm.FormBorderStyle = FormBorderStyle.None;
                addCustOrderForm.Dock = DockStyle.Fill;

                // Clear the content panel and add AddCustOrder
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(addCustOrderForm);
                addCustOrderForm.Show();
            }
        }

        private void ComBoxExporData_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}