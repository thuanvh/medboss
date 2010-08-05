namespace Nammedia.Medboss.lib
{
    using System;

    public class FormatFactory
    {
        public void Bind(FormatType type, object control)
        {
            switch (type)
            {
                case FormatType.AutoNumber:
                    new AutoNumberFormater(control);
                    break;

                case FormatType.Int:
                    new IntegerFomater(control);
                    break;

                case FormatType.UpperCase:
                    new UpperCaseFormater(control);
                    break;

                case FormatType.Trim:
                    new TrimFormater(control);
                    break;

                case FormatType.Double:
                    new DoubleFormater(control);
                    break;
            }
        }

        public void Bind(FormatType type, object[] controls)
        {
            foreach (object obj in controls)
            {
                this.Bind(type, obj);
            }
        }
    }
}
