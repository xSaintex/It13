using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class Sidebar : UserControl
    {
        private Button activeButton;

        public Sidebar()
        {
            InitializeComponent();
            RegisterButtonEvents();
        }

        private void RegisterButtonEvents()
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnHelp)
                    btn.Click += SidebarButton_Click;
            }
        }

        private void SidebarButton_Click(object sender, EventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = Color.White;
                activeButton.ForeColor = Color.FromArgb(40, 40, 40);
            }

            activeButton = sender as Button;
            activeButton.BackColor = Color.FromArgb(230, 240, 255);
            activeButton.ForeColor = Color.FromArgb(0, 90, 255);

            string section = activeButton.Text.Trim();
            SidebarItemClicked?.Invoke(this, section);
        }

        public event EventHandler<string> SidebarItemClicked;
    }
}
