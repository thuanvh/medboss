namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;
    using System.Data;

    internal class LoaiThuocController : DataController
    {
        public int Delete(int id)
        {
            string text = "delete * from LoaiThuoc where MaLoai=" + id;
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new DeleteException();
            }
            base.db.Command.Connection.Close();
            return id;
        }

        public LoaiThuocInfo Get(int id)
        {
            string text = "select * from LoaiThuoc where MaLoai=" + id;
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            LoaiThuocInfo info = null;
            if (reader.Read())
            {
                info = new LoaiThuocInfo();
                info.MaLoai = (int) reader["MaLoai"];
                info.TenLoai = (string) reader["TenLoai"];
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return info;
        }

        public LoaiThuocInfo Get(string tenLoai)
        {
            string text = "select * from LoaiThuoc where TenLoai='" + tenLoai + "'";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            LoaiThuocInfo info = null;
            if (reader.Read())
            {
                info = new LoaiThuocInfo();
                info.MaLoai = (int) reader["MaLoai"];
                info.TenLoai = (string) reader["TenLoai"];
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return info;
        }

        public int Insert(LoaiThuocInfo loaiThuocInfo)
        {
            int missingId = IdManager.GetMissingId("MaLoai", "LoaiThuoc");
            string text = string.Concat(new object[] { "insert into LoaiThuoc values(", missingId, ",'", loaiThuocInfo.TenLoai, "')" });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            base.db.Command.Connection.Close();
            return missingId;
        }

        public ArrayList List()
        {
            base.db.Command.CommandText = "select * from LoaiThuoc";
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                LoaiThuocInfo info = new LoaiThuocInfo();
                info.MaLoai = (int) reader["MaLoai"];
                info.TenLoai = (string) reader["TenLoai"];
                list.Add(info);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return list;
        }

        public int Update(LoaiThuocInfo loaiThuocInfo)
        {
            string text = string.Concat(new object[] { "update LoaiThuoc set TenLoai='", loaiThuocInfo.TenLoai, "' where MaLoai=", loaiThuocInfo.MaLoai });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new UpdateException();
            }
            base.db.Command.Connection.Close();
            return loaiThuocInfo.MaLoai;
        }
    }
}
