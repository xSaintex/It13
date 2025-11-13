using System;
using System.Windows.Forms;

namespace IT13
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show Login first
            using (var login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // Login SUCCESS → Open Form1
                    Application.Run(new Form1());
                }
                // Login failed or canceled → App closes
            }
        }
    }
}