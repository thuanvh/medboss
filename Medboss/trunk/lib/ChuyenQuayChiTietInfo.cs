namespace Nammedia.Medboss.lib
{
    using System;

    public class ChuyenQuayChiTietInfo
    {
        private int _donGiaBan;
        private string _ghiChu;
        private DateTime _hanDung;
        private int _maChuyen;
        private int _maThuocTraoDoi;
        private string _soLo;
        private int _soLuong;
        private int _stt;

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

        public int MaChuyen
        {
            get
            {
                return this._maChuyen;
            }
            set
            {
                this._maChuyen = value;
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
