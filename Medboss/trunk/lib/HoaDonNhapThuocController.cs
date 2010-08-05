namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    internal class HoaDonNhapThuocController : DataController, IFinder
    {
        public HoaDonNhapThuocController()
        {
            this.setFields();
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from HoaDonNhap";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                HoaDonNhapThuocInfo info = new HoaDonNhapThuocInfo();
                info.MaHoaDon = ConvertHelper.getInt(reader[0]);
                info.MaQuay = ConvertHelper.getInt(reader[1]);
                DateTime time = ConvertHelper.getDateTime(reader.GetValue(2));
                info.Ngay = time;
                info.MaKhachHang = Convert.ToInt32(reader[3]);
                info.TienNo = ConvertHelper.getInt(reader[4]);
                info.MaNhanVien = Convert.ToInt32(reader[5]);
                info.LoaiHoaDon = Convert.ToInt32(reader[6]);
                info.ChietKhau = ConvertHelper.getInt(reader[7]);
                info.ThucThu = ConvertHelper.getInt(reader[8]);
                info.MaHoaDonNCC = ConvertHelper.getString(reader[9]);
                list.Add(info);
            }
            reader.Close();
            foreach (HoaDonNhapThuocInfo info2 in list)
            {
                text = "select * from NhapThuocChiTiet where MaHoaDon=" + info2.MaHoaDon + " order by STT";
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    NhapThuocChiTietInfo info3 = new NhapThuocChiTietInfo();
                    info3.MaHoaDon = ConvertHelper.getInt(reader[0]);
                    info3.MaThuocTraoDoi = ConvertHelper.getInt(reader[1]);
                    info3.DonGiaNhap = ConvertHelper.getDouble(reader[2]);
                    info3.SoLuong = ConvertHelper.getInt(reader[3]);
                    info3.SoLo = reader.GetString(4);
                    DateTime time2 = ConvertHelper.getDateTime(reader.GetValue(5));
                    info3.HanDung = time2;
                    info3.GhiChu = reader.GetString(6);
                    info3.DonGiaBan = ConvertHelper.getInt(reader[7]);
                    info3.DaThanhToan = reader.GetBoolean(8);
                    info3.STT = ConvertHelper.getInt(reader[9]);
                    info2.HoaDonChiTiet.Add(info3);
                }
                reader.Close();
            }
            base.db.Command.Connection.Close();
            return list;
        }

        public int Delete(int MaHoaDon)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = "delete * from NhapThuocChiTiet where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from HoaDonNhap where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                LogManager.LogException(exception);
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return MaHoaDon;
        }

        public HoaDonNhapThuocInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaHoaDon=" + id);
            HoaDonNhapThuocInfo info = new HoaDonNhapThuocInfo();
            info.MaHoaDon = -1;
            if (list.Count > 0)
            {
                return (HoaDonNhapThuocInfo) list[0];
            }
            return info;
        }

        public HoaDonNhapThuocInfo getLatestImport(string TenThuoc, string DVT)
        {
            try
            {
                string text = "select top 1 HanDung,DonGiaNhap from qryNhapThuocChiTiet where TenThuoc='" + TenThuoc + "' and DVT='" + DVT + "' order by Ngay desc";
                base.db.Command.CommandText = text;
                IDataReader reader = base.db.Command.ExecuteReader();
                HoaDonNhapThuocInfo info = new HoaDonNhapThuocInfo();
                while (reader.Read())
                {
                    NhapThuocChiTietInfo info2 = new NhapThuocChiTietInfo();
                    info2.HanDung = ConvertHelper.getDateTime(reader.GetValue(0));
                    info2.DonGiaNhap = ConvertHelper.getDouble(reader[1]);
                    info.HoaDonChiTiet.Add(info2);
                }
                reader.Close();
                base.db.Command.Connection.Close();
                return info;
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
            return new HoaDonNhapThuocInfo();
        }

        public int Insert(HoaDonNhapThuocInfo imOrder)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int missingId = -1;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                missingId = IdManager.GetMissingId("MaHoaDon", "HoaDonNhap");
                imOrder.MaHoaDon = missingId;
                string text = string.Concat(new object[] { 
                    "insert into HoaDonNhap values(", imOrder.MaHoaDon, ",", imOrder.MaQuay, ",#", imOrder.Ngay.ToString("MM/dd/yyyy"), "#,", imOrder.MaKhachHang, ",", imOrder.TienNo, ",", imOrder.MaNhanVien, ",", imOrder.LoaiHoaDon, ",", imOrder.ChietKhau, 
                    ",", imOrder.ThucThu, ",'", imOrder.MaHoaDonNCC, "'", ")"
                 });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (NhapThuocChiTietInfo info2 in imOrder.HoaDonChiTiet)
                {
                    string text2 = "null";
                    if (info2.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info2.HanDung + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into NhapThuocChiTiet values(", imOrder.MaHoaDon, ",", info2.MaThuocTraoDoi, ",", info2.DonGiaNhap, ",", info2.SoLuong, ",'", info2.SoLo, "',", text2, ",'", info2.GhiChu, "',", info2.DonGiaBan, 
                        ",", info2.DaThanhToan, ",", info2.STT, ")"
                     });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        throw new InsertException();
                    }
                }
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                LogManager.LogException(exception);
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            try
            {
                foreach (NhapThuocChiTietInfo info2 in imOrder.HoaDonChiTiet)
                {
                    new MedicineController().UpdateDonGiaNhapBan(info2.MaThuocTraoDoi, imOrder.MaQuay, info2.DonGiaNhap, info2.DonGiaBan);
                }
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
            return missingId;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryNhapThuocChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryNhapThuocChiTiet");
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
            FindParam doitac = new FindParam();
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

                    case FindKeyParam.DoiTac:
                        doitac = param4;
                        break;
                }
            }
            if (param2.value.ToString() != "*")
            {
                obj2 = text;
                text = string.Concat(new object[] { obj2, " TenQuay like '", param2.value, "' and " });
            }
            if (doitac.value is string)
            {
                text = text + " TenKhachHang like '" + doitac.value.ToString() + "' and ";
            }
            obj2 = text;
            return string.Concat(new object[] { obj2, " Ngay between #", param.value, "# and #", param3.value, "#" });
        }

        protected override void setFields()
        {
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaHoaDon", "M\x00e3 ho\x00e1 đơn", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGiaNhap", "Đơn gi\x00e1 nhập", typeof(int)), new FindField("DonGiaBan", "Đơn gi\x00e1 b\x00e1n", typeof(int)), new FindField("ThucThu", "Thực chi", typeof(int)), new FindField("TenKhachHang", "Nh\x00e0 cung cấp", typeof(string)), new FindField("ChietKhau", "Chiết khấu", typeof(int)), new FindField("TienNo", "Tiền nợ", typeof(int)), new FindField("HanDung", "Hạn d\x00f9ng", typeof(DateTime)), new FindField("SoLo", "Số l\x00f4", typeof(string)), new FindField("GhiChu", "Ghi ch\x00fa", typeof(string)) };
        }

        public int Update(HoaDonNhapThuocInfo imOrder)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { 
                    "update HoaDonNhap set Ngay=#", imOrder.Ngay.ToString("MM/dd/yyyy"), "#, MaQuay=", imOrder.MaQuay, ",MaKhachHang=", imOrder.MaKhachHang, ", TienNo=", imOrder.TienNo, ", MaNhanVien=", imOrder.MaNhanVien, ", LoaiHoaDon=", imOrder.LoaiHoaDon, ", ChietKhau=", imOrder.ChietKhau, ", MaHoaDonNCC='", imOrder.MaHoaDonNCC, 
                    "' where MaHoaDon=", imOrder.MaHoaDon
                 });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from NhapThuocChiTiet where MaHoaDon=" + imOrder.MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                foreach (NhapThuocChiTietInfo info in imOrder.HoaDonChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into NhapThuocChiTiet values(", imOrder.MaHoaDon, ",", info.MaThuocTraoDoi, ",", info.DonGiaNhap, ",", info.SoLuong, ",'", info.SoLo, "',", text2, ",'", info.GhiChu, "',", info.DonGiaBan, 
                        ",", info.DaThanhToan, ",", info.STT, ")"
                     });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        throw new InsertException();
                    }
                }
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                LogManager.LogException(exception);
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return imOrder.MaHoaDon;
        }
    }
}
