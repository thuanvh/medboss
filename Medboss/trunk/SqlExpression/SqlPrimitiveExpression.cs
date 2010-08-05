namespace Nammedia.Medboss.SqlExpression
{
    using Nammedia.Medboss;
    using System;

    [Serializable]
    public class SqlPrimitiveExpression
    {
        public bool ComparedField;
        public ConditionTypeComplex Condition;
        public FindField Field;
        public bool IsNot;
        public object[] Values;

        public string toSqlSimpleString()
        {
            string text = "";
            if (this.IsNot)
            {
                text = text + "not ";
            }
            if (this.ComparedField)
            {
                text = text + this.Condition.SqlText[0];
                FindField field = (FindField) this.Values[0];
                return (text + " [" + field.Field + "] ");
            }
            for (int i = 0; i < this.Condition.ParameterNumbers; i++)
            {
                text = text + this.Condition.SqlText[i] + " ";
                string text2 = "";
                if (this.Field.Type == typeof(DateTime))
                {
                    text2 = "#" + ((DateTime) this.Values[i]).ToString("MM/dd/yyyy") + "#";
                }
                else if (this.Field.Type == typeof(string))
                {
                    text2 = "'" + this.Values[i].ToString() + "'";
                }
                else if (this.Field.Type == typeof(int))
                {
                    text2 = this.Values[i].ToString();
                }
                text = text + text2;
            }
            if (this.Condition.ParameterNumbers == 0)
            {
                text = text + this.Condition.SqlText[0];
            }
            return text;
        }

        public string toSqlString()
        {
            return (this.Field.Field + " " + this.toSqlSimpleString());
        }
    }
}
