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

    public class MedicineListOperator : OperatorBase
    {
        private Button butThem;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colGhiChu;
        private DataGridViewTextBoxColumn colHamLuong;
        private DataGridViewTextBoxColumn colNhaSanXuat;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn colTenThuoc;
        private DataGridViewTextBoxColumn colThanhPhan;
        private IContainer components = null;
        private int dataid = 0;
        private DataGridView dgrGiaThuoc;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;

        public MedicineListOperator()
        {
            this.InitializeComponent();
            this.bindFormating();
        }

        public void bindFormating()
        {
            FormatFactory factory = new FormatFactory();
            factory.Bind(FormatType.AutoNumber, this.colSTT);
            factory.Bind(FormatType.Trim, this.colGhiChu);
            factory.Bind(FormatType.Trim, this.colHamLuong);
            factory.Bind(FormatType.Trim, this.colNhaSanXuat);
            factory.Bind(FormatType.Trim, this.colThanhPhan);
            factory.Bind(FormatType.Trim, this.colTenThuoc);
            factory.Bind(FormatType.Trim, this.colDVT);
        }

        private void butThem_Click(object sender, EventArgs e)
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
            if (list.Count == 0)
            {
                throw new InvalidException("Nội dung chi tiết kh\x00f4ng được rỗng");
            }
            return list;
        }

        private void InitializeComponent()
        {
            this.dgrGiaThuoc = new DataGridView();
            this.colSTT = new DataGridViewTextBoxColumn();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colHamLuong = new DataGridViewTextBoxColumn();
            this.colThanhPhan = new DataGridViewTextBoxColumn();
            this.colNhaSanXuat = new DataGridViewTextBoxColumn();
            this.colGhiChu = new DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.butThem = new Button();
            ((ISupportInitialize) this.dgrGiaThuoc).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.dgrGiaThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrGiaThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrGiaThuoc.Columns.AddRange(new DataGridViewColumn[] { this.colSTT, this.colTenThuoc, this.colDVT, this.colHamLuong, this.colThanhPhan, this.colNhaSanXuat, this.colGhiChu });
            this.dgrGiaThuoc.Dock = DockStyle.Fill;
            this.dgrGiaThuoc.Location = new Point(3, 3);
            this.dgrGiaThuoc.Name = "dgrGiaThuoc";
            this.dgrGiaThuoc.Size = new Size(0x28f, 0x173);
            this.dgrGiaThuoc.TabIndex = 0;
            this.dgrGiaThuoc.CellEndEdit += new DataGridViewCellEventHandler(this.MedicineList_EndEdit);
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
            this.colHamLuong.HeaderText = "H\x00e0m Lượng";
            this.colHamLuong.Name = "colHamLuong";
            this.colHamLuong.Width = 0x57;
            this.colThanhPhan.HeaderText = "Th\x00e0nh phần";
            this.colThanhPhan.Name = "colThanhPhan";
            this.colThanhPhan.Width = 90;
            this.colNhaSanXuat.HeaderText = "Nh\x00e0 sản xuất";
            this.colNhaSanXuat.Name = "colNhaSanXuat";
            this.colNhaSanXuat.Width = 0x5f;
            this.colGhiChu.HeaderText = "Ghi ch\x00fa";
            this.colGhiChu.Name = "colGhiChu";
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
            this.panel1.Controls.Add(this.butThem);
            this.panel1.Location = new Point(0x1fa, 380);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x98, 0x2e);
            this.panel1.TabIndex = 1;
            this.butThem.Location = new Point(0x13, 3);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x7a, 0x17);
            this.butThem.TabIndex = 0;
            this.butThem.Text = "Th\x00eam thuốc mới";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "MedicineListOperator";
            base.Size = new Size(0x295, 0x1ad);
            base.Load += new EventHandler(this.MedicineList_Load);
            ((ISupportInitialize) this.dgrGiaThuoc).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            ArrayList medicineArray = this.GetMedicineArray();
            MedicineController controller = new MedicineController();
            foreach (MedicineInfo info in medicineArray)
            {
                controller.InsertMedicine(info);
            }
            this.loadAC();
            return 0;
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.colTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.colDVT, ref this._dvtSource);
        }

        private void MedicineList_EndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrGiaThuoc.Rows[e.RowIndex];
            if (e.ColumnIndex == this.colTenThuoc.Index)
            {
                MedicineController controller = new MedicineController();
                row.Cells[this.colDVT.Index].Value = controller.getDVT(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value));
                int num = controller.getMaThuocTraoDoi(ConvertHelper.getString(row.Cells[this.colTenThuoc.Index].Value), ConvertHelper.getString(row.Cells[this.colDVT.Index].Value));
                QuayController controller2 = new QuayController();
            }
        }

        private void MedicineList_Load(object sender, EventArgs e)
        {
            this.loadAC();
            new DataGridViewDnDEnabler(this.dgrGiaThuoc);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.colTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.colDVT, ref this._dvtSource);
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
                return Nammedia.Medboss.controls.DataType.Thuoc;
            }
        }
    }
}
