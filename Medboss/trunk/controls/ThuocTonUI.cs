using Nammedia.Medboss.lib;
using Nammedia.Medboss.Utils;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class ThuocTonUI : OperatorBase
    {
        private Button butChoKK;
        private Button butLoad;
        private Button butThem;
        private ComboBox cboQuay;
        private ComboBox cboThuoc;
        private IContainer components = null;
        private int dataid;
        private DateTimePicker dateNgay;
        private KiemKeUI kiemKeUI1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private DataTable table = new DataTable();
        private TableLayoutPanel tableLayoutPanel1;

        public ThuocTonUI()
        {
            this.InitializeComponent();
            this.loadAC();
        }

        private void butChoKK_Click(object sender, EventArgs e)
        {
            if (this.table.Rows.Count > 0)
            {
                int num;
                DataGridViewRow row;
                ArrayList medicine;
                MedicineInfo info;
                DataGridView dgrKiemKeChiTiet = this.kiemKeUI1.dgrKiemKeChiTiet;
                DataRow[] rowArray = this.table.Select("TenThuoc='" + this.cboThuoc.Text + "'");
                if (rowArray.Length > 0)
                {
                    num = dgrKiemKeChiTiet.Rows.Add();
                    row = dgrKiemKeChiTiet.Rows[num];
                    row.Cells[this.kiemKeUI1.ColumnThuoc.Index].Value = rowArray[0]["TenThuoc"];
                    row.Cells[this.kiemKeUI1.ColumnDVT.Index].Value = rowArray[0]["DVT"];
                    row.Cells[this.kiemKeUI1.ColumnSoLuong.Index].Value = rowArray[0]["SoLuong"];
                    row.Cells[this.kiemKeUI1.ColumnDonGiaBan.Index].Value = rowArray[0]["DonGiaBan"];
                    row.Cells[this.kiemKeUI1.ColumnDonGiaNhap.Index].Value = rowArray[0]["DonGiaNhap"];
                    row.Cells[this.kiemKeUI1.ColumnHanDung.Index].Value = rowArray[0]["HanDung"];
                    medicine = new MedicineController().GetMedicine(ConvertHelper.getString(rowArray[0]["TenThuoc"]));
                    if (medicine.Count > 0)
                    {
                        info = (MedicineInfo) medicine[0];
                        row.Cells[this.kiemKeUI1.ColumnMaThuoc.Index].Value = info.MaThuoc;
                    }
                }
                else
                {
                    num = dgrKiemKeChiTiet.Rows.Add();
                    row = dgrKiemKeChiTiet.Rows[num];
                    MedicineController controller = new MedicineController();
                    medicine = controller.GetMedicine(this.cboThuoc.Text);
                    info = new MedicineInfo();
                    if (medicine.Count > 0)
                    {
                        info = (MedicineInfo) medicine[0];
                        ThuocTraoDoi doi = (ThuocTraoDoi) info.ThuocTraoDois[0];
                        int maQuay = new QuayController().getMaQuay(this.cboQuay.Text);
                        row.Cells[this.kiemKeUI1.ColumnDVT.Index].Value = doi.DVT.TenDV;
                        row.Cells[this.kiemKeUI1.ColumnThuoc.Index].Value = this.cboThuoc.Text;
                        row.Cells[this.kiemKeUI1.ColumnSoLuong.Index].Value = 0;
                        row.Cells[this.kiemKeUI1.ColumnDonGiaBan.Index].Value = controller.getDonGia(doi.MaThuocTraoDoi, maQuay);
                        row.Cells[this.kiemKeUI1.ColumnMaThuoc.Index].Value = info.MaThuoc;
                        HoaDonNhapThuocInfo info2 = new HoaDonNhapThuocController().getLatestImport(info.TenThuoc, doi.DVT.TenDV);
                        if (info2.HoaDonChiTiet.Count > 0)
                        {
                            row.Cells[this.kiemKeUI1.ColumnHanDung.Index].Value = ((NhapThuocChiTietInfo) info2.HoaDonChiTiet[0]).HanDung.ToString("MM/yy");
                            row.Cells[this.kiemKeUI1.ColumnDonGiaNhap.Index].Value = ConvertHelper.formatNumber(((NhapThuocChiTietInfo) info2.HoaDonChiTiet[0]).DonGiaNhap);
                        }
                    }
                }
                this.cboThuoc.Focus();
            }
        }

        private void butLoad_Click(object sender, EventArgs e)
        {
            this.table = new MedicineController().getMedicineTon(this.dateNgay.Value, this.cboQuay.Text);
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboThuoc_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        public override void Clear()
        {
            this.kiemKeUI1.clear();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.butChoKK = new Button();
            this.label3 = new Label();
            this.cboThuoc = new ComboBox();
            this.butLoad = new Button();
            this.label2 = new Label();
            this.cboQuay = new ComboBox();
            this.label1 = new Label();
            this.dateNgay = new DateTimePicker();
            this.kiemKeUI1 = new KiemKeUI();
            this.butThem = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kiemKeUI1, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x2fb, 0x221);
            this.tableLayoutPanel1.TabIndex = 1;
            this.panel1.Controls.Add(this.butThem);
            this.panel1.Controls.Add(this.butChoKK);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboThuoc);
            this.panel1.Controls.Add(this.butLoad);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboQuay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateNgay);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2f5, 0x45);
            this.panel1.TabIndex = 1;
            this.butChoKK.Location = new Point(0x1a7, 0x22);
            this.butChoKK.Name = "butChoKK";
            this.butChoKK.Size = new Size(0x8a, 0x17);
            this.butChoKK.TabIndex = 7;
            this.butChoKK.Text = "&Cho v\x00e0o kiểm k\x00ea";
            this.butChoKK.UseVisualStyleBackColor = true;
            this.butChoKK.Click += new EventHandler(this.butChoKK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(11, 0x21);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "T\x00ean thuốc";
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new Point(0x49, 0x21);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new Size(0x149, 0x15);
            this.cboThuoc.TabIndex = 5;
            this.cboThuoc.SelectionChangeCommitted += new EventHandler(this.cboThuoc_SelectionChangeCommitted);
            this.cboThuoc.SelectedIndexChanged += new EventHandler(this.cboThuoc_SelectedIndexChanged);
            this.butLoad.Location = new Point(0x1a7, 5);
            this.butLoad.Name = "butLoad";
            this.butLoad.Size = new Size(0x8a, 0x17);
            this.butLoad.TabIndex = 4;
            this.butLoad.Text = "Tải &h\x00e0ng tồn";
            this.butLoad.UseVisualStyleBackColor = true;
            this.butLoad.Click += new EventHandler(this.butLoad_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xd6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quầy";
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0xfc, 6);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(150, 0x15);
            this.cboQuay.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ng\x00e0y";
            this.dateNgay.CustomFormat = "dd/MM/yyyy";
            this.dateNgay.Format = DateTimePickerFormat.Custom;
            this.dateNgay.Location = new Point(0x49, 7);
            this.dateNgay.Name = "dateNgay";
            this.dateNgay.Size = new Size(0x87, 20);
            this.dateNgay.TabIndex = 0;
            this.kiemKeUI1.Dock = DockStyle.Fill;
            this.kiemKeUI1.Location = new Point(3, 0x4e);
            this.kiemKeUI1.Name = "kiemKeUI1";
            this.kiemKeUI1.Size = new Size(0x2f5, 0x1d0);
            this.kiemKeUI1.TabIndex = 9;
            this.butThem.Location = new Point(0x286, 0x22);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 0;
            this.butThem.Text = "&Lưu kiểm k\x00ea";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ThuocTonUI";
            base.Size = new Size(0x2fb, 0x221);
            base.Load += new EventHandler(this.ThuocTonUI_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            KiemKeInfo ki = this.kiemKeUI1.getKiemKeInfo();
            KiemKeController controller = new KiemKeController();
            return (this.dataid = controller.Insert(ki));
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
            base._acFactory.EnableAutocomplete(this.cboThuoc, ref this._medSource);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
            base._acFactory.RefreshAutoCompleteSource(this.cboThuoc, ref this._medSource);
        }

        private void ThuocTonUI_Load(object sender, EventArgs e)
        {
            this.dateNgay.Value = DateTime.Now;
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
                return Nammedia.Medboss.controls.DataType.ThuocTon;
            }
        }
    }
}
