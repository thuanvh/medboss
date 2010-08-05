namespace Nammedia.Medboss
{
    using System;
    using System.Data;

    internal interface IFinder
    {
        DataTable AdvanceFind(string condition);
        DataTable Find(FindParam[] findParams);
        FindField[] getFields();
    }
}
