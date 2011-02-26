namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class HoaDonThanhToanInfo
    {
        private int _maHoaDonNhap;
        private int _maKhachHang;
        private int _maNhanVien;
        private int _maQuay;
        private int _maThanhToan;
        private DateTime _ngayThanhToan;
        private ArrayList _thanhToanChiTiet;
        public int TongTienThanhToan { get; set; }
        public int TongTienChietKhau { get; set; }
        public int TongTienPhaiTra { get; set; }
        public HoaDonThanhToanInfo()
        {
            this.ThanhToanChiTiet = new ArrayList();
        }

        public int MaHoaDonNhap
        {
            get
            {
                return this._maHoaDonNhap;
            }
            set
            {
                this._maHoaDonNhap = value;
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

        public int MaThanhToan
        {
            get
            {
                return this._maThanhToan;
            }
            set
            {
                this._maThanhToan = value;
            }
        }

        public DateTime NgayThanhToan
        {
            get
            {
                return this._ngayThanhToan;
            }
            set
            {
                this._ngayThanhToan = value;
            }
        }

        public ArrayList ThanhToanChiTiet
        {
            get
            {
                return this._thanhToanChiTiet;
            }
            set
            {
                this._thanhToanChiTiet = value;
            }
        }
    }
}
