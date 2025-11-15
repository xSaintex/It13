// ---------------------------------------------------------------------
// AddAttemptModal.designer.cs
// ---------------------------------------------------------------------
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class AddAttemptModal
    {
        private System.ComponentModel.IContainer components = null;
        private Guna2DateTimePicker dtpDate;
        private Guna2ComboBox cmbStatus;
        private Guna2TextBox txtRemarks;
        private Guna2Button btnCancel, btnSave;
        private Label lblHeader, lblDate, lblStatus, lblRemarks;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblHeader = new Label();
            lblDate = new Label(); dtpDate = new Guna2DateTimePicker();
            lblStatus = new Label(); cmbStatus = new Guna2ComboBox();
            lblRemarks = new Label(); txtRemarks = new Guna2TextBox();
            btnCancel = new Guna2Button(); btnSave = new Guna2Button();

            this.SuspendLayout();

            this.ClientSize = new Size(540, 460);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Padding = new Padding(35);

            lblHeader.Text = "Add Delivery Attempt";
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.Location = new Point(35, 25);
            lblHeader.AutoSize = true;

            lblDate.Text = "Attempt Date *";
            lblDate.Location = new Point(35, 80);
            lblDate.Font = new Font("Segoe UI", 10F);
            lblDate.AutoSize = true;

            dtpDate.Location = new Point(35, 105);
            dtpDate.Size = new Size(470, 44);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "MM/dd/yyyy";
            dtpDate.BorderRadius = 8;
            dtpDate.FillColor = Color.White;

            lblStatus.Text = "Status *";
            lblStatus.Location = new Point(35, 170);
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.AutoSize = true;

            cmbStatus.Location = new Point(35, 195);
            cmbStatus.Size = new Size(470, 44);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.BorderRadius = 8;
            cmbStatus.Items.AddRange(new[] { "Pending", "In Transit", "Delivered", "Failed" });

            lblRemarks.Text = "Remarks";
            lblRemarks.Location = new Point(35, 260);
            lblRemarks.Font = new Font("Segoe UI", 10F);
            lblRemarks.AutoSize = true;

            txtRemarks.Location = new Point(35, 285);
            txtRemarks.Size = new Size(470, 90);
            txtRemarks.Multiline = true;
            txtRemarks.BorderRadius = 8;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(310, 400);
            btnCancel.Size = new Size(90, 40);
            btnCancel.FillColor = Color.FromArgb(220, 53, 69);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 8;
            btnCancel.Click += btnCancel_Click;

            btnSave.Text = "Save";
            btnSave.Location = new Point(410, 400);
            btnSave.Size = new Size(90, 40);
            btnSave.FillColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.BorderRadius = 8;
            btnSave.Click += btnSave_Click;

            this.Controls.AddRange(new Control[] {
                lblHeader, lblDate, dtpDate, lblStatus, cmbStatus,
                lblRemarks, txtRemarks, btnCancel, btnSave
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}