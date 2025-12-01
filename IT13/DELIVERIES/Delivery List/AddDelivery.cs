// ---------------------------------------------------------------------
// AddDelivery.cs - Database Connected Version
// ---------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddDelivery : Form
    {
        private readonly string connString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public AddDelivery()
        {
            InitializeComponent();
            SetupCombos();
            datePicker.Value = DateTime.Today;
            LoadCustomerOrders();
            LoadVehicles();
            LoadEmployees();
        }

        #region ComboBox Setup
        private void SetupCombos()
        {
            comboCustomerOrder.Items.Clear();
            comboCustomerOrder.Items.Add("Select a customer order");
            comboCustomerOrder.SelectedIndex = 0;

            comboVehicle.Items.Clear();
            comboVehicle.Items.Add("Select a vehicle");
            comboVehicle.SelectedIndex = 0;

            comboEmployee.Items.Clear();
            comboEmployee.Items.Add("Select an employee");
            comboEmployee.SelectedIndex = 0;

            comboStatus.Items.Clear();
            comboStatus.Items.AddRange(new[] { "Pending", "In Transit", "Delivered" });
            comboStatus.SelectedIndex = 0;
        }

        private void LoadCustomerOrders()
        {
            string query = @"SELECT co.CusOrderID, c.CompanyName 
                           FROM customer_orders co
                           INNER JOIN customers c ON co.CustomerID = c.CustID
                           WHERE co.Status = 'confirmed'
                           ORDER BY co.CusOrderID DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string orderDisplay = $"ORD-{reader["CusOrderID"]} - {reader["CompanyName"]}";
                            comboCustomerOrder.Items.Add(new ComboBoxItem
                            {
                                Text = orderDisplay,
                                Value = reader["CusOrderID"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVehicles()
        {
            string query = "SELECT id, VehicleName, LicensePlate FROM vehicles WHERE Status = 'active' ORDER BY VehicleName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string vehicleDisplay = $"{reader["VehicleName"]} - {reader["LicensePlate"]}";
                            comboVehicle.Items.Add(new ComboBoxItem
                            {
                                Text = vehicleDisplay,
                                Value = reader["id"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            string query = @"SELECT u.id, (e.FirstName + ' ' + e.LastName) AS FullName 
                           FROM users u
                           INNER JOIN employees e ON u.id = e.UserID
                           INNER JOIN roles r ON u.role_id = r.RoleID
                           WHERE r.Status = 'active'
                           ORDER BY e.FirstName, e.LastName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboEmployee.Items.Add(new ComboBoxItem
                            {
                                Text = reader["FullName"].ToString(),
                                Value = reader["id"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Clicks
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                var orderItem = (ComboBoxItem)comboCustomerOrder.SelectedItem;
                var vehicleItem = (ComboBoxItem)comboVehicle.SelectedItem;
                var employeeItem = (ComboBoxItem)comboEmployee.SelectedItem;

                string query = @"INSERT INTO deliveries 
                               (CustOrderID, user_id, VehicleID, DeliveryDate, Status, created_at, updated_at) 
                               VALUES 
                               (@CustOrderID, @UserID, @VehicleID, @DeliveryDate, @Status, SYSUTCDATETIME(), SYSUTCDATETIME())";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustOrderID", orderItem.Value);
                        cmd.Parameters.AddWithValue("@UserID", employeeItem.Value);
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleItem.Value);
                        cmd.Parameters.AddWithValue("@DeliveryDate", datePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", comboStatus.SelectedItem.ToString());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Delivery added successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add delivery.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving delivery: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => ReturnToList();
        #endregion

        #region Validation
        private bool ValidateForm()
        {
            if (comboCustomerOrder.SelectedIndex <= 0 ||
                comboVehicle.SelectedIndex <= 0 ||
                comboEmployee.SelectedIndex <= 0 ||
                comboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select all required dropdown fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region Navigation
        private void ReturnToList()
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Delivery List";
            parent.pnlContent.Controls.Clear();

            var listForm = new DeliveryList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Add(listForm);
            listForm.Show();
        }
        #endregion

        #region Helper Class
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        #endregion
    }
}