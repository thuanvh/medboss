namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;

    public class HoaDonBanThuocInfo : OrderIOInfo
    {
        private ArrayList _hoaDonChiTiet = new ArrayList();

        public HoaDonBanThuocInfo()
        {
            base.LoaiHoaDon = OrderIOInfo.HOADONBAN;
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
    }
}
