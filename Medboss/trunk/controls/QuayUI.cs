namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QuayUI : MedUIBase
    {
        private IContainer components;
        private Label label1;
        public TextBox txtTenQuay;

        public QuayUI()
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
        }

        public QuayUI(QuayInfo qi)
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
            this.txtTenQuay.Text = qi.TenQuay;
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
            this.label1 = new Label();
            this.txtTenQuay = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quầy";
            this.txtTenQuay.Location = new Point(0x34, 3);
            this.txtTenQuay.Name = "txtTenQuay";
            this.txtTenQuay.Size = new Size(100, 20);
            this.txtTenQuay.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtTenQuay);
            base.Controls.Add(this.label1);
            base.Name = "QuayUI";
            base.Size = new Size(0xfc, 0x55);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.txtTenQuay, ref this._quaySource);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.txtTenQuay, ref this._quaySource);
        }

        public QuayInfo Quay
        {
            get
            {
                QuayInfo info = new QuayInfo();
                info.TenQuay = this.txtTenQuay.Text;
                return info;
            }
            set
            {
                QuayInfo info = value;
                this.txtTenQuay.Text = info.TenQuay;
            }
        }
    }
}
