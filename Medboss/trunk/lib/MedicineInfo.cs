namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class MedicineInfo : IFriendlyName, IComparable
    {
        private string _hamluong;
        private LoaiThuocInfo _loaiThuoc = new LoaiThuocInfo();
        private int _maDVTHinhThuc;
        private int _mathuoc;
        private string _nhasanxuat;
        private string _ten;
        private string _thanhphan;
        private ArrayList _thuocTraoDoi = new ArrayList();

        public int GetMaThuocTraoDoi(string DVT)
        {
            foreach (ThuocTraoDoi ttd in this.ThuocTraoDois)
            {
                if (ttd.DVT.TenDV == DVT)
                {
                    return ttd.MaThuocTraoDoi;
                }
            }
            return -1;
        }

        string IFriendlyName.FriendlyName()
        {
            return this.TenThuoc;
        }

        int IComparable.CompareTo(object obj)
        {
            MedicineInfo info = (MedicineInfo) obj;
            if (info.TenThuoc == this.TenThuoc)
            {
                return 0;
            }
            return 1;
        }

        public override string ToString()
        {
            return this._ten;
        }

        public string HamLuong
        {
            get
            {
                return this._hamluong;
            }
            set
            {
                this._hamluong = value;
            }
        }

        public LoaiThuocInfo LoaiThuoc
        {
            get
            {
                return this._loaiThuoc;
            }
            set
            {
                this._loaiThuoc = value;
            }
        }

        public int MaDVTHinhThuc
        {
            get
            {
                return this._maDVTHinhThuc;
            }
            set
            {
                this._maDVTHinhThuc = value;
            }
        }

        public int MaThuoc
        {
            get
            {
                return this._mathuoc;
            }
            set
            {
                this._mathuoc = value;
            }
        }

        public string NhaSanXuat
        {
            get
            {
                return this._nhasanxuat;
            }
            set
            {
                this._nhasanxuat = value;
            }
        }

        public string TenThuoc
        {
            get
            {
                return this._ten;
            }
            set
            {
                this._ten = value;
            }
        }

        public string ThanhPhan
        {
            get
            {
                return this._thanhphan;
            }
            set
            {
                this._thanhphan = value;
            }
        }

        public ArrayList ThuocTraoDois
        {
            get
            {
                return this._thuocTraoDoi;
            }
            set
            {
                this._thuocTraoDoi = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DonGiaNhapBan
        {
            public double DonGiaNhap;
            public double DonGiaBan;
        }
    }
}
