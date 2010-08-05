namespace Nammedia.Medboss
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct FindParam
    {
        public FindKeyParam key;
        public string name;
        public object value;
        public FindParam(FindKeyParam k, string Name, object Value)
        {
            this.key = k;
            this.name = Name;
            this.value = Value;
        }
    }
}
