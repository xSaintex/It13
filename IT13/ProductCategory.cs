using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ProductCategory : Form
    {
        private readonly Image _editIcon;
        private readonly Image _viewIcon;

        public ProductCategory()
        {
            InitializeComponent();

            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(24, 24));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(24, 24));

            SetupFilterComboBox();
            SetupExportComboBox();

            // === READ-ONLY + NO INTERACTION ===
            datagridviewcategory.ReadOnly = true;
            datagridviewcategory.AllowUserToAddRows = false;
            datagridviewcategory.AllowUserToDeleteRows = false;
            datagridviewcategory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridviewcategory.MultiSelect = false;

            // DISABLE COLUMN SORTING
            foreach (DataGridViewColumn col in datagridviewcategory.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // DISABLE HEADER VISUAL FEEDBACK (NO CLICK, NO HOVER)
            datagridviewcategory.EnableHeadersVisualStyles = false;
            datagridviewcategory.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                datagridviewcategory.ColumnHeadersDefaultCellStyle.BackColor;

            // NO ROW SELECTION HIGHLIGHT
            datagridviewcategory.DefaultCellStyle.SelectionBackColor =
                datagridviewcategory.DefaultCellStyle.BackColor;
            datagridviewcategory.DefaultCellStyle.SelectionForeColor =
                datagridviewcategory.DefaultCellStyle.ForeColor;

            // BIGGER FONT + TALL ROWS
            datagridviewcategory.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            datagridviewcategory.RowTemplate.Height = 45;
            datagridviewcategory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // AUTO-FILL COLUMNS
            datagridviewcategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridviewcategory.Columns["colID"].FillWeight = 15;
            datagridviewcategory.Columns["colName"].FillWeight = 45;
            datagridviewcategory.Columns["colDate"].FillWeight = 15;
            datagridviewcategory.Columns["colStatus"].FillWeight = 12;
            datagridviewcategory.Columns["colActions"].FillWeight = 13;

            datagridviewcategory.Columns["colName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadSampleData();
        }

        private void SetupFilterComboBox()
        {
            Filter.Items.Clear();
            Filter.Items.Add("Filter");
            Filter.Items.Add("All");
            Filter.Items.Add("Active");
            Filter.Items.Add("Inactive");
            Filter.SelectedIndex = 0;
            Filter.ForeColor = Color.Black;
            Filter.SelectedIndexChanged += (s, e) =>
            {
                Filter.ForeColor = Filter.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            };
        }

        private void SetupExportComboBox()
        {
            Export.Items.Clear();
            Export.Items.Add("Export");
            Export.Items.Add("Excel");
            Export.Items.Add("PDF");
            Export.Items.Add("CSV");
            Export.SelectedIndex = 0;
            Export.ForeColor = Color.Black;
            Export.SelectedIndexChanged += (s, e) =>
            {
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            };
        }

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtboxsearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in datagridviewcategory.Rows)
            {
                bool match = string.IsNullOrEmpty(filter) ||
                    row.Cells["colName"].Value?.ToString().ToLower().Contains(filter) == true ||
                    row.Cells["colID"].Tag?.ToString().ToLower().Contains(filter) == true;
                row.Visible = match;
            }
        }

        // FIXED: ID HEADER + ID TEXT VISIBLE
        private void datagridviewcategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Skip header row
            if (e.RowIndex < 0) return;

            // === CUSTOM PAINT: colID (Checkbox + ID Text) ===
            if (e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);

                // Draw checkbox
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 12, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                // Draw ID from Tag
                string idText = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                if (!string.IsNullOrEmpty(idText))
                {
                    var textSize = e.Graphics.MeasureString(idText, new Font("Segoe UI", 11F));
                    var textRect = new Rectangle(
                        e.CellBounds.X + 30,
                        e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                        e.CellBounds.Width - 35,
                        e.CellBounds.Height);
                    e.Graphics.DrawString(idText, new Font("Segoe UI", 11F), Brushes.Black, textRect);
                }

                e.Handled = true;
                return;
            }

            // === CUSTOM PAINT: colActions (Edit + View Icons) ===
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);

                // Total width for icons: 24 + 16 + 24 = 64px
                int totalWidth = 64;
                int x = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 24) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, 24, 24);
                e.Graphics.DrawImage(_viewIcon, x + 24 + 16, y, 24, 24); // 16px gap

                e.Handled = true;
                return;
            }

            // Let default paint handle all other cells (headers, normal text)
            e.Handled = false;
        }

        private void datagridviewcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Allow checkbox toggle
            if (e.ColumnIndex == 0)
            {
                var row = datagridviewcategory.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                datagridviewcategory.InvalidateCell(0, e.RowIndex);
                return;
            }

            // Only allow icon clicks in Actions column
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                var cellRect = datagridviewcategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = datagridviewcategory.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;

                int totalWidth = 64;
                int iconX = (cellRect.Width - totalWidth) / 2;

                string categoryId = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + 24)
                    OpenEditCategory(categoryId);
                else if (clickX >= iconX + 40 && clickX < iconX + 64) // 24 + 16 gap
                    OpenViewProdCategory(categoryId);
            }
        }

        private void OpenEditCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Edit Category";
            var editForm = new EditCategory(categoryId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(editForm);
            editForm.Show();
        }

        private void OpenViewProdCategory(string categoryId)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "View Category Details";
            var viewForm = new ViewProdCategory(categoryId)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(viewForm);
            viewForm.Show();
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;

            parent.navBar1.PageTitle = "Add Category";
            var addForm = new AddCategory
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(addForm);
            addForm.Show();
        }

        private void LoadSampleData()
        {
            AddRow("CAT-001", "High-Resolution CCTV Security Camera System", "2025-04-05", "Active");
            AddRow("CAT-002", "Wireless Bluetooth Speaker with Subwoofer", "2025-04-02", "Active");
            AddRow("CAT-003", "Dual Stereo Speaker Set for Home Theater", "2025-03-20", "Inactive");
            AddRow("CAT-004", "Portable Mini Speaker with LED Lights", "2025-03-15", "Active");
            AddRow("CAT-005", "Professional Studio Monitor Speakers", "2025-02-28", "Active");
        }

        private void AddRow(string id, string name, string date, string status)
        {
            int idx = datagridviewcategory.Rows.Add(false, name, date, status, null);
            var row = datagridviewcategory.Rows[idx];
            row.Cells[0].Tag = id;
            row.Height = 45;
        }
    }
}