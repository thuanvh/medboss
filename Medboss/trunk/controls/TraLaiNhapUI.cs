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
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class TraLaiNhapUI : MedUIBase
    {
        private HoaDonTraLaiInfo _orTraLai;
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colDonGia;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colHanSuDung;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colNgayNhap;
        private DataGridViewTextBoxColumn colSoLo;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhTien;
        private IContainer components;
        private DataGridView dgrNhapThuocChiTiet;
        private bool IsLoading;
        private bool IsTinhThanhTien;
        private Label label1;
        private Label label2;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblNhaCungCap;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtKhachHang;
        private TextBox txtNhanVien;
        private TextBox txtThucThu;
        private TextBox txtTotal;

        public TraLaiNhapUI()
        {
            this.IsTinhThanhTien = false;
            this.IsLoading = false;
            this.components = null;
            this._orTraLai = new HoaDonTraLaiInfo();
            this.InitializeComponent();
            this.dgrNhapThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrNhapThuocChiTiet.DefaultCellStyle);
            this._orTraLai.Ngay = DateTime.Now;
            this.loadInfo(this._orTraLai);
            this.bindFormating();
        }

        public TraLaiNhapUI(int MaHoaDon)
        {
            this.IsTinhThanhTien = false;
            this.IsLoading = false;
            this.components = null;
            this._orTraLai = new HoaDonTraLaiInfo();
            this.InitializeComponent();
            this._orTraLai = new HoaDonTraLaiNhapController().GetById(MaHoaDon);
            if (this._orTraLai.MaHoaDon != -1)
            {
                this.loadInfo(this._orTraLai);
            }
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colThanhTien);
            factory.Bind(FormatType.UpperCase, this.colSoLo);
            factory.Bind(FormatType.Int, this.txtTotal);
            factory.Bind(FormatType.Int, this.txtThucThu);
            factory.Bind(FormatType.Int, this.colDonGia);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Trim, this.colSoLo);
            factory.Bind(FormatType.Int, this.colMaThuoc);
        }

        private int calTotal(HoaDonTraLaiInfo hdn)
        {
            int num = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    int num2 = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    int num3 = ConvertHelper.getInt(row.Cells[this.colDonGia.Index].Value);
                    num += num2 * num3;
                }
            }
            return num;
        }

        public void clear()
        {
            this.dgrNhapThuocChiTiet.Rows.Clear();
            this.txtKhachHang.Text = "";
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineInfo.DonGiaNhapBan dgnb;
                DataGridViewRow row = this.dgrNhapThuocChiTiet.Rows[e.RowIndex];
                if (e.ColumnIndex == this.colMaThuoc.Index)
                {
                    MedicineController controller = new MedicineController();
                    int maThuoc = ConvertHelper.getInt(row.Cells[this.colMaThuoc.Index].Value);
                    MedicineInfo medicineByMaThuoc = controller.GetMedicineByMaThuoc(maThuoc);
                    if ((medicineByMaThuoc.TenThuoc != "") && (medicineByMaThuoc.TenThuoc != null))
                    {
                        row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuoc.TenThuoc;
                        if (medicineByMaThuoc.ThuocTraoDois.Count > 0)
                        {
                            row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                            dgnb = controller.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                            if (this._orTraLai.ThuHayChi)
                            {
                                row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaNhap;
                            }
                            else
                            {
                                row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaBan;
                            }
                        }
                    }
                }
                if ((e.ColumnIndex == this.colTenThuoc.Index) || (e.ColumnIndex == this.colDVT.Index))
                {
                    MedicineController controller3 = new MedicineController();
                    if (e.ColumnIndex == this.colTenThuoc.Index)
                    {
                        ArrayList medicine = controller3.GetMedicine((string) row.Cells[this.colTenThuoc.Index].Value);
                        if (medicine.Count > 0)
                        {
                            MedicineInfo info2 = (MedicineInfo) medicine[0];
                            row.Cells[this.colMaThuoc.Index].Value = info2.MaThuoc;
                            row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(info2);
                        }
                        else
                        {
                            row.Cells[this.colMaThuoc.Index].Value = -1;
                        }
                    }
                    dgnb = controller3.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                    if (this._orTraLai.ThuHayChi)
                    {
                        row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaNhap;
                    }
                    else
                    {
                        row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaBan;
                    }
                }
                if (e.ColumnIndex == this.colNgayNhap.Index)
                {
                    try
                    {
                        DateTimeFormatInfo dtf = new DateTimeFormatInfo();
                        CultureInfo ci = new CultureInfo("vi-VN", false);
                        ConvertHelper.getTimeFormat(row.Cells[e.ColumnIndex].Value.ToString());
                    }
                    catch (Exception exc)
                    {
                        LogManager.LogException(exc);
                    }
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void dgrBanThuocChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrNhapThuocChiTiet.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colThanhTien.Index)
            {
                this.IsTinhThanhTien = false;
            }
            if (e.ColumnIndex == this.colDonGia.Index)
            {
                this.IsTinhThanhTien = true;
            }
            if (e.ColumnIndex == this.colSoLuong.Index)
            {
                if (!this._orTraLai.ThuHayChi)
                {
                    this.updateDonGia(row);
                }
                else
                {
                    this.updateThanhTien(row);
                }
            }
            if ((e.ColumnIndex == this.colThanhTien.Index) && !this.IsTinhThanhTien)
            {
                this.updateDonGia(row);
            }
            if ((e.ColumnIndex == this.colDonGia.Index) && this.IsTinhThanhTien)
            {
                this.updateThanhTien(row);
            }
        }

        private void dgrNhapThuocChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgrNhapThuocChiTiet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.tinhTong();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public HoaDonTraLaiInfo getHoaDonTraLai()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this._orTraLai.Ngay = this.cboNgay.Value;
            this._orTraLai.MaQuay = new QuayController().getMaQuay(this.cboQuay.Text);
            if (this._orTraLai.MaQuay == -1)
            {
                arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", this.cboQuay.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.cboQuay;
                ufargs.Type = UnfoundType.Quay;
                throw new UnknownValueException(ufargs);
            }
            this._orTraLai.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text);
            if ((this._orTraLai.MaNhanVien == -1) && (this.txtNhanVien.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenNhanVien, "Nh\x00e2n vi\x00ean", this.txtNhanVien.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtNhanVien;
                ufargs.Type = UnfoundType.NV;
                throw new UnknownValueException(ufargs);
            }
            this._orTraLai.MaKhachHang = new CSController().GetIdByName(this.txtKhachHang.Text);
            if ((this._orTraLai.MaKhachHang == -1) && (this.txtKhachHang.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenKhachHang, "Nh\x00e0 cung cấp", this.txtKhachHang.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtKhachHang;
                ufargs.Type = UnfoundType.DoiTac;
                throw new UnknownValueException(ufargs);
            }
            this._orTraLai.TraLaiChiTiet.Clear();
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                TraLaiChiTietInfo info = new TraLaiChiTietInfo();
                info.STT = stt;
                stt++;
                info.DonGia = (int) ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value);
                info.GhiChu = ConvertHelper.getString(row.Cells[this.colGhiChu.Index].Value);
                info.MaThuocTraoDoi = ConvertHelper.getInt(row.Cells[this.colMaThuocTraoDoi.Index].Value);
                string tenThuoc = ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value);
                if (tenThuoc.Trim() == "")
                {
                    throw new InvalidException("T\x00ean thuốc kh\x00f4ng được rỗng");
                }
                string dVT = ConvertHelper.getString(row.Cells[this.colDVT.Index].Value);
                if (dVT.Trim() == "")
                {
                    throw new InvalidException("Đơn vị t\x00ednh kh\x00f4ng được rỗng");
                }
                if (info.MaThuocTraoDoi == -1)
                {
                    info.MaThuocTraoDoi = new MedicineController().getMaThuocTraoDoi(tenThuoc, dVT);
                }
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
                    args2.control = this.dgrNhapThuocChiTiet;
                    throw new UnknownValueException(args2);
                }
                info.SoLuong = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                info.SoLo = ConvertHelper.getString(row.Cells[this.colSoLo.Index].Value);
                string formatedTime = ConvertHelper.getString(row.Cells[this.colHanSuDung.Index].Value);
                if (formatedTime != "")
                {
                    info.HanDung = ConvertHelper.getTimeFormat(formatedTime);
                }
                else
                {
                    info.HanDung = DateTime.MinValue;
                }
                string ngaynhap = ConvertHelper.getString(row.Cells[this.colNgayNhap.Index].Value);
                if (ngaynhap != "")
                {
                    info.NgayNhap = ConvertHelper.getTimeFormat(ngaynhap);
                }
                else
                {
                    info.NgayNhap = DateTime.MinValue;
                }
                this._orTraLai.TraLaiChiTiet.Add(info);
            }
            if (this._orTraLai.TraLaiChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            this._orTraLai.TienTraLai = ConvertHelper.getInt(this.txtThucThu.Text);
            return this._orTraLai;
        }

        public bool getThuHayChi()
        {
            return this._orTraLai.ThuHayChi;
        }

        private void ImportOrderUI_Load(object sender, EventArgs e)
        {
            this.loadInfo(this._orTraLai);
            this.cboQuay.DataSource = base._quaySource;
            this.cboQuay.DisplayMember = "TenQuay";
            this.loadAC();
            this.dgrNhapThuocChiTiet.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dgrNhapThuocChiTiet.CellValueChanged += new DataGridViewCellEventHandler(this.dgrBanThuocChiTiet_CellValueChanged);
            new DataGridViewDnDEnabler(this.dgrNhapThuocChiTiet);
            DataGridViewCopyHandler.addDataGridViewClient(this.dgrNhapThuocChiTiet);
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
            this.tabItem1 = new TabItem(this.components);
            this.label6 = new Label();
            this.txtTotal = new TextBox();
            this.label2 = new Label();
            this.lblTitle = new Label();
            this.txtKhachHang = new TextBox();
            this.lblNhaCungCap = new Label();
            this.dgrNhapThuocChiTiet = new DataGridView();
            this.label1 = new Label();
            this.label7 = new Label();
            this.txtNhanVien = new TextBox();
            this.label8 = new Label();
            this.cboQuay = new ComboBox();
            this.txtThucThu = new TextBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.label9 = new Label();
            this.panel2 = new Panel();
            this.cboNgay = new DateTimePicker();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colNgayNhap = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGia = new DataGridViewTextBoxColumn();
            this.colThanhTien = new DataGridViewTextBoxColumn();
            this.colHanSuDung = new DataGridViewTextBoxColumn();
            this.colSoLo = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrNhapThuocChiTiet).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x93, 10);
            this.label6.Name = "label6";
            this.label6.Size = new Size(30, 13);
            this.label6.TabIndex = 0x1b;
            this.label6.Text = "VND";
            this.txtTotal.Location = new Point(0x29, 7);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new Size(100, 20);
            this.txtTotal.TabIndex = 14;
            this.txtTotal.TextAlign = HorizontalAlignment.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x20, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tổng";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(0xd3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0xc6, 0x1d);
            this.lblTitle.TabIndex = 0x17;
            this.lblTitle.Text = "Nhận thuốc trả lại";
            this.txtKhachHang.Location = new Point(100, 0x41);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new Size(100, 20);
            this.txtKhachHang.TabIndex = 7;
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new Point(0x13, 0x44);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new Size(0x41, 13);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "&Kh\x00e1ch h\x00e0ng";
            this.dgrNhapThuocChiTiet.AllowUserToOrderColumns = true;
            this.dgrNhapThuocChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrNhapThuocChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrNhapThuocChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colMaThuocTraoDoi, this.colSTT, this.colNgayNhap, this.colMaThuoc, this.colTenThuoc, this.colDVT, this.colSoLuong, this.colDonGia, this.colThanhTien, this.colHanSuDung, this.colSoLo, this.colGhiChu });
            this.dgrNhapThuocChiTiet.Dock = DockStyle.Fill;
            this.dgrNhapThuocChiTiet.Location = new Point(3, 0x67);
            this.dgrNhapThuocChiTiet.Name = "dgrNhapThuocChiTiet";
            this.dgrNhapThuocChiTiet.Size = new Size(0x27a, 0xff);
            this.dgrNhapThuocChiTiet.TabIndex = 12;
            this.dgrNhapThuocChiTiet.UserDeletedRow += new DataGridViewRowEventHandler(this.dgrNhapThuocChiTiet_UserDeletedRow);
            this.dgrNhapThuocChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrNhapThuocChiTiet_DataError);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xd5, 0x2b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nh\x00e2n &vi\x00ean";
            this.txtNhanVien.Location = new Point(0x11e, 40);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x188, 40);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "&Quầy";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x1b7, 40);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 5;
            this.txtThucThu.Location = new Point(0xf1, 7);
            this.txtThucThu.Name = "txtThucThu";
            this.txtThucThu.Size = new Size(100, 20);
            this.txtThucThu.TabIndex = 0x10;
            this.txtThucThu.Text = "0";
            this.txtThucThu.TextAlign = HorizontalAlignment.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrNhapThuocChiTiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            this.tableLayoutPanel1.Size = new Size(640, 0x191);
            this.tableLayoutPanel1.TabIndex = 0x20;
            this.panel1.Controls.Add(this.txtThucThu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x16c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x27a, 0x22);
            this.panel1.TabIndex = 0x10;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb9, 10);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x2f, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Thực t&rả";
            this.label9.Enter += new EventHandler(this.Thuc_enter);
            this.panel2.Controls.Add(this.cboNgay);
            this.panel2.Controls.Add(this.cboQuay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblNhaCungCap);
            this.panel2.Controls.Add(this.txtNhanVien);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtKhachHang);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x27a, 0x5e);
            this.panel2.TabIndex = 0x11;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(100, 40);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0x18;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colMaThuocTraoDoi.Visible = false;
            this.colMaThuocTraoDoi.Width = 0x61;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.colSTT.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.NullValue = null;
            this.colNgayNhap.DefaultCellStyle = dataGridViewCellStyle3;
            this.colNgayNhap.HeaderText = "Ng\x00e0y Nhập";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Width = 0x56;
            dataGridViewCellStyle4.NullValue = "-1";
            this.colMaThuoc.DefaultCellStyle = dataGridViewCellStyle4;
            this.colMaThuoc.HeaderText = "M\x00e3 thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x4d;
            this.colTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colTenThuoc.Width = 0x3e;
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Resizable = DataGridViewTriState.True;
            this.colDVT.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDVT.Width = 0x23;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLuong.Width = 0x37;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDonGia.HeaderText = "Đơn gi\x00e1";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDonGia.Width = 50;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            this.colThanhTien.DefaultCellStyle = dataGridViewCellStyle7;
            this.colThanhTien.HeaderText = "Th\x00e0nh tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhTien.Width = 0x40;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colHanSuDung.DefaultCellStyle = dataGridViewCellStyle8;
            this.colHanSuDung.HeaderText = "Hạn sử dụng";
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colHanSuDung.Width = 0x4a;
            this.colSoLo.HeaderText = "Số l\x00f4";
            this.colSoLo.Name = "colSoLo";
            this.colSoLo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLo.Width = 0x25;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colGhiChu.Width = 50;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "TraLaiNhapUI";
            base.Size = new Size(640, 0x191);
            base.Load += new EventHandler(this.ImportOrderUI_Load);
            ((ISupportInitialize) this.dgrNhapThuocChiTiet).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtKhachHang, ref this._csSource);
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
        }

        public void loadInfo(HoaDonTraLaiInfo hoaDonTraLai)
        {
            this.IsLoading = true;
            this.colNgayNhap.Visible = hoaDonTraLai.ThuHayChi;
            this._orTraLai = hoaDonTraLai;
            this.cboNgay.Value = this._orTraLai.Ngay;
            CSInfo byId = new CSController().GetById(this._orTraLai.MaKhachHang);
            this.txtKhachHang.Text = byId.Ten;
            this.txtThucThu.Text = this._orTraLai.TienTraLai.ToString();
            NhanVienInfo info2 = new NhanVienController().GetById(this._orTraLai.MaNhanVien);
            if (info2.MaNhanVien != -1)
            {
                IFriendlyName name = info2;
                this.txtNhanVien.Text = name.FriendlyName();
            }
            this.dgrNhapThuocChiTiet.Rows.Clear();
            foreach (TraLaiChiTietInfo info3 in this._orTraLai.TraLaiChiTiet)
            {
                int num = this.dgrNhapThuocChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrNhapThuocChiTiet.Rows[num];
                MedicineController controller3 = new MedicineController();
                MedicineInfo medicineByMaThuocTraoDoi = controller3.GetMedicineByMaThuocTraoDoi(info3.MaThuocTraoDoi);
                row.Cells[this.colMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colDVT.Index].Value = controller3.GetDVT(info3.MaThuocTraoDoi).TenDV;
                row.Cells[this.colSoLuong.Index].Value = info3.SoLuong;
                row.Cells[this.colDonGia.Index].Value = info3.DonGia;
                row.Cells[this.colThanhTien.Index].Value = info3.SoLuong * info3.DonGia;
                row.Cells[this.colSoLo.Index].Value = info3.SoLo;
                if (info3.HanDung != DateTime.MinValue)
                {
                    row.Cells[this.colHanSuDung.Index].Value = info3.HanDung.ToString("MM/yy");
                }
                row.Cells[this.colGhiChu.Index].Value = info3.GhiChu;
                row.Cells[this.colNgayNhap.Index].Value = info3.NgayNhap.ToString("dd/MM/yyyy");
            }
            this.txtTotal.Text = this.calTotal(this._orTraLai).ToString();
            this.IsLoading = false;
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtKhachHang, ref this._csSource);
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
        }

        public void setThuHayChi(bool thuHayChi)
        {
            this._orTraLai.ThuHayChi = thuHayChi;
            this.lblTitle.Text = "Trả lại thuốc cho nh\x00e0 cung cấp.";
        }

        private void Thuc_enter(object sender, EventArgs e)
        {
            this.txtThucThu.Focus();
        }

        private void tinhTong()
        {
            int a = 0;
            int num2 = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[this.colThanhTien.Index].Value = (row.Cells[this.colThanhTien.Index].Value == null) ? 0 : row.Cells[this.colThanhTien.Index].Value;
                    a += ((int) ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value)) * ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    num2 += ConvertHelper.getInt(row.Cells[this.colThanhTien.Index].Value);
                }
            }
            this.txtTotal.Text = ConvertHelper.formatNumber(a);
            if (!this.IsLoading)
            {
                this.txtThucThu.Text = ConvertHelper.formatNumber(num2);
            }
        }

        private void updateDonGia(DataGridViewRow dr)
        {
            try
            {
                int num = ConvertHelper.getInt(dr.Cells[this.colThanhTien.Index].Value);
                double num2 = Convert.ToDouble(ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value));
                if ((num2 != 0.0) && (num2 != -1.0))
                {
                    dr.Cells[this.colDonGia.Index].Value = ((double) num) / num2;
                }
                double num3 = ConvertHelper.getDouble(dr.Cells[this.colDonGia.Index].Value);
                dr.Cells[this.colDonGia.Index].Value = (num3 < 0.0) ? 0.0 : num3;
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
                dr.Cells[this.colThanhTien.Index].Value = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value) * ConvertHelper.getInt(dr.Cells[this.colDonGia.Index].Value);
                int a = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        a += ConvertHelper.getInt(row.Cells[this.colThanhTien.Index].Value);
                    }
                }
                this.txtTotal.Text = ConvertHelper.formatNumber(a);
                if (!this.IsLoading)
                {
                    this.txtThucThu.Text = this.txtTotal.Text;
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private HoaDonTraLaiInfo HoaDonTraLai
        {
            get
            {
                this.getHoaDonTraLai();
                return this._orTraLai;
            }
            set
            {
                this._orTraLai = value;
                this.loadInfo(this._orTraLai);
            }
        }
    }
}
