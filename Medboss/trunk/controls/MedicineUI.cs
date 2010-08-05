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

    public class MedicineUI : MedUIBase
    {
        private bool _isNew;
        private ComboBox cboDVTHinhThuc;
        private ComboBox cboLoaiThuoc;
        private IContainer components;
        private DataGridView dgrDVT;
        private DataGridViewTextBoxColumn DVT;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblLoaiThuoc;
        private DataGridViewTextBoxColumn MaThuocTraoDoi;
        private MedicineInfo mi;
        private DataGridViewTextBoxColumn TiLe;
        private TextBox txtHamLuong;
        private TextBox txtMaThuoc;
        private TextBox txtNhaSanXuat;
        private TextBox txtTenThuoc;
        private TextBox txtThanhPhan;

        public MedicineUI()
        {
            this.components = null;
            this.mi = new MedicineInfo();
            this.InitializeComponent();
            this.loadAC();
            this.txtMaThuoc.Text = IdManager.GetMissingId("MaThuoc", "Thuoc").ToString();
        }

        public MedicineUI(MedicineInfo medInfo)
        {
            this.components = null;
            this.mi = new MedicineInfo();
            this.InitializeComponent();
            this.loadAC();
            this.loadData(medInfo);
        }

        public MedicineUI(int MaThuoc)
        {
            this.components = null;
            this.mi = new MedicineInfo();
            this.InitializeComponent();
            this.loadAC();
            this.mi = new MedicineController().GetMedicineByMaThuoc(MaThuoc);
            this.loadData(this.mi);
        }

        private void dgrDVT_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        public MedicineInfo getMedicine()
        {
            this.mi.MaThuoc = ConvertHelper.getInt(this.txtMaThuoc.Text);
            this.mi.TenThuoc = this.txtTenThuoc.Text;
            if (this.mi.TenThuoc == "")
            {
                throw new InvalidException("T\x00ean thuốc kh\x00f4ng được rỗng");
            }
            this.mi.ThanhPhan = this.txtThanhPhan.Text;
            this.mi.HamLuong = this.txtHamLuong.Text;
            this.mi.NhaSanXuat = this.txtNhaSanXuat.Text;
            this.mi.MaDVTHinhThuc = new MedicineController().getMaDVT(this.cboDVTHinhThuc.Text);
            LoaiThuocController controller2 = new LoaiThuocController();
            LoaiThuocInfo info = new LoaiThuocInfo();
            info = controller2.Get(this.cboLoaiThuoc.Text);
            if (info == null)
            {
                throw new InvalidException("Loại thuốc phải được chỉ r\x00f5");
            }
            this.mi.LoaiThuoc = info;
            this.mi.ThuocTraoDois.Clear();
            foreach (DataGridViewRow row in (IEnumerable) this.dgrDVT.Rows)
            {
                if (!row.IsNewRow)
                {
                    ThuocTraoDoi doi = new ThuocTraoDoi();
                    Nammedia.Medboss.lib.DVT dvt = new Nammedia.Medboss.lib.DVT();
                    dvt.MaDVT = ConvertHelper.getInt(row.Cells["MaThuocTraoDoi"].Value);
                    dvt.TenDV = ConvertHelper.getString(row.Cells["DVT"].Value);
                    dvt.TenDV = dvt.TenDV.Trim();
                    if (dvt.TenDV == "")
                    {
                        throw new InvalidException("Đơn vị t\x00ednh kh\x00f4ng được rỗng");
                    }
                    doi.DVT = dvt;
                    doi.TiLe = ConvertHelper.getInt(row.Cells["TiLe"].Value);
                    if (doi.TiLe == 0)
                    {
                        throw new InvalidException("Tỉ lệ phải lớn hơn 0");
                    }
                    this.mi.ThuocTraoDois.Add(doi);
                }
            }
            return this.mi;
        }

        private void InitializeComponent()
        {
            this.txtThanhPhan = new TextBox();
            this.label1 = new Label();
            this.txtHamLuong = new TextBox();
            this.label2 = new Label();
            this.txtNhaSanXuat = new TextBox();
            this.txtTenThuoc = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.dgrDVT = new DataGridView();
            this.MaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.DVT = new DataGridViewTextBoxColumn();
            this.TiLe = new DataGridViewTextBoxColumn();
            this.cboDVTHinhThuc = new ComboBox();
            this.label5 = new Label();
            this.cboLoaiThuoc = new ComboBox();
            this.lblLoaiThuoc = new Label();
            this.label6 = new Label();
            this.txtMaThuoc = new TextBox();
            ((ISupportInitialize) this.dgrDVT).BeginInit();
            base.SuspendLayout();
            this.txtThanhPhan.Location = new Point(0x60, 0x47);
            this.txtThanhPhan.Name = "txtThanhPhan";
            this.txtThanhPhan.Size = new Size(100, 20);
            this.txtThanhPhan.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 0x89);
            this.label1.Name = "label1";
            this.label1.Size = new Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nh\x00e0 sản xuất";
            this.txtHamLuong.Location = new Point(0x60, 0x68);
            this.txtHamLuong.Name = "txtHamLuong";
            this.txtHamLuong.Size = new Size(100, 20);
            this.txtHamLuong.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 0x2d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "T\x00ean Thuốc";
            this.txtNhaSanXuat.Location = new Point(0x60, 0x89);
            this.txtNhaSanXuat.Name = "txtNhaSanXuat";
            this.txtNhaSanXuat.Size = new Size(100, 20);
            this.txtNhaSanXuat.TabIndex = 4;
            this.txtTenThuoc.Location = new Point(0x60, 0x2d);
            this.txtTenThuoc.Name = "txtTenThuoc";
            this.txtTenThuoc.Size = new Size(100, 20);
            this.txtTenThuoc.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x47);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Th\x00e0nh phần";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(9, 0x68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3e, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "H\x00e0m Lượng";
            this.dgrDVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrDVT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrDVT.Columns.AddRange(new DataGridViewColumn[] { this.MaThuocTraoDoi, this.DVT, this.TiLe });
            this.dgrDVT.Location = new Point(12, 0xf4);
            this.dgrDVT.Name = "dgrDVT";
            this.dgrDVT.Size = new Size(0xb8, 0xfe);
            this.dgrDVT.TabIndex = 8;
            this.dgrDVT.DataError += new DataGridViewDataErrorEventHandler(this.dgrDVT_DataError);
            this.MaThuocTraoDoi.DataPropertyName = "MaThuocTraoDoi";
            this.MaThuocTraoDoi.HeaderText = "MaThuocTraoDoi";
            this.MaThuocTraoDoi.Name = "MaThuocTraoDoi";
            this.MaThuocTraoDoi.Visible = false;
            this.DVT.DataPropertyName = "DVT";
            this.DVT.HeaderText = "ĐVT";
            this.DVT.Name = "DVT";
            this.DVT.Width = 0x36;
            this.TiLe.DataPropertyName = "TiLe";
            this.TiLe.HeaderText = "Tỉ lệ";
            this.TiLe.Name = "TiLe";
            this.TiLe.Width = 0x34;
            this.cboDVTHinhThuc.FormattingEnabled = true;
            this.cboDVTHinhThuc.Location = new Point(0x60, 0xa4);
            this.cboDVTHinhThuc.Name = "cboDVTHinhThuc";
            this.cboDVTHinhThuc.Size = new Size(100, 0x15);
            this.cboDVTHinhThuc.TabIndex = 9;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(9, 0xa7);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4c, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "ĐVT h\x00ecnh thức";
            this.cboLoaiThuoc.FormattingEnabled = true;
            this.cboLoaiThuoc.Location = new Point(0x60, 0xc9);
            this.cboLoaiThuoc.Name = "cboLoaiThuoc";
            this.cboLoaiThuoc.Size = new Size(100, 0x15);
            this.cboLoaiThuoc.TabIndex = 11;
            this.lblLoaiThuoc.AutoSize = true;
            this.lblLoaiThuoc.Location = new Point(9, 0xcc);
            this.lblLoaiThuoc.Name = "lblLoaiThuoc";
            this.lblLoaiThuoc.Size = new Size(0x39, 13);
            this.lblLoaiThuoc.TabIndex = 12;
            this.lblLoaiThuoc.Text = "Loại thuốc";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(9, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "M\x00e3 thuốc";
            this.txtMaThuoc.Location = new Point(0x60, 0x11);
            this.txtMaThuoc.Name = "txtMaThuoc";
            this.txtMaThuoc.Size = new Size(100, 20);
            this.txtMaThuoc.TabIndex = 14;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.txtMaThuoc);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.lblLoaiThuoc);
            base.Controls.Add(this.cboLoaiThuoc);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.cboDVTHinhThuc);
            base.Controls.Add(this.dgrDVT);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtTenThuoc);
            base.Controls.Add(this.txtNhaSanXuat);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtHamLuong);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtThanhPhan);
            base.Name = "MedicineUI";
            base.Size = new Size(0xd5, 510);
            ((ISupportInitialize) this.dgrDVT).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboDVTHinhThuc, ref this._dvtSource);
            base._acFactory.EnableAutocomplete(this.DVT, ref this._dvtSource);
            base._acFactory.EnableAutocomplete(this.txtTenThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.cboLoaiThuoc, ref this._loaiThuoc);
        }

        public void loadData(MedicineInfo medicineInfo)
        {
            this.mi = medicineInfo;
            this.txtHamLuong.Text = this.mi.HamLuong;
            this.txtNhaSanXuat.Text = this.mi.NhaSanXuat;
            this.txtTenThuoc.Text = this.mi.TenThuoc;
            this.txtThanhPhan.Text = this.mi.ThanhPhan;
            if (this.IsNew)
            {
                this.txtMaThuoc.Text = IdManager.GetMissingId("MaThuoc", "Thuoc").ToString();
            }
            else
            {
                this.txtMaThuoc.Text = this.mi.MaThuoc.ToString();
            }
            this.cboDVTHinhThuc.Text = MedicineController.GetDVTHinhThuc(this.mi);
            this.cboLoaiThuoc.Text = medicineInfo.LoaiThuoc.TenLoai;
            DataTable table = new DataTable();
            table.Columns.Add("DVT");
            table.Columns.Add("TiLe");
            table.Columns.Add("MaThuocTraoDoi");
            foreach (ThuocTraoDoi doi in this.mi.ThuocTraoDois)
            {
                Nammedia.Medboss.lib.DVT dVT = doi.DVT;
                DataRow row = table.NewRow();
                row["DVT"] = dVT.TenDV;
                row["TiLe"] = doi.TiLe;
                row["MaThuocTraoDoi"] = doi.MaThuocTraoDoi;
                table.Rows.Add(row);
            }
            this.dgrDVT.DataSource = table;
            this.dgrDVT.Refresh();
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboDVTHinhThuc, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.DVT, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.txtTenThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboLoaiThuoc, ref this._loaiThuoc);
        }

        public bool IsNew
        {
            get
            {
                return this._isNew;
            }
            set
            {
                this._isNew = value;
                this.txtMaThuoc.Enabled = this._isNew;
                if (this._isNew)
                {
                    this.txtMaThuoc.Text = IdManager.GetMissingId("MaThuoc", "Thuoc").ToString();
                }
            }
        }

        private MedicineInfo Medicine
        {
            get
            {
                this.getMedicine();
                return this.mi;
            }
            set
            {
                this.mi = value;
                this.loadData(this.mi);
            }
        }
    }
}
