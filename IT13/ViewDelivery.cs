// ---------------------------------------------------------------------
// ViewDelivery.cs - FINAL (0 ERRORS)
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace IT13
{
    public partial class ViewDelivery : Form
    {
        private readonly string _deliveryId;
        public ViewDelivery(string deliveryId)
        {
            _deliveryId = deliveryId;
            InitializeComponent();
            SetupTabs();
            SetupAttemptsGrid();
            LoadDeliveryData();
            ShowPanel(pnlAttempts, pnlDetails);
        }
        private void SetupTabs()
        {
            btnAttempts.Click += (s, e) => ShowPanel(pnlAttempts, pnlDetails);
            btnDetails.Click += (s, e) => ShowPanel(pnlDetails, pnlAttempts);
        }
        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide)
        {
            show.Visible = true;
            hide.Visible = false;
            btnAttempts.FillColor = show == pnlAttempts ? Color.FromArgb(0, 123, 255) : Color.FromArgb(240, 240, 240);
            btnDetails.FillColor = show == pnlDetails ? Color.FromArgb(0, 123, 255) : Color.FromArgb(240, 240, 240);
            btnAttempts.ForeColor = show == pnlAttempts ? Color.White : Color.Black;
            btnDetails.ForeColor = show == pnlDetails ? Color.White : Color.Black;
        }
        private void SetupAttemptsGrid()
        {
            dgvAttempts.Columns.Clear();
            dgvAttempts.Columns.Add("colNum", "#");
            dgvAttempts.Columns.Add("colDate", "Date");
            dgvAttempts.Columns.Add("colStatus", "Status");
            dgvAttempts.Columns.Add("colRemarks", "Remarks");
            dgvAttempts.Columns["colNum"].Width = 60;
            dgvAttempts.Columns["colDate"].Width = 150;
            dgvAttempts.Columns["colStatus"].Width = 120;
            dgvAttempts.Columns["colRemarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAttempts.AllowUserToAddRows = false;
            dgvAttempts.ReadOnly = true;
            dgvAttempts.RowTemplate.Height = 40;
            dgvAttempts.EnableHeadersVisualStyles = false;
            dgvAttempts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvAttempts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dgvAttempts.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvAttempts.BackgroundColor = Color.White;
            dgvAttempts.GridColor = Color.FromArgb(230, 230, 230);
            dgvAttempts.BorderStyle = BorderStyle.None;
            dgvAttempts.RowHeadersVisible = false;
            dgvAttempts.SelectionMode = DataGridViewSelectionMode.CellSelect; // ← Prevent row highlight
            dgvAttempts.ReadOnly = true;
            dgvAttempts.DefaultCellStyle.SelectionBackColor = dgvAttempts.DefaultCellStyle.BackColor;
            dgvAttempts.DefaultCellStyle.SelectionForeColor = dgvAttempts.DefaultCellStyle.ForeColor;
            dgvAttempts.Enabled = false; // ← FULLY UNCLICKABLE
        }
        private void LoadDeliveryData()
        {
            this.Text = $"Delivery #{_deliveryId}";
            lblCreated.Text = "Created: Oct 11, 2025 08:04 PM";
            if (_deliveryId == "DEL-001")
            {
                AddAttemptRow(1, "Oct 22, 2025", "Pending", "Initial delivery attempt created automatically.");
            }
        }
        private void AddAttemptRow(int num, string date, string status, string remarks)
        {
            int idx = dgvAttempts.Rows.Add(num, date, status, remarks);
            var row = dgvAttempts.Rows[idx];
            row.Cells["colStatus"].Style.BackColor = Color.FromArgb(255, 253, 230);
            row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(184, 134, 11);
            row.Cells["colStatus"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            row.Cells["colStatus"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void btnAddAttempt_Click(object sender, EventArgs e)
        {
            using (var modal = new AddAttemptModal())
            {
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    int num = dgvAttempts.Rows.Count + 1;
                    AddAttemptRow(num, modal.AttemptDate.ToString("MMM dd, yyyy"), modal.Status, modal.Remarks);
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Delivery List";
                parent.pnlContent.Controls.Clear();
                var list = new DeliveryList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                parent.pnlContent.Controls.Add(list);
                list.Show();
            }
        }
    }
}