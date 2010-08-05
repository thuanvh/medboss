namespace Nammedia.Medboss.lib
{
    using System;

    public class LoaiThuocInfo
    {
        private int _maLoai;
        private string _tenLoai = "";

        public override string ToString()
        {
            return this.TenLoai;
        }

        public int MaLoai
        {
            get
            {
                return this._maLoai;
            }
            set
            {
                this._maLoai = value;
            }
        }

        public string TenLoai
        {
            get
            {
                return this._tenLoai;
            }
            set
            {
                this._tenLoai = value;
            }
        }
    }
}
