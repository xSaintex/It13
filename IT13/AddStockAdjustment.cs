using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddStockAdjustment : Form
    {
        public AddStockAdjustment()
        {
            InitializeComponent();
            SetupCombos();               // fill all combos
            datePicker.Value = DateTime.Today;   // default today
        }

        #region ComboBox Setup
        private void SetupCombos()
        {
            // ----- Inventory Item -----
            comboItem.Items.Clear();
            comboItem.Items.Add("Select an inventory item");
            comboItem.Items.Add("CCTV Camera Pro");
            comboItem.Items.Add("Wireless Router");
            comboItem.SelectedIndex = 0;

            // ----- Requested By -----
            comboRequestedBy.Items.Clear();
            comboRequestedBy.Items.Add("Select an employee");
            comboRequestedBy.Items.Add("John Doe");
            comboRequestedBy.Items.Add("Jane Smith");
            comboRequestedBy.SelectedIndex = 0;

            // ----- Reviewed By (NEW) -----
            comboReviewedBy.Items.Clear();
            comboReviewedBy.Items.Add("Select an employee");
            comboReviewedBy.Items.Add("John Doe");
            comboReviewedBy.Items.Add("Jane Smith");
            comboReviewedBy.SelectedIndex = 0;

            // ----- Adjustment Type -----
            comboAdjType.Items.Clear();
            comboAdjType.Items.AddRange(new[] { "Increase", "Decrease" });
            comboAdjType.SelectedIndex = 0;

            // ----- Status -----
            comboStatus.Items.Clear();
            comboStatus.Items.AddRange(new[] { "Pending", "Approved", "Rejected" });
            comboStatus.SelectedIndex = 0;
        }
        #endregion

        #region Button Clicks
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            MessageBox.Show(
                "Stock adjustment added successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ReturnToList();
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToList();
        #endregion

        #region Validation
        private bool ValidateForm()
        {
            // Required combo selections
            if (comboItem.SelectedIndex <= 0 ||
                comboRequestedBy.SelectedIndex <= 0 ||
                comboReviewedBy.SelectedIndex <= 0 ||   // NEW
                comboAdjType.SelectedIndex == -1 ||
                comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select all required dropdown fields.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            // Required text fields
            if (string.IsNullOrWhiteSpace(txtPhysicalCount.Text) ||
                string.IsNullOrWhiteSpace(txtSystemCount.Text) ||
                string.IsNullOrWhiteSpace(txtAdjCount.Text) ||
                string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show(
                    "All fields marked with * are required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            // Numeric validation
            if (!int.TryParse(txtPhysicalCount.Text, out int physical) || physical < 0 ||
                !int.TryParse(txtSystemCount.Text, out int system) || system < 0 ||
                !int.TryParse(txtAdjCount.Text, out _))
            {
                MessageBox.Show(
                    "Physical Count, System Count, and Adjustment Count must be valid whole numbers.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region Navigation
        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Stock Adjustments";

            parent.pnlContent.Controls.Clear();
            var listForm = new StockAdjustment
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Add(listForm);
            listForm.Show();
        }
        #endregion

        #region KeyPress (numeric only)
        private void txtPhysicalCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtSystemCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void txtAdjCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        #endregion
    }
}