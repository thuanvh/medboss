namespace Nammedia.Medboss.report
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ReportParam
    {
        public string Name;
        public object Value;
        public ReportParam(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
