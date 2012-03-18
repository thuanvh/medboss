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
        private IContainer components = null;
        public DataGridView dgrKiemKeChiTiet;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colMaThuoc;
        private DataGridViewTextBoxColumn colThuoc;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colSoLuong;
        private DataGridViewTextBoxColumn colDonGiaNhap;
        private DataGridViewTextBoxColumn colDonGiaBan;
        private DataGridViewTextBoxColumn colHanDung;
        private DataGridViewTextBoxColumn colSoLo;
        private DataGridViewTextBoxColumn colTinhTrang;
        private DataGridViewTextBoxColumn colGhiChu;
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
                info2.SoLo = ConvertHelper.getString(row.Cells[this.colSoLo.Index].Value);
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNhanVien = new System.Windows.Forms.TextBox();
            this.dgrKiemKeChiTiet = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboNgay = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLoaiKiemKe = new System.Windows.Forms.ComboBox();
            this.cboQuay = new System.Windows.Forms.ComboBox();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTinhTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrKiemKeChiTiet)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&gày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhân &viên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Quầy";
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.Location = new System.Drawing.Point(213, 14);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.Size = new System.Drawing.Size(100, 20);
            this.txtNhanVien.TabIndex = 3;
            // 
            // dgrKiemKeChiTiet
            // 
            this.dgrKiemKeChiTiet.AllowUserToOrderColumns = true;
            this.dgrKiemKeChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrKiemKeChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrKiemKeChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colMaThuoc,
            this.colThuoc,
            this.colDVT,
            this.colSoLuong,
            this.colDonGiaNhap,
            this.colDonGiaBan,
            this.colHanDung,
            this.colSoLo,
            this.colTinhTrang,
            this.colGhiChu});
            this.dgrKiemKeChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrKiemKeChiTiet.Location = new System.Drawing.Point(3, 74);
            this.dgrKiemKeChiTiet.Name = "dgrKiemKeChiTiet";
            this.dgrKiemKeChiTiet.Size = new System.Drawing.Size(658, 302);
            this.dgrKiemKeChiTiet.TabIndex = 6;
            this.dgrKiemKeChiTiet.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.KiemKeChiTiet_EndEdit);
            this.dgrKiemKeChiTiet.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgrKiemKeChiTiet_DataError);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgrKiemKeChiTiet, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.79433F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.20567F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(664, 379);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboNgay);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cboLoaiKiemKe);
            this.panel1.Controls.Add(this.cboQuay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtNhanVien);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 65);
            this.panel1.TabIndex = 7;
            // 
            // cboNgay
            // 
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cboNgay.Location = new System.Drawing.Point(41, 14);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new System.Drawing.Size(104, 20);
            this.cboNgay.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(464, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Loại kiểm kê";
            // 
            // cboLoaiKiemKe
            // 
            this.cboLoaiKiemKe.FormattingEnabled = true;
            this.cboLoaiKiemKe.Location = new System.Drawing.Point(534, 13);
            this.cboLoaiKiemKe.Name = "cboLoaiKiemKe";
            this.cboLoaiKiemKe.Size = new System.Drawing.Size(121, 21);
            this.cboLoaiKiemKe.TabIndex = 6;
            // 
            // cboQuay
            // 
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new System.Drawing.Point(358, 14);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new System.Drawing.Size(100, 21);
            this.cboQuay.TabIndex = 5;
            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 53;
            // 
            // colMaThuoc
            // 
            this.colMaThuoc.HeaderText = "Mã thuốc";
            this.colMaThuoc.Name = "colMaThuoc";
            this.colMaThuoc.Width = 77;
            // 
            // colThuoc
            // 
            this.colThuoc.DataPropertyName = "TenThuoc";
            this.colThuoc.HeaderText = "Tên thuốc, thành phần, hàm lượng";
            this.colThuoc.Name = "colThuoc";
            this.colThuoc.Width = 134;
            // 
            // colDVT
            // 
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "Đơn vị tính";
            this.colDVT.Name = "colDVT";
            this.colDVT.Width = 79;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle1.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 69;
            // 
            // colDonGiaNhap
            // 
            this.colDonGiaNhap.DataPropertyName = "DonGiaNhap";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle2.Format = "N0";
            this.colDonGiaNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDonGiaNhap.HeaderText = "Đơn giá nhập";
            this.colDonGiaNhap.Name = "colDonGiaNhap";
            this.colDonGiaNhap.Width = 88;
            // 
            // colDonGiaBan
            // 
            this.colDonGiaBan.DataPropertyName = "DonGiaBan";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colDonGiaBan.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDonGiaBan.HeaderText = "Đơn giá bán";
            this.colDonGiaBan.Name = "colDonGiaBan";
            this.colDonGiaBan.Width = 67;
            // 
            // colHanDung
            // 
            this.colHanDung.DataPropertyName = "HanDung";
            this.colHanDung.HeaderText = "Hạn dùng";
            this.colHanDung.Name = "colHanDung";
            this.colHanDung.Width = 73;
            // 
            // colSoLo
            // 
            this.colSoLo.HeaderText = "Số lô";
            this.colSoLo.Name = "colSoLo";
            this.colSoLo.Width = 45;
            // 
            // colTinhTrang
            // 
            this.colTinhTrang.HeaderText = "Tình trạng";
            this.colTinhTrang.Name = "colTinhTrang";
            this.colTinhTrang.Width = 74;
            // 
            // colGhiChu
            // 
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 64;
            // 
            // KiemKeUI
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "KiemKeUI";
            this.Size = new System.Drawing.Size(664, 379);
            this.Load += new System.EventHandler(this.KiemKeUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrKiemKeChiTiet)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
            if ((e.ColumnIndex == this.colThuoc.Index) || (e.ColumnIndex == this.colDVT.Index) || (e.ColumnIndex==this.colSoLuong.Index))
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
                row.Cells[this.colSoLo.Index].Value = info.SoLo;
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
