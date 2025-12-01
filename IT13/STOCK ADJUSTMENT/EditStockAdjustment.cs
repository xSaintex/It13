using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditStockAdjustment : Form
    {
        private readonly string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly string _adjId;

        public EditStockAdjustment(string adjustmentId)
        {
            _adjId = adjustmentId;
            InitializeComponent();
            SetupCombos();
            LoadData();
        }

        #region ComboBox Setup
        private void SetupCombos()
        {
            // ----- Inventory Item -----
            LoadInventoryItems();

            // ----- Requested By -----
            LoadEmployees(comboRequestedBy);

            // ----- Reviewed By -----
            LoadEmployees(comboReviewedBy);

            // ----- Adjustment Type -----
            comboAdjType.Items.Clear();
            comboAdjType.Items.AddRange(new[] { "addition", "removal", "correction" });
            comboAdjType.SelectedIndex = 0;

            // ----- Status -----
            comboStatus.Items.Clear();
            comboStatus.Items.AddRange(new[] { "Pending", "Approved", "Rejected" });
            comboStatus.SelectedIndex = 0;
        }

        private void LoadInventoryItems()
        {
            try
            {
                comboItem.Items.Clear();
                comboItem.Items.Add("Select an inventory item");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT 
                            si.StockItemID,
                            pl.ProductName,
                            si.qty
                        FROM stock_items si
                        INNER JOIN product_list pl ON si.ProductID = pl.ProdID
                        WHERE si.Status = 'active'
                        ORDER BY pl.ProductName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ComboBoxItem
                            {
                                Value = reader["StockItemID"].ToString(),
                                Text = $"{reader["ProductName"]} (Qty: {reader["qty"]})"
                            };
                            comboItem.Items.Add(item);
                        }
                    }
                }
                comboItem.DisplayMember = "Text";
                comboItem.ValueMember = "Value";
                comboItem.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory items: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees(ComboBox combo)
        {
            try
            {
                combo.Items.Clear();
                combo.Items.Add("Select an employee");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            u.id,
                            CONCAT(e.FirstName, ' ', e.LastName) AS FullName
                        FROM users u
                        INNER JOIN employees e ON u.id = e.UserID
                        ORDER BY e.FirstName, e.LastName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ComboBoxItem
                            {
                                Value = reader["id"].ToString(),
                                Text = reader["FullName"].ToString()
                            };
                            combo.Items.Add(item);
                        }
                    }
                }
                combo.DisplayMember = "Text";
                combo.ValueMember = "Value";
                combo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class ComboBoxItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }
        #endregion

        #region Load Data from Database
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            sa.StockAdjustmentID,
                            sa.StockItemID,
                            sa.RequestedBy,
                            sa.ReviewedBy,
                            sa.RequestedDate,
                            sa.ReviewedDate,
                            sa.Reason,
                            sa.AdjustmentType,
                            sa.PhysicalCount,
                            sa.SystemCount,
                            sa.AdjustCount,
                            sa.Status
                        FROM stock_adjustments sa
                        WHERE sa.StockAdjustmentID = @AdjId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdjId", long.Parse(_adjId));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtId.Text = $"ADJ-{_adjId}";

                                // Set Inventory Item
                                string stockItemId = reader["StockItemID"].ToString();
                                SelectComboBoxItem(comboItem, stockItemId);

                                // Set Requested By
                                string requestedById = reader["RequestedBy"].ToString();
                                SelectComboBoxItem(comboRequestedBy, requestedById);

                                // Set Reviewed By
                                if (reader["ReviewedBy"] != DBNull.Value)
                                {
                                    string reviewedById = reader["ReviewedBy"].ToString();
                                    SelectComboBoxItem(comboReviewedBy, reviewedById);
                                }

                                // Set Adjustment Type
                                string adjType = reader["AdjustmentType"].ToString();
                                int typeIndex = comboAdjType.Items.IndexOf(adjType);
                                if (typeIndex >= 0) comboAdjType.SelectedIndex = typeIndex;

                                // Set counts
                                txtPhysicalCount.Text = reader["PhysicalCount"].ToString();
                                txtSystemCount.Text = reader["SystemCount"].ToString();
                                txtAdjCount.Text = reader["AdjustCount"].ToString();

                                // Set Status
                                string status = reader["Status"].ToString();
                                int statusIndex = comboStatus.FindStringExact(status);
                                if (statusIndex >= 0) comboStatus.SelectedIndex = statusIndex;

                                // Set Reason
                                txtReason.Text = reader["Reason"].ToString();

                                // Set Date
                                if (reader["RequestedDate"] != DBNull.Value)
                                    datePicker.Value = Convert.ToDateTime(reader["RequestedDate"]);
                            }
                            else
                            {
                                MessageBox.Show("Stock adjustment not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ReturnToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock adjustment: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectComboBoxItem(ComboBox combo, string value)
        {
            for (int i = 1; i < combo.Items.Count; i++)
            {
                if (combo.Items[i] is ComboBoxItem item && item.Value == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }
        #endregion

        #region Button Clicks
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE stock_adjustments 
                        SET StockItemID = @StockItemID,
                            RequestedBy = @RequestedBy,
                            ReviewedBy = @ReviewedBy,
                            RequestedDate = @RequestedDate,
                            ReviewedDate = @ReviewedDate,
                            Reason = @Reason,
                            AdjustmentType = @AdjustmentType,
                            PhysicalCount = @PhysicalCount,
                            SystemCount = @SystemCount,
                            AdjustCount = @AdjustCount,
                            Status = @Status,
                            updated_at = SYSUTCDATETIME()
                        WHERE StockAdjustmentID = @AdjId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Get selected values
                        var stockItem = comboItem.SelectedItem as ComboBoxItem;
                        var requestedBy = comboRequestedBy.SelectedItem as ComboBoxItem;
                        var reviewedBy = comboReviewedBy.SelectedItem as ComboBoxItem;

                        cmd.Parameters.AddWithValue("@AdjId", long.Parse(_adjId));
                        cmd.Parameters.AddWithValue("@StockItemID", long.Parse(stockItem.Value));
                        cmd.Parameters.AddWithValue("@RequestedBy", long.Parse(requestedBy.Value));
                        cmd.Parameters.AddWithValue("@ReviewedBy", long.Parse(reviewedBy.Value));
                        cmd.Parameters.AddWithValue("@RequestedDate", datePicker.Value);
                        cmd.Parameters.AddWithValue("@ReviewedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Reason", txtReason.Text.Trim());
                        cmd.Parameters.AddWithValue("@AdjustmentType", comboAdjType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@PhysicalCount", int.Parse(txtPhysicalCount.Text));
                        cmd.Parameters.AddWithValue("@SystemCount", int.Parse(txtSystemCount.Text));
                        cmd.Parameters.AddWithValue("@AdjustCount", int.Parse(txtAdjCount.Text));
                        cmd.Parameters.AddWithValue("@Status", comboStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "Stock adjustment updated successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ReturnToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock adjustment: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToList();
        #endregion

        #region Validation
        private bool ValidateForm()
        {
            // Required combo selections
            if (comboItem.SelectedIndex <= 0 ||
                comboRequestedBy.SelectedIndex <= 0 ||
                comboReviewedBy.SelectedIndex <= 0 ||
                comboAdjType.SelectedIndex == -1 ||
                comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select valid options for all required fields.",
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
            if (!int.TryParse(txtPhysicalCount.Text, out int physical) || physical < 0)
            {
                MessageBox.Show(
                    "Physical Count must be a non-negative whole number.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSystemCount.Text, out int system) || system < 0)
            {
                MessageBox.Show(
                    "System Count must be a non-negative whole number.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtAdjCount.Text, out _))
            {
                MessageBox.Show(
                    "Adjustment Count must be a valid whole number (negative allowed).",
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
            var txt = sender as Guna2TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '-' && txt.SelectionStart != 0)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '-' && txt.Text.Contains("-"))
            {
                e.Handled = true;
                return;
            }
        }
        #endregion
    }
}