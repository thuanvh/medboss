namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class KiemKeInfo
    {
        private ArrayList _kiemKeChiTiet;
        private LoaiKiemKeInfo _loaiKiemKe;
        private int _maKiemKe;
        private int _maNhanVien;
        private int _maQuay;
        private DateTime _ngay;

        public KiemKeInfo()
        {
            this._kiemKeChiTiet = new ArrayList();
            this._loaiKiemKe = new LoaiKiemKeInfo();
        }

        public KiemKeInfo(int maKiemKe, int maNhanVien, int maQuay, DateTime ngay)
        {
            this._kiemKeChiTiet = new ArrayList();
            this._loaiKiemKe = new LoaiKiemKeInfo();
            this.MaKiemKe = maKiemKe;
            this.MaNhanVien = maNhanVien;
            this.MaQuay = maQuay;
            this.Ngay = ngay;
        }

        public ArrayList KiemKeChiTiet
        {
            get
            {
                return this._kiemKeChiTiet;
            }
            set
            {
                this._kiemKeChiTiet = value;
            }
        }

        public LoaiKiemKeInfo LoaiKiemKe
        {
            get
            {
                return this._loaiKiemKe;
            }
            set
            {
                this._loaiKiemKe = value;
            }
        }

        public int MaKiemKe
        {
            get
            {
                return this._maKiemKe;
            }
            set
            {
                this._maKiemKe = value;
            }
        }

        public int MaNhanVien
        {
            get
            {
                return this._maNhanVien;
            }
            set
            {
                this._maNhanVien = value;
            }
        }

        public int MaQuay
        {
            get
            {
                return this._maQuay;
            }
            set
            {
                this._maQuay = value;
            }
        }

        public DateTime Ngay
        {
            get
            {
                return this._ngay;
            }
            set
            {
                this._ngay = value;
            }
        }
    }
}
