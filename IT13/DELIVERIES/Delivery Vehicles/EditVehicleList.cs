using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditVehicleList : Form
    {
        private readonly string _connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly string _vehicleId;
        private string _originalLicensePlate;

        public EditVehicleList(string vehicleId)
        {
            _vehicleId = vehicleId;
            InitializeComponent();
            SetupStatusComboBox();
            LoadVehicleData();
        }

        private void SetupStatusComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new[] { "Active", "Inactive", "Maintenance" });
        }

        private void LoadVehicleData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"SELECT VehicleName, LicensePlate, Status 
                                   FROM vehicles 
                                   WHERE id = @VehicleId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleId", _vehicleId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtVehicleName.Text = reader["VehicleName"].ToString();
                                txtPlateNumber.Text = reader["LicensePlate"].ToString();
                                _originalLicensePlate = txtPlateNumber.Text;

                                string status = reader["Status"].ToString();
                                int statusIndex = cmbStatus.FindStringExact(status);
                                cmbStatus.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;
                            }
                            else
                            {
                                MessageBox.Show("Vehicle not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ReturnToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicle data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReturnToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtVehicleName.Text))
            {
                MessageBox.Show("Vehicle Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVehicleName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPlateNumber.Text))
            {
                MessageBox.Show("Plate Number is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNumber.Focus();
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update database
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Check if license plate exists (excluding current vehicle)
                    if (txtPlateNumber.Text.Trim() != _originalLicensePlate)
                    {
                        string checkQuery = @"SELECT COUNT(*) FROM vehicles 
                                            WHERE LicensePlate = @LicensePlate 
                                            AND id != @VehicleId";

                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@LicensePlate", txtPlateNumber.Text.Trim());
                            checkCmd.Parameters.AddWithValue("@VehicleId", _vehicleId);
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("A vehicle with this license plate already exists.", "Duplicate Entry",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtPlateNumber.Focus();
                                return;
                            }
                        }
                    }

                    // Update vehicle
                    string updateQuery = @"UPDATE vehicles 
                                         SET VehicleName = @VehicleName, 
                                             LicensePlate = @LicensePlate, 
                                             Status = @Status,
                                             updated_at = SYSUTCDATETIME()
                                         WHERE id = @VehicleId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleName", txtVehicleName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LicensePlate", txtPlateNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                        cmd.Parameters.AddWithValue("@VehicleId", _vehicleId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Vehicle updated successfully!\n" +
                                          $"Vehicle Name: {txtVehicleName.Text}\n" +
                                          $"Plate Number: {txtPlateNumber.Text}\n" +
                                          $"Status: {cmbStatus.Text}",
                                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made or vehicle not found.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? All unsaved changes will be lost.",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ReturnToList();
            }
        }

        private void ReturnToList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Delivery Vehicles";
            var listForm = new DeliveryVehicleList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(listForm);
            listForm.Show();
        }
    }
}