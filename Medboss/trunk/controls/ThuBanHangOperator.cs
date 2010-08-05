namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ThuBanHangOperator : OperatorBase
    {
        private Button butThem;
        private DateTimePicker cboNgay;
        private ComboBox cboQuay;
        private DataGridViewTextBoxColumn colMaThu;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colThuBanHang;
        private DataGridViewTextBoxColumn colTienDoi;
        private DataGridViewTextBoxColumn colTongTien;
        private IContainer components = null;
        private int dataid;
        private DataGridView dgrListNgay;
        private Label label1;
        private Label label4;

        public ThuBanHangOperator()
        {
            this.InitializeComponent();
            this.loadAC();
            this.cboNgay.Value = DateTime.Now;
            this.formating();
            this.binding();
            DataGridViewDnDEnabler enabler = new DataGridViewDnDEnabler(this.dgrListNgay);
        }

        private void binding()
        {
            ThuBanHangController controller = new ThuBanHangController();
            QuayInfo info = this.cboQuay.SelectedItem as QuayInfo;
            DataTable table = controller.ListTable(string.Concat(new object[] { "Ngay=#", this.cboNgay.Value.ToString("MM/dd/yyyy"), "# and MaQuay=", info.MaQuay }));
            this.dgrListNgay.AutoGenerateColumns = false;
            this.dgrListNgay.DataSource = table;
            this.dgrListNgay.Refresh();
            foreach (DataGridViewRow row in (IEnumerable) this.dgrListNgay.Rows)
            {
                if (!row.IsNewRow)
                {
                    int num = ConvertHelper.getInt(row.Cells[this.colThuBanHang.Index].Value);
                    int num2 = ConvertHelper.getInt(row.Cells[this.colTienDoi.Index].Value);
                    row.Cells[this.colTongTien.Index].Value = (num + num2).ToString("#,#");
                }
            }
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        private void cboNgay_ValueChanged(object sender, EventArgs e)
        {
            this.binding();
        }

        private void cboQuay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.binding();
        }

        protected override int delete()
        {
            this.binding();
            return 0;
        }

        private void dgrListNgay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == this.colTienDoi.Index) || (e.ColumnIndex == this.colThuBanHang.Index))
            {
                DataGridViewRow row = this.dgrListNgay.Rows[e.RowIndex];
                int num = ConvertHelper.getInt(row.Cells[this.colTienDoi.Index].Value);
                num = (num < 0) ? 0 : num;
                int num2 = ConvertHelper.getInt(row.Cells[this.colThuBanHang.Index].Value);
                num2 = (num2 < 0) ? 0 : num2;
                row.Cells[this.colTongTien.Index].Value = num2 + num;
            }
        }

        private void dgrListNgay_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && ((e.ColumnIndex == this.colTienDoi.Index) || (e.ColumnIndex == this.colThuBanHang.Index)))
            {
                DataGridViewRow row = this.dgrListNgay.Rows[e.RowIndex];
                int num = ConvertHelper.getInt(row.Cells[this.colTienDoi.Index].Value);
                num = (num < 0) ? 0 : num;
                int num2 = ConvertHelper.getInt(row.Cells[this.colThuBanHang.Index].Value);
                num2 = (num2 < 0) ? 0 : num2;
                row.Cells[this.colTongTien.Index].Value = num2 + num;
            }
        }

        private void dgrListNgay_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void formating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colThuBanHang);
            factory.Bind(FormatType.Int, this.colTienDoi);
            factory.Bind(FormatType.Int, this.colTongTien);
        }

        private ThuBanHangInfo getInfo()
        {
            ThuBanHangInfo info = new ThuBanHangInfo();
            QuayInfo info2 = this.cboQuay.SelectedItem as QuayInfo;
            info.MaQuay = info2.MaQuay;
            info.Ngay = this.cboNgay.Value;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrListNgay.Rows)
            {
                if (!row.IsNewRow)
                {
                    ThuBanHangChiTietInfo info3 = new ThuBanHangChiTietInfo();
                    if (!row.IsNewRow)
                    {
                        info3.TienDoi = ConvertHelper.getInt(row.Cells[this.colTienDoi.Index].Value);
                        info3.ThuBanHang = ConvertHelper.getInt(row.Cells[this.colThuBanHang.Index].Value);
                        info3.MaThu = ConvertHelper.getInt(row.Cells[this.colMaThu.Index].Value);
                        info.ThuBanHangChiTiet.Add(info3);
                    }
                }
            }
            return info;
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.cboNgay = new DateTimePicker();
            this.label1 = new Label();
            this.cboQuay = new ComboBox();
            this.label4 = new Label();
            this.dgrListNgay = new DataGridView();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colMaThu = new DataGridViewTextBoxColumn();
            this.colThuBanHang = new DataGridViewTextBoxColumn();
            this.colTienDoi = new DataGridViewTextBoxColumn();
            this.colTongTien = new DataGridViewTextBoxColumn();
            this.butThem = new Button();
            ((ISupportInitialize) this.dgrListNgay).BeginInit();
            base.SuspendLayout();
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x5d, 14);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(100, 20);
            this.cboNgay.TabIndex = 0;
            this.cboNgay.ValueChanged += new EventHandler(this.cboNgay_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ng\x00e0y";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0xf7, 15);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(100, 0x15);
            this.cboQuay.TabIndex = 6;
            this.cboQuay.SelectedIndexChanged += new EventHandler(this.cboQuay_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xc7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x20, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quầy";
            this.dgrListNgay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrListNgay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrListNgay.Columns.AddRange(new DataGridViewColumn[] { this.colSTT, this.colMaThu, this.colThuBanHang, this.colTienDoi, this.colTongTien });
            this.dgrListNgay.Location = new Point(0x10, 0x2a);
            this.dgrListNgay.Name = "dgrListNgay";
            this.dgrListNgay.Size = new Size(0x1b4, 220);
            this.dgrListNgay.TabIndex = 8;
            this.dgrListNgay.CellValueChanged += new DataGridViewCellEventHandler(this.dgrListNgay_CellValueChanged);
            this.dgrListNgay.DataError += new DataGridViewDataErrorEventHandler(this.dgrListNgay_DataError);
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colMaThu.DataPropertyName = "MaThu";
            this.colMaThu.HeaderText = "M\x00e3 Thu";
            this.colMaThu.Name = "colMaThu";
            this.colMaThu.ReadOnly = true;
            this.colMaThu.Visible = false;
            this.colMaThu.Width = 0x45;
            this.colThuBanHang.DataPropertyName = "ThuBanHang";
            style.Alignment = DataGridViewContentAlignment.TopRight;
            style.Format = "N0";
            style.NullValue = "0";
            this.colThuBanHang.DefaultCellStyle = style;
            this.colThuBanHang.HeaderText = "Thu b\x00e1n h\x00e0ng";
            this.colThuBanHang.Name = "colThuBanHang";
            this.colThuBanHang.Width = 0x63;
            this.colTienDoi.DataPropertyName = "TienDoi";
            style2.Alignment = DataGridViewContentAlignment.TopRight;
            style2.Format = "N0";
            style2.NullValue = "0";
            this.colTienDoi.DefaultCellStyle = style2;
            this.colTienDoi.HeaderText = "Tiền d\x00f4i";
            this.colTienDoi.Name = "colTienDoi";
            this.colTienDoi.Width = 70;
            style3.Alignment = DataGridViewContentAlignment.TopRight;
            style3.Format = "N0";
            style3.NullValue = "0";
            this.colTongTien.DefaultCellStyle = style3;
            this.colTongTien.HeaderText = "Tổng tiền";
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.ReadOnly = true;
            this.colTongTien.Width = 0x4d;
            this.butThem.Location = new Point(0x1d3, 0x39);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 9;
            this.butThem.Text = "&Th\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.dgrListNgay);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboQuay);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboNgay);
            base.Name = "ThuBanHangOperator";
            base.Size = new Size(0x23f, 0x116);
            base.Load += new EventHandler(this.ThuBanHangOperator_Load);
            ((ISupportInitialize) this.dgrListNgay).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override int insert()
        {
            ThuBanHangController controller = new ThuBanHangController();
            ThuBanHangInfo thubanArr = this.getInfo();
            this.dataid = controller.Insert(thubanArr);
            this.binding();
            return this.dataid;
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
        }

        private void loadInfo(ThuBanHangInfo thuban)
        {
            QuayInfo info = new QuayController().getQuay(thuban.MaQuay);
            this.cboQuay.SelectedItem = info;
            this.cboNgay.Value = thuban.Ngay;
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
        }

        private void ThuBanHangOperator_Load(object sender, EventArgs e)
        {
        }

        protected override int update()
        {
            this.binding();
            return 0;
        }

        protected override int DataId
        {
            get
            {
                return this.dataid;
            }
        }

        protected override Nammedia.Medboss.controls.DataType DataType
        {
            get
            {
                return Nammedia.Medboss.controls.DataType.ThuBanHang;
            }
        }
    }
}
