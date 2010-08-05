namespace Nammedia.Medboss.Style
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class AutoSizeRender
    {
        public static void autoSizeControl(Control control, int textPadding)
        {
            Graphics graphics = control.CreateGraphics();
            Size size = graphics.MeasureString(control.Text, control.Font).ToSize();
            control.ClientSize = new Size(size.Width + (textPadding * 2), size.Height + (textPadding * 2));
            graphics.Dispose();
        }
    }
}
