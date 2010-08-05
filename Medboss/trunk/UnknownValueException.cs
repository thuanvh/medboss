namespace Nammedia.Medboss
{
    using System;

    public class UnknownValueException : OperatorException
    {
        public UnfoundArgs args;

        public UnknownValueException(UnfoundArgs ufargs)
        {
            this.args = ufargs;
        }

        public override string Message()
        {
            string text = "";
            foreach (UnfoundArg arg in this.args.fieldValue)
            {
                string text3 = text;
                text = text3 + arg.Field + " : " + arg.Value + ", ";
            }
            return ("Kh\x00f4ng t\x00ecm thấy th\x00f4ng tin:" + text);
        }
    }
}
