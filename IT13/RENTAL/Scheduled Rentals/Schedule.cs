using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class Schedule : Form
    {
        private List<Booking> bookings = new List<Booking>();
        private FlowLayoutPanel pnlBookings;
        private Label lblBookingTitle;
        private Label lblNoBooking;

        public Schedule()
        {
            InitializeComponent();
            CustomizeCalendar();
            SetupBookingsSection();
            LoadBookingsForDate(DateTime.Today);
        }

        private void CustomizeCalendar()
        {
            calendar.TodayDate = DateTime.Today;
            calendar.SetDate(DateTime.Today);
            calendar.TitleBackColor = Color.White;
            calendar.TitleForeColor = Color.FromArgb(40, 40, 40);
            calendar.TrailingForeColor = Color.FromArgb(200, 200, 200);
            calendar.BackColor = Color.White;
            calendar.ForeColor = Color.Black;
            calendar.Font = new Font("Bahnschrift", 26F, FontStyle.Bold);

            btnToday.Click += (s, e) =>
            {
                calendar.SetDate(DateTime.Today);
                calendar.Invalidate();
            };

            calendar.DateChanged += (s, e) =>
            {
                calendar.Invalidate();
                LoadBookingsForDate(calendar.SelectionStart.Date);
            };

            calendar.MouseDown += (s, e) =>
            {
                var hit = calendar.HitTest(e.Location);
                if (hit.HitArea == MonthCalendar.HitArea.Date)
                {
                    DateTime selected = hit.Time.Date;
                    calendar.SetDate(selected);
                    AddNewBooking(selected);
                }
            };
        }

        private void SetupBookingsSection()
        {
            lblBookingTitle = new Label
            {
                Font = new Font("Bahnschrift SemiCondensed", 28F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(300, 940)
            };
            this.Controls.Add(lblBookingTitle);

            lblNoBooking = new Label
            {
                Text = "No bookings scheduled for this date",
                Font = new Font("Bahnschrift SemiCondensed", 18F),
                ForeColor = Color.FromArgb(130, 130, 130),
                AutoSize = true,
                Location = new Point(300, 1020),
                Visible = false
            };
            this.Controls.Add(lblNoBooking);

            pnlBookings = new FlowLayoutPanel
            {
                Location = new Point(300, 1060),
                Size = new Size(1320, 700),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlBookings);
        }

        private void LoadBookingsForDate(DateTime date)
        {
            lblBookingTitle.Text = $"Bookings for {date:dddd, MMMM d, yyyy}";

            var todays = bookings.Where(b => b.Date.Date == date.Date).ToList();

            pnlBookings.Controls.Clear();
            lblNoBooking.Visible = todays.Count == 0;

            if (todays.Count == 0) return;

            foreach (var b in todays)
            {
                var card = new Guna2Panel
                {
                    Size = new Size(1280, 100),
                    FillColor = Color.White,
                    BorderRadius = 20,
                    ShadowDecoration = { Enabled = true },
                    Margin = new Padding(0, 0, 0, 18)
                };

                var lblItem = new Label
                {
                    Text = b.Item,
                    Font = new Font("Bahnschrift SemiCondensed", 22F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(35, 22),
                    AutoSize = true
                };

                var lblTime = new Label
                {
                    Text = b.Time,
                    Font = new Font("Bahnschrift SemiCondensed", 16F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    Location = new Point(35, 58),
                    AutoSize = true
                };

                var cmbStatus = new Guna2ComboBox
                {
                    Location = new Point(980, 28),
                    Size = new Size(260, 48),
                    Font = new Font("Bahnschrift SemiCondensed", 14F),
                    ForeColor = Color.Black,
                    FillColor = Color.White,
                    BorderColor = Color.LightGray,
                    BorderThickness = 1,
                    BorderRadius = 24,  // Rounded field
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Items = { "Pending", "Confirmed", "In Progress", "Completed", "Cancelled" },
                    SelectedItem = b.Status
                };

                // Rounded dropdown (real Guna2 way - no invalid properties)
                cmbStatus.HoverState.BorderColor = Color.FromArgb(0, 123, 255);
                cmbStatus.HoverState.FillColor = Color.FromArgb(245, 245, 255);

                cmbStatus.SelectedIndexChanged += (s, e) =>
                {
                    b.Status = cmbStatus.SelectedItem.ToString();
                    UpdateStatusColor(cmbStatus, b.Status);
                };
                UpdateStatusColor(cmbStatus, b.Status);

                card.Controls.AddRange(new Control[] { lblItem, lblTime, cmbStatus });
                pnlBookings.Controls.Add(card);
            }
        }

        private void UpdateStatusColor(Guna2ComboBox cmb, string status)
        {
            cmb.FillColor = status switch
            {
                "Pending" => Color.FromArgb(255, 240, 180),
                "Confirmed" => Color.FromArgb(180, 230, 255),
                "In Progress" => Color.FromArgb(180, 255, 180),
                "Completed" => Color.FromArgb(180, 255, 220),
                "Cancelled" => Color.FromArgb(255, 200, 200),
                _ => Color.White
            };
        }

        private void AddNewBooking(DateTime date)
        {
            var f = new Form
            {
                Text = "New Booking",
                Size = new Size(420, 360),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            var lblItem = new Label { Text = "Item / Service", Location = new Point(20, 20), Font = new Font("Bahnschrift SemiCondensed", 15F), ForeColor = Color.Black };
            var txtItem = new Guna2TextBox
            {
                PlaceholderText = "Enter item or service name",
                Location = new Point(20, 55),
                Size = new Size(360, 48),
                BorderRadius = 14,
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black,
                FillColor = Color.White
            };

            var lblTime = new Label { Text = "Time", Location = new Point(20, 120), Font = new Font("Bahnschrift SemiCondensed", 15F), ForeColor = Color.Black };
            var cmbTime = new Guna2ComboBox
            {
                Location = new Point(20, 155),
                Size = new Size(360, 48),
                BorderRadius = 14,
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black,
                FillColor = Color.White,
                Items = { "09:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM" },
                SelectedIndex = 0
            };

            var btnSave = new Guna2Button
            {
                Text = "Add Booking",
                Location = new Point(60, 250),
                Size = new Size(140, 52),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 16,
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold)
            };

            var btnCancel = new Guna2Button
            {
                Text = "Cancel",
                Location = new Point(220, 250),
                Size = new Size(140, 52),
                FillColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.Black,
                BorderColor = Color.LightGray,
                BorderThickness = 1,
                BorderRadius = 16,
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold)
            };

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtItem.Text)) return;
                bookings.Add(new Booking
                {
                    Date = date,
                    Item = txtItem.Text.Trim(),
                    Time = cmbTime.Text,
                    Status = "Pending"
                });
                LoadBookingsForDate(date);
                f.Close();
            };

            btnCancel.Click += (s, e) => f.Close();

            f.Controls.AddRange(new Control[] { lblItem, txtItem, lblTime, cmbTime, btnSave, btnCancel });
            f.ShowDialog(this);
        }

        private void calendar_Paint(object sender, PaintEventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime monthStart = new DateTime(calendar.SelectionStart.Year, calendar.SelectionStart.Month, 1);

            if (today.Month != monthStart.Month) return;

            int firstDayOffset = ((int)monthStart.DayOfWeek - (int)DayOfWeek.Sunday + 7) % 7;
            int dayIndex = today.Day + firstDayOffset - 1;
            int row = dayIndex / 7;
            int col = dayIndex % 7;

            int cellW = 218;
            int cellH = 160;
            int startX = 140;
            int startY = 200;

            Rectangle cellRect = new Rectangle(startX + col * cellW, startY + row * cellH, cellW, cellH);

            using (var brush = new SolidBrush(Color.FromArgb(0, 123, 255)))
            using (var pen = new Pen(Color.FromArgb(0, 123, 255), 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int diameter = 140;
                Rectangle circleRect = new Rectangle(
                    cellRect.X + (cellRect.Width - diameter) / 2,
                    cellRect.Y + (cellRect.Height - diameter) / 2,
                    diameter, diameter);

                e.Graphics.FillEllipse(brush, circleRect);
                e.Graphics.DrawEllipse(pen, circleRect);

                using (var textBrush = new SolidBrush(Color.White))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(today.Day.ToString(),
                        new Font("Bahnschrift", 48F, FontStyle.Bold),
                        textBrush, cellRect, sf);
                }
            }
        }
    }

    class Booking
    {
        public DateTime Date { get; set; }
        public string Item { get; set; }
        public string Time { get; set; }
        public string Status { get; set; } = "Pending";
    }
}