using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewCustomerReturns : Form
    {
        public ViewCustomerReturns(object selectedReturn = null)
        {
            InitializeComponent();
            SetupControls();
            WireEvents();
            ShowPanel(pnlCustomerOrder, pnlAddress, pnlReturns);
            MakeReadOnly();

            if (selectedReturn != null)
            {
                LoadDataForView(selectedReturn);
            }
            else
            {
                LoadDataForView(); // sample data
            }
        }

        private void SetupControls()
        {
            cmbCustomerOrderID.Items.AddRange(new[] { "ORD-2025-001", "ORD-2025-002", "ORD-2025-003" });
            cmbPaymentTerms.Items.AddRange(new[] { "Cash", "Credit Card", "Bank Transfer", "Net 30" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Processing", "Completed", "Refunded" });
            cmbReturnType.Items.AddRange(new[] { "Defective Product", "Wrong Item", "No Longer Needed", "Damaged in Transit", "Other" });

            dtpReturnDate.Format = DateTimePickerFormat.Custom;
            dtpReturnDate.CustomFormat = "MMMM dd, yyyy";

            dgvOrderItems.EnableHeadersVisualStyles = false;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvOrderItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void WireEvents()
        {
            btnCustomerOrder.Click += (s, e) => ShowPanel(pnlCustomerOrder, pnlAddress, pnlReturns);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlCustomerOrder, pnlReturns);
            btnReturns.Click += (s, e) => ShowPanel(pnlReturns, pnlCustomerOrder, pnlAddress);
            lnkBack.LinkClicked += (s, e) => CloseForm();
        }

        private void LoadDataForView()
        {
            cmbCustomerOrderID.Text = "ORD-2025-001";
            cmbPaymentTerms.Text = "Credit Card";
            cmbStatus.Text = "Processing";
            dtpReturnDate.Value = new DateTime(2025, 11, 15);
            cmbReturnType.Text = "Defective Product";
            txtReturnReason.Text = "Customer reported laptop overheating and screen flickering after 3 days of use.";
            txtBillingAddress.Text = "123 Sampaguita St., Brgy. Holy Spirit, Quezon City, Metro Manila 1127";
            txtShippingAddress.Text = "Same as billing address";

            dgvOrderItems.Rows.Add("Laptop Dell XPS 13", "1", "₱75,000.00", "₱75,000.00");
            dgvOrderItems.Rows.Add("Wireless Mouse", "2", "₱1,500.00", "₱3,000.00");
            UpdateTotal("₱78,000.00");
        }

        private void UpdateTotal(string amount)
        {
            lblTotalAmountCO.Text = amount;
            lblTotalAmountAddr.Text = amount;
            lblTotalAmountRet.Text = amount;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel h1, Guna2ShadowPanel h2)
        {
            h1.Visible = h2.Visible = false;
            show.Visible = true;
            btnCustomerOrder.FillColor = btnAddress.FillColor = btnReturns.FillColor = Color.WhiteSmoke;
            btnCustomerOrder.ForeColor = btnAddress.ForeColor = btnReturns.ForeColor = Color.Black;
            if (show == pnlCustomerOrder) { btnCustomerOrder.FillColor = Color.FromArgb(0, 123, 255); btnCustomerOrder.ForeColor = Color.White; }
            else if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            else if (show == pnlReturns) { btnReturns.FillColor = Color.FromArgb(0, 123, 255); btnReturns.ForeColor = Color.White; }
        }

        private void MakeReadOnly()
        {
            cmbCustomerOrderID.Enabled = false;
            cmbPaymentTerms.Enabled = false;
            cmbStatus.Enabled = false;
            cmbReturnType.Enabled = false;
            dtpReturnDate.Enabled = false;
            txtBillingAddress.ReadOnly = true;
            txtShippingAddress.ReadOnly = true;
            txtReturnReason.ReadOnly = true;
            dgvOrderItems.ReadOnly = true;

            btnSaveCustomerOrder.Visible = false;
            btnSaveAddress.Visible = false;
            btnSaveReturns.Visible = false;

            lblRequired.Text = "This is a read-only view of the customer return.";
            lblRequired.ForeColor = Color.Gray;
        }

        private void LoadDataForView(object data = null)
        {
            cmbCustomerOrderID.Text = "ORD-2025-001";
            cmbPaymentTerms.Text = "Credit Card";
            cmbStatus.Text = "Processing";
            dtpReturnDate.Value = new DateTime(2025, 11, 15);
            cmbReturnType.Text = "Defective Product";
            txtReturnReason.Text = "Customer reported laptop overheating and screen flickering after 3 days of use.";
            txtBillingAddress.Text = "123 Sampaguita St., Brgy. Holy Spirit, Quezon City, Metro Manila 1127";
            txtShippingAddress.Text = "Same as billing address";

            dgvOrderItems.Rows.Clear();
            dgvOrderItems.Rows.Add("Laptop Dell XPS 13", "1", "₱75,000.00", "₱75,000.00");
            dgvOrderItems.Rows.Add("Wireless Mouse", "2", "₱1,500.00", "₱3,000.00");
            UpdateTotal("₱78,000.00");
        }

        private void CloseForm()
        {
            // CASE 1: Opened inside your main Form1 (MDI or panel-based navigation)
            var parent = this.Owner as Form1 ?? this.ParentForm as Form1;
            if (parent != null)
            {
                // If you passed the original CustomerReturns instance via .Tag when opening
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

                // Fallback: use built-in navigation method
                parent.NavigateToCustomerReturns();
                this.Close();
                return;
            }
        }
    }
}