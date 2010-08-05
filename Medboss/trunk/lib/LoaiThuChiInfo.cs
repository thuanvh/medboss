namespace Nammedia.Medboss.lib
{
    using System;

    public class LoaiThuChiInfo
    {
        private bool _dangHoatDong;
        private int _maLoaiThuChi;
        private string _tenLoaiThuChi;

        public LoaiThuChiInfo()
        {
        }

        public LoaiThuChiInfo(int maLoaiThuChi, string tenLoaiThuChi)
        {
            this.MaLoaiThuChi = maLoaiThuChi;
            this.TenLoaiThuChi = tenLoaiThuChi;
        }

        public override string ToString()
        {
            return this._tenLoaiThuChi;
        }

        public bool DangHoatDong
        {
            get
            {
                return this._dangHoatDong;
            }
            set
            {
                this._dangHoatDong = value;
            }
        }

        public int MaLoaiThuChi
        {
            get
            {
                return this._maLoaiThuChi;
            }
            set
            {
                this._maLoaiThuChi = value;
            }
        }

        public string TenLoaiThuChi
        {
            get
            {
                return this._tenLoaiThuChi;
            }
            set
            {
                this._tenLoaiThuChi = value;
            }
        }
    }
}
