namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MedicinePriceOperator : OperatorBase
    {
        private Button butCapNhat;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colGiaCu;
        private DataGridViewTextBoxColumn colGiaMoi;
        private DataGridViewTextBoxColumn colHamLuong;
        private DataGridViewTextBoxColumn colNhaSanXuat;
        private DataGridViewTextBoxColumn colQuay;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhPhan;
        private IContainer components = null;
        private DataGridView dgrGiaThuoc;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;

        public MedicinePriceOperator()
        {
            this.InitializeComponent();
            this.bindFormating();
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.Int, this.colGiaCu);
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Int, this.colGiaMoi);
            factory.Bind(FormatType.Trim, this.colDVT);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
        }

        private void butCapNhat_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        public override void Clear()
        {
            this.dgrGiaThuoc.Rows.Clear();
        }

        private void dgrGiaThuoc_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private ArrayList GetMedicineArray()
        {
            ArrayList list = new ArrayList();
            foreach (DataGridViewRow row in (IEnumerable) this.dgrGiaThuoc.Rows)
            {
                if (!row.IsNewRow)
                {
                    MedicineInfo info = new MedicineInfo();
                    info.TenThuoc = ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value);
                    info.NhaSanXuat = ConvertHelper.getString(row.Cells[this.colNhaSanXuat.Index].Value);
                    info.ThanhPhan = ConvertHelper.getString(row.Cells[this.colThanhPhan.Index].Value);
                    info.HamLuong = ConvertHelper.getString(row.Cells[this.colHamLuong.Index].Value);
                    DVT dvt = new DVT();
                    dvt.TenDV = ConvertHelper.getString(row.Cells[this.colDVT.Index].Value);
                    ThuocTraoDoi doi = new ThuocTraoDoi();
                    doi.DVT = dvt;
                    info.ThuocTraoDois.Add(doi);
                    list.Add(info);
                }
            }
            return list;
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            this.dgrGiaThuoc = new DataGridView();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colGiaMoi = new DataGridViewTextBoxColumn();
            this.colGiaCu = new DataGridViewTextBoxColumn();
            this.colQuay = new DataGridViewTextBoxColumn();
            this.colHamLuong = new DataGridViewTextBoxColumn();
            this.colThanhPhan = new DataGridViewTextBoxColumn();
            this.colNhaSanXuat = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.butCapNhat = new Button();
            ((ISupportInitialize) this.dgrGiaThuoc).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.dgrGiaThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrGiaThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrGiaThuoc.Columns.AddRange(new DataGridViewColumn[] { this.colSTT, this.colTenThuoc, this.colDVT, this.colGiaMoi, this.colGiaCu, this.colQuay, this.colHamLuong, this.colThanhPhan, this.colNhaSanXuat, this.colGhiChu });
            this.dgrGiaThuoc.Dock = DockStyle.Fill;
            this.dgrGiaThuoc.Location = new Point(3, 3);
            this.dgrGiaThuoc.Name = "dgrGiaThuoc";
            this.dgrGiaThuoc.Size = new Size(0x28f, 0x173);
            this.dgrGiaThuoc.TabIndex = 0;
            this.dgrGiaThuoc.CellEndEdit += new DataGridViewCellEventHandler(this.MedicinePrice_EndEdit);
            this.dgrGiaThuoc.DataError += new DataGridViewDataErrorEventHandler(this.dgrGiaThuoc_DataError);
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 0x35;
            this.colTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.Width = 0x51;
            this.colDVT.HeaderText = "DVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Width = 0x36;
            style.Alignment = DataGridViewContentAlignment.TopRight;
            style.Format = "N0";
            style.NullValue = "0";
            this.colGiaMoi.DefaultCellStyle = style;
            this.colGiaMoi.HeaderText = "Gi\x00e1 mới";
            this.colGiaMoi.Name = "colGiaMoi";
            this.colGiaMoi.Width = 0x43;
            style2.Alignment = DataGridViewContentAlignment.TopRight;
            style2.Format = "N0";
            style2.NullValue = "0";
            this.colGiaCu.DefaultCellStyle = style2;
            this.colGiaCu.HeaderText = "Gi\x00e1 cũ";
            this.colGiaCu.Name = "colGiaCu";
            this.colGiaCu.ReadOnly = true;
            this.colGiaCu.Width = 0x3f;
            this.colQuay.HeaderText = "Quầy";
            this.colQuay.Name = "colQuay";
            this.colQuay.Width = 0x39;
            this.colHamLuong.HeaderText = "H\x00e0m Lượng";
            this.colHamLuong.Name = "colHamLuong";
            this.colHamLuong.ReadOnly = true;
            this.colHamLuong.Width = 0x57;
            this.colThanhPhan.HeaderText = "Th\x00e0nh phần";
            this.colThanhPhan.Name = "colThanhPhan";
            this.colThanhPhan.ReadOnly = true;
            this.colThanhPhan.Width = 90;
            this.colNhaSanXuat.HeaderText = "Nh\x00e0 sản xuất";
            this.colNhaSanXuat.Name = "colNhaSanXuat";
            this.colNhaSanXuat.ReadOnly = true;
            this.colNhaSanXuat.Width = 0x5f;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Width = 0x45;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrGiaThuoc, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            this.tableLayoutPanel1.Size = new Size(0x295, 0x1ad);
            this.tableLayoutPanel1.TabIndex = 1;
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel1.Controls.Add(this.butCapNhat);
            this.panel1.Location = new Point(0x1fa, 380);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x98, 0x2e);
            this.panel1.TabIndex = 1;
            this.butCapNhat.Location = new Point(3, 3);
            this.butCapNhat.Name = "butCapNhat";
            this.butCapNhat.Size = new Size(0x73, 0x17);
            this.butCapNhat.TabIndex = 1;
            this.butCapNhat.Text = "Cập Nhật Gi\x00e1";
            this.butCapNhat.UseVisualStyleBackColor = true;
            this.butCapNhat.Click += new EventHandler(this.butCapNhat_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "MedicinePriceOperator";
            base.Size = new Size(0x295, 0x1ad);
            base.Load += new EventHandler(this.MedicinePrice_Load);
            ((ISupportInitialize) this.dgrGiaThuoc).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            ArrayList medicineArray = this.GetMedicineArray();
            MedicineController controller = new MedicineController();
            QuayController controller2 = new QuayController();
            int num = -1;
            foreach (DataGridViewRow row in (IEnumerable) this.dgrGiaThuoc.Rows)
            {
                if (!row.IsNewRow)
                {
                    UnfoundArg arg;
                    string tenThuoc = ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value);
                    string dVT = ConvertHelper.getString(row.Cells[this.colDVT.Index].Value);
                    int maThuocTraoDoi = controller.getMaThuocTraoDoi(tenThuoc, dVT);
                    num = maThuocTraoDoi;
                    if (maThuocTraoDoi == -1)
                    {
                        UnfoundArgs ufargs = new UnfoundArgs();
                        ufargs.Type = UnfoundType.ThuocDVT;
                        arg = new UnfoundArg();
                        arg.Key = FieldKey.TenThuoc;
                        arg.Value = tenThuoc;
                        arg.Field = "T\x00ean Thuốc";
                        ufargs.fieldValue.Add(arg);
                        UnfoundArg arg2 = new UnfoundArg();
                        arg.Key = FieldKey.TenDVT;
                        arg2.Field = "ĐVT";
                        arg2.Value = dVT;
                        ufargs.fieldValue.Add(arg2);
                        ufargs.control = row;
                        throw new UnknownValueException(ufargs);
                    }
                    string tenQuay = ConvertHelper.getString(row.Cells[this.colQuay.Index].Value);
                    int maQuay = controller2.getMaQuay(tenQuay);
                    if ((maQuay == -1) && (tenQuay != ""))
                    {
                        arg = new UnfoundArg(FieldKey.TenQuay, "Quầy", tenQuay);
                        UnfoundArgs args2 = new UnfoundArgs();
                        args2.fieldValue.Add(arg);
                        args2.control = row;
                        args2.Type = UnfoundType.Quay;
                        throw new UnknownValueException(args2);
                    }
                    int donGiaBan = ConvertHelper.getInt(row.Cells[this.colGiaMoi.Index].Value);
                    controller.UpdateDonGiaBan(maThuocTraoDoi, maQuay, donGiaBan);
                }
            }
            return num;
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
        }

        private void MedicinePrice_EndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrGiaThuoc.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colTenThuoc.Index)
            {
                MedicineController controller = new MedicineController();
                row.Cells[this.colDVT.Index].Value = controller.getDVT(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value));
                int maThuocTraoDoi = controller.getMaThuocTraoDoi(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value));
                QuayController controller2 = new QuayController();
                int num2 = controller.getDonGia(maThuocTraoDoi, ConvertHelper.getInt(controller2.getMaQuay(ConvertHelper.getString(row.Cells[this.colQuay.Index].Value))));
                row.Cells[this.colGiaCu.Index].Value = (num2 < 0) ? 0 : num2;
            }
        }

        private void MedicinePrice_Load(object sender, EventArgs e)
        {
            this.loadAC();
            new DataGridViewDnDEnabler(this.dgrGiaThuoc);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
        }
    }
}
