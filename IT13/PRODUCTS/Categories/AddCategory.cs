using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCategory : Form
    {
        private string connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public AddCategory()
        {
            InitializeComponent();
            txtName.Text = "";
            txtStatus.Text = "Active";
            datePicker.Value = DateTime.Today;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO categories (CategoryName, Date, Status) 
                                   VALUES (@CategoryName, @Date, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date", datePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", txtStatus.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Category added successfully!\n" +
                                          $"Name: {txtName.Text}\n" +
                                          $"Date: {datePicker.Value.ToString("MM/dd/yyyy")}\n" +
                                          $"Status: {txtStatus.Text}",
                                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReturnToList();
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
            parent.pnlContent.Controls.Add(new ProductCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            });
            parent.pnlContent.Controls[0].Show();
        }
    }
}