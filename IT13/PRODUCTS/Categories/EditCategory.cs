using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditCategory : Form
    {
        private readonly string _categoryId;
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public EditCategory(string categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Extract numeric ID from "CAT-001" format
                string numericId = _categoryId.Replace("CAT-", "");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CategoryName, Date, Status FROM categories WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", numericId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtId.Text = _categoryId;
                                txtName.Text = reader["CategoryName"].ToString();

                                // Set date
                                if (reader["Date"] != DBNull.Value)
                                {
                                    datePicker.Value = Convert.ToDateTime(reader["Date"]);
                                }
                                else
                                {
                                    datePicker.Value = DateTime.Today;
                                }

                                // Set status
                                string status = reader["Status"].ToString();
                                comboStatus.SelectedItem = status;
                            }
                            else
                            {
                                MessageBox.Show("Category not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ReturnToList();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReturnToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReturnToList();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Extract numeric ID from "CAT-001" format
                string numericId = _categoryId.Replace("CAT-", "");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE categories 
                                   SET CategoryName = @name, 
                                       Date = @date, 
                                       Status = @status 
                                   WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@date", datePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@status", comboStatus.Text);
                        cmd.Parameters.AddWithValue("@id", numericId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Info",
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
            ReturnToList();
        }

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Product Categories";

            parent.pnlContent.Controls.Clear();

            // Create new instance - LoadDataFromDatabase is called in constructor
            var categoryForm = new ProductCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            parent.pnlContent.Controls.Add(categoryForm);
            categoryForm.Show();
        }
    }
}