namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ParamNode
    {
        public string name;
        public string value;
        public ParamNode(string Name, string Value)
        {
            this.name = Name;
            this.value = Value;
        }
    }
}
