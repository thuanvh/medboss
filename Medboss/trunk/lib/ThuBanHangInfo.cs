namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class ThuBanHangInfo
    {
        private int _maQuay;
        private DateTime _ngay;
        private ArrayList _thuBanHangChiTiet = new ArrayList();

        public int MaQuay
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

        public ArrayList ThuBanHangChiTiet
        {
            get
            {
                return this._thuBanHangChiTiet;
            }
            set
            {
                this._thuBanHangChiTiet = value;
            }
        }
    }
}
