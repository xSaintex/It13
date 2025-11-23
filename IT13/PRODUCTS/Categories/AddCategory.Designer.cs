using Guna.UI2.WinForms;

namespace IT13
{
    partial class AddCategory
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
            lblName = new Label();
            txtName = new Guna2TextBox();
            lblStatus = new Label();
            txtStatus = new Guna2TextBox();
            lblStatusNote = new Label();
            lblDate = new Label();
            datePicker = new Guna2DateTimePicker();
            btnCancel = new Guna2Button();
            btnAdd = new Guna2Button();

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
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(txtName);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(txtStatus);
            mainpanel.Controls.Add(lblStatusNote);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnAdd);

            // TITLE — Kept exactly like your original (big Tahoma bold)
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(77, 40);
            lblTitle.Text = "Add New Category";

            // CATEGORY NAME
            lblName.AutoSize = true;
            lblName.Font = new Font("Poppins", 11F);
            lblName.Location = new Point(77, 120);
            lblName.Text = "Category Name *";

            txtName.Location = new Point(77, 150);
            txtName.Size = new Size(600, 52);
            txtName.BorderRadius = 12;
            txtName.BorderThickness = 1;
            txtName.BorderColor = Color.FromArgb(180, 180, 180);
            txtName.FillColor = Color.White;
            txtName.ForeColor = Color.Black;
            txtName.Font = new Font("Poppins", 11F);
            txtName.PlaceholderText = "Enter category name";
            txtName.PlaceholderForeColor = Color.Gray;

            // STATUS
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Poppins", 11F);
            lblStatus.Location = new Point(750, 120);
            lblStatus.Text = "Category Status *";

            txtStatus.Location = new Point(750, 150);
            txtStatus.Size = new Size(600, 52);
            txtStatus.BorderRadius = 12;
            txtStatus.BorderThickness = 1;
            txtStatus.BorderColor = Color.FromArgb(180, 180, 180);
            txtStatus.FillColor = Color.FromArgb(240, 240, 240);
            txtStatus.ForeColor = Color.Black;
            txtStatus.Font = new Font("Poppins", 11F);
            txtStatus.Text = "Active";
            txtStatus.ReadOnly = true;

            lblStatusNote.AutoSize = true;
            lblStatusNote.Font = new Font("Poppins", 9F, FontStyle.Italic);
            lblStatusNote.ForeColor = Color.FromArgb(100, 100, 100);
            lblStatusNote.Location = new Point(750, 210);
            lblStatusNote.Text = "Status is automatically set to \"Active\" and cannot be edited.";

            // DATE
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Poppins", 11F);
            lblDate.Location = new Point(750, 260);
            lblDate.Text = "Date *";

            datePicker.Location = new Point(750, 290);
            datePicker.Size = new Size(600, 52);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.Font = new Font("Poppins", 11F);
            datePicker.FillColor = Color.White;
            datePicker.ForeColor = Color.Black;
            datePicker.BorderColor = Color.FromArgb(180, 180, 180);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 12;

            // CANCEL BUTTON — Smaller + Red
            btnCancel.Location = new Point(1180, 420);
            btnCancel.Size = new Size(130, 48);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 12;
            btnCancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnCancel.HoverState.FillColor = Color.FromArgb(200, 35, 51);
            btnCancel.Click += btnCancel_Click;

            // ADD BUTTON — Smaller + Blue
            btnAdd.Location = new Point(1320, 420);
            btnAdd.Size = new Size(160, 48);
            btnAdd.Text = "Add Category";
            btnAdd.FillColor = Color.FromArgb(0, 123, 255);
            btnAdd.ForeColor = Color.White;
            btnAdd.BorderRadius = 12;
            btnAdd.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btnAdd.HoverState.FillColor = Color.FromArgb(0, 105, 230);
            btnAdd.Click += btnAdd_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "AddCategory";
            Text = "Add Category";
            StartPosition = FormStartPosition.CenterScreen;

            mainpanel.ResumeLayout(false);
            mainpanel.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2ShadowPanel mainpanel;
        private Label lblTitle;
        private Label lblName;
        private Guna2TextBox txtName;
        private Label lblStatus;
        private Guna2TextBox txtStatus;
        private Label lblStatusNote;
        private Label lblDate;
        private Guna2DateTimePicker datePicker;
        private Guna2Button btnCancel;
        private Guna2Button btnAdd;
    }
}