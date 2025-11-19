// ViewVehicleList.cs
using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewVehicleList : Form
    {
        private readonly string _vehicleId;

        public ViewVehicleList(string vehicleId)
        {
            _vehicleId = vehicleId;
            InitializeComponent();
            LoadVehicleData();
        }

        private void LoadVehicleData()
        {
            // SAMPLE DATA - Replace with real data later
            lblPlateValue.Text = "NCR 1234";
            lblNameValue.Text = "Toyota Hiace Van";
            lblStatusValue.Text = "Available";
            lblCreatedValue.Text = "January 15, 2024 10:30 AM";
            lblUpdatedValue.Text = "March 22, 2025 02:20 PM";
        }

        private void btnClose_Click(object sender, EventArgs e)
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