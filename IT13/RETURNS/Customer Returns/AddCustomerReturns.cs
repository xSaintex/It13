using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCustomerReturns : Form
    {
        public AddCustomerReturns()
        {
            InitializeComponent();
            SetupControls();
            WireEvents();
            ShowPanel(pnlCustomerOrder, pnlAddress, pnlReturns);
        }

        private void SetupControls()
        {
            cmbCustomerOrderID.Items.AddRange(new[] { "ORD-2025-001", "ORD-2025-002", "ORD-2025-003" });
            cmbPaymentTerms.Items.AddRange(new[] { "Cash", "Credit Card", "Bank Transfer", "Net 30" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Processing", "Completed", "Refunded" });
            cmbReturnType.Items.AddRange(new[]
            {
                "Defective Product", "Wrong Item", "No Longer Needed", "Damaged in Transit", "Other"
            });

            dtpReturnDate.Value = DateTime.Today;
            dtpReturnDate.Format = DateTimePickerFormat.Custom;
            dtpReturnDate.CustomFormat = "MMMM dd, yyyy";

            // Clean grid style
            dgvOrderItems.EnableHeadersVisualStyles = false;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvOrderItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvOrderItems.DefaultCellStyle.SelectionBackColor = dgvOrderItems.DefaultCellStyle.BackColor;
            dgvOrderItems.DefaultCellStyle.SelectionForeColor = dgvOrderItems.DefaultCellStyle.ForeColor;
        }

        private void WireEvents()
        {
            btnCustomerOrder.Click += (s, e) => ShowPanel(pnlCustomerOrder, pnlAddress, pnlReturns);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlCustomerOrder, pnlReturns);
            btnReturns.Click += (s, e) => ShowPanel(pnlReturns, pnlCustomerOrder, pnlAddress);

            btnSaveCustomerOrder.Click += (s, e) => SaveCustomerReturn();
            btnSaveAddress.Click += (s, e) => SaveCustomerReturn();
            btnSaveReturns.Click += (s, e) => SaveCustomerReturn();

            lnkBack.LinkClicked += (s, e) => CloseForm(); // This now works perfectly
            cmbCustomerOrderID.SelectedIndexChanged += CmbCustomerOrderID_SelectedIndexChanged;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = hide2.Visible = false;
            show.Visible = true;

            btnCustomerOrder.FillColor = btnAddress.FillColor = btnReturns.FillColor = Color.WhiteSmoke;
            btnCustomerOrder.ForeColor = btnAddress.ForeColor = btnReturns.ForeColor = Color.Black;

            if (show == pnlCustomerOrder) { btnCustomerOrder.FillColor = Color.FromArgb(0, 123, 255); btnCustomerOrder.ForeColor = Color.White; }
            else if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            else if (show == pnlReturns) { btnReturns.FillColor = Color.FromArgb(0, 123, 255); btnReturns.ForeColor = Color.White; }
        }

        private void CmbCustomerOrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOrderItems.Rows.Clear();
            UpdateTotal("₱0.00");

            if (cmbCustomerOrderID.SelectedIndex == -1) return;

            string orderId = cmbCustomerOrderID.Text;
            if (orderId == "ORD-2025-001")
            {
                dgvOrderItems.Rows.Add("Laptop Dell XPS 13", "1", "₱75,000.00", "₱75,000.00");
                dgvOrderItems.Rows.Add("Wireless Mouse", "2", "₱1,500.00", "₱3,000.00");
                UpdateTotal("₱78,000.00");
            }
            else if (orderId == "ORD-2025-002")
            {
                dgvOrderItems.Rows.Add("iPhone 15 Pro Max", "1", "₱94,990.00", "₱94,990.00");
                UpdateTotal("₱94,990.00");
            }
            else if (orderId == "ORD-2025-003")
            {
                dgvOrderItems.Rows.Add("Samsung 55\" 4K TV", "1", "₱45,990.00", "₱45,990.00");
                dgvOrderItems.Rows.Add("HDMI Cable", "3", "₱890.00", "₱2,670.00");
                UpdateTotal("₱48,660.00");
            }
        }

        private void UpdateTotal(string amount)
        {
            lblTotalAmountCO.Text = amount;
            lblTotalAmountAddr.Text = amount;
            lblTotalAmountRet.Text = amount;
        }

        private void SaveCustomerReturn()
        {
            MessageBox.Show("Customer Return has been saved successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }

        // FIXED & IMPROVED — NOW 100% RETURNS TO CUSTOMER RETURNS PAGE
        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                // ← NEW: Check if opened from ReturnList (highest priority)
                if (this.Tag is ReturnList returnListForm)
                {
                    parent.navBar1.PageTitle = "Return List";
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(returnListForm);
                    returnListForm.Show();
                    returnListForm.BringToFront();
                    this.Close();
                    return;
                }

                // ← OLD behavior: if opened from CustomerReturns page
                if (this.Tag is CustomerReturns customerReturnsForm)
                {
                    parent.navBar1.PageTitle = "Customer Returns";
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(customerReturnsForm);
                    customerReturnsForm.Show();
                    customerReturnsForm.BringToFront();
                    this.Close();
                    return;
                }

                // Final fallback
                parent.NavigateToCustomerReturns();
                this.Close();
                return;
            }

            this.Close();
        }
    }
}