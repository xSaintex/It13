namespace IT13
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            sidebar1 = new Sidebar();
            navBar1 = new NavBar();
            pnlContent = new Panel();
            SuspendLayout();
            // 
            // sidebar1
            // 
            sidebar1.BackColor = Color.White;
            sidebar1.Location = new Point(0, 0);
            sidebar1.Margin = new Padding(0);
            sidebar1.Name = "sidebar1";
            sidebar1.Size = new Size(260, 700);
            sidebar1.TabIndex = 1;
            // 
            // navBar1
            // 
            navBar1.BackColor = Color.White;
            navBar1.Dock = DockStyle.Top;
            navBar1.Location = new Point(0, 0);
            navBar1.Name = "navBar1";
            navBar1.NavWidth = 1857;
            navBar1.PageTitle = "Dashboard";
            navBar1.Size = new Size(1857, 70);
            navBar1.TabIndex = 2;
            navBar1.UserImage = null;
            navBar1.UserName = "Guest";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(245, 247, 250);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1839, 963);
            pnlContent.TabIndex = 0;
            // 
            // Form1
            // 
            ClientSize = new Size(1839, 963);
            Controls.Add(navBar1);
            Controls.Add(sidebar1);
            Controls.Add(pnlContent);
            MinimumSize = new Size(1200, 700);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LE PARISIEN - Inventory System";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ResumeLayout(false);
        }
        #endregion
        private Sidebar sidebar1;
        public NavBar navBar1;      // Changed to public
        public Panel pnlContent;    // Changed to public
    }
}