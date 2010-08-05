namespace Nammedia.Medboss
{
    using System;

    internal class DeleteException : Exception, IMedbossException
    {
        string IMedbossException.Message()
        {
            return "Kh\x00f4ng thể xo\x00e1 trong cơ sở dữ liệu";
        }
    }
}
