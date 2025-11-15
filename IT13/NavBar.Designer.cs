using Guna.UI2.WinForms;

namespace IT13
{
    partial class NavBar
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlContainer = new Panel();
            this.lblTitle = new Label();
            this.pnlRight = new Panel();
            this.btnPOS = new Guna2Button();
            this.lblDate = new Label();
            this.picAdmin = new PictureBox();
            this.lblAdmin = new Label();

            this.pnlContainer.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAdmin)).BeginInit();
            this.SuspendLayout();

            // pnlContainer
            this.pnlContainer.BackColor = Color.White;
            this.pnlContainer.Dock = DockStyle.Fill;
            this.pnlContainer.Padding = new Padding(20, 10, 20, 10);
            this.pnlContainer.Controls.Add(this.lblTitle);
            this.pnlContainer.Controls.Add(this.pnlRight);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(30, 30, 60);
            this.lblTitle.Location = new Point(20, 22);
            this.lblTitle.Text = "Dashboard";

            // pnlRight — Perfect group with consistent 20px spacing
            this.pnlRight.Size = new Size(460, 80);
            this.pnlRight.Dock = DockStyle.Right;

            // btnPOS — First item
            this.btnPOS.FillColor = Color.FromArgb(0, 102, 204);
            this.btnPOS.ForeColor = Color.White;
            this.btnPOS.BorderRadius = 10;
            this.btnPOS.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPOS.Text = "POS";
            this.btnPOS.Size = new Size(85, 38);
            this.btnPOS.Location = new Point(20, 21);
            this.btnPOS.Cursor = Cursors.Hand;

            // lblDate — 20px after POS
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new Font("Segoe UI", 10F);
            this.lblDate.ForeColor = Color.Gray;
            this.lblDate.Location = new Point(125, 28);   // 85 + 20 = 105 → 125 (with padding)
            this.lblDate.Text = "November 15, 2025";

            // picAdmin — 20px after date
            this.picAdmin.Size = new Size(36, 36);
            this.picAdmin.Location = new Point(305, 22);  // 125 + 140 ≈ 265 → 285 (aligned)
            this.picAdmin.SizeMode = PictureBoxSizeMode.Zoom;
            this.picAdmin.Cursor = Cursors.Hand;
            this.picAdmin.Image = Properties.Resources.pfp_icon;

            // lblAdmin — 20px after icon
            this.lblAdmin.AutoSize = true;
            this.lblAdmin.Font = new Font("Segoe UI", 11F);
            this.lblAdmin.ForeColor = Color.FromArgb(60, 60, 60);
            this.lblAdmin.Text = "Admin";
            this.lblAdmin.Location = new Point(345, 28);  // 285 + 36 + 14 = ~335
            this.lblAdmin.Cursor = Cursors.Hand;

            this.pnlRight.Controls.Add(this.btnPOS);
            this.pnlRight.Controls.Add(this.lblDate);
            this.pnlRight.Controls.Add(this.picAdmin);
            this.pnlRight.Controls.Add(this.lblAdmin);

            // NavBar
            this.BackColor = Color.White;
            this.Controls.Add(this.pnlContainer);
            this.Size = new Size(1600, 80);

            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAdmin)).EndInit();
            this.ResumeLayout(false);
        }

        private Panel pnlContainer;
        private Label lblTitle;
        private Panel pnlRight;
        private Guna2Button btnPOS;
        private Label lblDate;
        private PictureBox picAdmin;
        private Label lblAdmin;
    }
}