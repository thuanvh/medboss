using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class CSInfoUI : MedUIBase
    {
        private CSInfo _csInfo;
        private ComboBox cboCsTypes;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtChucVu;
        private TextBox txtCongTy;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private TextBox txtTen;

        public CSInfoUI()
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
        }

        public CSInfoUI(CSInfo ci)
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
            this.loadCSInfo(ci);
        }

        public CSInfoUI(int MaKH)
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
            CSInfo csi = new CSController().GetById(MaKH);
            this.loadCSInfo(csi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public CSInfo getCSInfo()
        {
            this._csInfo.ChucVu = this.txtChucVu.Text;
            this._csInfo.CongTy = this.txtCongTy.Text;
            this._csInfo.DienThoai = this.txtDienThoai.Text;
            this._csInfo.Ten = this.txtTen.Text;
            this._csInfo.DiaChi = this.txtDiaChi.Text;
            this._csInfo.LoaiKhachHang = (LoaiKhachHang) this.cboCsTypes.SelectedItem;
            return this._csInfo;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.txtTen = new TextBox();
            this.txtCongTy = new TextBox();
            this.label2 = new Label();
            this.txtDiaChi = new TextBox();
            this.label3 = new Label();
            this.txtChucVu = new TextBox();
            this.label4 = new Label();
            this.txtDienThoai = new TextBox();
            this.label5 = new Label();
            this.cboCsTypes = new ComboBox();
            this.label6 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1a, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "T\x00ean";
            this.txtTen.Location = new Point(0x31, 8);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new Size(0x87, 20);
            this.txtTen.TabIndex = 1;
            this.txtCongTy.Location = new Point(0x49, 60);
            this.txtCongTy.Name = "txtCongTy";
            this.txtCongTy.Size = new Size(0x6f, 20);
            this.txtCongTy.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 0x3f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "T\x00ean c\x00f4ng ty";
            this.txtDiaChi.Location = new Point(0x31, 0x79);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new Size(0x87, 0x2f);
            this.txtDiaChi.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 0x79);
            this.label3.Name = "label3";
            this.label3.Size = new Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Địa chỉ";
            this.txtChucVu.Location = new Point(0x38, 0x22);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new Size(0x80, 20);
            this.txtChucVu.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 0x22);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2f, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chức vụ";
            this.txtDienThoai.Location = new Point(0x49, 0x5d);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new Size(0x6f, 20);
            this.txtDienThoai.TabIndex = 9;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 0x60);
            this.label5.Name = "label5";
            this.label5.Size = new Size(70, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Số điện thoại";
            this.cboCsTypes.Location = new Point(0x49, 0xae);
            this.cboCsTypes.Name = "cboCsTypes";
            this.cboCsTypes.Size = new Size(0x6f, 0x15);
            this.cboCsTypes.TabIndex = 10;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0xb1);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Nh\x00f3m";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label6);
            base.Controls.Add(this.cboCsTypes);
            base.Controls.Add(this.txtDienThoai);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtChucVu);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtDiaChi);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtCongTy);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtTen);
            base.Controls.Add(this.label1);
            base.Name = "CSInfoUI";
            base.Size = new Size(0xc3, 0xd4);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboCsTypes, ref this._nhomCS);
        }

        public void loadCSInfo(CSInfo csi)
        {
            this._csInfo = csi;
            this.txtChucVu.Text = csi.ChucVu;
            this.txtCongTy.Text = csi.CongTy;
            this.txtDienThoai.Text = csi.DienThoai;
            this.txtTen.Text = csi.Ten;
            this.txtDiaChi.Text = csi.DiaChi;
            this.cboCsTypes.ValueMember = "TenLoai";
            if (csi.LoaiKhachHang != null)
            {
                this.cboCsTypes.SelectedValue = csi.LoaiKhachHang.TenLoai;
            }
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboCsTypes, ref this._nhomCS);
        }

        private CSInfo CS
        {
            get
            {
                return this.getCSInfo();
            }
            set
            {
                this.loadCSInfo(value);
            }
        }
    }
}
