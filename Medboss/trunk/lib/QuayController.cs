namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    internal class QuayController : DataController
    {
        public ArrayList ConditionalList(string condition)
        {
            string text = "select * from Quay ";
            if (condition != "")
            {
                text = text + " where " + condition;
            }
            text = text + " order by TenQuay";
            IDbCommand command = base.db.Command;
            command.CommandText = text;
            IDataReader reader = command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                QuayInfo info = new QuayInfo();
                info.MaQuay = ConvertHelper.getInt(reader[0]);
                info.TenQuay = reader.GetString(1);
                list.Add(info);
            }
            reader.Close();
            command.Connection.Close();
            return list;
        }

        public int getMaQuay(string tenQuay)
        {
            int a = getMaQuayFromCache(tenQuay);
            if (a > 0)
            {
                return a;
            }
            string text = "select MaQuay from Quay where TenQuay='" + tenQuay + "'";
            IDbCommand command = base.db.Command;
            command.CommandText = text;
            int num = ConvertHelper.getInt(command.ExecuteScalar());
            command.Connection.Close();
            return num;
        }

        public static int getMaQuayFromCache(string tenQuay)
        {
            if ((Program.ACSource != null) && (Program.ACSource.QuaySource != null))
            {
                foreach (QuayInfo qi in Program.ACSource.QuaySource)
                {
                    if (qi.TenQuay == tenQuay)
                    {
                        return qi.MaQuay;
                    }
                }
            }
            return -1;
        }

        public QuayInfo getQuay(int Maquay)
        {
            QuayInfo qi = this.getQuayFromCache(Maquay);
            if (qi != null)
            {
                return qi;
            }
            ArrayList list = this.ConditionalList(" MaQuay=" + Maquay);
            if (list.Count > 0)
            {
                return (QuayInfo) list[0];
            }
            return new QuayInfo();
        }

        public QuayInfo getQuayFromCache(int Maquay)
        {
            if ((Program.ACSource != null) && (Program.ACSource.QuaySource != null))
            {
                foreach (QuayInfo qi in Program.ACSource.QuaySource)
                {
                    if (qi.MaQuay == Maquay)
                    {
                        return qi;
                    }
                }
            }
            return null;
        }

        public int Insert(QuayInfo qi)
        {
            IDbCommand command = base.db.Command;
            command.CommandText = "select * from Quay where TenQuay='" + qi.TenQuay + "'";
            IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                throw new InsertException();
            }
            qi.MaQuay = IdManager.GetMissingId("MaQuay", "Quay");
            reader.Close();
            command.CommandText = string.Concat(new object[] { "insert into Quay values(", qi.MaQuay, ",'", qi.TenQuay, "')" });
            if (command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            command.Connection.Close();
            return qi.MaQuay;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        public int Update(QuayInfo qi)
        {
            IDbCommand command = base.db.Command;
            command.CommandText = string.Concat(new object[] { "update Quay set TenQuay='", qi.TenQuay, "' where MaQuay=", qi.MaQuay });
            if (command.ExecuteNonQuery() < 0)
            {
                throw new UpdateException();
            }
            command.Connection.Close();
            return qi.MaQuay;
        }
    }
}
