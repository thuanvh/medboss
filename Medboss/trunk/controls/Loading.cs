namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Loading : Form, IRefresh
    {
        private bool _closed = false;
        private IContainer components = null;
        private Label label1;

        public Loading()
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
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Image = Resources.loader;
            this.label1.ImageAlign = ContentAlignment.MiddleLeft;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xc9, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "        Medboss is loading the initial data ...";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x120, 0x26);
            base.ControlBox = false;
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Loading";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void Refresh()
        {
            this.IsClosed = true;
            base.DialogResult = DialogResult.Yes;
        }

        public bool IsClosed
        {
            get
            {
                return this._closed;
            }
            set
            {
                this._closed = value;
            }
        }
    }
}
