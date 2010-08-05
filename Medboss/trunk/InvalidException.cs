namespace Nammedia.Medboss
{
    using System;

    internal class InvalidException : Exception, IMedbossException
    {
        private string value;

        public InvalidException(string a)
        {
            this.value = a;
        }

        string IMedbossException.Message()
        {
            return ("Sai định dạng:" + this.value);
        }
    }
}
