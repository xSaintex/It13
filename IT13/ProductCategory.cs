// ProductCategory.cs
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
            _editIcon = new Bitmap(Properties.Resources.edit_icon, new Size(16, 16));
            _viewIcon = new Bitmap(Properties.Resources.view_icon, new Size(16, 16));
            LoadSampleData();
        }

        private void txtboxsearch_TextChanged(object sender, EventArgs e) { }

        // CELL PAINTING – draws checkbox + ID + icons
        private void datagridviewcategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // ID + Checkbox in same cell
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                // Checkbox
                bool isChecked = (bool)(e.Value ?? false);
                var checkRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 8, 16, 16);
                ControlPaint.DrawCheckBox(e.Graphics, checkRect,
                    isChecked ? ButtonState.Checked : ButtonState.Normal);

                // ID text (from Tag)
                string idText = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag?.ToString() ?? "";
                var textSize = e.Graphics.MeasureString(idText, e.CellStyle.Font);
                var textRect = new Rectangle(
                    e.CellBounds.X + 30,
                    e.CellBounds.Y + (e.CellBounds.Height - (int)textSize.Height) / 2,
                    e.CellBounds.Width - 35,
                    e.CellBounds.Height);
                e.Graphics.DrawString(idText, e.CellStyle.Font, Brushes.Black, textRect);

                e.Handled = true;
            }

            // Edit + View icons
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                int x = e.CellBounds.X + (e.CellBounds.Width - 40) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - 16) / 2;

                e.Graphics.DrawImage(_editIcon, x, y, 16, 16);
                e.Graphics.DrawImage(_viewIcon, x + 20, y, 16, 16);

                e.Handled = true;
            }
        }

        // CELL CLICK – toggle checkbox + detect icon clicks
        private void datagridviewcategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Toggle checkbox
            if (e.ColumnIndex == 0)
            {
                var row = datagridviewcategory.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                datagridviewcategory.InvalidateCell(e.ColumnIndex, e.RowIndex);
                return;
            }

            // Actions: Edit or View
            if (e.ColumnIndex == datagridviewcategory.Columns["colActions"].Index)
            {
                // FIX: Use MouseEventArgs to get mouse position relative to cell
                var cellDisplayRect = datagridviewcategory.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var mouseEventArgs = datagridviewcategory.PointToClient(Control.MousePosition);
                int clickX = mouseEventArgs.X - cellDisplayRect.X;

                int x = (cellDisplayRect.Width - 40) / 2;

                string categoryId = datagridviewcategory.Rows[e.RowIndex].Cells[0].Tag.ToString();

                // Edit icon
                if (clickX >= x && clickX < x + 16)
                {
                    OpenEditCategory(categoryId);
                }
                // View icon
                else if (clickX >= x + 20 && clickX < x + 36)
                {
                    OpenViewProdCategory(categoryId);
                }
            }
        }

        private void OpenEditCategory(string categoryId)
        {
            Form1 parent = this.ParentForm as Form1;
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
            Form1 parent = this.ParentForm as Form1;
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
            Form1 parent = this.ParentForm as Form1;
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
            AddRow("1", "CCTV", "2025-04-05", "Active");
            AddRow("2", "Speaker", "2025-04-02", "Active");
            AddRow("3", "Dual Speaker", "2025-03-20", "Inactive");
        }

        private void AddRow(string id, string name, string date, string status)
        {
            int idx = datagridviewcategory.Rows.Add(false, name, date, status, null);
            var row = datagridviewcategory.Rows[idx];
            row.Cells[0].Tag = id;
        }
    }
}