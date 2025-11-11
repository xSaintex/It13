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
            mainpanel.Controls.Add(lblTitle);
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(txtName);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(txtStatus);
            mainpanel.Controls.Add(lblStatusNote);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnAdd);
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 8;
            mainpanel.ShadowColor = Color.Black;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.TabIndex = 0;

            // TITLE
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(77, 40);
            lblTitle.Text = "Add New Category";

            // CATEGORY NAME
            lblName.AutoSize = true;
            lblName.Location = new Point(77, 120);
            lblName.Text = "Category Name *";
            txtName.Location = new Point(77, 150);
            txtName.Size = new Size(600, 45);

            // STATUS
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(750, 120);
            lblStatus.Text = "Category Status *";
            txtStatus.Location = new Point(750, 150);
            txtStatus.Size = new Size(600, 45);
            txtStatus.Text = "Active";
            txtStatus.ReadOnly = true;

            lblStatusNote.AutoSize = true;
            lblStatusNote.ForeColor = Color.Gray;
            lblStatusNote.Location = new Point(750, 200);
            lblStatusNote.Text = "Status is automatically set to \"Active\" and cannot be edited.";

            // DATE – CALENDAR PICKER (WHITE BG)
            lblDate.AutoSize = true;
            lblDate.Location = new Point(750, 260);
            lblDate.Text = "Date *";

            datePicker.Location = new Point(750, 290);
            datePicker.Size = new Size(600, 45);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "MM/dd/yyyy";
            datePicker.ShowUpDown = false;
            datePicker.Font = new Font("Tahoma", 10F);
            datePicker.FillColor = Color.White;
            datePicker.BorderColor = Color.FromArgb(200, 200, 200);
            datePicker.BorderThickness = 1;
            datePicker.BorderRadius = 8;

            // CANCEL BUTTON
            btnCancel.Location = new Point(1150, 400);
            btnCancel.Size = new Size(140, 50);
            btnCancel.Text = "Cancel";
            btnCancel.FillColor = Color.FromArgb(240, 240, 240);
            btnCancel.ForeColor = Color.Black;
            btnCancel.BorderColor = Color.Gray;
            btnCancel.BorderThickness = 1;
            btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            // ADD BUTTON
            btnAdd.Location = new Point(1300, 400);
            btnAdd.Size = new Size(180, 50);
            btnAdd.Text = "Add Category";
            btnAdd.FillColor = Color.FromArgb(0, 123, 255);
            btnAdd.ForeColor = Color.White;
            btnAdd.BorderRadius = 8;
            btnAdd.Click += btnAdd_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "AddCategory";
            Text = "Add Category";

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