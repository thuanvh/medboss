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

    public class ThanhToanList : MedUIBase
    {
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colChietKhau;
        private DataGridViewTextBoxColumn colDonGia;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colNgayNhap;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhTienChietKhau;
        private DataGridViewTextBoxColumn colThanhTienThanhToan;
        private DataGridViewTextBoxColumn colThanhTienThucThanhToan;
        private IContainer components;
        private DataGridView dgrNhapThuocChiTiet;
        private HoaDonThanhToanInfo hdn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label8;
        private Label label9;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtKhachHang;
        private TextBox txtTongChietKhau;
        private TextBox txtTongPhaiTra;
        private TextBox txtTongThanhToan;

        public ThanhToanList()
        {
            this.components = null;
            this.hdn = new HoaDonThanhToanInfo();
            this.InitializeComponent();
            this.hdn.NgayThanhToan = DateTime.Now;
            this.dgrNhapThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrNhapThuocChiTiet.DefaultCellStyle);
        }

        public ThanhToanList(int MaHoaDon)
        {
            this.components = null;
            this.hdn = new HoaDonThanhToanInfo();
            this.InitializeComponent();
            this.hdn = new HoaDonThanhToanController().GetHoaDonThanhToan(MaHoaDon);
            if ((this.hdn != null) && (this.hdn.MaThanhToan != -1))
            {
                this.loadInfo(this.hdn);
            }
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.Int, this.colThanhTienThanhToan);
            factory.Bind(FormatType.Int, this.colThanhTienChietKhau);
            factory.Bind(FormatType.Int, this.txtTongThanhToan);
            factory.Bind(FormatType.Int, this.txtTongChietKhau);
            factory.Bind(FormatType.Int, this.txtTongPhaiTra);
            factory.Bind(FormatType.Int, this.colChietKhau);
            factory.Bind(FormatType.Int, this.colDonGia);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Int, this.colMaThuoc);
        }

        private int calTotal(HoaDonNhapThuocInfo hdn)
        {
            double num = 0.0;
            foreach (NhapThuocChiTietInfo info in hdn.HoaDonChiTiet)
            {
                num += info.SoLuong * info.DonGiaNhap;
            }
            return (int) Math.Round(num, 0);
        }

        public void clear()
        {
            this.dgrNhapThuocChiTiet.Rows.Clear();
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
                        row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                        dgnb = controller.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                        row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaNhap;
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
                    row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaNhap;
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void dgrBanThuocChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int tienck;
            DataGridViewRow row = this.dgrNhapThuocChiTiet.Rows[e.RowIndex];
            if ((e.ColumnIndex == this.colDonGia.Index) || (e.ColumnIndex == this.colSoLuong.Index))
            {
                this.tinhTong();
            }
            if (e.ColumnIndex == this.colChietKhau.Index)
            {
                int ck = ConvertHelper.getInt(row.Cells[this.colChietKhau.Index].Value);
                if (ck >= 0)
                {
                    tienck = (ConvertHelper.getInt(row.Cells[this.colThanhTienThanhToan.Index].Value) * ck) / 100;
                    row.Cells[this.colThanhTienChietKhau.Index].Value = tienck;
                }
            }
            if ((e.ColumnIndex == this.colThanhTienChietKhau.Index) || (e.ColumnIndex == this.colThanhTienThanhToan.Index))
            {
                int tientt = ConvertHelper.getInt(row.Cells[this.colThanhTienThanhToan.Index].Value);
                tienck = ConvertHelper.getInt(row.Cells[this.colThanhTienChietKhau.Index].Value);
                row.Cells[this.colThanhTienThucThanhToan.Index].Value = ConvertHelper.formatNumber((int) (tientt - tienck));
                this.tinhTong();
            }
            if (((e.ColumnIndex == this.colSoLuong.Index) || (e.ColumnIndex == this.colDonGia.Index)) || (e.ColumnIndex == this.colChietKhau.Index))
            {
                this.updateThanhTienThanhPhan(row);
            }
            if (((e.ColumnIndex == this.colThanhTienChietKhau.Index) || (e.ColumnIndex == this.colThanhTienThanhToan.Index)) || (e.ColumnIndex == this.colThanhTienThucThanhToan.Index))
            {
                this.tinhTong();
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

        public HoaDonThanhToanInfo getHoaDonThanhToan()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this.hdn.NgayThanhToan = this.cboNgay.Value;
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
            this.hdn.ThanhToanChiTiet.Clear();
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                ThanhToanChiTietInfo info = new ThanhToanChiTietInfo();
                info.STT = stt;
                stt++;
                info.DonGia = (int) ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value);
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
                if (ConvertHelper.getString(row.Cells[this.colNgayNhap.Index].Value) != "")
                {
                    info.NgayNhap = ConvertHelper.getTimeFormat(ConvertHelper.getString(row.Cells[this.colNgayNhap.Index].Value));
                }
                else
                {
                    info.NgayNhap = DateTime.MinValue;
                }
                info.ChietKhau = ConvertHelper.getInt(row.Cells[this.colChietKhau.Index].Value);
                info.TienChietKhau = ConvertHelper.getInt(row.Cells[this.colThanhTienChietKhau.Index].Value);
                this.hdn.ThanhToanChiTiet.Add(info);
            }
            if (this.hdn.ThanhToanChiTiet.Count == 0)
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
            if ((this.hdn != null) && (this.hdn.MaThanhToan > 0))
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
            this.tabItem1 = new TabItem(this.components);
            this.txtTongThanhToan = new TextBox();
            this.label2 = new Label();
            this.lblTitle = new Label();
            this.dgrNhapThuocChiTiet = new DataGridView();
            this.label1 = new Label();
            this.label8 = new Label();
            this.cboQuay = new ComboBox();
            this.txtTongChietKhau = new TextBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.txtTongPhaiTra = new TextBox();
            this.label3 = new Label();
            this.label9 = new Label();
            this.panel2 = new Panel();
            this.label4 = new Label();
            this.txtKhachHang = new TextBox();
            this.cboNgay = new DateTimePicker();
            this.colNgayNhap = new DataGridViewTextBoxColumn();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGia = new DataGridViewTextBoxColumn();
            this.colThanhTienThanhToan = new DataGridViewTextBoxColumn();
            this.colChietKhau = new DataGridViewTextBoxColumn();
            this.colThanhTienChietKhau = new DataGridViewTextBoxColumn();
            this.colThanhTienThucThanhToan = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrNhapThuocChiTiet).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.txtTongThanhToan.Location = new Point(0x73, 7);
            this.txtTongThanhToan.Name = "txtTongThanhToan";
            this.txtTongThanhToan.ReadOnly = true;
            this.txtTongThanhToan.Size = new Size(100, 20);
            this.txtTongThanhToan.TabIndex = 14;
            this.txtTongThanhToan.TextAlign = HorizontalAlignment.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x6a, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tổng tiền thanh to\x00e1n";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(0xec, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(230, 0x1d);
            this.lblTitle.TabIndex = 0x17;
            this.lblTitle.Text = "Th\x00f4ng tin thanh to\x00e1n";
            this.lblTitle.Click += new EventHandler(this.lblTitle_Click);
            this.dgrNhapThuocChiTiet.AllowUserToOrderColumns = true;
            this.dgrNhapThuocChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrNhapThuocChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrNhapThuocChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colNgayNhap, this.colMaThuocTraoDoi, this.colMaThuoc, this.colTenThuoc, this.colDVT, this.colSoLuong, this.colDonGia, this.colThanhTienThanhToan, this.colChietKhau, this.colThanhTienChietKhau, this.colThanhTienThucThanhToan, this.colGhiChu });
            this.dgrNhapThuocChiTiet.Dock = DockStyle.Fill;
            this.dgrNhapThuocChiTiet.Location = new Point(3, 0x53);
            this.dgrNhapThuocChiTiet.Name = "dgrNhapThuocChiTiet";
            this.dgrNhapThuocChiTiet.Size = new Size(0x2fd, 0x178);
            this.dgrNhapThuocChiTiet.TabIndex = 12;
            this.dgrNhapThuocChiTiet.UserDeletedRow += new DataGridViewRowEventHandler(this.dgrNhapThuocChiTiet_UserDeletedRow);
            this.dgrNhapThuocChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrNhapThuocChiTiet_DataError);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x29);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xab, 0x29);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "&Quầy";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0xd1, 0x24);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 5;
            this.txtTongChietKhau.Location = new Point(0x138, 7);
            this.txtTongChietKhau.Name = "txtTongChietKhau";
            this.txtTongChietKhau.ReadOnly = true;
            this.txtTongChietKhau.Size = new Size(100, 20);
            this.txtTongChietKhau.TabIndex = 0x10;
            this.txtTongChietKhau.Text = "0";
            this.txtTongChietKhau.TextAlign = HorizontalAlignment.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrNhapThuocChiTiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            this.tableLayoutPanel1.Size = new Size(0x303, 0x1f6);
            this.tableLayoutPanel1.TabIndex = 0x20;
            this.panel1.Controls.Add(this.txtTongPhaiTra);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtTongChietKhau);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTongThanhToan);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x1d1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2fd, 0x22);
            this.panel1.TabIndex = 0x10;
            this.txtTongPhaiTra.Location = new Point(0x202, 7);
            this.txtTongPhaiTra.Name = "txtTongPhaiTra";
            this.txtTongPhaiTra.ReadOnly = true;
            this.txtTongPhaiTra.Size = new Size(100, 20);
            this.txtTongPhaiTra.TabIndex = 0x12;
            this.txtTongPhaiTra.Text = "0";
            this.txtTongPhaiTra.TextAlign = HorizontalAlignment.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1a2, 10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(90, 13);
            this.label3.TabIndex = 0x11;
            this.label3.Text = "Tổng tiền phải trả";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xdd, 10);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x55, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tổng chiết khấu";
            this.label9.Enter += new EventHandler(this.Thuc_enter);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtKhachHang);
            this.panel2.Controls.Add(this.cboNgay);
            this.panel2.Controls.Add(this.cboQuay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2fd, 0x4a);
            this.panel2.TabIndex = 0x11;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x146, 0x29);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x21, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "H\x00e3ng";
            this.txtKhachHang.Location = new Point(0x17f, 0x26);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new Size(100, 20);
            this.txtKhachHang.TabIndex = 0x1d;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x36, 0x25);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0x1c;
            this.colNgayNhap.HeaderText = "Ng\x00e0y Nhập";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Width = 0x56;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMaThuocTraoDoi.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colMaThuocTraoDoi.Visible = false;
            this.colMaThuocTraoDoi.Width = 0x61;
            this.colMaThuoc.HeaderText = "M\x00e3 Thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x51;
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle2.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLuong.Width = 50;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDonGia.HeaderText = "Đơn gi\x00e1 nhập";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDonGia.Width = 0x45;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colThanhTienThanhToan.DefaultCellStyle = dataGridViewCellStyle4;
            this.colThanhTienThanhToan.HeaderText = "Th\x00e0nh tiền thanh to\x00e1n";
            this.colThanhTienThanhToan.Name = "colThanhTienThanhToan";
            this.colThanhTienThanhToan.Width = 0x6a;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N0";
            this.colChietKhau.DefaultCellStyle = dataGridViewCellStyle5;
            this.colChietKhau.HeaderText = "Chiết khấu";
            this.colChietKhau.Name = "colChietKhau";
            this.colChietKhau.Width = 0x4d;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colThanhTienChietKhau.DefaultCellStyle = dataGridViewCellStyle6;
            this.colThanhTienChietKhau.HeaderText = "Th\x00e0nh tiền chiết khấu";
            this.colThanhTienChietKhau.Name = "colThanhTienChietKhau";
            this.colThanhTienChietKhau.Width = 0x67;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = "0";
            this.colThanhTienThucThanhToan.DefaultCellStyle = dataGridViewCellStyle7;
            this.colThanhTienThucThanhToan.HeaderText = "Tiền thực thanh to\x00e1n";
            this.colThanhTienThucThanhToan.Name = "colThanhTienThucThanhToan";
            this.colThanhTienThucThanhToan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colThanhTienThucThanhToan.Width = 0x52;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colGhiChu.Width = 0x2d;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ThanhToanList";
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

        private void lblTitle_Click(object sender, EventArgs e)
        {
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtKhachHang, ref this._csSource);
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
        }

        public void loadInfo(HoaDonThanhToanInfo hoaDonNhap)
        {
            this.hdn = hoaDonNhap;
            this.cboNgay.Value = this.hdn.NgayThanhToan;
            CSInfo byId = new CSController().GetById(this.hdn.MaKhachHang);
            this.txtKhachHang.Text = byId.Ten;
            QuayController controller2 = new QuayController();
            this.cboQuay.Text = controller2.getQuay(this.hdn.MaQuay).TenQuay;
            NhanVienInfo info2 = new NhanVienController().GetById(this.hdn.MaNhanVien);
            if (info2.MaNhanVien != -1)
            {
                IFriendlyName name = info2;
            }
            this.dgrNhapThuocChiTiet.Rows.Clear();
            foreach (ThanhToanChiTietInfo info3 in this.hdn.ThanhToanChiTiet)
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
                row.Cells[this.colDonGia.Index].Value = info3.DonGia;
                row.Cells[this.colThanhTienThanhToan.Index].Value = info3.SoLuong * info3.DonGia;
                row.Cells[this.colChietKhau.Index].Value = info3.ChietKhau;
                if (info3.NgayNhap != DateTime.MinValue)
                {
                    row.Cells[this.colNgayNhap.Index].Value = info3.NgayNhap.ToString("dd/MM/yy");
                }
                row.Cells[this.colThanhTienChietKhau.Index].Value = info3.TienChietKhau;
            }
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtKhachHang, ref this._csSource);
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
        }

        private void Thuc_enter(object sender, EventArgs e)
        {
            this.txtTongChietKhau.Focus();
        }

        private void tinhTong()
        {
            int a = 0;
            int num2 = 0;
            int ck = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[this.colThanhTienThanhToan.Index].Value = (row.Cells[this.colThanhTienThanhToan.Index].Value == null) ? 0 : row.Cells[this.colThanhTienThanhToan.Index].Value;
                    a += ((int) ConvertHelper.getDouble(row.Cells[this.colDonGia.Index].Value)) * ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    num2 += ConvertHelper.getInt(row.Cells[this.colThanhTienThanhToan.Index].Value);
                    row.Cells[this.colThanhTienChietKhau.Index].Value = (row.Cells[this.colThanhTienChietKhau.Index].Value == null) ? 0 : row.Cells[this.colThanhTienChietKhau.Index].Value;
                    ck += ConvertHelper.getInt(row.Cells[this.colThanhTienChietKhau.Index].Value);
                }
            }
            this.txtTongThanhToan.Text = ConvertHelper.formatNumber(num2);
            this.txtTongChietKhau.Text = ConvertHelper.formatNumber(ck);
            this.txtTongPhaiTra.Text = ConvertHelper.formatNumber((int) (num2 - ck));
        }

        private void updateDonGia(DataGridViewRow dr)
        {
            try
            {
                int num = ConvertHelper.getInt(dr.Cells[this.colThanhTienThanhToan.Index].Value);
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
                dr.Cells[this.colThanhTienThanhToan.Index].Value = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value) * ConvertHelper.getInt(dr.Cells[this.colDonGia.Index].Value);
                int num = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dgrNhapThuocChiTiet.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        num += ConvertHelper.getInt(row.Cells[this.colThanhTienThanhToan.Index].Value);
                    }
                }
                this.txtTongThanhToan.Text = num.ToString();
                this.txtTongChietKhau.Text = this.txtTongThanhToan.Text;
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void updateThanhTienThanhPhan(DataGridViewRow dr)
        {
            int dongianhap = ConvertHelper.getInt(dr.Cells[this.colDonGia.Index].Value);
            int chietkhau = ConvertHelper.getInt(dr.Cells[this.colChietKhau.Index].Value);
            int soluong = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value);
            int thanhtienthanhtoan = dongianhap * soluong;
            int thanhtienchietkhau = (thanhtienthanhtoan * chietkhau) / 100;
            dr.Cells[this.colThanhTienThanhToan.Index].Value = thanhtienthanhtoan;
            dr.Cells[this.colThanhTienChietKhau.Index].Value = thanhtienchietkhau;
            dr.Cells[this.colThanhTienThucThanhToan.Index].Value = thanhtienthanhtoan - thanhtienchietkhau;
        }

        private HoaDonThanhToanInfo HoaDonNhapThuoc
        {
            get
            {
                this.getHoaDonThanhToan();
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
