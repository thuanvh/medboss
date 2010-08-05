namespace Nammedia.Medboss
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Operator : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;

        public Operator()
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
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            base.SuspendLayout();
            this.button1.Location = new Point(12, 15);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.Location = new Point(0x6a, 15);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button3.Location = new Point(0xcd, 15);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Name = "Operator";
            base.Size = new Size(0x12e, 0x33);
            base.ResumeLayout(false);
        }
    }
}
