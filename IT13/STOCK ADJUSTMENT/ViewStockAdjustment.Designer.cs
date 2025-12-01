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
            mainpanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            lblHeader = new System.Windows.Forms.Label();

            lblId = new System.Windows.Forms.Label(); txtId = new Guna.UI2.WinForms.Guna2TextBox();
            lblItem = new System.Windows.Forms.Label(); txtItem = new Guna.UI2.WinForms.Guna2TextBox();
            lblRequested = new System.Windows.Forms.Label(); txtRequested = new Guna.UI2.WinForms.Guna2TextBox();
            lblReviewed = new System.Windows.Forms.Label(); txtReviewed = new Guna.UI2.WinForms.Guna2TextBox();
            lblAdjType = new System.Windows.Forms.Label(); txtAdjType = new Guna.UI2.WinForms.Guna2TextBox();
            lblPhysical = new System.Windows.Forms.Label(); txtPhysical = new Guna.UI2.WinForms.Guna2TextBox();
            lblSystem = new System.Windows.Forms.Label(); txtSystem = new Guna.UI2.WinForms.Guna2TextBox();
            lblAdjCount = new System.Windows.Forms.Label(); txtAdjCount = new Guna.UI2.WinForms.Guna2TextBox();
            lblStatus = new System.Windows.Forms.Label(); txtStatus = new Guna.UI2.WinForms.Guna2TextBox();
            lblReason = new System.Windows.Forms.Label(); txtReason = new Guna.UI2.WinForms.Guna2TextBox();
            lblDate = new System.Windows.Forms.Label(); datePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();

            btnBack = new Guna.UI2.WinForms.Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

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

            // === HEADER (kept Tahoma 18 Bold as requested) ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 30);
            lblHeader.Text = "View Stock Adjustment";
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            // === ADJUSTMENT ID ===
            lblId.AutoSize = true;
            lblId.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblId.ForeColor = Color.FromArgb(70, 70, 70);
            lblId.Location = new Point(77, 110);
            lblId.Text = "Adjustment ID";

            txtId.Location = new Point(77, 140);
            txtId.Size = new Size(200, 45);
            txtId.ReadOnly = true;
            txtId.BorderRadius = 8;
            txtId.BorderColor = Color.FromArgb(200, 200, 200);
            txtId.BorderThickness = 1;
            txtId.FillColor = Color.White;
            txtId.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtId.ForeColor = Color.Black;

            // === INVENTORY ITEM ===
            lblItem.AutoSize = true;
            lblItem.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblItem.ForeColor = Color.FromArgb(70, 70, 70);
            lblItem.Location = new Point(300, 110);
            lblItem.Text = "Inventory Item";

            txtItem.Location = new Point(300, 140);
            txtItem.Size = new Size(378, 45);
            txtItem.ReadOnly = true;
            txtItem.BorderRadius = 8;
            txtItem.BorderColor = Color.FromArgb(200, 200, 200);
            txtItem.BorderThickness = 1;
            txtItem.FillColor = Color.White;
            txtItem.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtItem.ForeColor = Color.Black;

            // === DATE ===
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblDate.ForeColor = Color.FromArgb(70, 70, 70);
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
            datePicker.Font = new Font("Bahnschrift SemiCondensed", 11F);
            datePicker.ForeColor = Color.Black;

            // === REASON ===
            lblReason.AutoSize = true;
            lblReason.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblReason.ForeColor = Color.FromArgb(70, 70, 70);
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
            txtReason.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtReason.ForeColor = Color.Black;

            // === REQUESTED BY ===
            lblRequested.AutoSize = true;
            lblRequested.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblRequested.ForeColor = Color.FromArgb(70, 70, 70);
            lblRequested.Location = new Point(77, 210);
            lblRequested.Text = "Requested By";

            txtRequested.Location = new Point(77, 240);
            txtRequested.Size = new Size(600, 45);
            txtRequested.ReadOnly = true;
            txtRequested.BorderRadius = 8;
            txtRequested.BorderColor = Color.FromArgb(200, 200, 200);
            txtRequested.BorderThickness = 1;
            txtRequested.FillColor = Color.White;
            txtRequested.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtRequested.ForeColor = Color.Black;

            // === REVIEWED BY ===
            lblReviewed.AutoSize = true;
            lblReviewed.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblReviewed.ForeColor = Color.FromArgb(70, 70, 70);
            lblReviewed.Location = new Point(77, 310);
            lblReviewed.Text = "Reviewed By";

            txtReviewed.Location = new Point(77, 340);
            txtReviewed.Size = new Size(600, 45);
            txtReviewed.ReadOnly = true;
            txtReviewed.BorderRadius = 8;
            txtReviewed.BorderColor = Color.FromArgb(200, 200, 200);
            txtReviewed.BorderThickness = 1;
            txtReviewed.FillColor = Color.White;
            txtReviewed.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtReviewed.ForeColor = Color.Black;

            // === ADJUSTMENT TYPE ===
            lblAdjType.AutoSize = true;
            lblAdjType.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblAdjType.ForeColor = Color.FromArgb(70, 70, 70);
            lblAdjType.Location = new Point(77, 410);
            lblAdjType.Text = "Adjustment Type";

            txtAdjType.Location = new Point(77, 440);
            txtAdjType.Size = new Size(600, 45);
            txtAdjType.ReadOnly = true;
            txtAdjType.BorderRadius = 8;
            txtAdjType.BorderColor = Color.FromArgb(200, 200, 200);
            txtAdjType.BorderThickness = 1;
            txtAdjType.FillColor = Color.White;
            txtAdjType.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtAdjType.ForeColor = Color.Black;

            // === PHYSICAL COUNT ===
            lblPhysical.AutoSize = true;
            lblPhysical.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblPhysical.ForeColor = Color.FromArgb(70, 70, 70);
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
            txtPhysical.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtPhysical.ForeColor = Color.Black;

            // === SYSTEM COUNT ===
            lblSystem.AutoSize = true;
            lblSystem.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblSystem.ForeColor = Color.FromArgb(70, 70, 70);
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
            txtSystem.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtSystem.ForeColor = Color.Black;

            // === ADJUSTMENT COUNT ===
            lblAdjCount.AutoSize = true;
            lblAdjCount.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblAdjCount.ForeColor = Color.FromArgb(70, 70, 70);
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
            txtAdjCount.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtAdjCount.ForeColor = Color.Black;

            // === STATUS ===
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblStatus.ForeColor = Color.FromArgb(70, 70, 70);
            lblStatus.Location = new Point(750, 510);
            lblStatus.Text = "Status";

            txtStatus.Location = new Point(750, 540);
            txtStatus.Size = new Size(600, 45);
            txtStatus.ReadOnly = true;
            txtStatus.BorderRadius = 8;
            txtStatus.BorderColor = Color.FromArgb(200, 200, 200);
            txtStatus.BorderThickness = 1;
            txtStatus.FillColor = Color.White;
            txtStatus.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtStatus.ForeColor = Color.Black;

            // === BACK BUTTON (Poppins Bold) ===
            btnBack.Location = new Point(1300, 700);
            btnBack.Size = new Size(200, 50);
            btnBack.Text = "Back to List";
            btnBack.FillColor = Color.FromArgb(0, 123, 255);
            btnBack.ForeColor = Color.White;
            btnBack.BorderRadius = 8;
            btnBack.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnBack.Click += btnBack_Click;

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "ViewStockAdjustment";
            this.Text = "View Stock Adjustment";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2ShadowPanel mainpanel;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.Label lblId; private Guna.UI2.WinForms.Guna2TextBox txtId;
        private System.Windows.Forms.Label lblItem; private Guna.UI2.WinForms.Guna2TextBox txtItem;
        private System.Windows.Forms.Label lblRequested; private Guna.UI2.WinForms.Guna2TextBox txtRequested;
        private System.Windows.Forms.Label lblReviewed; private Guna.UI2.WinForms.Guna2TextBox txtReviewed;
        private System.Windows.Forms.Label lblAdjType; private Guna.UI2.WinForms.Guna2TextBox txtAdjType;
        private System.Windows.Forms.Label lblPhysical; private Guna.UI2.WinForms.Guna2TextBox txtPhysical;
        private System.Windows.Forms.Label lblSystem; private Guna.UI2.WinForms.Guna2TextBox txtSystem;
        private System.Windows.Forms.Label lblAdjCount; private Guna.UI2.WinForms.Guna2TextBox txtAdjCount;
        private System.Windows.Forms.Label lblStatus; private Guna.UI2.WinForms.Guna2TextBox txtStatus;
        private System.Windows.Forms.Label lblReason; private Guna.UI2.WinForms.Guna2TextBox txtReason;
        private System.Windows.Forms.Label lblDate; private Guna.UI2.WinForms.Guna2DateTimePicker datePicker;
        private Guna.UI2.WinForms.Guna2Button btnBack;
    }
}