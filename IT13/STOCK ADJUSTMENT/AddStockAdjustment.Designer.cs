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
            mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            lblHeader = new System.Windows.Forms.Label();
            lblRequired = new System.Windows.Forms.Label();
            lblItem = new System.Windows.Forms.Label(); comboItem = new Guna.UI2.WinForms.Guna2ComboBox();
            lblRequested = new System.Windows.Forms.Label(); comboRequestedBy = new Guna.UI2.WinForms.Guna2ComboBox();
            lblReviewed = new System.Windows.Forms.Label(); comboReviewedBy = new Guna.UI2.WinForms.Guna2ComboBox();
            lblAdjType = new System.Windows.Forms.Label(); comboAdjType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblPhysical = new System.Windows.Forms.Label(); txtPhysicalCount = new Guna.UI2.WinForms.Guna2TextBox();
            lblSystem = new System.Windows.Forms.Label(); txtSystemCount = new Guna.UI2.WinForms.Guna2TextBox();
            lblAdjCount = new System.Windows.Forms.Label(); txtAdjCount = new Guna.UI2.WinForms.Guna2TextBox();
            lblStatus = new System.Windows.Forms.Label(); comboStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            lblReason = new System.Windows.Forms.Label(); txtReason = new Guna.UI2.WinForms.Guna2TextBox();
            lblDate = new System.Windows.Forms.Label(); datePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnSubmit = new Guna.UI2.WinForms.Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

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
            mainpanel.Controls.Add(btnCancel);
            mainpanel.Controls.Add(btnSubmit);

            // === HEADER (kept as Tahoma 18 Bold - per your request) ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.Text = "Stock Adjustment Information";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            // === REQUIRED TEXT ===
            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Tahoma", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === INVENTORY ITEM ===
            lblItem.AutoSize = true;
            lblItem.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblItem.ForeColor = Color.FromArgb(70, 70, 70);
            lblItem.Location = new Point(77, 110);
            lblItem.Text = "Inventory Item *";

            comboItem.Location = new Point(77, 140);
            comboItem.Size = new Size(600, 45);
            comboItem.DropDownStyle = ComboBoxStyle.DropDownList;
            comboItem.BorderRadius = 8;
            comboItem.BorderColor = Color.FromArgb(200, 200, 200);
            comboItem.BorderThickness = 1;
            comboItem.FillColor = Color.White;
            comboItem.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboItem.ForeColor = Color.Black; // typed text black

            // === DATE ===
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblDate.ForeColor = Color.FromArgb(70, 70, 70);
            lblDate.Location = new Point(750, 110);
            lblDate.Text = "Date *";

            datePicker.Location = new Point(750, 140);
            datePicker.Size = new Size(600, 45);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;
            datePicker.Font = new Font("Bahnschrift SemiCondensed", 11F);
            datePicker.ForeColor = Color.Black;

            // === REASON ===
            lblReason.AutoSize = true;
            lblReason.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblReason.ForeColor = Color.FromArgb(70, 70, 70);
            lblReason.Location = new Point(750, 210);
            lblReason.Text = "Reason *";

            txtReason.Location = new Point(750, 240);
            txtReason.Size = new Size(600, 120);
            txtReason.Multiline = true;
            txtReason.BorderRadius = 8;
            txtReason.BorderColor = Color.FromArgb(200, 200, 200);
            txtReason.BorderThickness = 1;
            txtReason.FillColor = Color.White;
            txtReason.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtReason.ForeColor = Color.Black;

            // === REQUESTED BY ===
            lblRequested.AutoSize = true;
            lblRequested.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblRequested.ForeColor = Color.FromArgb(70, 70, 70);
            lblRequested.Location = new Point(77, 210);
            lblRequested.Text = "Requested By *";

            comboRequestedBy.Location = new Point(77, 240);
            comboRequestedBy.Size = new Size(600, 45);
            comboRequestedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRequestedBy.BorderRadius = 8;
            comboRequestedBy.BorderColor = Color.FromArgb(200, 200, 200);
            comboRequestedBy.BorderThickness = 1;
            comboRequestedBy.FillColor = Color.White;
            comboRequestedBy.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboRequestedBy.ForeColor = Color.Black;

            // === REVIEWED BY ===
            lblReviewed.AutoSize = true;
            lblReviewed.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblReviewed.ForeColor = Color.FromArgb(70, 70, 70);
            lblReviewed.Location = new Point(77, 310);
            lblReviewed.Text = "Reviewed By *";

            comboReviewedBy.Location = new Point(77, 340);
            comboReviewedBy.Size = new Size(600, 45);
            comboReviewedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            comboReviewedBy.BorderRadius = 8;
            comboReviewedBy.BorderColor = Color.FromArgb(200, 200, 200);
            comboReviewedBy.BorderThickness = 1;
            comboReviewedBy.FillColor = Color.White;
            comboReviewedBy.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboReviewedBy.ForeColor = Color.Black;

            // === ADJUSTMENT TYPE ===
            lblAdjType.AutoSize = true;
            lblAdjType.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblAdjType.ForeColor = Color.FromArgb(70, 70, 70);
            lblAdjType.Location = new Point(77, 410);
            lblAdjType.Text = "Adjustment Type *";

            comboAdjType.Location = new Point(77, 440);
            comboAdjType.Size = new Size(600, 45);
            comboAdjType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAdjType.BorderRadius = 8;
            comboAdjType.BorderColor = Color.FromArgb(200, 200, 200);
            comboAdjType.BorderThickness = 1;
            comboAdjType.FillColor = Color.White;
            comboAdjType.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboAdjType.ForeColor = Color.Black;

            // === PHYSICAL COUNT ===
            lblPhysical.AutoSize = true;
            lblPhysical.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblPhysical.ForeColor = Color.FromArgb(70, 70, 70);
            lblPhysical.Location = new Point(77, 510);
            lblPhysical.Text = "Physical Count *";

            txtPhysicalCount.Location = new Point(77, 540);
            txtPhysicalCount.Size = new Size(600, 45);
            txtPhysicalCount.Text = "0";
            txtPhysicalCount.TextAlign = HorizontalAlignment.Right;
            txtPhysicalCount.KeyPress += txtPhysicalCount_KeyPress;
            txtPhysicalCount.BorderRadius = 8;
            txtPhysicalCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtPhysicalCount.BorderThickness = 1;
            txtPhysicalCount.FillColor = Color.White;
            txtPhysicalCount.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtPhysicalCount.ForeColor = Color.Black;

            // === SYSTEM COUNT ===
            lblSystem.AutoSize = true;
            lblSystem.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblSystem.ForeColor = Color.FromArgb(70, 70, 70);
            lblSystem.Location = new Point(750, 410);
            lblSystem.Text = "System Count *";

            txtSystemCount.Location = new Point(750, 440);
            txtSystemCount.Size = new Size(600, 45);
            txtSystemCount.Text = "0";
            txtSystemCount.TextAlign = HorizontalAlignment.Right;
            txtSystemCount.KeyPress += txtSystemCount_KeyPress;
            txtSystemCount.BorderRadius = 8;
            txtSystemCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtSystemCount.BorderThickness = 1;
            txtSystemCount.FillColor = Color.White;
            txtSystemCount.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtSystemCount.ForeColor = Color.Black;

            // === ADJUSTMENT COUNT ===
            lblAdjCount.AutoSize = true;
            lblAdjCount.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblAdjCount.ForeColor = Color.FromArgb(70, 70, 70);
            lblAdjCount.Location = new Point(77, 610);
            lblAdjCount.Text = "Adjustment Count *";

            txtAdjCount.Location = new Point(77, 640);
            txtAdjCount.Size = new Size(600, 45);
            txtAdjCount.Text = "0";
            txtAdjCount.TextAlign = HorizontalAlignment.Right;
            txtAdjCount.KeyPress += txtAdjCount_KeyPress;
            txtAdjCount.BorderRadius = 8;
            txtAdjCount.BorderColor = Color.FromArgb(200, 200, 200);
            txtAdjCount.BorderThickness = 1;
            txtAdjCount.FillColor = Color.White;
            txtAdjCount.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtAdjCount.ForeColor = Color.Black;

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblStatus.ForeColor = Color.FromArgb(70, 70, 70);
            lblStatus.Location = new Point(750, 510);
            lblStatus.Text = "Status *";

            comboStatus.Location = new Point(750, 540);
            comboStatus.Size = new Size(600, 45);
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStatus.BorderRadius = 8;
            comboStatus.BorderColor = Color.FromArgb(200, 200, 200);
            comboStatus.BorderThickness = 1;
            comboStatus.FillColor = Color.White;
            comboStatus.Font = new Font("Bahnschrift SemiCondensed", 11F);
            comboStatus.ForeColor = Color.Black;

            // === BUTTONS (Poppins font) ===
            btnCancel.Location = new Point(1150, 700);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.Click += btnCancel_Click;

            btnSubmit.Location = new Point(1300, 700);
            btnSubmit.Size = new Size(140, 50);
            btnSubmit.Text = "Submit";
            btnSubmit.FillColor = Color.FromArgb(0, 123, 255);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.BorderRadius = 8;
            btnSubmit.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnSubmit.Click += btnSubmit_Click;

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "AddStockAdjustment";
            this.Text = "Add Stock Adjustment";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2ShadowPanel mainpanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Label lblItem; private Guna.UI2.WinForms.Guna2ComboBox comboItem;
        private System.Windows.Forms.Label lblRequested; private Guna.UI2.WinForms.Guna2ComboBox comboRequestedBy;
        private System.Windows.Forms.Label lblReviewed; private Guna.UI2.WinForms.Guna2ComboBox comboReviewedBy;
        private System.Windows.Forms.Label lblAdjType; private Guna.UI2.WinForms.Guna2ComboBox comboAdjType;
        private System.Windows.Forms.Label lblPhysical; private Guna.UI2.WinForms.Guna2TextBox txtPhysicalCount;
        private System.Windows.Forms.Label lblSystem; private Guna.UI2.WinForms.Guna2TextBox txtSystemCount;
        private System.Windows.Forms.Label lblAdjCount; private Guna.UI2.WinForms.Guna2TextBox txtAdjCount;
        private System.Windows.Forms.Label lblStatus; private Guna.UI2.WinForms.Guna2ComboBox comboStatus;
        private System.Windows.Forms.Label lblReason; private Guna.UI2.WinForms.Guna2TextBox txtReason;
        private System.Windows.Forms.Label lblDate; private Guna.UI2.WinForms.Guna2DateTimePicker datePicker;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
    }
}