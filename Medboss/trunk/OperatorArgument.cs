namespace Nammedia.Medboss
{
    using Nammedia.Medboss.controls;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OperatorArgument
    {
        public int DataId;
        public Nammedia.Medboss.controls.DataType DataType;
        public OperatorArgument(int dataid, Nammedia.Medboss.controls.DataType datatype)
        {
            this.DataId = dataid;
            this.DataType = datatype;
        }
    }
}
