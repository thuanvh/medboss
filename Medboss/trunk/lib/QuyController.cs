namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    internal class QuyController : DataController, IFinder
    {
        public QuyController()
        {
            this.setFields();
        }

        public ArrayList ConditionalList(string Condition)
        {
            string text = "select * from Quy";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                QuyInfo info = new QuyInfo();
                info.MaThuChi = ConvertHelper.getInt(reader[0]);
                info.Ngay = ConvertHelper.getDateTime(reader.GetValue(1));
                info.MaQuay = ConvertHelper.getInt(reader[2]);
                list.Add(info);
            }
            reader.Close();
            foreach (QuyInfo info2 in list)
            {
                text = "select * from KhoanThuChi where MaThuChi=" + info2.MaThuChi;
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    KhoanThuChiInfo info3 = new KhoanThuChiInfo();
                    info3.MaThuChi = ConvertHelper.getInt(reader[0]);
                    info3.DienGiai = reader.GetString(1);
                    info3.ThuHayChi = reader.GetBoolean(2);
                    info3.MaKhachHang = ConvertHelper.getInt(reader[3]);
                    info3.Tien = ConvertHelper.getInt(reader[4]);
                    info3.GhiChu = reader.GetString(5);
                    info3.MaLoaiThuChi = ConvertHelper.getInt(reader[6]);
                    info2.ThuChiChiTiet.Add(info3);
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
                string text = "delete * from KhoanThuChi where MaThuChi=" + MaHoaDon;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from Quy where MaThuChi=" + MaHoaDon;
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

        public QuyInfo GetById(int id)
        {
            ArrayList list = this.ConditionalList("MaThuChi=" + id);
            QuyInfo info = new QuyInfo();
            info.MaThuChi = -1;
            if (list.Count > 0)
            {
                return (QuyInfo) list[0];
            }
            return info;
        }

        public int Insert(QuyInfo qif)
        {
            IDbTransaction transaction = base.db.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
                int missingId = IdManager.GetMissingId("MaThuChi", "Quy");
                qif.MaThuChi = missingId;
                string text = string.Concat(new object[] { "insert into Quy values(", qif.MaThuChi, ",#", qif.Ngay.ToString("MM/dd/yyyy"), "#,", qif.MaQuay, ")" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new InsertException();
                }
                foreach (KhoanThuChiInfo info2 in qif.ThuChiChiTiet)
                {
                    text = string.Concat(new object[] { "insert into KhoanThuChi values(", qif.MaThuChi, ",'", info2.DienGiai, "',", info2.ThuHayChi, ",", info2.MaKhachHang, ",", info2.Tien, ",'", info2.GhiChu, "',", info2.MaLoaiThuChi, ")" });
                    base.db.Command.CommandText = text;
                    if (base.db.Command.ExecuteNonQuery() < 0)
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
                base.db.Command.Connection.Close();
            }
            return qif.MaThuChi;
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        DataTable IFinder.AdvanceFind(string condition)
        {
            return base._find(condition, "qryQuyChiTiet");
        }

        DataTable IFinder.Find(FindParam[] findParams)
        {
            string condition = this.para2str(findParams);
            return base._find(condition, "qryQuyChiTiet");
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
            base.fields = new FindField[] { new FindField("Ma", "Ma", typeof(int)), new FindField("MaThuChi", "M\x00e3 thu chi", typeof(int)), new FindField("TenQuay", "Quầy", typeof(string)), new FindField("Ngay", "Ng\x00e0y", typeof(DateTime)), new FindField("DienGiai", "Diễn giải", typeof(string)), new FindField("Tien", "Tiền", typeof(int)), new FindField("TenLoaiThuChi", "Loại thu chi", typeof(string)), new FindField("GhiChu", "Ghi ch\x00fa", typeof(string)) };
        }

        public int Update(QuyInfo quy)
        {
            IDbConnection connection = base.db.Connection;
            IDbCommand command = connection.CreateCommand();
            IDbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { "update Quy set MaQuay=", quy.MaQuay, ",Ngay=#", quy.Ngay.ToString("MM/dd/yyyy"), "# where MaThuChi=", quy.MaThuChi });
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                text = "delete * from KhoanThuChi where MaThuChi=" + quy.MaThuChi;
                command.CommandText = text;
                if (command.ExecuteNonQuery() <= 0)
                {
                    throw new UpdateException();
                }
                foreach (KhoanThuChiInfo info in quy.ThuChiChiTiet)
                {
                    text = string.Concat(new object[] { "insert into KhoanThuChi values(", quy.MaThuChi, ",'", info.DienGiai, "',", info.ThuHayChi, ",", info.MaKhachHang, ",", info.Tien, ",'", info.GhiChu, "',", info.MaLoaiThuChi, ")" });
                    command.CommandText = text;
                    if (command.ExecuteNonQuery() < 0)
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
            return quy.MaThuChi;
        }
    }
}
