namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Data;

    public class DataController
    {
        protected Nammedia.Medboss.DBDriver db = new Nammedia.Medboss.DBDriver();
        protected FindField[] fields;

        public DataController()
        {
            this.setFields();
        }

        protected DataTable _find(string condition, string tableSource)
        {
            int index;
            DataTable table = new DataTable();
            for (index = 0; index < this.fields.Length; index++)
            {
                table.Columns.Add(this.fields[index].DisplayField, this.fields[index].Type);
            }
            string text = "";
            index = 1;
            while (index < this.fields.Length)
            {
                text = text + this.fields[index].Field;
                if ((index + 1) < this.fields.Length)
                {
                    text = text + ",";
                }
                index++;
            }
            string text2 = "select " + text + " from " + tableSource;
            if (condition != "")
            {
                text2 = text2 + " where " + condition;
            }
            this.db.Command.CommandText = text2;
            IDataReader reader = this.db.Command.ExecuteReader();
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row[0] = ConvertHelper.getInt(reader.GetValue(0));
                for (index = 1; index < this.fields.Length; index++)
                {
                    object obj2 = new object();
                    if (this.fields[index].Type == typeof(int))
                    {
                        obj2 = ConvertHelper.getInt(reader.GetValue(index - 1));
                    }
                    else if (this.fields[index].Type == typeof(string))
                    {
                        obj2 = ConvertHelper.getString(reader.GetValue(index - 1));
                    }
                    else if (this.fields[index].Type == typeof(DateTime))
                    {
                        obj2 = ConvertHelper.getDateTime(reader.GetValue(index - 1));
                    }
                    row[index] = obj2;
                }
                table.Rows.Add(row);
            }
            reader.Close();
            this.db.Command.Connection.Close();
            return table;
        }

        protected virtual void setFields()
        {
            this.fields = new FindField[0];
        }

        public Nammedia.Medboss.DBDriver DBDriver
        {
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
            }
        }
    }
}
