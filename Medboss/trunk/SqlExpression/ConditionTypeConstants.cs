namespace Nammedia.Medboss.SqlExpression
{
    using System;
    using System.Collections;

    public class ConditionTypeConstants
    {
        private Hashtable _typeHash = new Hashtable();

        public ConditionTypeConstants()
        {
            this._typeHash.Add(ConditionType.Greater, new ConditionTypeComplex(ConditionType.Greater, "Lớn hơn", new string[] { ">" }, 1));
            this._typeHash.Add(ConditionType.GreaterOrEqual, new ConditionTypeComplex(ConditionType.GreaterOrEqual, "Lớn hơn hoặc bằng", new string[] { ">=" }, 1));
            this._typeHash.Add(ConditionType.Less, new ConditionTypeComplex(ConditionType.Less, "Nhỏ hơn", new string[] { "<" }, 1));
            this._typeHash.Add(ConditionType.LessOrEqual, new ConditionTypeComplex(ConditionType.LessOrEqual, "Nhỏ hơn hoặc bằng", new string[] { "<=" }, 1));
            this._typeHash.Add(ConditionType.Equal, new ConditionTypeComplex(ConditionType.Equal, "Bằng", new string[] { "=" }, 1));
            this._typeHash.Add(ConditionType.NotEqual, new ConditionTypeComplex(ConditionType.NotEqual, "Kh\x00e1c", new string[] { "<>" }, 1));
            this._typeHash.Add(ConditionType.BetweenAnd, new ConditionTypeComplex(ConditionType.BetweenAnd, "Trong khoảng", new string[] { "between", "and" }, 2));
            this._typeHash.Add(ConditionType.Like, new ConditionTypeComplex(ConditionType.Like, "Giống", new string[] { "like" }, 1));
            this._typeHash.Add(ConditionType.NotNull, new ConditionTypeComplex(ConditionType.NotNull, "Kh\x00f4ng rỗng", new string[] { "is not null" }, 0));
            this._typeHash.Add(ConditionType.Null, new ConditionTypeComplex(ConditionType.Null, "Rỗng", new string[] { "is null" }, 0));
        }

        public ArrayList getAllConditionTypeComplex()
        {
            ArrayList list = new ArrayList();
            foreach (object obj2 in this._typeHash.Values)
            {
                ConditionTypeComplex complex = (ConditionTypeComplex) obj2;
                list.Add(complex);
            }
            return list;
        }

        public ConditionTypeComplex getConditionTypeComplex(ConditionType type)
        {
            return (ConditionTypeComplex) this._typeHash[type];
        }
    }
}
