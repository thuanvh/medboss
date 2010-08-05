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

    public class SellOrderUI : MedUIBase
    {
        private bool _txtChietKhauFire;
        private bool _txtpcChietKhauFire;
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colChietKhau;
        private DataGridViewTextBoxColumn colDonGia;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhTien;
        private IContainer components;
        private DataGridView dgrBanThuocChiTiet;
        private Hashtable eventTable;
        private HoaDonBanThuocInfo hdb;
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
        private Label lblKhacHang;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtChietKhau;
        private TextBox txtKhachHang;
        private TextBox txtNhanVien;
        private TextBox txtpcChietKhau;
        private TextBox txtThucThu;
        private TextBox txtTienNo;
        private TextBox txtTotal;

        public event ValidateHandler DataInvalid
        {
            add
            {
                this.eventTable["Invalid"] = (ValidateHandler) Delegate.Combine((ValidateHandler) this.eventTable["Invalid"], value);
            }
            remove
            {
                this.eventTable["Invalid"] = (ValidateHandler) Delegate.Remove((ValidateHandler) this.eventTable["Invalid"], value);
            }
        }

        public SellOrderUI()
        {
            this.IsLoading = false;
            this.hdb = new HoaDonBanThuocInfo();
            this.eventTable = new Hashtable();
            this._txtChietKhauFire = false;
            this._txtpcChietKhauFire = false;
            this.components = null;
            this.InitializeComponent();
            this.dgrBanThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrBanThuocChiTiet.DefaultCellStyle);
            this.hdb.Ngay = DateTime.Now;
        }

        public SellOrderUI(int MaHoaDon)
        {
            this.IsLoading = false;
            this.hdb = new HoaDonBanThuocInfo();
            this.eventTable = new Hashtable();
            this._txtChietKhauFire = false;
            this._txtpcChietKhauFire = false;
            this.components = null;
            this.InitializeComponent();
            this.dgrBanThuocChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrBanThuocChiTiet.DefaultCellStyle);
            this.hdb = new HoaDonBanThuocController().GetById(MaHoaDon);
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colThanhTien);
            factory.Bind(FormatType.Int, this.txtTotal);
            factory.Bind(FormatType.Int, this.txtThucThu);
            factory.Bind(FormatType.Int, this.txtTienNo);
            factory.Bind(FormatType.Int, this.txtChietKhau);
            factory.Bind(FormatType.Int, this.colDonGia);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Int, this.colChietKhau);
            factory.Bind(FormatType.Int, this.colMaThuoc);
        }

        private int calTotal(HoaDonBanThuocInfo hdb)
        {
            int num = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrBanThuocChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    int num2 = (int) row.Cells[this.colSoLuong.Index].Value;
                    int num3 = (int) row.Cells[this.colDonGia.Index].Value;
                    num += num2 * num3;
                }
            }
            return num;
        }

        public void clear()
        {
            this.dgrBanThuocChiTiet.Rows.Clear();
            this.txtKhachHang.Text = "";
            this.txtChietKhau.Text = "0";
            this.txtpcChietKhau.Text = "0";
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MedicineInfo.DonGiaNhapBan dgnb;
            MedicineController controller3 = new MedicineController();
            DataGridViewRow row = this.dgrBanThuocChiTiet.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colMaThuoc.Index)
            {
                MedicineController controller = new MedicineController();
                int maThuoc = ConvertHelper.getInt(row.Cells[this.colMaThuoc.Index].Value);
                MedicineInfo medicineByMaThuoc = controller.GetMedicineByMaThuoc(maThuoc);
                if ((medicineByMaThuoc.TenThuoc != "") && (medicineByMaThuoc.TenThuoc != null))
                {
                    row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuoc.TenThuoc;
                    row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                    dgnb = controller3.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboQuay.Text));
                    row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaBan;
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
                row.Cells[this.colDonGia.Index].Value = (int) dgnb.DonGiaBan;
            }
        }

        private void dgrBanThuocChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.colSoLuong.Index)
            {
                this.updateThanhTien(this.dgrBanThuocChiTiet.Rows[e.RowIndex]);
            }
            else if (e.ColumnIndex == this.colDonGia.Index)
            {
                this.updateThanhTien(this.dgrBanThuocChiTiet.Rows[e.RowIndex]);
            }
        }

        private void dgrBanThuocChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        public HoaDonBanThuocInfo getHoaDonBanThuoc()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this.hdb.ChietKhau = ConvertHelper.getInt(this.txtChietKhau.Text);
            this.hdb.Ngay = this.cboNgay.Value;
            this.hdb.TienNo = ConvertHelper.getInt(this.txtTienNo.Text);
            this.hdb.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text);
            if ((this.hdb.MaNhanVien == -1) && (this.txtNhanVien.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenNhanVien, "Nh\x00e2n vi\x00ean", this.txtNhanVien.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtNhanVien;
                ufargs.Type = UnfoundType.NV;
                throw new UnknownValueException(ufargs);
            }
            this.hdb.MaKhachHang = new CSController().GetIdByName(this.txtKhachHang.Text);
            if ((this.hdb.MaKhachHang == -1) && (this.txtKhachHang.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenKhachHang, "Kh\x00e1ch H\x00e0ng", this.txtKhachHang.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtKhachHang;
                ufargs.Type = UnfoundType.DoiTac;
                throw new UnknownValueException(ufargs);
            }
            this.hdb.MaQuay = new QuayController().getMaQuay(this.cboQuay.Text);
            if (this.hdb.MaQuay == -1)
            {
                throw new InvalidException("Kh\x00f4ng thấy quầy.");
            }
            this.hdb.HoaDonChiTiet.Clear();
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrBanThuocChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                BanThuocChiTietInfo info = new BanThuocChiTietInfo();
                info.STT = stt;
                stt++;
                info.DonGiaBan = ConvertHelper.getInt(row.Cells[this.colDonGia.Index].Value);
                info.GhiChu = ConvertHelper.getString(row.Cells[this.colGhiChu.Index].Value);
                info.MaThuocTraoDoi = -1;
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
                    args2.control = this.dgrBanThuocChiTiet;
                    throw new UnknownValueException(args2);
                }
                info.SoLuong = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                if (row.Cells[this.colChietKhau.Index].Value == null)
                {
                    info.ChietKhau = 0;
                }
                else
                {
                    info.ChietKhau = ConvertHelper.getInt(row.Cells[this.colChietKhau.Index].Value);
                }
                this.hdb.HoaDonChiTiet.Add(info);
            }
            if (this.hdb.HoaDonChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            this.hdb.ThucThu = ConvertHelper.getInt(this.txtThucThu.Text);
            return this.hdb;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            this.label1 = new Label();
            this.dgrBanThuocChiTiet = new DataGridView();
            this.tabItem1 = new TabItem(this.components);
            this.txtChietKhau = new TextBox();
            this.lblKhacHang = new Label();
            this.txtKhachHang = new TextBox();
            this.label3 = new Label();
            this.txtTienNo = new TextBox();
            this.label4 = new Label();
            this.lblTitle = new Label();
            this.label2 = new Label();
            this.txtTotal = new TextBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.txtNhanVien = new TextBox();
            this.label7 = new Label();
            this.cboQuay = new ComboBox();
            this.txtThucThu = new TextBox();
            this.label8 = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.label9 = new Label();
            this.panel2 = new Panel();
            this.txtpcChietKhau = new TextBox();
            this.label10 = new Label();
            this.cboNgay = new DateTimePicker();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGia = new DataGridViewTextBoxColumn();
            this.colThanhTien = new DataGridViewTextBoxColumn();
            this.colChietKhau = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrBanThuocChiTiet).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0, 0x2c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.dgrBanThuocChiTiet.AllowUserToOrderColumns = true;
            this.dgrBanThuocChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrBanThuocChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrBanThuocChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colMaThuocTraoDoi, this.colSTT, this.colMaThuoc, this.colTenThuoc, this.colDVT, this.colSoLuong, this.colDonGia, this.colThanhTien, this.colChietKhau, this.colGhiChu });
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Window;
            style.Font = new Font("Microsoft Sans Serif", 10f);
            style.ForeColor = SystemColors.ControlText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.False;
            this.dgrBanThuocChiTiet.DefaultCellStyle = style;
            this.dgrBanThuocChiTiet.Dock = DockStyle.Fill;
            this.dgrBanThuocChiTiet.Location = new Point(3, 0x7b);
            this.dgrBanThuocChiTiet.Name = "dgrBanThuocChiTiet";
            this.dgrBanThuocChiTiet.Size = new Size(0x2a3, 0x105);
            this.dgrBanThuocChiTiet.TabIndex = 12;
            this.dgrBanThuocChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrBanThuocChiTiet_DataError);
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.txtChietKhau.Location = new Point(0x12a, 0x49);
            this.txtChietKhau.Name = "txtChietKhau";
            this.txtChietKhau.Size = new Size(0x48, 20);
            this.txtChietKhau.TabIndex = 9;
            this.txtChietKhau.Text = "0";
            this.txtChietKhau.Visible = false;
            this.txtChietKhau.Leave += new EventHandler(this.txtChietKhau_Leave);
            this.lblKhacHang.AutoSize = true;
            this.lblKhacHang.Location = new Point(0, 0x4c);
            this.lblKhacHang.Name = "lblKhacHang";
            this.lblKhacHang.Size = new Size(0x41, 13);
            this.lblKhacHang.TabIndex = 6;
            this.lblKhacHang.Text = "&Kh\x00e1ch h\x00e0ng";
            this.txtKhachHang.Location = new Point(0x47, 0x49);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new Size(100, 20);
            this.txtKhachHang.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xb1, 0x4d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3a, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "&Chiết khấu";
            this.label3.Visible = false;
            this.txtTienNo.Location = new Point(0x1a7, 0x49);
            this.txtTienNo.Name = "txtTienNo";
            this.txtTienNo.RightToLeft = RightToLeft.Yes;
            this.txtTienNo.Size = new Size(100, 20);
            this.txtTienNo.TabIndex = 11;
            this.txtTienNo.Text = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x17b, 0x4c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2b, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tiền &nợ";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(0xa1, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0xe0, 0x1d);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Th\x00f4ng tin b\x00e1n thuốc";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x20, 13);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "Tổng";
            this.txtTotal.Location = new Point(0x29, 11);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.RightToLeft = RightToLeft.Yes;
            this.txtTotal.Size = new Size(100, 20);
            this.txtTotal.TabIndex = 0x10;
            this.txtTotal.TabStop = false;
            this.txtTotal.Text = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x214, 0x4c);
            this.label5.Name = "label5";
            this.label5.Size = new Size(30, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "VND";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x93, 14);
            this.label6.Name = "label6";
            this.label6.Size = new Size(30, 13);
            this.label6.TabIndex = 0x12;
            this.label6.Text = "VND";
            this.txtNhanVien.Location = new Point(0xf1, 0x2c);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(0x81, 20);
            this.txtNhanVien.TabIndex = 3;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xb1, 0x2f);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nh\x00e2n &vi\x00ean";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x1a7, 0x2c);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 5;
            this.txtThucThu.Location = new Point(240, 11);
            this.txtThucThu.Name = "txtThucThu";
            this.txtThucThu.Size = new Size(100, 20);
            this.txtThucThu.TabIndex = 15;
            this.txtThucThu.Text = "0";
            this.txtThucThu.TextAlign = HorizontalAlignment.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x17b, 0x2c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "&Quầy";
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrBanThuocChiTiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            this.tableLayoutPanel1.Size = new Size(0x2a9, 0x1b5);
            this.tableLayoutPanel1.TabIndex = 0x12;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel1.Controls.Add(this.txtThucThu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Location = new Point(330, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x15c, 0x2b);
            this.panel1.TabIndex = 2;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb8, 14);
            this.label9.Name = "label9";
            this.label9.Size = new Size(50, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Thực th&u";
            this.panel2.Controls.Add(this.txtpcChietKhau);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cboNgay);
            this.panel2.Controls.Add(this.cboQuay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.lblKhacHang);
            this.panel2.Controls.Add(this.txtNhanVien);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTienNo);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtKhachHang);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtChietKhau);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2a3, 0x72);
            this.panel2.TabIndex = 3;
            this.txtpcChietKhau.Location = new Point(0xf1, 0x49);
            this.txtpcChietKhau.Name = "txtpcChietKhau";
            this.txtpcChietKhau.Size = new Size(0x25, 20);
            this.txtpcChietKhau.TabIndex = 15;
            this.txtpcChietKhau.Text = "0";
            this.txtpcChietKhau.Visible = false;
            this.txtpcChietKhau.Leave += new EventHandler(this.txtpcChietKhau_Leave);
            this.txtpcChietKhau.TextChanged += new EventHandler(this.txtpcChietKhau_TextChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x113, 0x4c);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x15, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "%=";
            this.label10.Visible = false;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x47, 0x2c);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 13;
            this.colMaThuocTraoDoi.DataPropertyName = "MaThuocTraoDoi";
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMaThuocTraoDoi.DefaultCellStyle = style2;
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.Visible = false;
            this.colMaThuocTraoDoi.Width = 0x74;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colMaThuoc.HeaderText = "M\x00e3 thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x4d;
            this.colTenThuoc.DataPropertyName = "TenThuoc";
            this.colTenThuoc.HeaderText = "T\x00ean thuốc, th\x00e0nh phần, h\x00e0m lượng";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.Width = 0x86;
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Width = 0x36;
            this.colSoLuong.DataPropertyName = "SoLuong";
            style3.Alignment = DataGridViewContentAlignment.TopRight;
            style3.Format = "N0";
            style3.NullValue = "0";
            this.colSoLuong.DefaultCellStyle = style3;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 0x45;
            this.colDonGia.DataPropertyName = "DonGia";
            style4.Alignment = DataGridViewContentAlignment.TopRight;
            style4.Format = "N0";
            style4.NullValue = "0";
            this.colDonGia.DefaultCellStyle = style4;
            this.colDonGia.HeaderText = "Đơn gi\x00e1";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Width = 0x34;
            this.colThanhTien.DataPropertyName = "ThanhTien";
            style5.Alignment = DataGridViewContentAlignment.TopRight;
            style5.Format = "N0";
            style5.NullValue = "0";
            this.colThanhTien.DefaultCellStyle = style5;
            this.colThanhTien.HeaderText = "Th\x00e0nh tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 0x4d;
            this.colChietKhau.DataPropertyName = "ChietKhau";
            style6.Alignment = DataGridViewContentAlignment.TopRight;
            style6.Format = "N0";
            style6.NullValue = "0";
            this.colChietKhau.DefaultCellStyle = style6;
            this.colChietKhau.HeaderText = "Chiết khấu";
            this.colChietKhau.Name = "colChietKhau";
            this.colChietKhau.Width = 0x4d;
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 0x40;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "SellOrderUI";
            base.Size = new Size(0x2a9, 0x1b5);
            base.Load += new EventHandler(this.SellOrderUI_Load);
            ((ISupportInitialize) this.dgrBanThuocChiTiet).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
            base._acFactory.EnableAutocomplete(this.txtKhachHang, ref this._csSource);
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
        }

        public void loadInfo(HoaDonBanThuocInfo hoaDonBanThuoc)
        {
            this.IsLoading = true;
            this.hdb = hoaDonBanThuoc;
            this.cboNgay.Value = hoaDonBanThuoc.Ngay;
            IFriendlyName byId = new CSController().GetById(hoaDonBanThuoc.MaKhachHang);
            this.txtKhachHang.Text = byId.FriendlyName();
            this.txtTienNo.Text = hoaDonBanThuoc.TienNo.ToString();
            this.txtTotal.Text = this.calTotal(hoaDonBanThuoc).ToString();
            NhanVienInfo info = new NhanVienController().GetById(hoaDonBanThuoc.MaNhanVien);
            if (info.MaNhanVien != -1)
            {
                IFriendlyName name2 = info;
                this.txtNhanVien.Text = name2.FriendlyName();
            }
            this.txtChietKhau.Text = hoaDonBanThuoc.ChietKhau.ToString();
            this.txtThucThu.Text = hoaDonBanThuoc.ThucThu.ToString();
            QuayController controller3 = new QuayController();
            this.cboQuay.Text = controller3.getQuay(hoaDonBanThuoc.MaQuay).TenQuay;
            this.dgrBanThuocChiTiet.Rows.Clear();
            foreach (BanThuocChiTietInfo info2 in this.hdb.HoaDonChiTiet)
            {
                this.dgrBanThuocChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrBanThuocChiTiet.Rows[this.dgrBanThuocChiTiet.NewRowIndex - 1];
                MedicineController controller4 = new MedicineController();
                MedicineInfo medicineByMaThuocTraoDoi = controller4.GetMedicineByMaThuocTraoDoi(info2.MaThuocTraoDoi);
                row.Cells[this.colMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colMaThuocTraoDoi.Index].Value = info2.MaThuocTraoDoi;
                row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colDVT.Index].Value = controller4.GetDVT(info2.MaThuocTraoDoi).TenDV;
                row.Cells[this.colSoLuong.Index].Value = info2.SoLuong;
                row.Cells[this.colDonGia.Index].Value = info2.DonGiaBan;
                row.Cells[this.colThanhTien.Index].Value = info2.SoLuong * info2.DonGiaBan;
                row.Cells[this.colGhiChu.Index].Value = info2.GhiChu;
                row.Cells[this.colChietKhau.Index].Value = info2.ChietKhau;
            }
            this.IsLoading = false;
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.txtKhachHang, ref this._csSource);
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
        }

        private void SellOrderUI_Load(object sender, EventArgs e)
        {
            this.cboQuay.DataSource = base._quaySource;
            this.cboQuay.DisplayMember = "TenQuay";
            this.cboQuay.ValueMember = "TenQuay";
            this.loadAC();
            this.bindFormating();
            this.loadInfo(this.hdb);
            this.dgrBanThuocChiTiet.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dgrBanThuocChiTiet.CellValueChanged += new DataGridViewCellEventHandler(this.dgrBanThuocChiTiet_CellValueChanged);
            new DataGridViewDnDEnabler(this.dgrBanThuocChiTiet);
            DataGridViewCopyHandler.addDataGridViewClient(this.dgrBanThuocChiTiet);
        }

        private void txtChietKhau_Leave(object sender, EventArgs e)
        {
            if (!this._txtpcChietKhauFire)
            {
                double num3;
                this._txtChietKhauFire = true;
                this._txtpcChietKhauFire = false;
                double num = ConvertHelper.getDouble(this.txtChietKhau.Text);
                double num2 = ConvertHelper.getDouble(this.txtTotal.Text);
                if ((num2 + num) == 0.0)
                {
                    num3 = 0.0;
                }
                else
                {
                    num3 = (num / (num2 + num)) * 100.0;
                }
                this.txtpcChietKhau.Text = num3.ToString("#.0");
            }
            else
            {
                this._txtpcChietKhauFire = false;
                this._txtChietKhauFire = false;
            }
        }

        private void txtChietKhau_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNgay_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtpcChietKhau_Leave(object sender, EventArgs e)
        {
            if (!this._txtChietKhauFire)
            {
                this._txtpcChietKhauFire = true;
                this._txtChietKhauFire = false;
                double num = ConvertHelper.getDouble(this.txtpcChietKhau.Text);
                if (num == 100.0)
                {
                    this.txtChietKhau.Text = "0";
                }
                else
                {
                    this.txtChietKhau.Text = ((int) ((ConvertHelper.getInt(this.txtTotal.Text) * num) / (100.0 - num))).ToString();
                }
            }
            else
            {
                this._txtChietKhauFire = false;
                this._txtpcChietKhauFire = false;
            }
        }

        private void txtpcChietKhau_TextChanged(object sender, EventArgs e)
        {
        }

        private void updateThanhTien(DataGridViewRow dr)
        {
            try
            {
                int num = ConvertHelper.getInt(dr.Cells[this.colSoLuong.Index].Value);
                int num2 = ConvertHelper.getInt(dr.Cells[this.colDonGia.Index].Value);
                num = (num < 0) ? 0 : num;
                num2 = (num2 < 0) ? 0 : num2;
                dr.Cells[this.colThanhTien.Index].Value = num * num2;
                int a = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dgrBanThuocChiTiet.Rows)
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
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
        }

        private HoaDonBanThuocInfo HoaDonBanThuoc
        {
            get
            {
                this.getHoaDonBanThuoc();
                return this.hdb;
            }
            set
            {
                this.hdb = value;
                this.loadInfo(this.hdb);
            }
        }
    }
}
