using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
namespace IT13
{
    public partial class ViewStockAdjustment : Form
    {
        private readonly string _adjId;
        public ViewStockAdjustment(string adjustmentId)
        {
            _adjId = adjustmentId;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            txtId.Text = _adjId;
            txtItem.Text = "CCTV Camera Pro";
            txtRequested.Text = "John Doe";
            txtReviewed.Text = "Jane Smith";          // NEW
            txtAdjType.Text = "Increase";
            txtPhysical.Text = "55";
            txtSystem.Text = "50";
            txtAdjCount.Text = "5";
            txtStatus.Text = "Pending";
            txtReason.Text = "Found extra units during physical count.";
            datePicker.Value = new DateTime(2025, 11, 10);
        }

        private void btnBack_Click(object sender, EventArgs e) => ReturnToList();

        private void ReturnToList()
        {
            Form1 parent = this.ParentForm as Form1;
            if (parent == null) return;
            parent.navBar1.PageTitle = "Stock Adjustments";
            parent.pnlContent.Controls.Clear();
            parent.pnlContent.Controls.Add(new StockAdjustment
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            });
            parent.pnlContent.Controls[0].Show();
        }
    }
}