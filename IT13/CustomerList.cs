// ---------------------------------------------------------------------
// CustomerList.cs - FINAL
// NO DELETE ICON | Only Edit + View
// Loads only on sidebar click
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class CustomerList : Form
    {
        private readonly Image _editIcon, _viewIcon;

        public CustomerList()
        {
            InitializeComponent();
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(16, 16));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(16, 16));
            SetupFilterComboBox();
            SetupExportComboBox();
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
            Filter.ForeColor = Color.Gray;
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
            Export.ForeColor = Color.Gray;
            Export.SelectedIndexChanged += (s, e) =>
            {
                Export.ForeColor = Export.SelectedIndex == 0 ? Color.Gray : Color.FromArgb(68, 88, 112);
            };
        }

        private void LoadSampleData()
        {
            AddRow("CUST-001", "ABC Corporation", "John Doe", "+63 905 123 4567", "john@abc.com", "Net 30", "Active");
            AddRow("CUST-002", "XYZ Trading", "Jane Smith", "+63 917 987 6543", "jane@xyz.com", "Cash", "Inactive");
            AddRow("CUST-003", "Global Mart", "Mike Tan", "+63 923 456 7890", "mike@global.com", "Net 60", "Active");
            AddRow("CUST-004", "Tech Depot", "Ana Cruz", "+63 912 345 6789", "ana@tech.com", "Net 15", "Active");
            AddRow("CUST-005", "Prime Supplies", "Luis Reyes", "+63 998 765 4321", "luis@prime.com", "Cash", "Inactive");
        }

        private void AddRow(string id, string company, string contact, string phone, string email, string payment, string status)
        {
            int idx = dgvCustomers.Rows.Add(false, company, contact, phone, email, payment, status, null);
            dgvCustomers.Rows[idx].Cells[0].Tag = id;
        }

        private void dgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Paint Checkbox + ID
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                bool isChecked = Convert.ToBoolean(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 8, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);
                string idText = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                var textSize = e.Graphics.MeasureString(idText, e.CellStyle.Font);
                var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);
                e.Graphics.DrawString(idText, e.CellStyle.Font, Brushes.Black, textRect);
                e.Handled = true;
            }

            // Paint Edit + View ONLY
            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int x = e.CellBounds.X + (e.CellBounds.Width - 40) / 2; // 2 icons = 40px
                int y = e.CellBounds.Y + (e.CellBounds.Height - 16) / 2;
                e.Graphics.DrawImage(_editIcon, x, y, 16, 16);
                e.Graphics.DrawImage(_viewIcon, x + 20, y, 16, 16);
                e.Handled = true;
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                var row = dgvCustomers.Rows[e.RowIndex];
                bool current = Convert.ToBoolean(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                dgvCustomers.InvalidateCell(0, e.RowIndex);
                return;
            }

            if (e.ColumnIndex == dgvCustomers.Columns["colActions"].Index)
            {
                var cellRect = dgvCustomers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mousePos = dgvCustomers.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int iconX = (cellRect.Width - 40) / 2;
                string custId = dgvCustomers.Rows[e.RowIndex].Cells[0].Tag.ToString();

                if (clickX >= iconX && clickX < iconX + 16)
                    OpenEditCustomer(custId);
                else if (clickX >= iconX + 20 && clickX < iconX + 36)
                    OpenViewCustomer(custId);
            }
        }

        private void OpenEditCustomer(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Edit Customer";
            var form = new EditCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void OpenViewCustomer(string id)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = $"View Customer: {id}";
            var form = new ViewCustomerList(id)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(form);
            form.Show();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Add Customer";
            var form = new AddCustomerList
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
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                bool match = string.IsNullOrEmpty(filter) ||
                    row.Cells[1].Value?.ToString().ToLower().Contains(filter) == true ||
                    row.Cells[3].Value?.ToString().ToLower().Contains(filter) == true ||
                    row.Cells[4].Value?.ToString().ToLower().Contains(filter) == true;
                row.Visible = match;
            }
        }
    }
}