namespace Nammedia.Medboss
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct FindField
    {
        public string Field;
        public string DisplayField;
        public System.Type Type;
        public FindField(string field, string displayField, System.Type type)
        {
            this.Field = field;
            this.DisplayField = displayField;
            this.Type = type;
        }

        public override string ToString()
        {
            return this.DisplayField;
        }
    }
}
