using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    public partial class ViewSupplierList : Form
    {
        private readonly string _supplierId;

        public ViewSupplierList(string supplierId)
        {
            _supplierId = supplierId;
            InitializeComponent();

            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther);
            btnBack.Click += (s, e) => CloseForm();

            ShowPanel(pnlOther, pnlAddress); // Default tab
            LoadSupplierData();
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

        private void LoadSupplierData()
        {
            txtTitle.Text = "Ms.";
            txtFName.Text = "Sarah";
            txtLName.Text = "Lim";
            txtEmail.Text = "sarah@supplies.co";
            txtCompany.Text = "Metro Supplies Co.";
            txtPhone.Text = "+63 917 888 7777";
            cmbPayment.Text = "Net 30";
            cmbStatus.Text = "Active";

            txtContactPerson.Text = "Robert Tan";
            txtContactNum.Text = "+63 922 555 4321";

            cmbBCountry.Text = "Philippines";
            txtBCity.Text = "Pasig City";
            txtBZip.Text = "1605";
            txtBLine1.Text = "Ortigas Center";
            txtBLine2.Text = "San Antonio";

            cmbSCountry.Text = "Philippines";
            txtSCity.Text = "Mandaluyong City";
            txtSZip.Text = "1550";
            txtSLine1.Text = "Shaw Boulevard";
            txtSLine2.Text = "Greenfield District";
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
            parent?.NavigateToSupplierList();
        }
    }
}