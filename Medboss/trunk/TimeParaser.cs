namespace Nammedia.Medboss
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TimeParaser : UserControl
    {
        private DateTimePicker cboDenNgay;
        private DateTimePicker cboNgay;
        private DateTimePicker cboTuNgay;
        private IContainer components = null;
        private GroupBox grpThoiGian;
        private Label label2;
        private Label label3;
        private RadioButton rdbHomnay;
        private RadioButton rdbKhoang;
        private RadioButton rdbMotNgay;

        public TimeParaser()
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

        public DateTime getDenNgay()
        {
            if (this.getType() == TimeParameter.Duration)
            {
                return this.cboDenNgay.Value;
            }
            if (this.getType() == TimeParameter.OneDay)
            {
                return this.cboNgay.Value;
            }
            return DateTime.Now;
        }

        public DateTime getTuNgay()
        {
            if (this.getType() == TimeParameter.Today)
            {
                return DateTime.Now;
            }
            if (this.getType() == TimeParameter.OneDay)
            {
                return this.cboNgay.Value;
            }
            return this.cboTuNgay.Value;
        }

        public TimeParameter getType()
        {
            foreach (Control control in this.grpThoiGian.Controls)
            {
                if (control.GetType() == typeof(RadioButton))
                {
                    RadioButton button = (RadioButton) control;
                    if (button.Checked)
                    {
                        if (button.Name == this.rdbHomnay.Name)
                        {
                            return TimeParameter.Today;
                        }
                        if (button.Name == this.rdbKhoang.Name)
                        {
                            return TimeParameter.Duration;
                        }
                        return TimeParameter.OneDay;
                    }
                }
            }
            return TimeParameter.OneDay;
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.label3 = new Label();
            this.rdbMotNgay = new RadioButton();
            this.rdbKhoang = new RadioButton();
            this.rdbHomnay = new RadioButton();
            this.grpThoiGian = new GroupBox();
            this.cboNgay = new DateTimePicker();
            this.cboTuNgay = new DateTimePicker();
            this.cboDenNgay = new DateTimePicker();
            this.grpThoiGian.SuspendLayout();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x12, 0x44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2e, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Từ ng\x00e0y";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x98, 0x44);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Đến ng\x00e0y";
            this.rdbMotNgay.AutoSize = true;
            this.rdbMotNgay.Location = new Point(0x11, 0x13);
            this.rdbMotNgay.Name = "rdbMotNgay";
            this.rdbMotNgay.Size = new Size(0x45, 0x11);
            this.rdbMotNgay.TabIndex = 6;
            this.rdbMotNgay.Text = "Một ng\x00e0y";
            this.rdbMotNgay.UseVisualStyleBackColor = true;
            this.rdbMotNgay.CheckedChanged += new EventHandler(this.rdbMotNgay_CheckedChanged);
            this.rdbKhoang.AutoSize = true;
            this.rdbKhoang.Location = new Point(0x11, 0x2a);
            this.rdbKhoang.Name = "rdbKhoang";
            this.rdbKhoang.Size = new Size(0x3e, 0x11);
            this.rdbKhoang.TabIndex = 7;
            this.rdbKhoang.Text = "Khoảng";
            this.rdbKhoang.UseVisualStyleBackColor = true;
            this.rdbKhoang.CheckedChanged += new EventHandler(this.rdbKhoang_CheckedChanged);
            this.rdbHomnay.AutoSize = true;
            this.rdbHomnay.Checked = true;
            this.rdbHomnay.Location = new Point(0x11, 0x5b);
            this.rdbHomnay.Name = "rdbHomnay";
            this.rdbHomnay.Size = new Size(0x43, 0x11);
            this.rdbHomnay.TabIndex = 8;
            this.rdbHomnay.TabStop = true;
            this.rdbHomnay.Text = "H\x00f4m nay";
            this.rdbHomnay.UseVisualStyleBackColor = true;
            this.grpThoiGian.Controls.Add(this.cboDenNgay);
            this.grpThoiGian.Controls.Add(this.cboTuNgay);
            this.grpThoiGian.Controls.Add(this.cboNgay);
            this.grpThoiGian.Controls.Add(this.rdbKhoang);
            this.grpThoiGian.Controls.Add(this.rdbHomnay);
            this.grpThoiGian.Controls.Add(this.rdbMotNgay);
            this.grpThoiGian.Controls.Add(this.label2);
            this.grpThoiGian.Controls.Add(this.label3);
            this.grpThoiGian.Dock = DockStyle.Fill;
            this.grpThoiGian.Location = new Point(0, 0);
            this.grpThoiGian.Name = "grpThoiGian";
            this.grpThoiGian.Size = new Size(0x13b, 0x7a);
            this.grpThoiGian.TabIndex = 9;
            this.grpThoiGian.TabStop = false;
            this.grpThoiGian.Text = "Thời gian";
            this.cboNgay.CustomFormat = "dd/MM/yyyy";
            this.cboNgay.Format = DateTimePickerFormat.Custom;
            this.cboNgay.Location = new Point(0x5c, 0x10);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Size = new Size(0x5e, 20);
            this.cboNgay.TabIndex = 9;
            this.cboTuNgay.CustomFormat = "dd/MM/yyyy";
            this.cboTuNgay.Format = DateTimePickerFormat.Custom;
            this.cboTuNgay.Location = new Point(70, 0x40);
            this.cboTuNgay.Name = "cboTuNgay";
            this.cboTuNgay.Size = new Size(0x54, 20);
            this.cboTuNgay.TabIndex = 10;
            this.cboDenNgay.CustomFormat = "dd/MM/yyyy";
            this.cboDenNgay.Format = DateTimePickerFormat.Custom;
            this.cboDenNgay.Location = new Point(0xd0, 0x40);
            this.cboDenNgay.Name = "cboDenNgay";
            this.cboDenNgay.Size = new Size(0x65, 20);
            this.cboDenNgay.TabIndex = 11;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.grpThoiGian);
            base.Name = "TimeParaser";
            base.Size = new Size(0x13b, 0x7a);
            this.grpThoiGian.ResumeLayout(false);
            this.grpThoiGian.PerformLayout();
            base.ResumeLayout(false);
        }

        private void rdbKhoang_CheckedChanged(object sender, EventArgs e)
        {
            this.cboDenNgay.Enabled = this.rdbKhoang.Checked;
            this.cboTuNgay.Enabled = this.rdbKhoang.Checked;
        }

        private void rdbMotNgay_CheckedChanged(object sender, EventArgs e)
        {
            this.cboNgay.Enabled = this.rdbMotNgay.Checked;
        }

        public void setDenNgay(DateTime denNgay)
        {
            this.cboDenNgay.Value = denNgay;
        }

        public void setTuNgay(DateTime tuNgay)
        {
            if (this.getType() == TimeParameter.OneDay)
            {
                this.cboNgay.Value = tuNgay;
            }
            if (this.getType() == TimeParameter.Duration)
            {
                this.cboTuNgay.Value = tuNgay;
            }
        }

        public void setType(TimeParameter timeParameter)
        {
            if (timeParameter == TimeParameter.Today)
            {
                this.rdbHomnay.Checked = true;
            }
            else if (timeParameter == TimeParameter.Duration)
            {
                this.rdbKhoang.Checked = true;
            }
            else if (timeParameter == TimeParameter.OneDay)
            {
                this.rdbMotNgay.Checked = true;
            }
        }
    }
}
