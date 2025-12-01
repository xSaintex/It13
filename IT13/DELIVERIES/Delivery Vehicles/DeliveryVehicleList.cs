using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class DeliveryVehicleList : Form
    {
        private readonly string _connectionString = "Data Source=HONEYYYS\\SQLEXPRESS01;Initial Catalog=IT13;Integrated Security=True;TrustServerCertificate=True";
        private readonly Image _editIcon;
        private bool? _headerCheckState = false;

        public DeliveryVehicleList()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));

            SetupFilterComboBox();

            // Grid Settings
            dgvVehicles.ReadOnly = true;
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvVehicles.MultiSelect = false;
            dgvVehicles.RowHeadersVisible = false;
            dgvVehicles.EnableHeadersVisualStyles = false;

            dgvVehicles.DefaultCellStyle.SelectionBackColor = dgvVehicles.DefaultCellStyle.BackColor;
            dgvVehicles.DefaultCellStyle.SelectionForeColor = dgvVehicles.DefaultCellStyle.ForeColor;
            dgvVehicles.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 11F);
            dgvVehicles.RowTemplate.Height = 45;
            dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn c in dgvVehicles.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Column setup
            dgvVehicles.Columns["colID"].MinimumWidth = 160;
            dgvVehicles.Columns["colID"].FillWeight = 12;
            dgvVehicles.Columns["colVehicleName"].FillWeight = 28;
            dgvVehicles.Columns["colPlateNumber"].FillWeight = 18;
            dgvVehicles.Columns["colStatus"].FillWeight = 12;
            dgvVehicles.Columns["colCreatedAt"].FillWeight = 15;
            dgvVehicles.Columns["colUpdatedAt"].FillWeight = 15;
            dgvVehicles.Columns["colActions"].FillWeight = 12;

            dgvVehicles.Columns["colPlateNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colCreatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colUpdatedAt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVehicles.Columns["colActions"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Add TextChanged event for real-time search
            txtSearch.TextChanged += (s, e) => ApplySearchAndFilter();

            LoadVehiclesFromDatabase();
            UpdateHeaderCheckState();
            dgvVehicles.ClearSelection();
            dgvVehicles.CurrentCell = null;
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Active");
            Filter.Items.Add("Inactive");
            Filter.Items.Add("Maintenance");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Gray;
            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
                ApplySearchAndFilter();
            };
        }

        private void LoadVehiclesFromDatabase()
        {
            dgvVehicles.Rows.Clear();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"SELECT id, VehicleName, LicensePlate, Status, 
                                    created_at, updated_at 
                                    FROM vehicles 
                                    ORDER BY id DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = "VH-" + reader["id"].ToString().PadLeft(3, '0');
                            string vehicleName = reader["VehicleName"].ToString();
                            string licensePlate = reader["LicensePlate"].ToString();
                            string status = reader["Status"].ToString();
                            string createdAt = Convert.ToDateTime(reader["created_at"]).ToString("yyyy-MM-dd HH:mm");
                            string updatedAt = Convert.ToDateTime(reader["updated_at"]).ToString("yyyy-MM-dd HH:mm");

                            AddRow(id, vehicleName, licensePlate, status, createdAt, updatedAt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string displayId, string name, string plate, string status, string created, string updated)
        {
            int idx = dgvVehicles.Rows.Add(false, name, plate, status, created, updated, null);
            // Store both display ID and numeric ID
            dgvVehicles.Rows[idx].Cells[0].Tag = new { DisplayId = displayId, NumericId = displayId.Replace("VH-", "").TrimStart('0') };
            dgvVehicles.Rows[idx].Height = 45;
        }

        private void ApplyFilter()
        {
            ApplySearchAndFilter();
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0, visibleCount = 0;
            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.Visible && !row.IsNewRow)
                {
                    visibleCount++;
                    if ((bool)(row.Cells[0].Value ?? false)) checkedCount++;
                }
            }
            _headerCheckState = visibleCount == 0 ? false :
                               checkedCount == 0 ? false :
                               checkedCount == visibleCount ? true : (bool?)null;
            dgvVehicles.InvalidateCell(0, -1);
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

        private void dgvVehicles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                TextRenderer.DrawText(e.Graphics, "ID",
                    new Font("Bahnschrift SemiCondensed", 12F, FontStyle.Bold),
                    new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                    Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                e.Handled = true;
                return;
            }

            // Row checkbox + ID text
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool chk = (bool)(e.Value ?? false);
                var r = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 12, 16, 16);
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

                // Get display ID from Tag
                string displayId = "";
                var tagData = dgvVehicles.Rows[e.RowIndex].Cells[0].Tag;
                if (tagData != null)
                {
                    var props = tagData.GetType().GetProperty("DisplayId");
                    if (props != null)
                        displayId = props.GetValue(tagData)?.ToString() ?? "";
                }

                if (!string.IsNullOrEmpty(displayId))
                    TextRenderer.DrawText(e.Graphics, displayId,
                        new Font("Poppins", 11F),
                        new Rectangle(e.CellBounds.X + 36, e.CellBounds.Y, e.CellBounds.Width - 36, e.CellBounds.Height),
                        Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                e.Handled = true;
                return;
            }

            // Status Badge
            if (e.ColumnIndex == dgvVehicles.Columns["colStatus"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                string status = e.Value?.ToString() ?? "";
                Color bg = status.ToLower() switch
                {
                    "active" => Color.FromArgb(34, 197, 94),
                    "inactive" => Color.FromArgb(108, 117, 125),
                    "maintenance" => Color.FromArgb(255, 193, 7),
                    _ => Color.Gray
                };

                var rect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 8, e.CellBounds.Width - 20, e.CellBounds.Height - 16);
                using (var path = GetRoundedRect(rect, 10f))
                using (var br = new SolidBrush(bg))
                    e.Graphics.FillPath(br, path);

                using (var f = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold))
                using (var br = new SolidBrush(Color.White))
                {
                    var sz = e.Graphics.MeasureString(status, f);
                    e.Graphics.DrawString(status, f, br,
                        e.CellBounds.X + (e.CellBounds.Width - sz.Width) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - sz.Height) / 2);
                }
                e.Handled = true;
                return;
            }

            // Edit Icon
            if (e.ColumnIndex == dgvVehicles.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 24) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 24, 24);
                e.Handled = true;
            }
        }

        private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvVehicles.CurrentCell = null;

            // Header checkbox
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                bool newState = !(_headerCheckState == true);
                foreach (DataGridViewRow r in dgvVehicles.Rows)
                    if (r.Visible && !r.IsNewRow)
                        r.Cells[0].Value = newState;

                _headerCheckState = newState ? true : (bool?)false;
                dgvVehicles.InvalidateCell(0, -1);
                return;
            }

            // Row checkbox
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var row = dgvVehicles.Rows[e.RowIndex];
                row.Cells[0].Value = !(bool)(row.Cells[0].Value ?? false);
                UpdateHeaderCheckState();
                return;
            }

            // Edit click
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvVehicles.Columns["colActions"].Index)
            {
                // Get display ID from Tag
                string displayId = "";
                var tagData = dgvVehicles.Rows[e.RowIndex].Cells[0].Tag;
                if (tagData != null)
                {
                    var props = tagData.GetType().GetProperty("DisplayId");
                    if (props != null)
                        displayId = props.GetValue(tagData)?.ToString() ?? "";
                }

                if (!string.IsNullOrEmpty(displayId))
                    OpenEditVehicle(displayId);
            }
        }

        private void OpenEditVehicle(string displayId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            // Find the row with this display ID and get numeric ID
            string numericId = "";
            foreach (DataGridViewRow row in dgvVehicles.Rows)
            {
                if (row.IsNewRow) continue;
                var tagData = row.Cells[0].Tag;
                if (tagData != null)
                {
                    var displayProp = tagData.GetType().GetProperty("DisplayId");
                    var numericProp = tagData.GetType().GetProperty("NumericId");

                    if (displayProp != null && numericProp != null)
                    {
                        string rowDisplayId = displayProp.GetValue(tagData)?.ToString() ?? "";
                        if (rowDisplayId == displayId)
                        {
                            numericId = numericProp.GetValue(tagData)?.ToString() ?? "";
                            break;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(numericId))
            {
                MessageBox.Show("Unable to load vehicle details.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            parent.navBar1.PageTitle = "Edit Delivery Vehicle";
            var form = new EditVehicleList(numericId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Delivery Vehicle";
            var form = new AddVehicleList
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearchAndFilter();
        }

        private void ApplySearchAndFilter()
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            string filterStatus = Filter.SelectedIndex > 1 ? Filter.SelectedItem.ToString() : "";

            foreach (DataGridViewRow r in dgvVehicles.Rows)
            {
                if (r.IsNewRow) continue;

                // Get display ID from Tag
                string id = "";
                var tagData = r.Cells[0].Tag;
                if (tagData != null)
                {
                    var props = tagData.GetType().GetProperty("DisplayId");
                    if (props != null)
                        id = props.GetValue(tagData)?.ToString().ToLower() ?? "";
                }

                string name = r.Cells["colVehicleName"].Value?.ToString().ToLower() ?? "";
                string plate = r.Cells["colPlateNumber"].Value?.ToString().ToLower() ?? "";
                string status = r.Cells["colStatus"].Value?.ToString() ?? "";

                // Check search criteria
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                    id.Contains(searchText) ||
                                    name.Contains(searchText) ||
                                    plate.Contains(searchText);

                // Check filter criteria
                bool matchesFilter = string.IsNullOrEmpty(filterStatus) ||
                                    status.Equals(filterStatus, StringComparison.OrdinalIgnoreCase);

                r.Visible = matchesSearch && matchesFilter;
            }
            UpdateHeaderCheckState();
        }

        public void RefreshData()
        {
            LoadVehiclesFromDatabase();
            UpdateHeaderCheckState();
        }
    }
}