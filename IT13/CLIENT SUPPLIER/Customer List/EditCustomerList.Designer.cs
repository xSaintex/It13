using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace IT13
{
    partial class EditCustomerList
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

            btnOther = new Guna2Button(); btnAddress = new Guna2Button();

            pnlOther = new Guna2ShadowPanel();
            lblContactPerson = new Label(); txtContactPerson = new Guna2TextBox();
            lblContactNum = new Label(); txtContactNum = new Guna2TextBox();

            pnlAddress = new Guna2ShadowPanel();
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

            btnCancel = new Guna2Button(); btnSave = new Guna2Button();

            mainPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            pnlOther.SuspendLayout();
            pnlAddress.SuspendLayout();
            this.SuspendLayout();

            // MAIN PANEL
            mainPanel.Location = new Point(300, 88);
            mainPanel.Size = new Size(1602, 878);
            mainPanel.FillColor = Color.White;
            mainPanel.Radius = 8;
            mainPanel.Controls.Add(scrollPanel);
            mainPanel.Controls.Add(bottomPanel);

            // PERFECT SCROLLING
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Size = new Size(1597, 700);
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(contentPanel);

            contentPanel.Size = new Size(1458, 950);

            // HEADER (kept Tahoma)
            lblHeader.Text = "Customer Information";
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(77, 20);
            lblHeader.AutoSize = true;
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);

            lblRequired.Text = "Fields marked with an asterisk (*) are required.";
            lblRequired.Font = new Font("Poppins", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 56);
            lblRequired.AutoSize = true;

            lblNote.Text = "Note: Customer's Email and Phone number must be unique.";
            lblNote.Font = new Font("Poppins", 9F);
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(77, 80);
            lblNote.AutoSize = true;

            int y = 120;

            // BASIC INFO - ALL POPPINS + BLACK TEXT
            lblTitle.Text = "Title";
            lblTitle.Font = new Font("Poppins", 10F);
            lblTitle.Location = new Point(77, y);
            lblTitle.AutoSize = true;
            txtTitle.Location = new Point(77, y + 25);
            txtTitle.Size = new Size(100, 36);
            txtTitle.BorderRadius = 5;
            txtTitle.Font = new Font("Poppins", 10.5F);
            txtTitle.ForeColor = Color.Black;
            txtTitle.PlaceholderText = "Mr.";

            lblFName.Text = "First Name *";
            lblFName.Font = new Font("Poppins", 10F);
            lblFName.Location = new Point(197, y);
            lblFName.AutoSize = true;
            txtFName.Location = new Point(197, y + 25);
            txtFName.Size = new Size(300, 36);
            txtFName.BorderRadius = 5;
            txtFName.Font = new Font("Poppins", 10.5F);
            txtFName.ForeColor = Color.Black;

            lblLName.Text = "Last Name *";
            lblLName.Font = new Font("Poppins", 10F);
            lblLName.Location = new Point(517, y);
            lblLName.AutoSize = true;
            txtLName.Location = new Point(517, y + 25);
            txtLName.Size = new Size(300, 36);
            txtLName.BorderRadius = 5;
            txtLName.Font = new Font("Poppins", 10.5F);
            txtLName.ForeColor = Color.Black;

            lblEmail.Text = "Email *";
            lblEmail.Font = new Font("Poppins", 10F);
            lblEmail.Location = new Point(837, y);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(837, y + 25);
            txtEmail.Size = new Size(500, 36);
            txtEmail.BorderRadius = 5;
            txtEmail.Font = new Font("Poppins", 10.5F);
            txtEmail.ForeColor = Color.Black;

            y += 80;

            lblCompany.Text = "Company Name";
            lblCompany.Font = new Font("Poppins", 10F);
            lblCompany.Location = new Point(77, y);
            lblCompany.AutoSize = true;
            txtCompany.Location = new Point(77, y + 25);
            txtCompany.Size = new Size(600, 36);
            txtCompany.BorderRadius = 5;
            txtCompany.Font = new Font("Poppins", 10.5F);
            txtCompany.ForeColor = Color.Black;

            lblPhone.Text = "Phone number *";
            lblPhone.Font = new Font("Poppins", 10F);
            lblPhone.Location = new Point(697, y);
            lblPhone.AutoSize = true;
            txtPhone.Location = new Point(697, y + 25);
            txtPhone.Size = new Size(300, 36);
            txtPhone.BorderRadius = 5;
            txtPhone.Font = new Font("Poppins", 10.5F);
            txtPhone.ForeColor = Color.Black;

            lblPayment.Text = "Payment Terms *";
            lblPayment.Font = new Font("Poppins", 10F);
            lblPayment.Location = new Point(77, y + 80);
            lblPayment.AutoSize = true;
            cmbPayment.Location = new Point(77, y + 105);
            cmbPayment.Size = new Size(300, 36);
            cmbPayment.BorderRadius = 5;
            cmbPayment.Font = new Font("Poppins", 10.5F);
            cmbPayment.ForeColor = Color.Black;
            cmbPayment.Items.AddRange(new[] { "Cash", "Net 15", "Net 30", "Net 60" });

            lblStatus.Text = "Status";
            lblStatus.Font = new Font("Poppins", 10F);
            lblStatus.Location = new Point(397, y + 80);
            lblStatus.AutoSize = true;
            cmbStatus.Location = new Point(397, y + 105);
            cmbStatus.Size = new Size(150, 36);
            cmbStatus.BorderRadius = 5;
            cmbStatus.Font = new Font("Poppins", 10.5F);
            cmbStatus.ForeColor = Color.Black;
            cmbStatus.Items.AddRange(new[] { "Active", "Inactive" });

            y += 180;

            btnOther.Text = "📄 Other Details";
            btnOther.Font = new Font("Tahoma", 8F, FontStyle.Regular);
            btnOther.Location = new Point(77, y);
            btnOther.Size = new Size(170, 36);
            btnOther.BorderRadius = 5;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;

            btnAddress.Text = "📍 Address";
            btnAddress.Font = new Font("Tahoma", 8F, FontStyle.Regular);
            btnAddress.Location = new Point(257, y);
            btnAddress.Size = new Size(150, 36);
            btnAddress.BorderRadius = 5;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;

            // OTHER DETAILS
            pnlOther.Location = new Point(77, y + 50);
            pnlOther.Size = new Size(1300, 200);
            pnlOther.FillColor = Color.FromArgb(248, 249, 252);
            pnlOther.ShadowColor = Color.FromArgb(100, 100, 100);
            pnlOther.Radius = 20;
            pnlOther.Visible = true;

            lblContactPerson.Text = "Contact Person *";
            lblContactPerson.Font = new Font("Poppins", 10F);
            lblContactPerson.Location = new Point(40, 25);
            lblContactPerson.AutoSize = true;
            txtContactPerson.Location = new Point(40, 50);
            txtContactPerson.Size = new Size(600, 36);
            txtContactPerson.BorderRadius = 5;
            txtContactPerson.Font = new Font("Poppins", 10.5F);
            txtContactPerson.ForeColor = Color.Black;

            lblContactNum.Text = "Contact number *";
            lblContactNum.Font = new Font("Poppins", 10F);
            lblContactNum.Location = new Point(40, 105);
            lblContactNum.AutoSize = true;
            txtContactNum.Location = new Point(40, 130);
            txtContactNum.Size = new Size(300, 36);
            txtContactNum.BorderRadius = 5;
            txtContactNum.Font = new Font("Poppins", 10.5F);
            txtContactNum.ForeColor = Color.Black;

            pnlOther.Controls.AddRange(new Control[] { lblContactPerson, txtContactPerson, lblContactNum, txtContactNum });

            // ADDRESS PANEL
            pnlAddress.Location = new Point(77, y + 50);
            pnlAddress.Size = new Size(1300, 520);
            pnlAddress.FillColor = Color.FromArgb(248, 249, 252);
            pnlAddress.ShadowColor = Color.FromArgb(100, 100, 100);
            pnlAddress.Radius = 20;
            pnlAddress.Visible = false;

            int leftX = 40, rightX = 760, fieldY = 70;

            lblBilling.Text = "Billing Address";
            lblBilling.Font = new Font("Poppins", 12F, FontStyle.Bold);
            lblBilling.Location = new Point(leftX, 25);
            lblBilling.AutoSize = true;

            lblShip.Text = "Shipping Address";
            lblShip.Font = new Font("Poppins", 12F, FontStyle.Bold);
            lblShip.Location = new Point(rightX, 25);
            lblShip.AutoSize = true;

            lnkCopy.Text = "Copy billing address";
            lnkCopy.Font = new Font("Poppins", 9.5F);
            lnkCopy.Location = new Point(rightX + 10, 28);
            lnkCopy.AutoSize = true;
            lnkCopy.LinkColor = Color.FromArgb(0, 123, 255);

            // Billing Fields
            lblBCountry.Text = "Country *"; lblBCountry.Font = new Font("Poppins", 10F); lblBCountry.Location = new Point(leftX, fieldY); lblBCountry.AutoSize = true;
            cmbBCountry.Location = new Point(leftX, fieldY + 25); cmbBCountry.Size = new Size(300, 36); cmbBCountry.BorderRadius = 5;
            cmbBCountry.Font = new Font("Poppins", 10.5F); cmbBCountry.ForeColor = Color.Black;
            cmbBCountry.Items.AddRange(new[] { "Philippines", "United States", "Canada", "United Kingdom", "Australia" });

            lblBCity.Text = "City *"; lblBCity.Font = new Font("Poppins", 10F); lblBCity.Location = new Point(leftX, fieldY + 80); lblBCity.AutoSize = true;
            txtBCity.Location = new Point(leftX, fieldY + 105); txtBCity.Size = new Size(300, 36); txtBCity.BorderRadius = 5;
            txtBCity.Font = new Font("Poppins", 10.5F); txtBCity.ForeColor = Color.Black;

            lblBZip.Text = "Zip Code *"; lblBZip.Font = new Font("Poppins", 10F); lblBZip.Location = new Point(leftX, fieldY + 160); lblBZip.AutoSize = true;
            txtBZip.Location = new Point(leftX, fieldY + 185); txtBZip.Size = new Size(150, 36); txtBZip.BorderRadius = 5;
            txtBZip.Font = new Font("Poppins", 10.5F); txtBZip.ForeColor = Color.Black;

            lblBLine1.Text = "Address Line 1 *"; lblBLine1.Font = new Font("Poppins", 10F); lblBLine1.Location = new Point(leftX, fieldY + 240); lblBLine1.AutoSize = true;
            txtBLine1.Location = new Point(leftX, fieldY + 265); txtBLine1.Size = new Size(500, 36); txtBLine1.BorderRadius = 5;
            txtBLine1.Font = new Font("Poppins", 10.5F); txtBLine1.ForeColor = Color.Black;

            lblBLine2.Text = "Address Line 2"; lblBLine2.Font = new Font("Poppins", 10F); lblBLine2.Location = new Point(leftX, fieldY + 320); lblBLine2.AutoSize = true;
            txtBLine2.Location = new Point(leftX, fieldY + 345); txtBLine2.Size = new Size(500, 36); txtBLine2.BorderRadius = 5;
            txtBLine2.Font = new Font("Poppins", 10.5F); txtBLine2.ForeColor = Color.Black;

            // Shipping Fields
            lblSCountry.Text = "Country *"; lblSCountry.Font = new Font("Poppins", 10F); lblSCountry.Location = new Point(rightX, fieldY); lblSCountry.AutoSize = true;
            cmbSCountry.Location = new Point(rightX, fieldY + 25); cmbSCountry.Size = new Size(300, 36); cmbSCountry.BorderRadius = 5;
            cmbSCountry.Font = new Font("Poppins", 10.5F); cmbSCountry.ForeColor = Color.Black;
            cmbSCountry.Items.AddRange(new[] { "Philippines", "United States", "Canada", "United Kingdom", "Australia" });

            lblSCity.Text = "City *"; lblSCity.Font = new Font("Poppins", 10F); lblSCity.Location = new Point(rightX, fieldY + 80); lblSCity.AutoSize = true;
            txtSCity.Location = new Point(rightX, fieldY + 105); txtSCity.Size = new Size(300, 36); txtSCity.BorderRadius = 5;
            txtSCity.Font = new Font("Poppins", 10.5F); txtSCity.ForeColor = Color.Black;

            lblSZip.Text = "Zip Code *"; lblSZip.Font = new Font("Poppins", 10F); lblSZip.Location = new Point(rightX, fieldY + 160); lblSZip.AutoSize = true;
            txtSZip.Location = new Point(rightX, fieldY + 185); txtSZip.Size = new Size(150, 36); txtSZip.BorderRadius = 5;
            txtSZip.Font = new Font("Poppins", 10.5F); txtSZip.ForeColor = Color.Black;

            lblSLine1.Text = "Address Line 1 *"; lblSLine1.Font = new Font("Poppins", 10F); lblSLine1.Location = new Point(rightX, fieldY + 240); lblSLine1.AutoSize = true;
            txtSLine1.Location = new Point(rightX, fieldY + 265); txtSLine1.Size = new Size(500, 36); txtSLine1.BorderRadius = 5;
            txtSLine1.Font = new Font("Poppins", 10.5F); txtSLine1.ForeColor = Color.Black;

            lblSLine2.Text = "Address Line 2"; lblSLine2.Font = new Font("Poppins", 10F); lblSLine2.Location = new Point(rightX, fieldY + 320); lblSLine2.AutoSize = true;
            txtSLine2.Location = new Point(rightX, fieldY + 345); txtSLine2.Size = new Size(500, 36); txtSLine2.BorderRadius = 5;
            txtSLine2.Font = new Font("Poppins", 10.5F); txtSLine2.ForeColor = Color.Black;

            pnlAddress.Controls.AddRange(new Control[] {
                lblBilling, lblShip, lnkCopy,
                lblBCountry, cmbBCountry, lblBCity, txtBCity, lblBZip, txtBZip, lblBLine1, txtBLine1, lblBLine2, txtBLine2,
                lblSCountry, cmbSCountry, lblSCity, txtSCity, lblSZip, txtSZip, lblSLine1, txtSLine1, lblSLine2, txtSLine2
            });

            // BOTTOM PANEL
            bottomPanel.Location = new Point(0, 780);
            bottomPanel.Size = new Size(1602, 78);
            bottomPanel.BackColor = Color.Transparent;

            btnCancel.Text = "Cancel";
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.Location = new Point(1200, 20);
            btnCancel.Size = new Size(120, 40);
            btnCancel.BorderRadius = 8;
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;

            btnSave.Text = "Update";
            btnSave.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnSave.Location = new Point(1330, 20);
            btnSave.Size = new Size(120, 40);
            btnSave.BorderRadius = 8;
            btnSave.FillColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;

            bottomPanel.Controls.AddRange(new Control[] { btnCancel, btnSave });

            contentPanel.Controls.AddRange(new Control[] {
                lblHeader, lblRequired, lblNote,
                lblTitle, txtTitle, lblFName, txtFName, lblLName, txtLName, lblEmail, txtEmail,
                lblCompany, txtCompany, lblPhone, txtPhone, lblPayment, cmbPayment, lblStatus, cmbStatus,
                btnOther, btnAddress, pnlOther, pnlAddress
            });

            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainPanel);
            this.Text = "Edit Customer";

            mainPanel.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            pnlOther.ResumeLayout(false);
            pnlAddress.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainPanel, pnlOther, pnlAddress;
        private Guna2Panel scrollPanel, contentPanel, bottomPanel;
        private Label lblHeader, lblRequired, lblNote, lblTitle, lblFName, lblLName, lblEmail, lblCompany, lblPhone, lblPayment, lblStatus;
        private Guna2TextBox txtTitle, txtFName, txtLName, txtEmail, txtCompany, txtPhone;
        private Guna2ComboBox cmbPayment, cmbStatus;
        private Guna2Button btnOther, btnAddress, btnCancel, btnSave;
        private Label lblContactPerson, lblContactNum, lblBilling, lblShip;
        private Guna2TextBox txtContactPerson, txtContactNum;
        private LinkLabel lnkCopy;
        private Label lblBCountry, lblBCity, lblBZip, lblBLine1, lblBLine2, lblSCountry, lblSCity, lblSZip, lblSLine1, lblSLine2;
        private Guna2ComboBox cmbBCountry, cmbSCountry;
        private Guna2TextBox txtBCity, txtBZip, txtBLine1, txtBLine2, txtSCity, txtSZip, txtSLine1, txtSLine2;
    }
}