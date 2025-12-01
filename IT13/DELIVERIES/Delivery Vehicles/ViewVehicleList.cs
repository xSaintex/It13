using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewVehicleList : Form
    {
        private readonly string _vehicleId;
        private readonly string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public ViewVehicleList(string vehicleId)
        {
            _vehicleId = vehicleId;
            InitializeComponent();
            LoadVehicleData();
            ApplyStatusColor();
        }

        private void LoadVehicleData()
        {
            try
            {
                // Extract numeric ID from "VH-001" format if needed
                string numericId = _vehicleId;
                if (_vehicleId.Contains("VH-"))
                {
                    numericId = _vehicleId.Replace("VH-", "").TrimStart('0');
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            id,
                            VehicleName,
                            LicensePlate,
                            Status,
                            FORMAT(created_at, 'MMMM dd, yyyy hh:mm tt') as CreatedAt,
                            FORMAT(updated_at, 'MMMM dd, yyyy hh:mm tt') as UpdatedAt
                        FROM vehicles 
                        WHERE id = @VehicleId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleId", numericId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display formatted ID
                                lblPlateValue.Text = reader["LicensePlate"]?.ToString() ?? "N/A";
                                lblNameValue.Text = reader["VehicleName"]?.ToString() ?? "N/A";

                                // Format status with proper casing
                                string status = reader["Status"]?.ToString() ?? "Active";
                                lblStatusValue.Text = FormatStatus(status);

                                // Date information
                                lblCreatedValue.Text = reader["CreatedAt"]?.ToString() ?? "N/A";
                                lblUpdatedValue.Text = reader["UpdatedAt"]?.ToString() ?? "N/A";

                                // Update form title with vehicle info
                                UpdateFormTitle(reader["VehicleName"]?.ToString(), reader["LicensePlate"]?.ToString());

                                // Apply color to status label
                                ApplyStatusColor();
                            }
                            else
                            {
                                MessageBox.Show($"Vehicle with ID {_vehicleId} not found.", "Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CloseForm();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicle data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData();
            }
        }

        private string FormatStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                return "Active";

            status = status.ToLower();
            return status switch
            {
                "active" => "Active",
                "inactive" => "Inactive",
                "maintenance" => "Maintenance",
                "available" => "Available",
                "unavailable" => "Unavailable",
                "repair" => "Under Repair",
                _ => char.ToUpper(status[0]) + status.Substring(1)
            };
        }

        private void UpdateFormTitle(string vehicleName, string licensePlate)
        {
            string displayText = "";

            if (!string.IsNullOrEmpty(vehicleName) && !string.IsNullOrEmpty(licensePlate))
                displayText = $"{vehicleName} ({licensePlate})";
            else if (!string.IsNullOrEmpty(vehicleName))
                displayText = vehicleName;
            else if (!string.IsNullOrEmpty(licensePlate))
                displayText = $"Vehicle {licensePlate}";
            else
                displayText = _vehicleId;

            this.Text = $"View Delivery Vehicle: {displayText}";
            lblTitle.Text = $"View Delivery Vehicle: {displayText}";
        }

        private void ApplyStatusColor()
        {
            string status = lblStatusValue.Text.ToLower();

            switch (status)
            {
                case "active":
                case "available":
                    lblStatusValue.ForeColor = Color.FromArgb(34, 197, 94); // Green
                    break;

                case "inactive":
                case "unavailable":
                    lblStatusValue.ForeColor = Color.FromArgb(239, 68, 68); // Red
                    break;

                case "maintenance":
                case "under repair":
                    lblStatusValue.ForeColor = Color.FromArgb(255, 193, 7); // Yellow/Amber
                    break;

                default:
                    lblStatusValue.ForeColor = Color.FromArgb(108, 117, 125); // Gray
                    break;
            }

            // Make the status text bold for better visibility
            lblStatusValue.Font = new Font(lblStatusValue.Font, FontStyle.Bold);
        }

        private void LoadSampleData()
        {
            // Fallback sample data
            lblPlateValue.Text = "NCR 1234";
            lblNameValue.Text = "Toyota Hiace Van";
            lblStatusValue.Text = "Available";
            lblCreatedValue.Text = "January 15, 2024 10:30 AM";
            lblUpdatedValue.Text = "March 22, 2025 02:20 PM";

            ApplyStatusColor();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                // Navigate back to vehicle list
                parent.navBar1.PageTitle = "Delivery Vehicles";
                parent.pnlContent.Controls.Clear();

                var vehicleList = new DeliveryVehicleList()
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                parent.pnlContent.Controls.Add(vehicleList);
                vehicleList.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}