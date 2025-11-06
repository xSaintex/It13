namespace IT13
{
    public partial class Form1 : Form
    {
        private Panel contentPanel;

        public Form1()
        {
            InitializeComponent();

            // CREATE NAVBAR FIRST
            NavBar navbar = new NavBar();
            navbar.Name = "navbar";
            navbar.Dock = DockStyle.Top;
            navbar.Title = "Dashboard";
            navbar.UserName = "Administrator";
            this.Controls.Add(navbar);

            // CREATE SIDEBAR
            Sidebar sidebar = new Sidebar();
            sidebar.Name = "sidebar";
            sidebar.Dock = DockStyle.Left;
            this.Controls.Add(sidebar);

            // CREATE CONTENT PANEL (Center area)
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Load Dashboard at startup
            LoadForm(new Dashboard());

            // Handle Sidebar Click Events
            sidebar.SidebarItemClicked += Sidebar_SidebarItemClicked;
        }

        private void Sidebar_SidebarItemClicked(object sender, string section)
        {
            // Update NavBar title
            NavBar navbar = this.Controls["navbar"] as NavBar;
            if (navbar != null)
                navbar.Title = section;

            // Load selected page
            switch (section)
            {
                case "Dashboard":
                    LoadForm(new Dashboard());
                    break;
                case "Inventory":
                    LoadForm(new Inventory()); // Example future form
                    break;
                // Add more cases when forms are created...
                default:
                    MessageBox.Show("No form assigned yet for: " + section);
                    break;
            }
        }

        private void LoadForm(Form frm)
        {
            // Clear previous form
            contentPanel.Controls.Clear();

            // Configure new form to act as UserControl
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(frm);
            frm.Show();
        }
    }
}
