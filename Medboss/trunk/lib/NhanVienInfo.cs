namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;

    public class NhanVienInfo : IFriendlyName
    {
        private string _ghiChu;
        private string _hoVaTenDem;
        private int _maNhanVien;
        private int _soCMT;
        private string _ten;

        public NhanVienInfo()
        {
        }

        public NhanVienInfo(int maNhanVien, string ghiChu, string hoVaTenDem, int soCMT, string ten)
        {
            this.GhiChu = ghiChu;
            this.HoVaTenDem = hoVaTenDem;
            this.MaNhanVien = maNhanVien;
            this.SoCMT = soCMT;
            this.Ten = ten;
        }

        string IFriendlyName.FriendlyName()
        {
            return (this.HoVaTenDem + " " + this.Ten);
        }

        public override string ToString()
        {
            return this.Ten;
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

        public string HoVaTenDem
        {
            get
            {
                return this._hoVaTenDem;
            }
            set
            {
                this._hoVaTenDem = value;
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

        public int SoCMT
        {
            get
            {
                return this._soCMT;
            }
            set
            {
                this._soCMT = value;
            }
        }

        public string Ten
        {
            get
            {
                return this._ten;
            }
            set
            {
                this._ten = value;
            }
        }
    }
}
