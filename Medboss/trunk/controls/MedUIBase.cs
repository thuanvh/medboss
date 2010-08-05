namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Autocomplete;
    using Nammedia.Medboss.lib;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class MedUIBase : UserControl, IRefresh
    {
        protected AutoCompleteFactory _acFactory = new AutoCompleteFactory();
        protected ArrayList _csSource = new ArrayList();
        protected ArrayList _dvtSource = new ArrayList();
        protected ArrayList _loaiKiemKe = new ArrayList();
        protected ArrayList _loaiThuChi = new ArrayList();
        protected ArrayList _loaiThuoc = new ArrayList();
        protected ArrayList _medSource = new ArrayList();
        protected ArrayList _nhanVienSource = new ArrayList();
        protected ArrayList _nhomCS = new ArrayList();
        protected ArrayList _quaySource = new ArrayList();
        protected bool isDataRefreshed = false;
        private Timer timer = new Timer();

        public MedUIBase()
        {
            if (Program.ACSource == null)
            {
                Program.ACSource = new ActiveACSource();
            }
            if (Program.ACSource != null)
            {
                this._quaySource = Program.ACSource.QuaySource;
                this._csSource = Program.ACSource.CsSource;
                this._medSource = Program.ACSource.MedicineSource;
                this._dvtSource = Program.ACSource.DVTSource;
                this._nhanVienSource = Program.ACSource.NhanVienSource;
                this._nhomCS = Program.ACSource.CSTypeSource;
                this._loaiKiemKe = Program.ACSource.LoaiKiemKe;
                this._loaiThuChi = Program.ACSource.LoaiThuChi;
                this._loaiThuoc = Program.ACSource.LoaiThuoc;
            }
            Program.ACSource.AddDataChangeListener(this);
            this.timer.Interval = 0x3e8;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.timer.Start();
        }

        private void _refresh()
        {
            this._quaySource = Program.ACSource.QuaySource;
            this._csSource = Program.ACSource.CsSource;
            this._medSource = Program.ACSource.MedicineSource;
            this._dvtSource = Program.ACSource.DVTSource;
            this._nhanVienSource = Program.ACSource.NhanVienSource;
            this._nhomCS = Program.ACSource.CSTypeSource;
            this._loaiKiemKe = Program.ACSource.LoaiKiemKe;
            this._loaiThuChi = Program.ACSource.LoaiThuChi;
            this.isDataRefreshed = true;
        }

        ~MedUIBase()
        {
            this.timer.Stop();
            this.timer.Dispose();
        }

        public virtual void loadAC()
        {
        }

        void IRefresh.Refresh()
        {
            this._refresh();
        }

        public virtual void RefreshAC()
        {
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.isDataRefreshed)
            {
                this.RefreshAC();
                this.isDataRefreshed = false;
            }
        }
    }
}
