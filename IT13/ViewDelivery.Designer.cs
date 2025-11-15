// ---------------------------------------------------------------------
// ViewDelivery.designer.cs - FINAL (TOP FIXED, TABLE CLEAN, BUTTONS CENTERED)
// ---------------------------------------------------------------------
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;
namespace IT13
{
    partial class ViewDelivery
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var mainPanel = new Guna2ShadowPanel();
            var scrollPanel = new Guna2Panel();
            var contentPanel = new Guna2Panel();
            var bottomPanel = new Guna2Panel();
            var lblHeader = new Label();
            lblCreated = new Label();
            infoPanel = new Guna2Panel();
            var lblCustomerOrder = new Label(); var txtCustomerOrder = new Label();
            var lblVehicle = new Label(); var txtVehicle = new Label();
            var lblDeliveryDate = new Label(); var txtDeliveryDate = new Label();
            var lblLastAttempt = new Label(); var txtLastAttempt = new Label();
            var lblEmployee = new Label(); var txtEmployee = new Label();
            var lblPlate = new Label(); var txtPlate = new Label();
            var lblStatus = new Label();
            pnlStatusBadge = new Guna2Panel();
            lblStatusValue = new Label();
            btnAttempts = new Guna2Button();
            btnDetails = new Guna2Button();
            pnlAttempts = new Guna2ShadowPanel();
            var lblAttemptsHeader = new Label();
            btnAddAttempt = new Guna2Button();
            dgvAttempts = new DataGridView();
            pnlDetails = new Guna2ShadowPanel();
            var lblInfoHeader = new Label();
            var txtCustomer = new Label();
            var txtShipping = new Label();
            var btnBack = new Guna2Button();
            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            infoPanel.SuspendLayout();
            pnlAttempts.SuspendLayout();
            pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dgvAttempts)).BeginInit();
            this.SuspendLayout();
            // MAIN PANEL
            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 878);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.Controls.Add(scrollPanel);
            mainPanel.Controls.Add(bottomPanel);
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1602, 800);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);
            contentPanel.Size = new Size(1458, 850);
            // HEADER
            lblHeader.Text = "Delivery #1";
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 25);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.FromArgb(30, 30, 30);
            lblCreated.Text = "Created: Oct 11, 2025 08:04 PM";
            lblCreated.Font = new Font("Segoe UI", 9F);
            lblCreated.ForeColor = Color.Gray;
            lblCreated.Location = new Point(77, 65);
            lblCreated.AutoSize = true;
            // INFO PANEL
            infoPanel.Location = new Point(77, 100);
            infoPanel.Size = new Size(1300, 200);
            infoPanel.FillColor = Color.White;
            infoPanel.BorderColor = Color.FromArgb(220, 220, 220);
            infoPanel.BorderThickness = 1;
            infoPanel.BorderRadius = 12;
            int ix = 50, iy = 30, gapY = 65;
            lblCustomerOrder.Text = "Customer Order";
            lblCustomerOrder.Font = new Font("Segoe UI", 10F);
            lblCustomerOrder.Location = new Point(ix, iy);
            lblCustomerOrder.AutoSize = true;
            txtCustomerOrder.Text = "1";
            txtCustomerOrder.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtCustomerOrder.Location = new Point(ix, iy + 22);
            txtCustomerOrder.AutoSize = true;
            lblDeliveryDate.Text = "Delivery Date";
            lblDeliveryDate.Font = new Font("Segoe UI", 10F);
            lblDeliveryDate.Location = new Point(350, iy);
            lblDeliveryDate.AutoSize = true;
            txtDeliveryDate.Text = "Oct 22, 2025";
            txtDeliveryDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtDeliveryDate.Location = new Point(350, iy + 22);
            txtDeliveryDate.AutoSize = true;
            lblLastAttempt.Text = "Last Attempt Date";
            lblLastAttempt.Font = new Font("Segoe UI", 10F);
            lblLastAttempt.Location = new Point(350, iy + gapY);
            lblLastAttempt.AutoSize = true;
            txtLastAttempt.Text = "Oct 22, 2025";
            txtLastAttempt.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtLastAttempt.Location = new Point(350, iy + 22 + gapY);
            txtLastAttempt.AutoSize = true;
            lblEmployee.Text = "Assigned Employee";
            lblEmployee.Font = new Font("Segoe UI", 10F);
            lblEmployee.Location = new Point(650, iy);
            lblEmployee.AutoSize = true;
            txtEmployee.Text = "N/A N/A";
            txtEmployee.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtEmployee.Location = new Point(650, iy + 22);
            txtEmployee.AutoSize = true;
            lblVehicle.Text = "Vehicle";
            lblVehicle.Font = new Font("Segoe UI", 10F);
            lblVehicle.Location = new Point(ix, iy + gapY);
            lblVehicle.AutoSize = true;
            txtVehicle.Text = "L3";
            txtVehicle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtVehicle.Location = new Point(ix, iy + 22 + gapY);
            lblPlate.Text = "Plate Number";
            lblPlate.Font = new Font("Segoe UI", 10F);
            lblPlate.Location = new Point(650, iy + gapY);
            lblPlate.AutoSize = true;
            txtPlate.Text = "JAA 1234";
            txtPlate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtPlate.Location = new Point(650, iy + 22 + gapY);
            // STATUS BADGE
            lblStatus.Text = "Status";
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.Location = new Point(950, iy + gapY);
            lblStatus.AutoSize = true;
            pnlStatusBadge.Location = new Point(950, iy + 22 + gapY);
            pnlStatusBadge.Size = new Size(90, 28);
            pnlStatusBadge.FillColor = Color.FromArgb(255, 253, 230);
            pnlStatusBadge.BorderRadius = 14;
            pnlStatusBadge.BorderThickness = 0;
            lblStatusValue.Text = "Pending";
            lblStatusValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatusValue.ForeColor = Color.FromArgb(184, 134, 11);
            lblStatusValue.Dock = DockStyle.Fill;
            lblStatusValue.TextAlign = ContentAlignment.MiddleCenter;
            lblStatusValue.BackColor = Color.Transparent;
            pnlStatusBadge.Controls.Add(lblStatusValue);
            infoPanel.Controls.AddRange(new Control[] {
lblCustomerOrder, txtCustomerOrder,
lblDeliveryDate, txtDeliveryDate,
lblLastAttempt, txtLastAttempt,
lblEmployee, txtEmployee,
lblVehicle, txtVehicle,
lblPlate, txtPlate,
lblStatus, pnlStatusBadge
});
            // TABS
            int tabY = 320;
            btnAttempts.Text = "Delivery Attempts";
            btnAttempts.Location = new Point(77, tabY);
            btnAttempts.Size = new Size(170, 36);
            btnAttempts.FillColor = Color.FromArgb(0, 123, 255);
            btnAttempts.ForeColor = Color.White;
            btnAttempts.BorderRadius = 8;
            btnAttempts.Font = new Font("Segoe UI", 10F);
            btnAttempts.TextAlign = HorizontalAlignment.Center;
            btnDetails.Text = "Delivery Details";
            btnDetails.Location = new Point(260, tabY);
            btnDetails.Size = new Size(170, 36);
            btnDetails.FillColor = Color.FromArgb(240, 240, 240);
            btnDetails.ForeColor = Color.Black;
            btnDetails.BorderRadius = 8;
            btnDetails.Font = new Font("Segoe UI", 10F);
            btnDetails.TextAlign = HorizontalAlignment.Center;
            // ATTEMPTS PANEL
            pnlAttempts.Location = new Point(77, tabY + 50);
            pnlAttempts.Size = new Size(1300, 360);
            pnlAttempts.FillColor = Color.FromArgb(248, 249, 252);
            pnlAttempts.Radius = 20;
            pnlAttempts.Visible = true;
            lblAttemptsHeader.Text = "Delivery Attempts";
            lblAttemptsHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAttemptsHeader.Location = new Point(30, 25);
            lblAttemptsHeader.AutoSize = true;
            btnAddAttempt.Text = "+ Add Attempt";
            btnAddAttempt.Location = new Point(1080, 20);
            btnAddAttempt.Size = new Size(170, 36);
            btnAddAttempt.FillColor = Color.FromArgb(0, 123, 255);
            btnAddAttempt.ForeColor = Color.White;
            btnAddAttempt.BorderRadius = 8;
            btnAddAttempt.Font = new Font("Segoe UI", 10F);
            btnAddAttempt.Click += btnAddAttempt_Click;
            dgvAttempts.Location = new Point(30, 70);
            dgvAttempts.Size = new Size(1240, 270);
            pnlAttempts.Controls.AddRange(new Control[] { lblAttemptsHeader, btnAddAttempt, dgvAttempts });
            // DETAILS PANEL
            pnlDetails.Location = new Point(77, tabY + 50);
            pnlDetails.Size = new Size(1300, 360);
            pnlDetails.FillColor = Color.FromArgb(248, 249, 252);
            pnlDetails.Radius = 20;
            pnlDetails.Visible = false;
            lblInfoHeader.Text = "Delivery Information";
            lblInfoHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInfoHeader.Location = new Point(30, 25);
            lblInfoHeader.AutoSize = true;
            txtCustomer.Text = "Customer: Incio";
            txtCustomer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtCustomer.Location = new Point(30, 80);
            txtCustomer.AutoSize = true;
            txtShipping.Text = "Shipping Address: dfd, sd, 2324, pl";
            txtShipping.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtShipping.Location = new Point(30, 110);
            txtShipping.AutoSize = true;
            pnlDetails.Controls.AddRange(new Control[] { lblInfoHeader, txtCustomer, txtShipping });
            // BOTTOM
            bottomPanel.Location = new Point(0, 800);
            bottomPanel.Size = new Size(1602, 78);
            bottomPanel.BackColor = Color.White;
            btnBack.Text = "Back to List";
            btnBack.Location = new Point(1330, 20);
            btnBack.Size = new Size(160, 36);
            btnBack.FillColor = Color.FromArgb(100, 88, 255);
            btnBack.ForeColor = Color.White;
            btnBack.BorderRadius = 8;
            btnBack.Click += btnBack_Click;
            bottomPanel.Controls.Add(btnBack);
            // CONTENT
            contentPanel.Controls.AddRange(new Control[] {
lblHeader, lblCreated, infoPanel, btnAttempts, btnDetails, pnlAttempts, pnlDetails
});
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainPanel);
            this.Name = "ViewDelivery";
            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            pnlAttempts.ResumeLayout(false);
            pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dgvAttempts)).EndInit();
            this.ResumeLayout(false);
        }
        private Guna2Panel infoPanel, pnlStatusBadge;
        private Guna2Button btnAttempts, btnDetails, btnAddAttempt;
        private Guna2ShadowPanel pnlAttempts, pnlDetails;
        private DataGridView dgvAttempts;
        private Label lblCreated, lblStatusValue;
    }
}