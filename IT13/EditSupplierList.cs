using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditSupplierList : Form
    {
        private readonly string _supplierId;

        public EditSupplierList(string supplierId)
        {
            _supplierId = supplierId;
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();

            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => UpdateSupplier();

            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;

            LoadSupplierData();
        }

        private void SetupPhoneFields()
        {
            txtPhone.PlaceholderText = "+63 9XX XXX XXXX";
            txtContactNum.PlaceholderText = "+63 9XX XXX XXXX";
        }

        private void SetupNumberOnlyFields()
        {
            txtPhone.KeyPress += NumberOnly_KeyPress;
            txtContactNum.KeyPress += NumberOnly_KeyPress;
            txtBZip.KeyPress += NumberOnly_KeyPress;
            txtSZip.KeyPress += NumberOnly_KeyPress;
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide)
        {
            show.Visible = true;
            hide.Visible = false;
            btnOther.FillColor = show == pnlOther ? Color.FromArgb(0, 123, 255) : Color.WhiteSmoke;
            btnAddress.FillColor = show == pnlAddress ? Color.FromArgb(0, 123, 255) : Color.WhiteSmoke;
            btnOther.ForeColor = show == pnlOther ? Color.White : Color.Black;
            btnAddress.ForeColor = show == pnlAddress ? Color.White : Color.Black;
        }

        private void LoadSupplierData()
        {
            txtTitle.Text = "Ms.";
            txtFName.Text = "Sarah";
            txtLName.Text = "Go";
            txtEmail.Text = "sarah@global.com";
            txtCompany.Text = "Global Supplies Inc.";
            txtPhone.Text = "+63 922 888 9999";
            cmbPayment.SelectedIndex = 1; // Net 15
            cmbStatus.SelectedIndex = 0;  // Active

            txtContactPerson.Text = "Mark Lim";
            txtContactNum.Text = "+63 917 555 1234";

            cmbBCountry.SelectedIndex = 0;
            txtBCity.Text = "Makati City";
            txtBZip.Text = "1229";
            txtBLine1.Text = "Ayala Avenue";
            txtBLine2.Text = "Legazpi Village";

            cmbSCountry.SelectedIndex = 0;
            txtSCity.Text = "Taguig City";
            txtSZip.Text = "1634";
            txtSLine1.Text = "32nd Street";
            txtSLine2.Text = "Bonifacio Global City";
        }

        private void LnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbSCountry.SelectedItem = cmbBCountry.SelectedItem;
            txtSCity.Text = txtBCity.Text;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
        }

        private void UpdateSupplier()
        {
            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                string.IsNullOrWhiteSpace(txtLName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtCompany.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Supplier {_supplierId} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            parent?.NavigateToSupplierList();
        }
    }
}