namespace Nammedia.Medboss
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ParseResult
    {
        public ConditionType Type;
        public bool Success;
        public string Output;
        public ParseResult(ConditionType type, bool suc, string output)
        {
            this.Type = type;
            this.Success = suc;
            this.Output = output;
        }
    }
}
