using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewSupplierReturns : Form
    {
        public ViewSupplierReturns(object selectedReturn = null)
        {
            InitializeComponent();
            SetupControls();
            WireEvents();
            ShowPanel(pnlSupplierOrder, pnlAddress, pnlReturns);
            MakeReadOnly();

            if (selectedReturn != null)
            {
                LoadDataForView(selectedReturn);
            }
            else
            {
                LoadDataForView(); // Sample data
            }
        }

        private void SetupControls()
        {
            // Populate dropdowns (even if disabled, for consistency)
            cmbSupplierOrderID.Items.AddRange(new[] { "SO-2025-001", "SO-2025-002", "SO-2025-003" });
            cmbPaymentTerms.Items.AddRange(new[] { "Cash", "Credit 30 Days", "Credit 60 Days", "Bank Transfer" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Processing", "Returned", "Rejected" });
            cmbReturnType.Items.AddRange(new[]
            {
                "Defective Product", "Over Delivery", "Wrong Item Sent", "Damaged in Transit", "Other"
            });

            dtpReturnDate.Format = DateTimePickerFormat.Custom;
            dtpReturnDate.CustomFormat = "MMMM dd, yyyy";

            dgvOrderItems.EnableHeadersVisualStyles = false;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvOrderItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void WireEvents()
        {
            btnSupplierOrder.Click += (s, e) => ShowPanel(pnlSupplierOrder, pnlAddress, pnlReturns);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlSupplierOrder, pnlReturns);
            btnReturns.Click += (s, e) => ShowPanel(pnlReturns, pnlSupplierOrder, pnlAddress);
            lnkBack.LinkClicked += (s, e) => CloseForm();
        }

        private void LoadDataForView(object data = null)
        {
            cmbSupplierOrderID.Text = "SO-2025-001";
            cmbPaymentTerms.Text = "Credit 30 Days";
            cmbStatus.Text = "Returned";
            dtpReturnDate.Value = new DateTime(2025, 11, 20);
            cmbReturnType.Text = "Defective Product";
            txtReturnReason.Text = "10 units of Dell XPS 13 arrived with cracked screens. Supplier agreed to full return.";
            txtBillingAddress.Text = "Unit 88, ABC Industrial Complex, Pasig City, Metro Manila";
            txtShippingAddress.Text = "Same as billing address";

            dgvOrderItems.Rows.Clear();
            dgvOrderItems.Rows.Add("Laptop Dell XPS 13", "5", "₱70,000.00", "₱350,000.00");
            dgvOrderItems.Rows.Add("Wireless Mouse", "20", "₱1,200.00", "₱24,000.00");
            UpdateTotal("₱374,000.00");
        }

        private void UpdateTotal(string amount)
        {
            lblTotalAmountSO.Text = amount;
            lblTotalAmountAddr.Text = amount;
            lblTotalAmountRet.Text = amount;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel h1, Guna2ShadowPanel h2)
        {
            h1.Visible = h2.Visible = false;
            show.Visible = true;

            btnSupplierOrder.FillColor = btnAddress.FillColor = btnReturns.FillColor = Color.WhiteSmoke;
            btnSupplierOrder.ForeColor = btnAddress.ForeColor = btnReturns.ForeColor = Color.Black;

            if (show == pnlSupplierOrder) { btnSupplierOrder.FillColor = Color.FromArgb(0, 123, 255); btnSupplierOrder.ForeColor = Color.White; }
            else if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            else if (show == pnlReturns) { btnReturns.FillColor = Color.FromArgb(0, 123, 255); btnReturns.ForeColor = Color.White; }
        }

        private void MakeReadOnly()
        {
            cmbSupplierOrderID.Enabled = false;
            cmbPaymentTerms.Enabled = false;
            cmbStatus.Enabled = false;
            cmbReturnType.Enabled = false;
            dtpReturnDate.Enabled = false;
            txtBillingAddress.ReadOnly = true;
            txtShippingAddress.ReadOnly = true;
            txtReturnReason.ReadOnly = true;
            dgvOrderItems.ReadOnly = true;         

            // Update info text
            lblRequired.Text = "This is a read-only view of the supplier return.";
            lblRequired.ForeColor = Color.Gray;
        }

        private void CloseForm()
        {
            var parent = this.Owner as Form1 ?? this.ParentForm as Form1;
            if (parent != null)
            {
                // ← NEW: Check if opened from ReturnList
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

                // ← OLD behavior: if opened from SupplierReturns page
                if (this.Tag is SupplierReturns supplierReturnsForm)
                {
                    parent.navBar1.PageTitle = "Supplier Returns";
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(supplierReturnsForm);
                    supplierReturnsForm.Show();
                    supplierReturnsForm.BringToFront();
                    this.Close();
                    return;
                }

                // Fallback
                parent.NavigateToSupplierReturns();
                this.Close();
                return;
            }
            this.Close();
        }
    }
}