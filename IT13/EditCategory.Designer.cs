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
            datePicker = new Guna2DateTimePicker();   // ← NEW
            btnCancel = new Guna2Button();
            btnUpdate = new Guna2Button();

            mainpanel.SuspendLayout();
            SuspendLayout();

            // MAIN PANEL
            mainpanel.BackColor = Color.Transparent;
            mainpanel.Controls.Add(lblTitle);
            mainpanel.Controls.Add(lblId); mainpanel.Controls.Add(txtId);
            mainpanel.Controls.Add(lblStatus); mainpanel.Controls.Add(comboStatus);
            mainpanel.Controls.Add(lblName); mainpanel.Controls.Add(txtName);
            mainpanel.Controls.Add(lblDate); mainpanel.Controls.Add(datePicker);
            mainpanel.Controls.Add(btnCancel); mainpanel.Controls.Add(btnUpdate);
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
            lblTitle.Text = "Edit Category";

            // ID
            lblId.AutoSize = true;
            lblId.Location = new Point(77, 120);
            lblId.Text = "Category ID *";
            txtId.Location = new Point(77, 150);
            txtId.Size = new Size(600, 45);
            txtId.ReadOnly = true;

            // STATUS
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(750, 120);
            lblStatus.Text = "Category Status *";
            comboStatus.Location = new Point(750, 150);
            comboStatus.Size = new Size(600, 45);
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStatus.Items.AddRange(new object[] { "Active", "Inactive" });

            // NAME
            lblName.AutoSize = true;
            lblName.Location = new Point(77, 220);
            lblName.Text = "Category Name *";
            txtName.Location = new Point(77, 250);
            txtName.Size = new Size(600, 45);

            // DATE – CALENDAR PICKER (WHITE BG)
            lblDate.AutoSize = true;
            lblDate.Location = new Point(750, 220);
            lblDate.Text = "Date *";

            datePicker.Location = new Point(750, 250);
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
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            // UPDATE BUTTON
            btnUpdate.Location = new Point(1300, 400);
            btnUpdate.Size = new Size(180, 50);
            btnUpdate.Text = "Update Category";
            btnUpdate.FillColor = Color.FromArgb(0, 123, 255);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.BorderRadius = 8;
            btnUpdate.Click += btnUpdate_Click;

            // FORM
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "EditCategory";
            Text = "Edit Category";

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
        private Guna2DateTimePicker datePicker;   // ← NEW
        private Guna2Button btnCancel;
        private Guna2Button btnUpdate;
    }
}