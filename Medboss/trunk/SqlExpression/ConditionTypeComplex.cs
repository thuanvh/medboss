namespace Nammedia.Medboss.SqlExpression
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct ConditionTypeComplex
    {
        public ConditionType Type;
        public string DisplayText;
        public string[] SqlText;
        public int ParameterNumbers;
        public ConditionTypeComplex(ConditionType type, string displayText, string[] sqlText, int paraNumber)
        {
            this.Type = type;
            this.DisplayText = displayText;
            this.SqlText = sqlText;
            this.ParameterNumbers = paraNumber;
        }

        public override string ToString()
        {
            return this.DisplayText;
        }
    }
}
