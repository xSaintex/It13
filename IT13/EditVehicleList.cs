// EditVehicleList.cs
using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class EditVehicleList : Form
    {
        private readonly string _vehicleId;

        public EditVehicleList(string vehicleId)
        {
            _vehicleId = vehicleId;
            InitializeComponent();
            SetupStatusComboBox();
            LoadVehicleData();
        }

        private void SetupStatusComboBox()
        {
            cmbStatus.Items.AddRange(new[] { "Available", "In Transit", "Maintenance", "Inactive" });
        }

        private void LoadVehicleData()
        {
            // SAMPLE DATA - Replace with actual DB/API call later
            txtPlateNumber.Text = "NCR 1234";
            txtVehicleName.Text = "Toyota Hiace Van";
            cmbStatus.SelectedIndex = 0; // Available
            // In real app: fetch by _vehicleId and populate
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlateNumber.Text))
            {
                MessageBox.Show("Plate Number is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNumber.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtVehicleName.Text))
            {
                MessageBox.Show("Vehicle Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVehicleName.Focus();
                return;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Vehicle {_vehicleId} updated successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

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