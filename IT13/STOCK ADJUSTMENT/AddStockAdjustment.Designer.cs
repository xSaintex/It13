using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class AddStockAdjustment
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

            lblItem = new Label(); comboItem = new Guna2ComboBox();
            lblRequested = new Label(); comboRequestedBy = new Guna2ComboBox();
            lblReviewed = new Label(); comboReviewedBy = new Guna2ComboBox();
            lblAdjType = new Label(); comboAdjType = new Guna2ComboBox();
            lblPhysical = new Label(); txtPhysicalCount = new Guna2TextBox();
            lblSystem = new Label(); txtSystemCount = new Guna2TextBox();
            lblAdjCount = new Label(); txtAdjCount = new Guna2TextBox();
            lblStatus = new Label(); comboStatus = new Guna2ComboBox();
            lblReason = new Label(); txtReason = new Guna2TextBox();
            lblDate = new Label(); datePicker = new Guna2DateTimePicker();
            btnCancel = new Guna2Button();
            btnSubmit = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(lblHeader);
            mainpanel.Controls.Add(lblRequired);
            mainpanel.Controls.Add(lblItem); mainpanel.Controls.Add(comboItem);
            mainpanel.Controls.Add(lblRequested); mainpanel.Controls.Add(comboRequestedBy);
            mainpanel.Controls.Add(lblReviewed); mainpanel.Controls.Add(comboReviewedBy);
            mainpanel.Controls.Add(lblAdjType); mainpanel.Controls.Add(comboAdjType);
            mainpanel.Controls.Add(lblPhysical); mainpanel.Controls.Add(txtPhysicalCount);
            mainpanel.Controls.Add(lblSystem); mainpanel.Controls.Add(txtSystemCount);
            mainpanel.Controls.Add(lblAdjCount); mainpanel.Controls.Add(txtAdjCount);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(comboStatus);
            mainpanel.Controls.Add(lblReason); mainpanel.Controls.Add(txtReason);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnSubmit);

            // === HEADER & REQUIRED TEXT ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "Stock Adjustment Information";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Segoe UI", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === INVENTORY ITEM ===
            lblItem.AutoSize = true;
            lblItem.Location = new Point(77, 110);
            lblItem.Text = "Inventory Item *";
            lblItem.Font = new Font("Segoe UI", 10F);
            lblItem.ForeColor = Color.FromArgb(70, 70, 70);

            comboItem.Location = new Point(77, 140);
            comboItem.Size = new Size(600, 45);
            comboItem.DropDownStyle = ComboBoxStyle.DropDownList;
            comboItem.BorderRadius = 8;
            comboItem.BorderColor = Color.FromArgb(200, 200, 200);
            comboItem.BorderThickness = 1;
            comboItem.FillColor = Color.White;
            comboItem.Font = new Font("Segoe UI", 11F);

            // === DATE ===
            lblDate.AutoSize = true;
            lblDate.Location = new Point(750, 110);
            lblDate.Text = "Date *";
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.ForeColor = Color.FromArgb(70, 70, 70);

            datePicker.Location = new Point(750, 140);
            datePicker.Size = new Size(600, 45);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;

            // === REASON ===
            lblReason.AutoSize = true;
            lblReason.Location = new Point(750, 210);
            lblReason.Text = "Reason *";
            lblReason.Font = new Font("Segoe UI", 10F);
            lblReason.ForeColor = Color.FromArgb(70, 70, 70);

            txtReason.Location = new Point(750, 240);
            txtReason.Size = new Size(600, 120);
            txtReason.Multiline = true;
            txtReason.BorderRadius = 8;
            txtReason.BorderColor = Color.FromArgb(200, 200, 200);
            txtReason.BorderThickness = 1;
            txtReason.FillColor = Color.White;
            txtReason.Font = new Font("Segoe UI", 11F);

            // === REQUESTED BY ===
            lblRequested.AutoSize = true;
            lblRequested.Location = new Point(77, 210);
            lblRequested.Text = "Requested By *";
            lblRequested.Font = new Font("Segoe UI", 10F);
            lblRequested.ForeColor = Color.FromArgb(70, 70, 70);

            comboRequestedBy.Location = new Point(77, 240);
            comboRequestedBy.Size = new Size(600, 45);
            comboRequestedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRequestedBy.BorderRadius = 8;
            comboRequestedBy.BorderColor = Color.FromArgb(200, 200, 200);
            comboRequestedBy.BorderThickness = 1;
            comboRequestedBy.FillColor = Color.White;
            comboRequestedBy.Font = new Font("Segoe UI", 11F);

            // === REVIEWED BY ===
            lblReviewed.AutoSize = true;
            lblReviewed.Location = new Point(77, 310);
            lblReviewed.Text = "Reviewed By *";
            lblReviewed.Font = new Font("Segoe UI", 10F);
            lblReviewed.ForeColor = Color.FromArgb(70, 70, 70);

            comboReviewedBy.Location = new Point(77, 340);
            comboReviewedBy.Size = new Size(600, 45);
            comboReviewedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            comboReviewedBy.BorderRadius = 8;
            comboReviewedBy.BorderColor = Color.FromArgb(200, 200, 200);
            comboReviewedBy.BorderThickness = 1;
            comboReviewedBy.FillColor = Color.White;
            comboReviewedBy.Font = new Font("Segoe UI", 11F);

            // === ADJUSTMENT TYPE ===
            lblAdjType.AutoSize = true;
            lblAdjType.Location = new Point(77, 410);
            lblAdjType.Text = "Adjustment Type *";
            lblAdjType.Font = new Font("Segoe UI", 10F);
            lblAdjType.ForeColor = Color.FromArgb(70, 70, 70);

            comboAdjType.Location = new Point(77, 440);
            comboAdjType.Size = new Size(600, 45);
            comboAdjType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAdjType.BorderRadius = 8;
            comboAdjType.BorderColor = Color.FromArgb(200, 200, 200);
            comboAdjType.BorderThickness = 1;
            comboAdjType.FillColor = Color.White;
            comboAdjType.Font = new Font("Segoe UI", 11F);

            // === PHYSICAL COUNT ===
            lblPhysical.AutoSize = true;
            lblPhysical.Location = new Point(77, 510);
            lblPhysical.Text = "Physical Count *";
            lblPhysical.Font = new Font("Segoe UI", 10F);
            lblPhysical.ForeColor = Color.FromArgb(70, 70, 70);

            txtPhysicalCount.Location = new Point(77, 540);
            txtPhysicalCount.Size = new Size(600, 45);
            txtPhysicalCount.Text = "0";
            txtPhysicalCount.TextAlign = HorizontalAlignment.Right;
            txtPhysicalCount.KeyPress += txtPhysicalCount_KeyPress;
            txtPhysicalCount.BorderRadius = 8;
            txtPhysicalCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtPhysicalCount.BorderThickness = 1;
            txtPhysicalCount.FillColor = Color.White;
            txtPhysicalCount.Font = new Font("Segoe UI", 11F);

            // === SYSTEM COUNT ===
            lblSystem.AutoSize = true;
            lblSystem.Location = new Point(750, 410);
            lblSystem.Text = "System Count *";
            lblSystem.Font = new Font("Segoe UI", 10F);
            lblSystem.ForeColor = Color.FromArgb(70, 70, 70);

            txtSystemCount.Location = new Point(750, 440);
            txtSystemCount.Size = new Size(600, 45);
            txtSystemCount.Text = "0";
            txtSystemCount.TextAlign = HorizontalAlignment.Right;
            txtSystemCount.KeyPress += txtSystemCount_KeyPress;
            txtSystemCount.BorderRadius = 8;
            txtSystemCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtSystemCount.BorderThickness = 1;
            txtSystemCount.FillColor = Color.White;
            txtSystemCount.Font = new Font("Segoe UI", 11F);

            // === ADJUSTMENT COUNT ===
            lblAdjCount.AutoSize = true;
            lblAdjCount.Location = new Point(77, 610);
            lblAdjCount.Text = "Adjustment Count *";
            lblAdjCount.Font = new Font("Segoe UI", 10F);
            lblAdjCount.ForeColor = Color.FromArgb(70, 70, 70);

            txtAdjCount.Location = new Point(77, 640);
            txtAdjCount.Size = new Size(600, 45);
            txtAdjCount.Text = "0";
            txtAdjCount.TextAlign = HorizontalAlignment.Right;
            txtAdjCount.KeyPress += txtAdjCount_KeyPress;
            txtAdjCount.BorderRadius = 8;
            txtAdjCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtAdjCount.BorderThickness = 1;
            txtAdjCount.FillColor = Color.White;
            txtAdjCount.Font = new Font("Segoe UI", 11F);

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(750, 510);
            lblStatus.Text = "Status *";
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(70, 70, 70);

            comboStatus.Location = new Point(750, 540);
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

            // === SUBMIT BUTTON ===
            btnSubmit.Location = new Point(1300, 700);
            btnSubmit.Size = new Size(200, 50);
            btnSubmit.Text = "Submit Stock Adjustment";
            btnSubmit.FillColor = Color.FromArgb(0, 123, 255);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.BorderRadius = 8;
            btnSubmit.Click += btnSubmit_Click;

            // === FORM ===
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "AddStockAdjustment";
            Text = "Add Stock Adjustment";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblRequired;
        private Label lblItem; private Guna2ComboBox comboItem;
        private Label lblRequested; private Guna2ComboBox comboRequestedBy;
        private Label lblReviewed; private Guna2ComboBox comboReviewedBy;
        private Label lblAdjType; private Guna2ComboBox comboAdjType;
        private Label lblPhysical; private Guna2TextBox txtPhysicalCount;
        private Label lblSystem; private Guna2TextBox txtSystemCount;
        private Label lblAdjCount; private Guna2TextBox txtAdjCount;
        private Label lblStatus; private Guna2ComboBox comboStatus;
        private Label lblReason; private Guna2TextBox txtReason;
        private Label lblDate; private Guna2DateTimePicker datePicker;
        private Guna2Button btnCancel;
        private Guna2Button btnSubmit;
    }
}