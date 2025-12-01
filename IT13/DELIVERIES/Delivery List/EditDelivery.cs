using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditDelivery : Form
    {
        private readonly string connString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly long _deliveryId;

        public EditDelivery(string deliveryId)
        {
            // Convert string "DEL 2025-001" or just "1" to long
            if (long.TryParse(deliveryId.Replace("DEL ", "").Replace("DEL-", "").Trim(), out long id))
            {
                _deliveryId = id;
            }
            else
            {
                _deliveryId = 0;
            }

            InitializeComponent();
            SetupCombos();
            datePicker.Value = DateTime.Today;
            LoadCustomerOrders();
            LoadVehicles();
            LoadEmployees();
            LoadDeliveryData();
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
            string query = "SELECT id, VehicleName, LicensePlate FROM vehicles ORDER BY VehicleName";

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

        #region Load Delivery Data
        private void LoadDeliveryData()
        {
            string query = @"SELECT CustOrderID, user_id, VehicleID, DeliveryDate, Status 
                           FROM deliveries 
                           WHERE DeliveryID = @DeliveryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DeliveryID", _deliveryId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                long custOrderId = Convert.ToInt64(reader["CustOrderID"]);
                                long userId = Convert.ToInt64(reader["user_id"]);
                                long vehicleId = Convert.ToInt64(reader["VehicleID"]);
                                DateTime deliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);
                                string status = reader["Status"].ToString();

                                // Set Customer Order
                                for (int i = 1; i < comboCustomerOrder.Items.Count; i++)
                                {
                                    var item = (ComboBoxItem)comboCustomerOrder.Items[i];
                                    if (Convert.ToInt64(item.Value) == custOrderId)
                                    {
                                        comboCustomerOrder.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set Vehicle
                                for (int i = 1; i < comboVehicle.Items.Count; i++)
                                {
                                    var item = (ComboBoxItem)comboVehicle.Items[i];
                                    if (Convert.ToInt64(item.Value) == vehicleId)
                                    {
                                        comboVehicle.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set Employee
                                for (int i = 1; i < comboEmployee.Items.Count; i++)
                                {
                                    var item = (ComboBoxItem)comboEmployee.Items[i];
                                    if (Convert.ToInt64(item.Value) == userId)
                                    {
                                        comboEmployee.SelectedIndex = i;
                                        break;
                                    }
                                }

                                // Set Date and Status
                                datePicker.Value = deliveryDate;
                                comboStatus.SelectedItem = status;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading delivery data: {ex.Message}", "Error",
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

                string query = @"UPDATE deliveries 
                               SET CustOrderID = @CustOrderID, 
                                   user_id = @UserID, 
                                   VehicleID = @VehicleID, 
                                   DeliveryDate = @DeliveryDate, 
                                   Status = @Status,
                                   updated_at = SYSUTCDATETIME()
                               WHERE DeliveryID = @DeliveryID";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DeliveryID", _deliveryId);
                        cmd.Parameters.AddWithValue("@CustOrderID", orderItem.Value);
                        cmd.Parameters.AddWithValue("@UserID", employeeItem.Value);
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleItem.Value);
                        cmd.Parameters.AddWithValue("@DeliveryDate", datePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", comboStatus.SelectedItem.ToString());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show($"Delivery DEL {_deliveryId} updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update delivery.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating delivery: {ex.Message}", "Error",
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