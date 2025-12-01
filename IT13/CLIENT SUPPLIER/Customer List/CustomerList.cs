using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class CustomerList : Form
    {
        private readonly Image _editIcon, _viewIcon;
        private bool? _headerCheckState = false;
        private string connectionString = @"Data Source=HONEYYYS\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";

        public CustomerList()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.DefaultCellStyle.SelectionBackColor = dgvCustomers.DefaultCellStyle.BackColor;
            dgvCustomers.DefaultCellStyle.SelectionForeColor = dgvCustomers.DefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn c in dgvCustomers.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvCustomers.RowTemplate.Height = 45;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.Columns["colID"].FillWeight = 8;
            dgvCustomers.Columns["colCompany"].FillWeight = 15;
            dgvCustomers.Columns["colContact"].FillWeight = 18;
            dgvCustomers.Columns["colPhone"].FillWeight = 16;
            dgvCustomers.Columns["colEmail"].FillWeight = 20;
            dgvCustomers.Columns["colPayment"].FillWeight = 12;
            dgvCustomers.Columns["colStatus"].FillWeight = 12;
            dgvCustomers.Columns["colActions"].FillWeight = 12;

            // Center align all columns except ID (which stays left because of checkbox)
            dgvCustomers.Columns["colID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCustomers.Columns["colCompany"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colContact"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colPayment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCustomers.Columns["colCompany"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvCustomers.Columns["colEmail"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            LoadCustomersFromDatabase();
            UpdateHeaderCheckState();
            dgvCustomers.ClearSelection();
        }

        private void LoadCustomersFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT CustID, CompanyName, ContactPerson, PhoneNo, Email, PaymentTerms, Status, FirstName, LastName
                        FROM customers
                        ORDER BY CustID ASC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvCustomers.Rows.Clear();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string fullName = GetFullName(row["FirstName"]?.ToString(), row["LastName"]?.ToString());
                        string contactPerson = !string.IsNullOrEmpty(row["ContactPerson"]?.ToString())
                            ? row["ContactPerson"].ToString()
                            : fullName;

                        AddRow(
                            row["CustID"].ToString(),
                            row["CompanyName"]?.ToString() ?? "N/A",
                            contactPerson,
                            row["PhoneNo"]?.ToString() ?? "N/A",
                            row["Email"]?.ToString() ?? "N/A",
                            row["PaymentTerms"]?.ToString() ?? "N/A",
                            row["Status"]?.ToString() ?? "Active"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleData();
            }
        }

        private string GetFullName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                return string.Empty;
            if (string.IsNullOrEmpty(firstName))
                return lastName;
            if (string.IsNullOrEmpty(lastName))
                return firstName;
            return $"{firstName} {lastName}";
        }

        private void LoadSampleData()
        {
            AddRow("1", "eme", "pamela mendoza", "09923654762", "fayloga@gmail.com", "Net 60", "Active");
            AddRow("2", "mskdejie", "Cyprus Basio", "09243596048", "Culaba@gmail.com", "Net 15", "Active");
            AddRow("3", "saehrhr", "Kaye Mayugba", "09246091759", "mendoza@gmail.com", "Net 15", "Active");
        }

        private void AddRow(string id, string company, string contact, string phone, string email, string payment, string status)
        {
            int idx = dgvCustomers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            // Store ONLY the numeric ID without any formatting
            dgvCustomers.Rows[idx].Cells[0].Tag = id.Trim();
            dgvCustomers.Rows[idx].Height = 45;
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false))
                        checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? (bool?)false :
                checkedCount == 0 ? false :
                checkedCount == visibleCount ? true : (bool?)null;

            dgvCustomers.InvalidateCell(0, -1);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearchAndFilter(txtSearch.Text.Trim());
        }

        private void ApplySearchAndFilter(string searchText)
        {
            string searchTrimmed = searchText.Trim();
            bool hasSearch = !string.IsNullOrEmpty(searchTrimmed);
            string filterStatus = Filter.SelectedItem?.ToString() ?? "Filter";

            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                if (row.IsNewRow) continue;

                // Get values from row
                string id = row.Cells[0].Tag?.ToString()?.Trim() ?? "";
                string company = row.Cells["colCompany"].Value?.ToString() ?? "";
                string contact = row.Cells["colContact"].Value?.ToString() ?? "";
                string phone = row.Cells["colPhone"].Value?.ToString() ?? "";
                string email = row.Cells["colEmail"].Value?.ToString() ?? "";
                string status = row.Cells["colStatus"].Value?.ToString() ?? "Active";

                bool matchesSearch = !hasSearch; // Default to true if no search

                if (hasSearch)
                {
                    // Check if search text is a number (for ID search)
                    bool isNumericSearch = searchTrimmed.All(char.IsDigit);

                    if (isNumericSearch)
                    {
                        // If searching for a number, ONLY match exact ID
                        matchesSearch = id == searchTrimmed;
                    }
                    else
                    {
                        // If searching for text, search in company, contact, phone, email
                        bool companyMatch = company.IndexOf(searchTrimmed, StringComparison.OrdinalIgnoreCase) >= 0;
                        bool contactMatch = contact.IndexOf(searchTrimmed, StringComparison.OrdinalIgnoreCase) >= 0;
                        bool phoneMatch = phone.Contains(searchTrimmed);
                        bool emailMatch = email.IndexOf(searchTrimmed, StringComparison.OrdinalIgnoreCase) >= 0;

                        matchesSearch = companyMatch || contactMatch || phoneMatch || emailMatch;
                    }
                }

                bool matchesFilter = filterStatus switch
                {
                    "All" => true,
                    "Active" => status.Equals("Active", StringComparison.OrdinalIgnoreCase),
                    "Inactive" => status.Equals("Inactive", StringComparison.OrdinalIgnoreCase),
                    _ => true
                };

                row.Visible = matchesSearch && matchesFilter;
            }

            UpdateHeaderCheckState();
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.AddRange(new object[] { "Filter", "All", "Active", "Inactive" });
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;

            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplySearchAndFilter(txtSearch.Text.Trim());
            };
        }

        private void SetupExportComboBox()
        {
            Export.Items.AddRange(new object[] { "Export", "Excel", "PDF", "CSV" });
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Gray;

            Export.SelectedIndexChanged += (s, e) =>
            {
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                if (Export.SelectedIndex > 0)
                {
                    ExportData(Export.SelectedItem.ToString());
                    Export.SelectedIndex = 0;
                }
            };
        }

        private void ExportData(string format)
        {
            MessageBox.Show($"Exporting to {format} format...", "Export",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void dgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Header checkbox + "ID"
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (_headerCheckState == true)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, r.X + 3, r.Y + 3, 10, 10);

                TextRenderer.DrawText(e.Graphics, "ID", new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                e.Handled = true;
                return;
            }

            // Other column headers - centered
            if (e.RowIndex == -1 && e.ColumnIndex > 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string headerText = dgvCustomers.Columns[e.ColumnIndex].HeaderText;
                TextRenderer.DrawText(e.Graphics, headerText, new Font("Poppins", 12F, FontStyle.Bold),
                    e.CellBounds, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            // Row checkbox + ID display
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 14, 16, 16);
                e.Graphics.FillRectangle(Brushes.White, r);
                e.Graphics.DrawRectangle(Pens.Black, r.X, r.Y, 15, 15);

                if (chk)
                {
                    using (Pen p = new Pen(Color.Black, 2.5f))
                        e.Graphics.DrawLines(p, new Point[] {
                            new Point(r.X + 3, r.Y + 8),
                            new Point(r.X + 7, r.Y + 12),
                            new Point(r.X + 13, r.Y + 5)
                        });
                }

                string id = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                {
                    TextRenderer.DrawText(e.Graphics, id, new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 40, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                }

                e.Handled = true;
                return;
            }

            // Actions column
            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int sz = 24, gap = 16, total = sz * 2 + gap;
                int x = e.CellBounds.X + (e.CellBounds.Width - total) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - sz) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, sz, sz);
                e.Graphics.DrawImage(_viewIcon, x + sz + gap, y, sz, sz);

                e.Handled = true;
                return;
            }

            // Status badge - centered
            if (e.ColumnIndex == dgvCustomers.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status == "Active" ? Color.FromArgb(34, 197, 94) : Color.FromArgb(239, 68, 68);

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 10,
                    e.CellBounds.Width - 20, e.CellBounds.Height - 20);

                using (var path = GetRoundedRect(rect, 8f))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                using (var font = new Font("Poppins", 10F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var size = e.Graphics.MeasureString(status, font);
                    float x = e.CellBounds.X + (e.CellBounds.Width - size.Width) / 2;
                    float y = e.CellBounds.Y + (e.CellBounds.Height - size.Height) / 2;
                    e.Graphics.DrawString(status, font, textBrush, x, y);
                }

                e.Handled = true;
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCustomers.CurrentCell = null;

            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvCustomers.Rows)
                    if (r.Visible && !r.IsNewRow)
                        r.Cells[0].Value = newState;

                _headerCheckState = newState ? true : (bool?)false;
                dgvCustomers.InvalidateCell(0, -1);
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvCustomers.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCustomers.Columns["colActions"].Index)
            {
                var cellRect = dgvCustomers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = dgvCustomers.PointToClient(Cursor.Position);
                int clickX = pt.X - cellRect.X;

                int sz = 24, gap = 16, total = sz * 2 + gap;
                int startX = (cellRect.Width - total) / 2;

                string id = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";

                if (clickX >= startX && clickX < startX + sz)
                    OpenEditCustomer(id);
                else if (clickX >= startX + sz + gap && clickX < startX + total)
                    OpenViewCustomer(id);
            }
        }

        private void OpenEditCustomer(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = "Edit Customer";

            var f = new EditCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void OpenViewCustomer(string id)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = $"View Customer: {id}";

            var f = new ViewCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var p = this.ParentForm as Form1;
            if (p == null) return;

            p.navBar1.PageTitle = "Add Customer";

            var f = new AddCustomerList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            p.pnlContent.Controls.Clear();
            p.pnlContent.Controls.Add(f);
            f.Show();
        }

        public void RefreshData()
        {
            LoadCustomersFromDatabase();
            ApplySearchAndFilter(txtSearch.Text.Trim());
        }
    }
}