namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class ChuyenQuayInfo
    {
        private ArrayList _chuyenQuayChiTiet;
        private int _maChuyen;
        private int _maNhanVien;
        private int _maQuayNhan;
        private int _maQuayXuat;
        private DateTime _ngay;

        public ChuyenQuayInfo()
        {
            this.ChuyenQuayChiTiet = new ArrayList();
        }

        public ArrayList ChuyenQuayChiTiet
        {
            get
            {
                return this._chuyenQuayChiTiet;
            }
            set
            {
                this._chuyenQuayChiTiet = value;
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

        public int MaQuayNhan
        {
            get
            {
                return this._maQuayNhan;
            }
            set
            {
                this._maQuayNhan = value;
            }
        }

        public int MaQuayXuat
        {
            get
            {
                return this._maQuayXuat;
            }
            set
            {
                this._maQuayXuat = value;
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
    }
}
