namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class AboutBox1 : Form
    {
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button okButton;
        private PictureBox pictureBox1;
        private TextBox textBoxDescription;

        public AboutBox1()
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AboutBox1));
            this.textBoxDescription = new TextBox();
            this.okButton = new Button();
            this.pictureBox1 = new PictureBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.textBoxDescription.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBoxDescription.Location = new Point(0xd4, 0x55);
            this.textBoxDescription.Margin = new Padding(6, 3, 3, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new Size(0x13c, 0x44);
            this.textBoxDescription.TabIndex = 0x17;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Chương tr\x00ecnh quản l\x00fd thuốc Medboss 0.1 Beta 1\r\nĐ\x00e2y l\x00e0 bản thử nghiệm, v\x00ec thế ch\x00fang t\x00f4i kh\x00f4ng chịu tr\x00e1ch nhiệm về bất cứ thiệt hại n\x00e0o.\r\n\r\n";
            this.okButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.okButton.DialogResult = DialogResult.Cancel;
            this.okButton.Location = new Point(0x1d3, 0xa6);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(0x3e, 0x1b);
            this.okButton.TabIndex = 0x18;
            this.okButton.Text = "&OK";
            this.okButton.Click += new EventHandler(this.okButton_Click);
            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new Point(2, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x1a5, 0x40);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0x19;
            this.pictureBox1.TabStop = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0xa6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1b2, 0x1a);
            this.label1.TabIndex = 0x1a;
            this.label1.Text = "Chương tr\x00ecnh được bảo vệ bởi luật bản quyền vả sở hữu tr\x00ed tuệ. \r\nBất kỳ h\x00ecnh thức sao ch\x00e9p v\x00e0 ph\x00e2n phối bất hợp ph\x00e1p chương tr\x00ecnh đều kh\x00f4ng được ph\x00e9p\r\n";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(12, 0x56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x7e, 0x10);
            this.label2.TabIndex = 0x1b;
            this.label2.Text = "Medboss 0.1 Beta 1";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(12, 0x66);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xb3, 0x10);
            this.label3.TabIndex = 0x1c;
            this.label3.Text = "Copyright \x00a9 NAMMEDIA .INC";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x21d, 0xd3);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.textBoxDescription);
            base.Controls.Add(this.okButton);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AboutBox1";
            base.Padding = new Padding(9);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "About Medboss";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void labelCopyright_Click(object sender, EventArgs e)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
