namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;

    internal interface IAutoCompleter
    {
        void DisableAutoCompleted(object control);
        void EnableAutoCompleted(object control, ref ArrayList datasource);
        void RefreshSource(ref ArrayList datasource);
        void RemoveAutoCompleted();
    }
}
