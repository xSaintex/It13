using Guna.UI2.WinForms;
namespace IT13
{
    partial class EditStockAdjustment
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            mainpanel = new Guna2ShadowPanel();
            lblTitle = new Label();
            lblId = new Label(); txtId = new Guna2TextBox();
            lblItem = new Label(); comboItem = new Guna2ComboBox();
            lblRequested = new Label(); comboRequestedBy = new Guna2ComboBox();
            lblReviewed = new Label(); comboReviewedBy = new Guna2ComboBox();   // NEW
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

            // MAIN PANEL
            mainpanel.BackColor = Color.Transparent;
            mainpanel.Controls.Add(lblTitle);
            mainpanel.Controls.Add(lblId); mainpanel.Controls.Add(txtId);
            mainpanel.Controls.Add(lblItem); mainpanel.Controls.Add(comboItem);
            mainpanel.Controls.Add(lblRequested); mainpanel.Controls.Add(comboRequestedBy);
            mainpanel.Controls.Add(lblReviewed); mainpanel.Controls.Add(comboReviewedBy); // NEW
            mainpanel.Controls.Add(lblAdjType); mainpanel.Controls.Add(comboAdjType);
            mainpanel.Controls.Add(lblPhysical); mainpanel.Controls.Add(txtPhysicalCount);
            mainpanel.Controls.Add(lblSystem); mainpanel.Controls.Add(txtSystemCount);
            mainpanel.Controls.Add(lblAdjCount); mainpanel.Controls.Add(txtAdjCount);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(comboStatus);
            mainpanel.Controls.Add(lblReason); mainpanel.Controls.Add(txtReason);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnSubmit);
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 8;
            mainpanel.ShadowColor = Color.Black;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.TabIndex = 0;

            // TITLE
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(77, 40);
            lblTitle.Text = "Edit Stock Adjustment";

            // ADJUSTMENT ID
            lblId.AutoSize = true; lblId.Location = new Point(77, 110); lblId.Text = "Adjustment ID";
            txtId.Location = new Point(77, 140); txtId.Size = new Size(200, 35);
            txtId.ReadOnly = true;

            // INVENTORY ITEM
            lblItem.AutoSize = true; lblItem.Location = new Point(300, 110); lblItem.Text = "Inventory Item *";
            comboItem.Location = new Point(300, 140); comboItem.Size = new Size(378, 45);
            comboItem.DropDownStyle = ComboBoxStyle.DropDownList;

            // DATE
            lblDate.AutoSize = true; lblDate.Location = new Point(750, 110); lblDate.Text = "Date *";
            datePicker.Location = new Point(750, 140); datePicker.Size = new Size(600, 45);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;

            // REASON
            lblReason.AutoSize = true; lblReason.Location = new Point(750, 210); lblReason.Text = "Reason *";
            txtReason.Location = new Point(750, 240); txtReason.Size = new Size(600, 120);
            txtReason.Multiline = true;

            // REQUESTED BY
            lblRequested.AutoSize = true; lblRequested.Location = new Point(77, 210); lblRequested.Text = "Requested By *";
            comboRequestedBy.Location = new Point(77, 240); comboRequestedBy.Size = new Size(600, 45);
            comboRequestedBy.DropDownStyle = ComboBoxStyle.DropDownList;

            // REVIEWED BY (NEW)
            lblReviewed.AutoSize = true; lblReviewed.Location = new Point(77, 310); lblReviewed.Text = "Reviewed By *";
            comboReviewedBy.Location = new Point(77, 340); comboReviewedBy.Size = new Size(600, 45);
            comboReviewedBy.DropDownStyle = ComboBoxStyle.DropDownList;

            // ADJUSTMENT TYPE (moved down)
            lblAdjType.AutoSize = true; lblAdjType.Location = new Point(77, 410); lblAdjType.Text = "Adjustment Type *";
            comboAdjType.Location = new Point(77, 440); comboAdjType.Size = new Size(600, 45);
            comboAdjType.DropDownStyle = ComboBoxStyle.DropDownList;

            // PHYSICAL COUNT
            lblPhysical.AutoSize = true; lblPhysical.Location = new Point(77, 510); lblPhysical.Text = "Physical Count *";
            txtPhysicalCount.Location = new Point(77, 540); txtPhysicalCount.Size = new Size(600, 45);
            txtPhysicalCount.TextAlign = HorizontalAlignment.Right;
            txtPhysicalCount.KeyPress += txtPhysicalCount_KeyPress;

            // SYSTEM COUNT
            lblSystem.AutoSize = true; lblSystem.Location = new Point(750, 410); lblSystem.Text = "System Count *";
            txtSystemCount.Location = new Point(750, 440); txtSystemCount.Size = new Size(600, 45);
            txtSystemCount.TextAlign = HorizontalAlignment.Right;
            txtSystemCount.KeyPress += txtSystemCount_KeyPress;

            // ADJUSTMENT COUNT
            lblAdjCount.AutoSize = true; lblAdjCount.Location = new Point(77, 610); lblAdjCount.Text = "Adjustment Count *";
            txtAdjCount.Location = new Point(77, 640); txtAdjCount.Size = new Size(600, 45);
            txtAdjCount.TextAlign = HorizontalAlignment.Right;
            txtAdjCount.KeyPress += txtAdjCount_KeyPress;

            // STATUS
            lblStatus.AutoSize = true; lblStatus.Location = new Point(750, 510); lblStatus.Text = "Status *";
            comboStatus.Location = new Point(750, 540); comboStatus.Size = new Size(600, 45);
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // CANCEL / SUBMIT (unchanged)
            btnCancel.Location = new Point(1150, 700); btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel"; btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White; btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            btnSubmit.Location = new Point(1300, 700); btnSubmit.Size = new Size(200, 50);
            btnSubmit.Text = "Submit Stock Adjustment"; btnSubmit.FillColor = Color.FromArgb(0, 123, 255);
            btnSubmit.ForeColor = Color.White; btnSubmit.BorderRadius = 8;
            btnSubmit.Click += btnSubmit_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "EditStockAdjustment";
            Text = "Edit Stock Adjustment";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblTitle;
        private Label lblId; private Guna2TextBox txtId;
        private Label lblItem; private Guna2ComboBox comboItem;
        private Label lblRequested; private Guna2ComboBox comboRequestedBy;
        private Label lblReviewed; private Guna2ComboBox comboReviewedBy;   // NEW
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