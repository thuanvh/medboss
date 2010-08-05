namespace Nammedia.Medboss.lib
{
    using System;

    public class KhoanThuChiInfo
    {
        private string _dienGiai;
        private string _ghiChu;
        private int _maKhachHang;
        private int _maLoaiThuChi;
        private int _maThuChi;
        private string _tenLoaiThuChi;
        private bool _thuHayChi;
        private int _tien;
        public static bool Chi = false;
        public static bool Thu = true;

        public KhoanThuChiInfo()
        {
        }

        public KhoanThuChiInfo(int maThuChi, string dienGiai, string ghiChu, int maKhachHang, int maLoaiThuChi, bool thuHayChi, int tien)
        {
            this.DienGiai = dienGiai;
            this.GhiChu = ghiChu;
            this.MaKhachHang = maKhachHang;
            this.MaLoaiThuChi = maLoaiThuChi;
            this.MaThuChi = maThuChi;
            this.ThuHayChi = thuHayChi;
            this.Tien = tien;
        }

        public string DienGiai
        {
            get
            {
                return this._dienGiai;
            }
            set
            {
                this._dienGiai = value;
            }
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

        public bool ThuHayChi
        {
            get
            {
                return this._thuHayChi;
            }
            set
            {
                this._thuHayChi = value;
            }
        }

        public int Tien
        {
            get
            {
                return this._tien;
            }
            set
            {
                this._tien = value;
            }
        }
    }
}
