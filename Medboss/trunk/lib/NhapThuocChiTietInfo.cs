namespace Nammedia.Medboss.lib
{
    using System;

    public class NhapThuocChiTietInfo
    {
        private bool _daThanhToan;
        private int _donGiaBan;
        private double _donGiaNhap;
        private string _ghiChu;
        private DateTime _hanDung;
        private int _maHoaDon;
        private int _maThuocTraoDoi;
        private string _soLo;
        private int _soLuong;
        private int _stt;

        public NhapThuocChiTietInfo()
        {
        }

        public NhapThuocChiTietInfo(int maHoaDon, int maThuocTraoDoi, int donGiaNhap, string ghiChu, DateTime hanDung, string soLo, int soLuong)
        {
            this.DonGiaNhap = donGiaNhap;
            this.GhiChu = ghiChu;
            this.HanDung = hanDung;
            this.MaHoaDon = maHoaDon;
            this.MaThuocTraoDoi = maThuocTraoDoi;
            this.SoLo = soLo;
            this.SoLuong = soLuong;
        }

        public bool DaThanhToan
        {
            get
            {
                return this._daThanhToan;
            }
            set
            {
                this._daThanhToan = value;
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

        public double DonGiaNhap
        {
            get
            {
                return this._donGiaNhap;
            }
            set
            {
                this._donGiaNhap = value;
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

        public DateTime HanDung
        {
            get
            {
                return this._hanDung;
            }
            set
            {
                this._hanDung = value;
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

        public string SoLo
        {
            get
            {
                return this._soLo;
            }
            set
            {
                this._soLo = value;
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
