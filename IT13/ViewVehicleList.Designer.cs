// ViewVehicleList.designer.cs
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class ViewVehicleList
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
            lblTitle = new Label();

            lblPlate = new Label();
            lblPlateValue = new Label();

            lblName = new Label();
            lblNameValue = new Label();

            lblStatus = new Label();
            lblStatusValue = new Label();

            lblCreated = new Label();
            lblCreatedValue = new Label();

            lblUpdated = new Label();
            lblUpdatedValue = new Label();

            btnClose = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(lblTitle);
            mainpanel.Controls.Add(lblPlate); mainpanel.Controls.Add(lblPlateValue);
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(lblNameValue);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(lblStatusValue);
            mainpanel.Controls.Add(lblCreated); mainpanel.Controls.Add(lblCreatedValue);
            mainpanel.Controls.Add(lblUpdated); mainpanel.Controls.Add(lblUpdatedValue);
            mainpanel.Controls.Add(btnClose);

            // Title
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(77, 50);
            lblTitle.Text = "View Delivery Vehicle";

            int y = 140;

            // Plate Number
            lblPlate.AutoSize = true;
            lblPlate.Location = new Point(77, y);
            lblPlate.Text = "Plate Number";
            lblPlateValue.AutoSize = true;
            lblPlateValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPlateValue.Location = new Point(77, y + 35);
            y += 100;

            // Vehicle Name
            lblName.AutoSize = true;
            lblName.Location = new Point(77, y);
            lblName.Text = "Vehicle Name";
            lblNameValue.AutoSize = true;
            lblNameValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNameValue.Location = new Point(77, y + 35);
            y += 100;

            // Status
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(77, y);
            lblStatus.Text = "Status";
            lblStatusValue.AutoSize = true;
            lblStatusValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStatusValue.Location = new Point(77, y + 35);
            y += 100;

            // Created At
            lblCreated.AutoSize = true;
            lblCreated.Location = new Point(77, y);
            lblCreated.Text = "Created At";
            lblCreatedValue.AutoSize = true;
            lblCreatedValue.Font = new Font("Segoe UI", 11F);
            lblCreatedValue.ForeColor = Color.Gray;
            lblCreatedValue.Location = new Point(77, y + 35);
            y += 100;

            // Updated At
            lblUpdated.AutoSize = true;
            lblUpdated.Location = new Point(77, y);
            lblUpdated.Text = "Updated At";
            lblUpdatedValue.AutoSize = true;
            lblUpdatedValue.Font = new Font("Segoe UI", 11F);
            lblUpdatedValue.ForeColor = Color.Gray;
            lblUpdatedValue.Location = new Point(77, y + 35);

            // Close Button
            btnClose.Location = new Point(1450, 680);
            btnClose.Size = new Size(180, 52);
            btnClose.Text = "Close";
            btnClose.FillColor = Color.FromArgb(0, 123, 255);
            btnClose.ForeColor = Color.White;
            btnClose.BorderRadius = 10;
            btnClose.Click += btnClose_Click;

            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "ViewVehicleList";
            mainpanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblTitle;
        private Label lblPlate, lblPlateValue;
        private Label lblName, lblNameValue;
        private Label lblStatus, lblStatusValue;
        private Label lblCreated, lblCreatedValue;
        private Label lblUpdated, lblUpdatedValue;
        private Guna2Button btnClose;
    }
}