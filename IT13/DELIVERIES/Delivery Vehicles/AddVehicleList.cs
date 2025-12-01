using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddVehicleList : Form
    {
        private readonly string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public AddVehicleList()
        {
            InitializeComponent();
            SetupStatusComboBox();
        }

        private void SetupStatusComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "Active", "Inactive", "Maintenance" });
            cmbStatus.SelectedIndex = 0; // Default: Active
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

            // Save to Database
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO vehicles (VehicleName, LicensePlate, Status, created_at, updated_at) 
                                   VALUES (@VehicleName, @LicensePlate, @Status, SYSUTCDATETIME(), SYSUTCDATETIME())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VehicleName", txtVehicleName.Text.Trim());
                        cmd.Parameters.AddWithValue("@LicensePlate", txtPlateNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Delivery Vehicle added successfully!\n" +
                                          $"Vehicle Name: {txtVehicleName.Text}\n" +
                                          $"Plate Number: {txtPlateNumber.Text}\n" +
                                          $"Status: {cmbStatus.Text}",
                                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add vehicle. Please try again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToList();
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