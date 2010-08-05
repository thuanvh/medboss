namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    internal class ChuyenQuayController : DataController, IFinder
    {
        public ChuyenQuayController()
        {
            this.setFields();
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from ChuyenQuay";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                ChuyenQuayInfo info = new ChuyenQuayInfo();
                info.MaChuyen = ConvertHelper.getInt(reader[0]);
                info.MaQuayXuat = ConvertHelper.getInt(reader[1]);
                info.MaQuayNhan = ConvertHelper.getInt(reader[2]);
                info.Ngay = ConvertHelper.getDateTime(reader.GetValue(3));
                info.MaNhanVien = ConvertHelper.getInt(reader[4]);
                list.Add(info);
            }
            reader.Close();
            foreach (ChuyenQuayInfo info2 in list)
            {
                text = "select * from ChuyenQuayChiTiet where ChuyenQuayChiTiet.MaChuyen = " + info2.MaChuyen + " order by STT";
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    ChuyenQuayChiTietInfo info3 = new ChuyenQuayChiTietInfo();
                    info3.MaChuyen = ConvertHelper.getInt(reader["MaChuyen"]);
                    info3.MaThuocTraoDoi = ConvertHelper.getInt(reader["MaThuocTraoDoi"]);
                    info3.SoLuong = ConvertHelper.getInt(reader["SoLuong"]);
                    info3.DonGiaBan = ConvertHelper.getInt(reader["DonGiaBan"]);
                    info3.HanDung = ConvertHelper.getDateTime(reader["HanDung"]);
                    info3.SoLo = ConvertHelper.getString(reader["SoLo"]);
                    info3.GhiChu = ConvertHelper.getString(reader["GhiChu"]);
                    info3.STT = ConvertHelper.getInt(reader["STT"]);
                    info2.ChuyenQuayChiTiet.Add(info3);
                }
                reader.Close();
            }
            base.db.Command.Connection.Close();
            return list;
        }

        public int Delete(int MaChuyen)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = "delete * from ChuyenQuayChiTiet where MaChuyen=" + MaChuyen;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from ChuyenQuay where MaChuyen=" + MaChuyen;
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
            return MaChuyen;
        }

        public ChuyenQuayInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaChuyen=" + id);
            ChuyenQuayInfo info = new ChuyenQuayInfo();
            info.MaChuyen = -1;
            if (list.Count > 0)
            {
                return (ChuyenQuayInfo) list[0];
            }
            return info;
        }

        public int Insert(ChuyenQuayInfo cQuayInfo)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            int missingId = -1;
            try
            {
                missingId = IdManager.GetMissingId("MaChuyen", "ChuyenQuay");
                cQuayInfo.MaChuyen = missingId;
                string text = string.Concat(new object[] { "insert into ChuyenQuay values(", cQuayInfo.MaChuyen, ",", cQuayInfo.MaQuayXuat, ",", cQuayInfo.MaQuayNhan, ",#", cQuayInfo.Ngay.ToString("MM/dd/yyyy"), "#,", cQuayInfo.MaNhanVien, ")" });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new InsertException();
                }
                foreach (ChuyenQuayChiTietInfo info in cQuayInfo.ChuyenQuayChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung.ToString("MM/dd/yyyy") + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into ChuyenQuayChiTiet values(", cQuayInfo.MaChuyen, ",", info.MaThuocTraoDoi, ",", info.SoLuong, ",", info.DonGiaBan, ",", text2, ",'", info.SoLo, "','", info.GhiChu, "',", info.STT, 
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
            return base._find(condition, "qryChuyenQuay");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryChuyenQuay");
        }

        FindField[] IFinder.getFields()
        {
            return base.fields;
        }

        private string para2str(FindParam[] findParams)
        {
            string text3;
            string text = "";
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < findParams.Length; i++)
            {
                hashtable.Add(findParams[i].key, findParams[i].value);
            }
            if ((bool) hashtable[FindKeyParam.MotChieu])
            {
                text3 = text;
                text = text3 + " TenQuayXuat like '" + ((string) hashtable[FindKeyParam.TuQuay]) + "' and TenQuayNhan like '" + ((string) hashtable[FindKeyParam.ToiQuay]) + "' and ";
            }
            else
            {
                text3 = text;
                text = text3 + "(( TenQuayXuat like '" + ((string) hashtable[FindKeyParam.TuQuay]) + "' and TenQuayNhan like '" + ((string) hashtable[FindKeyParam.ToiQuay]) + "') or ( TenQuayXuat like '" + ((string) hashtable[FindKeyParam.ToiQuay]) + "' and TenQuayNhan like '" + ((string) hashtable[FindKeyParam.TuQuay]) + "')) and ";
            }
            text3 = text;
            return (text3 + " Ngay between #" + ((string) hashtable[FindKeyParam.TuNgay]) + "# and #" + ((string) hashtable[FindKeyParam.ToiNgay]) + "#");
        }

        protected override void setFields()
        {
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaChuyen", "M\x00e3 chuyển", typeof(int)), new FindField("TenQuayXuat", "Quầy xuất", typeof(string)), new FindField("TenQuayNhan", "Quầy nhận", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("TenThuoc", "T\x00ean thuốc", typeof(string)), new FindField("DVT", "ĐVT", typeof(string)), new FindField("SoLuong", "Số lượng", typeof(int)), new FindField("DonGiaBan", "Đơn gi\x00e1 b\x00e1n", typeof(int)), new FindField("HanDung", "Hạn d\x00f9ng", typeof(DateTime)), new FindField("SoLo", "Số l\x00f4", typeof(string)), new FindField("GhiChu", "Ghi ch\x00fa", typeof(string)) };
        }

        public int Update(ChuyenQuayInfo cQuayInfo)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { "update ChuyenQuay set MaQuayXuat=", cQuayInfo.MaQuayXuat, ", MaQuayNhan=", cQuayInfo.MaQuayNhan, ", MaNhanVien=", cQuayInfo.MaNhanVien, ", Ngay=#", cQuayInfo.Ngay.ToString("MM/dd/yyyy"), "# where MaChuyen=", cQuayInfo.MaChuyen });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from ChuyenQuayChiTiet where MaChuyen=" + cQuayInfo.MaChuyen;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                foreach (ChuyenQuayChiTietInfo info in cQuayInfo.ChuyenQuayChiTiet)
                {
                    string text2 = "null";
                    if (info.HanDung != DateTime.MinValue)
                    {
                        text2 = "#" + info.HanDung + "#";
                    }
                    text = string.Concat(new object[] { 
                        "insert into ChuyenQuayChiTiet values(", cQuayInfo.MaChuyen, ",", info.MaThuocTraoDoi, ",", info.SoLuong, ",", info.DonGiaBan, ",", text2, ",'", info.SoLo, "','", info.GhiChu, "',", info.STT, 
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
            catch (Exception)
            {
                transaction.Rollback();
                throw new UpdateException();
            }
            finally
            {
                connection.Close();
            }
            return cQuayInfo.MaChuyen;
        }
    }
}
