namespace Nammedia.Medboss.lib
{
    using System;
    using System.Collections;

    public class QuyInfo
    {
        private int _maQuay;
        private int _maThuChi;
        private DateTime _ngay;
        private ArrayList _thuChiChiTiet;

        public QuyInfo()
        {
            this._thuChiChiTiet = new ArrayList();
        }

        public QuyInfo(int maThuChi, int maQuay, DateTime ngay)
        {
            this.MaQuay = maQuay;
            this.MaThuChi = maThuChi;
            this.Ngay = ngay;
        }

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

        public int MaThuChi
        {
            get
            {
                return this._maThuChi;
            }
            set
            {
                this._maThuChi = value;
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

        public ArrayList ThuChiChiTiet
        {
            get
            {
                return this._thuChiChiTiet;
            }
            set
            {
                this._thuChiChiTiet = value;
            }
        }
    }
}
