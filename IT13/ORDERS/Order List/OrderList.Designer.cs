using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    partial class OrderList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // create container
            components = new System.ComponentModel.Container();

            // Header style for DataGridView
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(100, 88, 255),
                ForeColor = Color.White,
                Font = new Font("Tahoma", 10.2F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Instantiate controls (all)
            mainpanel = new Guna2ShadowPanel();
            panelTop = new Guna2Panel();
            txtSearch = new Guna2TextBox();
            btnSearch = new Guna2Button();
            cmbFilters = new Guna2ComboBox();
            btnAddCustomer = new Guna2Button();
            btnAddSupplier = new Guna2Button();
            cmbExport = new Guna2ComboBox();
            panelGrid = new Guna2ShadowPanel();
            datagridvieworders = new Guna2DataGridView();
            colCheck = new DataGridViewCheckBoxColumn();
            colOrderType = new DataGridViewTextBoxColumn();
            colCompany = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colActions = new DataGridViewTextBoxColumn();

            // Suspend layouts
            mainpanel.SuspendLayout();
            panelTop.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridvieworders).BeginInit();
            SuspendLayout();

            // ==================== MAIN PANEL ====================
            mainpanel.BackColor = Color.Transparent;
            mainpanel.FillColor = Color.White;
            mainpanel.Location = new Point(300, 88);
            mainpanel.Name = "mainpanel";
            mainpanel.Radius = 8;
            mainpanel.ShadowColor = Color.Black;
            mainpanel.Size = new Size(1602, 878);
            mainpanel.TabIndex = 0;

            // ==================== TOP BAR ====================
            panelTop.Location = new Point(77, 40);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1458, 56);

            // Create controls and add to top panel
            txtSearch.Name = "txtSearch";
            txtSearch.Location = new Point(0, 8);
            txtSearch.Size = new Size(300, 40);
            txtSearch.PlaceholderText = "Search by Order ID or Company";
            txtSearch.BorderRadius = 8;
            txtSearch.FillColor = Color.WhiteSmoke;

            btnSearch.Name = "btnSearch";
            btnSearch.Location = new Point(310, 8);
            btnSearch.Size = new Size(90, 40);
            btnSearch.Text = "Search";
            btnSearch.FillColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.BorderRadius = 8;

            // Compute explicit positions (avoid repeated --= that caused problems)
            int exportX = panelTop.Size.Width - 140;         // far right (export)
            int supplierX = exportX - 180;                   // left of export
            int customerX = supplierX - 180;                 // left of supplier
            int filterX = customerX - 130;                   // left of customer

            // Export (far right)
            cmbExport.Name = "cmbExport";
            cmbExport.Location = new Point(exportX, 8);
            cmbExport.Size = new Size(140, 40);
            cmbExport.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add Supplier (to the left of Export)
            btnAddSupplier.Name = "btnAddSupplier";
            btnAddSupplier.Location = new Point(supplierX, 8);
            btnAddSupplier.Size = new Size(170, 40);
            btnAddSupplier.Text = "+Add Supplier Order";
            btnAddSupplier.FillColor = Color.FromArgb(0, 123, 255);
            btnAddSupplier.ForeColor = Color.White;
            btnAddSupplier.BorderRadius = 8;
            btnAddSupplier.TextAlign = HorizontalAlignment.Center;
            btnAddSupplier.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnAddSupplier.TextOffset = new Point(0, 0);
            btnAddSupplier.Padding = new Padding(0);
            btnAddSupplier.Margin = new Padding(0);
            btnAddSupplier.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Add Customer (to the left of Supplier)
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Location = new Point(customerX, 8);
            btnAddCustomer.Size = new Size(170, 40);
            btnAddCustomer.Text = "+Add Customer Order";
            btnAddCustomer.FillColor = Color.FromArgb(0, 123, 255);
            btnAddCustomer.ForeColor = Color.White;
            btnAddCustomer.BorderRadius = 8;
            btnAddCustomer.TextAlign = HorizontalAlignment.Center;
            btnAddCustomer.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            btnAddCustomer.TextOffset = new Point(0, 0);
            btnAddCustomer.Padding = new Padding(0);
            btnAddCustomer.Margin = new Padding(0);
            btnAddCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Filters (square) - to the left of Add Customer
            cmbFilters.Name = "cmbFilters";
            cmbFilters.Location = new Point(filterX, 8);
            cmbFilters.Size = new Size(120, 40);
            cmbFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilters.Font = new Font("Segoe UI", 9F);

            // Add controls to the panelTop (after positions assigned)
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(cmbFilters);
            panelTop.Controls.Add(btnAddCustomer);
            panelTop.Controls.Add(btnAddSupplier);
            panelTop.Controls.Add(cmbExport);

            // Now add the top panel to main panel
            mainpanel.Controls.Add(panelTop);

            // ==================== GRID PANEL ====================
            panelGrid.Location = new Point(77, 104);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1458, 716);
            panelGrid.FillColor = Color.White;

            // DATAGRIDVIEW
            datagridvieworders.Location = new Point(22, 27);
            datagridvieworders.Name = "datagridvieworders";
            datagridvieworders.Size = new Size(1412, 662);
            datagridvieworders.AllowUserToAddRows = false;
            datagridvieworders.AllowUserToResizeColumns = false;
            datagridvieworders.AllowUserToResizeRows = false;
            datagridvieworders.ColumnHeadersDefaultCellStyle = headerStyle;
            datagridvieworders.ColumnHeadersHeight = 40;
            datagridvieworders.GridColor = Color.FromArgb(231, 229, 255);
            datagridvieworders.RowHeadersVisible = false;

            // Ensure these handlers exist in your other partial class
            datagridvieworders.CellPainting += datagridvieworders_CellPainting;
            datagridvieworders.CellClick += datagridvieworders_CellClick;

            // Columns
            colCheck.HeaderText = "";
            colCheck.Name = "colCheck";
            colCheck.Width = 140;

            colOrderType.HeaderText = "ORDER TYPE";
            colOrderType.Name = "colOrderType";
            colOrderType.Width = 140;

            colCompany.HeaderText = "COMPANY NAME";
            colCompany.Name = "colCompany";
            colCompany.Width = 200;

            colQty.HeaderText = "QTY";
            colQty.Name = "colQty";
            colQty.Width = 100;

            colTotal.HeaderText = "TOTAL COST";
            colTotal.Name = "colTotal";
            colTotal.Width = 130;

            colStatus.HeaderText = "STATUS";
            colStatus.Name = "colStatus";
            colStatus.Width = 120;

            colDate.HeaderText = "ESTIMATED DATE";
            colDate.Name = "colDate";
            colDate.Width = 180;

            colActions.HeaderText = "ACTIONS";
            colActions.Name = "colActions";
            colActions.Width = 120;
            colActions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            datagridvieworders.Columns.AddRange(new DataGridViewColumn[]
            {
                colCheck, colOrderType, colCompany, colQty, colTotal, colStatus, colDate, colActions
            });

            // add datagridview to panelGrid and panelGrid to mainpanel
            panelGrid.Controls.Add(datagridvieworders);
            mainpanel.Controls.Add(panelGrid);

            // ==================== FORM ====================
            ClientSize = new Size(1914, 1055);
            Controls.Add(mainpanel);
            Name = "OrderList";
            Text = "Order List";

            // Resume layouts
            mainpanel.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagridvieworders).EndInit();
            ResumeLayout(false);
        }

        // ==================== CONTROL DECLARATIONS ====================
        private Guna2ShadowPanel mainpanel;
        private Guna2Panel panelTop;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSearch;
        private Guna2ComboBox cmbFilters;
        private Guna2Button btnAddCustomer;
        private Guna2Button btnAddSupplier;
        private Guna2ComboBox cmbExport;
        private Guna2ShadowPanel panelGrid;
        private Guna2DataGridView datagridvieworders;
        private DataGridViewCheckBoxColumn colCheck;
        private DataGridViewTextBoxColumn colOrderType;
        private DataGridViewTextBoxColumn colCompany;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colActions;
    }
}
