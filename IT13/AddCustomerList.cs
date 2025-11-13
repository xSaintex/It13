// AddCustomerList.cs
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCustomerList : Form
    {
        public AddCustomerList()
        {
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => SaveCustomer();

            // DEFAULT: Other Details
            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;
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
            btnAddress.ForeColor = show == pnlAddress ? Color.White : Color.White;
        }

        private void LnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbSCountry.SelectedItem = cmbBCountry.SelectedItem;
            txtSCity.Text = txtBCity.Text;
            txtSZip.Text = txtBZip.Text;
            txtSLine1.Text = txtBLine1.Text;
            txtSLine2.Text = txtBLine2.Text;
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Customer List";
                parent.LoadCustomerListForm();
            }
        }

        private void SaveCustomer()
        {
            MessageBox.Show("Customer saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }
    }
}