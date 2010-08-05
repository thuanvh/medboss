namespace Nammedia.Medboss.controls
{
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

    public class QuyUI : MedUIBase
    {
        private bool _isLoaiThuChiEditing = false;
        private Nammedia.Medboss.lib.QuyInfo _quy = new Nammedia.Medboss.lib.QuyInfo();
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colChi;
        private DataGridViewTextBoxColumn colDienGiai;
        private DataGridViewTextBoxColumn colDoiTac;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewComboBoxColumn colLoaiThuChi;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colThu;
        private IContainer components = null;
        private DataGridView dgrQuy;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtTongChi;
        private TextBox txtTongThu;

        public QuyUI()
        {
            this.InitializeComponent();
            this.dgrQuy.DefaultCellStyle = new CommonDataGridViewCellStyle(this.dgrQuy.DefaultCellStyle);
            this.loadAC();
            this.cboNgay.Value = DateTime.Now;
            this.bindFormating();
            new DataGridViewDnDEnabler(this.dgrQuy);
            DataGridViewCopyHandler.addDataGridViewClient(this.dgrQuy);
            this.colLoaiThuChi.ValueType = typeof(string);
            this.dgrQuy.CellEndEdit += new DataGridViewCellEventHandler(this.dgrQuy_CellEndEdit);
            this.dgrQuy.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dgrQuy_CellBeginEdit);
            this.dgrQuy.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dgrQuy_EditingControlShowing);
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colChi);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colThu);
            factory.Bind(FormatType.Int, this.txtTongThu);
            factory.Bind(FormatType.Int, this.txtTongChi);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Trim, this.colDoiTac);
            factory.Bind(FormatType.Trim, this.colDienGiai);
        }

        private void calTongThuChi(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == this.colThu.Index) || (e.ColumnIndex == this.colChi.Index))
            {
                try
                {
                    int a = 0;
                    int num2 = 0;
                    foreach (DataGridViewRow row in (IEnumerable) this.dgrQuy.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            int num3 = ConvertHelper.getInt(row.Cells[this.colChi.Index].Value);
                            num3 = (num3 < 0) ? 0 : num3;
                            num2 += num3;
                            num3 = ConvertHelper.getInt(row.Cells[this.colThu.Index].Value);
                            num3 = (num3 < 0) ? 0 : num3;
                            a += num3;
                        }
                    }
                    this.txtTongChi.Text = ConvertHelper.formatNumber(num2);
                    this.txtTongThu.Text = ConvertHelper.formatNumber(a);
                }
                catch (Exception exc)
                {
                    LogManager.LogException(exc);
                }
            }
        }

        public void clear()
        {
            this.dgrQuy.Rows.Clear();
        }

        private void dgrQuy_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == this.colLoaiThuChi.Index)
            {
                this._isLoaiThuChiEditing = true;
            }
        }

        private void dgrQuy_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this._isLoaiThuChiEditing = false;
        }

        private void dgrQuy_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void dgrQuy_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgrQuy_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this._isLoaiThuChiEditing)
            {
                DataGridViewComboBoxEditingControl editingControl = (DataGridViewComboBoxEditingControl) this.dgrQuy.EditingControl;
                editingControl.AllowDrop = true;
                editingControl.MaxDropDownItems = 8;
                editingControl.DropDownHeight = 200;
                editingControl.AutoCompleteSource = AutoCompleteSource.ListItems;
                editingControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
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

        public Nammedia.Medboss.lib.QuyInfo getQuyInfo()
        {
            UnfoundArg arg;
            UnfoundArgs ufargs;
            Nammedia.Medboss.lib.QuyInfo info = this._quy;
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
            info.Ngay = this.cboNgay.Value;
            info.ThuChiChiTiet.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgrQuy.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                KhoanThuChiInfo info2 = new KhoanThuChiInfo();
                LoaiThuChiController controller2 = new LoaiThuChiController();
                info2.TenLoaiThuChi = ConvertHelper.getString(row.Cells[this.colLoaiThuChi.Index].Value);
                info2.MaLoaiThuChi = controller2.getId(info2.TenLoaiThuChi);
                if ((info2.MaLoaiThuChi == -1) || (info2.TenLoaiThuChi == ""))
                {
                    throw new InvalidException("Loại thu chi:" + info2.TenLoaiThuChi);
                }
                int num = ConvertHelper.getInt(row.Cells[this.colThu.Index].Value);
                int num2 = ConvertHelper.getInt(row.Cells[this.colChi.Index].Value);
                if ((num > 0) && (((row.Cells[this.colChi.Index].Value == null) && (num2 == -1)) || ((row.Cells[this.colChi.Index].Value != null) && (num2 == 0))))
                {
                    info2.Tien = num;
                    info2.ThuHayChi = KhoanThuChiInfo.Thu;
                }
                else
                {
                    if ((num2 <= 0) || (((row.Cells[this.colThu.Index].Value != null) || (num != -1)) && ((row.Cells[this.colThu.Index].Value == null) || (num != 0))))
                    {
                        throw new InvalidException("Tiền thu: " + ConvertHelper.getString(row.Cells[this.colThu.Index].Value) + ", Tiền chi: " + ConvertHelper.getString(row.Cells[this.colChi.Index].Value) + "Kh\x00f4ng hợp lệ");
                    }
                    info2.Tien = num2;
                    info2.ThuHayChi = KhoanThuChiInfo.Chi;
                }
                info2.GhiChu = ConvertHelper.getString(row.Cells[this.colGhiChu.Index].Value);
                info2.DienGiai = ConvertHelper.getString(row.Cells[this.colDienGiai.Index].Value);
                if (info2.DienGiai.Trim() == "")
                {
                    throw new InvalidException("Diễn giải kh\x00f4ng được rỗng");
                }
                CSController controller3 = new CSController();
                string csName = ConvertHelper.getString(row.Cells[this.colDoiTac.Index].Value);
                info2.MaKhachHang = controller3.GetIdByName(csName);
                if ((info2.MaKhachHang == -1) && (csName != ""))
                {
                    arg = new UnfoundArg(FieldKey.TenKhachHang, "Nh\x00e0 cung cấp", csName);
                    ufargs = new UnfoundArgs();
                    ufargs.fieldValue.Add(arg);
                    ufargs.control = row;
                    ufargs.Type = UnfoundType.DoiTac;
                    throw new UnknownValueException(ufargs);
                }
                info.ThuChiChiTiet.Add(info2);
            }
            if (info.ThuChiChiTiet.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            return info;
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            this.label1 = new Label();
            this.dgrQuy = new DataGridView();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.cboNgay = new DateTimePicker();
            this.cboQuay = new ComboBox();
            this.label2 = new Label();
            this.panel2 = new Panel();
            this.txtTongChi = new TextBox();
            this.txtTongThu = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colLoaiThuChi = new DataGridViewComboBoxColumn();
            this.colDienGiai = new DataGridViewTextBoxColumn();
            this.colThu = new DataGridViewTextBoxColumn();
            this.colChi = new DataGridViewTextBoxColumn();
            this.colDoiTac = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrQuy).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "N&g\x00e0y";
            this.dgrQuy.AllowUserToOrderColumns = true;
            this.dgrQuy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrQuy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrQuy.Columns.AddRange(new DataGridViewColumn[] { this.colSTT, this.colLoaiThuChi, this.colDienGiai, this.colThu, this.colChi, this.colDoiTac, this.colGhiChu });
            this.dgrQuy.Dock = DockStyle.Fill;
            this.dgrQuy.Location = new Point(3, 0x2f);
            this.dgrQuy.Name = "dgrQuy";
            this.dgrQuy.Size = new Size(0x255, 230);
            this.dgrQuy.TabIndex = 4;
            this.dgrQuy.CellEndEdit += new DataGridViewCellEventHandler(this.calTongThuChi);
            this.dgrQuy.DataError += new DataGridViewDataErrorEventHandler(this.dgrQuy_DataError_1);
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgrQuy, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15.82278f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 84.17722f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 57f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x25b, 0x152);
            this.tableLayoutPanel1.TabIndex = 6;
            this.panel1.Controls.Add(this.cboNgay);
            this.panel1.Controls.Add(this.cboQuay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x255, 0x26);
            this.panel1.TabIndex = 7;
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x2b, 3);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(0x80, 20);
            this.cboNgay.TabIndex = 4;
            this.cboQuay.AllowDrop = true;
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x106, 1);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(0x79, 0x15);
            this.cboQuay.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xdf, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x20, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Quầy";
            this.panel2.Controls.Add(this.txtTongChi);
            this.panel2.Controls.Add(this.txtTongThu);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(3, 0x11b);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x255, 0x34);
            this.panel2.TabIndex = 8;
            this.txtTongChi.Location = new Point(0x1b0, 0x10);
            this.txtTongChi.Name = "txtTongChi";
            this.txtTongChi.ReadOnly = true;
            this.txtTongChi.Size = new Size(100, 20);
            this.txtTongChi.TabIndex = 8;
            this.txtTongChi.TextAlign = HorizontalAlignment.Right;
            this.txtTongThu.Location = new Point(270, 0x10);
            this.txtTongThu.Name = "txtTongThu";
            this.txtTongThu.ReadOnly = true;
            this.txtTongThu.Size = new Size(100, 20);
            this.txtTongThu.TabIndex = 6;
            this.txtTongThu.TextAlign = HorizontalAlignment.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x178, 0x13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tổng Chi";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(210, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tổng Thu";
            this.colSTT.Frozen = true;
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colLoaiThuChi.DropDownWidth = 5;
            this.colLoaiThuChi.HeaderText = "Loại thu chi";
            this.colLoaiThuChi.Name = "colLoaiThuChi";
            this.colLoaiThuChi.Resizable = DataGridViewTriState.True;
            this.colLoaiThuChi.SortMode = DataGridViewColumnSortMode.Automatic;
            this.colLoaiThuChi.Width = 0x57;
            this.colDienGiai.HeaderText = "Diễn giải";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Width = 0x49;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.colThu.DefaultCellStyle = dataGridViewCellStyle1;
            this.colThu.HeaderText = "Tiền thu";
            this.colThu.Name = "colThu";
            this.colThu.Resizable = DataGridViewTriState.True;
            this.colThu.Width = 0x47;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.colChi.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChi.HeaderText = "Tiền chi";
            this.colChi.Name = "colChi";
            this.colChi.Width = 70;
            this.colDoiTac.HeaderText = "Đối t\x00e1c";
            this.colDoiTac.Name = "colDoiTac";
            this.colDoiTac.Width = 0x42;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 0x45;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "QuyUI";
            base.Size = new Size(0x25b, 0x152);
            ((ISupportInitialize) this.dgrQuy).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            if ((base._loaiThuChi != null) && (base._loaiThuChi.Count > 0))
            {
                this.colLoaiThuChi.DataSource = base._loaiThuChi;
                this.colLoaiThuChi.DisplayMember = "TenLoaiThuChi";
                this.colLoaiThuChi.ValueMember = "TenLoaiThuChi";
            }
        }

        public void loadQuyInfo(Nammedia.Medboss.lib.QuyInfo quyi)
        {
            this._quy = quyi;
            QuayInfo info = new QuayController().getQuay(quyi.MaQuay);
            this.cboQuay.Text = info.TenQuay;
            this.cboNgay.Value = quyi.Ngay;
            this.dgrQuy.Rows.Clear();
            foreach (KhoanThuChiInfo info2 in quyi.ThuChiChiTiet)
            {
                this.dgrQuy.Rows.Add();
                DataGridViewRow row = this.dgrQuy.Rows[this.dgrQuy.NewRowIndex - 1];
                row.Cells[this.colDienGiai.Index].Value = info2.DienGiai;
                CSInfo byId = new CSController().GetById(info2.MaKhachHang);
                row.Cells[this.colDoiTac.Index].Value = byId.Ten;
                row.Cells[this.colGhiChu.Index].Value = info2.GhiChu;
                if (info2.ThuHayChi == KhoanThuChiInfo.Chi)
                {
                    row.Cells[this.colChi.Index].Value = info2.Tien;
                }
                else
                {
                    row.Cells[this.colThu.Index].Value = info2.Tien;
                }
                LoaiThuChiInfo info4 = new LoaiThuChiController().get(info2.MaLoaiThuChi);
                row.Cells[this.colLoaiThuChi.Index].Value = info4.TenLoaiThuChi;
            }
        }

        public override void RefreshAC()
        {
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            if ((base._loaiThuChi != null) && (base._loaiThuChi.Count > 0))
            {
                this.colLoaiThuChi.DataSource = base._loaiThuChi;
                this.colLoaiThuChi.DisplayMember = "TenLoaiThuChi";
                this.colLoaiThuChi.ValueMember = "TenLoaiThuChi";
            }
        }

        private Nammedia.Medboss.lib.QuyInfo QuyInfo
        {
            get
            {
                this._quy = this.getQuyInfo();
                return this._quy;
            }
            set
            {
                this._quy = value;
                this.loadQuyInfo(this._quy);
            }
        }
    }
}
