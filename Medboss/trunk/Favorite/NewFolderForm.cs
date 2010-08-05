namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewFolderForm : Form
    {
        private string _folder = "";
        private Button butClose;
        private Button butTaoMoi;
        private IContainer components = null;
        private Label label1;
        private TextBox txtFolderName;

        public NewFolderForm()
        {
            this.InitializeComponent();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void butTaoMoi_Click(object sender, EventArgs e)
        {
            if (this.txtFolderName.Text != "")
            {
                this._folder = this.txtFolderName.Text;
                base.DialogResult = DialogResult.OK;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string getFolderName()
        {
            return this._folder;
        }

        private void InitializeComponent()
        {
            this.butTaoMoi = new Button();
            this.label1 = new Label();
            this.txtFolderName = new TextBox();
            this.butClose = new Button();
            base.SuspendLayout();
            this.butTaoMoi.Location = new Point(0xf1, 0x1a);
            this.butTaoMoi.Name = "butTaoMoi";
            this.butTaoMoi.Size = new Size(0x3e, 0x17);
            this.butTaoMoi.TabIndex = 0;
            this.butTaoMoi.Text = "&Tạo mới";
            this.butTaoMoi.UseVisualStyleBackColor = true;
            this.butTaoMoi.Click += new EventHandler(this.butTaoMoi_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(160, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đ\x00e1nh t\x00ean thư mục bạn muốn tạo";
            this.txtFolderName.Location = new Point(0x10, 0x1c);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new Size(0xdb, 20);
            this.txtFolderName.TabIndex = 2;
            this.butClose.Location = new Point(0x135, 0x1a);
            this.butClose.Name = "butClose";
            this.butClose.Size = new Size(0x4b, 0x17);
            this.butClose.TabIndex = 3;
            this.butClose.Text = "Đ\x00f3&ng";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new EventHandler(this.butClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18c, 0x41);
            base.Controls.Add(this.butClose);
            base.Controls.Add(this.txtFolderName);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.butTaoMoi);
            base.Name = "NewFolderForm";
            this.Text = "Tạo thư mục mới";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void setRefFolderName(ref string folder)
        {
            this._folder = folder;
        }
    }
}
