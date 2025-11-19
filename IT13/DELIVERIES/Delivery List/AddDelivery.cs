// ---------------------------------------------------------------------
// AddDelivery.cs 
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace IT13
{
    public partial class AddDelivery : Form
    {
        public AddDelivery()
        {
            InitializeComponent();
            SetupCombos();
            datePicker.Value = DateTime.Today;
        }
        #region ComboBox Setup
        private void SetupCombos()
        {
            // ----- Customer Order -----
            comboCustomerOrder.Items.Clear();
            comboCustomerOrder.Items.Add("Select a customer order");
            comboCustomerOrder.Items.Add("ORD-1001 - ABC Corp");
            comboCustomerOrder.Items.Add("ORD-1002 - XYZ Trading");
            comboCustomerOrder.SelectedIndex = 0;
            // ----- Delivery Vehicle -----
            comboVehicle.Items.Clear();
            comboVehicle.Items.Add("Select a vehicle");
            comboVehicle.Items.Add("Toyota Hiace - ABC123");
            comboVehicle.Items.Add("Mitsubishi L300 - XYZ789");
            comboVehicle.SelectedIndex = 0;
            // ----- Employee -----
            comboEmployee.Items.Clear();
            comboEmployee.Items.Add("Select an employee");
            comboEmployee.Items.Add("Juan Dela Cruz");
            comboEmployee.Items.Add("Maria Santos");
            comboEmployee.SelectedIndex = 0;
            // ----- Status -----
            comboStatus.Items.Clear();
            comboStatus.Items.AddRange(new[] { "Pending", "In Transit", "Delivered" });
            comboStatus.SelectedIndex = 0;
        }
        #endregion
        #region Button Clicks
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            MessageBox.Show(
            "Delivery added successfully!",
            "Success",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            ReturnToList();
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
                MessageBox.Show(
                "Please select all required dropdown fields.",
                "Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
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
    }
}