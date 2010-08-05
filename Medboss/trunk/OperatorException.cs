namespace Nammedia.Medboss
{
    using System;

    public class OperatorException : Exception, IMedbossException
    {
        public IOperator Operator;
        public Nammedia.Medboss.OperatorFunctionType OperatorFunctionType;

        public virtual string Message()
        {
            return "";
        }
    }
}
