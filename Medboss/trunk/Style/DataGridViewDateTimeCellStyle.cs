namespace Nammedia.Medboss.Style
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    internal class DataGridViewDateTimeCellStyle : DataGridViewCellStyle
    {
        public DataGridViewDateTimeCellStyle(string formatString)
        {
            base.Format = formatString;
            base.FormatProvider = new DateTimeFormatInfo();
            base.Alignment = DataGridViewContentAlignment.TopRight;
        }

        public DataGridViewDateTimeCellStyle(string[] patterns, char charFormat)
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            info.SetAllDateTimePatterns(patterns, charFormat);
            base.FormatProvider = info;
            base.Alignment = DataGridViewContentAlignment.TopRight;
        }
    }
}
