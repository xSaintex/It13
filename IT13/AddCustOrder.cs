using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT13
{
    public partial class AddCustOrder : Form
    {
        public AddCustOrder()
        {
            InitializeComponent();

            // Enable double buffering for the entire form to reduce flicker
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Enable double buffering for DataGridView
            if (dgvItems != null)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.SetProperty,
                    null, dgvItems, new object[] { true });
            }

            // Enable double buffering for all shadow panels
            EnableDoubleBuffering(mainpanel);
            EnableDoubleBuffering(guna2ShadowPanel1);
            EnableDoubleBuffering(guna2ShadowPanel2);
            EnableDoubleBuffering(guna2ShadowPanel3);
            EnableDoubleBuffering(panelDgvWrapper);
        }

        private void EnableDoubleBuffering(Control control)
        {
            if (control != null)
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.SetProperty,
                    null, control, new object[] { true });
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void comboxcompanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void AddCustOrder_Load(object sender, EventArgs e)
        {
            // Suspend layout during load to improve performance
            this.SuspendLayout();
            mainpanel.SuspendLayout();

            // Your initialization code here

            // Resume layout after initialization
            mainpanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}