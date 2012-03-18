namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    public class KiemKeController : DataController, IFinder
    {
        public KiemKeController()
        {
            base.db = new DBDriver();
        }

        public KiemKeController(DBDriver dbms)
        {
            base.db = dbms;
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from KiemKeThuoc";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                KiemKeInfo info = new KiemKeInfo();
                info.MaKiemKe = ConvertHelper.getInt(reader[0]);
                info.Ngay = reader.GetDateTime(1);
                info.MaNhanVien = ConvertHelper.getInt(reader.GetValue(2));
                info.MaQuay = ConvertHelper.getInt(reader[3]);
                info.LoaiKiemKe = new KiemKeController().GetLoaiKiemKeById(ConvertHelper.getInt(reader.GetValue(4)));
                list.Add(info);
            }
            reader.Close();
            foreach (KiemKeInfo info2 in list)
            {
                text = "select * from KiemKeChiTiet where MaKiemKe=" + info2.MaKiemKe + " order by STT";
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    KiemKeChiTietInfo info3 = new KiemKeChiTietInfo();
                    info3.MaKiemKe = ConvertHelper.getInt(reader.GetValue(0));
                    info3.MaThuocTraoDoi = ConvertHelper.getInt(reader.GetValue(1));
                    info3.TinhTrang = ConvertHelper.getString(reader.GetValue(2));
                    info3.SoLuong = ConvertHelper.getInt(reader.GetValue(3));
                    info3.HanDung = ConvertHelper.getDateTime(reader.GetValue(4));
                    info3.GhiChu = ConvertHelper.getString(reader.GetValue(5));
                    info3.DonGiaNhap = ConvertHelper.getInt(reader.GetValue(6));
                    info3.DonGiaBan = ConvertHelper.getInt(reader.GetValue(7));
                    info2.KiemKeChiTiet.Add(info3);
                }
                reader.Close();
            }
            base.db.Connection.Close();
            return list;
        }

        public int Delete(LoaiKiemKeInfo lkki)
        {
            try
            {
                string text = "delete * from LoaiKiemKe where MaLoaiKiemKe=" + lkki.MaLoaiKiemKe;
                base.db.Command.CommandText = text;
                base.db.Command.ExecuteNonQuery();
                base.db.Command.Connection.Close();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                throw new DeleteException();
            }
            return lkki.MaLoaiKiemKe;
        }

        public int Delete(int MaKiemKe)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = "delete * from KiemKeChiTiet where MaKiemKe=" + MaKiemKe;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from KiemKeThuoc where MaKiemKe=" + MaKiemKe;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                transaction.Rollback();
                throw new DeleteException();
            }
            finally
            {
                connection.Close();
            }
            return MaKiemKe;
        }

        public KiemKeInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaKiemKe=" + id);
            KiemKeInfo info = new KiemKeInfo();
            info.MaKiemKe = -1;
            if (list.Count > 0)
            {
                return (KiemKeInfo) list[0];
            }
            return info;
        }

        public LoaiKiemKeInfo GetLoaiKiemKeById(int id)
        {
            LoaiKiemKeInfo info = new LoaiKiemKeInfo();
            string text = "select * from LoaiKiemKe where MaLoaiKiemKe=" + id;
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            if (reader.Read())
            {
                info.MaLoaiKiemKe = ConvertHelper.getInt(reader[0]);
                info.TenLoaiKiemKe = reader.GetString(1);
                info.Huy = reader.GetBoolean(2);
            }
            reader.Close();
            return info;
        }

        public int Insert(KiemKeInfo ki)
        {
            IDbTransaction transaction = base.db.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            int missingId = -1;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                missingId = IdManager.GetMissingId("MaKiemKe", "KiemKeThuoc");
                ki.MaKiemKe = missingId;
                string text = string.Concat(new object[] { "insert into KiemKeThuoc values(", ki.MaKiemKe, ",#", ki.Ngay.ToString("MM/dd/yyyy"), "#,", ki.MaNhanVien, ",", ki.MaQuay, ",", ki.LoaiKiemKe.MaLoaiKiemKe, ")" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (KiemKeChiTietInfo info2 in ki.KiemKeChiTiet)
                {
                    string text2 = "null";
                    if (info2.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info2.HanDung.ToString("MM/dd/yyyy") + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into KiemKeChiTiet values(", ki.MaKiemKe, ",", info2.MaThuocTraoDoi, ",'", info2.TinhTrang, "',", info2.SoLuong, ",", text2, ",'", info2.GhiChu, "',", info2.DonGiaNhap, ",", info2.DonGiaBan, 
                        ",", info2.STT, ",'", info2.SoLo, "')"
                     });
                    base.db.Command.CommandText = text;
                    if (base.db.Command.ExecuteNonQuery() <= 0)
                    {
                        throw new InsertException();
                    }
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                transaction.Rollback();
                throw new InsertException();
            }
            finally
            {
                base.db.Command.Connection.Close();
            }
            foreach (KiemKeChiTietInfo info2 in ki.KiemKeChiTiet)
            {
                new MedicineController().UpdateDonGiaNhapBan(info2.MaThuocTraoDoi, ki.MaQuay, (double) info2.DonGiaNhap, info2.DonGiaBan);
            }
            return missingId;
        }

        public int Insert(LoaiKiemKeInfo lkki)
        {
            int missingId;
            try
            {
                missingId = IdManager.GetMissingId("MaLoaiKiemKe", "LoaiKiemKe");
                string text = string.Concat(new object[] { "insert into LoaiKiemKe values(", missingId, ",'", lkki.TenLoaiKiemKe, "',", lkki.Huy, ")" });
                base.db.Command.CommandText = text;
                base.db.Command.ExecuteNonQuery();
                base.db.Command.Connection.Close();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                throw new InsertException();
            }
            return missingId;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        public ArrayList ListLoaiKiemKe()
        {
            ArrayList list = new ArrayList();
            string text = "select * from LoaiKiemKe";
            text = text + " order by TenLoaiKiemKe";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            while (reader.Read())
            {
                LoaiKiemKeInfo info = new LoaiKiemKeInfo();
                info.MaLoaiKiemKe = ConvertHelper.getInt(reader[0]);
                info.TenLoaiKiemKe = reader.GetString(1);
                info.Huy = reader.GetBoolean(2);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryKiemKeChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryKiemKeChiTiet");
        }

        FindField[] IFinder.getFields()
        {
            return base.fields;
        }

        private string para2str(FindParam[] findParams)
        {
            object obj2;
            string text = "";
            FindParam param = new FindParam();
            FindParam param2 = new FindParam();
            FindParam param3 = new FindParam();
            for (int i = 0; i < findParams.Length; i++)
            {
                FindParam param4 = findParams[i];
                switch (param4.key)
                {
                    case FindKeyParam.TuNgay:
                        param = param4;
                        break;

                    case FindKeyParam.ToiNgay:
                        param3 = param4;
                        break;

                    case FindKeyParam.Quay:
                        param2 = param4;
                        break;
                }
            }
            if (param2.value.ToString() != "*")
            {
                obj2 = text;
                text = string.Concat(new object[] { obj2, " TenQuay like '", param2.value, "' and " });
            }
            obj2 = text;
            return string.Concat(new object[] { obj2, " Ngay between #", param.value, "# and #", param3.value, "#" });
        }

        protected override void setFields()
        {
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaKiemKe", "M\x00e3 kiểm k\x00ea", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGiaNhap", "Đơn gi\x00e1 nhập", typeof(int)), new FindField("DonGiaBan", "Đơn gi\x00e1 b\x00e1n", typeof(int)), new FindField("HanDung", "Hạn d\x00f9ng", typeof(DateTime)), new FindField("GhiChu", "Ghi ch\x00fa", typeof(string)) };
        }

        public int Update(KiemKeInfo kiemKeInfo)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { "update KiemKeThuoc set Ngay=#", kiemKeInfo.Ngay.ToString("MM/dd/yyyy"), "#, MaQuay=", kiemKeInfo.MaQuay, ",MaNhanVien=", kiemKeInfo.MaNhanVien, ",MaLoaiKiemKe=", kiemKeInfo.LoaiKiemKe.MaLoaiKiemKe, " where MaKiemKe=", kiemKeInfo.MaKiemKe });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from KiemKeChiTiet where MaKiemKe=" + kiemKeInfo.MaKiemKe;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                foreach (KiemKeChiTietInfo info in kiemKeInfo.KiemKeChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into KiemKeChiTiet values(", kiemKeInfo.MaKiemKe, ",", info.MaThuocTraoDoi, ",'", info.TinhTrang, "',", info.SoLuong, ",", text2, ",'", info.GhiChu, "',", info.DonGiaNhap, ",", info.DonGiaBan, 
                        ",", info.STT, ",'", info.SoLo, "')"
                     });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        throw new UpdateException();
                    }
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                transaction.Rollback();
                LogManager.LogException(exc);
                throw new UpdateException();
            }
            finally
            {
                connection.Close();
            }
            return kiemKeInfo.MaKiemKe;
        }

        public int Update(LoaiKiemKeInfo lkki)
        {
            try
            {
                string text = string.Concat(new object[] { "update LoaiKiemKe set TenLoaiKiemKe='", lkki.TenLoaiKiemKe, "',Huy=", lkki.Huy, " where MaLoaiKiemKe=", lkki.MaLoaiKiemKe });
                base.db.Command.CommandText = text;
                base.db.Command.ExecuteNonQuery();
                base.db.Command.Connection.Close();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                throw new UpdateException();
            }
            return lkki.MaLoaiKiemKe;
        }
    }
}
