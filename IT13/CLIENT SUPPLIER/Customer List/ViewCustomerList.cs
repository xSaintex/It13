using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewCustomerList : Form
    {
        private readonly string _customerId;

        public ViewCustomerList(string customerId)
        {
            _customerId = customerId;
            InitializeComponent();

            // Tab switching
            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);

            // Back button
            btnBack.Click += (s, e) => CloseForm();

            // Default: Show Other Details
            ShowPanel(pnlOther, pnlAddress);

            LoadCustomerData();
            MakeFieldsReadOnly();
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
            cmbPayment.Text = "Net 30";
            cmbStatus.Text = "Active";

            txtContactPerson.Text = "Maria Santos";
            txtContactNum.Text = "+63 923 456 7890";

            cmbBCountry.Text = "Philippines";
            txtBCity.Text = "Manila";
            txtBZip.Text = "1000";
            txtBLine1.Text = "123 Rizal Ave";
            txtBLine2.Text = "Sta. Cruz";

            cmbSCountry.Text = "Philippines";
            txtSCity.Text = "Quezon City";
            txtSZip.Text = "1100";
            txtSLine1.Text = "456 EDSA";
            txtSLine2.Text = "Cubao";
        }

        private void MakeFieldsReadOnly()
        {
            txtTitle.ReadOnly = txtFName.ReadOnly = txtLName.ReadOnly = true;
            txtEmail.ReadOnly = txtCompany.ReadOnly = txtPhone.ReadOnly = true;
            txtContactPerson.ReadOnly = txtContactNum.ReadOnly = true;
            txtBCity.ReadOnly = txtBZip.ReadOnly = txtBLine1.ReadOnly = txtBLine2.ReadOnly = true;
            txtSCity.ReadOnly = txtSZip.ReadOnly = txtSLine1.ReadOnly = txtSLine2.ReadOnly = true;

            cmbPayment.Enabled = false;
            cmbStatus.Enabled = false;
            cmbBCountry.Enabled = false;
            cmbSCountry.Enabled = false;
            lnkCopy.Enabled = false;
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.NavigateToCustomerList(); // CORRECT & SAFE
            }
        }
    }
}