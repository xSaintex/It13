// ViewRental.cs
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewRental : Form
    {
        private readonly List<RentalItem> rentalItems = new List<RentalItem>();
        private readonly string rentalId;

        public ViewRental(string id = "RENT-001")
        {
            InitializeComponent();
            rentalId = id;

            // Prevent NumericUpDown crash
            numServiceFee.Maximum = 999999m;
            numServiceFee_Addr.Maximum = 999999m;
            numDiscount.Maximum = 100m;
            numDiscount_Addr.Maximum = 100m;

            SetupDataGridViewStyle();   // ← Makes table look like EditRental
            LoadRentalData();
            SetupReadOnlyMode();
            WireEvents();
            ShowPanel(pnlRentalInfo, pnlAddress);
        }

        private void SetupDataGridViewStyle()
        {
            // ← EXACT SAME STYLE AS EDITRENTAL / ADDRENTAL
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 57, 101);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F, FontStyle.Bold);
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvItems.DefaultCellStyle.Font = new Font("Bahnschrift SemiCondensed", 10F);
            dgvItems.DefaultCellStyle.ForeColor = Color.Black;
            dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dgvItems.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvItems.RowTemplate.Height = 50;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.GridColor = Color.FromArgb(230, 230, 230);

            // Optional: Add subtle alternating row color (very professional)
            dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
        }

        private void LoadRentalData()
        {
            lblHeader.Text = $"View Rental – {rentalId}";

            txtCustomerName.Text = "Maria Clara Santos";
            txtContactPerson.Text = "Maria Clara";
            txtContactNumber.Text = "0998-765-4321";
            txtEmail.Text = "maria.clara@example.com";
            cmbBookingType.Text = "Event";
            cmbPaymentTerms.Text = "Full Payment";
            cmbStatus.Text = "Ongoing";
            dtpScheduledDate.Value = DateTime.Today.AddDays(-1);
            dtpReturnDate.Value = DateTime.Today.AddDays(6);
            txtBillingAddress.Text = "456 Rizal Ave., Manila, Metro Manila";
            txtShippingAddress.Text = "SM Mall of Asia, Pasay City (Event Venue)";
            numDiscount.Value = 5;
            numServiceFee.Value = 3500;

            var sampleItems = new[]
            {
                new RentalItem { ProductName = "LED Wall 15x25",       Quantity = 3, RentalPrice = 65000m, AvailableQty = 10 },
                new RentalItem { ProductName = "Stage Lights Package", Quantity = 1, RentalPrice = 25000m, AvailableQty = 5  },
                new RentalItem { ProductName = "Wireless Mic Set",     Quantity = 4, RentalPrice = 3000m,  AvailableQty = 12 }
            };

            rentalItems.Clear();
            rentalItems.AddRange(sampleItems);
            dgvItems.Rows.Clear();

            foreach (var item in sampleItems)
            {
                dgvItems.Rows.Add(
                    item.ProductName,
                    item.Quantity,
                    $"₱{item.RentalPrice:N2}",
                    item.AvailableQty,
                    $"₱{item.Subtotal:N2}"
                );
            }

            RecalculateTotals();
        }

        private void SetupReadOnlyMode()
        {
            foreach (Control c in this.GetAllControls())
            {
                switch (c)
                {
                    case Guna2TextBox txt: txt.ReadOnly = true; break;
                    case Guna2ComboBox cmb: cmb.Enabled = false; break;
                    case Guna2DateTimePicker dtp: dtp.Enabled = false; break;
                    case Guna2NumericUpDown num: num.Enabled = false; break;
                    case Guna2Button btn when btn != btnRentalInfo && btn != btnAddress && btn != btnClose:
                        btn.Enabled = false;
                        btn.Visible = false; // Hide Add Product, Search, etc.
                        break;
                }
            }

            txtSearchProduct.Visible = false;
            btnSearch.Visible = false;
            btnAddProduct.Visible = false;
        }

        private void WireEvents()
        {
            btnRentalInfo.Click += (s, e) => ShowPanel(pnlRentalInfo, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlRentalInfo);
            btnClose.Click += (s, e) => GoBackToRentalList();
            lnkBack.LinkClicked += (s, e) => GoBackToRentalList();
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
        }

        private void RecalculateTotals()
        {
            decimal subtotal = rentalItems.Sum(x => x.Subtotal);
            decimal discount = subtotal * numDiscount.Value / 100m;
            decimal total = subtotal - discount + numServiceFee.Value;

            string fmt = "₱{0:N2}";
            lblSubtotalVal.Text = lblSubtotalVal_Addr.Text = string.Format(fmt, subtotal);
            lblTotalVal.Text = lblTotalVal_Addr.Text = string.Format(fmt, total);
        }

        // SAME LOGIC AS EDITRENTAL – PERFECT NAVIGATION BACK
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