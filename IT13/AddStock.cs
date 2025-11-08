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
        private bool isHeaderCheckBoxChecked = false;

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

            // Setup DataGridView with checkbox column
            SetupDataGridView();

            // Add rows to DataGridView
            AddDataGridRows();
        }

        private void SetupDataGridView()
        {
            // Check if checkbox column already exists
            bool hasCheckboxColumn = false;
            foreach (DataGridViewColumn col in datagridtableaddstock.Columns)
            {
                if (col is DataGridViewCheckBoxColumn && col.Name == "SelectColumn")
                {
                    hasCheckboxColumn = true;
                    break;
                }
            }

            // Only add checkbox column if it doesn't exist
            if (!hasCheckboxColumn)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "";
                checkBoxColumn.Name = "SelectColumn";
                checkBoxColumn.Width = 50;
                checkBoxColumn.ReadOnly = false;
                checkBoxColumn.FalseValue = false;
                checkBoxColumn.TrueValue = true;
                datagridtableaddstock.Columns.Insert(0, checkBoxColumn);
            }

            // Disable adding new rows automatically
            datagridtableaddstock.AllowUserToAddRows = false;

            // Subscribe to events for header checkbox
            datagridtableaddstock.CellPainting += DataGridView_CellPainting;
            datagridtableaddstock.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
            datagridtableaddstock.CellValueChanged += DataGridView_CellValueChanged;
            datagridtableaddstock.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (datagridtableaddstock.IsCurrentCellDirty)
            {
                datagridtableaddstock.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Update header checkbox state when individual checkboxes change
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                UpdateHeaderCheckBoxState();
            }
        }

        private void UpdateHeaderCheckBoxState()
        {
            if (datagridtableaddstock.Rows.Count == 0)
            {
                isHeaderCheckBoxChecked = false;
                datagridtableaddstock.InvalidateCell(0, -1);
                return;
            }

            bool allChecked = true;
            int validRowCount = 0;

            foreach (DataGridViewRow row in datagridtableaddstock.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Check if row has data
                    bool hasData = row.Cells.Count > 1 && row.Cells[1].Value != null &&
                                   !string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString());

                    if (hasData)
                    {
                        validRowCount++;
                        bool isChecked = row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value);
                        if (!isChecked)
                        {
                            allChecked = false;
                            break;
                        }
                    }
                }
            }

            isHeaderCheckBoxChecked = (validRowCount > 0 && allChecked);
            datagridtableaddstock.InvalidateCell(0, -1);
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Paint checkbox in the header of the first column
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.ClipBounds, true);

                // Calculate checkbox position in header
                Point checkBoxLocation = new Point(
                    e.CellBounds.Location.X + (e.CellBounds.Width / 2) - 7,
                    e.CellBounds.Location.Y + (e.CellBounds.Height / 2) - 7
                );

                Size checkBoxSize = new Size(14, 14);

                // Draw the checkbox based on current state
                CheckBoxState state = isHeaderCheckBoxChecked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
                CheckBoxRenderer.DrawCheckBox(e.Graphics, checkBoxLocation, state);

                e.Handled = true;
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Handle checkbox click in header - Select All / Deselect All
            if (e.ColumnIndex == 0)
            {
                // Toggle the header checkbox state
                isHeaderCheckBoxChecked = !isHeaderCheckBoxChecked;

                // Temporarily unsubscribe from CellValueChanged to prevent multiple updates
                datagridtableaddstock.CellValueChanged -= DataGridView_CellValueChanged;

                // Apply the new state to all rows with data
                foreach (DataGridViewRow row in datagridtableaddstock.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // Check if row has data (at least product name is not empty)
                        bool hasData = row.Cells.Count > 1 && row.Cells[1].Value != null &&
                                       !string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString());

                        if (hasData)
                        {
                            row.Cells[0].Value = isHeaderCheckBoxChecked;
                        }
                    }
                }

                // Re-subscribe to CellValueChanged
                datagridtableaddstock.CellValueChanged += DataGridView_CellValueChanged;

                // Refresh the entire DataGridView
                datagridtableaddstock.Invalidate();
                datagridtableaddstock.Refresh();
            }
        }

        private void AddDataGridRows()
        {
            // Clear existing rows if any
            datagridtableaddstock.Rows.Clear();

            // Add HiKvision row - checkbox value first
            datagridtableaddstock.Rows.Add(false, "HiKvision", "CCTV", "XXXX-XXXX-XXXX", "1", "₱1500.00", "₱4500.00");

            // Add Dahua row - checkbox value first
            datagridtableaddstock.Rows.Add(false, "Dahua", "CCTV", "XXXX-XXXX-XXXX", "1", "₱400.00", "₱1200.00");

            // Initialize the header checkbox state
            UpdateHeaderCheckBoxState();
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
            // Get the parent form (Form1)
            Form1 parentForm = this.ParentForm as Form1;
            if (parentForm != null)
            {
                // Update the navbar title
                parentForm.navBar1.PageTitle = "Inventory";

                // Create inven form
                inven inventoryForm = new inven();
                inventoryForm.TopLevel = false;
                inventoryForm.FormBorderStyle = FormBorderStyle.None;
                inventoryForm.Dock = DockStyle.Fill;

                // Clear the content panel and add inven
                parentForm.pnlContent.Controls.Clear();
                parentForm.pnlContent.Controls.Add(inventoryForm);
                inventoryForm.Show();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            // Already on Add Stock page, so do nothing or refresh
            // This is the current active page
        }
    }
}