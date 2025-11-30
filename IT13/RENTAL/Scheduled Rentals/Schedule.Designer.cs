namespace IT13
{
    partial class Schedule
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.btnToday = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();

            this.SuspendLayout();

            this.lblTitle.Text = "Schedule Calendar";
            this.lblTitle.Font = new Font("Bahnschrift SemiCondensed", 32F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.Black;
            this.lblTitle.Location = new Point(300, 120);
            this.lblTitle.AutoSize = true;

            this.calendar.Location = new Point(120, 200);
            this.calendar.Size = new Size(1440, 690);
            this.calendar.TabIndex = 0;
            this.calendar.Paint += new PaintEventHandler(this.calendar_Paint);
            this.calendar.DateChanged += (s, e) => calendar.Invalidate();

            this.btnToday.Text = "Today";
            this.btnToday.Size = new Size(100, 38);
            this.btnToday.Location = new Point(1460, 150);
            this.btnToday.FillColor = Color.White;
            this.btnToday.BorderColor = Color.FromArgb(0, 123, 255);
            this.btnToday.BorderThickness = 2;
            this.btnToday.ForeColor = Color.FromArgb(0, 123, 255);
            this.btnToday.Font = new Font("Bahnschrift SemiCondensed", 11F, FontStyle.Regular);
            this.btnToday.BorderRadius = 8;

            this.ClientSize = new Size(1920, 1080);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.calendar);

            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Schedule";

            // ENABLE SCROLLING
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 1900);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        protected System.Windows.Forms.MonthCalendar calendar;
        protected Guna.UI2.WinForms.Guna2Button btnToday;
        protected Label lblTitle;
    }
}