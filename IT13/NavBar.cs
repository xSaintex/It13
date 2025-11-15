using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class NavBar : UserControl
    {
        private readonly System.Windows.Forms.Timer _dateTimer;
        private int _navHeight = 80;

        public NavBar()
        {
            InitializeComponent();

            _dateTimer = new System.Windows.Forms.Timer { Interval = 60000 };
            _dateTimer.Tick += (s, e) => lblDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
            _dateTimer.Start();
            lblDate.Text = DateTime.Now.ToString("MMMM d, yyyy");

            ApplySize();

            btnPOS.Click += (s, e) => POSClicked?.Invoke(this, EventArgs.Empty);
            picAdmin.Click += (s, e) => AdminClicked?.Invoke(this, EventArgs.Empty);
            lblAdmin.Click += (s, e) => AdminClicked?.Invoke(this, EventArgs.Empty);
        }

        [Category("Appearance")]
        [DefaultValue(80)]
        public int NavHeight
        {
            get => _navHeight;
            set { _navHeight = value; ApplySize(); }
        }

        public string PageTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string UserName
        {
            get => lblAdmin.Text;
            set => lblAdmin.Text = value;
        }

        public event EventHandler POSClicked;
        public event EventHandler AdminClicked;

        public void ApplySize()
        {
            this.Height = _navHeight;
            pnlContainer.Height = _navHeight;

            if (Parent != null)
            {
                int available = Parent.ClientSize.Width - 260;
                this.Width = available < 1000 ? 1000 : available;
                this.Left = 260;
                this.Top = 0;
            }

            pnlContainer.Width = this.Width;

            // 30px margin from right edge
            pnlRight.Left = this.Width - pnlRight.Width - 70;

            // Vertical centering
            int centerY = (_navHeight - lblTitle.Height) / 2;
            lblTitle.Top = centerY < 15 ? 22 : centerY;
            pnlRight.Top = centerY < 10 ? 18 : centerY;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplySize();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            ApplySize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dateTimer?.Stop();
                _dateTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}