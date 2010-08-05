namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;

    internal class Array2StringArray
    {
        public static string[] Array2StrArr(ArrayList list)
        {
            if (list == null)
            {
                return new string[0];
            }
            string[] textArray = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                textArray[i] = list[i].ToString();
            }
            return textArray;
        }
    }
}
