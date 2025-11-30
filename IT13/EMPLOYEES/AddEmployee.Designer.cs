using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class AddEmployee
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
            lblHeader = new Label();
            lblRequired = new Label();

            lblFirstName = new Label();
            txtFirstName = new Guna2TextBox();

            lblLastName = new Label();
            txtLastName = new Guna2TextBox();

            btnCancel = new Guna2Button();
            btnSubmit = new Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            // ShadowMode.Forward removed – not needed and causes error
            mainpanel.Controls.Add(lblHeader);
            mainpanel.Controls.Add(lblRequired);
            mainpanel.Controls.Add(lblFirstName);
            mainpanel.Controls.Add(txtFirstName);
            mainpanel.Controls.Add(lblLastName);
            mainpanel.Controls.Add(txtLastName);
            mainpanel.Controls.Add(btnCancel);
            mainpanel.Controls.Add(btnSubmit);

            // === HEADER ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);
            lblHeader.Location = new Point(77, 40);
            lblHeader.Text = "Add Employee";

            // === REQUIRED TEXT ===
            lblRequired.AutoSize = true;
            lblRequired.Font = new Font("Tahoma", 9F);
            lblRequired.ForeColor = Color.Red;
            lblRequired.Location = new Point(77, 76);
            lblRequired.Text = "Fields marked with an asterisk (*) are required.";

            // === FIRST NAME ===
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblFirstName.ForeColor = Color.Black;
            lblFirstName.Location = new Point(77, 140);
            lblFirstName.Text = "First Name *";

            txtFirstName.Location = new Point(77, 170);
            txtFirstName.Size = new Size(600, 48);
            txtFirstName.BorderRadius = 8;
            txtFirstName.BorderColor = Color.FromArgb(200, 200, 200);
            txtFirstName.BorderThickness = 1;
            txtFirstName.FillColor = Color.White;
            txtFirstName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtFirstName.PlaceholderText = "Enter first name";
            txtFirstName.PlaceholderForeColor = Color.Gray;
            txtFirstName.ForeColor = Color.Black;

            // === LAST NAME ===
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblLastName.ForeColor = Color.Black;
            lblLastName.Location = new Point(750, 140);
            lblLastName.Text = "Last Name *";

            txtLastName.Location = new Point(750, 170);
            txtLastName.Size = new Size(600, 48);
            txtLastName.BorderRadius = 8;
            txtLastName.BorderColor = Color.FromArgb(200, 200, 200);
            txtLastName.BorderThickness = 1;
            txtLastName.FillColor = Color.White;
            txtLastName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtLastName.PlaceholderText = "Enter last name";
            txtLastName.PlaceholderForeColor = Color.Gray;
            txtLastName.ForeColor = Color.Black;

            // === CANCEL BUTTON ===
            btnCancel.Location = new Point(1150, 350);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.Click += btnCancel_Click;

            // === ADD EMPLOYEE BUTTON ===
            btnSubmit.Location = new Point(1300, 350);
            btnSubmit.Size = new Size(180, 50);
            btnSubmit.Text = "Add Employee";
            btnSubmit.FillColor = Color.FromArgb(0, 123, 255);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.BorderRadius = 8;
            btnSubmit.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnSubmit.Click += btnSubmit_Click;

            // === FORM ===
            this.ClientSize = new Size(1914, 1055);
            this.Controls.Add(mainpanel);
            this.Name = "AddEmployee";
            this.Text = "Add Employee";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblRequired;
        private Label lblFirstName;
        private Guna2TextBox txtFirstName;
        private Label lblLastName;
        private Guna2TextBox txtLastName;
        private Guna2Button btnCancel;
        private Guna2Button btnSubmit;
    }
}