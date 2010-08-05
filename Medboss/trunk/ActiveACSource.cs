namespace Nammedia.Medboss
{
    using Nammedia.Medboss.lib;
    using System;
    using System.Collections;

    public class ActiveACSource
    {
        private CommonSource _activeSource;
        private ArrayList _cs;
        private ArrayList _dvt;
        private ArrayList _loaiKiemKe;
        private ArrayList _loaiThuChi;
        private ArrayList _loaiThuoc;
        private ArrayList _med;
        private ArrayList _nhanVien;
        private ArrayList _NhomCS;
        private ArrayList _quay;
        private ArrayList _refreshList = new ArrayList();

        public void AddDataChangeListener(IRefresh refresh)
        {
            this._refreshList.Add(refresh);
        }

        public void RefreshAll()
        {
            this._quay = new QuayController().List();
            CSController controller2 = new CSController();
            this._cs = controller2.List();
            this._NhomCS = controller2.GetACCSGroups();
            MedicineController controller3 = new MedicineController();
            this._med = controller3.List();
            this._dvt = controller3.DVTList();
            this._nhanVien = new NhanVienController().List();
            this._loaiKiemKe = new KiemKeController().ListLoaiKiemKe();
            this._loaiThuChi = new LoaiThuChiController().list();
            this._loaiThuoc = new LoaiThuocController().List();
        }

        public void RefreshCSSource()
        {
            this._cs = new CSController().List();
        }

        public void RefreshCSTypeSource()
        {
            this._NhomCS = new CSController().GetACCSGroups();
        }

        public void RefreshDVTSource()
        {
            this._dvt = new MedicineController().DVTList();
        }

        public void RefreshLoaiKiemKe()
        {
            this._loaiKiemKe = new KiemKeController().ListLoaiKiemKe();
        }

        public void RefreshLoaiThuChi()
        {
            this._loaiThuChi = new LoaiThuChiController().list();
        }

        public void RefreshLoaiThuoc()
        {
            this._loaiThuoc = new LoaiThuocController().List();
        }

        public void RefreshMedicineSource()
        {
            ArrayList list = new MedicineController().List();
            this._med = list;
        }

        public void RefreshNhanVienSource()
        {
            this._nhanVien = new NhanVienController().List();
        }

        public void RefreshQuaySource()
        {
            this._quay = new QuayController().List();
        }

        public void RefreshSource()
        {
            switch (this._activeSource)
            {
                case CommonSource.Quay:
                    this.RefreshQuaySource();
                    break;

                case CommonSource.Medicine:
                    this.RefreshMedicineSource();
                    break;

                case CommonSource.CS:
                    this.RefreshCSSource();
                    break;

                case CommonSource.DVT:
                    this.RefreshDVTSource();
                    break;

                case CommonSource.NhanVien:
                    this.RefreshNhanVienSource();
                    break;

                case CommonSource.NhomCS:
                    this.RefreshCSTypeSource();
                    break;

                case CommonSource.LoaiKiemKe:
                    this.RefreshLoaiKiemKe();
                    break;

                case CommonSource.LoaiThuChi:
                    this.RefreshLoaiThuChi();
                    break;

                case CommonSource.All:
                    this.RefreshAll();
                    break;

                case CommonSource.LoaiThuoc:
                    this.RefreshLoaiThuoc();
                    break;
            }
            foreach (IRefresh refresh in this._refreshList)
            {
                if (refresh != null)
                {
                    refresh.Refresh();
                }
            }
        }

        public CommonSource ActiveSource
        {
            get
            {
                return this._activeSource;
            }
            set
            {
                this._activeSource = value;
            }
        }

        public ArrayList CsSource
        {
            get
            {
                return this._cs;
            }
            set
            {
                this._cs = value;
            }
        }

        public ArrayList CSTypeSource
        {
            get
            {
                return this._NhomCS;
            }
            set
            {
                this._NhomCS = value;
            }
        }

        public ArrayList DVTSource
        {
            get
            {
                return this._dvt;
            }
            set
            {
                this._dvt = value;
            }
        }

        public ArrayList LoaiKiemKe
        {
            get
            {
                return this._loaiKiemKe;
            }
            set
            {
                this._loaiKiemKe = value;
            }
        }

        public ArrayList LoaiThuChi
        {
            get
            {
                return this._loaiThuChi;
            }
            set
            {
                this._loaiThuChi = value;
            }
        }

        public ArrayList LoaiThuoc
        {
            get
            {
                return this._loaiThuoc;
            }
            set
            {
                this._loaiThuoc = value;
            }
        }

        public ArrayList MedicineSource
        {
            get
            {
                return this._med;
            }
            set
            {
                this._med = value;
            }
        }

        public ArrayList NhanVienSource
        {
            get
            {
                return this._nhanVien;
            }
            set
            {
                this._nhanVien = value;
            }
        }

        public ArrayList QuaySource
        {
            get
            {
                return this._quay;
            }
            set
            {
                this._quay = value;
            }
        }
    }
}
