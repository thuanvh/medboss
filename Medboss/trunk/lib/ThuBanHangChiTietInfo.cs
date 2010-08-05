namespace Nammedia.Medboss.lib
{
    using System;

    public class ThuBanHangChiTietInfo
    {
        private int _maThu;
        private int _thuBanHang;
        private int _tienDoi;

        public int MaThu
        {
            get
            {
                return this._maThu;
            }
            set
            {
                this._maThu = value;
            }
        }

        public int ThuBanHang
        {
            get
            {
                return this._thuBanHang;
            }
            set
            {
                this._thuBanHang = value;
            }
        }

        public int TienDoi
        {
            get
            {
                return this._tienDoi;
            }
            set
            {
                this._tienDoi = value;
            }
        }
    }
}
