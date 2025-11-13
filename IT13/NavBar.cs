using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace IT13
{
    public partial class NavBar : UserControl
    {
        private int _navHeight = 80;
        public NavBar()
        {
            InitializeComponent();
            ApplySize();
        }
        [Category("Appearance")]
        [Description("Height of the navbar")]
        [DefaultValue(80)]
        public int NavHeight
        {
            get => _navHeight;
            set { _navHeight = value; ApplySize(); }
        }
        [Category("Layout")]
        [Description("Width of the navbar (0 = auto full width)")]
        [DefaultValue(0)]
        public int NavWidth
        {
            get => this.Width;
            set { this.Width = value <= 0 ? this.Parent?.Width ?? 800 : value; ApplySize(); }
        }
        private void ApplySize()
        {
            this.Height = _navHeight;
            pnlContainer.Height = _navHeight;
            pnlContainer.Width = this.Width;
            int centerY = (_navHeight - lblTitle.Height) / 2;
            lblTitle.Top = centerY < 0 ? 0 : centerY;
            pnlUser.Top = centerY < 0 ? 0 : centerY;
            if (this.Parent != null)
                this.Width = this.Parent.Width;
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
        public string PageTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }
        public string UserName
        {
            get => lblUserName.Text;
            set => lblUserName.Text = value;
        }
        public Image UserImage
        {
            get => picUser.Image;
            set => picUser.Image = value;
        }
        public event EventHandler? UserProfileClicked;
        private void PicUser_Click(object sender, EventArgs e)
        {
            UserProfileClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}