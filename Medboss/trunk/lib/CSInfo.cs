namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss;
    using System;

    public class CSInfo : IFriendlyName
    {
        private string _chucVu;
        private string _congTy;
        private string _diaChi;
        private string _dienThoai;
        private Nammedia.Medboss.lib.LoaiKhachHang _loaiKhachHang;
        private int _maKhachHang;
        private string _ten;

        public CSInfo()
        {
            this.LoaiKhachHang = new Nammedia.Medboss.lib.LoaiKhachHang();
        }

        public CSInfo(int maKhachHang, string chucVu, string congTy, string diaChi, string dienThoai, string ten, Nammedia.Medboss.lib.LoaiKhachHang loaiKhachHang)
        {
            this.ChucVu = chucVu;
            this.CongTy = congTy;
            this.DiaChi = diaChi;
            this.DienThoai = dienThoai;
            this.MaKhachHang = maKhachHang;
            this.Ten = ten;
            this.LoaiKhachHang = loaiKhachHang;
            this.LoaiKhachHang = new Nammedia.Medboss.lib.LoaiKhachHang();
        }

        string IFriendlyName.FriendlyName()
        {
            return this._ten;
        }

        public override string ToString()
        {
            return this.Ten;
        }

        public string ChucVu
        {
            get
            {
                return this._chucVu;
            }
            set
            {
                this._chucVu = value;
            }
        }

        public string CongTy
        {
            get
            {
                return this._congTy;
            }
            set
            {
                this._congTy = value;
            }
        }

        public string DiaChi
        {
            get
            {
                return this._diaChi;
            }
            set
            {
                this._diaChi = value;
            }
        }

        public string DienThoai
        {
            get
            {
                return this._dienThoai;
            }
            set
            {
                this._dienThoai = value;
            }
        }

        public Nammedia.Medboss.lib.LoaiKhachHang LoaiKhachHang
        {
            get
            {
                return this._loaiKhachHang;
            }
            set
            {
                this._loaiKhachHang = value;
            }
        }

        public int MaKhachHang
        {
            get
            {
                return this._maKhachHang;
            }
            set
            {
                this._maKhachHang = value;
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
