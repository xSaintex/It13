using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddSupplierReturns : Form
    {
        public AddSupplierReturns()
        {
            InitializeComponent();
            SetupControls();
            WireEvents();
            ShowPanel(pnlSupplierOrder, pnlAddress, pnlReturns);
            LoadSupplierOrderIDs();
        }

        private void LoadSupplierOrderIDs()
        {
            string[] supplierOrders = {
                "SO-2025-001", "SO-2025-002", "SO-2025-003",
                "SO-2025-004", "SO-2025-005"
            };
            cmbSupplierOrderID.Items.AddRange(supplierOrders);
        }

        private void SetupControls()
        {
            cmbPaymentTerms.Items.AddRange(new[] { "Cash", "Credit 30 Days", "Credit 60 Days", "Bank Transfer" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Processing", "Returned", "Rejected" });
            cmbReturnType.Items.AddRange(new[]
            {
                "Defective Product", "Over Delivery", "Wrong Item Sent", "Damaged in Transit", "Other"
            });

            dtpReturnDate.Value = DateTime.Today;
            dtpReturnDate.Format = DateTimePickerFormat.Custom;
            dtpReturnDate.CustomFormat = "MMMM dd, yyyy";

            // EXACT SAME GRID STYLE AS CUSTOMER RETURNS
            dgvOrderItems.EnableHeadersVisualStyles = false;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvOrderItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvOrderItems.DefaultCellStyle.SelectionBackColor = dgvOrderItems.DefaultCellStyle.BackColor;
            dgvOrderItems.DefaultCellStyle.SelectionForeColor = dgvOrderItems.DefaultCellStyle.ForeColor;
        }

        private void WireEvents()
        {
            btnSupplierOrder.Click += (s, e) => ShowPanel(pnlSupplierOrder, pnlAddress, pnlReturns);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlSupplierOrder, pnlReturns);
            btnReturns.Click += (s, e) => ShowPanel(pnlReturns, pnlSupplierOrder, pnlAddress);
            btnSaveSupplierOrder.Click += (s, e) => SaveSupplierReturn();
            btnSaveAddress.Click += (s, e) => SaveSupplierReturn();
            btnSaveReturns.Click += (s, e) => SaveSupplierReturn();
            lnkBack.LinkClicked += (s, e) => CloseForm();
            cmbSupplierOrderID.SelectedIndexChanged += CmbSupplierOrderID_SelectedIndexChanged;
        }

        private void CmbSupplierOrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOrderItems.Rows.Clear();
            UpdateTotal("₱0.00");

            if (cmbSupplierOrderID.SelectedIndex == -1) return;

            string orderId = cmbSupplierOrderID.Text;

            if (orderId == "SO-2025-001")
            {
                dgvOrderItems.Rows.Add("Laptop Dell XPS 13", "5", "₱70,000.00", "₱350,000.00");
                dgvOrderItems.Rows.Add("Wireless Mouse", "20", "₱1,200.00", "₱24,000.00");
                UpdateTotal("₱374,000.00");
            }
            else if (orderId == "SO-2025-002")
            {
                dgvOrderItems.Rows.Add("iPhone 15 Pro Max", "10", "₱90,000.00", "₱900,000.00");
                UpdateTotal("₱900,000.00");
            }
            else if (orderId == "SO-2025-003")
            {
                dgvOrderItems.Rows.Add("Samsung 55\" 4K TV", "8", "₱42,000.00", "₱336,000.00");
                UpdateTotal("₱336,000.00");
            }
        }

        private void UpdateTotal(string amount)
        {
            lblTotalAmountSO.Text = amount;
            lblTotalAmountAddr.Text = amount;
            lblTotalAmountRet.Text = amount;
        }

        private void SaveSupplierReturn()
        {
            MessageBox.Show("Supplier Return has been saved successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = hide2.Visible = false;
            show.Visible = true;

            btnSupplierOrder.FillColor = btnAddress.FillColor = btnReturns.FillColor = Color.WhiteSmoke;
            btnSupplierOrder.ForeColor = btnAddress.ForeColor = btnReturns.ForeColor = Color.Black;

            if (show == pnlSupplierOrder) { btnSupplierOrder.FillColor = Color.FromArgb(0, 123, 255); btnSupplierOrder.ForeColor = Color.White; }
            else if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            else if (show == pnlReturns) { btnReturns.FillColor = Color.FromArgb(0, 123, 255); btnReturns.ForeColor = Color.White; }
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                if (this.Tag is SupplierReturns supplierReturnsForm)
                {
                    parent.navBar1.PageTitle = "Supplier Returns";
                    parent.pnlContent.Controls.Clear();
                    parent.pnlContent.Controls.Add(supplierReturnsForm);
                    supplierReturnsForm.Show();
                    return;
                }
                parent.NavigateToSupplierReturns();
                return;
            }

            this.Hide();
            var form = Application.OpenForms["SupplierReturns"] as SupplierReturns;
            if (form != null) { form.Show(); form.BringToFront(); }
            else { new SupplierReturns().Show(); }
            this.Close();
        }
    }
}