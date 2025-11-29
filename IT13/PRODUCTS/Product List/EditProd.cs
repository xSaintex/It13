using System;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace IT13
{
    public partial class EditProd : Form
    {
        private readonly string _pid;

        public EditProd(string productId = "")
        {
            _pid = productId;
            InitializeComponent();
            this.AutoScroll = true;

            btncancel.Click += btncancel_Click;
            btnaddprod.Click += btnaddprod_Click;

            ApplyModernStyling();

            label2.Text = "Edit Product";
            btnaddprod.Text = "Update Product";

            LoadProductData();
        }

        private void ApplyModernStyling()
        {
            // Hide breadcrumb buttons
            btnhome.Visible = btnproductlist.Visible = btnadd.Visible = false;

            // Red required text
            var lblRequired = new Label
            {
                Text = "Fields marked with (*) are required",
                Font = new Font("Poppins", 9F),
                ForeColor = Color.Red,
                AutoSize = true,
                Location = new Point(77, 70)
            };
            mainpanel.Controls.Add(lblRequired);
            mainpanel.Controls.SetChildIndex(lblRequired, 1);

            // TextBoxes
            foreach (var tb in new[] { guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4 })
            {
                tb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                tb.ForeColor = Color.Black;
                tb.BorderRadius = 12;
                tb.BorderThickness = 1;
                tb.BorderColor = Color.FromArgb(180, 180, 180);
                tb.FillColor = Color.White;
                tb.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
                tb.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
                tb.PlaceholderForeColor = Color.Gray;
            }

            guna2TextBox1.PlaceholderText = "Enter Product Name";
            guna2TextBox2.PlaceholderText = "0.00";
            guna2TextBox3.PlaceholderText = "0.00";
            guna2TextBox4.PlaceholderText = "Enter product description...";
            guna2TextBox4.TextAlign = HorizontalAlignment.Left;
            guna2TextBox4.Multiline = true;
            guna2TextBox4.BorderRadius = 16;

            // ComboBoxes
            foreach (var cb in new[] { guna2ComboBox1, guna2ComboBox2, guna2ComboBox3 })
            {
                cb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                cb.ForeColor = Color.Black;
                cb.BorderRadius = 12;
                cb.BorderThickness = 1;
                cb.BorderColor = Color.FromArgb(180, 180, 180);
                cb.FillColor = Color.White;
            }

            // Labels (except header)
            foreach (Control c in mainpanel.Controls)
                if (c is Label lbl && lbl != label2)
                    lbl.Font = new Font("Bahnschrift SemiCondensed", 11F);

            // Buttons
            foreach (var btn in new[] { btnaddprod, btncancel })
            {
                btn.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
                btn.BorderRadius = 12;
                btn.ForeColor = Color.White;
                btn.FillColor = btn == btnaddprod ? Color.FromArgb(0, 123, 255) : Color.FromArgb(220, 53, 69);
                btn.HoverState.FillColor = btn == btnaddprod ? Color.FromArgb(0, 105, 230) : Color.FromArgb(200, 35, 51);
            }

            // Numbers only
            guna2TextBox2.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };
            guna2TextBox3.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true; };
            guna2TextBox2.TextChanged += (s, e) => LimitDecimalPlaces(guna2TextBox2);
            guna2TextBox3.TextChanged += (s, e) => LimitDecimalPlaces(guna2TextBox3);
        }

        private void LimitDecimalPlaces(Guna.UI2.WinForms.Guna2TextBox tb)
        {
            if (tb.Text.Contains(".") && tb.Text.Split('.').Length > 2)
                tb.Text = tb.Text.Remove(tb.Text.LastIndexOf('.'));
            if (tb.Text.StartsWith(".")) tb.Text = "0" + tb.Text;
            tb.SelectionStart = tb.Text.Length;
        }

        private void LoadProductData()
        {
            // Populate ComboBoxes first
            if (guna2ComboBox1.Items.Count == 0)
            {
                guna2ComboBox1.Items.AddRange(new object[] { "Electronics", "Accessories", "Furniture", "Office Supplies", "Cables", "Audio", "Others" });
                guna2ComboBox2.Items.AddRange(new object[] { "TechSupply Co.", "Cable World", "Office Plus", "AudioTech", "Global Traders" });
                guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock", "Discontinued" });
            }

            // Sample data (replace later with real row data)
            guna2TextBox1.Text = "Wireless Mouse";
            guna2TextBox2.Text = "250.00";
            guna2TextBox3.Text = "350.00";
            guna2TextBox4.Text = "Ergonomic wireless mouse with adjustable DPI.";

            guna2ComboBox1.Text = "Electronics";
            guna2ComboBox2.Text = "TechSupply Co.";
            guna2ComboBox3.Text = "In Stock";
        }

        private void btncancel_Click(object sender, EventArgs e) => NavigateBack();

        private void btnaddprod_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NavigateBack();
        }

        private void NavigateBack()
        {
            var parent = this.ParentForm as Form1;
            if (parent != null)
            {
                parent.navBar1.PageTitle = "Product List";
                var list = new ProductList
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                parent.pnlContent.Controls.Clear();
                parent.pnlContent.Controls.Add(list);
                list.Show();
            }
        }
    }
}