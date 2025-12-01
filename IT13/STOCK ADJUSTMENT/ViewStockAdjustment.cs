using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewStockAdjustment : Form
    {
        private readonly string _adjId;
        private readonly string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewStockAdjustment(string adjustmentId)
        {
            _adjId = adjustmentId;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Clear any existing data
                ClearForm();

                // Extract numeric ID from "ADJ-123" format if needed
                string numericId = _adjId.Contains("ADJ-") ? _adjId.Replace("ADJ-", "") : _adjId;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            sa.StockAdjustmentID,
                            FORMAT(sa.RequestedDate, 'MM/dd/yyyy') as RequestedDate,
                            pl.ProductName,
                            sa.AdjustmentType,
                            sa.PhysicalCount,
                            sa.SystemCount,
                            sa.AdjustCount,
                            sa.Reason,
                            sa.Status,
                            CONCAT(reqEmp.FirstName, ' ', reqEmp.LastName) AS RequestedBy,
                            CONCAT(revEmp.FirstName, ' ', revEmp.LastName) AS ReviewedBy,
                            FORMAT(sa.ReviewedDate, 'MM/dd/yyyy') as ReviewedDate
                        FROM stock_adjustments sa
                        INNER JOIN stock_items si ON sa.StockItemID = si.StockItemID
                        INNER JOIN product_list pl ON si.ProductID = pl.ProdID
                        INNER JOIN users reqUser ON sa.RequestedBy = reqUser.id
                        INNER JOIN employees reqEmp ON reqUser.id = reqEmp.UserID
                        LEFT JOIN users revUser ON sa.ReviewedBy = revUser.id
                        LEFT JOIN employees revEmp ON revUser.id = revEmp.UserID
                        WHERE sa.StockAdjustmentID = @AdjustmentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdjustmentID", numericId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Fill the form with data from database
                                txtId.Text = $"ADJ-{reader["StockAdjustmentID"]}";

                                // Set date value
                                if (!string.IsNullOrEmpty(reader["RequestedDate"].ToString()))
                                {
                                    DateTime requestedDate;
                                    if (DateTime.TryParse(reader["RequestedDate"].ToString(), out requestedDate))
                                    {
                                        datePicker.Value = requestedDate;
                                    }
                                }

                                txtItem.Text = reader["ProductName"].ToString();
                                txtRequested.Text = reader["RequestedBy"].ToString();
                                txtReviewed.Text = reader["ReviewedBy"].ToString();
                                txtReason.Text = reader["Reason"].ToString();

                                // Format Adjustment Type
                                string adjType = reader["AdjustmentType"].ToString();
                                txtAdjType.Text = FormatAdjustmentType(adjType);

                                txtPhysical.Text = reader["PhysicalCount"].ToString();
                                txtSystem.Text = reader["SystemCount"].ToString();
                                txtAdjCount.Text = reader["AdjustCount"].ToString();

                                // Set Status with proper color formatting
                                string status = reader["Status"].ToString();
                                txtStatus.Text = status;
                                ApplyStatusColor(status);
                            }
                            else
                            {
                                MessageBox.Show($"No adjustment record found with ID: {_adjId}", "Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ReturnToList();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading adjustment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatAdjustmentType(string adjType)
        {
            switch (adjType.ToLower())
            {
                case "addition":
                    return "Addition";
                case "removal":
                    return "Removal";
                case "correction":
                    return "Correction";
                default:
                    return adjType;
            }
        }

        private void ApplyStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "approved":
                    txtStatus.FillColor = Color.FromArgb(220, 255, 220);
                    txtStatus.ForeColor = Color.FromArgb(0, 100, 0);
                    break;
                case "pending":
                    txtStatus.FillColor = Color.FromArgb(255, 255, 200);
                    txtStatus.ForeColor = Color.FromArgb(153, 102, 0);
                    break;
                case "rejected":
                    txtStatus.FillColor = Color.FromArgb(255, 220, 220);
                    txtStatus.ForeColor = Color.FromArgb(139, 0, 0);
                    break;
                default:
                    txtStatus.FillColor = Color.White;
                    txtStatus.ForeColor = Color.Black;
                    break;
            }
        }

        private void ClearForm()
        {
            txtId.Text = "";
            datePicker.Value = DateTime.Now;
            txtItem.Text = "";
            txtRequested.Text = "";
            txtReviewed.Text = "";
            txtReason.Text = "";
            txtAdjType.Text = "";
            txtPhysical.Text = "";
            txtSystem.Text = "";
            txtAdjCount.Text = "";
            txtStatus.Text = "";
            txtStatus.FillColor = Color.White;
            txtStatus.ForeColor = Color.Black;
        }

        private void btnBack_Click(object sender, EventArgs e) => ReturnToList();

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Stock Adjustments";
            parent.pnlContent.Controls.Clear();

            var stockAdjForm = new StockAdjustment
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Add(stockAdjForm);
            stockAdjForm.Show();
            this.Close();
        }

        // Optional: Add a refresh button handler if needed
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}