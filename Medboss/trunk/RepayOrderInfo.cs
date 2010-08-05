namespace Nammedia.Medboss
{
    using System;

    public class RepayOrderInfo
    {
        private string _ghiChu;
        private int _hoaDonTraLai;
        private int _maHoaDon;
        private short _maNhanVien;
        private byte _maQuay;
        private DateTime _ngay;
        private bool _thuHayChi;
        private int _tienTraLai;

        public RepayOrderInfo()
        {
        }

        public RepayOrderInfo(int maHoaDon, string ghiChu, int hoaDonTraLai, short maNhanVien, byte maQuay, DateTime ngay, bool thuHayChi, int tienTraLai)
        {
            this.GhiChu = ghiChu;
            this.HoaDonTraLai = hoaDonTraLai;
            this.MaHoaDon = maHoaDon;
            this.MaNhanVien = maNhanVien;
            this.MaQuay = maQuay;
            this.Ngay = ngay;
            this.ThuHayChi = thuHayChi;
            this.TienTraLai = tienTraLai;
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

        public int HoaDonTraLai
        {
            get
            {
                return this._hoaDonTraLai;
            }
            set
            {
                this._hoaDonTraLai = value;
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

        public short MaNhanVien
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

        public byte MaQuay
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
    }
}
