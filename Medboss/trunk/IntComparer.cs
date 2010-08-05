namespace Nammedia.Medboss
{
    using System;
    using System.Collections;

    internal class IntComparer : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            if (((int) a) > ((int) b))
            {
                return 1;
            }
            if (((int) a) == ((int) b))
            {
                return 0;
            }
            return -1;
        }
    }
}
