// ---------------------------------------------------------------------
// DeliveryVehicleList.cs – 100% IDENTICAL TO DeliveryList (checkboxes + style)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class DeliveryVehicleList : Form
    {
        private readonly Image _editIcon;
        private bool? _headerCheckState = false;

        public DeliveryVehicleList()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));

            SetupFilterComboBox();

            // Grid Settings - Match DeliveryList exactly
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

            LoadSampleData();
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
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
        }

        private void LoadSampleData()
        {
            AddRow("VH-001", "Toyota Hiace", "NCR 1234", "Active", "2024-01-15 10:30", "2025-03-22 14:20");
            AddRow("VH-002", "Isuzu Elf", "NCR 5678", "Active", "2024-02-20 09:15", "2025-02-28 11:45");
            AddRow("VH-003", "Mitsubishi L300", "NCR 9012", "Inactive", "2024-03-10 13:00", "2024-12-01 08:30");
            AddRow("VH-004", "Fuso Canter", "NCR 3456", "Active", "2024-05-18 11:00", "2025-01-10 09:15");
            AddRow("VH-005", "Hyundai Porter", "NCR 7890", "Maintenance", "2024-07-22 14:30", "2025-03-01 16:45");
        }

        private void AddRow(string id, string name, string plate, string status, string created, string updated)
        {
            int idx = dgvVehicles.Rows.Add(false, name, plate, status, created, updated, null);
            dgvVehicles.Rows[idx].Cells[0].Tag = id;
            dgvVehicles.Rows[idx].Height = 45;
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

        // EXACT SAME CHECKBOX PAINTING AS DELIVERYLIST
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

                string id = dgvVehicles.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    TextRenderer.DrawText(e.Graphics, id,
                        new Font("Bahnschrift SemiCondensed", 11F),
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

            // Edit Icon Only
            if (e.ColumnIndex == dgvVehicles.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 24) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 24, 24);
                e.Handled = true;
            }
        }

        // EXACT SAME CLICK BEHAVIOR AS DELIVERYLIST
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
                string id = dgvVehicles.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(id))
                    OpenEditVehicle(id);
            }
        }

        private void OpenEditVehicle(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Delivery Vehicle";
            var form = new EditVehicleList(id)
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
            string s = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow r in dgvVehicles.Rows)
            {
                if (r.IsNewRow) continue;
                string id = r.Cells[0].Tag?.ToString().ToLower() ?? "";
                string name = r.Cells["colVehicleName"].Value?.ToString().ToLower() ?? "";
                string plate = r.Cells["colPlateNumber"].Value?.ToString().ToLower() ?? "";
                r.Visible = string.IsNullOrEmpty(s) || id.Contains(s) || name.Contains(s) || plate.Contains(s);
            }
            UpdateHeaderCheckState();
        }
    }
}