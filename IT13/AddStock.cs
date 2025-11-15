using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
            // Set border radius for search controls and action buttons
            StyleSearchControls();
            // Setup placeholder behavior
            SetupPlaceholders();
            // Setup DataGridView with checkbox column
            SetupDataGridView();
            // Add rows to DataGridView
            AddDataGridRows();
            UpdateRowCount();
            txtboxsearchproductname.TextChanged += txtboxsearchproductname_TextChanged;

            // Add missing event handlers
            btnsearchcompany.Click += btnsearchcompany_Click;
            btnsearchprod.Click += btnsearchprod_Click; // Renamed from guna2Button1_Click for clarity
            btnaddincstock.Click += btnaddincstock_Click;
            btnsavestcok.Click += btnsavestcok_Click;
            guna2Button3.Click += guna2Button3_Click;
            datagridtableaddstock.CellValueChanged += datagridtableaddstock_CellValueChanged;
            labelfeildsrequired.Click += label2_Click; // If needed, but empty in original
        }

        private void SetupDataGridView()
        {
            datagridtableaddstock.AllowUserToAddRows = true;
            datagridtableaddstock.ReadOnly = false;
            datagridtableaddstock.AllowUserToDeleteRows = true;
            datagridtableaddstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridtableaddstock.MultiSelect = false;
            foreach (DataGridViewColumn col in datagridtableaddstock.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            datagridtableaddstock.EnableHeadersVisualStyles = false;
            datagridtableaddstock.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridtableaddstock.ColumnHeadersDefaultCellStyle.BackColor;
            datagridtableaddstock.DefaultCellStyle.SelectionBackColor =
                datagridtableaddstock.DefaultCellStyle.BackColor;
            datagridtableaddstock.DefaultCellStyle.SelectionForeColor =
                datagridtableaddstock.DefaultCellStyle.ForeColor;
            datagridtableaddstock.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridtableaddstock.RowTemplate.Height = 45;
            datagridtableaddstock.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            // === COLUMN LAYOUT ===
            datagridtableaddstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridtableaddstock.Columns[0].FillWeight = 30; // Product Name
            datagridtableaddstock.Columns[1].FillWeight = 20; // Category
            datagridtableaddstock.Columns[2].FillWeight = 15; // Quantity
            datagridtableaddstock.Columns[3].FillWeight = 15; // Unit Cost
            datagridtableaddstock.Columns[4].FillWeight = 20; // Subtotal
            datagridtableaddstock.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void AddDataGridRows()
        {
            // Clear existing rows if any
            datagridtableaddstock.Rows.Clear();
            // Add HiKvision row
            datagridtableaddstock.Rows.Add("HiKvision", "CCTV", "1", "₱1500.00", "₱1500.00");
            // Add Dahua row
            datagridtableaddstock.Rows.Add("Dahua", "CCTV", "1", "₱400.00", "₱400.00");
        }

        private void UpdateRowCount()
        {
            int visibleCount = 0;
            foreach (DataGridViewRow row in datagridtableaddstock.Rows)
            {
                if (row.Visible && !row.IsNewRow) visibleCount++;
            }
            labelshow.Text = $"Showing {visibleCount} items";
        }

        private void txtboxsearchproductname_TextChanged(object sender, EventArgs e)
        {
            // Skip filtering if it's just the placeholder text
            if (txtboxsearchproductname.Text == "Search by product name" || txtboxsearchproductname.ForeColor == Color.Gray)
            {
                return;
            }

            string filter = txtboxsearchproductname.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridtableaddstock.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells[0].Value?.ToString().ToLower() ?? "";
                string category = row.Cells[1].Value?.ToString().ToLower() ?? "";
                bool match = string.IsNullOrEmpty(filter) ||
                             name.Contains(filter) ||
                             category.Contains(filter);
                row.Visible = match;
            }
            UpdateRowCount();
        }

        private void StyleSearchControls()
        {
            // Set border radius to 5 for textboxes
            txtboxsearchcompany.BorderRadius = 5;
            txtboxsearchproductname.BorderRadius = 5;
            // Set border radius to 5 for search buttons
            btnsearchcompany.BorderRadius = 5;
            btnsearchprod.BorderRadius = 5;
            // Set border radius to 5 for action buttons
            btnaddincstock.BorderRadius = 5;
            btnsavestcok.BorderRadius = 5;
            guna2Button3.BorderRadius = 5;
        }

        private void SetupPlaceholders()
        {
            // Set placeholder behavior for search textboxes
            SetupPlaceholderBehavior(txtboxsearchproductname, "Search by product name");
            SetupPlaceholderBehavior(txtboxsearchcompany, "Select Company");
        }

        private void SetupPlaceholderBehavior(Guna.UI2.WinForms.Guna2TextBox textBox, string placeholder)
        {
            // Set initial placeholder text and color
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;

            // When textbox gets focus, clear placeholder
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            // When textbox loses focus, restore placeholder if empty
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void StyleBreadcrumbButton(Guna.UI2.WinForms.Guna2Button btn, string text, bool showHomeIcon = false)
        {
            btn.Text = showHomeIcon ? "🏠 Home" : text;
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

        private void label2_Click(object sender, EventArgs e)
        {
            // Label click handler, empty as per original
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigating to Home");
            // Uncomment if Home form exists
            // Home homeForm = new Home();
            // homeForm.Show();
            // this.Hide();
        }

        private void btninventory_Click(object sender, EventArgs e)
        {
            // Navigate back to Inventory
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Inventory";
                inven inventoryForm = new inven();
                inventoryForm.TopLevel = false;
                inventoryForm.FormBorderStyle = FormBorderStyle.None;
                inventoryForm.Dock = DockStyle.Fill;
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(inventoryForm);
                inventoryForm.Show();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Already on Add Stock page");
        }

        private void btnsearchcompany_Click(object sender, EventArgs e)
        {
            string company = txtboxsearchcompany.Text.Trim();
            // Skip if it's the placeholder text
            if (string.IsNullOrEmpty(company) || company == "Select Company" || txtboxsearchcompany.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please enter a company name to search.");
            }
            else
            {
                MessageBox.Show($"Searching for company: {company}");
                // TODO: Implement actual search logic, e.g., filter grid or load data
                UpdateRowCount();
            }
        }

        private void btnsearchprod_Click(object sender, EventArgs e)
        {
            string product = txtboxsearchproductname.Text.Trim();
            // Skip if it's the placeholder text
            if (string.IsNullOrEmpty(product) || product == "Search by product name" || txtboxsearchproductname.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please enter a product name to search.");
            }
            else
            {
                MessageBox.Show($"Searching for product: {product}");
                // TODO: Implement actual search logic
            }
        }

        private void btnaddincstock_Click(object sender, EventArgs e)
        {
            datagridtableaddstock.Rows.Add("", "", "1", "₱0.00", "₱0.00");
            MessageBox.Show("New inventory stock item added.");
            UpdateRowCount();
        }

        private void btnsavestcok_Click(object sender, EventArgs e)
        {
            // Cancel button - navigate back to inventory
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Inventory";
                inven inventoryForm = new inven();
                inventoryForm.TopLevel = false;
                inventoryForm.FormBorderStyle = FormBorderStyle.None;
                inventoryForm.Dock = DockStyle.Fill;
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(inventoryForm);
                inventoryForm.Show();
            }
            else
            {
                // Fallback if parent form is not found
                this.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Validate company name (check for placeholder)
            string companyName = txtboxsearchcompany.Text.Trim();
            if (string.IsNullOrEmpty(companyName) || companyName == "Select Company" || txtboxsearchcompany.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please enter a company name before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that there are items to save
            int itemCount = 0;
            foreach (DataGridViewRow row in datagridtableaddstock.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    itemCount++;
                }
            }

            if (itemCount == 0)
            {
                MessageBox.Show("Please add at least one item before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm save
            DialogResult result = MessageBox.Show($"Save {itemCount} item(s) to inventory?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            // Save Stock - collect all data to pass to inventory
            List<StockItem> stockItems = new List<StockItem>();
            foreach (DataGridViewRow row in datagridtableaddstock.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    StockItem item = new StockItem
                    {
                        ProductName = row.Cells[0].Value?.ToString() ?? "",
                        Category = row.Cells[1].Value?.ToString() ?? "",
                        Quantity = row.Cells[2].Value?.ToString() ?? "0",
                        UnitCost = row.Cells[3].Value?.ToString() ?? "₱0.00",
                        TotalCost = row.Cells[4].Value?.ToString() ?? "₱0.00",
                        Supplier = companyName,
                        Status = "Active"
                    };
                    stockItems.Add(item);
                }
            }

            // Navigate back to inventory and pass the data
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                parentForm.navBar1.PageTitle = "Inventory";
                inven inventoryForm = new inven();

                // Add the stock items to inventory
                inventoryForm.AddStockItems(stockItems);

                inventoryForm.TopLevel = false;
                inventoryForm.FormBorderStyle = FormBorderStyle.None;
                inventoryForm.Dock = DockStyle.Fill;
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(inventoryForm);
                inventoryForm.Show();

                MessageBox.Show($"Successfully saved {itemCount} item(s) to inventory!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Helper class to hold stock item data
        public class StockItem
        {
            public string ProductName { get; set; }
            public string Category { get; set; }
            public string Quantity { get; set; }
            public string UnitCost { get; set; }
            public string TotalCost { get; set; }
            public string Supplier { get; set; }
            public string Status { get; set; }
        }

        private void datagridtableaddstock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || datagridtableaddstock.Rows[e.RowIndex].IsNewRow) return;

            if (e.ColumnIndex == 2 || e.ColumnIndex == 3) // Quantity or Unit Cost
            {
                DataGridViewRow row = datagridtableaddstock.Rows[e.RowIndex];
                string qtyStr = row.Cells[2].Value?.ToString() ?? "0";
                string costStr = row.Cells[3].Value?.ToString() ?? "₱0.00";

                double qty = double.TryParse(qtyStr, out double q) ? q : 0;

                costStr = costStr.Replace("₱", "").Replace(",", "").Trim();
                double cost = double.TryParse(costStr, out double c) ? c : 0;

                double subtotal = qty * cost;
                row.Cells[4].Value = "₱" + subtotal.ToString("N2");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // If this was in designer, redirect to btnsearchprod_Click
            btnsearchprod_Click(sender, e);
        }
    }
}