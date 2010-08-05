namespace Nammedia.Medboss
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class InsertSuccessfulLabel : Label
    {
        public InsertSuccessfulLabel()
        {
            this.Text = "Th\x00eam mới th\x00e0nh c\x00f4ng.";
            base.ImageAlign = ContentAlignment.MiddleLeft;
        }
    }
}
