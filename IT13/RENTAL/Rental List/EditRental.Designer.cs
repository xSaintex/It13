// EditRental.Designer.cs  ← 100% IDENTICAL TO AddRental except 4 small changes
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class EditRental
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2ShadowPanel mainPanel;
        private Guna2Panel scrollPanel;
        private Guna2Panel contentPanel;

        private Label lblHeader;
        private Label lblRequired;
        private LinkLabel lnkBack;

        private Guna2Button btnRentalInfo;
        private Guna2Button btnAddress;

        private Guna2ShadowPanel pnlRentalInfo;
        private Guna2ShadowPanel pnlAddress;

        private Label lblCustomerName; private Guna2TextBox txtCustomerName;
        private Label lblContactPerson; private Guna2TextBox txtContactPerson;
        private Label lblContactNumber; private Guna2TextBox txtContactNumber;
        private Label lblEmail; private Guna2TextBox txtEmail;
        private Label lblBookingType; private Guna2ComboBox cmbBookingType;
        private Label lblPaymentTerms; private Guna2ComboBox cmbPaymentTerms;
        private Label lblStatus; private Guna2ComboBox cmbStatus;
        private Label lblScheduledDate; private Guna2DateTimePicker dtpScheduledDate;
        private Label lblReturnDate; private Guna2DateTimePicker dtpReturnDate;

        private Guna2Panel pnlItems;
        private Label lblItemTable;
        private Guna2TextBox txtSearchProduct;
        private Guna2Button btnSearch;
        private Guna2Button btnAddProduct;
        private Guna2DataGridView dgvItems;

        private Label lblBillingTitle; private Guna2TextBox txtBillingAddress;
        private Label lblShippingTitle; private Guna2TextBox txtShippingAddress;

        private Label lblSubtotal; private Label lblSubtotalVal;
        private Label lblDiscount; private Guna2NumericUpDown numDiscount;
        private Label lblServiceFee; private Guna2NumericUpDown numServiceFee;
        private Label lblTotal; private Label lblTotalVal;

        private Label lblSubtotal_Addr; private Label lblSubtotalVal_Addr;
        private Label lblDiscount_Addr; private Guna2NumericUpDown numDiscount_Addr;
        private Label lblServiceFee_Addr; private Guna2NumericUpDown numServiceFee_Addr;
        private Label lblTotal_Addr; private Label lblTotalVal_Addr;

        private Guna2Button btnSaveRental;  // ← Only ONE save button

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainPanel = new Guna2ShadowPanel();
            scrollPanel = new Guna2Panel();
            contentPanel = new Guna2Panel();
            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            this.SuspendLayout();

            // ===================== MAIN PANEL =====================
            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 878);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 16;
            mainPanel.Controls.Add(scrollPanel);

            // ===================== SCROLL PANEL =====================
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1602, 700);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);

            // ===================== CONTENT PANEL =====================
            contentPanel.Location = new Point(0, 0);
            contentPanel.Size = new Size(1540, 1200);
            contentPanel.Padding = new Padding(30, 20, 30, 40);
            contentPanel.BackColor = Color.Transparent;

            // ===================== HEADER =====================
            lblHeader = new Label { Text = "Edit Rental", Font = new Font("Tahoma", 18F, FontStyle.Bold), Location = new Point(77, 20), AutoSize = true };
            lblRequired = new Label { Text = "Fields marked with an asterisk (*) are required.", ForeColor = Color.Red, Font = new Font("Tahoma", 9F), Location = new Point(80, 56), AutoSize = true };
            lnkBack = new LinkLabel { Text = "← Back to Rental List", LinkColor = Color.FromArgb(0, 123, 255), Font = new Font("Poppins", 10F), Location = new Point(1350, 68), AutoSize = true };

            // ===================== TAB BUTTONS =====================
            int tabY = 100;
            btnRentalInfo = new Guna2Button
            {
                Text = "🔑 Rental Information",
                Size = new Size(250, 36),
                Location = new Point(80, tabY),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 5
            };
            btnAddress = new Guna2Button
            {
                Text = "📍 Address",
                Size = new Size(170, 36),
                Location = new Point(341, tabY),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 5
            };

            int baseY = tabY + 70;

            // ===================== RENTAL INFO PANEL =====================
            pnlRentalInfo = new Guna2ShadowPanel
            {
                Location = new Point(77, baseY),
                Size = new Size(1386, 900),
                FillColor = Color.FromArgb(248, 249, 252),
                Radius = 20,
                Visible = true
            };

            int cy = 40;
            // Row 1
            lblCustomerName = new Label { Text = "Customer Name *", Location = new Point(20, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            txtCustomerName = new Guna2TextBox { Location = new Point(20, cy + 25), Size = new Size(420, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblBookingType = new Label { Text = "Booking Type *", Location = new Point(480, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            cmbBookingType = new Guna2ComboBox { Location = new Point(480, cy + 25), Size = new Size(300, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblPaymentTerms = new Label { Text = "Payment Terms *", Location = new Point(820, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            cmbPaymentTerms = new Guna2ComboBox { Location = new Point(820, cy + 25), Size = new Size(300, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            cy += 90;

            // Row 2
            lblContactPerson = new Label { Text = "Contact Person *", Location = new Point(20, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            txtContactPerson = new Guna2TextBox { Location = new Point(20, cy + 25), Size = new Size(420, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblContactNumber = new Label { Text = "Contact Number *", Location = new Point(480, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            txtContactNumber = new Guna2TextBox { Location = new Point(480, cy + 25), Size = new Size(300, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblEmail = new Label { Text = "Email *", Location = new Point(820, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            txtEmail = new Guna2TextBox { Location = new Point(820, cy + 25), Size = new Size(420, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            cy += 90;

            // Row 3
            lblScheduledDate = new Label { Text = "Scheduled Date *", Location = new Point(20, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            dtpScheduledDate = new Guna2DateTimePicker { Location = new Point(20, cy + 25), Size = new Size(420, 36), FillColor = Color.White, Format = DateTimePickerFormat.Custom, CustomFormat = "MMMM dd, yyyy", Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblReturnDate = new Label { Text = "Return Date *", Location = new Point(480, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            dtpReturnDate = new Guna2DateTimePicker { Location = new Point(480, cy + 25), Size = new Size(300, 36), FillColor = Color.White, Format = DateTimePickerFormat.Custom, CustomFormat = "MMMM dd, yyyy", Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblStatus = new Label { Text = "Status *", Location = new Point(820, cy), AutoSize = true, Font = new Font("Bahnschrift SemiCondensed", 10.2F), Parent = pnlRentalInfo };
            cmbStatus = new Guna2ComboBox { Location = new Point(820, cy + 25), Size = new Size(300, 36), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            cy += 110;

            // Items Panel
            pnlItems = new Guna2Panel { Location = new Point(20, cy), Size = new Size(1346, 380), BackColor = Color.WhiteSmoke, Parent = pnlRentalInfo };
            lblItemTable = new Label { Text = "Rental Items", Location = new Point(20, 15), Font = new Font("Segoe UI", 12F, FontStyle.Bold), AutoSize = true, Parent = pnlItems };
            txtSearchProduct = new Guna2TextBox { Location = new Point(20, 50), Size = new Size(300, 36), PlaceholderText = "Search product...", BorderRadius = 5, Font = new Font("Poppins", 10.5F), ForeColor = Color.Black, Parent = pnlItems };
            btnSearch = new Guna2Button { Text = "Search", Location = new Point(330, 50), Size = new Size(90, 36), FillColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, BorderRadius = 5, Parent = pnlItems };
            btnAddProduct = new Guna2Button { Text = "+ Add Product", Location = new Point(1160, 50), Size = new Size(160, 36), FillColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, BorderRadius = 5, Font = new Font("Poppins", 8F), Parent = pnlItems };
            dgvItems = new Guna2DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(1306, 260),
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                ColumnHeadersHeight = 40,
                Parent = pnlItems
            };
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            dgvItems.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "PRODUCT NAME", Width = 450 },
                new DataGridViewTextBoxColumn { HeaderText = "QUANTITY", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "RENTAL PRICE", Width = 180 },
                new DataGridViewTextBoxColumn { HeaderText = "AVAILABLE QTY", Width = 180 },
                new DataGridViewTextBoxColumn { HeaderText = "SUBTOTAL", Width = 180 }
            });
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            cy += 400;

            // Totals + Save Button (Rental Info Tab)
            lblSubtotal = new Label { Text = "SubTotal:", Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold), Location = new Point(900, cy), AutoSize = true, Parent = pnlRentalInfo };
            lblSubtotalVal = new Label { Text = "₱0.00", Font = new Font("Bahnschrift SemiCondensed", 10F), Location = new Point(1100, cy), AutoSize = true, Parent = pnlRentalInfo };
            lblDiscount = new Label { Text = "Discount (%):", Location = new Point(900, cy + 40), AutoSize = true, Parent = pnlRentalInfo };
            numDiscount = new Guna2NumericUpDown { Location = new Point(1100, cy + 40), Size = new Size(100, 30), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblServiceFee = new Label { Text = "Service Fee:", Location = new Point(900, cy + 80), AutoSize = true, Parent = pnlRentalInfo };
            numServiceFee = new Guna2NumericUpDown { Location = new Point(1100, cy + 80), Size = new Size(100, 30), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlRentalInfo };
            lblTotal = new Label { Text = "Total Amount:", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), Location = new Point(900, cy + 130), AutoSize = true, Parent = pnlRentalInfo };
            lblTotalVal = new Label { Text = "₱0.00", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 123, 255), Location = new Point(1100, cy + 130), AutoSize = true, Parent = pnlRentalInfo };

            btnSaveRental = new Guna2Button
            {
                Name = "btnSaveRental",
                Text = "Update Rental",
                Size = new Size(180, 48),
                Location = new Point(1386 - 180 - 40, cy + 200),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 10,
                Font = new Font("Poppins", 11F, FontStyle.Bold),
                Parent = pnlRentalInfo
            };

            // ===================== ADDRESS PANEL =====================
            pnlAddress = new Guna2ShadowPanel
            {
                Location = new Point(77, baseY),
                Size = new Size(1386, 900),
                FillColor = Color.FromArgb(248, 249, 252),
                Radius = 20,
                Visible = false
            };

            int ay = 40;
            lblBillingTitle = new Label { Text = "Billing Address *", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), Location = new Point(20, ay), AutoSize = true, Parent = pnlAddress };
            txtBillingAddress = new Guna2TextBox { Location = new Point(20, ay + 35), Size = new Size(640, 180), Multiline = true, BorderRadius = 10, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlAddress };
            lblShippingTitle = new Label { Text = "Shipping Address *", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), Location = new Point(680, ay), AutoSize = true, Parent = pnlAddress };
            txtShippingAddress = new Guna2TextBox { Location = new Point(680, ay + 35), Size = new Size(640, 180), Multiline = true, BorderRadius = 10, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlAddress };  

            int ty = ay + 240;
            lblSubtotal_Addr = new Label { Text = "SubTotal:", Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold), Location = new Point(900, ty), AutoSize = true, Parent = pnlAddress };
            lblSubtotalVal_Addr = new Label { Text = "₱0.00", Font = new Font("Bahnschrift SemiCondensed", 10F), Location = new Point(1100, ty), AutoSize = true, Parent = pnlAddress };
            lblDiscount_Addr = new Label { Text = "Discount (%):", Location = new Point(900, ty + 40), AutoSize = true, Parent = pnlAddress };
            numDiscount_Addr = new Guna2NumericUpDown { Location = new Point(1100, ty + 40), Size = new Size(100, 30), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlAddress };
            lblServiceFee_Addr = new Label { Text = "Service Fee:", Location = new Point(900, ty + 80), AutoSize = true, Parent = pnlAddress };
            numServiceFee_Addr = new Guna2NumericUpDown { Location = new Point(1100, ty + 80), Size = new Size(100, 30), BorderRadius = 5, Font = new Font("Bahnschrift SemiCondensed", 10.5F), ForeColor = Color.Black, Parent = pnlAddress };
            lblTotal_Addr = new Label { Text = "Total Amount:", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), Location = new Point(900, ty + 130), AutoSize = true, Parent = pnlAddress };
            lblTotalVal_Addr = new Label { Text = "₱0.00", Font = new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 123, 255), Location = new Point(1100, ty + 130), AutoSize = true, Parent = pnlAddress };

            // ← NO SECOND SAVE BUTTON HERE

            contentPanel.Controls.AddRange(new Control[]
            {
                lblHeader, lblRequired, lnkBack,
                btnRentalInfo, btnAddress,
                pnlRentalInfo, pnlAddress
            });

            pnlItems.Controls.AddRange(new Control[] { lblItemTable, txtSearchProduct, btnSearch, btnAddProduct, dgvItems });

            this.Controls.Add(mainPanel);
            this.ClientSize = new Size(1914, 1055);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Edit Rental";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}