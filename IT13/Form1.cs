using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // NAVBAR: SMALLER WIDTH + STARTS AFTER SIDEBAR
            navBar1.Dock = DockStyle.None; // no dock
            navBar1.Width = 1190; // SMALLER WIDTH (total 1450 - 260 sidebar)
            navBar1.Height = 70; // your height
            navBar1.Left = 260; // STARTS AFTER SIDEBAR
            navBar1.Top = 0; // at very top
            navBar1.Padding = new Padding(20, 0, 30, 0); // normal padding inside

            // DEFAULT TEXT
            navBar1.PageTitle = "Dashboard";
            navBar1.UserName = "John Doe";

            // SIDEBAR: FULL TOP TO BOTTOM
            sidebar1.Dock = DockStyle.Left;
            sidebar1.Width = 260;
            sidebar1.Height = this.ClientSize.Height; // full height
            sidebar1.BringToFront(); // on top of navbar

            // CONTENT: FILLS REMAINING SPACE
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Left = 260; // after sidebar
            pnlContent.Top = 70; // after navbar
            pnlContent.Width = 1190;
            pnlContent.Height = this.ClientSize.Height - 70;

            // AUTO-RESIZE ON WINDOW CHANGE
            this.Resize += (s, ev) =>
            {
                sidebar1.Height = this.ClientSize.Height;
                navBar1.Width = this.ClientSize.Width - 260;
                navBar1.Left = 260;
                pnlContent.Width = this.ClientSize.Width - 260;
                pnlContent.Height = this.ClientSize.Height - navBar1.Height;
            };

            // UPDATED: SidebarItemClicked now passes SidebarItemClickedEventArgs
            sidebar1.SidebarItemClicked += (s, ev) =>
            {
                navBar1.PageTitle = ev.Section;   // reads the real name
            };
        }
    }
}