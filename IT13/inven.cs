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
        private CheckBox headerCheckBox;

        public inven()
        {
            InitializeComponent();

            // Set BorderRadius to 5 for all specified controls
            txtboxsearch.BorderRadius = 5;
            btnsearchinv.BorderRadius = 5;
            ComBoxFilters.BorderRadius = 5;
            btnaddstock.BorderRadius = 5;
            ComBoxExporData.BorderRadius = 5;

            // Set ComBoxFilters text and color
            ComBoxFilters.Items.Clear();
            ComBoxFilters.Items.Add("Filter");
            ComBoxFilters.StartIndex = 0;
            ComBoxFilters.ForeColor = Color.Black;

            // Set ComBoxExporData text and color
            ComBoxExporData.Items.Clear();
            ComBoxExporData.Items.Add("Export Data");
            ComBoxExporData.StartIndex = 0;
            ComBoxExporData.ForeColor = Color.Black;

            // Add checkbox column if it doesn't exist
            if (datagridviewinventory.Columns.Count == 0 || datagridviewinventory.Columns[0].GetType() != typeof(DataGridViewCheckBoxColumn))
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Name = "checkboxColumn";
                checkBoxColumn.Width = 50;
                datagridviewinventory.Columns.Insert(0, checkBoxColumn);
            }

            // Add sample data to datagridviewinventory
            datagridviewinventory.Rows.Add(false, "1", "cctv", "Dome Cameras", "43 pcs.", "₱200.00", "₱8,600.00", "Verizon Communications Inc.", "Active");
            datagridviewinventory.Rows.Add(false, "2", "cctv", "Bullet Cameras", "18 pcs.", "₱300.00", "₱5,400.00", "AT&T Inc.", "Active");
            datagridviewinventory.Rows.Add(false, "3", "siriejir", "kisweksiwe", "18 pcs.", "₱400.00", "₱7,200.00", "AT&T Inc.", "Active");
            datagridviewinventory.Rows.Add(false, "4", "cdsr", "krokro", "10 pcs.", "₱400.00", "₱4,000.00", "AT&T Inc.", "Active");

            // Add header checkbox for Select All
            datagridviewinventory.CellPainting += DataGridView_CellPainting;
            datagridviewinventory.CellClick += DataGridView_CellClick;
            datagridviewinventory.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;

            // Update labelshow with row count
            UpdateRowCount();
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);

                if (headerCheckBox == null)
                {
                    headerCheckBox = new CheckBox();
                    headerCheckBox.Size = new Size(18, 18);
                    headerCheckBox.BackColor = Color.Transparent;
                    headerCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;
                    datagridviewinventory.Controls.Add(headerCheckBox);
                }

                Point checkBoxLocation = new Point(
                    e.CellBounds.Left + (e.CellBounds.Width - headerCheckBox.Width) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - headerCheckBox.Height) / 2
                );
                headerCheckBox.Location = checkBoxLocation;

                e.Handled = true;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                headerCheckBox.Checked = !headerCheckBox.Checked;
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (datagridviewinventory.IsCurrentCellDirty)
            {
                datagridviewinventory.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            datagridviewinventory.EndEdit();

            for (int i = 0; i < datagridviewinventory.Rows.Count; i++)
            {
                if (!datagridviewinventory.Rows[i].IsNewRow)
                {
                    datagridviewinventory.Rows[i].Cells[0].Value = headerCheckBox.Checked;
                }
            }

            datagridviewinventory.RefreshEdit();
        }

        private void UpdateRowCount()
        {
            labelshow.Text = $"Showing {datagridviewinventory.Rows.Count} items";
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