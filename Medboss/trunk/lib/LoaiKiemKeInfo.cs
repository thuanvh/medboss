namespace Nammedia.Medboss.lib
{
    using System;

    public class LoaiKiemKeInfo : IComparable
    {
        private bool _huy;
        private int _maLoaiKiemKe;
        private string _tenLoaiKiemKe = "";

        int IComparable.CompareTo(object loaiKKobj)
        {
            LoaiKiemKeInfo info = (LoaiKiemKeInfo) loaiKKobj;
            return this.TenLoaiKiemKe.CompareTo(info.TenLoaiKiemKe);
        }

        public override string ToString()
        {
            return this.TenLoaiKiemKe;
        }

        public bool Huy
        {
            get
            {
                return this._huy;
            }
            set
            {
                this._huy = value;
            }
        }

        public int MaLoaiKiemKe
        {
            get
            {
                return this._maLoaiKiemKe;
            }
            set
            {
                this._maLoaiKiemKe = value;
            }
        }

        public string TenLoaiKiemKe
        {
            get
            {
                return this._tenLoaiKiemKe;
            }
            set
            {
                this._tenLoaiKiemKe = value;
            }
        }
    }
}
