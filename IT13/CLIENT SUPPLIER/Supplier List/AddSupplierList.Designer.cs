using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class AddSupplierList
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

            mainPanel = new Guna2ShadowPanel();
            scrollPanel = new Guna2Panel();
            contentPanel = new Guna2Panel();
            bottomPanel = new Guna2Panel();

            lblHeader = new Label(); lblRequired = new Label(); lblNote = new Label();
            lblTitle = new Label(); txtTitle = new Guna2TextBox();
            lblFName = new Label(); txtFName = new Guna2TextBox();
            lblLName = new Label(); txtLName = new Guna2TextBox();
            lblEmail = new Label(); txtEmail = new Guna2TextBox();
            lblCompany = new Label(); txtCompany = new Guna2TextBox();
            lblPhone = new Label(); txtPhone = new Guna2TextBox();
            lblPayment = new Label(); cmbPayment = new Guna2ComboBox();
            lblStatus = new Label(); cmbStatus = new Guna2ComboBox();

            btnOther = new Guna2Button();
            btnAddress = new Guna2Button();
            btnRemarks = new Guna2Button();

            pnlOther = new Guna2ShadowPanel();
            pnlAddress = new Guna2ShadowPanel();
            pnlRemarks = new Guna2ShadowPanel();

            lblContactPerson = new Label(); txtContactPerson = new Guna2TextBox();
            lblContactNum = new Label(); txtContactNum = new Guna2TextBox();

            lblBilling = new Label(); lblShip = new Label(); lnkCopy = new LinkLabel();
            lblBCountry = new Label(); cmbBCountry = new Guna2ComboBox();
            lblBCity = new Label(); txtBCity = new Guna2TextBox();
            lblBZip = new Label(); txtBZip = new Guna2TextBox();
            lblBLine1 = new Label(); txtBLine1 = new Guna2TextBox();
            lblBLine2 = new Label(); txtBLine2 = new Guna2TextBox();
            lblSCountry = new Label(); cmbSCountry = new Guna2ComboBox();
            lblSCity = new Label(); txtSCity = new Guna2TextBox();
            lblSZip = new Label(); txtSZip = new Guna2TextBox();
            lblSLine1 = new Label(); txtSLine1 = new Guna2TextBox();
            lblSLine2 = new Label(); txtSLine2 = new Guna2TextBox();

            lblRemarksTitle = new Label();
            txtRemarks = new Guna2TextBox();

            btnCancel = new Guna2Button();
            btnSave = new Guna2Button();

            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            pnlOther.SuspendLayout();
            pnlAddress.SuspendLayout();
            pnlRemarks.SuspendLayout();
            this.SuspendLayout();

            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 860);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.Controls.Add(scrollPanel);
            mainPanel.Controls.Add(bottomPanel);

            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1602, 780);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);
            contentPanel.AutoSize = true;
            contentPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            bottomPanel.Location = new Point(0, 780);
            bottomPanel.Size = new Size(1602, 80);
            bottomPanel.BackColor = Color.White;

            // ==================== HEADER ====================
            lblHeader.Text = "Add New Supplier";
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.AutoSize = true;

            lblRequired.Text = "Fields marked with an asterisk (*) are required.";
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.AutoSize = true;

            lblNote.Text = "Note: Supplier's Email and Phone number must be unique.";
            lblNote.Font = new Font("Tahoma", 9F);
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(77, 80);
            lblNote.AutoSize = true;

            int y = 120;

            // ==================== BASIC INFO ====================
            lblTitle.Text = "Title"; lblTitle.Location = new Point(77, y);
            txtTitle.Location = new Point(77, y + 25); txtTitle.Size = new Size(100, 36); txtTitle.BorderRadius = 5;

            lblFName.Text = "First Name *"; lblFName.Location = new Point(197, y);
            txtFName.Location = new Point(197, y + 25); txtFName.Size = new Size(300, 36); txtFName.BorderRadius = 5;

            lblLName.Text = "Last Name *"; lblLName.Location = new Point(517, y);
            txtLName.Location = new Point(517, y + 25); txtLName.Size = new Size(300, 36); txtLName.BorderRadius = 5;

            lblEmail.Text = "Email *"; lblEmail.Location = new Point(837, y);
            txtEmail.Location = new Point(837, y + 25); txtEmail.Size = new Size(500, 36); txtEmail.BorderRadius = 5;

            y += 80;

            lblCompany.Text = "Company Name"; lblCompany.Location = new Point(77, y);
            txtCompany.Location = new Point(77, y + 25); txtCompany.Size = new Size(600, 36); txtCompany.BorderRadius = 5;

            lblPhone.Text = "Phone number *"; lblPhone.Location = new Point(697, y);
            txtPhone.Location = new Point(697, y + 25); txtPhone.Size = new Size(300, 36); txtPhone.BorderRadius = 5;

            lblPayment.Text = "Payment Terms *"; lblPayment.Location = new Point(77, y + 80);
            cmbPayment.Location = new Point(77, y + 105); cmbPayment.Size = new Size(300, 36); cmbPayment.BorderRadius = 5;
            cmbPayment.Items.AddRange(new[] { "Cash", "Net 15", "Net 30", "Net 60" });

            lblStatus.Text = "Status"; lblStatus.Location = new Point(397, y + 80);
            cmbStatus.Location = new Point(397, y + 105); cmbStatus.Size = new Size(150, 36); cmbStatus.BorderRadius = 5;
            cmbStatus.Items.AddRange(new[] { "Active", "Inactive" });
            cmbStatus.Text = "Active";

            y += 180;

            // ==================== TAB BUTTONS ====================
            btnOther.Text = "Other Details"; btnOther.Location = new Point(77, y); btnOther.Size = new Size(150, 36); btnOther.BorderRadius = 5;
            btnOther.FillColor = Color.FromArgb(0, 123, 255); btnOther.ForeColor = Color.White;

            btnAddress.Text = "Address"; btnAddress.Location = new Point(237, y); btnAddress.Size = new Size(150, 36); btnAddress.BorderRadius = 5;
            btnAddress.FillColor = Color.WhiteSmoke; btnAddress.ForeColor = Color.Black;

            btnRemarks.Text = "Remarks"; btnRemarks.Location = new Point(397, y); btnRemarks.Size = new Size(150, 36); btnRemarks.BorderRadius = 5;
            btnRemarks.FillColor = Color.WhiteSmoke; btnRemarks.ForeColor = Color.Black;

            int panelY = y + 50;

            // ==================== OTHER DETAILS PANEL ====================
            pnlOther.Location = new Point(77, panelY); pnlOther.Size = new Size(1300, 200);
            pnlOther.FillColor = Color.FromArgb(248, 249, 252); pnlOther.Radius = 20; pnlOther.Visible = true;

            lblContactPerson.Text = "Contact Person *"; lblContactPerson.Location = new Point(40, 25);
            txtContactPerson.Location = new Point(40, 50); txtContactPerson.Size = new Size(600, 36); txtContactPerson.BorderRadius = 5;

            lblContactNum.Text = "Contact number *"; lblContactNum.Location = new Point(40, 105);
            txtContactNum.Location = new Point(40, 130); txtContactNum.Size = new Size(300, 36); txtContactNum.BorderRadius = 5;

            pnlOther.Controls.AddRange(new Control[] { lblContactPerson, txtContactPerson, lblContactNum, txtContactNum });

            // ==================== ADDRESS PANEL ====================
            pnlAddress.Location = new Point(77, panelY); pnlAddress.Size = new Size(1300, 520);
            pnlAddress.FillColor = Color.FromArgb(248, 249, 252); pnlAddress.Radius = 20; pnlAddress.Visible = false;

            int lx = 40, rx = 760, fy = 70;
            lblBilling.Text = "Billing Address"; lblBilling.Font = new Font("Segoe UI", 12F, FontStyle.Bold); lblBilling.Location = new Point(lx, 25);
            lblShip.Text = "Shipping Address"; lblShip.Font = new Font("Segoe UI", 12F, FontStyle.Bold); lblShip.Location = new Point(rx, 25);
            lnkCopy.Text = "Copy billing address"; lnkCopy.Location = new Point(rx, 25); lnkCopy.LinkColor = Color.FromArgb(0, 123, 255);

            // Billing
            lblBCountry.Text = "Country *"; lblBCountry.Location = new Point(lx, fy);
            cmbBCountry.Location = new Point(lx, fy + 25); cmbBCountry.Size = new Size(300, 36); cmbBCountry.BorderRadius = 5;
            cmbBCountry.Items.AddRange(new[] { "Philippines", "United States", "Canada", "United Kingdom", "Australia" });
            cmbBCountry.Text = "Philippines";

            lblBCity.Text = "City *"; lblBCity.Location = new Point(lx, fy + 80);
            txtBCity.Location = new Point(lx, fy + 105); txtBCity.Size = new Size(300, 36); txtBCity.BorderRadius = 5;

            lblBZip.Text = "Zip Code *"; lblBZip.Location = new Point(lx, fy + 160);
            txtBZip.Location = new Point(lx, fy + 185); txtBZip.Size = new Size(150, 36); txtBZip.BorderRadius = 5;

            lblBLine1.Text = "Address Line 1 *"; lblBLine1.Location = new Point(lx, fy + 240);
            txtBLine1.Location = new Point(lx, fy + 265); txtBLine1.Size = new Size(500, 36); txtBLine1.BorderRadius = 5;

            lblBLine2.Text = "Address Line 2"; lblBLine2.Location = new Point(lx, fy + 320);
            txtBLine2.Location = new Point(lx, fy + 345); txtBLine2.Size = new Size(500, 36); txtBLine2.BorderRadius = 5;

            // Shipping
            lblSCountry.Text = "Country *"; lblSCountry.Location = new Point(rx, fy);
            cmbSCountry.Location = new Point(rx, fy + 25); cmbSCountry.Size = new Size(300, 36); cmbSCountry.BorderRadius = 5;
            cmbSCountry.Items.AddRange(new[] { "Philippines", "United States", "Canada", "United Kingdom", "Australia" });
            cmbSCountry.Text = "Philippines";

            lblSCity.Text = "City *"; lblSCity.Location = new Point(rx, fy + 80);
            txtSCity.Location = new Point(rx, fy + 105); txtSCity.Size = new Size(300, 36); txtSCity.BorderRadius = 5;

            lblSZip.Text = "Zip Code *"; lblSZip.Location = new Point(rx, fy + 160);
            txtSZip.Location = new Point(rx, fy + 185); txtSZip.Size = new Size(150, 36); txtSZip.BorderRadius = 5;

            lblSLine1.Text = "Address Line 1 *"; lblSLine1.Location = new Point(rx, fy + 240);
            txtSLine1.Location = new Point(rx, fy + 265); txtSLine1.Size = new Size(500, 36); txtSLine1.BorderRadius = 5;

            lblSLine2.Text = "Address Line 2"; lblSLine2.Location = new Point(rx, fy + 320);
            txtSLine2.Location = new Point(rx, fy + 345); txtSLine2.Size = new Size(500, 36); txtSLine2.BorderRadius = 5;

            pnlAddress.Controls.AddRange(new Control[] {
                lblBilling, lblShip, lnkCopy,
                lblBCountry, cmbBCountry, lblBCity, txtBCity, lblBZip, txtBZip, lblBLine1, txtBLine1, lblBLine2, txtBLine2,
                lblSCountry, cmbSCountry, lblSCity, txtSCity, lblSZip, txtSZip, lblSLine1, txtSLine1, lblSLine2, txtSLine2
            });

            // ==================== REMARKS PANEL ====================
            pnlRemarks.Location = new Point(77, panelY); pnlRemarks.Size = new Size(1300, 320);
            pnlRemarks.FillColor = Color.FromArgb(248, 249, 252); pnlRemarks.Radius = 20; pnlRemarks.Visible = false;

            lblRemarksTitle.Text = "Remarks / Additional Notes";
            lblRemarksTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRemarksTitle.Location = new Point(40, 30);

            txtRemarks.Location = new Point(40, 70);
            txtRemarks.Size = new Size(1220, 220);
            txtRemarks.Multiline = true;
            txtRemarks.BorderRadius = 8;
            txtRemarks.Font = new Font("Segoe UI", 10F);
            txtRemarks.ScrollBars = ScrollBars.Vertical;
            txtRemarks.AcceptsReturn = true;

            pnlRemarks.Controls.Add(lblRemarksTitle);
            pnlRemarks.Controls.Add(txtRemarks);

            // ==================== BOTTOM BUTTONS ====================
            btnCancel.Text = "Cancel"; btnCancel.Location = new Point(1200, 20); btnCancel.Size = new Size(120, 40);
            btnCancel.FillColor = Color.FromArgb(220, 53, 69); btnCancel.ForeColor = Color.White; btnCancel.BorderRadius = 8;

            btnSave.Text = "Save Supplier"; btnSave.Location = new Point(1330, 20); btnSave.Size = new Size(180, 40);
            btnSave.FillColor = Color.FromArgb(0, 123, 255); btnSave.ForeColor = Color.White; btnSave.BorderRadius = 8;

            bottomPanel.Controls.AddRange(new Control[] { btnCancel, btnSave });

            // ==================== ADD ALL CONTROLS ====================
            contentPanel.Controls.AddRange(new Control[] {
                lblHeader, lblRequired, lblNote,
                lblTitle, txtTitle, lblFName, txtFName, lblLName, txtLName, lblEmail, txtEmail,
                lblCompany, txtCompany, lblPhone, txtPhone, lblPayment, cmbPayment, lblStatus, cmbStatus,
                btnOther, btnAddress, btnRemarks,
                pnlOther, pnlAddress, pnlRemarks
            });

            this.Controls.Add(mainPanel);
            this.ClientSize = new Size(1914, 1020);
            this.Text = "Add Supplier";
            this.StartPosition = FormStartPosition.CenterScreen;

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            pnlOther.ResumeLayout(false);
            pnlAddress.ResumeLayout(false);
            pnlRemarks.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ==================== ALL CONTROLS DECLARATION ====================
        private Guna2ShadowPanel mainPanel, pnlOther, pnlAddress, pnlRemarks;
        private Guna2Panel scrollPanel, contentPanel, bottomPanel;
        private Label lblHeader, lblRequired, lblNote, lblTitle, lblFName, lblLName, lblEmail, lblCompany, lblPhone, lblPayment, lblStatus;
        private Guna2TextBox txtTitle, txtFName, txtLName, txtEmail, txtCompany, txtPhone, txtRemarks;
        private Guna2ComboBox cmbPayment, cmbStatus;
        private Guna2Button btnOther, btnAddress, btnRemarks, btnCancel, btnSave;
        private Label lblContactPerson, lblContactNum, lblBilling, lblShip, lblRemarksTitle;
        private Guna2TextBox txtContactPerson, txtContactNum;
        private LinkLabel lnkCopy;
        private Label lblBCountry, lblBCity, lblBZip, lblBLine1, lblBLine2, lblSCountry, lblSCity, lblSZip, lblSLine1, lblSLine2;
        private Guna2ComboBox cmbBCountry, cmbSCountry;
        private Guna2TextBox txtBCity, txtBZip, txtBLine1, txtBLine2, txtSCity, txtSZip, txtSLine1, txtSLine2;
    }
}