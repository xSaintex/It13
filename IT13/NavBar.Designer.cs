namespace IT13
{
    partial class NavBar
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        #region Component Designer generated code
        private void InitializeComponent()
        {
            pnlContainer = new Panel();
            lblTitle = new Label();
            pnlUser = new Panel();
            picArrow = new PictureBox();
            lblUserName = new Label();
            picUser = new PictureBox();
            pnlContainer.SuspendLayout();
            pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picArrow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            SuspendLayout();
            //
            // pnlContainer
            //
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(lblTitle);
            pnlContainer.Controls.Add(pnlUser);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20, 10, 30, 10);
            pnlContainer.Size = new Size(1871, 60);
            pnlContainer.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(120, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard";
            //
            // pnlUser
            //
            pnlUser.AutoSize = true;
            pnlUser.Controls.Add(picArrow);
            pnlUser.Controls.Add(lblUserName);
            pnlUser.Controls.Add(picUser);
            pnlUser.Dock = DockStyle.Right;
            pnlUser.Location = new Point(1782, 10);
            pnlUser.Name = "pnlUser";
            pnlUser.Padding = new Padding(0, 8, 0, 8);
            pnlUser.Size = new Size(59, 40);
            pnlUser.TabIndex = 1;
            //
            // picArrow
            //
            picArrow.Cursor = Cursors.Hand;
            picArrow.Location = new Point(0, 0);
            picArrow.Margin = new Padding(0, 18, 0, 0);
            picArrow.Name = "picArrow";
            picArrow.Size = new Size(12, 12);
            picArrow.SizeMode = PictureBoxSizeMode.Zoom;
            picArrow.TabIndex = 0;
            picArrow.TabStop = false;
            picArrow.Click += PicUser_Click;
            //
            // lblUserName
            //
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F);
            lblUserName.ForeColor = Color.FromArgb(0, 51, 102);
            lblUserName.Location = new Point(0, 0);
            lblUserName.Margin = new Padding(10, 12, 5, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(54, 23);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Guest";
            lblUserName.Click += PicUser_Click;
            //
            // picUser
            //
            picUser.BackColor = Color.FromArgb(0, 89, 179);
            picUser.Cursor = Cursors.Hand;
            picUser.Location = new Point(0, 0);
            picUser.Name = "picUser";
            picUser.Size = new Size(36, 36);
            picUser.SizeMode = PictureBoxSizeMode.Zoom;
            picUser.TabIndex = 2;
            picUser.TabStop = false;
            picUser.Click += PicUser_Click;
            //
            // NavBar
            //
            BackColor = Color.White;
            Controls.Add(pnlContainer);
            Name = "NavBar";
            Size = new Size(1871, 60);
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            pnlUser.ResumeLayout(false);
            pnlUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picArrow).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            ResumeLayout(false);
        }
        #endregion
        private Panel pnlContainer;
        private Label lblTitle;
        private Panel pnlUser;
        private PictureBox picUser;
        private Label lblUserName;
        private PictureBox picArrow;
    }
}