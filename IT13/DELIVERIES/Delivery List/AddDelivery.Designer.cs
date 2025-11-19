// ---------------------------------------------------------------------
// AddDelivery.designer.cs 
// ---------------------------------------------------------------------
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class AddDelivery
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainpanel = new Guna2ShadowPanel();
            lblHeader = new Label();
            lblRequired = new Label();

            lblCustomerOrder = new Label(); comboCustomerOrder = new Guna2ComboBox();
            lblVehicle = new Label(); comboVehicle = new Guna2ComboBox();
            lblEmployee = new Label(); comboEmployee = new Guna2ComboBox();
            lblDeliveryDate = new Label(); datePicker = new Guna2DateTimePicker();
            lblStatus = new Label(); comboStatus = new Guna2ComboBox();

            btnCancel = new Guna2Button();
            btnSave = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(lblHeader);
            mainpanel.Controls.Add(lblRequired);
            mainpanel.Controls.Add(lblCustomerOrder); mainpanel.Controls.Add(comboCustomerOrder);
            mainpanel.Controls.Add(lblVehicle); mainpanel.Controls.Add(comboVehicle);
            mainpanel.Controls.Add(lblEmployee); mainpanel.Controls.Add(comboEmployee);
            mainpanel.Controls.Add(lblDeliveryDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(comboStatus);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnSave);

            // === HEADER & REQUIRED TEXT ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "Create Delivery";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Segoe UI", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === CUSTOMER ORDER ===
            lblCustomerOrder.AutoSize = true;
            lblCustomerOrder.Location = new Point(77, 110);
            lblCustomerOrder.Text = "Customer Order *";
            lblCustomerOrder.Font = new Font("Segoe UI", 10F);
            lblCustomerOrder.ForeColor = Color.FromArgb(70, 70, 70);

            comboCustomerOrder.Location = new Point(77, 140);
            comboCustomerOrder.Size = new Size(600, 45);
            comboCustomerOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCustomerOrder.BorderRadius = 8;
            comboCustomerOrder.BorderColor = Color.FromArgb(200, 200, 200);
            comboCustomerOrder.BorderThickness = 1;
            comboCustomerOrder.FillColor = Color.White;
            comboCustomerOrder.Font = new Font("Segoe UI", 11F);

            // === DELIVERY VEHICLE ===
            lblVehicle.AutoSize = true;
            lblVehicle.Location = new Point(750, 110);
            lblVehicle.Text = "Delivery Vehicle *";
            lblVehicle.Font = new Font("Segoe UI", 10F);
            lblVehicle.ForeColor = Color.FromArgb(70, 70, 70);

            comboVehicle.Location = new Point(750, 140);
            comboVehicle.Size = new Size(600, 45);
            comboVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
            comboVehicle.BorderRadius = 8;
            comboVehicle.BorderColor = Color.FromArgb(200, 200, 200);
            comboVehicle.BorderThickness = 1;
            comboVehicle.FillColor = Color.White;
            comboVehicle.Font = new Font("Segoe UI", 11F);

            // === EMPLOYEE ===
            lblEmployee.AutoSize = true;
            lblEmployee.Location = new Point(77, 210);
            lblEmployee.Text = "Employee *";
            lblEmployee.Font = new Font("Segoe UI", 10F);
            lblEmployee.ForeColor = Color.FromArgb(70, 70, 70);

            comboEmployee.Location = new Point(77, 240);
            comboEmployee.Size = new Size(600, 45);
            comboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEmployee.BorderRadius = 8;
            comboEmployee.BorderColor = Color.FromArgb(200, 200, 200);
            comboEmployee.BorderThickness = 1;
            comboEmployee.FillColor = Color.White;
            comboEmployee.Font = new Font("Segoe UI", 11F);

            // === DELIVERY DATE ===
            lblDeliveryDate.AutoSize = true;
            lblDeliveryDate.Location = new Point(750, 210);
            lblDeliveryDate.Text = "Delivery Date *";
            lblDeliveryDate.Font = new Font("Segoe UI", 10F);
            lblDeliveryDate.ForeColor = Color.FromArgb(70, 70, 70);

            datePicker.Location = new Point(750, 240);
            datePicker.Size = new Size(600, 45);  // ← FIXED: 45px
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(77, 310);
            lblStatus.Text = "Status *";
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(70, 70, 70);

            comboStatus.Location = new Point(77, 340);
            comboStatus.Size = new Size(600, 45);
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStatus.BorderRadius = 8;
            comboStatus.BorderColor = Color.FromArgb(200, 200, 200);
            comboStatus.BorderThickness = 1;
            comboStatus.FillColor = Color.White;
            comboStatus.Font = new Font("Segoe UI", 11F);

            // === CANCEL BUTTON ===
            btnCancel.Location = new Point(1150, 700);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            // === SAVE BUTTON ===
            btnSave.Location = new Point(1300, 700);
            btnSave.Size = new Size(200, 50);
            btnSave.Text = "Save Delivery";
            btnSave.FillColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.BorderRadius = 8;
            btnSave.Click += btnSave_Click;

            // === FORM ===
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "AddDelivery";
            Text = "Add Delivery";
            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblRequired;

        private Label lblCustomerOrder; private Guna2ComboBox comboCustomerOrder;
        private Label lblVehicle; private Guna2ComboBox comboVehicle;
        private Label lblEmployee; private Guna2ComboBox comboEmployee;
        private Label lblDeliveryDate; private Guna2DateTimePicker datePicker;
        private Label lblStatus; private Guna2ComboBox comboStatus;

        private Guna2Button btnCancel;
        private Guna2Button btnSave;
    }
}