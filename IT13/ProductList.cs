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
    public partial class ProductList : Form
    {
        public ProductList()
        {
            InitializeComponent();
            ApplyRadiusToControls();
        }

        private void ApplyRadiusToControls()
        {
            // Apply radius 5 to txtboxsearch
            if (this.Controls.Find("txtboxsearch", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2TextBox txtboxsearch)
            {
                txtboxsearch.BorderRadius = 5;
            }

            // Apply radius 5 to btnsearch
            if (this.Controls.Find("btnsearch", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2Button btnsearch)
            {
                btnsearch.BorderRadius = 5;
            }

            // Apply radius 5 to ComBoxFilters and set text to "Filter" with black color
            if (this.Controls.Find("ComBoxFilters", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2ComboBox ComBoxFilters)
            {
                ComBoxFilters.BorderRadius = 5;
                ComBoxFilters.ForeColor = Color.Black;
                ComBoxFilters.Items.Clear();
                ComBoxFilters.Items.Add("Filter");
                ComBoxFilters.StartIndex = 0; // Select the first item
            }

            // Apply radius 5 to CombExport and set text to "Export Data" with black color
            if (this.Controls.Find("CombExport", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2ComboBox CombExport)
            {
                CombExport.BorderRadius = 5;
                CombExport.ForeColor = Color.Black;
                CombExport.Items.Clear();
                CombExport.Items.Add("Export Data");
                CombExport.StartIndex = 0; // Select the first item
            }

            // Apply radius 5 to btnaddstock and wire up click event
            if (this.Controls.Find("btnaddstock", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2Button btnaddstock)
            {
                btnaddstock.BorderRadius = 5;
                btnaddstock.Click += btnaddstock_Click;
            }
        }

        private void btnaddstock_Click(object sender, EventArgs e)
        {
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Add Product";

                // Create AddProd form
                AddProd addProdForm = new AddProd();
                addProdForm.TopLevel = false;
                addProdForm.FormBorderStyle = FormBorderStyle.None;
                addProdForm.Dock = DockStyle.Fill;

                // Clear the content panel and add AddProd
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(addProdForm);
                addProdForm.Show();
            }
        }

        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void datagridviewinventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ComBoxFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}