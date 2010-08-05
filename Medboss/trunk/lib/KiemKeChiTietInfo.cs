namespace Nammedia.Medboss.lib
{
    using System;

    public class KiemKeChiTietInfo
    {
        private int _dongiaban;
        private int _dongianhap;
        private string _ghiChu;
        private DateTime _hanDung;
        private int _maKiemKe;
        private int _maThuocTraoDoi;
        private int _soLuong;
        private int _stt;
        private string _tinhTrang;

        public KiemKeChiTietInfo()
        {
            this._stt = 1;
        }

        public KiemKeChiTietInfo(int maKiemKe, string ghiChu, DateTime hanDung, int maThuocTraoDoi, int soLuong, string tinhTrang, int donGiaNhap, int donGiaBan)
        {
            this._stt = 1;
            this.GhiChu = ghiChu;
            this.HanDung = hanDung;
            this.MaKiemKe = maKiemKe;
            this.MaThuocTraoDoi = maThuocTraoDoi;
            this.SoLuong = soLuong;
            this.TinhTrang = tinhTrang;
            this.DonGiaBan = donGiaBan;
            this.DonGiaNhap = donGiaNhap;
        }

        public int DonGiaBan
        {
            get
            {
                return this._dongiaban;
            }
            set
            {
                this._dongiaban = value;
            }
        }

        public int DonGiaNhap
        {
            get
            {
                return this._dongianhap;
            }
            set
            {
                this._dongianhap = value;
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

        public string TinhTrang
        {
            get
            {
                return this._tinhTrang;
            }
            set
            {
                this._tinhTrang = value;
            }
        }
    }
}
