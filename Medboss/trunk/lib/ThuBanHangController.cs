using Nammedia.Medboss;
using Nammedia.Medboss.Log;
using System;
using System.Collections;
using System.Data;
namespace Nammedia.Medboss.lib
{


    public class ThuBanHangController : DataController
    {
        public void Delete(string condition)
        {
            string text = "delete * from ThuBanHang";
            if (condition != "")
            {
                text = text + " where " + condition;
            }
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new DeleteException();
            }
        }

        public int Insert(ThuBanHangInfo thubanArr)
        {
            IDbTransaction transaction = base.db.Command.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            int least = -1;
            try
            {
                least = IdManager.GetMissingId("MaThu", "ThuBanHang");
                this.Delete(string.Concat(new object[] { "Ngay=#", thubanArr.Ngay.ToString("MM/dd/yyyy"), "# and MaQuay=", thubanArr.MaQuay }));
                foreach (ThuBanHangChiTietInfo info in thubanArr.ThuBanHangChiTiet)
                {
                    info.MaThu = IdManager.GetMissingId("MaThu", "ThuBanHang", least);
                    least++;
                    string text = string.Concat(new object[] { "insert into ThuBanHang values(", info.MaThu, ",#", thubanArr.Ngay.ToString("MM/dd/yyyy"), "#,", thubanArr.MaQuay, ",", info.ThuBanHang, ",", info.TienDoi, ")" });
                    base.db.Command.CommandText = text;
                    if (base.db.Command.ExecuteNonQuery() < 0)
                    {
                        throw new InsertException();
                    }
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                transaction.Rollback();
                LogManager.LogException(exc);
                throw new InsertException();
            }
            finally
            {
                base.db.Command.Connection.Close();
            }
            return least;
        }

        public ArrayList List()
        {
            base.db.Command.CommandText = "select * from qryThuBanHang";
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                ThuBanHangInfo info = new ThuBanHangInfo();
                info.Ngay = (DateTime) reader["Ngay"];
                info.MaQuay = (int) reader["MaQuay"];
                ThuBanHangChiTietInfo info2 = new ThuBanHangChiTietInfo();
                info2.MaThu = (int) reader["MaThu"];
                info2.ThuBanHang = (int) reader["ThuBanHang"];
                info2.TienDoi = (int) reader["TienDoi"];
                list.Add(info);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return list;
        }

        public DataTable ListTable(string condition)
        {
            string text = "select * from qryThuBanHang";
            if (condition != "")
            {
                text = text + " where " + condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("MaThu");
            table.Columns.Add("Ngay");
            table.Columns.Add("TenQuay");
            table.Columns.Add("ThuBanHang");
            table.Columns.Add("TienDoi");
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row["MaThu"] = reader["MaThu"];
                row["Ngay"] = reader["Ngay"];
                row["TenQuay"] = reader["TenQuay"];
                row["ThuBanHang"] = reader["ThuBanHang"];
                row["TienDoi"] = reader["TienDoi"];
                table.Rows.Add(row);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return table;
        }
    }
}
