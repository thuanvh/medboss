namespace Nammedia.Medboss.controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ThanhToan : UserControl
    {
        private IContainer components = null;

        public ThanhToan()
        {
            this.InitializeComponent();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "ThanhToan";
            base.Size = new Size(590, 0x147);
            base.ResumeLayout(false);
        }
    }
}
