namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    internal class LoaiThuChiController : DataController
    {
        public int Delete(int maLoaiThuChi)
        {
            string text = "select count(*) from KhoanThuChi where MaLoaiThuChi=" + maLoaiThuChi;
            base.db.Command.CommandText = text;
            if (ConvertHelper.getInt(base.db.Command.ExecuteScalar()) != 0)
            {
                throw new DeleteException();
            }
            text = "delete * from LoaiThuChi where MaLoaiThuChi=" + maLoaiThuChi;
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() <= 0)
            {
                throw new DeleteException();
            }
            base.db.Command.Connection.Close();
            return maLoaiThuChi;
        }

        public LoaiThuChiInfo get(int id)
        {
            string text = "select MaLoaiThuChi,TenLoaiThuChi from LoaiThuChi where MaLoaiThuChi=" + id;
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            LoaiThuChiInfo info = new LoaiThuChiInfo();
            while (reader.Read())
            {
                info.MaLoaiThuChi = ConvertHelper.getInt(reader[0]);
                info.TenLoaiThuChi = reader.GetString(1);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return info;
        }

        public int getId(string tenLoaiThuChi)
        {
            string text = "select MaLoaiThuChi from LoaiThuChi where TenLoaiThuChi='" + tenLoaiThuChi + "'";
            base.db.Command.CommandText = text;
            int num = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            base.db.Command.Connection.Close();
            return num;
        }

        public int Insert(LoaiThuChiInfo tci)
        {
            tci.MaLoaiThuChi = IdManager.GetMissingId("MaLoaiThuChi", "LoaiThuChi");
            string text = string.Concat(new object[] { "insert into LoaiThuChi values(", tci.MaLoaiThuChi, ",'", tci.TenLoaiThuChi, "',", tci.DangHoatDong, ")" });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            base.db.Command.Connection.Close();
            return tci.MaLoaiThuChi;
        }

        public ArrayList list()
        {
            base.db.Command.CommandText = "select MaLoaiThuChi, TenLoaiThuChi, DangHoatDong from LoaiThuChi where DangHoatDong=true";
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                LoaiThuChiInfo info = new LoaiThuChiInfo();
                info.MaLoaiThuChi = ConvertHelper.getInt(reader[0]);
                info.TenLoaiThuChi = reader.GetString(1);
                info.DangHoatDong = reader.GetBoolean(2);
                list.Add(info);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return list;
        }

        public ArrayList listAll()
        {
            base.db.Command.CommandText = "select MaLoaiThuChi, TenLoaiThuChi, DangHoatDong from LoaiThuChi ";
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                LoaiThuChiInfo info = new LoaiThuChiInfo();
                info.MaLoaiThuChi = ConvertHelper.getInt(reader[0]);
                info.TenLoaiThuChi = reader.GetString(1);
                info.DangHoatDong = reader.GetBoolean(2);
                list.Add(info);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return list;
        }

        public int Update(LoaiThuChiInfo tci)
        {
            string text = string.Concat(new object[] { "update LoaiThuChi set TenLoaiThuChi='", tci.TenLoaiThuChi, "', DangHoatDong=", tci.DangHoatDong, " where MaLoaiThuChi=", tci.MaLoaiThuChi });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new UpdateException();
            }
            base.db.Command.Connection.Close();
            return tci.MaLoaiThuChi;
        }
    }
}
