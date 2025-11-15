using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddVehicleList : Form
    {
        public AddVehicleList()
        {
            InitializeComponent();
            SetupStatusComboBox();
        }

        private void SetupStatusComboBox()
        {
            cmbStatus.Items.AddRange(new string[] { "Available", "In Transit", "Maintenance", "Inactive" });
            cmbStatus.SelectedIndex = 0; // Default: Available
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlateNumber.Text))
            {
                MessageBox.Show("Plate Number is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNumber.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtVehicleName.Text))
            {
                MessageBox.Show("Vehicle Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVehicleName.Focus();
                return;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Delivery Vehicle added successfully!\n" +
                            $"Plate Number: {txtPlateNumber.Text}\n" +
                            $"Vehicle Name: {txtVehicleName.Text}\n" +
                            $"Status: {cmbStatus.Text}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReturnToList();
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