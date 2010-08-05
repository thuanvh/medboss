namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CSInfoUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private IContainer components;
        private CSInfoUI csuiInfo;
        private int dataid;
        private int MaKH;

        public CSInfoUIOperator()
        {
            this.components = null;
            this.InitializeComponent();
        }

        public CSInfoUIOperator(CSInfo csi)
        {
            this.components = null;
            this.InitializeComponent();
            this.csuiInfo.loadCSInfo(csi);
            this.MaKH = csi.MaKhachHang;
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        public override void Clear()
        {
        }

        private void CSInfoUIOperator_Load(object sender, EventArgs e)
        {
        }

        private void csuiInfo_Load(object sender, EventArgs e)
        {
        }

        protected override int delete()
        {
            this.dataid = new CSController().Delete(this.MaKH);
            Program.ACSource.RefreshCSSource();
            return this.dataid;
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
            this.butThem = new Button();
            this.csuiInfo = new CSInfoUI();
            this.butXoa = new Button();
            this.butSua = new Button();
            base.SuspendLayout();
            this.butThem.Location = new Point(0, 210);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x3a, 0x17);
            this.butThem.TabIndex = 1;
            this.butThem.Text = "T&h\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.Insert);
            this.csuiInfo.Location = new Point(0, 3);
            this.csuiInfo.Name = "csuiInfo";
            this.csuiInfo.Size = new Size(190, 0xcb);
            this.csuiInfo.TabIndex = 0;
            this.csuiInfo.Load += new EventHandler(this.csuiInfo_Load);
            this.butXoa.Location = new Point(0x8e, 210);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x30, 0x17);
            this.butXoa.TabIndex = 2;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.butSua.Location = new Point(80, 210);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x2e, 0x17);
            this.butSua.TabIndex = 3;
            this.butSua.Text = "&Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.csuiInfo);
            base.Name = "CSInfoUIOperator";
            base.Size = new Size(0xe2, 0xec);
            base.Load += new EventHandler(this.CSInfoUIOperator_Load);
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            CSController controller = new CSController();
            CSInfo khachHang = this.csuiInfo.getCSInfo();
            return (this.dataid = controller.Insert(khachHang));
        }

        private void Insert(object sender, EventArgs e)
        {
            base.Insert();
        }

        public void loadCSInfo(CSInfo csi)
        {
            this.csuiInfo.loadCSInfo(csi);
            this.MaKH = csi.MaKhachHang;
        }

        protected override int update()
        {
            this.dataid = new CSController().Update(this.csuiInfo.getCSInfo());
            Program.ACSource.RefreshCSSource();
            return this.dataid;
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
                return Nammedia.Medboss.controls.DataType.KhachHang;
            }
        }
    }
}
