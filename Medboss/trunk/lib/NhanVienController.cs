namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;
    using System.Data;

    public class NhanVienController : DataController
    {
        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from Nhanvien";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            text = text + " order by Ten";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                NhanVienInfo info = new NhanVienInfo();
                info.MaNhanVien = reader.GetInt32(0);
                info.Ten = reader.GetString(1);
                info.HoVaTenDem = reader.GetString(2);
                info.SoCMT = reader.GetInt32(3);
                info.GhiChu = reader.GetString(4);
                list.Add(info);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return list;
        }

        public int Delete(int maNhanVien)
        {
            string text = "delete NhanVien where MaNhanVien=" + maNhanVien;
            IDbCommand command = base.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new DeleteException();
            }
            command.Connection.Close();
            return maNhanVien;
        }

        public NhanVienInfo GetById(int maNhanVien)
        {
            ArrayList list = this.ConditionalList(" MaNhanVien=" + maNhanVien.ToString());
            NhanVienInfo info = new NhanVienInfo();
            info.MaNhanVien = -1;
            if (list.Count > 0)
            {
                return (NhanVienInfo) list[0];
            }
            return info;
        }

        public int getIdByName(string name)
        {
            ArrayList list = this.ConditionalList(" Ten like '" + MedicineController.StandardizeMedicineName(name) + "'");
            if (list.Count > 0)
            {
                return ((NhanVienInfo) list[0]).MaNhanVien;
            }
            return -1;
        }

        public int Insert(NhanVienInfo nhanvien)
        {
            int missingId = IdManager.GetMissingId("MaNhanVien", "NhanVien");
            nhanvien.MaNhanVien = missingId;
            string text = string.Concat(new object[] { "insert into NhanVien values(", nhanvien.MaNhanVien, ",'", MedicineController.StandardizeMedicineName(nhanvien.Ten), "','", nhanvien.HoVaTenDem, "','", nhanvien.SoCMT, "','", nhanvien.GhiChu, "')" });
            IDbCommand command = base.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InsertException();
            }
            command.Connection.Close();
            return nhanvien.MaNhanVien;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        public int Update(NhanVienInfo nhanvien)
        {
            string text = string.Concat(new object[] { "update NhanVien set Ten='", MedicineController.StandardizeMedicineName(nhanvien.Ten), "',HoVaTenDem='", nhanvien.HoVaTenDem, "',SoCMT='", nhanvien.SoCMT, "',GhiChu='", nhanvien.GhiChu, "' where MaNhanVien=", nhanvien.MaNhanVien });
            IDbCommand command = base.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new UpdateException();
            }
            command.Connection.Close();
            return nhanvien.MaNhanVien;
        }
    }
}
