using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class Inventory : Form
    {
        private DataTable dt = new DataTable();
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRecords = 1000;

        public Inventory()
        {
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            GenerateSampleData();
            LoadPage();
            StyleGrid();
        }

        private void SetupDataGridView()
        {
            dt.Columns.Add("SID", typeof(int));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("QTY", typeof(string));
            dt.Columns.Add("Unit Cost", typeof(string));
            dt.Columns.Add("Total Cost", typeof(string));
            dt.Columns.Add("Supplier", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Actions", typeof(string)); // placeholder

            dataGridView.DataSource = dt;

            // Hide Actions column (we'll draw icons manually)
            if (dataGridView.Columns["Actions"] != null)
                dataGridView.Columns["Actions"].Visible = false;
        }

        private void GenerateSampleData()
        {
            Random rand = new Random();
            string[] products = { "CCTV", "Laptop", "Mouse", "Keyboard", "Monitor", "Printer" };
            string[] categories = { "Category Nav", "Electronics", "Accessories" };
            string[] suppliers = { "Supplier Name", "TechCorp", "Global Supply" };
            string[] statuses = { "Active", "Inactive", "Low Stock", "No Stock", "Hold" };

            for (int i = 1; i <= totalRecords; i++)
            {
                dt.Rows.Add(
                    i,
                    products[rand.Next(products.Length)],
                    categories[rand.Next(categories.Length)],
                    rand.Next(0, 100) + " pcs.",
                    "P" + rand.Next(100, 5000).ToString("N2"),
                    "P" + (rand.Next(1000, 50000)).ToString("N2"),
                    suppliers[rand.Next(suppliers.Length)],
                    statuses[rand.Next(statuses.Length)],
                    "" // Actions
                );
            }
        }

        private void LoadPage()
        {
            int start = (currentPage - 1) * pageSize;
            // In real app, use SQL LIMIT/OFFSET or LINQ Skip/Take
            lblPageInfo.Text = $"Showing {start + 1}-{Math.Min(start + pageSize, totalRecords)} of {totalRecords}";
        }

        private void StyleGrid()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 87, 255);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, 87, 255);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView.CellPainting += DataGridView_CellPainting;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "Active": e.CellStyle.ForeColor = Color.Green; break;
                    case "Inactive": e.CellStyle.ForeColor = Color.Red; break;
                    case "Low Stock": e.CellStyle.ForeColor = Color.Orange; break;
                    case "No Stock": e.CellStyle.ForeColor = Color.Gray; break;
                    case "Hold": e.CellStyle.ForeColor = Color.RoyalBlue; break;
                }
                e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns["Actions"].Index)
            {
                e.PaintBackground(e.CellBounds, true);
                using (Brush brush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                {
                    int iconSize = 16;
                    int spacing = 20;
                    int startX = e.CellBounds.X + 10;

                    // View icon
                    Rectangle r1 = new Rectangle(startX, e.CellBounds.Y + 10, iconSize, iconSize);
                    e.Graphics.FillEllipse(brush, r1);
                    e.Graphics.DrawString("👁", new Font("Segoe UI Emoji", 9F), Brushes.White, r1, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                    // Edit icon
                    Rectangle r2 = new Rectangle(startX + spacing, e.CellBounds.Y + 10, iconSize, iconSize);
                    e.Graphics.FillEllipse(brush, r2);
                    e.Graphics.DrawString("✏", new Font("Segoe UI Emoji", 9F), Brushes.White, r2, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                    // Delete icon
                    Rectangle r3 = new Rectangle(startX + spacing * 2, e.CellBounds.Y + 10, iconSize, iconSize);
                    e.Graphics.FillEllipse(brush, r3);
                    e.Graphics.DrawString("🗑", new Font("Segoe UI Emoji", 9F), Brushes.White, r3, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                e.Handled = true;
            }
        }

        // Pagination buttons (you can enhance later)
        private void btnFirst_Click(object sender, EventArgs e) { currentPage = 1; LoadPage(); }
        private void btnPrev_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; LoadPage(); } }
        private void btnNext_Click(object sender, EventArgs e) { if (currentPage < totalRecords / pageSize) { currentPage++; LoadPage(); } }
        private void btnLast_Click(object sender, EventArgs e) { currentPage = totalRecords / pageSize; LoadPage(); }
    }
}