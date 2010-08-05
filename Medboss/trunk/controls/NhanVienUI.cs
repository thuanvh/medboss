using Nammedia.Medboss.lib;
using Nammedia.Medboss.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class NhanVienUI : MedUIBase
    {
        private NhanVienInfo _nhanVienInfo = new NhanVienInfo();
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtCMT;
        private TextBox txtGhiChu;
        private TextBox txtHo;
        private TextBox txtTen;

        public NhanVienUI()
        {
            this.InitializeComponent();
            this.loadAC();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public NhanVienInfo getNhanVienInfo()
        {
            this._nhanVienInfo.SoCMT = ConvertHelper.getInt(this.txtCMT.Text);
            this._nhanVienInfo.Ten = ConvertHelper.getString(this.txtTen.Text);
            this._nhanVienInfo.HoVaTenDem = ConvertHelper.getString(this.txtHo.Text);
            this._nhanVienInfo.GhiChu = ConvertHelper.getString(this.txtGhiChu.Text);
            return this._nhanVienInfo;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtCMT = new TextBox();
            this.txtGhiChu = new TextBox();
            this.txtHo = new TextBox();
            this.txtTen = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1a, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "T\x00ean";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4e, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ v\x00e0 t\x00ean đệm";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(13, 0x45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2e, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Số CMT";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(13, 0x5f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2c, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ghi ch\x00fa";
            this.txtCMT.Location = new Point(0x69, 0x41);
            this.txtCMT.Name = "txtCMT";
            this.txtCMT.Size = new Size(0x73, 20);
            this.txtCMT.TabIndex = 7;
            this.txtGhiChu.Location = new Point(0x4f, 0x5b);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(0x8d, 0x3d);
            this.txtGhiChu.TabIndex = 8;
            this.txtHo.Location = new Point(0x69, 0x27);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new Size(0x73, 20);
            this.txtHo.TabIndex = 9;
            this.txtTen.Location = new Point(0x69, 13);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new Size(0x73, 20);
            this.txtTen.TabIndex = 10;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtTen);
            base.Controls.Add(this.txtHo);
            base.Controls.Add(this.txtGhiChu);
            base.Controls.Add(this.txtCMT);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "NhanVienUI";
            base.Size = new Size(0xef, 0xa8);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtTen, ref this._nhanVienSource);
        }

        public void loadInfo(NhanVienInfo nhanVienInfo)
        {
            this.txtGhiChu.Text = nhanVienInfo.GhiChu;
            this.txtHo.Text = nhanVienInfo.HoVaTenDem;
            this.txtCMT.Text = nhanVienInfo.SoCMT.ToString();
            this.txtTen.Text = nhanVienInfo.Ten;
            this._nhanVienInfo = nhanVienInfo;
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtTen, ref this._nhanVienSource);
        }

        private NhanVienInfo NhanVien
        {
            get
            {
                return this.getNhanVienInfo();
            }
            set
            {
                this.loadInfo(value);
            }
        }
    }
}
