using Nammedia.Medboss;
using Nammedia.Medboss.lib;
using Nammedia.Medboss.Style;
using Nammedia.Medboss.Utils;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class KiemKeUI : MedUIBase
    {
        private Nammedia.Medboss.lib.KiemKeInfo _kiemKe = new Nammedia.Medboss.lib.KiemKeInfo();
        private ComboBox cboLoaiKiemKe;
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colDonGiaBan;
        private DataGridViewTextBoxColumn colDonGiaNhap;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colHanDung;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colThuoc;
        private DataGridViewTextBoxColumn colTinhTrang;
        private IContainer components = null;
        public DataGridView dgrKiemKeChiTiet;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtNhanVien;

        public KiemKeUI()
        {
            this.InitializeComponent();
            this.dgrKiemKeChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrKiemKeChiTiet.DefaultCellStyle);
            this.cboNgay.Value = DateTime.Now;
            this.bindFormating();
            DataGridViewCopyHandler.addDataGridViewClient(this.dgrKiemKeChiTiet);
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colDonGiaNhap);
            factory.Bind(FormatType.Int, this.colDonGiaBan);
            factory.Bind(FormatType.Trim, this.colThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Int, this.colMaThuoc);
        }

        public void clear()
        {
            this.dgrKiemKeChiTiet.Rows.Clear();
            this._kiemKe.KiemKeChiTiet.Clear();
        }

        private void dgrKiemKeChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public Nammedia.Medboss.lib.KiemKeInfo getKiemKeInfo()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            Nammedia.Medboss.lib.KiemKeInfo info = this._kiemKe;
            info.Ngay = this.cboNgay.Value;
            info.LoaiKiemKe = (LoaiKiemKeInfo) this.cboLoaiKiemKe.SelectedItem;
            info.MaQuay = new QuayController().getMaQuay(this.cboQuay.Text);
            if (info.MaQuay == -1)
            {
                arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", this.cboQuay.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.cboQuay;
                ufargs.Type = UnfoundType.Quay;
                throw new UnknownValueException(ufargs);
            }
            if (info.MaQuay == -1)
            {
                throw new InvalidException("Kh\x00f4ng thấy quầy");
            }
            info.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text);
            if ((info.MaNhanVien == -1) && (this.txtNhanVien.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenNhanVien, "Nh\x00e2n vi\x00ean", this.txtNhanVien.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtNhanVien;
                ufargs.Type = UnfoundType.NV;
                throw new UnknownValueException(ufargs);
            }
            info.KiemKeChiTiet.Clear();
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrKiemKeChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                KiemKeChiTietInfo info2 = new KiemKeChiTietInfo();
                string tenThuoc = ConvertHelper.getString(row.Cells[this.colThuoc.Index].Value);
                if (tenThuoc.Trim() == "")
                {
                    throw new InvalidException("T\x00ean thuốc kh\x00f4ng được rỗng");
                }
                string dVT = ConvertHelper.getString(row.Cells[this.colDVT.Index].Value);
                if (dVT.Trim() == "")
                {
                    throw new InvalidException("Đơn vị t\x00ednh kh\x00f4ng được rỗng");
                }
                info2.MaThuocTraoDoi = new MedicineController().getMaThuocTraoDoi(tenThuoc, dVT);
                if (info2.MaThuocTraoDoi == -1)
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
                    args2.control = this.dgrKiemKeChiTiet;
                    throw new UnknownValueException(args2);
                }
                info2.SoLuong = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                info2.TinhTrang = ConvertHelper.getString(row.Cells[this.colTinhTrang.Index].Value);
                info2.GhiChu = ConvertHelper.getString(row.Cells[this.colGhiChu.Index].Value);
                string formatedTime = ConvertHelper.getString(row.Cells[this.colHanDung.Index].Value);
                if (formatedTime != "")
                {
                    info2.HanDung = ConvertHelper.getTimeFormat(formatedTime);
                }
                else
                {
                    info2.HanDung = DateTime.MinValue;
                }
                info2.DonGiaBan = ConvertHelper.getInt(row.Cells[this.colDonGiaBan.Index].Value);
                info2.DonGiaNhap = ConvertHelper.getInt(row.Cells[this.colDonGiaNhap.Index].Value);
                info2.STT = stt++;
                info.KiemKeChiTiet.Add(info2);
            }
            if (info.KiemKeChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            return info;
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtNhanVien = new TextBox();
            this.dgrKiemKeChiTiet = new DataGridView();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.cboNgay = new DateTimePicker();
            this.label4 = new Label();
            this.cboLoaiKiemKe = new ComboBox();
            this.cboQuay = new ComboBox();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGiaNhap = new DataGridViewTextBoxColumn();
            this.colDonGiaBan = new DataGridViewTextBoxColumn();
            this.colHanDung = new DataGridViewTextBoxColumn();
            this.colTinhTrang = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrKiemKeChiTiet).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x97, 0x11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nh\x00e2n &vi\x00ean";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x13f, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x20, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Quầy";
            this.txtNhanVien.Location = new Point(0xd5, 14);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            this.dgrKiemKeChiTiet.AllowUserToOrderColumns = true;
            this.dgrKiemKeChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrKiemKeChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrKiemKeChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colSTT, this.colMaThuoc, this.colThuoc, this.colDVT, this.colSoLuong, this.colDonGiaNhap, this.colDonGiaBan, this.colHanDung, this.colTinhTrang, this.colGhiChu });
            this.dgrKiemKeChiTiet.Dock = DockStyle.Fill;
            this.dgrKiemKeChiTiet.Location = new Point(3, 0x4a);
            this.dgrKiemKeChiTiet.Name = "dgrKiemKeChiTiet";
            this.dgrKiemKeChiTiet.Size = new Size(0x292, 0x12e);
            this.dgrKiemKeChiTiet.TabIndex = 6;
            this.dgrKiemKeChiTiet.CellEndEdit += new DataGridViewCellEventHandler(this.KiemKeChiTiet_EndEdit);
            this.dgrKiemKeChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrKiemKeChiTiet_DataError);
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.dgrKiemKeChiTiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 18.79433f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 81.20567f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x298, 0x17b);
            this.tableLayoutPanel1.TabIndex = 7;
            this.panel1.Controls.Add(this.cboNgay);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cboLoaiKiemKe);
            this.panel1.Controls.Add(this.cboQuay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtNhanVien);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x292, 0x41);
            this.panel1.TabIndex = 7;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x29, 14);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(0x68, 20);
            this.cboNgay.TabIndex = 8;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1d0, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x43, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Loại kiểm k\x00ea";
            this.cboLoaiKiemKe.FormattingEnabled = true;
            this.cboLoaiKiemKe.Location = new Point(0x216, 13);
            this.cboLoaiKiemKe.Name = "cboLoaiKiemKe";
            this.cboLoaiKiemKe.Size = new Size(0x79, 0x15);
            this.cboLoaiKiemKe.TabIndex = 6;
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x166, 14);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 5;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colMaThuoc.HeaderText = "M\x00e3 thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x4d;
            this.colThuoc.DataPropertyName = "TenThuoc";
            this.colThuoc.HeaderText = "T\x00ean thuốc, th\x00e0nh phần, h\x00e0m lượng";
            this.colThuoc.Name = "colThuoc";
            this.colThuoc.Width = 0x86;
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "Đơn vị t\x00ednh";
            this.colDVT.Name = "colDVT";
            this.colDVT.Width = 0x4f;
            this.colSoLuong.DataPropertyName = "SoLuong";
            style.Alignment = DataGridViewContentAlignment.TopRight;
            style.Format = "N0";
            this.colSoLuong.DefaultCellStyle = style;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 0x45;
            this.colDonGiaNhap.DataPropertyName = "DonGiaNhap";
            style2.Alignment = DataGridViewContentAlignment.TopRight;
            style2.Format = "N0";
            this.colDonGiaNhap.DefaultCellStyle = style2;
            this.colDonGiaNhap.HeaderText = "Đơn gi\x00e1 nhập";
            this.colDonGiaNhap.Name = "colDonGiaNhap";
            this.colDonGiaNhap.Width = 0x58;
            this.colDonGiaBan.DataPropertyName = "DonGiaBan";
            style3.Alignment = DataGridViewContentAlignment.TopRight;
            style3.Format = "N0";
            this.colDonGiaBan.DefaultCellStyle = style3;
            this.colDonGiaBan.HeaderText = "Đơn gi\x00e1 b\x00e1n";
            this.colDonGiaBan.Name = "colDonGiaBan";
            this.colDonGiaBan.Width = 0x43;
            this.colHanDung.DataPropertyName = "HanDung";
            this.colHanDung.HeaderText = "Hạn d\x00f9ng";
            this.colHanDung.Name = "colHanDung";
            this.colHanDung.Width = 0x49;
            this.colTinhTrang.HeaderText = "T\x00ecnh trạng";
            this.colTinhTrang.Name = "colTinhTrang";
            this.colTinhTrang.Width = 0x4a;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 0x40;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "KiemKeUI";
            base.Size = new Size(0x298, 0x17b);
            base.Load += new EventHandler(this.KiemKeUI_Load);
            ((ISupportInitialize) this.dgrKiemKeChiTiet).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void KiemKeChiTiet_EndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MedicineInfo.DonGiaNhapBan dgnb;
            double dongianhap = 0.0;
            DataGridViewRow row = this.dgrKiemKeChiTiet.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colMaThuoc.Index)
            {
                MedicineController controller = new MedicineController();
                int maThuoc = ConvertHelper.getInt(row.Cells[this.colMaThuoc.Index].Value);
                MedicineInfo medicineByMaThuoc = controller.GetMedicineByMaThuoc(maThuoc);
                if ((medicineByMaThuoc.TenThuoc != "") && (medicineByMaThuoc.TenThuoc != null))
                {
                    row.Cells[this.colThuoc.Index].Value = medicineByMaThuoc.TenThuoc;
                    row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                    dgnb = controller.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                    if (dgnb.DonGiaNhap != 0.0)
                    {
                        row.Cells[this.colDonGiaNhap.Index].Value = ConvertHelper.formatNumber(dgnb.DonGiaNhap);
                    }
                    if (dgnb.DonGiaBan != 0.0)
                    {
                        row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
                    }
                }
            }
            if ((e.ColumnIndex == this.colThuoc.Index) || (e.ColumnIndex == this.colDVT.Index))
            {
                MedicineController controller3 = new MedicineController();
                string tenThuoc = ConvertHelper.getString(row.Cells[this.colThuoc.Index].Value);
                ArrayList medicine = controller3.GetMedicine(tenThuoc);
                if (medicine.Count > 0)
                {
                    MedicineInfo info2 = (MedicineInfo) medicine[0];
                    if (e.ColumnIndex == this.colThuoc.Index)
                    {
                        row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(info2);
                    }
                    string dVT = ConvertHelper.getString(row.Cells[this.colDVT.Index].Value);
                    row.Cells[this.colMaThuoc.Index].Value = info2.MaThuoc;
                    HoaDonNhapThuocInfo info3 = new HoaDonNhapThuocController().getLatestImport(tenThuoc, dVT);
                    if (info3.HoaDonChiTiet.Count > 0)
                    {
                        row.Cells[this.colHanDung.Index].Value = ((NhapThuocChiTietInfo) info3.HoaDonChiTiet[0]).HanDung.ToString("MM/yy");
                        dongianhap = ((NhapThuocChiTietInfo) info3.HoaDonChiTiet[0]).DonGiaNhap;
                        row.Cells[this.colDonGiaNhap.Index].Value = ConvertHelper.formatNumber(dongianhap);
                    }
                    dgnb = controller3.getDonGiaNhapBan(info2.TenThuoc, dVT, new QuayController().getMaQuay(this.cboQuay.Text));
                    row.Cells[this.colDonGiaNhap.Index].Value = ConvertHelper.formatNumber(dgnb.DonGiaNhap);
                    row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
                }
                else
                {
                    row.Cells[this.colMaThuoc.Index].Value = -1;
                }
            }
        }

        private void KiemKeUI_Load(object sender, EventArgs e)
        {
            this.loadAC();
            this.loadKiemKeInfo(this._kiemKe);
            new DataGridViewDnDEnabler(this.dgrKiemKeChiTiet);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            base._acFactory.EnableAutocomplete(this.cboLoaiKiemKe, ref this._loaiKiemKe);
            this.cboLoaiKiemKe.ValueMember = "TenLoaiKiemKe";
            base._acFactory.EnableAutocomplete(this.colThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
        }

        public void loadKiemKeInfo(Nammedia.Medboss.lib.KiemKeInfo kiemKeInfo)
        {
            this._kiemKe = kiemKeInfo;
            if ((this._kiemKe.Ngay < this.cboNgay.MinDate) || (this._kiemKe.Ngay > this.cboNgay.MaxDate))
            {
                this.cboNgay.Value = DateTime.Now;
            }
            else
            {
                this.cboNgay.Value = this._kiemKe.Ngay;
            }
            NhanVienController controller = new NhanVienController();
            this.txtNhanVien.Text = controller.GetById(this._kiemKe.MaNhanVien).Ten;
            QuayController controller2 = new QuayController();
            this.cboQuay.Text = controller2.getQuay(this._kiemKe.MaQuay).TenQuay;
            this.cboLoaiKiemKe.SelectedValue = this._kiemKe.LoaiKiemKe.TenLoaiKiemKe;
            this.dgrKiemKeChiTiet.Rows.Clear();
            foreach (KiemKeChiTietInfo info in this._kiemKe.KiemKeChiTiet)
            {
                int num = this.dgrKiemKeChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrKiemKeChiTiet.Rows[num];
                MedicineInfo medicineByMaThuocTraoDoi = new MedicineController().GetMedicineByMaThuocTraoDoi(info.MaThuocTraoDoi);
                row.Cells[this.colMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colDVT.Index].Value = ((ThuocTraoDoi) medicineByMaThuocTraoDoi.ThuocTraoDois[0]).DVT.TenDV;
                row.Cells[this.colGhiChu.Index].Value = info.GhiChu;
                if (info.HanDung != DateTime.MinValue)
                {
                    row.Cells[this.colHanDung.Index].Value = info.HanDung.ToString("MM/yy");
                }
                row.Cells[this.colThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colTinhTrang.Index].Value = info.TinhTrang;
                row.Cells[this.colSoLuong.Index].Value = info.SoLuong.ToString();
                row.Cells[this.colDonGiaBan.Index].Value = info.DonGiaBan;
                row.Cells[this.colDonGiaNhap.Index].Value = info.DonGiaNhap;
            }
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
            base._acFactory.RefreshAutoCompleteSource(this.cboLoaiKiemKe, ref this._loaiKiemKe);
            this.cboLoaiKiemKe.ValueMember = "TenLoaiKiemKe";
            base._acFactory.RefreshAutoCompleteSource(this.colThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
        }

        public DataGridViewTextBoxColumn ColumnDonGiaBan
        {
            get
            {
                return this.colDonGiaBan;
            }
        }

        public DataGridViewTextBoxColumn ColumnDonGiaNhap
        {
            get
            {
                return this.colDonGiaNhap;
            }
        }

        public DataGridViewTextBoxColumn ColumnDVT
        {
            get
            {
                return this.colDVT;
            }
        }

        public DataGridViewTextBoxColumn ColumnGhiChu
        {
            get
            {
                return this.colGhiChu;
            }
        }

        public DataGridViewTextBoxColumn ColumnHanDung
        {
            get
            {
                return this.colHanDung;
            }
        }

        public DataGridViewTextBoxColumn ColumnMaThuoc
        {
            get
            {
                return this.colMaThuoc;
            }
        }

        public DataGridViewTextBoxColumn ColumnSoLuong
        {
            get
            {
                return this.colSoLuong;
            }
        }

        public DataGridViewTextBoxColumn ColumnSTT
        {
            get
            {
                return this.colSTT;
            }
        }

        public DataGridViewTextBoxColumn ColumnThuoc
        {
            get
            {
                return this.colThuoc;
            }
        }

        public DataGridViewTextBoxColumn ColumnTinhTrang
        {
            get
            {
                return this.colTinhTrang;
            }
        }

        private Nammedia.Medboss.lib.KiemKeInfo KiemKeInfo
        {
            get
            {
                return this.getKiemKeInfo();
            }
            set
            {
                this._kiemKe = value;
                this.loadKiemKeInfo(this._kiemKe);
            }
        }
    }
}
