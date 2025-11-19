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

            btnOther.Click += (s, e) => ShowPanel(pnlOther, pnlAddress, pnlRemarks);
            btnAddress.Click += (s, e) => ShowPanel(pnlAddress, pnlOther, pnlRemarks);
            btnRemarks.Click += (s, e) => ShowPanel(pnlRemarks, pnlOther, pnlAddress);
            btnBack.Click += (s, e) => CloseForm();

            ShowPanel(pnlOther, pnlAddress, pnlRemarks); // default tab
            LoadSupplierData();
        }

        private void ShowPanel(Guna2ShadowPanel show, Guna2ShadowPanel hide1, Guna2ShadowPanel hide2)
        {
            hide1.Visible = hide2.Visible = false;
            show.Visible = true;

            btnOther.FillColor = Color.WhiteSmoke; btnOther.ForeColor = Color.Black;
            btnAddress.FillColor = Color.WhiteSmoke; btnAddress.ForeColor = Color.Black;
            btnRemarks.FillColor = Color.WhiteSmoke; btnRemarks.ForeColor = Color.Black;

            if (show == pnlOther) { btnOther.FillColor = Color.FromArgb(0, 123, 255); btnOther.ForeColor = Color.White; }
            if (show == pnlAddress) { btnAddress.FillColor = Color.FromArgb(0, 123, 255); btnAddress.ForeColor = Color.White; }
            if (show == pnlRemarks) { btnRemarks.FillColor = Color.FromArgb(0, 123, 255); btnRemarks.ForeColor = Color.White; }
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

            txtRemarks.Text =
                "Reliable supplier for office supplies and equipment.\r\n" +
                "• Offers 5% discount for bulk orders over ₱50,000\r\n" +
                "• Contact Robert Tan for quotations and delivery schedule\r\n" +
                "• Preferred payment: Bank transfer within Net 30";
        }

        private void CloseForm()
        {
            var parent = this.ParentForm as Form1;
            parent?.NavigateToSupplierList();
        }
    }
}