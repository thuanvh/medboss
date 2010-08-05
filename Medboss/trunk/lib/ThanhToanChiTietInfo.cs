namespace Nammedia.Medboss.lib
{
    using System;

    internal class ThanhToanChiTietInfo
    {
        private int _chietKhau;
        private int _donGia;
        private int _maThanhToan;
        private int _maThuocTraoDoi;
        private DateTime _ngayNhap;
        private int _soLuong;
        private int _stt;
        private int _tienChietKhau;

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

        public int DonGia
        {
            get
            {
                return this._donGia;
            }
            set
            {
                this._donGia = value;
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

        public int MaThuocTraoDoi
        {
            get
            {
                return this._maThuocTraoDoi;
            }
            set
            {
                this._maThuocTraoDoi = value;
            }
        }

        public DateTime NgayNhap
        {
            get
            {
                return this._ngayNhap;
            }
            set
            {
                this._ngayNhap = value;
            }
        }

        public int SoLuong
        {
            get
            {
                return this._soLuong;
            }
            set
            {
                this._soLuong = value;
            }
        }

        public int STT
        {
            get
            {
                return this._stt;
            }
            set
            {
                this._stt = value;
            }
        }

        public int TienChietKhau
        {
            get
            {
                return this._tienChietKhau;
            }
            set
            {
                this._tienChietKhau = value;
            }
        }
    }
}
