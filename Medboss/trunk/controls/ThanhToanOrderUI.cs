namespace Nammedia.Medboss.controls
{
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Style;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ThanhToanOrderUI : MedUIBase
    {
        private int _MaHoaDonNhap;
        private int _MaHoaDonThanhToan;
        private int[] a_DonGiaBan;
        private int[] a_DonGiaNhap;
        private string[] a_DVTs;
        private int[] a_MaThuocs;
        private int[] a_MaThuocTraoDois;
        private ArrayList a_TenThuocs;
        private Button butCapNhat;
        private Button butTaiHD;
        private Button butTaiHoaDonThanhToan;
        private Button butThanhToanMoi;
        private Button butXemHoaDonNhap;
        private Button butXoaThanhToan;
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private ComboBox cboThaoTac;
        private CheckBox chkThanhToanAll;
        private DataGridViewTextBoxColumn colLichSuChietKhau;
        private DataGridViewTextBoxColumn colLichSuDonGia;
        private DataGridViewTextBoxColumn colLichSuDonGiaBan;
        private DataGridViewTextBoxColumn colLichSuDVT;
        private DataGridViewTextBoxColumn colLichSuGhiChu;
        private DataGridViewTextBoxColumn colLichSuLoaiHoaDon;
        private DataGridViewTextBoxColumn colLichSuMaHoaDon;
        private DataGridViewTextBoxColumn colLichSuMaThuoc;
        private DataGridViewTextBoxColumn colLichSuMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colLichSuNgay;
        private DataGridViewTextBoxColumn colLichSuSoLuong;
        private DataGridViewTextBoxColumn colLichSuTenThuoc;
        private DataGridViewTextBoxColumn colLichSuThanhTienBan;
        private DataGridViewTextBoxColumn colLichSuThanhTienNhap;
        private DataGridViewTextBoxColumn colLichSuTienChietKhau;
        private DataGridViewTextBoxColumn colNotDonGiaBan;
        private DataGridViewTextBoxColumn colNotDonGiaNhap;
        private DataGridViewTextBoxColumn colNotDVT;
        private DataGridViewTextBoxColumn colNotMaThuoc;
        private DataGridViewTextBoxColumn colNotMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colNotSoLuong;
        private DataGridViewTextBoxColumn colNotTenThuoc;
        private DataGridViewTextBoxColumn colNotThanhTienBan;
        private DataGridViewTextBoxColumn colNotThanhTienNhap;
        private DataGridViewTextBoxColumn colThanhToanChietKhau;
        private DataGridViewTextBoxColumn colThanhToanDonGiaBan;
        private DataGridViewTextBoxColumn colThanhToanDonGiaNhap;
        private DataGridViewTextBoxColumn colThanhToanDVT;
        private DataGridViewTextBoxColumn colThanhToanGhiChu;
        private DataGridViewTextBoxColumn colThanhToanMaThuoc;
        private DataGridViewTextBoxColumn colThanhToanMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colThanhToanSoLuong;
        private DataGridViewTextBoxColumn colThanhToanSTT;
        private DataGridViewTextBoxColumn colThanhToanTenThuoc;
        private DataGridViewTextBoxColumn colThanhToanThanhTienBan;
        private DataGridViewTextBoxColumn colThanhToanThanhTienNhap;
        private DataGridViewTextBoxColumn colThanhToanTienChietKhau;
        private IContainer components;
        private DataGridView dgrLichSuNhapThuocChiTiet;
        private DataGridView dgrNotHoaDonNhap;
        private DataGridView dgrThanhToanHoaDon;
        private Hashtable eventBag;
        private HoaDonNhapThuocInfo hdn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblMaHDN;
        private Label lblNhaCungCap;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private System.Windows.Forms.TabControl tabHistory;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TabPage tbpChuaThanhToan;
        private TabPage tbpQuaTrinhThanhToan;
        private TabPage tbpThanhToan;
        private TextBox txtChietKhau;
        private TextBox txtKhachHang;
        private TextBox txtMaHDN;
        private TextBox txtNhanVien;
        private TextBox txtTienNo;
        private TextBox txtTotalThanhToanBan;
        private TextBox txtTotalThanhToanCK;
        private TextBox txtTotalThanhToanNhap;

        public event ViewFunc SelectedItemView
        {
            add
            {
                this.eventBag["SelectedItemView"] = (ViewFunc) Delegate.Combine((ViewFunc) this.eventBag["SelectedItemView"], value);
            }
            remove
            {
                this.eventBag["SelectedItemView"] = (ViewFunc) Delegate.Remove((ViewFunc) this.eventBag["SelectedItemView"], value);
            }
        }

        public ThanhToanOrderUI()
        {
            this.eventBag = new Hashtable();
            this.components = null;
            this.hdn = new HoaDonNhapThuocInfo();
            this.InitializeComponent();
            this.hdn.Ngay = DateTime.Now;
            this.dgrLichSuNhapThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrLichSuNhapThuocChiTiet.DefaultCellStyle);
        }

        public ThanhToanOrderUI(int MaHoaDon)
        {
            this.eventBag = new Hashtable();
            this.components = null;
            this.InitializeComponent();
            this.LoadFormById(MaHoaDon);
        }

        public void bindFormating()
        {
            new FormatFactory().Bind(FormatType.Int, new object[] { 
                this.colLichSuChietKhau, this.colLichSuDonGia, this.colLichSuDonGiaBan, this.colLichSuSoLuong, this.colLichSuThanhTienBan, this.colLichSuThanhTienNhap, this.colLichSuTienChietKhau, this.colNotDonGiaBan, this.colNotDonGiaNhap, this.colNotSoLuong, this.colNotThanhTienBan, this.colNotThanhTienNhap, this.colThanhToanChietKhau, this.colThanhToanDonGiaBan, this.colThanhToanDonGiaNhap, this.colThanhToanSoLuong, 
                this.colThanhToanThanhTienBan, this.colThanhToanThanhTienNhap, this.colThanhToanTienChietKhau, this.txtChietKhau, this.txtTienNo, this.txtTotalThanhToanBan, this.txtTotalThanhToanCK, this.txtTotalThanhToanNhap
             });
        }

        private void butCapNhat_Click(object sender, EventArgs e)
        {
            if (this.cboThaoTac.SelectedIndex == 0)
            {
                HoaDonTraLaiNhapController hdtl = new HoaDonTraLaiNhapController();
                HoaDonTraLaiInfo hdtli = new HoaDonTraLaiInfo();
                hdtli.HoaDonNhapBan = this._MaHoaDonNhap;
                hdtli.MaKhachHang = this.hdn.MaKhachHang;
                hdtli.MaNhanVien = this.hdn.MaNhanVien;
                hdtli.MaQuay = this.hdn.MaQuay;
                hdtli.Ngay = this.cboNgay.Value;
                hdtli.ThuHayChi = false;
                hdtli.MaHoaDon = this._MaHoaDonThanhToan;
                foreach (DataGridViewRow dgvr in (IEnumerable) this.dgrThanhToanHoaDon.Rows)
                {
                    TraLaiChiTietInfo tlct = new TraLaiChiTietInfo();
                    tlct.DonGia = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                    tlct.GhiChu = ConvertHelper.getString(dgvr.Cells[this.colThanhToanGhiChu.Index].Value);
                    tlct.MaThuocTraoDoi = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value);
                    tlct.SoLuong = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanSoLuong.Index].Value);
                    hdtli.TraLaiChiTiet.Add(tlct);
                }
                HoaDonTraLaiNhapController hdtlc = new HoaDonTraLaiNhapController();
                if (hdtli.MaHoaDon > 0)
                {
                    hdtlc.Update(hdtli);
                }
                else
                {
                    hdtlc.Insert(hdtli);
                }
            }
            else
            {
                HoaDonThanhToanInfo hdtt = new HoaDonThanhToanInfo();
                hdtt.MaHoaDonNhap = this._MaHoaDonNhap;
                hdtt.MaThanhToan = this._MaHoaDonThanhToan;
                hdtt.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text.Trim());
                hdtt.NgayThanhToan = this.cboNgay.Value;
                foreach (DataGridViewRow dgvr in (IEnumerable) this.dgrThanhToanHoaDon.Rows)
                {
                    ThanhToanChiTietInfo ttct = new ThanhToanChiTietInfo();
                    ttct.ChietKhau = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanChietKhau.Index].Value);
                    ttct.MaThuocTraoDoi = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value);
                    ttct.SoLuong = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanSoLuong.Index].Value);
                    ttct.TienChietKhau = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanTienChietKhau.Index].Value);
                    ttct.DonGia = ConvertHelper.getInt(dgvr.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                    hdtt.ThanhToanChiTiet.Add(ttct);
                }
                HoaDonThanhToanController hdnc = new HoaDonThanhToanController();
                if (hdtt.MaThanhToan > 0)
                {
                    hdnc.Update(hdtt);
                }
                else
                {
                    hdnc.Insert(hdtt);
                }
            }
            this.RefreshData();
        }

        private void butTaiHD_Click(object sender, EventArgs e)
        {
            try
            {
                int mahoadon = int.Parse(this.txtMaHDN.Text.Trim());
                this.LoadFormById(mahoadon);
                this.tabHistory.SelectedTab = this.tbpQuaTrinhThanhToan;
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void butTaiHoaDonThanhToan_Click(object sender, EventArgs e)
        {
            if (this.dgrLichSuNhapThuocChiTiet.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.dgrLichSuNhapThuocChiTiet.SelectedRows[0];
                this._MaHoaDonThanhToan = ConvertHelper.getInt(row.Cells[this.colLichSuMaHoaDon.Index].Value);
                if (ConvertHelper.getString(row.Cells[this.colLichSuLoaiHoaDon.Index].Value) == "ThanhToan")
                {
                    this.TaiHoaDonThanhToan(this._MaHoaDonThanhToan);
                }
                else
                {
                    this.TaiHoaDonTraLai(this._MaHoaDonThanhToan);
                }
                this.cboThaoTac.Enabled = false;
                this.tabHistory.SelectedTab = this.tbpThanhToan;
                this.LoadACForThongTinThanhToan();
                this.UpdateTongTien();
            }
        }

        private void butThanhToanMoi_Click(object sender, EventArgs e)
        {
            this._MaHoaDonThanhToan = 0;
            this.cboThaoTac.Enabled = true;
            this.LoadThongTinChuaThanhToan();
            this.tabHistory.SelectedTab = this.tbpThanhToan;
            this.LoadACForThongTinThanhToan();
            this.UpdateTongTien();
        }

        private void butXemHoaDonNhap_Click(object sender, EventArgs e)
        {
            ViewFunc func = (ViewFunc) this.eventBag["SelectedItemView"];
            ViewArg args = new ViewArg(DataType.HoaDonNhap, this._MaHoaDonNhap);
            if (func != null)
            {
                func(args);
            }
        }

        private void butXoaThanhToan_Click(object sender, EventArgs e)
        {
            if (this.cboThaoTac.SelectedIndex == 0)
            {
                new HoaDonTraLaiNhapController().Delete(this._MaHoaDonThanhToan);
            }
            else
            {
                new HoaDonThanhToanController().Delete(this._MaHoaDonThanhToan);
            }
            this.RefreshData();
        }

        private int calTotal(HoaDonNhapThuocInfo hdn)
        {
            int num = 0;
            foreach (NhapThuocChiTietInfo info in hdn.HoaDonChiTiet)
            {
                num += (int) Math.Round((double) (info.SoLuong * info.DonGiaNhap), 0);
            }
            return num;
        }

        private void chkThanhToanAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dgrLichSuNhapThuocChiTiet.Rows)
            {
            }
        }

        public void clear()
        {
            this.dgrLichSuNhapThuocChiTiet.Rows.Clear();
            this.txtKhachHang.Text = "";
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineInfo.DonGiaNhapBan dgnb;
                DataGridViewRow row = this.dgrLichSuNhapThuocChiTiet.Rows[e.RowIndex];
                if (e.ColumnIndex == this.colThanhToanMaThuoc.Index)
                {
                    MedicineController controller = new MedicineController();
                    int maThuoc = ConvertHelper.getInt(row.Cells[this.colThanhToanMaThuoc.Index].Value);
                    MedicineInfo medicineByMaThuoc = controller.GetMedicineByMaThuoc(maThuoc);
                    if ((medicineByMaThuoc.TenThuoc != "") && (medicineByMaThuoc.TenThuoc != null))
                    {
                        row.Cells[this.colThanhToanTenThuoc.Index].Value = medicineByMaThuoc.TenThuoc;
                        row.Cells[this.colThanhToanDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                        dgnb = controller.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colThanhToanTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colThanhToanDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                        row.Cells[this.colThanhToanDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
                    }
                }
                if ((e.ColumnIndex == this.colThanhToanTenThuoc.Index) || (e.ColumnIndex == this.colThanhToanDVT.Index))
                {
                    MedicineController controller3 = new MedicineController();
                    if (e.ColumnIndex == this.colThanhToanTenThuoc.Index)
                    {
                        ArrayList medicine = controller3.GetMedicine((string) row.Cells[this.colThanhToanTenThuoc.Index].Value);
                        if (medicine.Count > 0)
                        {
                            MedicineInfo info2 = (MedicineInfo) medicine[0];
                            row.Cells[this.colThanhToanMaThuoc.Index].Value = info2.MaThuoc;
                            row.Cells[this.colThanhToanDVT.Index].Value = MedicineController.GetDVTHinhThuc(info2);
                        }
                        else
                        {
                            row.Cells[this.colThanhToanMaThuoc.Index].Value = -1;
                        }
                    }
                    dgnb = controller3.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colThanhToanTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colThanhToanDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                    row.Cells[this.colThanhToanDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void dgrBanThuocChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dgrLichSuNhapThuocChiTiet.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colThanhToanSoLuong.Index)
            {
                this.updateDonGia(dr);
                this.updateThanhTienBan(dr);
            }
            else if (e.ColumnIndex == this.colThanhToanThanhTienNhap.Index)
            {
                this.updateDonGia(dr);
            }
            else if (e.ColumnIndex == this.colThanhToanDonGiaBan.Index)
            {
                this.updateThanhTienBan(dr);
            }
        }

        private void dgrNhapThuocChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgrNhapThuocChiTiet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.tinhTong();
        }

        private void dgrThanhToanHoaDon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int soluong;
            int dongianhap;
            int dongiaban;
            int chietkhau;
            int thanhtiennhap;
            DataGridViewRow row = this.dgrThanhToanHoaDon.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colThanhToanTenThuoc.Index)
            {
                string tenthuoc = ConvertHelper.getString(row.Cells[e.ColumnIndex].Value);
                int pos = -1;
                if (tenthuoc != null)
                {
                    pos = this.a_TenThuocs.IndexOf(tenthuoc);
                }
                if (pos != -1)
                {
                    row.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value = this.a_MaThuocTraoDois[pos];
                    row.Cells[this.colThanhToanDVT.Index].Value = this.a_DVTs[pos];
                    row.Cells[this.colThanhToanMaThuoc.Index].Value = this.a_MaThuocs[pos];
                    row.Cells[this.colThanhToanDonGiaNhap.Index].Value = this.a_DonGiaNhap[pos];
                    row.Cells[this.colThanhToanDonGiaBan.Index].Value = this.a_DonGiaBan[pos];
                    soluong = ConvertHelper.getInt(row.Cells[this.colThanhToanSoLuong.Index].Value);
                    dongianhap = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                    dongiaban = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaBan.Index].Value);
                    row.Cells[this.colThanhToanThanhTienBan.Index].Value = soluong * dongiaban;
                    row.Cells[this.colThanhToanThanhTienNhap.Index].Value = soluong * dongianhap;
                    chietkhau = ConvertHelper.getInt(row.Cells[this.colThanhToanChietKhau.Index].Value);
                    thanhtiennhap = ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                    row.Cells[this.colThanhToanTienChietKhau.Index].Value = (chietkhau * thanhtiennhap) / 100;
                    this.UpdateTongTien();
                }
            }
            if (e.ColumnIndex == this.colThanhToanSoLuong.Index)
            {
                soluong = ConvertHelper.getInt(row.Cells[this.colThanhToanSoLuong.Index].Value);
                dongianhap = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                dongiaban = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaBan.Index].Value);
                row.Cells[this.colThanhToanThanhTienBan.Index].Value = soluong * dongiaban;
                row.Cells[this.colThanhToanThanhTienNhap.Index].Value = soluong * dongianhap;
                chietkhau = ConvertHelper.getInt(row.Cells[this.colThanhToanChietKhau.Index].Value);
                thanhtiennhap = ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                row.Cells[this.colThanhToanTienChietKhau.Index].Value = (chietkhau * thanhtiennhap) / 100;
                this.UpdateTongTien();
            }
            if (e.ColumnIndex == this.colThanhToanChietKhau.Index)
            {
                chietkhau = ConvertHelper.getInt(row.Cells[this.colThanhToanChietKhau.Index].Value);
                thanhtiennhap = ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                row.Cells[this.colThanhToanTienChietKhau.Index].Value = (chietkhau * thanhtiennhap) / 100;
                this.UpdateTongTien();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public HoaDonNhapThuocInfo getHoaDonNhapThuoc()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this.hdn.Ngay = this.cboNgay.Value;
            this.hdn.TienNo = ConvertHelper.getInt(this.txtTienNo.Text);
            this.hdn.MaQuay = new QuayController().getMaQuay(this.cboQuay.Text);
            if (this.hdn.MaQuay == -1)
            {
                arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", this.cboQuay.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.cboQuay;
                ufargs.Type = UnfoundType.Quay;
                throw new UnknownValueException(ufargs);
            }
            if (this.hdn.MaQuay == -1)
            {
                throw new InvalidException("Kh\x00f4ng thấy quầy.");
            }
            this.hdn.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text);
            this.hdn.ChietKhau = ConvertHelper.getInt(this.txtChietKhau.Text);
            if ((this.hdn.MaNhanVien == -1) && (this.txtNhanVien.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenNhanVien, "Nh\x00e2n vi\x00ean", this.txtNhanVien.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtNhanVien;
                ufargs.Type = UnfoundType.NV;
                throw new UnknownValueException(ufargs);
            }
            this.hdn.MaKhachHang = new CSController().GetIdByName(this.txtKhachHang.Text);
            if ((this.hdn.MaKhachHang == -1) && (this.txtKhachHang.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenKhachHang, "Nh\x00e0 cung cấp", this.txtKhachHang.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtKhachHang;
                ufargs.Type = UnfoundType.DoiTac;
                throw new UnknownValueException(ufargs);
            }
            this.hdn.HoaDonChiTiet.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgrLichSuNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    NhapThuocChiTietInfo info = new NhapThuocChiTietInfo();
                    info.DonGiaNhap = (int) ConvertHelper.getDouble(row.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                    info.DonGiaBan = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaBan.Index].Value);
                    info.GhiChu = ConvertHelper.getString(row.Cells[this.colThanhToanGhiChu.Index].Value);
                    string tenThuoc = ConvertHelper.getString(row.Cells[this.colThanhToanTenThuoc.Index].Value);
                    if (tenThuoc.Trim() == "")
                    {
                        throw new InvalidException("T\x00ean thuốc kh\x00f4ng được rỗng");
                    }
                    string dVT = ConvertHelper.getString(row.Cells[this.colThanhToanDVT.Index].Value);
                    if (dVT.Trim() == "")
                    {
                        throw new InvalidException("Đơn vị t\x00ednh kh\x00f4ng được rỗng");
                    }
                    info.MaThuocTraoDoi = new MedicineController().getMaThuocTraoDoi(tenThuoc, dVT);
                    if (info.MaThuocTraoDoi == -1)
                    {
                        UnfoundArgs args2 = new UnfoundArgs();
                        args2.Type = UnfoundType.ThuocDVT;
                        arg = new UnfoundArg();
                        arg.Key = FieldKey.TenThuoc;
                        arg.Value = tenThuoc;
                        arg.Field = "T\x00ean Thuốc";
                        args2.fieldValue.Add(arg);
                        UnfoundArg arg2 = new UnfoundArg();
                        arg2.Key = FieldKey.TenDVT;
                        arg2.Field = "ĐVT";
                        arg2.Value = dVT;
                        args2.fieldValue.Add(arg2);
                        args2.control = this.dgrLichSuNhapThuocChiTiet;
                        throw new UnknownValueException(args2);
                    }
                    info.SoLuong = ConvertHelper.getInt(row.Cells[this.colThanhToanSoLuong.Index].Value);
                    this.hdn.HoaDonChiTiet.Add(info);
                }
            }
            this.hdn.ThucThu = ConvertHelper.getInt(this.txtTotalThanhToanBan.Text);
            if (this.hdn.HoaDonChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            return this.hdn;
        }

        private void ImportOrderUI_Load(object sender, EventArgs e)
        {
            this.bindFormating();
            this.cboQuay.DataSource = base._quaySource;
            this.cboQuay.DisplayMember = "TenQuay";
            this.loadAC();
            this.dgrLichSuNhapThuocChiTiet.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dgrLichSuNhapThuocChiTiet.CellValueChanged += new DataGridViewCellEventHandler(this.dgrBanThuocChiTiet_CellValueChanged);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle25 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle26 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle27 = new DataGridViewCellStyle();
            this.tabItem1 = new TabItem(this.components);
            this.label5 = new Label();
            this.txtTotalThanhToanNhap = new TextBox();
            this.label2 = new Label();
            this.lblTitle = new Label();
            this.txtTienNo = new TextBox();
            this.label4 = new Label();
            this.txtKhachHang = new TextBox();
            this.lblNhaCungCap = new Label();
            this.label1 = new Label();
            this.label7 = new Label();
            this.txtNhanVien = new TextBox();
            this.label8 = new Label();
            this.cboQuay = new ComboBox();
            this.txtTotalThanhToanBan = new TextBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.tabHistory = new System.Windows.Forms.TabControl();
            this.tbpQuaTrinhThanhToan = new TabPage();
            this.dgrLichSuNhapThuocChiTiet = new DataGridView();
            this.colLichSuMaHoaDon = new DataGridViewTextBoxColumn();
            this.colLichSuNgay = new DataGridViewTextBoxColumn();
            this.colLichSuLoaiHoaDon = new DataGridViewTextBoxColumn();
            this.colLichSuMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colLichSuMaThuoc = new DataGridViewTextBoxColumn();
            this.colLichSuTenThuoc = new DataGridViewTextBoxColumn();
            this.colLichSuDVT = new DataGridViewTextBoxColumn();
            this.colLichSuSoLuong = new DataGridViewTextBoxColumn();
            this.colLichSuDonGia = new DataGridViewTextBoxColumn();
            this.colLichSuThanhTienNhap = new DataGridViewTextBoxColumn();
            this.colLichSuDonGiaBan = new DataGridViewTextBoxColumn();
            this.colLichSuThanhTienBan = new DataGridViewTextBoxColumn();
            this.colLichSuChietKhau = new DataGridViewTextBoxColumn();
            this.colLichSuTienChietKhau = new DataGridViewTextBoxColumn();
            this.colLichSuGhiChu = new DataGridViewTextBoxColumn();
            this.tbpChuaThanhToan = new TabPage();
            this.dgrNotHoaDonNhap = new DataGridView();
            this.colNotMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colNotMaThuoc = new DataGridViewTextBoxColumn();
            this.colNotTenThuoc = new DataGridViewTextBoxColumn();
            this.colNotDVT = new DataGridViewTextBoxColumn();
            this.colNotSoLuong = new DataGridViewTextBoxColumn();
            this.colNotDonGiaNhap = new DataGridViewTextBoxColumn();
            this.colNotThanhTienNhap = new DataGridViewTextBoxColumn();
            this.colNotDonGiaBan = new DataGridViewTextBoxColumn();
            this.colNotThanhTienBan = new DataGridViewTextBoxColumn();
            this.tbpThanhToan = new TabPage();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.dgrThanhToanHoaDon = new DataGridView();
            this.colThanhToanMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colThanhToanSTT = new DataGridViewTextBoxColumn();
            this.colThanhToanMaThuoc = new DataGridViewTextBoxColumn();
            this.colThanhToanTenThuoc = new DataGridViewTextBoxColumn();
            this.colThanhToanDVT = new DataGridViewTextBoxColumn();
            this.colThanhToanSoLuong = new DataGridViewTextBoxColumn();
            this.colThanhToanDonGiaNhap = new DataGridViewTextBoxColumn();
            this.colThanhToanThanhTienNhap = new DataGridViewTextBoxColumn();
            this.colThanhToanChietKhau = new DataGridViewTextBoxColumn();
            this.colThanhToanTienChietKhau = new DataGridViewTextBoxColumn();
            this.colThanhToanDonGiaBan = new DataGridViewTextBoxColumn();
            this.colThanhToanThanhTienBan = new DataGridViewTextBoxColumn();
            this.colThanhToanGhiChu = new DataGridViewTextBoxColumn();
            this.panel3 = new Panel();
            this.label3 = new Label();
            this.cboThaoTac = new ComboBox();
            this.cboNgay = new DateTimePicker();
            this.panel4 = new Panel();
            this.txtTotalThanhToanCK = new TextBox();
            this.label6 = new Label();
            this.butXoaThanhToan = new Button();
            this.label9 = new Label();
            this.butCapNhat = new Button();
            this.panel2 = new Panel();
            this.txtChietKhau = new TextBox();
            this.chkThanhToanAll = new CheckBox();
            this.panel1 = new Panel();
            this.butXemHoaDonNhap = new Button();
            this.butTaiHoaDonThanhToan = new Button();
            this.butThanhToanMoi = new Button();
            this.txtMaHDN = new TextBox();
            this.lblMaHDN = new Label();
            this.butTaiHD = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.tbpQuaTrinhThanhToan.SuspendLayout();
            ((ISupportInitialize) this.dgrLichSuNhapThuocChiTiet).BeginInit();
            this.tbpChuaThanhToan.SuspendLayout();
            ((ISupportInitialize) this.dgrNotHoaDonNhap).BeginInit();
            this.tbpThanhToan.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((ISupportInitialize) this.dgrThanhToanHoaDon).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x272, 6);
            this.label5.Name = "label5";
            this.label5.Size = new Size(30, 13);
            this.label5.TabIndex = 0x1a;
            this.label5.Text = "VND";
            this.label5.Visible = false;
            this.txtTotalThanhToanNhap.Location = new Point(0x42, 13);
            this.txtTotalThanhToanNhap.Name = "txtTotalThanhToanNhap";
            this.txtTotalThanhToanNhap.ReadOnly = true;
            this.txtTotalThanhToanNhap.Size = new Size(0x56, 20);
            this.txtTotalThanhToanNhap.TabIndex = 14;
            this.txtTotalThanhToanNhap.TextAlign = HorizontalAlignment.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 0x11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x39, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tiền Nhập";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(3, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x11d, 0x1d);
            this.lblTitle.TabIndex = 0x17;
            this.lblTitle.Text = "Thanh to\x00e1n h\x00f3a đơn nhập";
            this.txtTienNo.Location = new Point(510, 0);
            this.txtTienNo.Name = "txtTienNo";
            this.txtTienNo.Size = new Size(100, 20);
            this.txtTienNo.TabIndex = 11;
            this.txtTienNo.Text = "0";
            this.txtTienNo.TextAlign = HorizontalAlignment.Right;
            this.txtTienNo.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1cd, 6);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2b, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tiền &nợ";
            this.label4.Visible = false;
            this.txtKhachHang.Location = new Point(0xe8, 40);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new Size(100, 20);
            this.txtKhachHang.TabIndex = 7;
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new Point(0x97, 40);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new Size(0x4b, 13);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "Nh\x00e0 &cung cấp";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x191, 3);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nh\x00e2n &vi\x00ean";
            this.label7.Visible = false;
            this.txtNhanVien.Location = new Point(0x126, 3);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            this.txtNhanVien.Visible = false;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(7, 40);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "&Quầy";
            this.cboQuay.Enabled = false;
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x2d, 0x25);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 5;
            this.txtTotalThanhToanBan.Location = new Point(0xd5, 14);
            this.txtTotalThanhToanBan.Name = "txtTotalThanhToanBan";
            this.txtTotalThanhToanBan.ReadOnly = true;
            this.txtTotalThanhToanBan.Size = new Size(0x79, 20);
            this.txtTotalThanhToanBan.TabIndex = 0x10;
            this.txtTotalThanhToanBan.Text = "0";
            this.txtTotalThanhToanBan.TextAlign = HorizontalAlignment.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.tabHistory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x303, 0x1f6);
            this.tableLayoutPanel1.TabIndex = 0x20;
            this.tabHistory.Controls.Add(this.tbpQuaTrinhThanhToan);
            this.tabHistory.Controls.Add(this.tbpChuaThanhToan);
            this.tabHistory.Controls.Add(this.tbpThanhToan);
            this.tabHistory.Dock = DockStyle.Fill;
            this.tabHistory.Location = new Point(3, 0x67);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.SelectedIndex = 0;
            this.tabHistory.Size = new Size(0x2fd, 0x15c);
            this.tabHistory.TabIndex = 0x1f;
            this.tbpQuaTrinhThanhToan.Controls.Add(this.dgrLichSuNhapThuocChiTiet);
            this.tbpQuaTrinhThanhToan.Location = new Point(4, 0x16);
            this.tbpQuaTrinhThanhToan.Name = "tbpQuaTrinhThanhToan";
            this.tbpQuaTrinhThanhToan.Padding = new Padding(3);
            this.tbpQuaTrinhThanhToan.Size = new Size(0x2f5, 0x142);
            this.tbpQuaTrinhThanhToan.TabIndex = 0;
            this.tbpQuaTrinhThanhToan.Text = "Qu\x00e1 tr\x00ecnh thanh to\x00e1n";
            this.tbpQuaTrinhThanhToan.UseVisualStyleBackColor = true;
            this.dgrLichSuNhapThuocChiTiet.AllowUserToAddRows = false;
            this.dgrLichSuNhapThuocChiTiet.AllowUserToDeleteRows = false;
            this.dgrLichSuNhapThuocChiTiet.AllowUserToOrderColumns = true;
            this.dgrLichSuNhapThuocChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrLichSuNhapThuocChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrLichSuNhapThuocChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colLichSuMaHoaDon, this.colLichSuNgay, this.colLichSuLoaiHoaDon, this.colLichSuMaThuocTraoDoi, this.colLichSuMaThuoc, this.colLichSuTenThuoc, this.colLichSuDVT, this.colLichSuSoLuong, this.colLichSuDonGia, this.colLichSuThanhTienNhap, this.colLichSuDonGiaBan, this.colLichSuThanhTienBan, this.colLichSuChietKhau, this.colLichSuTienChietKhau, this.colLichSuGhiChu });
            this.dgrLichSuNhapThuocChiTiet.Dock = DockStyle.Fill;
            this.dgrLichSuNhapThuocChiTiet.Location = new Point(3, 3);
            this.dgrLichSuNhapThuocChiTiet.Name = "dgrLichSuNhapThuocChiTiet";
            this.dgrLichSuNhapThuocChiTiet.ReadOnly = true;
            this.dgrLichSuNhapThuocChiTiet.Size = new Size(0x2ef, 0x13c);
            this.dgrLichSuNhapThuocChiTiet.TabIndex = 12;
            this.dgrLichSuNhapThuocChiTiet.UserDeletedRow += new DataGridViewRowEventHandler(this.dgrNhapThuocChiTiet_UserDeletedRow);
            this.dgrLichSuNhapThuocChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrNhapThuocChiTiet_DataError);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopRight;
            this.colLichSuMaHoaDon.DefaultCellStyle = dataGridViewCellStyle1;
            this.colLichSuMaHoaDon.HeaderText = "M\x00e3 H\x00f3a Đơn";
            this.colLichSuMaHoaDon.Name = "colLichSuMaHoaDon";
            this.colLichSuMaHoaDon.ReadOnly = true;
            this.colLichSuMaHoaDon.Width = 0x56;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.TopRight;
            this.colLichSuNgay.DefaultCellStyle = dataGridViewCellStyle2;
            this.colLichSuNgay.HeaderText = "Ng\x00e0y";
            this.colLichSuNgay.Name = "colLichSuNgay";
            this.colLichSuNgay.ReadOnly = true;
            this.colLichSuNgay.Width = 0x39;
            this.colLichSuLoaiHoaDon.HeaderText = "Loại H\x00f3a Đơn";
            this.colLichSuLoaiHoaDon.Name = "colLichSuLoaiHoaDon";
            this.colLichSuLoaiHoaDon.ReadOnly = true;
            this.colLichSuLoaiHoaDon.Width = 90;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopRight;
            this.colLichSuMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle3;
            this.colLichSuMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colLichSuMaThuocTraoDoi.Name = "colLichSuMaThuocTraoDoi";
            this.colLichSuMaThuocTraoDoi.ReadOnly = true;
            this.colLichSuMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuMaThuocTraoDoi.Visible = false;
            this.colLichSuMaThuocTraoDoi.Width = 0x61;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.TopRight;
            this.colLichSuMaThuoc.DefaultCellStyle = dataGridViewCellStyle4;
            this.colLichSuMaThuoc.HeaderText = "M\x00e3 Thuốc";
            this.colLichSuMaThuoc.Name = "colLichSuMaThuoc";
            this.colLichSuMaThuoc.ReadOnly = true;
            this.colLichSuMaThuoc.Width = 0x4b;
            this.colLichSuTenThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.colLichSuTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colLichSuTenThuoc.Name = "colLichSuTenThuoc";
            this.colLichSuTenThuoc.ReadOnly = true;
            this.colLichSuTenThuoc.Width = 0x4b;
            this.colLichSuDVT.DataPropertyName = "DVT";
            this.colLichSuDVT.HeaderText = "ĐVT";
            this.colLichSuDVT.Name = "colLichSuDVT";
            this.colLichSuDVT.ReadOnly = true;
            this.colLichSuDVT.Resizable = DataGridViewTriState.True;
            this.colLichSuDVT.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuDVT.Width = 0x23;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N0";
            this.colLichSuSoLuong.DefaultCellStyle = dataGridViewCellStyle5;
            this.colLichSuSoLuong.HeaderText = "Số lượng";
            this.colLichSuSoLuong.Name = "colLichSuSoLuong";
            this.colLichSuSoLuong.ReadOnly = true;
            this.colLichSuSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuSoLuong.Width = 50;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colLichSuDonGia.DefaultCellStyle = dataGridViewCellStyle6;
            this.colLichSuDonGia.HeaderText = "Đơn gi\x00e1 nhập";
            this.colLichSuDonGia.Name = "colLichSuDonGia";
            this.colLichSuDonGia.ReadOnly = true;
            this.colLichSuDonGia.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuDonGia.Width = 0x45;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.Format = "N0";
            this.colLichSuThanhTienNhap.DefaultCellStyle = dataGridViewCellStyle7;
            this.colLichSuThanhTienNhap.HeaderText = "Th\x00e0nh tiền nhập";
            this.colLichSuThanhTienNhap.Name = "colLichSuThanhTienNhap";
            this.colLichSuThanhTienNhap.ReadOnly = true;
            this.colLichSuThanhTienNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuThanhTienNhap.Width = 60;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Format = "N0";
            this.colLichSuDonGiaBan.DefaultCellStyle = dataGridViewCellStyle8;
            this.colLichSuDonGiaBan.HeaderText = "Đơn gi\x00e1 b\x00e1n";
            this.colLichSuDonGiaBan.Name = "colLichSuDonGiaBan";
            this.colLichSuDonGiaBan.ReadOnly = true;
            this.colLichSuDonGiaBan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuDonGiaBan.Width = 0x30;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle9.Format = "N0";
            this.colLichSuThanhTienBan.DefaultCellStyle = dataGridViewCellStyle9;
            this.colLichSuThanhTienBan.HeaderText = "Th\x00e0nh tiền b\x00e1n";
            this.colLichSuThanhTienBan.Name = "colLichSuThanhTienBan";
            this.colLichSuThanhTienBan.ReadOnly = true;
            this.colLichSuThanhTienBan.Width = 0x4f;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = "0";
            this.colLichSuChietKhau.DefaultCellStyle = dataGridViewCellStyle10;
            this.colLichSuChietKhau.HeaderText = "Chiết khấu (%)";
            this.colLichSuChietKhau.Name = "colLichSuChietKhau";
            this.colLichSuChietKhau.ReadOnly = true;
            this.colLichSuChietKhau.Width = 0x4f;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = "0";
            this.colLichSuTienChietKhau.DefaultCellStyle = dataGridViewCellStyle11;
            this.colLichSuTienChietKhau.HeaderText = "Tiền Chiết Khấu";
            this.colLichSuTienChietKhau.Name = "colLichSuTienChietKhau";
            this.colLichSuTienChietKhau.ReadOnly = true;
            this.colLichSuTienChietKhau.Width = 0x63;
            this.colLichSuGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colLichSuGhiChu.Name = "colLichSuGhiChu";
            this.colLichSuGhiChu.ReadOnly = true;
            this.colLichSuGhiChu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colLichSuGhiChu.Width = 0x2d;
            this.tbpChuaThanhToan.Controls.Add(this.dgrNotHoaDonNhap);
            this.tbpChuaThanhToan.Location = new Point(4, 0x16);
            this.tbpChuaThanhToan.Name = "tbpChuaThanhToan";
            this.tbpChuaThanhToan.Padding = new Padding(3);
            this.tbpChuaThanhToan.Size = new Size(0x2f5, 0x142);
            this.tbpChuaThanhToan.TabIndex = 1;
            this.tbpChuaThanhToan.Text = "Thuốc chưa thanh to\x00e1n";
            this.tbpChuaThanhToan.UseVisualStyleBackColor = true;
            this.dgrNotHoaDonNhap.AllowUserToAddRows = false;
            this.dgrNotHoaDonNhap.AllowUserToDeleteRows = false;
            this.dgrNotHoaDonNhap.AllowUserToOrderColumns = true;
            this.dgrNotHoaDonNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrNotHoaDonNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrNotHoaDonNhap.Columns.AddRange(new DataGridViewColumn[] { this.colNotMaThuocTraoDoi, this.colNotMaThuoc, this.colNotTenThuoc, this.colNotDVT, this.colNotSoLuong, this.colNotDonGiaNhap, this.colNotThanhTienNhap, this.colNotDonGiaBan, this.colNotThanhTienBan });
            this.dgrNotHoaDonNhap.Dock = DockStyle.Fill;
            this.dgrNotHoaDonNhap.Location = new Point(3, 3);
            this.dgrNotHoaDonNhap.Name = "dgrNotHoaDonNhap";
            this.dgrNotHoaDonNhap.ReadOnly = true;
            this.dgrNotHoaDonNhap.Size = new Size(0x2ef, 0x13c);
            this.dgrNotHoaDonNhap.TabIndex = 0x13;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.TopRight;
            this.colNotMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle12;
            this.colNotMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colNotMaThuocTraoDoi.Name = "colNotMaThuocTraoDoi";
            this.colNotMaThuocTraoDoi.ReadOnly = true;
            this.colNotMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotMaThuocTraoDoi.Visible = false;
            this.colNotMaThuocTraoDoi.Width = 0x61;
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.TopRight;
            this.colNotMaThuoc.DefaultCellStyle = dataGridViewCellStyle13;
            this.colNotMaThuoc.HeaderText = "M\x00e3 Thuốc";
            this.colNotMaThuoc.Name = "colNotMaThuoc";
            this.colNotMaThuoc.ReadOnly = true;
            this.colNotMaThuoc.Width = 0x4b;
            this.colNotTenThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.colNotTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colNotTenThuoc.Name = "colNotTenThuoc";
            this.colNotTenThuoc.ReadOnly = true;
            this.colNotTenThuoc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotTenThuoc.Width = 0x38;
            this.colNotDVT.DataPropertyName = "DVT";
            this.colNotDVT.HeaderText = "ĐVT";
            this.colNotDVT.Name = "colNotDVT";
            this.colNotDVT.ReadOnly = true;
            this.colNotDVT.Resizable = DataGridViewTriState.True;
            this.colNotDVT.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotDVT.Width = 0x23;
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle14.Format = "N0";
            this.colNotSoLuong.DefaultCellStyle = dataGridViewCellStyle14;
            this.colNotSoLuong.HeaderText = "Số lượng";
            this.colNotSoLuong.Name = "colNotSoLuong";
            this.colNotSoLuong.ReadOnly = true;
            this.colNotSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotSoLuong.Width = 50;
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle15.Format = "N0";
            this.colNotDonGiaNhap.DefaultCellStyle = dataGridViewCellStyle15;
            this.colNotDonGiaNhap.HeaderText = "Đơn gi\x00e1 nhập";
            this.colNotDonGiaNhap.Name = "colNotDonGiaNhap";
            this.colNotDonGiaNhap.ReadOnly = true;
            this.colNotDonGiaNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotDonGiaNhap.Width = 0x45;
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle16.Format = "N0";
            this.colNotThanhTienNhap.DefaultCellStyle = dataGridViewCellStyle16;
            this.colNotThanhTienNhap.HeaderText = "Th\x00e0nh tiền nhập";
            this.colNotThanhTienNhap.Name = "colNotThanhTienNhap";
            this.colNotThanhTienNhap.ReadOnly = true;
            this.colNotThanhTienNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotThanhTienNhap.Width = 60;
            dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle17.Format = "N0";
            this.colNotDonGiaBan.DefaultCellStyle = dataGridViewCellStyle17;
            this.colNotDonGiaBan.HeaderText = "Đơn gi\x00e1 b\x00e1n";
            this.colNotDonGiaBan.Name = "colNotDonGiaBan";
            this.colNotDonGiaBan.ReadOnly = true;
            this.colNotDonGiaBan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNotDonGiaBan.Width = 0x30;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle18.Format = "N0";
            this.colNotThanhTienBan.DefaultCellStyle = dataGridViewCellStyle18;
            this.colNotThanhTienBan.HeaderText = "Th\x00e0nh tiền b\x00e1n";
            this.colNotThanhTienBan.Name = "colNotThanhTienBan";
            this.colNotThanhTienBan.ReadOnly = true;
            this.colNotThanhTienBan.Width = 0x4f;
            this.tbpThanhToan.Controls.Add(this.tableLayoutPanel2);
            this.tbpThanhToan.Location = new Point(4, 0x16);
            this.tbpThanhToan.Name = "tbpThanhToan";
            this.tbpThanhToan.Padding = new Padding(3);
            this.tbpThanhToan.Size = new Size(0x2f5, 0x142);
            this.tbpThanhToan.TabIndex = 2;
            this.tbpThanhToan.Text = "Thanh to\x00e1n";
            this.tbpThanhToan.UseVisualStyleBackColor = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.Controls.Add(this.dgrThanhToanHoaDon, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel2.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Location = new Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.27119f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 83.72881f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            this.tableLayoutPanel2.Size = new Size(0x2ef, 0x13c);
            this.tableLayoutPanel2.TabIndex = 0;
            this.dgrThanhToanHoaDon.AllowUserToOrderColumns = true;
            this.dgrThanhToanHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrThanhToanHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrThanhToanHoaDon.Columns.AddRange(new DataGridViewColumn[] { this.colThanhToanMaThuocTraoDoi, this.colThanhToanSTT, this.colThanhToanMaThuoc, this.colThanhToanTenThuoc, this.colThanhToanDVT, this.colThanhToanSoLuong, this.colThanhToanDonGiaNhap, this.colThanhToanThanhTienNhap, this.colThanhToanChietKhau, this.colThanhToanTienChietKhau, this.colThanhToanDonGiaBan, this.colThanhToanThanhTienBan, this.colThanhToanGhiChu });
            this.dgrThanhToanHoaDon.Dock = DockStyle.Fill;
            this.dgrThanhToanHoaDon.Location = new Point(3, 0x2e);
            this.dgrThanhToanHoaDon.Name = "dgrThanhToanHoaDon";
            this.dgrThanhToanHoaDon.Size = new Size(0x2e9, 0xd8);
            this.dgrThanhToanHoaDon.TabIndex = 0x12;
            this.dgrThanhToanHoaDon.CellEndEdit += new DataGridViewCellEventHandler(this.dgrThanhToanHoaDon_CellEndEdit);
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colThanhToanMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle19;
            this.colThanhToanMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colThanhToanMaThuocTraoDoi.Name = "colThanhToanMaThuocTraoDoi";
            this.colThanhToanMaThuocTraoDoi.ReadOnly = true;
            this.colThanhToanMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanMaThuocTraoDoi.Visible = false;
            this.colThanhToanMaThuocTraoDoi.Width = 0x61;
            this.colThanhToanSTT.HeaderText = "STT";
            this.colThanhToanSTT.Name = "colThanhToanSTT";
            this.colThanhToanSTT.Width = 0x35;
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.TopRight;
            this.colThanhToanMaThuoc.DefaultCellStyle = dataGridViewCellStyle20;
            this.colThanhToanMaThuoc.HeaderText = "M\x00e3 Thuốc";
            this.colThanhToanMaThuoc.Name = "colThanhToanMaThuoc";
            this.colThanhToanMaThuoc.ReadOnly = true;
            this.colThanhToanMaThuoc.Width = 0x4b;
            this.colThanhToanTenThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.colThanhToanTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colThanhToanTenThuoc.Name = "colThanhToanTenThuoc";
            this.colThanhToanTenThuoc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanTenThuoc.Width = 0x38;
            this.colThanhToanDVT.DataPropertyName = "DVT";
            this.colThanhToanDVT.HeaderText = "ĐVT";
            this.colThanhToanDVT.Name = "colThanhToanDVT";
            this.colThanhToanDVT.ReadOnly = true;
            this.colThanhToanDVT.Resizable = DataGridViewTriState.True;
            this.colThanhToanDVT.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanDVT.Width = 0x23;
            dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle21.Format = "N0";
            this.colThanhToanSoLuong.DefaultCellStyle = dataGridViewCellStyle21;
            this.colThanhToanSoLuong.HeaderText = "Số lượng";
            this.colThanhToanSoLuong.Name = "colThanhToanSoLuong";
            this.colThanhToanSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanSoLuong.Width = 50;
            dataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle22.Format = "N0";
            this.colThanhToanDonGiaNhap.DefaultCellStyle = dataGridViewCellStyle22;
            this.colThanhToanDonGiaNhap.HeaderText = "Đơn gi\x00e1 nhập";
            this.colThanhToanDonGiaNhap.Name = "colThanhToanDonGiaNhap";
            this.colThanhToanDonGiaNhap.ReadOnly = true;
            this.colThanhToanDonGiaNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanDonGiaNhap.Width = 0x45;
            dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle23.Format = "N0";
            dataGridViewCellStyle23.NullValue = "0";
            this.colThanhToanThanhTienNhap.DefaultCellStyle = dataGridViewCellStyle23;
            this.colThanhToanThanhTienNhap.HeaderText = "Th\x00e0nh tiền nhập";
            this.colThanhToanThanhTienNhap.Name = "colThanhToanThanhTienNhap";
            this.colThanhToanThanhTienNhap.ReadOnly = true;
            this.colThanhToanThanhTienNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanThanhTienNhap.Width = 60;
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle24.Format = "N0";
            dataGridViewCellStyle24.NullValue = "0";
            this.colThanhToanChietKhau.DefaultCellStyle = dataGridViewCellStyle24;
            this.colThanhToanChietKhau.HeaderText = "Chiết khấu (%)";
            this.colThanhToanChietKhau.Name = "colThanhToanChietKhau";
            this.colThanhToanChietKhau.Width = 0x4f;
            dataGridViewCellStyle25.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle25.Format = "N0";
            dataGridViewCellStyle25.NullValue = "0";
            this.colThanhToanTienChietKhau.DefaultCellStyle = dataGridViewCellStyle25;
            this.colThanhToanTienChietKhau.HeaderText = "Tiền Chiết Khấu";
            this.colThanhToanTienChietKhau.Name = "colThanhToanTienChietKhau";
            this.colThanhToanTienChietKhau.Width = 0x63;
            dataGridViewCellStyle26.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle26.Format = "N0";
            this.colThanhToanDonGiaBan.DefaultCellStyle = dataGridViewCellStyle26;
            this.colThanhToanDonGiaBan.HeaderText = "Đơn gi\x00e1 b\x00e1n";
            this.colThanhToanDonGiaBan.Name = "colThanhToanDonGiaBan";
            this.colThanhToanDonGiaBan.ReadOnly = true;
            this.colThanhToanDonGiaBan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanDonGiaBan.Width = 0x30;
            dataGridViewCellStyle27.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle27.Format = "N0";
            dataGridViewCellStyle27.NullValue = "0";
            this.colThanhToanThanhTienBan.DefaultCellStyle = dataGridViewCellStyle27;
            this.colThanhToanThanhTienBan.HeaderText = "Th\x00e0nh tiền b\x00e1n";
            this.colThanhToanThanhTienBan.Name = "colThanhToanThanhTienBan";
            this.colThanhToanThanhTienBan.ReadOnly = true;
            this.colThanhToanThanhTienBan.Width = 0x4f;
            this.colThanhToanGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colThanhToanGhiChu.Name = "colThanhToanGhiChu";
            this.colThanhToanGhiChu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhToanGhiChu.Width = 0x2d;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cboThaoTac);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cboNgay);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x2e9, 0x25);
            this.panel3.TabIndex = 0;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x93, 12);
            this.label3.Name = "label3";
            this.label3.Size = new Size(50, 13);
            this.label3.TabIndex = 0x20;
            this.label3.Text = "Thao t\x00e1c";
            this.cboThaoTac.FormattingEnabled = true;
            this.cboThaoTac.Items.AddRange(new object[] { "Trả Lại", "Thanh To\x00e1n" });
            this.cboThaoTac.Location = new Point(0xcb, 9);
            this.cboThaoTac.Name = "cboThaoTac";
            this.cboThaoTac.Size = new Size(0x79, 0x15);
            this.cboThaoTac.TabIndex = 0x1f;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x29, 8);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0x1c;
            this.panel4.Controls.Add(this.txtTotalThanhToanCK);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtTotalThanhToanBan);
            this.panel4.Controls.Add(this.txtTotalThanhToanNhap);
            this.panel4.Controls.Add(this.butXoaThanhToan);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.butCapNhat);
            this.panel4.Dock = DockStyle.Fill;
            this.panel4.Location = new Point(3, 0x10c);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x2e9, 0x2d);
            this.panel4.TabIndex = 0x13;
            this.txtTotalThanhToanCK.Location = new Point(0x194, 13);
            this.txtTotalThanhToanCK.Name = "txtTotalThanhToanCK";
            this.txtTotalThanhToanCK.ReadOnly = true;
            this.txtTotalThanhToanCK.Size = new Size(100, 20);
            this.txtTotalThanhToanCK.TabIndex = 0x20;
            this.txtTotalThanhToanCK.TextAlign = HorizontalAlignment.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(340, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3a, 13);
            this.label6.TabIndex = 0x1f;
            this.label6.Text = "Chiết khấu";
            this.butXoaThanhToan.Location = new Point(0x27a, 11);
            this.butXoaThanhToan.Name = "butXoaThanhToan";
            this.butXoaThanhToan.Size = new Size(0x62, 0x17);
            this.butXoaThanhToan.TabIndex = 30;
            this.butXoaThanhToan.Text = "X\x00f3a thanh to\x00e1n";
            this.butXoaThanhToan.UseVisualStyleBackColor = true;
            this.butXoaThanhToan.Click += new EventHandler(this.butXoaThanhToan_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x9e, 0x11);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x31, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tiền b\x00e1n";
            this.label9.Enter += new EventHandler(this.Thuc_enter);
            this.butCapNhat.Location = new Point(0x22c, 11);
            this.butCapNhat.Name = "butCapNhat";
            this.butCapNhat.Size = new Size(0x3e, 0x16);
            this.butCapNhat.TabIndex = 0x1d;
            this.butCapNhat.Text = "Cập nhật";
            this.butCapNhat.UseVisualStyleBackColor = true;
            this.butCapNhat.Click += new EventHandler(this.butCapNhat_Click);
            this.panel2.Controls.Add(this.txtChietKhau);
            this.panel2.Controls.Add(this.chkThanhToanAll);
            this.panel2.Controls.Add(this.cboQuay);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTienNo);
            this.panel2.Controls.Add(this.lblNhaCungCap);
            this.panel2.Controls.Add(this.txtNhanVien);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtKhachHang);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2fd, 0x5e);
            this.panel2.TabIndex = 0x11;
            this.txtChietKhau.Location = new Point(0x282, 3);
            this.txtChietKhau.Name = "txtChietKhau";
            this.txtChietKhau.Size = new Size(100, 20);
            this.txtChietKhau.TabIndex = 30;
            this.txtChietKhau.Visible = false;
            this.chkThanhToanAll.AutoSize = true;
            this.chkThanhToanAll.Location = new Point(0x152, 0x27);
            this.chkThanhToanAll.Name = "chkThanhToanAll";
            this.chkThanhToanAll.Size = new Size(0x98, 0x11);
            this.chkThanhToanAll.TabIndex = 0x1b;
            this.chkThanhToanAll.Text = "Đ\x00e3 thanh to\x00e1n cả h\x00f3a đơn";
            this.chkThanhToanAll.UseVisualStyleBackColor = true;
            this.chkThanhToanAll.CheckedChanged += new EventHandler(this.chkThanhToanAll_CheckedChanged);
            this.panel1.Controls.Add(this.butTaiHD);
            this.panel1.Controls.Add(this.lblMaHDN);
            this.panel1.Controls.Add(this.txtMaHDN);
            this.panel1.Controls.Add(this.butXemHoaDonNhap);
            this.panel1.Controls.Add(this.butTaiHoaDonThanhToan);
            this.panel1.Controls.Add(this.butThanhToanMoi);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x1c9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2fd, 0x2a);
            this.panel1.TabIndex = 0x10;
            this.butXemHoaDonNhap.Location = new Point(0x24f, 3);
            this.butXemHoaDonNhap.Name = "butXemHoaDonNhap";
            this.butXemHoaDonNhap.Size = new Size(0x76, 0x17);
            this.butXemHoaDonNhap.TabIndex = 0x1f;
            this.butXemHoaDonNhap.Text = "Xem H\x00f3a Đơn Nhập";
            this.butXemHoaDonNhap.UseVisualStyleBackColor = true;
            this.butXemHoaDonNhap.Click += new EventHandler(this.butXemHoaDonNhap_Click);
            this.butTaiHoaDonThanhToan.Location = new Point(0x148, 3);
            this.butTaiHoaDonThanhToan.Name = "butTaiHoaDonThanhToan";
            this.butTaiHoaDonThanhToan.Size = new Size(0x8e, 0x17);
            this.butTaiHoaDonThanhToan.TabIndex = 0x1d;
            this.butTaiHoaDonThanhToan.Text = "Tải H\x00f3a Đơn Thanh To\x00e1n";
            this.butTaiHoaDonThanhToan.UseVisualStyleBackColor = true;
            this.butTaiHoaDonThanhToan.Click += new EventHandler(this.butTaiHoaDonThanhToan_Click);
            this.butThanhToanMoi.Location = new Point(0x1dc, 3);
            this.butThanhToanMoi.Name = "butThanhToanMoi";
            this.butThanhToanMoi.Size = new Size(0x6d, 0x17);
            this.butThanhToanMoi.TabIndex = 0x1c;
            this.butThanhToanMoi.Text = "Bắt đầu thanh to\x00e1n";
            this.butThanhToanMoi.UseVisualStyleBackColor = true;
            this.butThanhToanMoi.Click += new EventHandler(this.butThanhToanMoi_Click);
            this.txtMaHDN.Location = new Point(0x6c, 5);
            this.txtMaHDN.Name = "txtMaHDN";
            this.txtMaHDN.Size = new Size(0x40, 20);
            this.txtMaHDN.TabIndex = 0x20;
            this.lblMaHDN.AutoSize = true;
            this.lblMaHDN.Location = new Point(5, 8);
            this.lblMaHDN.Name = "lblMaHDN";
            this.lblMaHDN.Size = new Size(0x61, 13);
            this.lblMaHDN.TabIndex = 0x21;
            this.lblMaHDN.Text = "M\x00e3 H\x00f3a Đơn Nhập";
            this.butTaiHD.Location = new Point(0xb2, 5);
            this.butTaiHD.Name = "butTaiHD";
            this.butTaiHD.Size = new Size(0x2f, 0x17);
            this.butTaiHD.TabIndex = 0x22;
            this.butTaiHD.Text = "Tải";
            this.butTaiHD.UseVisualStyleBackColor = true;
            this.butTaiHD.Click += new EventHandler(this.butTaiHD_Click);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ThanhToanOrderUI";
            base.Size = new Size(0x303, 0x1f6);
            base.Load += new EventHandler(this.ImportOrderUI_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.tbpQuaTrinhThanhToan.ResumeLayout(false);
            ((ISupportInitialize) this.dgrLichSuNhapThuocChiTiet).EndInit();
            this.tbpChuaThanhToan.ResumeLayout(false);
            ((ISupportInitialize) this.dgrNotHoaDonNhap).EndInit();
            this.tbpThanhToan.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((ISupportInitialize) this.dgrThanhToanHoaDon).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
        }

        private void LoadACForThongTinThanhToan()
        {
            this.a_DVTs = new string[this.dgrThanhToanHoaDon.Rows.Count];
            this.a_MaThuocs = new int[this.dgrThanhToanHoaDon.Rows.Count];
            this.a_MaThuocTraoDois = new int[this.dgrThanhToanHoaDon.Rows.Count];
            this.a_DonGiaBan = new int[this.dgrThanhToanHoaDon.Rows.Count];
            this.a_DonGiaNhap = new int[this.dgrThanhToanHoaDon.Rows.Count];
            this.a_TenThuocs = new ArrayList();
            int rowindex = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrThanhToanHoaDon.Rows)
            {
                if (!row.IsNewRow)
                {
                    this.a_TenThuocs.Add(ConvertHelper.getString(row.Cells[this.colThanhToanTenThuoc.Index].Value));
                    this.a_DVTs[rowindex] = ConvertHelper.getString(row.Cells[this.colThanhToanDVT.Index].Value);
                    this.a_MaThuocs[rowindex] = ConvertHelper.getInt(row.Cells[this.colThanhToanMaThuoc.Index].Value);
                    this.a_MaThuocTraoDois[rowindex] = ConvertHelper.getInt(row.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value);
                    this.a_DonGiaBan[rowindex] = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaBan.Index].Value);
                    this.a_DonGiaNhap[rowindex] = ConvertHelper.getInt(row.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                    rowindex++;
                }
            }
            base._acFactory.EnableAutocomplete(this.colThanhToanTenThuoc, ref this.a_TenThuocs);
        }

        private void LoadAllHoaDonThanhToanFromHDNhap(int hoadonnhap)
        {
            IDataReader dr = new HoaDonThanhToanController().GetThongTinThanhToanChiTiet(hoadonnhap);
            if (dr != null)
            {
                this.dgrLichSuNhapThuocChiTiet.Rows.Clear();
                while (dr.Read())
                {
                    int rowindex = this.dgrLichSuNhapThuocChiTiet.Rows.Add();
                    DataGridViewRow dgvr = this.dgrLichSuNhapThuocChiTiet.Rows[rowindex];
                    dgvr.Cells[this.colLichSuChietKhau.Index].Value = ConvertHelper.getInt(dr["ChietKhau"]);
                    dgvr.Cells[this.colLichSuDonGia.Index].Value = ConvertHelper.getInt(dr["DonGiaNhap"]);
                    dgvr.Cells[this.colLichSuDonGiaBan.Index].Value = ConvertHelper.getInt(dr["DonGiaBan"]);
                    dgvr.Cells[this.colLichSuDVT.Index].Value = ConvertHelper.getString(dr["DVT"]);
                    dgvr.Cells[this.colLichSuGhiChu.Index].Value = ConvertHelper.getString(dr["GhiChu"]);
                    dgvr.Cells[this.colLichSuLoaiHoaDon.Index].Value = ConvertHelper.getString(dr["Loai"]);
                    dgvr.Cells[this.colLichSuMaHoaDon.Index].Value = ConvertHelper.getInt(dr["MaHoaDon"]);
                    dgvr.Cells[this.colLichSuMaThuoc.Index].Value = ConvertHelper.getInt(dr["MaThuoc"]);
                    dgvr.Cells[this.colLichSuMaThuocTraoDoi.Index].Value = ConvertHelper.getInt(dr["MaThuocTraoDoi"]);
                    dgvr.Cells[this.colLichSuNgay.Index].Value = ConvertHelper.getDateTime(dr["Ngay"]).ToString("dd/MM/yyyy");
                    dgvr.Cells[this.colLichSuSoLuong.Index].Value = ConvertHelper.getInt(dr["SoLuong"]);
                    dgvr.Cells[this.colLichSuTenThuoc.Index].Value = ConvertHelper.getString(dr["TenThuoc"]);
                    dgvr.Cells[this.colLichSuThanhTienBan.Index].Value = ConvertHelper.getInt(dr["ThanhTienBan"]);
                    dgvr.Cells[this.colLichSuThanhTienNhap.Index].Value = ConvertHelper.getInt(dr["ThanhTienNhap"]);
                    dgvr.Cells[this.colLichSuTienChietKhau.Index].Value = ConvertHelper.getInt(dr["TienChietKhau"]);
                }
                dr.Close();
            }
        }

        private void LoadFormById(int MaHoaDon)
        {
            this._MaHoaDonNhap = MaHoaDon;
            this.dgrLichSuNhapThuocChiTiet.Rows.Clear();
            this.dgrNotHoaDonNhap.Rows.Clear();
            this.dgrThanhToanHoaDon.Rows.Clear();
            this.hdn = new HoaDonNhapThuocInfo();
            this.hdn = new HoaDonNhapThuocController().GetById(MaHoaDon);
            this.bindFormating();
            this.LoadAllHoaDonThanhToanFromHDNhap(MaHoaDon);
            if (this.hdn != null)
            {
                this.LoadHoaDonNhapInfo();
            }
        }

        private void LoadHoaDonNhapInfo()
        {
            CSInfo byId = new CSController().GetById(this.hdn.MaKhachHang);
            this.txtKhachHang.Text = byId.Ten;
            QuayController controller2 = new QuayController();
            this.cboQuay.Text = controller2.getQuay(this.hdn.MaQuay).TenQuay;
            NhanVienInfo info2 = new NhanVienController().GetById(this.hdn.MaNhanVien);
            if (info2.MaNhanVien != -1)
            {
                IFriendlyName name = info2;
                this.txtNhanVien.Text = name.FriendlyName();
            }
        }

        public void loadInfo(HoaDonNhapThuocInfo hoaDonNhap)
        {
            this.hdn = hoaDonNhap;
            this.cboNgay.Value = this.hdn.Ngay;
            CSInfo byId = new CSController().GetById(this.hdn.MaKhachHang);
            this.txtKhachHang.Text = byId.Ten;
            this.txtTienNo.Text = this.hdn.TienNo.ToString();
            this.txtTotalThanhToanNhap.Text = this.calTotal(this.hdn).ToString();
            this.txtTotalThanhToanBan.Text = this.hdn.ThucThu.ToString();
            this.txtChietKhau.Text = this.hdn.ChietKhau.ToString();
            QuayController controller2 = new QuayController();
            this.cboQuay.Text = controller2.getQuay(this.hdn.MaQuay).TenQuay;
            NhanVienInfo info2 = new NhanVienController().GetById(this.hdn.MaNhanVien);
            if (info2.MaNhanVien != -1)
            {
                IFriendlyName name = info2;
                this.txtNhanVien.Text = name.FriendlyName();
            }
            this.dgrLichSuNhapThuocChiTiet.Rows.Clear();
            foreach (NhapThuocChiTietInfo info3 in this.hdn.HoaDonChiTiet)
            {
                int num = this.dgrLichSuNhapThuocChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrLichSuNhapThuocChiTiet.Rows[num];
                MedicineController controller4 = new MedicineController();
                MedicineInfo medicineByMaThuocTraoDoi = controller4.GetMedicineByMaThuocTraoDoi(info3.MaThuocTraoDoi);
                row.Cells[this.colThanhToanMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value = info3.MaThuocTraoDoi;
                row.Cells[this.colThanhToanTenThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colThanhToanDVT.Index].Value = controller4.GetDVT(info3.MaThuocTraoDoi).TenDV;
                row.Cells[this.colThanhToanSoLuong.Index].Value = info3.SoLuong;
                row.Cells[this.colThanhToanDonGiaNhap.Index].Value = info3.DonGiaNhap;
                row.Cells[this.colThanhToanThanhTienNhap.Index].Value = info3.SoLuong * info3.DonGiaNhap;
                row.Cells[this.colThanhToanThanhTienBan.Index].Value = info3.SoLuong * info3.DonGiaBan;
                row.Cells[this.colThanhToanDonGiaBan.Index].Value = info3.DonGiaBan;
                if (info3.HanDung != DateTime.MinValue)
                {
                }
                row.Cells[this.colThanhToanGhiChu.Index].Value = info3.GhiChu;
            }
        }

        private void LoadThongTinChuaThanhToan()
        {
            IDataReader dr = new HoaDonThanhToanController().GetThongTinChuaTT(this._MaHoaDonNhap);
            this.dgrNotHoaDonNhap.Rows.Clear();
            this.dgrThanhToanHoaDon.Rows.Clear();
            this.cboThaoTac.SelectedIndex = 0;
            while (dr.Read())
            {
                int rid = this.dgrNotHoaDonNhap.Rows.Add();
                DataGridViewRow gvr = this.dgrNotHoaDonNhap.Rows[rid];
                gvr.Cells[this.colNotDonGiaBan.Index].Value = ConvertHelper.getInt(dr["DonGiaBan"]);
                gvr.Cells[this.colNotDonGiaNhap.Index].Value = ConvertHelper.getInt(dr["DonGiaNhap"]);
                gvr.Cells[this.colNotDVT.Index].Value = ConvertHelper.getString(dr["DVT"]);
                gvr.Cells[this.colNotMaThuoc.Index].Value = ConvertHelper.getInt(dr["MaThuoc"]);
                gvr.Cells[this.colNotMaThuocTraoDoi.Index].Value = ConvertHelper.getInt(dr["MaThuocTraoDoi"]);
                gvr.Cells[this.colNotSoLuong.Index].Value = ConvertHelper.getInt(dr["SoLuongChuaThanhToan"]);
                gvr.Cells[this.colNotTenThuoc.Index].Value = ConvertHelper.getString(dr["TenThuoc"]);
                gvr.Cells[this.colNotThanhTienBan.Index].Value = ConvertHelper.getInt(dr["ThanhTienBan"]);
                gvr.Cells[this.colNotThanhTienNhap.Index].Value = ConvertHelper.getInt(dr["ThanhTienNhap"]);
                int ttrid = this.dgrThanhToanHoaDon.Rows.Add();
                DataGridViewRow ttr = this.dgrThanhToanHoaDon.Rows[rid];
                ttr.Cells[this.colThanhToanDonGiaBan.Index].Value = ConvertHelper.getInt(dr["DonGiaBan"]);
                ttr.Cells[this.colThanhToanDonGiaNhap.Index].Value = ConvertHelper.getInt(dr["DonGiaNhap"]);
                ttr.Cells[this.colThanhToanDVT.Index].Value = ConvertHelper.getString(dr["DVT"]);
                ttr.Cells[this.colThanhToanGhiChu.Index].Value = ConvertHelper.getString(dr["GhiChu"]);
                ttr.Cells[this.colThanhToanMaThuoc.Index].Value = ConvertHelper.getInt(dr["MaThuoc"]);
                ttr.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value = ConvertHelper.getInt(dr["MaThuocTraoDoi"]);
                ttr.Cells[this.colThanhToanSoLuong.Index].Value = ConvertHelper.getInt(dr["SoLuongChuaThanhToan"]);
                ttr.Cells[this.colThanhToanTenThuoc.Index].Value = ConvertHelper.getString(dr["TenThuoc"]);
                ttr.Cells[this.colThanhToanThanhTienBan.Index].Value = ConvertHelper.getString(dr["ThanhTienBan"]);
                ttr.Cells[this.colThanhToanThanhTienNhap.Index].Value = ConvertHelper.getString(dr["ThanhTienNhap"]);
            }
            dr.Close();
            this.LoadACForThongTinThanhToan();
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
        }

        private void RefreshData()
        {
            this.LoadAllHoaDonThanhToanFromHDNhap(this._MaHoaDonNhap);
            this.dgrNotHoaDonNhap.Rows.Clear();
            this.dgrThanhToanHoaDon.Rows.Clear();
        }

        private void TaiHoaDonThanhToan(int mahoadon)
        {
            IDataReader dr = new HoaDonThanhToanController().GetHoaDonThanhToanChiTiet(mahoadon);
            this.cboThaoTac.SelectedIndex = 1;
            if (dr != null)
            {
                this.dgrThanhToanHoaDon.Rows.Clear();
                while (dr.Read())
                {
                    int rowindex = this.dgrThanhToanHoaDon.Rows.Add();
                    this.cboNgay.Value = ConvertHelper.getDateTime(dr["Ngay"]);
                    DataGridViewRow dgvr = this.dgrThanhToanHoaDon.Rows[rowindex];
                    dgvr.Cells[this.colThanhToanChietKhau.Index].Value = ConvertHelper.getInt(dr["ChietKhau"]);
                    dgvr.Cells[this.colThanhToanDonGiaNhap.Index].Value = ConvertHelper.getInt(dr["DonGiaNhap"]);
                    dgvr.Cells[this.colThanhToanDonGiaBan.Index].Value = ConvertHelper.getInt(dr["DonGiaBan"]);
                    dgvr.Cells[this.colThanhToanDVT.Index].Value = ConvertHelper.getString(dr["DVT"]);
                    dgvr.Cells[this.colThanhToanGhiChu.Index].Value = ConvertHelper.getString(dr["GhiChu"]);
                    dgvr.Cells[this.colThanhToanMaThuoc.Index].Value = ConvertHelper.getInt(dr["MaThuoc"]);
                    dgvr.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value = ConvertHelper.getInt(dr["MaThuocTraoDoi"]);
                    dgvr.Cells[this.colThanhToanSoLuong.Index].Value = ConvertHelper.getInt(dr["SoLuong"]);
                    dgvr.Cells[this.colThanhToanTenThuoc.Index].Value = ConvertHelper.getString(dr["TenThuoc"]);
                    dgvr.Cells[this.colThanhToanThanhTienBan.Index].Value = ConvertHelper.getInt(dr["ThanhTienBan"]);
                    dgvr.Cells[this.colThanhToanThanhTienNhap.Index].Value = ConvertHelper.getInt(dr["ThanhTienNhap"]);
                    dgvr.Cells[this.colThanhToanTienChietKhau.Index].Value = ConvertHelper.getInt(dr["TienChietKhau"]);
                }
                dr.Close();
            }
        }

        private void TaiHoaDonTraLai(int mahoadon)
        {
            IDataReader dr = new HoaDonThanhToanController().GetThongTinTraLaiChiTiet(mahoadon);
            this.cboThaoTac.SelectedIndex = 0;
            if (dr != null)
            {
                this.dgrThanhToanHoaDon.Rows.Clear();
                while (dr.Read())
                {
                    int rowindex = this.dgrThanhToanHoaDon.Rows.Add();
                    DataGridViewRow dgvr = this.dgrThanhToanHoaDon.Rows[rowindex];
                    dgvr.Cells[this.colThanhToanDonGiaNhap.Index].Value = ConvertHelper.getInt(dr["DonGiaNhap"]);
                    dgvr.Cells[this.colThanhToanDonGiaBan.Index].Value = ConvertHelper.getInt(dr["DonGiaBan"]);
                    dgvr.Cells[this.colThanhToanDVT.Index].Value = ConvertHelper.getString(dr["DVT"]);
                    dgvr.Cells[this.colThanhToanGhiChu.Index].Value = ConvertHelper.getString(dr["GhiChu"]);
                    dgvr.Cells[this.colThanhToanMaThuoc.Index].Value = ConvertHelper.getInt(dr["MaThuoc"]);
                    dgvr.Cells[this.colThanhToanMaThuocTraoDoi.Index].Value = ConvertHelper.getInt(dr["MaThuocTraoDoi"]);
                    dgvr.Cells[this.colThanhToanSoLuong.Index].Value = ConvertHelper.getInt(dr["SoLuong"]);
                    dgvr.Cells[this.colThanhToanTenThuoc.Index].Value = ConvertHelper.getString(dr["TenThuoc"]);
                    dgvr.Cells[this.colThanhToanThanhTienBan.Index].Value = ConvertHelper.getInt(dr["ThanhTienBan"]);
                    dgvr.Cells[this.colThanhToanThanhTienNhap.Index].Value = ConvertHelper.getInt(dr["ThanhTienNhap"]);
                }
                dr.Close();
            }
        }

        private void Thuc_enter(object sender, EventArgs e)
        {
            this.txtTotalThanhToanBan.Focus();
        }

        private void tinhTong()
        {
            int a = 0;
            int num2 = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrLichSuNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[this.colThanhToanThanhTienNhap.Index].Value = (row.Cells[this.colThanhToanThanhTienNhap.Index].Value == null) ? 0 : row.Cells[this.colThanhToanThanhTienNhap.Index].Value;
                    a += ((int) ConvertHelper.getDouble(row.Cells[this.colThanhToanDonGiaNhap.Index].Value)) * ConvertHelper.getInt(row.Cells[this.colThanhToanSoLuong.Index].Value);
                    num2 += ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                }
            }
            this.txtTotalThanhToanNhap.Text = ConvertHelper.formatNumber(a);
            this.txtTotalThanhToanBan.Text = ConvertHelper.formatNumber(num2);
        }

        private void updateDonGia(DataGridViewRow dr)
        {
            try
            {
                int num = ConvertHelper.getInt(dr.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                double num2 = Convert.ToDouble(ConvertHelper.getInt(dr.Cells[this.colThanhToanSoLuong.Index].Value));
                if ((num2 != 0.0) && (num2 != -1.0))
                {
                    dr.Cells[this.colThanhToanDonGiaNhap.Index].Value = ((double) num) / num2;
                }
                double num3 = ConvertHelper.getDouble(dr.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                dr.Cells[this.colThanhToanDonGiaNhap.Index].Value = (num3 < 0.0) ? 0.0 : num3;
                this.tinhTong();
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void updateThanhTien(DataGridViewRow dr)
        {
            try
            {
                dr.Cells[this.colThanhToanThanhTienNhap.Index].Value = ConvertHelper.getInt(dr.Cells[this.colThanhToanSoLuong.Index].Value) * ConvertHelper.getInt(dr.Cells[this.colThanhToanDonGiaNhap.Index].Value);
                int num = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dgrLichSuNhapThuocChiTiet.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        num += ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                    }
                }
                this.txtTotalThanhToanNhap.Text = num.ToString();
                this.txtTotalThanhToanBan.Text = this.txtTotalThanhToanNhap.Text;
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void updateThanhTienBan(DataGridViewRow dr)
        {
            int num = ConvertHelper.getInt(dr.Cells[this.colThanhToanDonGiaBan.Index].Value);
            int num2 = ConvertHelper.getInt(dr.Cells[this.colThanhToanSoLuong.Index].Value);
            dr.Cells[this.colThanhToanThanhTienBan.Index].Value = num * num2;
        }

        private void UpdateTongTien()
        {
            int tongtiennhap = 0;
            int tongtienban = 0;
            int tongchietkhau = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrThanhToanHoaDon.Rows)
            {
                if (!row.IsNewRow)
                {
                    int tiennhap = ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienNhap.Index].Value);
                    if (tiennhap != -1)
                    {
                        tongtiennhap += tiennhap;
                    }
                    int tienban = ConvertHelper.getInt(row.Cells[this.colThanhToanThanhTienBan.Index].Value);
                    if (tienban != -1)
                    {
                        tongtienban += tienban;
                    }
                    int chietkhau = ConvertHelper.getInt(row.Cells[this.colThanhToanTienChietKhau.Index].Value);
                    if (chietkhau != -1)
                    {
                        tongchietkhau += chietkhau;
                    }
                }
            }
            this.txtTotalThanhToanBan.Text = tongtienban.ToString("#,#");
            this.txtTotalThanhToanCK.Text = tongchietkhau.ToString("#,#");
            this.txtTotalThanhToanNhap.Text = tongtiennhap.ToString("#,#");
        }

        private HoaDonNhapThuocInfo HoaDonNhapThuoc
        {
            get
            {
                this.getHoaDonNhapThuoc();
                return this.hdn;
            }
            set
            {
                this.hdn = value;
                this.loadInfo(this.hdn);
            }
        }
    }
}
