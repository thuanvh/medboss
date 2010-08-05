namespace Nammedia.Medboss
{
    using System;

    internal class UpdateException : OperatorException
    {
        public override string Message()
        {
            return "Kh\x00f4ng thể cập nhật cơ sở dữ liệu";
        }
    }
}
