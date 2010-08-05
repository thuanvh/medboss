namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    internal class HoaDonThanhToanController : DataController, IFinder
    {
        public HoaDonThanhToanController()
        {
            this.setFields();
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from HoaDonThanhToan";
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
                info.MaQuay = (byte) ConvertHelper.getInt(reader[1]);
                DateTime time = ConvertHelper.getDateTime(reader.GetValue(2));
                info.Ngay = time;
                info.MaKhachHang = Convert.ToInt32(reader[3]);
                info.TienNo = ConvertHelper.getInt(reader[4]);
                info.MaNhanVien = Convert.ToInt32(reader[5]);
                info.LoaiHoaDon = Convert.ToInt32(reader[6]);
                info.ChietKhau = ConvertHelper.getInt(reader[7]);
                info.ThucThu = ConvertHelper.getInt(reader[8]);
                list.Add(info);
            }
            reader.Close();
            foreach (HoaDonNhapThuocInfo info2 in list)
            {
                text = "select * from ThanhToanChiTiet where MaHoaDon=" + info2.MaHoaDon + " order by STT";
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
            IDbTransaction trans = base.db.Connection.BeginTransaction();
            IDbCommand cmd = base.db.Command;
            cmd.Transaction = trans;
            try
            {
                try
                {
                    string qrydelchitiet = "delete from HoaDonThanhToanChiTiet where MaThanhToan=" + MaHoaDon;
                    cmd.CommandText = qrydelchitiet;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    string qrydelhoadon = "delete from HoaDonThanhToan where MaThanhToan=" + MaHoaDon;
                    cmd.CommandText = qrydelhoadon;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    LogManager.LogException(exc);
                }
            }
            finally
            {
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

        public HoaDonThanhToanInfo GetHoaDonThanhToan(int MaHoaDon)
        {
            HoaDonThanhToanInfo hdtt = new HoaDonThanhToanInfo();
            IDataReader dr = null;
            IDbCommand command = base.db.Connection.CreateCommand();
            try
            {
                try
                {
                    string qry = "select MaThanhToan,Ngay,MaHoaDonNhap,MaNhanVien,MaKhachHang,MaQuay from HoaDonThanhToan where MaThanhToan=" + MaHoaDon;
                    command.CommandText = qry;
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        hdtt.MaThanhToan = ConvertHelper.getInt(dr[0]);
                        hdtt.NgayThanhToan = ConvertHelper.getDateTime(dr[1]);
                        hdtt.MaHoaDonNhap = ConvertHelper.getInt(dr[2]);
                        hdtt.MaNhanVien = ConvertHelper.getInt(dr[3]);
                        hdtt.MaKhachHang = ConvertHelper.getInt(dr[4]);
                        hdtt.MaQuay = ConvertHelper.getInt(dr[5]);
                    }
                    dr.Close();
                    qry = "select MaThanhToan,MaThuocTraoDoi,SoLuong,DonGia,ChietKhau,TienChietKhau,NgayNhap from HoaDonThanhToanChiTiet where MaThanhToan=" + MaHoaDon;
                    command.CommandText = qry;
                    if (hdtt.ThanhToanChiTiet != null)
                    {
                        hdtt.ThanhToanChiTiet = new ArrayList();
                    }
                    dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        ThanhToanChiTietInfo ttct = new ThanhToanChiTietInfo();
                        ttct.MaThanhToan = ConvertHelper.getInt(dr[0]);
                        ttct.MaThuocTraoDoi = ConvertHelper.getInt(dr[1]);
                        ttct.SoLuong = ConvertHelper.getInt(dr[2]);
                        ttct.DonGia = ConvertHelper.getInt(dr[3]);
                        ttct.ChietKhau = ConvertHelper.getInt(dr[4]);
                        ttct.TienChietKhau = ConvertHelper.getInt(dr[5]);
                        ttct.NgayNhap = ConvertHelper.getDateTime(dr[6]);
                        hdtt.ThanhToanChiTiet.Add(ttct);
                    }
                    dr.Close();
                }
                catch (Exception exc)
                {
                    LogManager.LogException(exc);
                }
            }
            finally
            {
            }
            return hdtt;
        }

        public IDataReader GetHoaDonThanhToanChiTiet(int MaHoaDon)
        {
            IDbCommand command = base.db.Connection.CreateCommand();
            try
            {
                try
                {
                    string qry = "select * from qryCongNo_ThanhToanChiTiet where MaThanhToan=" + MaHoaDon;
                    command.CommandText = qry;
                    return command.ExecuteReader();
                }
                catch (Exception exc)
                {
                    LogManager.LogException(exc);
                }
            }
            finally
            {
            }
            return null;
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

        public IDataReader GetThongTinChuaTT(int MaHoaDon)
        {
            IDbCommand cmd = base.db.Command;
            string text = "execute qryHoaDonChuaThanhToan " + MaHoaDon;
            cmd.CommandText = text;
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteReader();
        }

        public IDataReader GetThongTinThanhToanChiTiet(int MaHoaDon)
        {
            IDbCommand command = base.db.Connection.CreateCommand();
            try
            {
                try
                {
                    string qry = "select * from qryThanhToanHoaDonNhapChiTiet where MaHoaDonNhap=" + MaHoaDon;
                    command.CommandText = qry;
                    return command.ExecuteReader();
                }
                catch (Exception exc)
                {
                    LogManager.LogException(exc);
                }
            }
            finally
            {
            }
            return null;
        }

        public IDataReader GetThongTinTraLaiChiTiet(int MaHoaDon)
        {
            IDbCommand command = base.db.Connection.CreateCommand();
            try
            {
                try
                {
                    string qry = "select * from qryTraLaiNhapChiTiet where MaHoaDon=" + MaHoaDon;
                    command.CommandText = qry;
                    return command.ExecuteReader();
                }
                catch (Exception exc)
                {
                    LogManager.LogException(exc);
                }
            }
            finally
            {
            }
            return null;
        }

        public int Insert(HoaDonThanhToanInfo hdtt)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int missingId = -1;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                missingId = IdManager.GetMissingId("MaThanhToan", "HoaDonThanhToan");
                hdtt.MaThanhToan = missingId;
                string text = string.Concat(new object[] { "insert into HoaDonThanhToan values(", hdtt.MaThanhToan, ",#", hdtt.NgayThanhToan.ToString("MM/dd/yyyy"), "#,", hdtt.MaHoaDonNhap, ",", hdtt.MaNhanVien, ",", hdtt.MaKhachHang, ",", hdtt.MaQuay, ")" });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (ThanhToanChiTietInfo ttct in hdtt.ThanhToanChiTiet)
                {
                    text = string.Concat(new object[] { 
                        "insert into HoaDonThanhToanChiTiet values(", hdtt.MaThanhToan, ",", ttct.MaThuocTraoDoi, ",", ttct.SoLuong, ",", ttct.DonGia, ",", ttct.ChietKhau, ",", ttct.TienChietKhau, ",#", ttct.NgayNhap.ToString("MM/dd/yyyy"), "#,", ttct.STT, 
                        ")"
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
            }
            finally
            {
                connection.Close();
            }
            return missingId;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryCongNo_ThanhToanChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryCongNo_ThanhToanChiTiet");
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
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaThanhToan", "M\x00e3 ho\x00e1 đơn", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGia", "Đơn gi\x00e1 nhập", typeof(int)), new FindField("TenKhachHang", "Nh\x00e0 cung cấp", typeof(string)), new FindField("ChietKhau", "Chiết khấu", typeof(int)), new FindField("TienChietKhau", "Tiền chiết khấu", typeof(int)), new FindField("NgayNhap", "Ng\x00e0y nhập", typeof(DateTime)) };
        }

        public int Update(HoaDonThanhToanInfo hdtt)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                string text = string.Concat(new object[] { "update HoaDonThanhToan set Ngay=#", hdtt.NgayThanhToan.ToString("MM/dd/yyyy"), "#,MaHoaDonNhap=", hdtt.MaHoaDonNhap, ",MaQuay=", hdtt.MaQuay, ",MaNhanVien=", hdtt.MaNhanVien, ",MaKhachHang=", hdtt.MaKhachHang, " where MaThanhToan=", hdtt.MaThanhToan });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                text = "delete from HoaDonThanhToanChiTiet where MaThanhToan=" + hdtt.MaThanhToan;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (ThanhToanChiTietInfo ttct in hdtt.ThanhToanChiTiet)
                {
                    text = string.Concat(new object[] { 
                        "insert into HoaDonThanhToanChiTiet values(", hdtt.MaThanhToan, ",", ttct.MaThuocTraoDoi, ",", ttct.SoLuong, ",", ttct.DonGia, ",", ttct.ChietKhau, ",", ttct.TienChietKhau, ",#", ttct.NgayNhap.ToString("MM/dd/yyyy"), "#,", ttct.STT, 
                        ")"
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
            }
            finally
            {
                connection.Close();
            }
            return hdtt.MaThanhToan;
        }
    }
}
