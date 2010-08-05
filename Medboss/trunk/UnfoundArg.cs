namespace Nammedia.Medboss
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct UnfoundArg
    {
        public FieldKey Key;
        public string Field;
        public string Value;
        public UnfoundArg(FieldKey key, string field, string value)
        {
            this.Key = key;
            this.Field = field;
            this.Value = value;
        }
    }
}
