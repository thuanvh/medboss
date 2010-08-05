namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    internal class HoaDonBanThuocController : DataController, IFinder
    {
        public HoaDonBanThuocController()
        {
            this.setFields();
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from HoaDonBan";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                HoaDonBanThuocInfo info = new HoaDonBanThuocInfo();
                info.MaHoaDon = ConvertHelper.getInt(reader[0]);
                info.MaQuay = ConvertHelper.getInt(reader[1]);
                info.Ngay = ConvertHelper.getDateTime(reader.GetValue(2));
                info.MaKhachHang = ConvertHelper.getInt(reader[3]);
                info.TienNo = ConvertHelper.getInt(reader[4]);
                info.MaNhanVien = ConvertHelper.getInt(reader[5]);
                info.LoaiHoaDon = ConvertHelper.getInt(reader[6]);
                info.ChietKhau = ConvertHelper.getInt(reader[7]);
                info.ThucThu = ConvertHelper.getInt(reader.GetValue(8));
                list.Add(info);
            }
            reader.Close();
            foreach (HoaDonBanThuocInfo info2 in list)
            {
                text = "select * from BanThuocChiTiet where BanThuocChiTiet.MaHoaDon=" + info2.MaHoaDon + " order by STT";
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    BanThuocChiTietInfo info3 = new BanThuocChiTietInfo();
                    info3.MaHoaDon = ConvertHelper.getInt(reader[0]);
                    info3.MaThuocTraoDoi = ConvertHelper.getInt(reader[1]);
                    info3.DonGiaBan = ConvertHelper.getInt(reader[2]);
                    info3.SoLuong = ConvertHelper.getInt(reader[3]);
                    info3.GhiChu = reader.GetString(4);
                    info3.STT = ConvertHelper.getInt(reader[5]);
                    info2.HoaDonChiTiet.Add(info3);
                }
                reader.Close();
            }
            base.db.Connection.Close();
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
                string text = "delete * from BanThuocChiTiet where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from HoaDonBan where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new DeleteException();
            }
            finally
            {
                connection.Close();
            }
            return MaHoaDon;
        }

        public HoaDonBanThuocInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaHoaDon=" + id);
            HoaDonBanThuocInfo info = new HoaDonBanThuocInfo();
            info.MaHoaDon = -1;
            if (list.Count > 0)
            {
                return (HoaDonBanThuocInfo) list[0];
            }
            return info;
        }

        public int Insert(HoaDonBanThuocInfo buyOrder)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int missingId = -1;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                missingId = IdManager.GetMissingId("MaHoaDon", "HoaDonBan");
                buyOrder.MaHoaDon = missingId;
                string text = string.Concat(new object[] { 
                    "insert into HoaDonBan values(", buyOrder.MaHoaDon, ",", buyOrder.MaQuay, ",#", buyOrder.Ngay.ToString("MM/dd/yyyy"), "#,", buyOrder.MaKhachHang, ",", buyOrder.TienNo, ",", buyOrder.MaNhanVien, ",", buyOrder.LoaiHoaDon, ",", buyOrder.ChietKhau, 
                    ",", buyOrder.ThucThu, ")"
                 });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (BanThuocChiTietInfo info2 in buyOrder.HoaDonChiTiet)
                {
                    text = string.Concat(new object[] { "insert into BanThuocChiTiet values(", buyOrder.MaHoaDon, ",", info2.MaThuocTraoDoi, ",", info2.DonGiaBan, ",", info2.SoLuong, ",'", info2.GhiChu, "',", info2.ChietKhau, ",", info2.STT, ")" });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        throw new InsertException();
                    }
                }
                transaction.Commit();
            }
            catch (InsertException exception)
            {
                transaction.Rollback();
                LogManager.LogException(exception);
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            foreach (BanThuocChiTietInfo info2 in buyOrder.HoaDonChiTiet)
            {
                new MedicineController().UpdateDonGiaBan(info2.MaThuocTraoDoi, buyOrder.MaQuay, info2.DonGiaBan);
            }
            return missingId;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryBanThuocChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryBanThuocChiTiet");
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
            if (((string) param2.value) != "*")
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
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaHoaDon", "M\x00e3 ho\x00e1 đơn", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGiaBan", "Đơn gi\x00e1 b\x00e1n", typeof(int)), new FindField("ThucThu", "Thực thu", typeof(int)), new FindField("TenKhachHang", "Kh\x00e1ch h\x00e0ng", typeof(string)), new FindField("ChietKhau", "Chiết khấu", typeof(int)), new FindField("TienNo", "Tiền nợ", typeof(int)) };
        }

        public int Update(HoaDonBanThuocInfo buyOrder)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { 
                    "update HoaDonBan set Ngay=#", buyOrder.Ngay.ToString("MM/dd/yyyy"), "#, MaQuay=", buyOrder.MaQuay, ",MaKhachHang=", buyOrder.MaKhachHang, ", TienNo=", buyOrder.TienNo, ", MaNhanVien=", buyOrder.MaNhanVien, ", LoaiHoaDon=", buyOrder.LoaiHoaDon, ", ChietKhau=", buyOrder.ChietKhau, ", ThucThu=", buyOrder.ThucThu, 
                    " where MaHoaDon=", buyOrder.MaHoaDon
                 });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from BanThuocChiTiet where MaHoaDon=" + buyOrder.MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                foreach (BanThuocChiTietInfo info in buyOrder.HoaDonChiTiet)
                {
                    text = string.Concat(new object[] { "insert into BanThuocChiTiet values(", buyOrder.MaHoaDon, ",", info.MaThuocTraoDoi, ",", info.DonGiaBan, ",", info.SoLuong, ",'", info.GhiChu, "',", info.ChietKhau, ",", info.STT, ")" });
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
            return buyOrder.MaHoaDon;
        }
    }
}
