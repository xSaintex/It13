using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class ViewDelivery : Form
    {
        public string CustomerOrderNo { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public string Vehicle { get; set; }
        public string PlateNumber { get; set; }
        public string Employee { get; set; }
        public string DeliveryDate { get; set; }
        public string LastAttempt { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }

        public ViewDelivery()
        {
            InitializeComponent();
            this.Load += ViewDelivery_Load;
        }

        private void ViewDelivery_Load(object sender, EventArgs e)
        {
            lblHeader.Text = $"Delivery #{CustomerOrderNo ?? "N/A"}";
            lblCreated.Text = "Created: " + (!string.IsNullOrWhiteSpace(CreatedDate) ? CreatedDate : DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"));

            txtCustomerOrder.Text = CustomerOrderNo ?? "-";
            txtDeliveryDate.Text = DeliveryDate ?? "-";
            txtEmployee.Text = string.IsNullOrEmpty(Employee) ? "Not Assigned" : Employee;
            txtVehicle.Text = Vehicle ?? "-";
            txtPlate.Text = PlateNumber ?? "-";
            txtLastAttempt.Text = string.IsNullOrEmpty(LastAttempt) ? "No attempts yet" : LastAttempt;

            lblCustomerValue.Text = CustomerName ?? "N/A";
            lblShippingValue.Text = ShippingAddress ?? "N/A";

            lblStatusValue.Text = Status ?? "Unknown";
            switch ((Status ?? "").Trim().ToLower())
            {
                case "delivered":
                    pnlStatusBadge.FillColor = Color.FromArgb(230, 255, 240);
                    lblStatusValue.ForeColor = Color.FromArgb(0, 150, 80);
                    break;
                case "pending":
                    pnlStatusBadge.FillColor = Color.FromArgb(255, 253, 230);
                    lblStatusValue.ForeColor = Color.FromArgb(184, 134, 11);
                    break;
                case "failed":
                case "undelivered":
                    pnlStatusBadge.FillColor = Color.FromArgb(255, 230, 230);
                    lblStatusValue.ForeColor = Color.FromArgb(200, 30, 30);
                    break;
                default:
                    pnlStatusBadge.FillColor = Color.FromArgb(240, 240, 240);
                    lblStatusValue.ForeColor = Color.Gray;
                    break;
            }

            btnAttempts.Click += (s, ev) => ShowAttempts();
            btnDetails.Click += (s, ev) => ShowDetails();
            btnAddAttempt.Click += btnAddAttempt_Click;
            btnBack.Click += btnBack_Click;

            dgvAttempts.Rows.Clear(); // Ensures empty on load

            ShowAttempts();
            RefreshAttemptsTable(); // Show table with style
        }

        private void RefreshAttemptsTable()
        {
            dgvAttempts.Rows.Clear();
            dgvAttempts.ClearSelection();
        }

        private void ShowAttempts()
        {
            pnlAttempts.Visible = true;
            pnlDetails.Visible = false;
            btnAttempts.FillColor = Color.FromArgb(0, 123, 255);
            btnAttempts.ForeColor = Color.White;
            btnDetails.FillColor = Color.FromArgb(240, 240, 240);
            btnDetails.ForeColor = Color.Black;
        }

        private void ShowDetails()
        {
            pnlDetails.Visible = true;
            pnlAttempts.Visible = false;
            btnDetails.FillColor = Color.FromArgb(0, 123, 255);
            btnDetails.ForeColor = Color.White;
            btnAttempts.FillColor = Color.FromArgb(240, 240, 240);
            btnAttempts.ForeColor = Color.Black;
        }

        // ADD ATTEMPT BUTTON — NOW OPENS YOUR MODAL!
        private void btnAddAttempt_Click(object sender, EventArgs e)
        {
            using (var modal = new AddAttemptModal())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    string formattedDate = modal.AttemptDate.ToString("MMM dd, yyyy");
                    dgvAttempts.Rows.Add(formattedDate, modal.Status, modal.Remarks);
                    dgvAttempts.ClearSelection(); // Keeps it unclickable
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var mainForm = this.ParentForm as Form1;
            if (mainForm != null)
            {
                mainForm.pnlContent.Controls.Clear();
                var deliveryList = new DeliveryList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                mainForm.pnlContent.Controls.Add(deliveryList);
                deliveryList.Show();
                mainForm.navBar1.PageTitle = "Delivery List";
            }
        }
    }
}