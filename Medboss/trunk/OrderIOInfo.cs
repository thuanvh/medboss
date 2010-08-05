namespace Nammedia.Medboss
{
    using System;

    public class OrderIOInfo
    {
        private int _chietKhau;
        private int _loaiHoaDon;
        private int _maHoaDon;
        private int _maKhachHang;
        private int _maNhanVien;
        private int _maQuay;
        private DateTime _ngay;
        private int _thucThu;
        private int _tienNo;
        public static int HOADONBAN = 0;
        public static int HOADONNHAP = 1;

        public OrderIOInfo()
        {
        }

        public OrderIOInfo(int maHoaDon, int chietKhau, byte loaiHoaDon, int maKhachHang, byte maNhanVien, byte maQuay, DateTime ngay, int tienNo)
        {
            this.ChietKhau = chietKhau;
            this.LoaiHoaDon = loaiHoaDon;
            this.MaHoaDon = maHoaDon;
            this.MaKhachHang = maKhachHang;
            this.MaNhanVien = maNhanVien;
            this.MaQuay = maQuay;
            this.Ngay = ngay;
            this.TienNo = tienNo;
        }

        public int ChietKhau
        {
            get
            {
                return this._chietKhau;
            }
            set
            {
                this._chietKhau = value;
            }
        }

        public int LoaiHoaDon
        {
            get
            {
                return this._loaiHoaDon;
            }
            set
            {
                this._loaiHoaDon = value;
            }
        }

        public int MaHoaDon
        {
            get
            {
                return this._maHoaDon;
            }
            set
            {
                this._maHoaDon = value;
            }
        }

        public int MaKhachHang
        {
            get
            {
                return this._maKhachHang;
            }
            set
            {
                this._maKhachHang = value;
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

        public int ThucThu
        {
            get
            {
                return this._thucThu;
            }
            set
            {
                this._thucThu = value;
            }
        }

        public int TienNo
        {
            get
            {
                return this._tienNo;
            }
            set
            {
                this._tienNo = value;
            }
        }
    }
}
