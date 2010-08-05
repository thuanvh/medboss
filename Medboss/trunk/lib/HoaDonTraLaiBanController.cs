namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    internal class HoaDonTraLaiBanController : DataController, IFinder
    {
        public bool ThuHayChiSearchParam;

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from HoaDonBanTraLai";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                HoaDonTraLaiInfo info = new HoaDonTraLaiInfo();
                info.MaHoaDon = ConvertHelper.getInt(reader[0]);
                info.MaQuay = ConvertHelper.getInt(reader[1]);
                info.Ngay = ConvertHelper.getDateTime(reader.GetValue(2));
                info.HoaDonNhapBan = ConvertHelper.getInt(reader[3]);
                info.GhiChu = reader.GetString(4);
                info.MaNhanVien = (byte) ConvertHelper.getInt(reader[5]);
                info.TienTraLai = ConvertHelper.getInt(reader[6]);
                info.ThuHayChi = reader.GetBoolean(7);
                info.MaKhachHang = ConvertHelper.getInt(reader[8]);
                list.Add(info);
            }
            reader.Close();
            foreach (HoaDonTraLaiInfo info2 in list)
            {
                text = "select MaHoaDon,MaThuocTraoDoi,SoLuong,DonGia,HanDung,SoLo,GhiChu,NgayNhap,STT from HoaDonBanTraLaiChiTiet where MaHoaDon=" + info2.MaHoaDon + " order by STT";
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    TraLaiChiTietInfo info3 = new TraLaiChiTietInfo();
                    info3.MaHoaDon = ConvertHelper.getInt(reader[0]);
                    info3.MaThuocTraoDoi = ConvertHelper.getInt(reader[1]);
                    info3.SoLuong = ConvertHelper.getInt(reader[2]);
                    info3.DonGia = ConvertHelper.getInt(reader[3]);
                    info3.HanDung = ConvertHelper.getDateTime(reader.GetValue(4));
                    info3.SoLo = ConvertHelper.getString(reader.GetValue(5));
                    info3.GhiChu = ConvertHelper.getString(reader.GetValue(6));
                    info3.NgayNhap = ConvertHelper.getDateTime(reader.GetValue(7));
                    info3.STT = ConvertHelper.getInt(reader.GetValue(8));
                    info2.TraLaiChiTiet.Add(info3);
                }
                reader.Close();
            }
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
                string text = "delete * from HoaDonBanTraLaiChiTiet where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from HoaDonBanTraLai where MaHoaDon=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                transaction.Rollback();
                LogManager.LogException(exc);
                throw new DeleteException();
            }
            finally
            {
                connection.Close();
            }
            return MaHoaDon;
        }

        public HoaDonTraLaiInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaHoaDon=" + id);
            HoaDonTraLaiInfo info = new HoaDonTraLaiInfo();
            info.MaHoaDon = -1;
            if (list.Count > 0)
            {
                return (HoaDonTraLaiInfo) list[0];
            }
            return info;
        }

        public int Insert(HoaDonTraLaiInfo ordTraLai)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int missingId = -1;
            try
            {
                missingId = IdManager.GetMissingId("MaHoaDon", "HoaDonBanTraLai");
                ordTraLai.MaHoaDon = missingId;
                string text = string.Concat(new object[] { 
                    "insert into HoaDonBanTraLai values(", ordTraLai.MaHoaDon, ",", ordTraLai.MaQuay, ",#", ordTraLai.Ngay.ToString("MM/dd/yyyy"), "#,", ordTraLai.HoaDonNhapBan, ",'", ordTraLai.GhiChu, "',", ordTraLai.MaNhanVien, ",", ordTraLai.TienTraLai, ",", ordTraLai.ThuHayChi, 
                    ",", ordTraLai.MaKhachHang, ")"
                 });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (TraLaiChiTietInfo info in ordTraLai.TraLaiChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung.ToString("MM/dd/yyyy") + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into HoaDonBanTraLaiChiTiet values(", ordTraLai.MaHoaDon, ",", info.MaThuocTraoDoi, ",", info.SoLuong, ",", info.DonGia, ",", text2, ",'", info.SoLo, "','", info.GhiChu, "',#", info.NgayNhap.ToString("MM/dd/yyyy"), 
                        "#,", info.STT, ")"
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
            return missingId;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryTraLaiChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryTraLaiChiTiet");
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
            return (string.Concat(new object[] { obj2, " Ngay between #", param.value, "# and #", param3.value, "#" }) + " and ThuHayChi=true").ToString();
        }

        protected override void setFields()
        {
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaHoaDon", "M\x00e3 ho\x00e1 đơn", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGia", "Đơn gi\x00e1", typeof(int)), new FindField("TienTraLai", "Tiền trả lại", typeof(int)), new FindField("GhiChu", "Ghi ch\x00fa", typeof(string)) };
        }

        public int Update(HoaDonTraLaiInfo traLaiInfo)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { "update HoaDonBanTraLai set MaQuay=", traLaiInfo.MaQuay, ",MaKhachHang=", traLaiInfo.MaKhachHang, ", Ngay=#", traLaiInfo.Ngay.ToString("MM/dd/yyyy"), "#, MaNhanVien=", traLaiInfo.MaNhanVien, " where MaHoaDon=", traLaiInfo.MaHoaDon });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from HoaDonBanTraLaiChiTiet where MaHoaDon=" + traLaiInfo.MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                foreach (TraLaiChiTietInfo info in traLaiInfo.TraLaiChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung.ToString("MM/dd/yyyy") + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into HoaDonBanTraLaiChiTiet values(", traLaiInfo.MaHoaDon, ",", info.MaThuocTraoDoi, ",", info.SoLuong, ",", info.DonGia, ",", text2, ",'", info.SoLo, "','", info.GhiChu, "',#", info.NgayNhap.ToString("MM/dd/yyyy"), 
                        "#,", info.STT, ")"
                     });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() <= 0)
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
                throw new UpdateException();
            }
            finally
            {
                connection.Close();
            }
            return traLaiInfo.MaHoaDon;
        }
    }
}
