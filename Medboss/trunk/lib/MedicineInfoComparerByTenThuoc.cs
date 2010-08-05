namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections.Generic;

    internal class MedicineInfoComparerByTenThuoc : IComparer<MedicineInfo>
    {
        int IComparer<MedicineInfo>.Compare(MedicineInfo x, MedicineInfo y)
        {
            return x.TenThuoc.CompareTo(y.TenThuoc);
        }
    }
}
