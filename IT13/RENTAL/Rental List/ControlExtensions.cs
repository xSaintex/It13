// ControlExtensions.cs   ← CREATE THIS FILE ONCE
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public static class ControlExtensions
    {
        public static IEnumerable<Control> GetAllControls(this Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(c => GetAllControls(c)).Concat(controls);
        }
    }
}