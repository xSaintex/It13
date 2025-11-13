// ---------------------------------------------------------------------
// EditCustomerList.cs
// Title = TextBox | CustomerList = UserControl | No ArgumentException
// Numbers Only | Cancel = Red | Rounded Panels
// ---------------------------------------------------------------------
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class EditCustomerList : Form
    {
        private readonly string _customerId;

        public EditCustomerList(string customerId)
        {
            _customerId = customerId;
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => UpdateCustomer();

            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;

            LoadCustomerData();
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

        private void LoadCustomerData()
        {
            txtTitle.Text = "Mr.";
            txtFName.Text = "John";
            txtLName.Text = "Doe";
            txtEmail.Text = "john@abc.com";
            txtCompany.Text = "ABC Corporation";
            txtPhone.Text = "+63 905 123 4567";
            cmbPayment.SelectedIndex = 2;
            cmbStatus.SelectedIndex = 0;
            txtContactPerson.Text = "Maria Santos";
            txtContactNum.Text = "+63 923 456 7890";

            cmbBCountry.SelectedIndex = 0;
            txtBCity.Text = "Manila";
            txtBZip.Text = "1000";
            txtBLine1.Text = "123 Rizal Ave";
            txtBLine2.Text = "Sta. Cruz";

            cmbSCountry.SelectedIndex = 0;
            txtSCity.Text = "Quezon City";
            txtSZip.Text = "1100";
            txtSLine1.Text = "456 EDSA";
            txtSLine2.Text = "Cubao";
        }

        private void LnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbSCountry.SelectedItem = cmbBCountry.SelectedItem;
            txtSCity.Text = txtBCity.Text;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
        }

        private void UpdateCustomer()
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

            MessageBox.Show($"Customer {_customerId} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Customer List";
                parent.pnlContent.Controls.Clear();

                var customerList = new CustomerList();
                customerList.Dock = DockStyle.Fill;
                parent.pnlContent.Controls.Add(customerList);
            }
        }
    }
}