using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class SelectProductsModal : Form
    {
        public List<ProductRow> SelectedProducts { get; private set; } = new List<ProductRow>();
        private bool? _headerCheckState = false;

        public SelectProductsModal()
        {
            InitializeComponent();

            // === DISABLE ALL INTERACTION EXCEPT CHECKBOXES ===
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeColumns = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.MultiSelect = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Disable sorting & header clicks
            foreach (DataGridViewColumn col in dgvProducts.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Remove selection highlight completely
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                dgvProducts.ColumnHeadersDefaultCellStyle.BackColor;
            dgvProducts.DefaultCellStyle.SelectionBackColor =
                dgvProducts.DefaultCellStyle.BackColor;
            dgvProducts.DefaultCellStyle.SelectionForeColor =
                dgvProducts.DefaultCellStyle.ForeColor;

            // Events
            dgvProducts.CellPainting += dgvProducts_CellPainting;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellValueChanged += (s, e) => { if (e.ColumnIndex == 0) UpdateHeaderCheckState(); };
            txtSearch.TextChanged += txtSearch_TextChanged;

            LoadSampleProducts();
            UpdateHeaderCheckState();
        }

        private void LoadSampleProducts()
        {
            AddProduct("HikVision Camera", 10, 2000.00m);
            AddProduct("Logitech Mouse", 5, 200.00m);
            AddProduct("Dell Monitor", 3, 8000.00m);
            AddProduct("Samsung SSD 1TB", 8, 6500.00m);
            AddProduct("USB-C Hub 7-in-1", 15, 1200.00m);
        }

        private void AddProduct(string name, int available, decimal price)
        {
            int idx = dgvProducts.Rows.Add(false, name, 1, $"₱{price:F2}", available);
            dgvProducts.Rows[idx].Tag = new ProductRow
            {
                Name = name,
                Price = price,
                Available = available,
                Qty = 1
            };
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            SelectedProducts.Clear();
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells[0].Value is bool checkedVal && checkedVal)
                {
                    var product = (ProductRow)row.Tag;
                    product.Qty = Convert.ToInt32(row.Cells[2].Value);
                    SelectedProducts.Add(product);
                }
            }

            if (SelectedProducts.Count == 0)
            {
                MessageBox.Show("Select at least one product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                string name = row.Cells[1].Value?.ToString().ToLower() ?? "";
                row.Visible = string.IsNullOrEmpty(filter) || name.Contains(filter);
            }
            UpdateHeaderCheckState();
        }

        private void UpdateHeaderCheckState()
        {
            int checkedCount = 0;
            int visibleCount = 0;

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Visible)
                {
                    visibleCount++;
                    if (row.Cells[0].Value is bool b && b) checkedCount++;
                }
            }

            _headerCheckState = visibleCount == 0 ? false :
                                checkedCount == 0 ? false :
                                checkedCount == visibleCount ? true : (bool?)null;

            dgvProducts.InvalidateCell(0, -1);
        }

        // PERFECTLY CENTERED HEADER CHECKBOX
        private void dgvProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                e.PaintBackground(e.CellBounds, true);

                var checkRect = new Rectangle(
                    e.CellBounds.X + (e.CellBounds.Width - 16) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - 16) / 2,
                    16, 16);

                ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Normal);

                if (_headerCheckState == true)
                    ControlPaint.DrawCheckBox(e.Graphics, checkRect, ButtonState.Checked);
                else if (_headerCheckState == null)
                    e.Graphics.FillRectangle(Brushes.Gray, checkRect.X + 2, checkRect.Y + 2, 12, 12);

                e.Handled = true;
            }
        }

        // ONLY CHECKBOXES ARE CLICKABLE — EVERYTHING ELSE IGNORED
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return; // Only checkbox column allowed

            if (e.RowIndex == -1) // Header checkbox
            {
                var cellRect = dgvProducts.GetCellDisplayRectangle(0, -1, false);
                var mousePos = dgvProducts.PointToClient(Control.MousePosition);
                int clickX = mousePos.X - cellRect.X;
                int clickY = mousePos.Y - cellRect.Y;
                int centerX = cellRect.Width / 2;
                int centerY = cellRect.Height / 2;

                if (Math.Abs(clickX - centerX) <= 12 && Math.Abs(clickY - centerY) <= 12)
                {
                    bool newState = !_headerCheckState.GetValueOrDefault(true);
                    foreach (DataGridViewRow row in dgvProducts.Rows)
                        if (row.Visible) row.Cells[0].Value = newState;

                    UpdateHeaderCheckState();
                }
                return;
            }

            if (e.RowIndex >= 0) // Row checkbox
            {
                var row = dgvProducts.Rows[e.RowIndex];
                bool current = (bool)(row.Cells[0].Value ?? false);
                row.Cells[0].Value = !current;
                UpdateHeaderCheckState();
            }
        }
    }
}