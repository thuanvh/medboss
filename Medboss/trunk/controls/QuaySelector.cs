namespace Nammedia.Medboss.controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QuaySelector : MedUIBase
    {
        private ComboBox cboQuay;
        private IContainer components = null;
        private GroupBox grpChonQuay;
        private Label lblQuay;
        private RadioButton rdbCacQuay;
        private RadioButton rdbMotQuay;

        public QuaySelector()
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

        public string getQuay()
        {
            if (this.getType() == QuayParameter.MotQuay)
            {
                return this.cboQuay.Text;
            }
            return "*";
        }

        public QuayParameter getType()
        {
            foreach (Control control in this.grpChonQuay.Controls)
            {
                if (control.GetType() == typeof(RadioButton))
                {
                    RadioButton button = (RadioButton) control;
                    if (button.Checked)
                    {
                        if (button.Name == this.rdbCacQuay.Name)
                        {
                            return QuayParameter.NhieuQuay;
                        }
                        return QuayParameter.MotQuay;
                    }
                }
            }
            return QuayParameter.MotQuay;
        }

        private void InitializeComponent()
        {
            this.cboQuay = new ComboBox();
            this.lblQuay = new Label();
            this.rdbMotQuay = new RadioButton();
            this.rdbCacQuay = new RadioButton();
            this.grpChonQuay = new GroupBox();
            this.grpChonQuay.SuspendLayout();
            base.SuspendLayout();
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x2c, 0x22);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(0x41, 0x15);
            this.cboQuay.TabIndex = 13;
            this.lblQuay.AutoSize = true;
            this.lblQuay.Location = new Point(6, 0x25);
            this.lblQuay.Name = "lblQuay";
            this.lblQuay.Size = new Size(0x20, 13);
            this.lblQuay.TabIndex = 12;
            this.lblQuay.Text = "Quầy";
            this.rdbMotQuay.AutoSize = true;
            this.rdbMotQuay.Checked = true;
            this.rdbMotQuay.Location = new Point(6, 0x11);
            this.rdbMotQuay.Name = "rdbMotQuay";
            this.rdbMotQuay.Size = new Size(0x45, 0x11);
            this.rdbMotQuay.TabIndex = 14;
            this.rdbMotQuay.TabStop = true;
            this.rdbMotQuay.Text = "Một quầy";
            this.rdbMotQuay.UseVisualStyleBackColor = true;
            this.rdbMotQuay.CheckedChanged += new EventHandler(this.rdbMotQuay_CheckedChanged);
            this.rdbCacQuay.AutoSize = true;
            this.rdbCacQuay.Location = new Point(6, 0x3d);
            this.rdbCacQuay.Name = "rdbCacQuay";
            this.rdbCacQuay.Size = new Size(0x67, 0x11);
            this.rdbCacQuay.TabIndex = 15;
            this.rdbCacQuay.Text = "Tất cả c\x00e1c quầy";
            this.rdbCacQuay.UseVisualStyleBackColor = true;
            this.rdbCacQuay.CheckedChanged += new EventHandler(this.rdbCacQuay_CheckedChanged);
            this.grpChonQuay.Controls.Add(this.rdbCacQuay);
            this.grpChonQuay.Controls.Add(this.lblQuay);
            this.grpChonQuay.Controls.Add(this.rdbMotQuay);
            this.grpChonQuay.Controls.Add(this.cboQuay);
            this.grpChonQuay.Dock = DockStyle.Fill;
            this.grpChonQuay.Location = new Point(0, 0);
            this.grpChonQuay.Name = "grpChonQuay";
            this.grpChonQuay.Size = new Size(0x74, 0x5c);
            this.grpChonQuay.TabIndex = 0x10;
            this.grpChonQuay.TabStop = false;
            this.grpChonQuay.Text = "Quầy";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.grpChonQuay);
            base.Name = "QuaySelector";
            base.Size = new Size(0x74, 0x5c);
            this.grpChonQuay.ResumeLayout(false);
            this.grpChonQuay.PerformLayout();
            base.ResumeLayout(false);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
        }

        private void rdbCacQuay_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdbMotQuay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbMotQuay.Checked)
            {
                this.cboQuay.Enabled = true;
            }
            else
            {
                this.cboQuay.Enabled = false;
            }
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
        }

        public void setQuay(string quay)
        {
            this.cboQuay.Text = quay;
        }

        public void setType(QuayParameter quayParameter)
        {
            if (quayParameter == QuayParameter.NhieuQuay)
            {
                this.rdbCacQuay.Checked = true;
            }
            else if (quayParameter == QuayParameter.MotQuay)
            {
                this.rdbMotQuay.Checked = true;
            }
        }

        public string Title
        {
            get
            {
                return this.grpChonQuay.Text;
            }
            set
            {
                this.grpChonQuay.Text = value;
            }
        }
    }
}
