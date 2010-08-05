namespace Nammedia.Medboss
{
    using System;

    internal class UnNullValueException : Exception, IMedbossException
    {
        private string _fname;

        public UnNullValueException(string FieldName)
        {
            this._fname = FieldName;
        }

        string IMedbossException.Message()
        {
            return (this._fname + " kh\x00f4ng được ph\x00e9p rỗng.");
        }
    }
}
