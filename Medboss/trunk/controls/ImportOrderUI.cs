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
    using System.Windows.Forms;

    public class ImportOrderUI : MedUIBase
    {
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private CheckBox chkThanhToanAll;
        private DataGridViewCheckBoxColumn colDaThanhToan;
        private DataGridViewTextBoxColumn colDonGia;
        private DataGridViewTextBoxColumn colDonGiaBan;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colHanSuDung;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colSoLo;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhTienBan;
        private DataGridViewTextBoxColumn colThanhTienNhap;
        private IContainer components;
        private DataGridView dgrNhapThuocChiTiet;
        private HoaDonNhapThuocInfo hdn;
        private bool IsLoading;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
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
        private TextBox txtChietKhau;
        private TextBox txtKhachHang;
        private TextBox txtMaHoaDonNCC;
        private TextBox txtNhanVien;
        private TextBox txtThucThu;
        private TextBox txtTienNo;
        private TextBox txtTotal;

        public ImportOrderUI()
        {
            this.components = null;
            this.hdn = new HoaDonNhapThuocInfo();
            this.InitializeComponent();
            this.hdn.Ngay = DateTime.Now;
            this.dgrNhapThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrNhapThuocChiTiet.DefaultCellStyle);
        }

        public ImportOrderUI(int MaHoaDon)
        {
            this.components = null;
            this.hdn = new HoaDonNhapThuocInfo();
            this.InitializeComponent();
            this.hdn = new HoaDonNhapThuocController().GetById(MaHoaDon);
            if (this.hdn.MaHoaDon != -1)
            {
                this.loadInfo(this.hdn);
            }
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Double, this.colThanhTienNhap);
            factory.Bind(FormatType.Int, this.colThanhTienBan);
            factory.Bind(FormatType.UpperCase, this.colSoLo);
            factory.Bind(FormatType.Double, this.txtTotal);
            factory.Bind(FormatType.Double, this.txtThucThu);
            factory.Bind(FormatType.Int, this.txtTienNo);
            factory.Bind(FormatType.Int, this.txtChietKhau);
            factory.Bind(FormatType.Int, this.colDonGiaBan);
            factory.Bind(FormatType.Double, this.colDonGia);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Trim, this.colSoLo);
            factory.Bind(FormatType.Int, this.colMaThuoc);
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
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                row.Cells[this.colDaThanhToan.Index].Value = this.chkThanhToanAll.Checked;
            }
        }

        public void clear()
        {
            this.dgrNhapThuocChiTiet.Rows.Clear();
            this.txtKhachHang.Text = "";
            this.txtMaHoaDonNCC.Text = "";
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineInfo.DonGiaNhapBan dgnb;
                MedicineController controller3 = new MedicineController();
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
                            dgnb = controller3.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                            row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
                        }
                    }
                }
                if ((e.ColumnIndex == this.colTenThuoc.Index) || (e.ColumnIndex == this.colDVT.Index))
                {
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
                    row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaBan;
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
            if (e.ColumnIndex == this.colSoLuong.Index)
            {
                this.updateDonGia(row);
                this.updateThanhTienBan(row);
            }
            else if (e.ColumnIndex == this.colThanhTienNhap.Index)
            {
                this.updateDonGia(row);
            }
            else if (e.ColumnIndex == this.colDonGiaBan.Index)
            {
                this.updateThanhTienBan(row);
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

        public HoaDonNhapThuocInfo getHoaDonNhapThuoc()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this.hdn.Ngay = this.cboNgay.Value;
            this.hdn.TienNo = ConvertHelper.getInt(this.txtTienNo.Text);
            this.hdn.MaQuay = new QuayController().getMaQuay(this.cboQuay.Text);
            this.hdn.MaHoaDonNCC = this.txtMaHoaDonNCC.Text.Trim();
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
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                NhapThuocChiTietInfo info = new NhapThuocChiTietInfo();
                info.STT = stt;
                stt++;
                info.DonGiaNhap = ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value);
                info.DonGiaBan = ConvertHelper.getInt(row.Cells[this.colDonGiaBan.Index].Value);
                info.GhiChu = ConvertHelper.getString(row.Cells[this.colGhiChu.Index].Value);
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
                    args2.control = this.dgrNhapThuocChiTiet;
                    throw new UnknownValueException(args2);
                }
                info.SoLuong = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                info.SoLo = ConvertHelper.getString(row.Cells[this.colSoLo.Index].Value);
                if (ConvertHelper.getString(row.Cells[this.colHanSuDung.Index].Value) != "")
                {
                    info.HanDung = ConvertHelper.getTimeFormat(ConvertHelper.getString(row.Cells[this.colHanSuDung.Index].Value));
                }
                else
                {
                    info.HanDung = DateTime.MinValue;
                }
                info.DaThanhToan = ConvertHelper.getBool(row.Cells[this.colDaThanhToan.Index].Value);
                this.hdn.HoaDonChiTiet.Add(info);
            }
            this.hdn.ThucThu = (int) Math.Round(ConvertHelper.getDouble(this.txtThucThu.Text), 0);
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
            if ((this.hdn != null) && (this.hdn.MaHoaDon > 0))
            {
                this.loadInfo(this.hdn);
            }
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
            this.label5 = new Label();
            this.txtTotal = new TextBox();
            this.label2 = new Label();
            this.lblTitle = new Label();
            this.txtTienNo = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtKhachHang = new TextBox();
            this.txtChietKhau = new TextBox();
            this.lblNhaCungCap = new Label();
            this.dgrNhapThuocChiTiet = new DataGridView();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGia = new DataGridViewTextBoxColumn();
            this.colThanhTienNhap = new DataGridViewTextBoxColumn();
            this.colDonGiaBan = new DataGridViewTextBoxColumn();
            this.colThanhTienBan = new DataGridViewTextBoxColumn();
            this.colHanSuDung = new DataGridViewTextBoxColumn();
            this.colSoLo = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            this.colDaThanhToan = new DataGridViewCheckBoxColumn();
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
            this.txtMaHoaDonNCC = new TextBox();
            this.label10 = new Label();
            this.cboNgay = new DateTimePicker();
            this.chkThanhToanAll = new CheckBox();
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
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x229, 70);
            this.label5.Name = "label5";
            this.label5.Size = new Size(30, 13);
            this.label5.TabIndex = 0x1a;
            this.label5.Text = "VND";
            this.label5.Visible = false;
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
            this.lblTitle.Location = new Point(0xae, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0xed, 0x1d);
            this.lblTitle.TabIndex = 0x17;
            this.lblTitle.Text = "Th\x00f4ng tin nhập thuốc";
            this.txtTienNo.Location = new Point(0x1c1, 0x41);
            this.txtTienNo.Name = "txtTienNo";
            this.txtTienNo.Size = new Size(100, 20);
            this.txtTienNo.TabIndex = 11;
            this.txtTienNo.Text = "0";
            this.txtTienNo.TextAlign = HorizontalAlignment.Right;
            this.txtTienNo.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x183, 70);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2b, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tiền &nợ";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xde, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3a, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chiết &khấu";
            this.label3.Visible = false;
            this.label3.Click += new EventHandler(this.label3_Click);
            this.txtKhachHang.Location = new Point(0x54, 0x41);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new Size(100, 20);
            this.txtKhachHang.TabIndex = 7;
            this.txtChietKhau.Location = new Point(0x11e, 0x41);
            this.txtChietKhau.Name = "txtChietKhau";
            this.txtChietKhau.Size = new Size(100, 20);
            this.txtChietKhau.TabIndex = 9;
            this.txtChietKhau.TextAlign = HorizontalAlignment.Right;
            this.txtChietKhau.Visible = false;
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new Point(3, 0x44);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new Size(0x4b, 13);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "Nh\x00e0 &cung cấp";
            this.dgrNhapThuocChiTiet.AllowUserToOrderColumns = true;
            this.dgrNhapThuocChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrNhapThuocChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrNhapThuocChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colMaThuocTraoDoi, this.colSTT, this.colMaThuoc, this.colTenThuoc, this.colDVT, this.colSoLuong, this.colDonGia, this.colThanhTienNhap, this.colDonGiaBan, this.colThanhTienBan, this.colHanSuDung, this.colSoLo, this.colGhiChu, this.colDaThanhToan });
            this.dgrNhapThuocChiTiet.Dock = DockStyle.Fill;
            this.dgrNhapThuocChiTiet.Location = new Point(3, 0x67);
            this.dgrNhapThuocChiTiet.Name = "dgrNhapThuocChiTiet";
            this.dgrNhapThuocChiTiet.Size = new Size(0x2fd, 0x164);
            this.dgrNhapThuocChiTiet.TabIndex = 12;
            this.dgrNhapThuocChiTiet.UserDeletedRow += new DataGridViewRowEventHandler(this.dgrNhapThuocChiTiet_UserDeletedRow);
            this.dgrNhapThuocChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrNhapThuocChiTiet_DataError);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colMaThuocTraoDoi.Visible = false;
            this.colMaThuocTraoDoi.Width = 0x61;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "-1";
            this.colMaThuoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.colMaThuoc.HeaderText = "M\x00e3 Thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x4b;
            this.colTenThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.colTenThuoc.HeaderText = "T\x00ean thuốc, th\x00e0nh phần, h\x00e0m lượng";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colTenThuoc.Width = 0x73;
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Resizable = DataGridViewTriState.True;
            this.colDVT.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDVT.Width = 0x23;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLuong.Width = 50;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = null;
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDonGia.HeaderText = "Đơn gi\x00e1 nhập";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDonGia.Width = 0x45;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N3";
            dataGridViewCellStyle5.NullValue = null;
            this.colThanhTienNhap.DefaultCellStyle = dataGridViewCellStyle5;
            this.colThanhTienNhap.HeaderText = "Th\x00e0nh tiền nhập";
            this.colThanhTienNhap.Name = "colThanhTienNhap";
            this.colThanhTienNhap.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhTienNhap.Width = 60;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colDonGiaBan.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDonGiaBan.HeaderText = "Đơn gi\x00e1 b\x00e1n";
            this.colDonGiaBan.Name = "colDonGiaBan";
            this.colDonGiaBan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDonGiaBan.Width = 0x30;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.Format = "N0";
            this.colThanhTienBan.DefaultCellStyle = dataGridViewCellStyle7;
            this.colThanhTienBan.HeaderText = "Th\x00e0nh tiền b\x00e1n";
            this.colThanhTienBan.Name = "colThanhTienBan";
            this.colThanhTienBan.ReadOnly = true;
            this.colThanhTienBan.Width = 0x4f;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colHanSuDung.DefaultCellStyle = dataGridViewCellStyle8;
            this.colHanSuDung.HeaderText = "Hạn sử dụng";
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colHanSuDung.Width = 0x43;
            this.colSoLo.HeaderText = "Số l\x00f4";
            this.colSoLo.Name = "colSoLo";
            this.colSoLo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLo.Width = 0x1a;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colGhiChu.Width = 0x2d;
            this.colDaThanhToan.FalseValue = "false";
            this.colDaThanhToan.HeaderText = "Đ\x00e3 Thanh To\x00e1n";
            this.colDaThanhToan.Name = "colDaThanhToan";
            this.colDaThanhToan.TrueValue = "True";
            this.colDaThanhToan.Width = 80;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2d, 0x29);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xe0, 0x2a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nh\x00e2n &vi\x00ean";
            this.label7.Visible = false;
            this.txtNhanVien.Location = new Point(0x11e, 0x27);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            this.txtNhanVien.Visible = false;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x183, 0x2b);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "&Quầy";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x1c1, 0x27);
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
            this.tableLayoutPanel1.Size = new Size(0x303, 0x1f6);
            this.tableLayoutPanel1.TabIndex = 0x20;
            this.panel1.Anchor = AnchorStyles.Right;
            this.panel1.Controls.Add(this.txtThucThu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new Point(0x194, 0x1d1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x16c, 0x22);
            this.panel1.TabIndex = 0x10;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb9, 10);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x31, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Thực ch&i";
            this.label9.Enter += new EventHandler(this.Thuc_enter);
            this.panel2.Controls.Add(this.txtMaHoaDonNCC);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cboNgay);
            this.panel2.Controls.Add(this.chkThanhToanAll);
            this.panel2.Controls.Add(this.cboQuay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTienNo);
            this.panel2.Controls.Add(this.lblNhaCungCap);
            this.panel2.Controls.Add(this.txtNhanVien);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtKhachHang);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtChietKhau);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2fd, 0x5e);
            this.panel2.TabIndex = 0x11;
            this.txtMaHoaDonNCC.Location = new Point(630, 0x26);
            this.txtMaHoaDonNCC.Name = "txtMaHoaDonNCC";
            this.txtMaHoaDonNCC.Size = new Size(100, 20);
            this.txtMaHoaDonNCC.TabIndex = 30;
            this.txtMaHoaDonNCC.Leave += new EventHandler(this.txtMaHoaDonNCC_Leave);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x22c, 0x29);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x42, 13);
            this.label10.TabIndex = 0x1d;
            this.label10.Text = "M\x00e3 HĐ NCC";
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x54, 0x27);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0x1c;
            this.chkThanhToanAll.AutoSize = true;
            this.chkThanhToanAll.Location = new Point(0x251, 0x42);
            this.chkThanhToanAll.Name = "chkThanhToanAll";
            this.chkThanhToanAll.Size = new Size(0x98, 0x11);
            this.chkThanhToanAll.TabIndex = 0x1b;
            this.chkThanhToanAll.Text = "Đ\x00e3 thanh to\x00e1n cả h\x00f3a đơn";
            this.chkThanhToanAll.UseVisualStyleBackColor = true;
            this.chkThanhToanAll.CheckedChanged += new EventHandler(this.chkThanhToanAll_CheckedChanged);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ImportOrderUI";
            base.Size = new Size(0x303, 0x1f6);
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
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
        }

        public void loadInfo(HoaDonNhapThuocInfo hoaDonNhap)
        {
            this.IsLoading = true;
            this.hdn = hoaDonNhap;
            this.cboNgay.Value = this.hdn.Ngay;
            CSInfo byId = new CSController().GetById(this.hdn.MaKhachHang);
            this.txtKhachHang.Text = byId.Ten;
            this.txtTienNo.Text = this.hdn.TienNo.ToString();
            this.txtTotal.Text = this.calTotal(this.hdn).ToString();
            this.txtThucThu.Text = this.hdn.ThucThu.ToString();
            this.txtChietKhau.Text = this.hdn.ChietKhau.ToString();
            this.txtMaHoaDonNCC.Text = this.hdn.MaHoaDonNCC;
            QuayController controller2 = new QuayController();
            this.cboQuay.Text = controller2.getQuay(this.hdn.MaQuay).TenQuay;
            NhanVienInfo info2 = new NhanVienController().GetById(this.hdn.MaNhanVien);
            if (info2.MaNhanVien != -1)
            {
                IFriendlyName name = info2;
                this.txtNhanVien.Text = name.FriendlyName();
            }
            this.dgrNhapThuocChiTiet.Rows.Clear();
            foreach (NhapThuocChiTietInfo info3 in this.hdn.HoaDonChiTiet)
            {
                int num = this.dgrNhapThuocChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrNhapThuocChiTiet.Rows[num];
                MedicineController controller4 = new MedicineController();
                MedicineInfo medicineByMaThuocTraoDoi = controller4.GetMedicineByMaThuocTraoDoi(info3.MaThuocTraoDoi);
                row.Cells[this.colMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colMaThuocTraoDoi.Index].Value = info3.MaThuocTraoDoi;
                row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colDVT.Index].Value = controller4.GetDVT(info3.MaThuocTraoDoi).TenDV;
                row.Cells[this.colSoLuong.Index].Value = info3.SoLuong;
                row.Cells[this.colDonGia.Index].Value = info3.DonGiaNhap;
                row.Cells[this.colThanhTienNhap.Index].Value = info3.SoLuong * info3.DonGiaNhap;
                row.Cells[this.colThanhTienBan.Index].Value = info3.SoLuong * info3.DonGiaBan;
                row.Cells[this.colDonGiaBan.Index].Value = info3.DonGiaBan;
                if (info3.HanDung != DateTime.MinValue)
                {
                    row.Cells[this.colHanSuDung.Index].Value = info3.HanDung.ToString("MM/yy");
                }
                row.Cells[this.colSoLo.Index].Value = info3.SoLo;
                row.Cells[this.colGhiChu.Index].Value = info3.GhiChu;
                row.Cells[this.colDaThanhToan.Index].Value = info3.DaThanhToan;
            }
            this.IsLoading = false;
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtKhachHang, ref this._csSource);
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
        }

        private void Thuc_enter(object sender, EventArgs e)
        {
            this.txtThucThu.Focus();
        }

        private void tinhTong()
        {
            double a = 0.0;
            double num2 = 0.0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[this.colThanhTienNhap.Index].Value = (row.Cells[this.colThanhTienNhap.Index].Value == null) ? 0 : row.Cells[this.colThanhTienNhap.Index].Value;
                    a += ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value) * ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    num2 += ConvertHelper.getDouble(row.Cells[this.colThanhTienNhap.Index].Value);
                }
            }
            this.txtTotal.Text = ConvertHelper.formatNumber(a);
            if (!this.IsLoading)
            {
                this.txtThucThu.Text = ConvertHelper.formatNumber(num2);
            }
        }

        private void txtMaHoaDonNCC_Leave(object sender, EventArgs e)
        {
            this.txtMaHoaDonNCC.Text = this.txtMaHoaDonNCC.Text.Trim().ToUpper();
        }

        private void updateDonGia(DataGridViewRow dr)
        {
            try
            {
                double num = ConvertHelper.getDouble(dr.Cells[this.colThanhTienNhap.Index].Value);
                double num2 = Convert.ToDouble(ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value));
                double num3 = 0.0;
                if ((num2 != 0.0) && (num2 != -1.0))
                {
                    num3 = num / num2;
                }
                dr.Cells[this.colDonGia.Index].Value = ConvertHelper.formatNumber((num3 < 0.0) ? 0.0 : num3);
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
                dr.Cells[this.colThanhTienNhap.Index].Value = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value) * ConvertHelper.getInt(dr.Cells[this.colDonGia.Index].Value);
                double num = 0.0;
                foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        num += ConvertHelper.getInt(row.Cells[this.colThanhTienNhap.Index].Value);
                    }
                }
                this.txtTotal.Text = ConvertHelper.formatNumber(num);
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

        private void updateThanhTienBan(DataGridViewRow dr)
        {
            int num = ConvertHelper.getInt(dr.Cells[this.colDonGiaBan.Index].Value);
            int num2 = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value);
            dr.Cells[this.colThanhTienBan.Index].Value = num * num2;
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
