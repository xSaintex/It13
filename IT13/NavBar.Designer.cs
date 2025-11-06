namespace IT13
{
    partial class NavBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavBar));
            lblTitle = new Label();
            picUser = new PictureBox();
            lblUserName = new Label();
            picArrow = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picArrow).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTitle.Location = new Point(49, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Inventory";
            lblTitle.Click += lblTitle_Click;
            // 
            // picUser
            // 
            picUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picUser.Image = (Image)resources.GetObject("picUser.Image");
            picUser.Location = new Point(1670, 13);
            picUser.Name = "picUser";
            picUser.Size = new Size(24, 24);
            picUser.SizeMode = PictureBoxSizeMode.Zoom;
            picUser.TabIndex = 1;
            picUser.TabStop = false;
            // 
            // lblUserName
            // 
            lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F);
            lblUserName.ForeColor = Color.FromArgb(0, 51, 102);
            lblUserName.Location = new Point(1520, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(46, 23);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "John";
            // 
            // picArrow
            // 
            picArrow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picArrow.Location = new Point(1520, 0);
            picArrow.Name = "picArrow";
            picArrow.Size = new Size(14, 14);
            picArrow.SizeMode = PictureBoxSizeMode.Zoom;
            picArrow.TabIndex = 3;
            picArrow.TabStop = false;
            // 
            // NavBar
            // 
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(picUser);
            Controls.Add(lblUserName);
            Controls.Add(picArrow);
            Name = "NavBar";
            Padding = new Padding(20, 5, 20, 5);
            Size = new Size(1520, 59);
            Load += NavBar_Load_1;
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)picArrow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox picArrow;
    }
}
