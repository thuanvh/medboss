namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    public class CSController
    {
        private DBDriver db = new DBDriver();

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from qryKhachHang";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            text = text + " order by Ten";
            this.db.Command.CommandText = text;
            IDataReader reader = this.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                CSInfo info = new CSInfo();
                info.MaKhachHang = ConvertHelper.getInt(reader[0]);
                info.Ten = reader.GetString(1);
                info.ChucVu = reader.GetString(2);
                info.CongTy = reader.GetString(3);
                info.DiaChi = reader.GetString(4);
                info.DienThoai = reader.GetString(5);
                info.LoaiKhachHang.MaLoai = ConvertHelper.getInt(reader.GetValue(6));
                info.LoaiKhachHang.TenLoai = ConvertHelper.getString(reader.GetValue(7));
                list.Add(info);
            }
            reader.Close();
            this.db.Command.Connection.Close();
            return list;
        }

        public int Delete(int maKhachHang)
        {
            string text = "delete from KhachHang where MaKhachHang=" + maKhachHang;
            IDbCommand command = this.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new DeleteException();
            }
            command.Connection.Close();
            return maKhachHang;
        }

        public int DeleteLoaiKhachHang(LoaiKhachHang loaiKh)
        {
            string text = "delete from LoaiDoiTac where MaLoai=" + loaiKh.MaLoai;
            this.db.Command.CommandText = text;
            if (this.db.Command.ExecuteNonQuery() < 0)
            {
                throw new DeleteException();
            }
            this.db.Command.Connection.Close();
            return loaiKh.MaLoai;
        }

        public ArrayList GetACCSGroups()
        {
            ArrayList list = new ArrayList();
            string text = "select * from LoaiDoiTac";
            text = text + " order by tenloai";
            this.db.Command.CommandText = text;
            IDataReader reader = this.db.Command.ExecuteReader();
            while (reader.Read())
            {
                LoaiKhachHang hang = new LoaiKhachHang();
                hang.MaLoai = ConvertHelper.getInt(reader[0]);
                hang.TenLoai = reader.GetString(1);
                list.Add(hang);
            }
            return list;
        }

        public CSInfo GetById(int maKhachHang)
        {
            ArrayList list = this.ConditionalList(" MaKhachHang=" + maKhachHang.ToString());
            CSInfo info = new CSInfo();
            info.MaKhachHang = -1;
            if (list.Count > 0)
            {
                return (CSInfo) list[0];
            }
            return info;
        }

        public LoaiKhachHang GetCSTypeById(int id)
        {
            string text = "select * from LoaiDoiTac where MaLoai=" + id.ToString();
            this.db.Command.CommandText = text;
            IDataReader reader = this.db.Command.ExecuteReader();
            LoaiKhachHang hang = new LoaiKhachHang();
            if (reader.Read())
            {
                hang.MaLoai = ConvertHelper.getInt(reader[0]);
                hang.TenLoai = reader.GetString(1);
            }
            reader.Close();
            return hang;
        }

        public int GetIdByName(string csName)
        {
            ArrayList list = this.ConditionalList("Ten like '" + MedicineController.StandardizeMedicineName(csName) + "'");
            if (list.Count > 0)
            {
                return ((CSInfo) list[0]).MaKhachHang;
            }
            return -1;
        }

        public int Insert(CSInfo KhachHang)
        {
            int missingId = IdManager.GetMissingId("MaKhachHang", "KhachHang");
            KhachHang.MaKhachHang = missingId;
            string text = string.Concat(new object[] { "insert into KhachHang values(", KhachHang.MaKhachHang, ",'", MedicineController.StandardizeMedicineName(KhachHang.Ten), "','", KhachHang.ChucVu, "','", KhachHang.CongTy, "','", KhachHang.DiaChi, "','", KhachHang.DienThoai, "',", KhachHang.LoaiKhachHang.MaLoai, ")" });
            IDbCommand command = this.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InsertException();
            }
            command.Connection.Close();
            return KhachHang.MaKhachHang;
        }

        public int InsertLoaiKhachHang(LoaiKhachHang loaiKK)
        {
            int missingId = IdManager.GetMissingId("MaLoai", "LoaiDoiTac");
            string text = string.Concat(new object[] { "insert into LoaiDoiTac values(", missingId, ",'", MedicineController.StandardizeMedicineName(loaiKK.TenLoai), "')" });
            this.db.Command.CommandText = text;
            if (this.db.Command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            this.db.Command.Connection.Close();
            return loaiKK.MaLoai;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        public int Update(CSInfo KhachHang)
        {
            string text = string.Concat(new object[] { "update KhachHang set Ten='", MedicineController.StandardizeMedicineName(KhachHang.Ten), "',ChucVu='", KhachHang.ChucVu, "',CongTy='", KhachHang.CongTy, "',DiaChi='", KhachHang.DiaChi, "', DienThoai='", KhachHang.DienThoai, "', MaNhom=", KhachHang.LoaiKhachHang.MaLoai, " where MaKhachHang=", KhachHang.MaKhachHang });
            IDbCommand command = this.db.Command;
            command.CommandText = text;
            if (command.ExecuteNonQuery() <= 0)
            {
                throw new UpdateException();
            }
            command.Connection.Close();
            return KhachHang.MaKhachHang;
        }

        public int UpdateLoaiKhachHang(LoaiKhachHang loaiKh)
        {
            string text = string.Concat(new object[] { "update LoaiDoiTac set TenLoai='", MedicineController.StandardizeMedicineName(loaiKh.TenLoai), "' where MaLoai=", loaiKh.MaLoai });
            this.db.Command.CommandText = text;
            if (this.db.Command.ExecuteNonQuery() < 0)
            {
                throw new UpdateException();
            }
            this.db.Command.Connection.Close();
            return loaiKh.MaLoai;
        }
    }
}
