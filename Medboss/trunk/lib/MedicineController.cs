namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Data;

    public class MedicineController : DataController
    {
        public ArrayList ConditionalList(string Condition)
        {
            string text = "select distinct MaThuoc,TenThuoc,ThanhPhan,HamLuong,NhaSanXuat,MaDVTHinhThuc,MaLoaiThuoc from qryThuocDVT";
            if (Condition != "")
            {
                text = text + " where " + Condition;
            }
            text = text + " order by TenThuoc";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                MedicineInfo info = new MedicineInfo();
                info.MaThuoc = ConvertHelper.getInt(reader[0]);
                info.TenThuoc = reader.GetString(1);
                info.ThanhPhan = reader.GetString(2);
                info.HamLuong = reader.GetString(3);
                info.NhaSanXuat = reader.GetString(4);
                info.MaDVTHinhThuc = ConvertHelper.getInt(reader.GetValue(5));
                info.LoaiThuoc = new LoaiThuocController().Get(ConvertHelper.getInt(reader[6]));
                list.Add(info);
            }
            reader.Close();
            foreach (MedicineInfo info in list)
            {
                text = "select MaDVT,DVT,Tile,MaThuocTraoDoi from qryThuocDVT where MaThuoc=" + info.MaThuoc;
                base.db.Command.CommandText = text;
                reader = base.db.Command.ExecuteReader();
                while (reader.Read())
                {
                    DVT dvt = new DVT();
                    dvt.MaDVT = ConvertHelper.getInt(reader[0]);
                    dvt.TenDV = reader.GetString(1);
                    ThuocTraoDoi doi = new ThuocTraoDoi();
                    doi.DVT = dvt;
                    doi.MaThuocTraoDoi = ConvertHelper.getInt(reader.GetValue(3));
                    doi.TiLe = ConvertHelper.getInt(reader.GetValue(2));
                    info.ThuocTraoDois.Add(doi);
                }
                reader.Close();
            }
            base.db.Connection.Close();
            return list;
        }

        public void DeleteMedicine()
        {
            string text = "select MaThuoc from qryThuocXoa";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                list.Add(ConvertHelper.getInt(reader[0]));
            }
            reader.Close();
            foreach (int num in list)
            {
                text = "delete * from Thuoc where MaThuoc=" + num;
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new DeleteException();
                }
            }
            base.db.Command.Connection.Close();
        }

        public void DeleteMedicine(ArrayList idList)
        {
            if (idList.Count != 0)
            {
                string text = "";
                string text2 = "";
                foreach (int num in idList)
                {
                    if (text != "")
                    {
                        text = text + " or ";
                    }
                    text = text + " MaThuocTraoDoi=" + num;
                }
                IDbTransaction transaction = base.db.Command.Connection.BeginTransaction();
                base.db.Command.Transaction = transaction;
                try
                {
                    text2 = "delete * from ThuocQuay where " + text;
                    base.db.Command.CommandText = text2;
                    if (base.db.Command.ExecuteNonQuery() < 0)
                    {
                        throw new DeleteException();
                    }
                    text2 = "delete * from ThuocDVT where " + text;
                    base.db.Command.CommandText = text2;
                    if (base.db.Command.ExecuteNonQuery() < 0)
                    {
                        throw new DeleteException();
                    }
                    transaction.Commit();
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    LogManager.LogException(exc);
                }
                finally
                {
                    base.db.Command.Connection.Close();
                }
            }
        }

        public int DeleteMedicine(int MaThuocTraoDoi)
        {
            string text = "select count(*) from qryThuocNone where MTTD=" + MaThuocTraoDoi;
            base.db.Command.CommandText = text;
            if (ConvertHelper.getInt(base.db.Command.ExecuteScalar()) <= 0)
            {
                throw new DeleteException();
            }
            IDbTransaction transaction = base.db.Command.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            try
            {
                text = "delete * from ThuocQuay where MaThuocTraoDoi=" + MaThuocTraoDoi;
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new DeleteException();
                }
                text = "delete * from ThuocDVT where MaThuocTraoDoi=" + MaThuocTraoDoi;
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
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
                base.db.Command.Connection.Close();
            }
            return MaThuocTraoDoi;
        }

        public ArrayList DVTList()
        {
            string text = "select MaDVT,DVT from DVT";
            text = text + " order by DVT";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                DVT dvt = new DVT();
                dvt.MaDVT = ConvertHelper.getInt(reader[0]);
                dvt.TenDV = reader.GetString(1);
                list.Add(dvt);
            }
            reader.Close();
            base.db.Connection.Close();
            return list;
        }

        private bool existThuocDVT(DVT dvt, int MaThuoc)
        {
            int num = this.getMaDVT(dvt.TenDV);
            if (num == -1)
            {
                return false;
            }
            string text = string.Concat(new object[] { "select count(*) from ThuocDVT where MaThuoc=", MaThuoc, " and MaDVT=", num });
            base.db.Command.CommandText = text;
            int num2 = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            base.db.Command.Connection.Close();
            if (num2 <= 0)
            {
                return false;
            }
            return true;
        }

        public int getDonGia(int MaThuocTraoDoi, int MaQuay)
        {
            string text = string.Concat(new object[] { "select DonGiaBan,MaThuocTraoDoi,MaQuay from ThuocQuay where MaThuocTraoDoi=", MaThuocTraoDoi, " and MaQuay=", MaQuay });
            base.db.Command.CommandText = text;
            int num = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            if (num == -1)
            {
                text = "select DonGiaBan,MaThuocTraoDoi from ThuocQuay where MaThuocTraoDoi=" + MaThuocTraoDoi;
                base.db.Command.CommandText = text;
                num = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            }
            base.db.Command.Connection.Close();
            return num;
        }

        public double getDonGiaNhap(int MaThuocTraoDoi, int MaQuay)
        {
            string text = string.Concat(new object[] { "select DonGiaNhap,MaThuocTraoDoi,MaQuay from ThuocQuay where MaThuocTraoDoi=", MaThuocTraoDoi, " and MaQuay=", MaQuay });
            base.db.Command.CommandText = text;
            int num = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            if (num == -1)
            {
                text = "select DonGiaNhap,MaThuocTraoDoi from ThuocQuay where MaThuocTraoDoi=" + MaThuocTraoDoi;
                base.db.Command.CommandText = text;
                num = ConvertHelper.getInt(base.db.Command.ExecuteScalar());
            }
            base.db.Command.Connection.Close();
            return (double) num;
        }

        public MedicineInfo.DonGiaNhapBan getDonGiaNhapBan(int MaThuocTraoDoiHinhThuc, int MaQuay)
        {
            MedicineInfo.DonGiaNhapBan dgnb = new MedicineInfo.DonGiaNhapBan();
            string query = "select DonGiaNhapQuyDoi,DonGiaBanQuyDoi,MaQuay,MaThuocTraoDoi from qryGiaThuoc_QuyDoi where MaThuocTraoDoiHinhThuc=" + MaThuocTraoDoiHinhThuc;
            base.db.Command.CommandText = query;
            IDataReader dr = base.db.Command.ExecuteReader();
            while (dr.Read())
            {
                int mq = ConvertHelper.getInt(dr[2]);
                int mttd = ConvertHelper.getInt(dr[3]);
                if (((dgnb.DonGiaNhap <= 0.0) || (mq == MaQuay)) || (mttd == MaThuocTraoDoiHinhThuc))
                {
                    dgnb.DonGiaNhap = ConvertHelper.getInt(dr[0]);
                }
                if (((dgnb.DonGiaBan <= 0.0) || (mq == MaQuay)) || (mttd == MaThuocTraoDoiHinhThuc))
                {
                    dgnb.DonGiaBan = ConvertHelper.getInt(dr[1]);
                }
                if ((mttd == MaThuocTraoDoiHinhThuc) && (mq == MaQuay))
                {
                    return dgnb;
                }
            }
            return dgnb;
        }

        public MedicineInfo.DonGiaNhapBan getDonGiaNhapBan(string TenThuoc, string DVT, int maQuay)
        {
            MedicineInfo.DonGiaNhapBan dgnb = new MedicineInfo.DonGiaNhapBan();
            ArrayList listMedicines = this.GetMedicine(TenThuoc);
            if (listMedicines.Count > 0)
            {
                MedicineInfo mi = (MedicineInfo) listMedicines[0];
                string dvthinhthuc = this.getDVT(mi.MaDVTHinhThuc);
                int mathuoctradoihinhthuc = this.getMaThuocTraoDoi(TenThuoc, dvthinhthuc);
                int mathuoctradoicantinh = this.getMaThuocTraoDoi(TenThuoc, DVT);
                ThuocTraoDoi ttdchuan = null;
                ThuocTraoDoi ttdcantim = null;
                foreach (ThuocTraoDoi ttd in mi.ThuocTraoDois)
                {
                    if (ttd.MaThuocTraoDoi == mathuoctradoihinhthuc)
                    {
                        ttdchuan = ttd;
                    }
                    if (ttd.MaThuocTraoDoi == mathuoctradoicantinh)
                    {
                        ttdcantim = ttd;
                    }
                }
                if ((ttdchuan != null) && (ttdcantim != null))
                {
                    double tile = ((double) ttdcantim.TiLe) / ((double) ttdchuan.TiLe);
                    dgnb = this.getDonGiaNhapBan(mathuoctradoihinhthuc, maQuay);
                    dgnb.DonGiaBan *= tile;
                    dgnb.DonGiaNhap *= tile;
                }
            }
            return dgnb;
        }

        public string getDVT(int maDVT)
        {
            string text2 = string.Empty;
            text2 = this.getDVTFromCache(maDVT);
            if (text2 == string.Empty)
            {
                string text = "select DVT from DVT where MaDVT=" + maDVT;
                base.db.Command.CommandText = text;
                text2 = ConvertHelper.getString(base.db.Command.ExecuteScalar());
                base.db.Command.Connection.Close();
            }
            return text2;
        }

        public string getDVT(string TenThuoc)
        {
            ArrayList a = this.GetMedicine(TenThuoc);
            if (a.Count == 0)
            {
                return GetDVTHinhThuc((MedicineInfo) a[0]);
            }
            string text = "select DVT from Thuoc,ThuocDVT,DVT where Thuoc.TenThuoc='" + StandardizeMedicineName(TenThuoc) + "' and Thuoc.MaThuoc=ThuocDVT.Mathuoc and ThuocDVT.MaDVT=DVT.MaDVT";
            base.db.Command.CommandText = text;
            string text2 = ConvertHelper.getString(base.db.Command.ExecuteScalar());
            base.db.Command.Connection.Close();
            return text2;
        }

        public DVT GetDVT(int MaThuocTraoDoi)
        {
            string text = "select MaDVT,DVT from qryThuocDVT where MaThuocTraoDoi=" + MaThuocTraoDoi;
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            DVT dvt = new DVT();
            while (reader.Read())
            {
                dvt.MaDVT = ConvertHelper.getInt(reader[0]);
                dvt.TenDV = reader.GetString(1);
            }
            reader.Close();
            base.db.Connection.Close();
            return dvt;
        }

        public string getDVTFromCache(int maDVT)
        {
            if ((Program.ACSource != null) && (Program.ACSource.DVTSource != null))
            {
                foreach (DVT dvt in Program.ACSource.DVTSource)
                {
                    if (dvt.MaDVT == maDVT)
                    {
                        return dvt.TenDV;
                    }
                }
            }
            return string.Empty;
        }

        public string getDVTFromCache(string DVT)
        {
            if ((Program.ACSource != null) && (Program.ACSource.DVTSource != null))
            {
                foreach (DVT dvt in Program.ACSource.DVTSource)
                {
                    if (dvt.TenDV == DVT)
                    {
                        return dvt.TenDV;
                    }
                }
            }
            return string.Empty;
        }

        public static string GetDVTHinhThuc(MedicineInfo mi)
        {
            foreach (ThuocTraoDoi ttd in mi.ThuocTraoDois)
            {
                if (ttd.DVT.MaDVT == mi.MaDVTHinhThuc)
                {
                    return ttd.DVT.TenDV;
                }
            }
            return string.Empty;
        }

        public int getMaDVT(string TenDVT)
        {
            TenDVT = StandardizeMedicineName(TenDVT);
            int madvt = this.getMaDVTFromCache(TenDVT);
            if (madvt > 0)
            {
                return madvt;
            }
            string text = "select MaDVT from DVT where DVT='" + TenDVT + "'";
            base.db.Command.CommandText = text;
            object obj2 = base.db.Command.ExecuteScalar();
            int num = (obj2 == null) ? -1 : ((int) obj2);
            base.db.Connection.Close();
            return num;
        }

        public int getMaDVTFromCache(string DVT)
        {
            if ((Program.ACSource != null) && (Program.ACSource.DVTSource != null))
            {
                foreach (DVT dvt in Program.ACSource.DVTSource)
                {
                    if (dvt.TenDV == DVT)
                    {
                        return dvt.MaDVT;
                    }
                }
            }
            return -1;
        }

        public int getMaThuocTraoDoi(string TenThuoc, string DVT)
        {
            ArrayList list = this.GetMedicine(TenThuoc);
            if (list.Count > 0)
            {
                return ((MedicineInfo) list[0]).GetMaThuocTraoDoi(DVT);
            }
            string text = "select MaThuocTraoDoi from Thuoc,DVT,ThuocDVT where Thuoc.TenThuoc='" + StandardizeMedicineName(TenThuoc) + "' and DVT.DVT='" + DVT + "' and Thuoc.MaThuoc=ThuocDVT.MaThuoc and DVT.MaDVT=ThuocDVT.MaDVT";
            base.db.Command.CommandText = text;
            object obj2 = base.db.Command.ExecuteScalar();
            base.db.Command.Connection.Close();
            return ConvertHelper.getInt(obj2);
        }

        public ArrayList GetMedicine(string TenThuoc)
        {
            ArrayList list = new ArrayList();
            if (string.IsNullOrEmpty(TenThuoc))
            {
                return list;
            }
            list = this.GetMedicineFromCache(TenThuoc);
            if (list.Count > 0)
            {
                return list;
            }
            return this.GetMedicineFromDB(TenThuoc);
        }

        public MedicineInfo GetMedicineByMaThuoc(int MaThuoc)
        {
            ArrayList list = this.ConditionalList("MaThuoc=" + MaThuoc);
            MedicineInfo info = new MedicineInfo();
            info.MaThuoc = -1;
            if (list.Count > 0)
            {
                return (MedicineInfo) list[0];
            }
            return info;
        }

        public MedicineInfo GetMedicineByMaThuocTraoDoi(int MaThuocTraoDoi)
        {
            MedicineInfo info = new MedicineInfo();
            string text = "select MaThuoc,TenThuoc,ThanhPhan,HamLuong,NhaSanXuat,DVT,MaDVT,MaThuocTraoDoi,TiLe,MaLoaiThuoc from qryThuocDVT where MaThuocTraoDoi=" + MaThuocTraoDoi;
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            while (reader.Read())
            {
                info.MaThuoc = ConvertHelper.getInt(reader[0]);
                info.TenThuoc = reader.GetString(1);
                info.ThanhPhan = reader.GetString(2);
                info.HamLuong = reader.GetString(3);
                info.NhaSanXuat = reader.GetString(4);
                DVT dvt = new DVT();
                dvt.TenDV = reader.GetString(5);
                dvt.MaDVT = ConvertHelper.getInt(reader[6]);
                ThuocTraoDoi doi = new ThuocTraoDoi();
                doi.DVT = dvt;
                doi.MaThuocTraoDoi = ConvertHelper.getInt(reader[7]);
                doi.TiLe = ConvertHelper.getInt(reader[8]);
                info.ThuocTraoDois.Add(doi);
                info.LoaiThuoc.MaLoai = ConvertHelper.getInt(reader[9]);
                info.LoaiThuoc = new LoaiThuocController().Get(info.LoaiThuoc.MaLoai);
            }
            reader.Close();
            base.db.Connection.Close();
            return info;
        }

        public ArrayList GetMedicineFromCache(string TenThuoc)
        {
            ArrayList a = new ArrayList();
            if (Program.ACSource != null)
            {
                if (Program.ACSource.MedicineSource == null)
                {
                    return a;
                }
                MedicineInfo b = this.GetMedicineFromCacheThoughBinarySearch(TenThuoc, Program.ACSource.MedicineSource);
                if (b != null)
                {
                    a.Add(b);
                }
            }
            return a;
        }

        private MedicineInfo GetMedicineFromCacheThoughBinarySearch(string TenThuoc, ArrayList MedicineSource)
        {
            MedicineInfo[] a = (MedicineInfo[]) MedicineSource.ToArray(typeof(MedicineInfo));
            MedicineInfo mi = new MedicineInfo();
            mi.TenThuoc = TenThuoc;
            int i = Array.BinarySearch<MedicineInfo>(a, mi, new MedicineInfoComparerByTenThuoc());
            if (i < 0)
            {
                return null;
            }
            return a[i];
        }

        public ArrayList GetMedicineFromDB(string TenThuoc)
        {
            return this.ConditionalList(" TenThuoc like '" + StandardizeMedicineName(TenThuoc).ToLower() + "'");
        }

        public DataTable getMedicineInHD(string tenThuoc, string tenQuay)
        {
            tenThuoc = StandardizeMedicineName(tenThuoc);
            string text = "select MaHoaDon,TenThuoc,DVT,SoLuongBan,DonGiaBan,SoLuongNhap,DonGiaNhap,Ngay,TenLoaiHD from qryMedHD where TenThuoc='" + tenThuoc + "' and TenQuay='" + tenQuay + "'";
            base.db.Command.CommandText = text;
            IDataReader reader = base.db.Command.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("M\x00e3 ho\x00e1 đơn", typeof(int));
            table.Columns.Add("T\x00ean thuốc", typeof(string));
            table.Columns.Add("ĐVT", typeof(string));
            table.Columns.Add("Số lượng b\x00e1n", typeof(int));
            table.Columns.Add("Đơn gi\x00e1 b\x00e1n", typeof(int));
            table.Columns.Add("Số lượng nhập", typeof(int));
            table.Columns.Add("Đơn gi\x00e1 nhập", typeof(int));
            table.Columns.Add("Ng\x00e0y", typeof(string));
            table.Columns.Add("Loại ho\x00e1 đơn", typeof(string));
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row["M\x00e3 ho\x00e1 đơn"] = ConvertHelper.getInt(reader["MaHoaDon"]);
                row["T\x00ean thuốc"] = (string) reader["TenThuoc"];
                row["ĐVT"] = (string) reader["DVT"];
                row["Số lượng b\x00e1n"] = ConvertHelper.getInt(reader["SoLuongBan"]);
                row["Đơn gi\x00e1 b\x00e1n"] = ConvertHelper.getInt(reader["DonGiaBan"]);
                row["Số lượng nhập"] = ConvertHelper.getInt(reader["SoLuongNhap"]);
                row["Đơn gi\x00e1 nhập"] = ConvertHelper.getDouble(reader["DonGiaNhap"]);
                row["Ng\x00e0y"] = ((DateTime) reader["Ngay"]).ToString("dd/MM/yyyy");
                row["Loại ho\x00e1 đơn"] = (string) reader["TenLoaiHD"];
                table.Rows.Add(row);
            }
            reader.Close();
            return table;
        }

        public DataTable getMedicineNone()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TenThuoc");
            table.Columns.Add("DVT");
            table.Columns.Add("MaThuocTraoDoi");
            base.db.Command.CommandText = "select TenThuoc,DVTThuoc,MTTD from qryThuocNone";
            IDataReader reader = base.db.Command.ExecuteReader();
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row["TenThuoc"] = ConvertHelper.getString(reader.GetString(0));
                row["DVT"] = ConvertHelper.getString(reader.GetString(1));
                row["MaThuocTraoDoi"] = ConvertHelper.getInt(ConvertHelper.getInt(reader[2]));
                table.Rows.Add(row);
            }
            reader.Close();
            base.db.Command.Connection.Close();
            return table;
        }

        public DataTable getMedicineTon(DateTime dateTime, string quay)
        {
            string text = "#" + dateTime.AddDays(-1.0).ToString("MM/dd/yyyy") + "#";
            string text2 = "#" + dateTime.ToString("MM/dd/yyyy") + "#";
            string text3 = "execute qryThuocTon_View ";
            string text4 = text3;
            text3 = text4 + text + "," + text2 + "," + text2 + ",'" + quay + "'," + text2;
            base.db.Command.CommandText = text3;
            base.db.Command.CommandType = CommandType.StoredProcedure;
            IDataReader reader = base.db.Command.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("MaThuocTraoDoi", typeof(int));
            table.Columns.Add("TenThuoc", typeof(string));
            table.Columns.Add("DVT", typeof(string));
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("DonGiaBan", typeof(int));
            table.Columns.Add("DonGiaNhap", typeof(int));
            table.Columns.Add("HanDung", typeof(string));
            table.Columns.Add("MaKiemKe", typeof(int));
            table.Columns.Add("IsInserted", typeof(bool));
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row["MaThuocTraoDoi"] = ConvertHelper.getInt(reader["MaThuocTraoDoi"]);
                row["TenThuoc"] = ConvertHelper.getString(reader["TenThuoc"]);
                row["DVT"] = ConvertHelper.getString(reader["DVTHinhThuc"]);
                row["SoLuong"] = ConvertHelper.getInt(reader["SoLuong"]);
                row["DonGiaBan"] = ConvertHelper.getInt(reader["DonGiaBan"]);
                row["DonGiaNhap"] = ConvertHelper.getInt(reader["DonGiaNhap"]);
                row["HanDung"] = ConvertHelper.getDateTime(reader["HanDung"]).ToString("MM/yyyy");
                row["MaKiemKe"] = ConvertHelper.getInt(reader["MaKiemKe"]);
                if (((int) row["MaKiemKe"]) == -1)
                {
                    row["MaKiemKe"] = DBNull.Value;
                }
                row["IsInserted"] = ConvertHelper.getInt(reader["IsInserted"]) != -1;
                table.Rows.Add(row);
            }
            reader.Close();
            return table;
        }

        public int InsertMedicine(MedicineInfo mi)
        {
            try
            {
                ArrayList medicine = this.GetMedicine(mi.TenThuoc);
                bool flag = false;
                foreach (MedicineInfo info in medicine)
                {
                    if (((IComparable) info).CompareTo(mi) == 0)
                    {
                        flag = true;
                        mi.MaThuoc = info.MaThuoc;
                    }
                }
                if (!flag)
                {
                    string text = string.Concat(new object[] { "insert into Thuoc values(", mi.MaThuoc, ",'", StandardizeMedicineName(mi.TenThuoc), "','", mi.ThanhPhan, "','", mi.HamLuong, "','", mi.NhaSanXuat, "',", mi.MaDVTHinhThuc, ",", mi.LoaiThuoc.MaLoai, ")" });
                    base.db.Command.CommandText = text;
                    if (base.db.Command.ExecuteNonQuery() <= 0)
                    {
                        throw new InsertException();
                    }
                }
                foreach (ThuocTraoDoi doi in mi.ThuocTraoDois)
                {
                    this.insertThuocTraoDoi(doi, mi.MaThuoc);
                }
            }
            catch (Exception exception)
            {
                LogManager.LogException(exception);
                throw exception;
            }
            finally
            {
                base.db.Connection.Close();
            }
            return mi.MaThuoc;
        }

        private void insertThuocTraoDoi(ThuocTraoDoi ttd, int MaThuoc)
        {
            int missingId = this.getMaDVT(ttd.DVT.TenDV);
            if (this.existThuocDVT(ttd.DVT, MaThuoc))
            {
                throw new InsertException();
            }
            if (missingId == -1)
            {
                missingId = IdManager.GetMissingId("MaDVT", "DVT");
                string text = string.Concat(new object[] { "insert into DVT values(", missingId, ",'", ttd.DVT.TenDV, "')" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new InsertException();
                }
            }
            int num2 = IdManager.GetMissingId("MaThuocTraoDoi", "ThuocDVT");
            string text2 = string.Concat(new object[] { "insert into ThuocDVT values(", num2, ",", MaThuoc, ",", missingId, ",", ttd.TiLe, ")" });
            base.db.Command.CommandText = text2;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            base.db.Connection.Close();
        }

        public ArrayList List()
        {
            return this.ConditionalList("");
        }

        public void medicineUnion(int maThuocTraoDoiSource, int maThuocTraoDoiDes)
        {
            IDbTransaction transaction = base.db.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            try
            {
                string[] textArray = new string[] { "BanThuocChiTiet", "NhapThuocChiTiet", "ChuyenQuayChiTiet", "KiemKeChiTiet", "HoaDonNhapTraLaiChiTiet", "HoaDonBanTraLaiChiTiet", "HoaDonThanhToanChiTiet", "DatThuocChiTiet" };
                foreach (string text in textArray)
                {
                    string text2 = string.Concat(new object[] { "update ", text, " set MaThuocTraoDoi=", maThuocTraoDoiDes, " where MaThuocTraoDoi=", maThuocTraoDoiSource });
                    base.db.Command.CommandText = text2;
                    if (base.db.Command.ExecuteNonQuery() < 0)
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
                base.db.Connection.Close();
            }
        }

        public static string StandardizeMedicineName(string TenThuoc)
        {
            if (TenThuoc != null)
            {
                TenThuoc = TenThuoc.Trim();
                while (TenThuoc.Contains("  "))
                {
                    TenThuoc = TenThuoc.Replace("  ", " ");
                }
                return TenThuoc;
            }
            return "";
        }

        public void UpdateDonGiaBan(int MaThuocTraoDoi, int MaQuay, int DonGiaBan)
        {
            string text = string.Concat(new object[] { "update ThuocQuay set DonGiaBan=", DonGiaBan, " where MaThuocTraoDoi=", MaThuocTraoDoi, " and MaQuay=", MaQuay });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() <= 0)
            {
                text = string.Concat(new object[] { "insert into ThuocQuay values(", MaThuocTraoDoi, ",", MaQuay, ",null,", DonGiaBan, ")" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new InsertException();
                }
            }
            base.db.Connection.Close();
        }

        public void UpdateDonGiaNhap(int MaThuocTraoDoi, int MaQuay, double DonGiaNhap)
        {
            string text = string.Concat(new object[] { "update ThuocQuay set DonGiaNhap=", DonGiaNhap, " where MaThuocTraoDoi=", MaThuocTraoDoi, " and MaQuay=", MaQuay });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() <= 0)
            {
                text = string.Concat(new object[] { "insert into ThuocQuay values(", MaThuocTraoDoi, ",", MaQuay, ",", DonGiaNhap, ",null)" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new InsertException();
                }
            }
            base.db.Connection.Close();
        }

        public void UpdateDonGiaNhapBan(int MaThuocTraoDoi, int MaQuay, double DonGiaNhap, int DonGiaBan)
        {
            string nhap = "DonGiaNhap=" + DonGiaNhap + ",";
            if (DonGiaNhap <= 0.0)
            {
                nhap = "";
            }
            string text = string.Concat(new object[] { "update ThuocQuay set ", nhap, " DonGiaBan=", DonGiaBan, " where MaThuocTraoDoi=", MaThuocTraoDoi, " and MaQuay=", MaQuay });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() <= 0)
            {
                text = string.Concat(new object[] { "insert into ThuocQuay values(", MaThuocTraoDoi, ",", MaQuay, ",", DonGiaNhap, ",", DonGiaBan, ")" });
                base.db.Command.CommandText = text;
                if (base.db.Command.ExecuteNonQuery() < 0)
                {
                    throw new InsertException();
                }
            }
            base.db.Connection.Close();
        }

        public int UpdateMedicine(MedicineInfo mi)
        {
            IDbTransaction transaction = base.db.Connection.BeginTransaction();
            base.db.Command.Transaction = transaction;
            try
            {
                string text = string.Concat(new object[] { "update Thuoc set TenThuoc='", StandardizeMedicineName(mi.TenThuoc), "',ThanhPhan='", mi.ThanhPhan, "',HamLuong='", mi.HamLuong, "',NhaSanXuat='", mi.NhaSanXuat, "',MaDVTHinhThuc=", mi.MaDVTHinhThuc, ",MaLoaiThuoc=", mi.LoaiThuoc.MaLoai, " where MaThuoc=", mi.MaThuoc });
                base.db.Command.CommandText = text;
                int num = base.db.Command.ExecuteNonQuery();
                foreach (ThuocTraoDoi doi in mi.ThuocTraoDois)
                {
                    int num2 = new MedicineController().getMaDVT(doi.DVT.TenDV);
                    if (num <= 0)
                    {
                        throw new InsertException();
                    }
                    text = string.Concat(new object[] { "update ThuocDVT set TiLe=", doi.TiLe, " where MaDVT=", num2, " and MaThuoc=", mi.MaThuoc });
                    base.db.Command.CommandText = text;
                    if (base.db.Command.ExecuteNonQuery() <= 0)
                    {
                        int missingId = IdManager.GetMissingId("MaThuocTraoDoi", "ThuocDVT");
                        text = string.Concat(new object[] { "insert into ThuocDVT values(", missingId, ",", mi.MaThuoc, ",", num2, ",", doi.TiLe, ")" });
                        base.db.Command.CommandText = text;
                        if (base.db.Command.ExecuteNonQuery() <= 0)
                        {
                            throw new UpdateException();
                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
                transaction.Rollback();
            }
            finally
            {
                base.db.Connection.Close();
            }
            return mi.MaThuoc;
        }
    }
}
