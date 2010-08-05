using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class NhanVienUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private IContainer components;
        private int dataid;
        private NhanVienUI nvui;

        public NhanVienUIOperator()
        {
            this.components = null;
            this.InitializeComponent();
        }

        public NhanVienUIOperator(NhanVienInfo nvi)
        {
            this.components = null;
            this.InitializeComponent();
            this.nvui.loadInfo(nvi);
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        public override void Clear()
        {
        }

        protected override int delete()
        {
            NhanVienController controller = new NhanVienController();
            NhanVienInfo info = this.nvui.getNhanVienInfo();
            return (this.dataid = controller.Delete(info.MaNhanVien));
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
            this.nvui = new NhanVienUI();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.butThem = new Button();
            base.SuspendLayout();
            this.nvui.Location = new Point(0, 0);
            this.nvui.Name = "nvui";
            this.nvui.Size = new Size(0xe8, 0xa8);
            this.nvui.TabIndex = 0;
            this.butSua.Location = new Point(60, 0xae);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x2b, 0x17);
            this.butSua.TabIndex = 1;
            this.butSua.Text = "&Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Visible = false;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.butXoa.Location = new Point(0x6d, 0xae);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x34, 0x17);
            this.butXoa.TabIndex = 2;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Visible = false;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.butThem.Location = new Point(3, 0xae);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x33, 0x17);
            this.butThem.TabIndex = 3;
            this.butThem.Text = "T&h\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.nvui);
            base.Name = "NhanVienUIOperator";
            base.Size = new Size(0xff, 0xd6);
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            NhanVienInfo nhanvien = this.nvui.getNhanVienInfo();
            this.dataid = new NhanVienController().Insert(nhanvien);
            return this.dataid;
        }

        public void loadNhanVienInfo(NhanVienInfo nvi)
        {
            this.nvui.loadInfo(nvi);
            this.butSua.Visible = true;
            this.butXoa.Visible = false;
        }

        protected override int update()
        {
            NhanVienInfo nhanvien = this.nvui.getNhanVienInfo();
            NhanVienController controller = new NhanVienController();
            return (this.dataid = controller.Update(nhanvien));
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
                return Nammedia.Medboss.controls.DataType.NhanVien;
            }
        }
    }
}
