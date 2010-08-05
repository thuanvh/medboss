namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QuayUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private IContainer components;
        private int dataid;
        private ArrayList quayArray;
        public QuayUI quayUI;

        public QuayUIOperator()
        {
            this.components = null;
            this.InitializeComponent();
            this.butThem.Visible = true;
            this.butSua.Visible = this.butXoa.Visible = false;
        }

        public QuayUIOperator(OperatorType type)
        {
            this.components = null;
            this.InitializeComponent();
            switch (type)
            {
                case OperatorType.Insert:
                    this.butThem.Visible = true;
                    this.butSua.Visible = this.butXoa.Visible = false;
                    break;

                case OperatorType.Update:
                    this.butSua.Visible = true;
                    this.butXoa.Visible = this.butThem.Visible = false;
                    break;

                case OperatorType.Delete:
                    this.butXoa.Visible = true;
                    this.butSua.Visible = this.butThem.Visible = false;
                    break;
            }
        }

        private void butThemQuay_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        public override void Clear()
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

        private void initBindQuayData()
        {
            this.quayArray = new QuayController().List();
        }

        private void InitializeComponent()
        {
            QuayInfo info = new QuayInfo();
            this.butXoa = new Button();
            this.butThem = new Button();
            this.butSua = new Button();
            this.quayUI = new QuayUI();
            base.SuspendLayout();
            this.butXoa.Location = new Point(0x7a, 0x2c);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x24, 0x17);
            this.butXoa.TabIndex = 8;
            this.butXoa.Text = "Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butThem.Location = new Point(3, 0x2c);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x3e, 0x17);
            this.butThem.TabIndex = 5;
            this.butThem.Text = "Th\x00eam mới";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThemQuay_Click);
            this.butSua.Location = new Point(0x47, 0x2c);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x2d, 0x17);
            this.butSua.TabIndex = 7;
            this.butSua.Text = "Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.quayUI.Location = new Point(3, 11);
            this.quayUI.Name = "quayUI1";
            info.MaQuay = 0;
            info.TenQuay = "";
            this.quayUI.Quay = info;
            this.quayUI.Size = new Size(0x9d, 0x1b);
            this.quayUI.TabIndex = 9;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.quayUI);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.butThem);
            base.Name = "QuayUIOperator";
            base.Size = new Size(0xbd, 70);
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            if (this.quayUI.txtTenQuay.Text == "")
            {
                throw new UnNullValueException("T\x00ean quầy");
            }
            this.dataid = new QuayController().Insert(this.quayUI.Quay);
            return this.dataid;
        }

        private void RefreshQuayData()
        {
            this.quayArray = new QuayController().List();
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
                return Nammedia.Medboss.controls.DataType.Quay;
            }
        }
    }
}
