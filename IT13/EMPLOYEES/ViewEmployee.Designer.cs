using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class ViewEmployee
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

            lblId = new Label();
            txtId = new Guna2TextBox();

            lblFirstName = new Label();
            txtFirstName = new Guna2TextBox();

            lblLastName = new Label();
            txtLastName = new Guna2TextBox();

            btnBack = new Guna2Button();

            mainpanel.SuspendLayout();
            this.SuspendLayout();

            // === MAIN PANEL ===
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Radius = 8;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.Controls.Add(lblHeader);
            mainpanel.Controls.Add(lblId);
            mainpanel.Controls.Add(txtId);
            mainpanel.Controls.Add(lblFirstName);
            mainpanel.Controls.Add(txtFirstName);
            mainpanel.Controls.Add(lblLastName);
            mainpanel.Controls.Add(txtLastName);
            mainpanel.Controls.Add(btnBack);

            // === HEADER ===
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Tahoma", 18F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(50, 50, 50);
            lblHeader.Location = new Point(77, 40);
            lblHeader.Text = "View Employee";

            // === EMPLOYEE ID ===
            lblId.AutoSize = true;
            lblId.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblId.ForeColor = Color.FromArgb(70, 70, 70);
            lblId.Location = new Point(77, 130);
            lblId.Text = "Employee ID";

            txtId.Location = new Point(77, 160);
            txtId.Size = new Size(200, 48);
            txtId.ReadOnly = true;
            txtId.BorderRadius = 8;
            txtId.BorderColor = Color.FromArgb(200, 200, 200);
            txtId.FillColor = Color.White;
            txtId.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtId.ForeColor = Color.Black;

            // === FIRST NAME ===
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblFirstName.ForeColor = Color.FromArgb(70, 70, 70);
            lblFirstName.Location = new Point(77, 230);
            lblFirstName.Text = "First Name";

            txtFirstName.Location = new Point(77, 260);
            txtFirstName.Size = new Size(600, 48);
            txtFirstName.ReadOnly = true;
            txtFirstName.BorderRadius = 8;
            txtFirstName.BorderColor = Color.FromArgb(200, 200, 200);
            txtFirstName.FillColor = Color.White;
            txtFirstName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtFirstName.ForeColor = Color.Black;

            // === LAST NAME ===
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Bahnschrift SemiCondensed", 10F);
            lblLastName.ForeColor = Color.FromArgb(70, 70, 70);
            lblLastName.Location = new Point(750, 230);
            lblLastName.Text = "Last Name";

            txtLastName.Location = new Point(750, 260);
            txtLastName.Size = new Size(600, 48);
            txtLastName.ReadOnly = true;
            txtLastName.BorderRadius = 8;
            txtLastName.BorderColor = Color.FromArgb(200, 200, 200);
            txtLastName.FillColor = Color.White;
            txtLastName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtLastName.ForeColor = Color.Black;

            // === BACK BUTTON ===
            btnBack.Location = new Point(1300, 380);
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
            this.Name = "ViewEmployee";
            this.Text = "View Employee";

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblHeader;
        private Label lblId;
        private Guna2TextBox txtId;
        private Label lblFirstName;
        private Guna2TextBox txtFirstName;
        private Label lblLastName;
        private Guna2TextBox txtLastName;
        private Guna2Button btnBack;
    }
}