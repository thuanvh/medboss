namespace Nammedia.Medboss.Style
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    internal class DataGridViewIntCellStyle : DataGridViewCellStyle
    {
        public DataGridViewIntCellStyle()
        {
            base.Format = "#,#";
            base.FormatProvider = new NumberFormatInfo();
            base.Alignment = DataGridViewContentAlignment.TopRight;
        }
    }
}
