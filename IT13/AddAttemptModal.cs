// ---------------------------------------------------------------------
// AddAttemptModal.cs
// ---------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace IT13
{
    public partial class AddAttemptModal : Form
    {
        public DateTime AttemptDate { get; private set; }
        public string Status { get; private set; }
        public string Remarks { get; private set; }

        public AddAttemptModal()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Today;
            cmbStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AttemptDate = dtpDate.Value;
            Status = cmbStatus.Text;
            Remarks = txtRemarks.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}