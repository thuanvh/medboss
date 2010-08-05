namespace Nammedia.Medboss.Style
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class CommonDataGridViewCellStyle : DataGridViewCellStyle
    {
        public CommonDataGridViewCellStyle(DataGridViewCellStyle style) : base(style)
        {
            base.Font = new Font("Microsoft Sans Serif", 9.75f);
        }
    }
}
