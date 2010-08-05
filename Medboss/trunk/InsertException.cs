namespace Nammedia.Medboss
{
    using System;

    internal class InsertException : OperatorException
    {
        public override string Message()
        {
            return "Kh\x00f4ng thể th\x00eam v\x00e0o cơ sở dữ liệu";
        }
    }
}
