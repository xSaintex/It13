using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class ViewStockAdjustment
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
            lblId = new Label(); txtId = new Guna2TextBox();
            lblItem = new Label(); txtItem = new Guna2TextBox();
            lblRequested = new Label(); txtRequested = new Guna2TextBox();
            lblReviewed = new Label(); txtReviewed = new Guna2TextBox();
            lblAdjType = new Label(); txtAdjType = new Guna2TextBox();
            lblPhysical = new Label(); txtPhysical = new Guna2TextBox();
            lblSystem = new Label(); txtSystem = new Guna2TextBox();
            lblAdjCount = new Label(); txtAdjCount = new Guna2TextBox();
            lblStatus = new Label(); txtStatus = new Guna2TextBox();
            lblReason = new Label(); txtReason = new Guna2TextBox();
            lblDate = new Label(); datePicker = new Guna2DateTimePicker();
            btnBack = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(lblHeader);
            mainpanel.Controls.Add(lblId); mainpanel.Controls.Add(txtId);
            mainpanel.Controls.Add(lblItem); mainpanel.Controls.Add(txtItem);
            mainpanel.Controls.Add(lblRequested); mainpanel.Controls.Add(txtRequested);
            mainpanel.Controls.Add(lblReviewed); mainpanel.Controls.Add(txtReviewed);
            mainpanel.Controls.Add(lblAdjType); mainpanel.Controls.Add(txtAdjType);
            mainpanel.Controls.Add(lblPhysical); mainpanel.Controls.Add(txtPhysical);
            mainpanel.Controls.Add(lblSystem); mainpanel.Controls.Add(txtSystem);
            mainpanel.Controls.Add(lblAdjCount); mainpanel.Controls.Add(txtAdjCount);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(txtStatus);
            mainpanel.Controls.Add(lblReason); mainpanel.Controls.Add(txtReason);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnBack);

            // === HEADER ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "View Stock Adjustment";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            // === ADJUSTMENT ID ===
            lblId.AutoSize = true;
            lblId.Location = new Point(77, 110);
            lblId.Text = "Adjustment ID";

            txtId.Location = new Point(77, 140);
            txtId.Size = new Size(200, 45);
            txtId.ReadOnly = true;
            txtId.BorderRadius = 8;
            txtId.BorderColor = Color.FromArgb(200, 200, 200);
            txtId.BorderThickness = 1;
            txtId.FillColor = Color.White;

            // === INVENTORY ITEM ===
            lblItem.AutoSize = true;
            lblItem.Location = new Point(300, 110);
            lblItem.Text = "Inventory Item";

            txtItem.Location = new Point(300, 140);
            txtItem.Size = new Size(378, 45);
            txtItem.ReadOnly = true;
            txtItem.BorderRadius = 8;
            txtItem.BorderColor = Color.FromArgb(200, 200, 200);
            txtItem.BorderThickness = 1;
            txtItem.FillColor = Color.White;
            txtItem.Font = new Font("Segoe UI", 11F);

            // === DATE ===
            lblDate.AutoSize = true;
            lblDate.Location = new Point(750, 110);
            lblDate.Text = "Date";

            datePicker.Location = new Point(750, 140);
            datePicker.Size = new Size(600, 45);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.Enabled = false;
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;

            // === REASON ===
            lblReason.AutoSize = true;
            lblReason.Location = new Point(750, 210);
            lblReason.Text = "Reason";

            txtReason.Location = new Point(750, 240);
            txtReason.Size = new Size(600, 120);
            txtReason.Multiline = true;
            txtReason.ReadOnly = true;
            txtReason.BorderRadius = 8;
            txtReason.BorderColor = Color.FromArgb(200, 200, 200);
            txtReason.BorderThickness = 1;
            txtReason.FillColor = Color.White;
            txtReason.Font = new Font("Segoe UI", 11F);

            // === REQUESTED BY ===
            lblRequested.AutoSize = true;
            lblRequested.Location = new Point(77, 210);
            lblRequested.Text = "Requested By";

            txtRequested.Location = new Point(77, 240);
            txtRequested.Size = new Size(600, 45);
            txtRequested.ReadOnly = true;
            txtRequested.BorderRadius = 8;
            txtRequested.BorderColor = Color.FromArgb(200, 200, 200);
            txtRequested.BorderThickness = 1;
            txtRequested.FillColor = Color.White;
            txtRequested.Font = new Font("Segoe UI", 11F);

            // === REVIEWED BY ===
            lblReviewed.AutoSize = true;
            lblReviewed.Location = new Point(77, 310);
            lblReviewed.Text = "Reviewed By";

            txtReviewed.Location = new Point(77, 340);
            txtReviewed.Size = new Size(600, 45);
            txtReviewed.ReadOnly = true;
            txtReviewed.BorderRadius = 8;
            txtReviewed.BorderColor = Color.FromArgb(200, 200, 200);
            txtReviewed.BorderThickness = 1;
            txtReviewed.FillColor = Color.White;
            txtReviewed.Font = new Font("Segoe UI", 11F);

            // === ADJUSTMENT TYPE ===
            lblAdjType.AutoSize = true;
            lblAdjType.Location = new Point(77, 410);
            lblAdjType.Text = "Adjustment Type";

            txtAdjType.Location = new Point(77, 440);
            txtAdjType.Size = new Size(600, 45);
            txtAdjType.ReadOnly = true;
            txtAdjType.BorderRadius = 8;
            txtAdjType.BorderColor = Color.FromArgb(200, 200, 200);
            txtAdjType.BorderThickness = 1;
            txtAdjType.FillColor = Color.White;
            txtAdjType.Font = new Font("Segoe UI", 11F);

            // === PHYSICAL COUNT ===
            lblPhysical.AutoSize = true;
            lblPhysical.Location = new Point(77, 510);
            lblPhysical.Text = "Physical Count";

            txtPhysical.Location = new Point(77, 540);
            txtPhysical.Size = new Size(600, 45);
            txtPhysical.ReadOnly = true;
            txtPhysical.TextAlign = HorizontalAlignment.Right;
            txtPhysical.BorderRadius = 8;
            txtPhysical.BorderColor = Color.FromArgb(200, 200, 200);
            txtPhysical.BorderThickness = 1;
            txtPhysical.FillColor = Color.White;
            txtPhysical.Font = new Font("Segoe UI", 11F);

            // === SYSTEM COUNT ===
            lblSystem.AutoSize = true;
            lblSystem.Location = new Point(750, 410);
            lblSystem.Text = "System Count";

            txtSystem.Location = new Point(750, 440);
            txtSystem.Size = new Size(600, 45);
            txtSystem.ReadOnly = true;
            txtSystem.TextAlign = HorizontalAlignment.Right;
            txtSystem.BorderRadius = 8;
            txtSystem.BorderColor = Color.FromArgb(200, 200, 200);
            txtSystem.BorderThickness = 1;
            txtSystem.FillColor = Color.White;
            txtSystem.Font = new Font("Segoe UI", 11F);

            // === ADJUSTMENT COUNT ===
            lblAdjCount.AutoSize = true;
            lblAdjCount.Location = new Point(77, 610);
            lblAdjCount.Text = "Adjustment Count";

            txtAdjCount.Location = new Point(77, 640);
            txtAdjCount.Size = new Size(600, 45);
            txtAdjCount.ReadOnly = true;
            txtAdjCount.TextAlign = HorizontalAlignment.Right;
            txtAdjCount.BorderRadius = 8;
            txtAdjCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtAdjCount.BorderThickness = 1;
            txtAdjCount.FillColor = Color.White;
            txtAdjCount.Font = new Font("Segoe UI", 11F);

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(750, 510);
            lblStatus.Text = "Status";

            txtStatus.Location = new Point(750, 540);
            txtStatus.Size = new Size(600, 45);
            txtStatus.ReadOnly = true;
            txtStatus.BorderRadius = 8;
            txtStatus.BorderColor = Color.FromArgb(200, 200, 200);
            txtStatus.BorderThickness = 1;
            txtStatus.FillColor = Color.White;
            txtStatus.Font = new Font("Segoe UI", 11F);

            // === BACK BUTTON ===
            btnBack.Location = new Point(1300, 700);
            btnBack.Size = new Size(200, 50);
            btnBack.Text = "Back to List";
            btnBack.FillColor = Color.FromArgb(0, 123, 255);
            btnBack.ForeColor = Color.White;
            btnBack.BorderRadius = 8;
            btnBack.Click += btnBack_Click;

            // === FORM ===
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "ViewStockAdjustment";
            Text = "View Stock Adjustment";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblId; private Guna2TextBox txtId;
        private Label lblItem; private Guna2TextBox txtItem;
        private Label lblRequested; private Guna2TextBox txtRequested;
        private Label lblReviewed; private Guna2TextBox txtReviewed;
        private Label lblAdjType; private Guna2TextBox txtAdjType;
        private Label lblPhysical; private Guna2TextBox txtPhysical;
        private Label lblSystem; private Guna2TextBox txtSystem;
        private Label lblAdjCount; private Guna2TextBox txtAdjCount;
        private Label lblStatus; private Guna2TextBox txtStatus;
        private Label lblReason; private Guna2TextBox txtReason;
        private Label lblDate; private Guna2DateTimePicker datePicker;
        private Guna2Button btnBack;
    }
}