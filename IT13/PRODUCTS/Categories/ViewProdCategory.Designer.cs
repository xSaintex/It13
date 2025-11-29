using Guna.UI2.WinForms;

namespace IT13
{
    partial class ViewProdCategory
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
            lblId = new Label();
            txtId = new Guna2TextBox();
            lblStatus = new Label();
            txtStatus = new Guna2TextBox();
            lblName = new Label();
            txtName = new Guna2TextBox();
            lblDate = new Label();
            txtDate = new Guna2TextBox();
            btnBack = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            // MAIN PANEL
            mainpanel.BackColor = Color.Transparent;
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 12;
            mainpanel.ShadowColor = Color.FromArgb(100, 0, 0, 0);
            mainpanel.Size = new Size(1602, 878);
            mainpanel.TabIndex = 0;
            mainpanel.Controls.Add(lblTitle);
            mainpanel.Controls.Add(lblId); mainpanel.Controls.Add(txtId);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(txtStatus);
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(txtName);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(txtDate);
            mainpanel.Controls.Add(btnBack);

            // TITLE — Kept your original Tahoma bold
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(77, 40);
            lblTitle.Text = "View Category Details";

            // ID
            lblId.AutoSize = true;
            lblId.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblId.Location = new Point(77, 120);
            lblId.Text = "Category ID";

            txtId.Location = new Point(77, 150);
            txtId.Size = new Size(600, 52);
            txtId.BorderRadius = 12;
            txtId.FillColor = Color.FromArgb(245, 245, 245);
            txtId.ForeColor = Color.Black;
            txtId.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtId.ReadOnly = true;

            // STATUS
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblStatus.Location = new Point(750, 120);
            lblStatus.Text = "Category Status";

            txtStatus.Location = new Point(750, 150);
            txtStatus.Size = new Size(600, 52);
            txtStatus.BorderRadius = 12;
            txtStatus.FillColor = Color.FromArgb(245, 245, 245);
            txtStatus.ForeColor = Color.Black;
            txtStatus.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtStatus.ReadOnly = true;

            // NAME
            lblName.AutoSize = true;
            lblName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblName.Location = new Point(77, 220);
            lblName.Text = "Category Name";

            txtName.Location = new Point(77, 250);
            txtName.Size = new Size(600, 52);
            txtName.BorderRadius = 12;
            txtName.FillColor = Color.FromArgb(245, 245, 245);
            txtName.ForeColor = Color.Black;
            txtName.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtName.ReadOnly = true;

            // DATE
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Bahnschrift SemiCondensed", 11F);
            lblDate.Location = new Point(750, 220);
            lblDate.Text = "Date";

            txtDate.Location = new Point(750, 250);
            txtDate.Size = new Size(600, 52);
            txtDate.BorderRadius = 12;
            txtDate.FillColor = Color.FromArgb(245, 245, 245);
            txtDate.ForeColor = Color.Black;
            txtDate.Font = new Font("Bahnschrift SemiCondensed", 11F);
            txtDate.ReadOnly = true;

            // BACK BUTTON — Red + Smaller (consistent with Cancel)
            btnBack.Location = new Point(1320, 420);
            btnBack.Size = new Size(160, 48);
            btnBack.Text = "Back to List";
            btnBack.FillColor = Color.FromArgb(220, 53, 69);
            btnBack.ForeColor = Color.White;
            btnBack.BorderRadius = 12;
            btnBack.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnBack.HoverState.FillColor = Color.FromArgb(200, 35, 51);
            btnBack.Click += btnBack_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "ViewProdCategory";
            Text = "View Category Details";
            StartPosition = FormStartPosition.CenterScreen;

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblTitle;
        private Label lblId;
        private Guna2TextBox txtId;
        private Label lblStatus;
        private Guna2TextBox txtStatus;
        private Label lblName;
        private Guna2TextBox txtName;
        private Label lblDate;
        private Guna2TextBox txtDate;
        private Guna2Button btnBack;
    }
}