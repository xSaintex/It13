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
    public partial class inven : Form
    {
        public inven()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void datagridviewinventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnaddstock_Click(object sender, EventArgs e)
        {
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;

            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Add Stock";

                // Create AddStock form
                AddStock addStockForm = new AddStock();
                addStockForm.TopLevel = false;
                addStockForm.FormBorderStyle = FormBorderStyle.None;
                addStockForm.Dock = DockStyle.Fill;

                // Clear the content panel and add AddStock
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(addStockForm);
                addStockForm.Show();
            }
        }
    }
}