using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class Schedule : Form
    {
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
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
                LoadBookingsForDate(DateTime.Today);
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

            // Load bookings from database
            var todaysBookings = LoadBookingsFromDatabase(date);

            pnlBookings.Controls.Clear();
            lblNoBooking.Visible = todaysBookings.Count == 0;

            if (todaysBookings.Count == 0) return;

            foreach (var booking in todaysBookings)
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
                    Text = $"Rental #{booking.RentalID} - {booking.BookingType}",
                    Font = new Font("Bahnschrift SemiCondensed", 22F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(35, 22),
                    AutoSize = true
                };

                var lblTime = new Label
                {
                    Text = $"{booking.ContactPerson} • {booking.ContactNumber}",
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
                    BorderRadius = 24,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Items = { "pending", "confirmed", "cancelled", "completed" },
                    SelectedItem = booking.Status
                };

                cmbStatus.HoverState.BorderColor = Color.FromArgb(0, 123, 255);
                cmbStatus.HoverState.FillColor = Color.FromArgb(245, 245, 255);

                // Store RentalID in Tag for update
                cmbStatus.Tag = booking.RentalID;

                cmbStatus.SelectedIndexChanged += async (s, e) =>
                {
                    var cmb = s as Guna2ComboBox;
                    if (cmb != null && cmb.Tag != null)
                    {
                        string newStatus = cmb.SelectedItem.ToString();
                        long rentalId = (long)cmb.Tag;
                        bool success = await UpdateBookingStatus(rentalId, newStatus);
                        if (success)
                        {
                            UpdateStatusColor(cmb, newStatus);
                        }
                        else
                        {
                            // Revert to previous selection
                            cmb.SelectedItem = booking.Status;
                        }
                    }
                };
                UpdateStatusColor(cmbStatus, booking.Status);

                card.Controls.AddRange(new Control[] { lblItem, lblTime, cmbStatus });
                pnlBookings.Controls.Add(card);
            }
        }

        private List<RentalBooking> LoadBookingsFromDatabase(DateTime date)
        {
            List<RentalBooking> bookings = new List<RentalBooking>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            r.RentalID,
                            r.booking_type,
                            r.status,
                            r.contact_person,
                            r.contact_number,
                            r.email,
                            c.FirstName + ' ' + c.LastName as CustomerName,
                            r.scheduled_date,
                            r.return_date
                        FROM rentals r
                        LEFT JOIN customers c ON r.CustomerID = c.CustID
                        WHERE r.scheduled_date = @SelectedDate
                        ORDER BY r.scheduled_date";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SelectedDate", date.Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var booking = new RentalBooking
                                {
                                    RentalID = Convert.ToInt64(reader["RentalID"]),
                                    BookingType = reader["booking_type"]?.ToString() ?? "Rental",
                                    Status = reader["status"]?.ToString() ?? "pending",
                                    ContactPerson = reader["contact_person"]?.ToString() ?? "",
                                    ContactNumber = reader["contact_number"]?.ToString() ?? "",
                                    Email = reader["email"]?.ToString() ?? "",
                                    CustomerName = reader["CustomerName"]?.ToString() ?? "",
                                    ScheduledDate = reader["scheduled_date"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["scheduled_date"]) : DateTime.MinValue,
                                    ReturnDate = reader["return_date"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["return_date"]) : DateTime.MinValue
                                };
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return bookings;
        }

        private async System.Threading.Tasks.Task<bool> UpdateBookingStatus(long rentalId, string newStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"
                        UPDATE rentals 
                        SET status = @Status, 
                            updated_at = GETDATE()
                        WHERE RentalID = @RentalID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@RentalID", rentalId);

                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UpdateStatusColor(Guna2ComboBox cmb, string status)
        {
            cmb.FillColor = status.ToLower() switch
            {
                "pending" => Color.FromArgb(255, 240, 180),
                "confirmed" => Color.FromArgb(180, 230, 255),
                "completed" => Color.FromArgb(180, 255, 220),
                "cancelled" => Color.FromArgb(255, 200, 200),
                _ => Color.White
            };
        }

        private void AddNewBooking(DateTime date)
        {
            var form = new Form
            {
                Text = "New Rental Booking",
                Size = new Size(420, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            // Customer selection
            var lblCustomer = new Label
            {
                Text = "Customer",
                Location = new Point(20, 20),
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black
            };

            var cmbCustomer = new Guna2ComboBox
            {
                Location = new Point(20, 55),
                Size = new Size(360, 48),
                BorderRadius = 14,
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black,
                FillColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Load customers
            LoadCustomersIntoComboBox(cmbCustomer);

            var lblBookingType = new Label
            {
                Text = "Booking Type",
                Location = new Point(20, 120),
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black
            };

            var cmbBookingType = new Guna2ComboBox
            {
                Location = new Point(20, 155),
                Size = new Size(360, 48),
                BorderRadius = 14,
                Font = new Font("Bahnschrift SemiCondensed", 15F),
                ForeColor = Color.Black,
                FillColor = Color.White,
                Items = { "Equipment Rental", "Vehicle Rental", "Service", "Other" },
                SelectedIndex = 0
            };

            var btnSave = new Guna2Button
            {
                Text = "Add Booking",
                Location = new Point(60, 290),
                Size = new Size(140, 52),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 16,
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold)
            };

            var btnCancel = new Guna2Button
            {
                Text = "Cancel",
                Location = new Point(220, 290),
                Size = new Size(140, 52),
                FillColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.Black,
                BorderColor = Color.LightGray,
                BorderThickness = 1,
                BorderRadius = 16,
                Font = new Font("Bahnschrift SemiCondensed", 14F, FontStyle.Bold)
            };

            btnSave.Click += async (s, e) =>
            {
                if (cmbCustomer.SelectedItem == null)
                {
                    MessageBox.Show("Please select a customer.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get customer ID from selected item
                var selectedItem = cmbCustomer.SelectedItem as ComboboxItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Invalid customer selection.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long customerId = (long)selectedItem.Value;

                bool success = await SaveBookingToDatabase(
                    customerId,
                    cmbBookingType.SelectedItem.ToString(),
                    date
                );

                if (success)
                {
                    MessageBox.Show("Booking saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookingsForDate(date);
                    form.Close();
                }
            };

            btnCancel.Click += (s, e) => form.Close();

            form.Controls.AddRange(new Control[] {
                lblCustomer, cmbCustomer,
                lblBookingType, cmbBookingType,
                btnSave, btnCancel
            });
            form.ShowDialog(this);
        }

        private void LoadCustomersIntoComboBox(Guna2ComboBox comboBox)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT CustID, FirstName + ' ' + LastName as FullName, CompanyName
                        FROM customers 
                        WHERE Status = 'Active'
                        ORDER BY FirstName, LastName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long custId = Convert.ToInt64(reader["CustID"]);
                            string fullName = reader["FullName"].ToString();
                            string company = reader["CompanyName"].ToString();
                            string displayText = !string.IsNullOrEmpty(company)
                                ? $"{fullName} ({company})"
                                : fullName;

                            comboBox.Items.Add(new ComboboxItem
                            {
                                Text = displayText,
                                Value = custId
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task<bool> SaveBookingToDatabase(long customerId, string bookingType, DateTime scheduledDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"
                        INSERT INTO rentals 
                        (CustomerID, booking_type, scheduled_date, status, 
                         contact_person, contact_number, email, subtotal, discount, service_fee, total)
                        VALUES 
                        (@CustomerID, @BookingType, @ScheduledDate, 'pending',
                         'TBD', 'TBD', 'TBD', 0.00, 0.00, 0.00, 0.00)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@BookingType", bookingType);
                        cmd.Parameters.AddWithValue("@ScheduledDate", scheduledDate);

                        int result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving booking: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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

    class RentalBooking
    {
        public long RentalID { get; set; }
        public string BookingType { get; set; }
        public string Status { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}