using Guna.UI2.WinForms;

namespace IT13
{
    partial class EditCategory
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
            comboStatus = new Guna2ComboBox();
            lblName = new Label();
            txtName = new Guna2TextBox();
            lblDate = new Label();
            datePicker = new Guna2DateTimePicker();
            btnCancel = new Guna2Button();
            btnUpdate = new Guna2Button();

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
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(comboStatus);
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(txtName);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnUpdate);

            // TITLE — Kept your original Tahoma bold style
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(77, 40);
            lblTitle.Text = "Edit Category";

            // ID
            lblId.AutoSize = true;
            lblId.Font = new Font("Poppins", 11F);
            lblId.Location = new Point(77, 120);
            lblId.Text = "Category ID *";

            txtId.Location = new Point(77, 150);
            txtId.Size = new Size(600, 52);
            txtId.BorderRadius = 12;
            txtId.BorderThickness = 1;
            txtId.BorderColor = Color.FromArgb(180, 180, 180);
            txtId.FillColor = Color.FromArgb(240, 240, 240);
            txtId.ForeColor = Color.Black;
            txtId.Font = new Font("Poppins", 11F);
            txtId.ReadOnly = true;

            // STATUS
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Poppins", 11F);
            lblStatus.Location = new Point(750, 120);
            lblStatus.Text = "Category Status *";

            comboStatus.Location = new Point(750, 150);
            comboStatus.Size = new Size(600, 52);
            comboStatus.BorderRadius = 12;
            comboStatus.FillColor = Color.White;
            comboStatus.ForeColor = Color.Black;
            comboStatus.Font = new Font("Poppins", 11F);
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStatus.Items.AddRange(new[] { "Active", "Inactive" });

            // NAME
            lblName.AutoSize = true;
            lblName.Font = new Font("Poppins", 11F);
            lblName.Location = new Point(77, 220);
            lblName.Text = "Category Name *";

            txtName.Location = new Point(77, 250);
            txtName.Size = new Size(600, 52);
            txtName.BorderRadius = 12;
            txtName.BorderThickness = 1;
            txtName.BorderColor = Color.FromArgb(180, 180, 180);
            txtName.FillColor = Color.White;
            txtName.ForeColor = Color.Black;
            txtName.Font = new Font("Poppins", 11F);
            txtName.PlaceholderText = "Enter category name";
            txtName.PlaceholderForeColor = Color.Gray;

            // DATE
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Poppins", 11F);
            lblDate.Location = new Point(750, 220);
            lblDate.Text = "Date *";

            datePicker.Location = new Point(750, 250);
            datePicker.Size = new Size(600, 52);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.Font = new Font("Poppins", 11F);
            datePicker.FillColor = Color.White;
            datePicker.ForeColor = Color.Black;
            datePicker.BorderColor = Color.FromArgb(180, 180, 180);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 12;

            // CANCEL BUTTON — Red + Smaller
            btnCancel.Location = new Point(1180, 420);
            btnCancel.Size = new Size(130, 48);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 12;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.HoverState.FillColor = Color.FromArgb(200, 35, 51);
            btnCancel.Click += btnCancel_Click;

            // UPDATE BUTTON — Blue + Smaller
            btnUpdate.Location = new Point(1320, 420);
            btnUpdate.Size = new Size(190, 48);
            btnUpdate.Text = "Update Category";
            btnUpdate.FillColor = Color.FromArgb(0, 123, 255);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.BorderRadius = 12;
            btnUpdate.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnUpdate.HoverState.FillColor = Color.FromArgb(0, 105, 230);
            btnUpdate.Click += btnUpdate_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "EditCategory";
            Text = "Edit Category";
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
        private Guna2ComboBox comboStatus;
        private Label lblName;
        private Guna2TextBox txtName;
        private Label lblDate;
        private Guna2DateTimePicker datePicker;
        private Guna2Button btnCancel;
        private Guna2Button btnUpdate;
    }
}