// ProdRentalModal.Designer.cs
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace IT13
{
    partial class ProdRentalModal
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblHeader;
        private Label lblProductName, lblQuantity, lblRentalPrice, lblAvailableQty, lblSubtotal;
        private Guna2ComboBox cmbProductName;
        private Guna2TextBox txtQuantity, txtRentalPrice, txtAvailableQty;
        private Label lblSubtotalValue;
        private Guna2Button btnCancel, btnAdd;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new Size(580, 560);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Padding = new Padding(40);
            this.Font = new Font("Bahnschrift SemiCondensed", 10F);

            // Header
            lblHeader = new Label
            {
                Text = "Add Rental Item",
                Font = new Font("Tahoma", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(40, 30),
                AutoSize = true
            };

            int y = 100;

            // Product Name
            lblProductName = new Label
            {
                Text = "Product Name *",
                Location = new Point(40, y),
                ForeColor = Color.FromArgb(70, 70, 70),
                AutoSize = true
            };

            cmbProductName = new Guna2ComboBox
            {
                Location = new Point(40, y + 25),
                Size = new Size(500, 48),
                BorderRadius = 10,
                FillColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Bahnschrift SemiCondensed", 10F)
            };
            cmbProductName.SelectedIndexChanged += cmbProductName_SelectedIndexChanged;

            y += 90;

            // Quantity + Available (side by side)
            lblQuantity = new Label
            {
                Text = "Quantity *",
                Location = new Point(40, y),
                ForeColor = Color.FromArgb(70, 70, 70),
                AutoSize = true
            };

            txtQuantity = new Guna2TextBox
            {
                Location = new Point(40, y + 25),
                Size = new Size(180, 48),           // Short & neat
                BorderRadius = 10,
                TextAlign = HorizontalAlignment.Left,
                Padding = new Padding(15, 0, 0, 0),
                Text = "1"
            };
            txtQuantity.KeyPress += (s, e) => { if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true; };
            txtQuantity.TextChanged += txtQuantity_TextChanged;

            lblAvailableQty = new Label
            {
                Text = "Available Quantity",
                Location = new Point(250, y),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true
            };

            txtAvailableQty = new Guna2TextBox
            {
                Location = new Point(250, y + 25),
                Size = new Size(290, 48),
                BorderRadius = 10,
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245),
                Text = "12"
            };

            y += 90;

            // Rental Price
            lblRentalPrice = new Label
            {
                Text = "Rental Price (per day) *",
                Location = new Point(40, y),
                ForeColor = Color.FromArgb(70, 70, 70),
                AutoSize = true
            };

            txtRentalPrice = new Guna2TextBox
            {
                Location = new Point(40, y + 25),
                Size = new Size(500, 48),
                BorderRadius = 10,
                PlaceholderText = "0.00",
                TextAlign = HorizontalAlignment.Left,
                Padding = new Padding(15, 0, 0, 0)
            };
            txtRentalPrice.TextChanged += txtRentalPrice_TextChanged;

            y += 90;

            // Subtotal
            lblSubtotal = new Label
            {
                Text = "Subtotal:",
                Font = new Font("Bahnschrift SemiCondensed", 13F, FontStyle.Bold),
                Location = new Point(40, y),
                AutoSize = true
            };

            lblSubtotalValue = new Label
            {
                Text = "₱0.00",
                Font = new Font("Bahnschrift SemiCondensed", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 123, 255),
                Location = new Point(40, y + 40),
                AutoSize = true
            };

            y += 120;

            // Buttons
            btnCancel = new Guna2Button
            {
                Text = "Cancel",
                Location = new Point(330, y),
                Size = new Size(100, 46),
                FillColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                BorderRadius = 12,
                Font = new Font("Poppins", 11F, FontStyle.Bold)
            };
            btnCancel.Click += btnCancel_Click;

            btnAdd = new Guna2Button
            {
                Text = "Add",
                Location = new Point(440, y),
                Size = new Size(100, 46),
                FillColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                BorderRadius = 12,
                Font = new Font("Poppins", 11F, FontStyle.Bold)
            };
            btnAdd.Click += btnAdd_Click;

            // Add all controls
            this.Controls.AddRange(new Control[]
            {
                lblHeader,
                lblProductName, cmbProductName,
                lblQuantity, txtQuantity,
                lblAvailableQty, txtAvailableQty,
                lblRentalPrice, txtRentalPrice,
                lblSubtotal, lblSubtotalValue,
                btnCancel, btnAdd
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}