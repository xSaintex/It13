using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class Sidebar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSidebar = new System.Windows.Forms.Panel();

            // Replace IconButtons with normal Buttons
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnRental = new System.Windows.Forms.Button();
            this.btnReturns = new System.Windows.Forms.Button();
            this.btnDeliveries = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnStockAdjustments = new System.Windows.Forms.Button();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();

            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblLogo = new System.Windows.Forms.Label();

            this.panelSidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();

            // 
            // panelSidebar
            // 
            this.panelSidebar.AutoScroll = true;
            this.panelSidebar.BackColor = System.Drawing.Color.White;
            this.panelSidebar.Controls.Add(this.btnHelp);
            this.panelSidebar.Controls.Add(this.btnReports);
            this.panelSidebar.Controls.Add(this.btnUsers);
            this.panelSidebar.Controls.Add(this.btnEmployees);
            this.panelSidebar.Controls.Add(this.btnRental);
            this.panelSidebar.Controls.Add(this.btnReturns);
            this.panelSidebar.Controls.Add(this.btnDeliveries);
            this.panelSidebar.Controls.Add(this.btnSuppliers);
            this.panelSidebar.Controls.Add(this.btnCustomers);
            this.panelSidebar.Controls.Add(this.btnStockAdjustments);
            this.panelSidebar.Controls.Add(this.btnSales);
            this.panelSidebar.Controls.Add(this.btnOrders);
            this.panelSidebar.Controls.Add(this.btnProducts);
            this.panelSidebar.Controls.Add(this.btnInventory);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSidebar.Size = new System.Drawing.Size(260, 800);
            this.panelSidebar.Name = "panelSidebar";

            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(0, 89, 179);
            this.panelLogo.Controls.Add(this.pictureBoxLogo);
            this.panelLogo.Controls.Add(this.lblLogo);
            this.panelLogo.Dock = DockStyle.Top;
            this.panelLogo.Size = new System.Drawing.Size(260, 80);

            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = Properties.Resources.le_parisien_logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxLogo.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;

            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Location = new Point(70, 25);
            this.lblLogo.Text = "LE PARISIEN";

            // 🔹 Default style for all buttons
            Action<Button> setupBtn = btn =>
            {
                btn.Dock = DockStyle.Top;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(260, 52);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(16, 0, 0, 0);
                btn.Font = new Font("Segoe UI", 10F);
                btn.ForeColor = Color.FromArgb(80, 80, 80);
                btn.BackColor = Color.Transparent;
            };

            setupBtn(this.btnDashboard);
            setupBtn(this.btnInventory);
            setupBtn(this.btnProducts);
            setupBtn(this.btnOrders);
            setupBtn(this.btnSales);
            setupBtn(this.btnStockAdjustments);
            setupBtn(this.btnCustomers);
            setupBtn(this.btnSuppliers);
            setupBtn(this.btnDeliveries);
            setupBtn(this.btnReturns);
            setupBtn(this.btnRental);
            setupBtn(this.btnEmployees);
            setupBtn(this.btnUsers);
            setupBtn(this.btnReports);
            setupBtn(this.btnHelp);

            this.btnSuppliers.Visible = false; // merged into Customers section

            // 
            // Sidebar (UserControl)
            // 
            this.Controls.Add(this.panelSidebar);
            this.Name = "Sidebar";
            this.Size = new System.Drawing.Size(260, 800);
            this.BackColor = Color.White;

            this.panelSidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Panel panelLogo;
        private PictureBox pictureBoxLogo;
        private Label lblLogo;

        // ✅ Standard WinForms Buttons (no FontAwesome)
        private Button btnDashboard;
        private Button btnInventory;
        private Button btnProducts;
        private Button btnOrders;
        private Button btnSales;
        private Button btnStockAdjustments;
        private Button btnCustomers;
        private Button btnSuppliers;
        private Button btnDeliveries;
        private Button btnReturns;
        private Button btnRental;
        private Button btnEmployees;
        private Button btnUsers;
        private Button btnReports;
        private Button btnHelp;
    }
}
