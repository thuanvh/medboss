namespace Nammedia.Medboss.lib
{
    using System;

    public class TraLaiChiTietInfo
    {
        private int _donGia;
        private string _ghiChu;
        private DateTime _hanDung;
        private int _maHoaDon;
        private int _maThuocTraoDoi;
        private DateTime _ngayNhap;
        private string _soLo;
        private int _soLuong;
        private int _stt;

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
