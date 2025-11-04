using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
            this.Resize += NavBar_Resize;
            this.Load += NavBar_Load;
        }

        private void NavBar_Load(object sender, EventArgs e)
        {
            // Setup default styles on load
            picArrow.Image = CreateDownArrowIcon(Color.FromArgb(0, 51, 102));
            PositionControls();
        }

        private void NavBar_Resize(object sender, EventArgs e)
        {
            PositionControls();
        }

        private void PositionControls()
        {
            // Adjust elements to the right dynamically
            int marginRight = 160;
            picUser.Location = new Point(this.Width - marginRight, 13);
            lblUserName.Location = new Point(this.Width - marginRight + 32, 16);
            picArrow.Location = new Point(this.Width - 60, 18);
        }

        // Create ▼ arrow dynamically
        private Bitmap CreateDownArrowIcon(Color color)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Point[] triangle =
                {
                    new Point(3, 5),
                    new Point(13, 5),
                    new Point(8, 11)
                };
                using (Brush brush = new SolidBrush(color))
                {
                    g.FillPolygon(brush, triangle);
                }
            }
            return bmp;
        }

        // Public properties to easily change title & username
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string UserName
        {
            get => lblUserName.Text;
            set => lblUserName.Text = value;
        }
    }
}
