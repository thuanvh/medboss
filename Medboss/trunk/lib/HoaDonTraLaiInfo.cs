namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class HoaDonTraLaiInfo
    {
        private string _ghiChu;
        private int _hoaDonNhapBan;
        private int _maHoaDon;
        private int _maKhachHang;
        private int _maNhanVien;
        private int _maQuay;
        private DateTime _ngay;
        private bool _thuHayChi;
        private int _tienTraLai;
        private ArrayList _traLaiChiTiet = new ArrayList();

        public string GhiChu
        {
            get
            {
                return this._ghiChu;
            }
            set
            {
                this._ghiChu = value;
            }
        }

        public int HoaDonNhapBan
        {
            get
            {
                return this._hoaDonNhapBan;
            }
            set
            {
                this._hoaDonNhapBan = value;
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

        public bool ThuHayChi
        {
            get
            {
                return this._thuHayChi;
            }
            set
            {
                this._thuHayChi = value;
            }
        }

        public int TienTraLai
        {
            get
            {
                return this._tienTraLai;
            }
            set
            {
                this._tienTraLai = value;
            }
        }

        public ArrayList TraLaiChiTiet
        {
            get
            {
                return this._traLaiChiTiet;
            }
            set
            {
                this._traLaiChiTiet = value;
            }
        }
    }
}
