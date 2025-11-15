using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddSupplierList : Form
    {
        public AddSupplierList()
        {
            InitializeComponent();
            SetupPhoneFields();
            SetupNumberOnlyFields();

            // Tab button clicks
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress, pnlRemarks);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther, pnlRemarks);
            btnRemarks.Click += (s, e) => ShowPanel(pnlRemarks, pnlOther, pnlAddress);

            lnkCopy.LinkClicked += LnkCopy_LinkClicked;
            btnCancel.Click += (s, e) => CloseForm();
            btnSave.Click += (s, e) => SaveSupplier();

            // Initial state: Other Details open
            pnlOther.Visible = true;
            pnlAddress.Visible = false;
            pnlRemarks.Visible = false;

            btnOther.FillColor = Color.FromArgb(0, 123, 255);
            btnOther.ForeColor = Color.White;
            btnAddress.FillColor = Color.WhiteSmoke;
            btnAddress.ForeColor = Color.Black;
            btnRemarks.FillColor = Color.WhiteSmoke;
            btnRemarks.ForeColor = Color.Black;
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

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = false;
            hide2.Visible = false;
            show.Visible = true;

            // Reset all buttons
            btnOther.FillColor = Color.WhiteSmoke; btnOther.ForeColor = Color.Black;
            btnAddress.FillColor = Color.WhiteSmoke; btnAddress.ForeColor = Color.Black;
            btnRemarks.FillColor = Color.WhiteSmoke; btnRemarks.ForeColor = Color.Black;

            // Highlight active
            if (show == pnlOther) { btnOther.FillColor = Color.FromArgb(0, 123, 255); btnOther.ForeColor = Color.White; }
            if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            if (show == pnlRemarks) { btnRemarks.FillColor = Color.FromArgb(0, 123, 255); btnRemarks.ForeColor = Color.White; }
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
            parent?.NavigateToSupplierList();
        }

        private void SaveSupplier()
        {
            string remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? "None" : txtRemarks.Text.Trim();
            MessageBox.Show($"Supplier saved successfully!\n\nRemarks:\n{remarks}",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CloseForm();
        }
    }
}