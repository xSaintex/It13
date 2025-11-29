using System;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace IT13
{
    public partial class ViewProd : Form
    {
        public ViewProd(string productId = "")
        {
            InitializeComponent();
            this.AutoScroll = true;
            btncancel.Click += btncancel_Click;
            ApplyViewStyling();
            LoadProductData();
        }

        private void ApplyViewStyling()
        {
            btnhome.Visible = btnproductlist.Visible = btnadd.Visible = false;

            var lblInfo = new Label
            {
                Text = "Product details are displayed below.",
                Font = new Font("Poppins", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(77, 70)
            };
            mainpanel.Controls.Add(lblInfo);
            mainpanel.Controls.SetChildIndex(lblInfo, 1);

            foreach (var tb in new[] { guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4 })
            {
                tb.ReadOnly = true;
                tb.BorderColor = Color.FromArgb(200, 200, 200);
                tb.BackColor = Color.FromArgb(248, 248, 248);
                tb.Font = new Font("Bahnschrift SemiCondensed", 11F);
                tb.Cursor = Cursors.Default;
            }
            guna2TextBox4.Multiline = true;
            guna2TextBox4.BorderRadius = 16;
            guna2TextBox4.TextAlign = HorizontalAlignment.Left;

            foreach (var cb in new[] { guna2ComboBox1, guna2ComboBox2, guna2ComboBox3 })
            {
                cb.Enabled = false;
                cb.BackColor = Color.FromArgb(248, 248, 248);
                cb.BorderColor = Color.FromArgb(200, 200, 200);
                cb.Font = new Font("Bahnschrift SemiCondensed", 11F);
            }

            foreach (Control c in mainpanel.Controls)
                if (c is Label lbl && lbl != label2)
                    lbl.Font = new Font("Bahnschrift SemiCondensed", 11F);

            btnaddprod.Visible = false;
            btncancel.Text = "Close";
            btncancel.FillColor = Color.FromArgb(108, 117, 125);
            btncancel.Font = new Font("Poppins", 10.5F, FontStyle.Bold);
            btncancel.BorderRadius = 12;
        }

        private void LoadProductData()
        {
            if (guna2ComboBox1.Items.Count == 0)
            {
                guna2ComboBox1.Items.AddRange(new object[] { "Electronics", "Accessories", "Furniture", "Office Supplies", "Cables", "Audio", "Others" });
                guna2ComboBox2.Items.AddRange(new object[] { "TechSupply Co.", "Cable World", "Office Plus", "AudioTech", "Global Traders" });
                guna2ComboBox3.Items.AddRange(new object[] { "In Stock", "Low Stock", "Out of Stock", "Discontinued" });
            }

            guna2TextBox1.Text = "Wireless Mouse";
            guna2TextBox2.Text = "250.00";
            guna2TextBox3.Text = "350.00";
            guna2TextBox4.Text = "Ergonomic wireless mouse with adjustable DPI and long battery life.";

            guna2ComboBox1.Text = "Electronics";
            guna2ComboBox2.Text = "TechSupply Co.";
            guna2ComboBox3.Text = "In Stock";

            label2.Text = "View Product Details";
        }

        private void btncancel_Click(object sender, EventArgs e)
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