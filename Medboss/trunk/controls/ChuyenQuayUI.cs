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

    public class ChuyenQuayUI : MedUIBase
    {
        private ComboBox cboDenQuay;
        private DateTimePicker cboNgay;
        private ComboBox cboTuQuay;
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
        private IContainer components;
        private ChuyenQuayInfo cQuayInfo;
        private DataGridView dgrChuyenQuayChiTiet;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtNhanVien;
        private TextBox txtTotal;

        public ChuyenQuayUI()
        {
            this.cQuayInfo = new ChuyenQuayInfo();
            this.components = null;
            try
            {
                this.InitializeComponent();
                this.dgrChuyenQuayChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrChuyenQuayChiTiet.DefaultCellStyle);
                this.cQuayInfo.Ngay = DateTime.Now;
                this.loadInfo(this.cQuayInfo);
                this.bindFormating();
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        public ChuyenQuayUI(int MaHoaDon)
        {
            this.cQuayInfo = new ChuyenQuayInfo();
            this.components = null;
            try
            {
                this.InitializeComponent();
                this.dgrChuyenQuayChiTiet.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrChuyenQuayChiTiet.DefaultCellStyle);
                this.cQuayInfo = new ChuyenQuayController().GetById(MaHoaDon);
                if (this.cQuayInfo.MaChuyen != -1)
                {
                    this.loadInfo(this.cQuayInfo);
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colSoLuong);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colDonGiaBan);
            factory.Bind(FormatType.UpperCase, this.colSoLo);
            factory.Bind(FormatType.Int, this.txtTotal);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Trim, this.colSoLo);
            factory.Bind(FormatType.Int, this.colMaThuoc);
        }

        private int calTotal()
        {
            int num = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrChuyenQuayChiTiet.Rows)
            {
                if (!row.IsNewRow)
                {
                    int num2 = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    int num3 = ConvertHelper.getInt(row.Cells[this.colDonGiaBan.Index].Value);
                    num2 = (num2 == -1) ? 0 : num2;
                    num3 = (num3 == -1) ? 0 : num3;
                    num += num2 * num3;
                }
            }
            return num;
        }

        private void ChuyenQuayUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.cboTuQuay.DataSource = base._quaySource;
                this.cboTuQuay.DisplayMember = "TenQuay";
                this.cboTuQuay.AutoCompleteSource = AutoCompleteSource.CustomSource;
                if (base._quaySource != null)
                {
                    this.cboDenQuay.DataSource = base._quaySource.Clone();
                    this.cboDenQuay.DisplayMember = "TenQuay";
                }
                this.loadAC();
                this.loadInfo(this.cQuayInfo);
                this.dgrChuyenQuayChiTiet.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
                this.dgrChuyenQuayChiTiet.CellValueChanged += new DataGridViewCellEventHandler(this.dgrChuyenQuayChiTiet_CellValueChanged);
                new DataGridViewDnDEnabler(this.dgrChuyenQuayChiTiet);
                DataGridViewCopyHandler.addDataGridViewClient(this.dgrChuyenQuayChiTiet);
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        public void clear()
        {
            this.dgrChuyenQuayChiTiet.Rows.Clear();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineInfo.DonGiaNhapBan dgnb;
                DataGridViewRow row = this.dgrChuyenQuayChiTiet.Rows[e.RowIndex];
                if (e.ColumnIndex == this.colMaThuoc.Index)
                {
                    MedicineController controller = new MedicineController();
                    int maThuoc = ConvertHelper.getInt(row.Cells[this.colMaThuoc.Index].Value);
                    MedicineInfo medicineByMaThuoc = controller.GetMedicineByMaThuoc(maThuoc);
                    if ((medicineByMaThuoc.TenThuoc != "") && (medicineByMaThuoc.TenThuoc != null))
                    {
                        row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuoc.TenThuoc;
                        row.Cells[this.colDVT.Index].Value = MedicineController.GetDVTHinhThuc(medicineByMaThuoc);
                        dgnb = controller.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboTuQuay.Text));
                        row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaNhap;
                    }
                }
                if ((e.ColumnIndex == this.colTenThuoc.Index) || (e.ColumnIndex == this.colDVT.Index))
                {
                    MedicineController controller3 = new MedicineController();
                    string tenThuoc = ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value);
                    if (e.ColumnIndex == this.colTenThuoc.Index)
                    {
                        ArrayList medicine = controller3.GetMedicine(tenThuoc);
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
                    dgnb = controller3.getDonGiaNhapBan(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value), new QuayController().getMaQuay(this.cboTuQuay.Text));
                    row.Cells[this.colDonGiaBan.Index].Value = (int) dgnb.DonGiaNhap;
                }
            }
            catch (InvalidException exc)
            {
                LogManager.LogException(exc);
            }
        }

        private void dgrChuyenQuayChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == this.colDonGiaBan.Index) || (e.ColumnIndex == this.colSoLuong.Index))
            {
                this.tinhTong();
            }
        }

        private void dgrChuyenQuayChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        public ChuyenQuayInfo getThongTinChuyenQuay()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            this.cQuayInfo.Ngay = this.cboNgay.Value;
            QuayController controller = new QuayController();
            this.cQuayInfo.MaQuayNhan = controller.getMaQuay(this.cboDenQuay.Text);
            if (this.cQuayInfo.MaQuayNhan == -1)
            {
                arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", this.cboDenQuay.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.cboTuQuay;
                ufargs.Type = UnfoundType.Quay;
                throw new UnknownValueException(ufargs);
            }
            if (this.cQuayInfo.MaQuayNhan == -1)
            {
                throw new InvalidException("Kh\x00f4ng thấy quầy");
            }
            this.cQuayInfo.MaQuayXuat = controller.getMaQuay(this.cboTuQuay.Text);
            if (this.cQuayInfo.MaQuayXuat == -1)
            {
                arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", this.cboTuQuay.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.cboTuQuay;
                ufargs.Type = UnfoundType.Quay;
                throw new UnknownValueException(ufargs);
            }
            if (this.cQuayInfo.MaQuayXuat == this.cQuayInfo.MaQuayNhan)
            {
                throw new InvalidException("Quầy xuất v\x00e0 quầy nhận kh\x00f4ng được tr\x00f9ng t\x00ean");
            }
            this.cQuayInfo.MaNhanVien = new NhanVienController().getIdByName(this.txtNhanVien.Text);
            if ((this.cQuayInfo.MaNhanVien == -1) && (this.txtNhanVien.Text != ""))
            {
                arg = new UnfoundArg(FieldKey.TenNhanVien, "Nh\x00e2n vi\x00ean", this.txtNhanVien.Text);
                ufargs = new UnfoundArgs();
                ufargs.fieldValue.Add(arg);
                ufargs.control = this.txtNhanVien;
                ufargs.Type = UnfoundType.NV;
                throw new UnknownValueException(ufargs);
            }
            CSController controller3 = new CSController();
            this.cQuayInfo.ChuyenQuayChiTiet.Clear();
            int stt = 1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrChuyenQuayChiTiet.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                ChuyenQuayChiTietInfo info = new ChuyenQuayChiTietInfo();
                info.STT = stt;
                stt++;
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
                    args2.control = this.dgrChuyenQuayChiTiet;
                    throw new UnknownValueException(args2);
                }
                if (row.Cells[this.colSoLuong.Index].Value == null)
                {
                    info.SoLuong = 0;
                }
                else
                {
                    info.SoLuong = ConvertHelper.getInt(row.Cells[this.colSoLuong.Index].Value);
                    if (info.SoLuong < 0)
                    {
                        throw new InvalidException("Số lượng:" + row.Cells[this.colSoLuong.Index].Value);
                    }
                }
                info.SoLo = ConvertHelper.getString(row.Cells[this.colSoLo.Index].Value);
                if (ConvertHelper.getString(row.Cells[this.colHanSuDung.Index].Value) != "")
                {
                    info.HanDung = ConvertHelper.getTimeFormat(ConvertHelper.getString(row.Cells[this.colHanSuDung.Index].Value));
                }
                else
                {
                    info.HanDung = DateTime.MinValue;
                }
                this.cQuayInfo.ChuyenQuayChiTiet.Add(info);
            }
            if (this.cQuayInfo.ChuyenQuayChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            return this.cQuayInfo;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            this.tabItem1 = new TabItem(this.components);
            this.label6 = new Label();
            this.txtTotal = new TextBox();
            this.label2 = new Label();
            this.lblTitle = new Label();
            this.dgrChuyenQuayChiTiet = new DataGridView();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colMaThuoc = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colSoLuong = new DataGridViewTextBoxColumn();
            this.colDonGiaBan = new DataGridViewTextBoxColumn();
            this.colHanSuDung = new DataGridViewTextBoxColumn();
            this.colSoLo = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            this.label1 = new Label();
            this.label7 = new Label();
            this.txtNhanVien = new TextBox();
            this.label8 = new Label();
            this.cboTuQuay = new ComboBox();
            this.cboDenQuay = new ComboBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.cboNgay = new DateTimePicker();
            this.label3 = new Label();
            ((ISupportInitialize) this.dgrChuyenQuayChiTiet).BeginInit();
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
            this.lblTitle.Location = new Point(0x90, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0xfd, 0x1d);
            this.lblTitle.TabIndex = 0x17;
            this.lblTitle.Text = "Th\x00f4ng tin chuyển quầy";
            this.dgrChuyenQuayChiTiet.AllowUserToOrderColumns = true;
            this.dgrChuyenQuayChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrChuyenQuayChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrChuyenQuayChiTiet.Columns.AddRange(new DataGridViewColumn[] { this.colMaThuocTraoDoi, this.colSTT, this.colMaThuoc, this.colTenThuoc, this.colDVT, this.colSoLuong, this.colDonGiaBan, this.colHanSuDung, this.colSoLo, this.colGhiChu });
            this.dgrChuyenQuayChiTiet.Dock = DockStyle.Fill;
            this.dgrChuyenQuayChiTiet.Location = new Point(3, 0x67);
            this.dgrChuyenQuayChiTiet.Name = "dgrChuyenQuayChiTiet";
            this.dgrChuyenQuayChiTiet.Size = new Size(0x27a, 0xff);
            this.dgrChuyenQuayChiTiet.TabIndex = 12;
            this.dgrChuyenQuayChiTiet.UserDeletedRow += new DataGridViewRowEventHandler(this.dgrNhapThuocChiTiet_UserDeletedRow);
            this.dgrChuyenQuayChiTiet.DataError += new DataGridViewDataErrorEventHandler(this.dgrChuyenQuayChiTiet_DataError);
            style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMaThuocTraoDoi.DefaultCellStyle = style;
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colMaThuocTraoDoi.Visible = false;
            this.colMaThuocTraoDoi.Width = 0x61;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colMaThuoc.HeaderText = "M\x00e3 thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 0x47;
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
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            style2.Format = "N0";
            this.colSoLuong.DefaultCellStyle = style2;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colSoLuong.Width = 50;
            style3.Alignment = DataGridViewContentAlignment.TopRight;
            style3.Format = "N0";
            this.colDonGiaBan.DefaultCellStyle = style3;
            this.colDonGiaBan.HeaderText = "Đơn gi\x00e1 nhập";
            this.colDonGiaBan.Name = "colDonGiaBan";
            this.colDonGiaBan.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colDonGiaBan.Width = 0x30;
            style4.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colHanSuDung.DefaultCellStyle = style4;
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
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x2b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xdd, 0x2b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nh\x00e2n &vi\x00ean";
            this.txtNhanVien.Location = new Point(0x11e, 40);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x13, 0x45);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x2e, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Từ quầy";
            this.cboTuQuay.FormattingEnabled = true;
            this.cboTuQuay.Location = new Point(100, 0x45);
            this.cboTuQuay.Name = "cboTuQuay";
            this.cboTuQuay.Size = new Size(100, 0x15);
            this.cboTuQuay.TabIndex = 5;
            this.cboDenQuay.FormattingEnabled = true;
            this.cboDenQuay.Location = new Point(0x11e, 0x43);
            this.cboDenQuay.Name = "cboDenQuay";
            this.cboDenQuay.Size = new Size(100, 0x15);
            this.cboDenQuay.TabIndex = 0x19;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrChuyenQuayChiTiet, 0, 1);
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
            this.panel1.Anchor = AnchorStyles.Right;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new Point(0x1bb, 0x16c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xc2, 0x22);
            this.panel1.TabIndex = 0x10;
            this.panel2.Controls.Add(this.cboNgay);
            this.panel2.Controls.Add(this.cboDenQuay);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cboTuQuay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtNhanVien);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x27a, 0x5e);
            this.panel2.TabIndex = 0x11;
            this.cboNgay.AllowDrop = true;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(100, 0x2b);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0x1a;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xdd, 0x47);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 13);
            this.label3.TabIndex = 0x18;
            this.label3.Text = "Đến quầy";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ChuyenQuayUI";
            base.Size = new Size(640, 0x191);
            base.Load += new EventHandler(this.ChuyenQuayUI_Load);
            ((ISupportInitialize) this.dgrChuyenQuayChiTiet).EndInit();
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
            base._acFactory.EnableAutocomplete(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.EnableAutocomplete(this.cboTuQuay, ref this._quaySource);
            if (base._quaySource != null)
            {
                ArrayList datasource = (ArrayList) base._quaySource.Clone();
                base._acFactory.EnableAutocomplete(this.cboDenQuay, ref datasource);
            }
        }

        public void loadInfo(ChuyenQuayInfo chuyenQuayInfo)
        {
            this.cQuayInfo = chuyenQuayInfo;
            this.cboNgay.Value = this.cQuayInfo.Ngay;
            CSController controller = new CSController();
            QuayController controller2 = new QuayController();
            this.cboDenQuay.Text = controller2.getQuay(chuyenQuayInfo.MaQuayNhan).TenQuay;
            this.cboTuQuay.Text = controller2.getQuay(chuyenQuayInfo.MaQuayXuat).TenQuay;
            NhanVienInfo byId = new NhanVienController().GetById(this.cQuayInfo.MaNhanVien);
            if (byId.MaNhanVien != -1)
            {
                IFriendlyName name = byId;
                this.txtNhanVien.Text = name.FriendlyName();
            }
            this.dgrChuyenQuayChiTiet.Rows.Clear();
            foreach (ChuyenQuayChiTietInfo info2 in this.cQuayInfo.ChuyenQuayChiTiet)
            {
                int num = this.dgrChuyenQuayChiTiet.Rows.Add();
                DataGridViewRow row = this.dgrChuyenQuayChiTiet.Rows[num];
                MedicineController controller4 = new MedicineController();
                MedicineInfo medicineByMaThuocTraoDoi = controller4.GetMedicineByMaThuocTraoDoi(info2.MaThuocTraoDoi);
                row.Cells[this.colMaThuoc.Index].Value = medicineByMaThuocTraoDoi.MaThuoc;
                row.Cells[this.colTenThuoc.Index].Value = medicineByMaThuocTraoDoi.TenThuoc;
                row.Cells[this.colDVT.Index].Value = controller4.GetDVT(info2.MaThuocTraoDoi).TenDV;
                row.Cells[this.colSoLuong.Index].Value = info2.SoLuong;
                row.Cells[this.colDonGiaBan.Index].Value = info2.DonGiaBan;
                row.Cells[this.colSoLo.Index].Value = info2.SoLo;
                row.Cells[this.colSoLuong.Index].Value = info2.SoLuong;
                row.Cells[this.colGhiChu.Index].Value = info2.GhiChu;
                row.Cells[this.colMaThuocTraoDoi.Index].Value = info2.MaThuocTraoDoi;
                if (info2.HanDung != DateTime.MinValue)
                {
                    row.Cells[this.colHanSuDung.Index].Value = info2.HanDung.ToString("MM/yy");
                }
            }
            this.txtTotal.Text = this.calTotal().ToString();
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.txtNhanVien, ref this._nhanVienSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboTuQuay, ref this._quaySource);
            if (base._quaySource != null)
            {
                ArrayList datasource = (ArrayList) base._quaySource.Clone();
                base._acFactory.RefreshAutoCompleteSource(this.cboDenQuay, ref datasource);
            }
        }

        private void tinhTong()
        {
            int a = 0;
            a = this.calTotal();
            this.txtTotal.Text = ConvertHelper.formatNumber(a);
        }

        private ChuyenQuayInfo ThongTinChuyenQuay
        {
            get
            {
                this.getThongTinChuyenQuay();
                return this.cQuayInfo;
            }
            set
            {
                this.cQuayInfo = value;
                this.loadInfo(this.cQuayInfo);
            }
        }
    }
}
