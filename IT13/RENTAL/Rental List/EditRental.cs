// EditRental.cs
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditRental : Form
    {
        private readonly List<RentalItem> rentalItems = new List<RentalItem>();
        private readonly string rentalId;

        public EditRental(string id = "RENT-001")
        {
            InitializeComponent();
            rentalId = id;
            numServiceFee.Maximum = 999999;
            numDiscount.Maximum = 100;     // discount % stays 0–100
            numServiceFee_Addr.Maximum = 999999;
            numDiscount_Addr.Maximum = 100;

            SetupControls();
            WireEvents();
            LoadExistingData();           // ← Only difference from AddRental
            RecalculateTotals();
            ShowPanel(pnlRentalInfo, pnlAddress);
        }

        private void SetupControls()
        {
            cmbBookingType.Items.AddRange(new[] { "Daily", "Weekly", "Monthly", "Event", "Long Term" });
            cmbPaymentTerms.Items.AddRange(new[] { "Full Payment", "50% Downpayment", "Installment", "Cash on Delivery" });
            cmbStatus.Items.AddRange(new[] { "Pending", "Confirmed", "Ongoing", "Completed", "Cancelled" });

            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 57, 101);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F);
            dgvItems.RowTemplate.Height = 50;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadExistingData()
        {
            lblHeader.Text = $"Edit Rental - {rentalId}";

            // Simulate loading real data (replace with your actual DB load later)
            txtCustomerName.Text = "Juan Dela Cruz";
            txtContactPerson.Text = "Juan";
            txtContactNumber.Text = "0917-123-4567";
            txtEmail.Text = "juan@example.com";
            cmbBookingType.SelectedIndex = 3;     // Event
            cmbPaymentTerms.SelectedIndex = 1;    // 50% Downpayment
            cmbStatus.SelectedIndex = 2;          // Ongoing
            dtpScheduledDate.Value = DateTime.Today.AddDays(-2);
            dtpReturnDate.Value = DateTime.Today.AddDays(5);

            txtBillingAddress.Text = "123 Sampaguita St., Brgy. Holy Spirit, Quezon City";
            txtShippingAddress.Text = "Same as billing";

            numDiscount.Value = 10;
            numServiceFee.Value = 2500;

            // Sample rented items
            var items = new[]
            {
                new RentalItem { ProductName = "LED Wall 10x20", Quantity = 2, RentalPrice = 45000m, AvailableQty = 8 },
                new RentalItem { ProductName = "Sound System", Quantity = 1, RentalPrice = 18000m, AvailableQty = 4 }
            };

            rentalItems.AddRange(items);
            foreach (var item in items)
            {
                dgvItems.Rows.Add(
                    item.ProductName,
                    item.Quantity,
                    $"₱{item.RentalPrice:N2}",
                    item.AvailableQty,
                    $"₱{item.Subtotal:N2}"
                );
            }
        }

        private void WireEvents()
        {
            btnRentalInfo.Click += (s, e) => ShowPanel(pnlRentalInfo, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlRentalInfo);
            btnSaveRental.Click += (s, e) => SaveRental();
            lnkBack.LinkClicked += (s, e) => GoBackToRentalList();
            btnAddProduct.Click += btnAddProduct_Click;

            numDiscount.ValueChanged += (s, e) => SyncAndRecalculate(numDiscount, numDiscount_Addr);
            numServiceFee.ValueChanged += (s, e) => SyncAndRecalculate(numServiceFee, numServiceFee_Addr);
            numDiscount_Addr.ValueChanged += (s, e) => SyncAndRecalculate(numDiscount_Addr, numDiscount);
            numServiceFee_Addr.ValueChanged += (s, e) => SyncAndRecalculate(numServiceFee_Addr, numServiceFee);
        }

        private void SyncAndRecalculate(Guna2NumericUpDown source, Guna2NumericUpDown target)
        {
            if (source.Value != target.Value)
                target.Value = source.Value;
            RecalculateTotals();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var modal = new ProdRentalModal())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    var item = new RentalItem
                    {
                        ProductName = modal.SelectedProductName,
                        Quantity = modal.Quantity,
                        RentalPrice = modal.RentalPrice,
                        AvailableQty = modal.AvailableQuantity
                    };
                    rentalItems.Add(item);
                    dgvItems.Rows.Add(
                        item.ProductName,
                        item.Quantity,
                        $"₱{item.RentalPrice:N2}",
                        item.AvailableQty,
                        $"₱{item.Subtotal:N2}"
                    );
                    RecalculateTotals();
                }
            }
        }

        private void RecalculateTotals()
        {
            decimal subtotal = rentalItems.Sum(x => x.Subtotal);
            decimal discountAmount = subtotal * numDiscount.Value / 100m;
            decimal total = subtotal - discountAmount + numServiceFee.Value;

            string subText = $"₱{subtotal:N2}";
            string totalText = $"₱{total:N2}";

            lblSubtotalVal.Text = subText;
            lblTotalVal.Text = totalText;
            lblSubtotalVal_Addr.Text = subText;
            lblTotalVal_Addr.Text = totalText;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide)
        {
            hide.Visible = false;
            show.Visible = true;
            btnRentalInfo.FillColor = btnAddress.FillColor = Color.FromArgb(245, 245, 245);
            btnRentalInfo.ForeColor = btnAddress.ForeColor = Color.Black;

            if (show == pnlRentalInfo)
            {
                btnRentalInfo.FillColor = Color.FromArgb(0, 123, 255);
                btnRentalInfo.ForeColor = Color.White;
            }
            else
            {
                btnAddress.FillColor = Color.FromArgb(0, 123, 255);
                btnAddress.ForeColor = Color.White;
            }
            RecalculateTotals();
        }

        private void SaveRental()
        {
            MessageBox.Show($"Rental {rentalId} has been updated successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            GoBackToRentalList();
        }

        private void GoBackToRentalList()
        {
            var mainForm = this.ParentForm as Form1;
            if (mainForm == null)
            {
                this.Close();
                return;
            }

            var rentalList = mainForm.pnlContent.Controls.OfType<RentalList>().FirstOrDefault();
            if (rentalList != null)
            {
                mainForm.navBar1.PageTitle = "Rental List";
                rentalList.BringToFront();
                rentalList.RefreshData();
            }
            else
            {
                rentalList = new RentalList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                mainForm.pnlContent.Controls.Clear();
                mainForm.pnlContent.Controls.Add(rentalList);
                mainForm.navBar1.PageTitle = "Rental List";
                rentalList.Show();
                rentalList.RefreshData();
            }
            this.Close();
        }
    }
}