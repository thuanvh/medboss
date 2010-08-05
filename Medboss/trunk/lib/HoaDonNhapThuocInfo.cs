namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;

    public class HoaDonNhapThuocInfo : OrderIOInfo
    {
        private ArrayList _hoaDonChiTiet = new ArrayList();
        private string _MaHoaDonNCC;

        public HoaDonNhapThuocInfo()
        {
            base.LoaiHoaDon = OrderIOInfo.HOADONNHAP;
            this._hoaDonChiTiet = new ArrayList();
        }

        public ArrayList HoaDonChiTiet
        {
            get
            {
                return this._hoaDonChiTiet;
            }
            set
            {
                this._hoaDonChiTiet = value;
            }
        }

        public string MaHoaDonNCC
        {
            get
            {
                return this._MaHoaDonNCC;
            }
            set
            {
                this._MaHoaDonNCC = value;
            }
        }
    }
}
