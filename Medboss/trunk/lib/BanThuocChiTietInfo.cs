namespace Nammedia.Medboss.lib
{
    using System;

    public class BanThuocChiTietInfo
    {
        private int _chietKhau;
        private int _donGiaBan;
        private string _ghiChu;
        private int _maHoaDon;
        private int _maThuocTraoDoi;
        private int _soLuong;
        private int _stt;

        public BanThuocChiTietInfo()
        {
        }

        public BanThuocChiTietInfo(int maHoaDon, int maThuocTraoDoi, int donGiaBan, string ghiChu, int soLuong)
        {
            this.DonGiaBan = donGiaBan;
            this.GhiChu = ghiChu;
            this.MaHoaDon = maHoaDon;
            this.MaThuocTraoDoi = maThuocTraoDoi;
            this.SoLuong = soLuong;
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

        public int DonGiaBan
        {
            get
            {
                return this._donGiaBan;
            }
            set
            {
                this._donGiaBan = value;
            }
        }

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
    }
}
