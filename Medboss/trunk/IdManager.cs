namespace Nammedia.Medboss
{
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    internal class IdManager
    {
        private static Dictionary<string, int> MissingIdDictionary;

        public static int GetMissingId(string FieldName, string TableName)
        {
            if (MissingIdDictionary == null)
            {
                MissingIdDictionary = new Dictionary<string, int>();
            }
            int i = 1;
            string key = TableName + "." + FieldName;
            if (MissingIdDictionary.ContainsKey(key))
            {
                i = MissingIdDictionary[key];
            }
            else
            {
                MissingIdDictionary.Add(key, 1);
            }
            i = GetMissingId(FieldName, TableName, i);
            MissingIdDictionary[key] = i + 1;
            return i;
        }

        public static int GetMissingId(string FieldName, string TableName, int least)
        {
            DBDriver driver = new DBDriver();
            IDbCommand command = driver.Command;
            string text = string.Concat(new object[] { " where ", FieldName, "=", least });
            command.CommandText = "select " + FieldName + " from " + TableName + text;
            if (ConvertHelper.getInt(command.ExecuteScalar()) != least)
            {
                return least;
            }
            text = string.Concat(new object[] { " where ", FieldName, ">=", least });
            command.CommandText = "select " + FieldName + " from " + TableName + text;
            IDataReader reader = command.ExecuteReader();
            ArrayList list = new ArrayList();
            list.Add(least - 1);
            int num = 0;
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            list.Sort(intComparer());
            for (num = 0; num < (list.Count - 1); num++)
            {
                if ((((int) list[num]) + 1) < ((int) list[num + 1]))
                {
                    return (((int) list[num]) + 1);
                }
            }
            return ((list.Count == 0) ? least : (((int) list[list.Count - 1]) + 1));
        }

        public static IComparer intComparer()
        {
            return new IntComparer();
        }
    }
}
