using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class AddVehicleList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainpanel = new Guna2ShadowPanel();
            lblHeader = new Label();
            lblRequired = new Label();
            lblPlateNumber = new Label();
            txtPlateNumber = new Guna2TextBox();
            lblVehicleName = new Label();
            txtVehicleName = new Guna2TextBox();
            lblStatus = new Label();
            cmbStatus = new Guna2ComboBox();
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
            mainpanel.Controls.Add(lblPlateNumber);
            mainpanel.Controls.Add(txtPlateNumber);
            mainpanel.Controls.Add(lblVehicleName);
            mainpanel.Controls.Add(txtVehicleName);
            mainpanel.Controls.Add(lblStatus);
            mainpanel.Controls.Add(cmbStatus);
            mainpanel.Controls.Add(btnCancel);
            mainpanel.Controls.Add(btnSave);

            // === HEADER (kept Tahoma as requested) ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "Add Delivery Vehicle";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            // === REQUIRED TEXT ===
            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Tahoma", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === PLATE NUMBER ===
            lblPlateNumber.AutoSize = true;
            lblPlateNumber.Location = new Point(77, 110);
            lblPlateNumber.Text = "Plate Number *";
            lblPlateNumber.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblPlateNumber.ForeColor = Color.FromArgb(70, 70, 70);

            txtPlateNumber.Location = new Point(77, 140);
            txtPlateNumber.Size = new Size(600, 45);
            txtPlateNumber.BorderRadius = 8;
            txtPlateNumber.BorderColor = Color.FromArgb(200, 200, 200);
            txtPlateNumber.BorderThickness = 1;
            txtPlateNumber.FillColor = Color.White;
            txtPlateNumber.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtPlateNumber.ForeColor = Color.Black;                   
            txtPlateNumber.PlaceholderText = "e.g. NCR 1234";
            txtPlateNumber.PlaceholderForeColor = Color.Gray;

            // === VEHICLE NAME ===
            lblVehicleName.AutoSize = true;
            lblVehicleName.Location = new Point(750, 110);
            lblVehicleName.Text = "Vehicle Name *";
            lblVehicleName.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblVehicleName.ForeColor = Color.FromArgb(70, 70, 70);

            txtVehicleName.Location = new Point(750, 140);
            txtVehicleName.Size = new Size(600, 45);
            txtVehicleName.BorderRadius = 8;
            txtVehicleName.BorderColor = Color.FromArgb(200, 200, 200);
            txtVehicleName.BorderThickness = 1;
            txtVehicleName.FillColor = Color.White;
            txtVehicleName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtVehicleName.ForeColor = Color.Black;                   
            txtVehicleName.PlaceholderText = "e.g. Toyota Hiace Van";
            txtVehicleName.PlaceholderForeColor = Color.Gray;

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(77, 210);
            lblStatus.Text = "Status *";
            lblStatus.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblStatus.ForeColor = Color.FromArgb(70, 70, 70);

            cmbStatus.Location = new Point(77, 240);
            cmbStatus.Size = new Size(600, 45);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.BorderRadius = 8;
            cmbStatus.BorderColor = Color.FromArgb(200, 200, 200);
            cmbStatus.BorderThickness = 1;
            cmbStatus.FillColor = Color.White;
            cmbStatus.Font = new Font("Bahnschrift SemiCondensed", 11F);
            cmbStatus.ForeColor = Color.Black;                        

            // === CANCEL BUTTON ===
            btnCancel.Location = new Point(1150, 700);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.Click += btnCancel_Click;

            // === SAVE BUTTON ===
            btnSave.Location = new Point(1300, 700);
            btnSave.Size = new Size(200, 50);
            btnSave.Text = "Save Vehicle";
            btnSave.FillColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.BorderRadius = 8;
            btnSave.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnSave.Click += btnSave_Click;

            // === FORM ===
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "AddVehicleList";
            Text = "Add Delivery Vehicle";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblRequired;
        private Label lblPlateNumber;
        private Guna2TextBox txtPlateNumber;
        private Label lblVehicleName;
        private Guna2TextBox txtVehicleName;
        private Label lblStatus;
        private Guna2ComboBox cmbStatus;
        private Guna2Button btnCancel;
        private Guna2Button btnSave;
    }
}