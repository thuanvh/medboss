namespace Nammedia.Medboss
{
    using Nammedia.Medboss.security;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Password : Form
    {
        private Button butClose;
        private Button butOk;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TextBox txtNguoiDung;
        private TextBox txtPass;

        public Password()
        {
            this.InitializeComponent();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            string userName = this.txtNguoiDung.Text;
            string pass = this.txtPass.Text;
            if ((userName == "") || (pass == ""))
            {
                MessageBox.Show("Y\x00eau cầu nhập đầy đủ th\x00f4ng tin");
            }
            else
            {
                Validator validator = new Validator();
                if (validator.IsValidated(userName, pass))
                {
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
                else
                {
                    MessageBox.Show("Người d\x00f9ng v\x00e0 mật khẩu kh\x00f4ng hợp lệ, bạn h\x00e3y đăng nhập lại.\n Ch\x00fa \x00fd đến ph\x00edm Caps Lock.", "Th\x00f4ng b\x00e1o");
                    this.txtPass.Text = "";
                }
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.txtNguoiDung = new TextBox();
            this.txtPass = new TextBox();
            this.label2 = new Label();
            this.butOk = new Button();
            this.butClose = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3e, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Người d\x00f9ng";
            this.txtNguoiDung.Location = new Point(0x5f, 0x16);
            this.txtNguoiDung.Name = "txtNguoiDung";
            this.txtNguoiDung.Size = new Size(0x9b, 20);
            this.txtNguoiDung.TabIndex = 1;
            this.txtNguoiDung.WordWrap = false;
            this.txtPass.Location = new Point(0x5f, 0x37);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new Size(0x9b, 20);
            this.txtPass.TabIndex = 2;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.WordWrap = false;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu";
            this.butOk.Location = new Point(0x5e, 0x58);
            this.butOk.Name = "butOk";
            this.butOk.Size = new Size(0x4b, 0x17);
            this.butOk.TabIndex = 4;
            this.butOk.Text = "Đăng nhập";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new EventHandler(this.butOk_Click);
            this.butClose.Location = new Point(0xaf, 0x58);
            this.butClose.Name = "butClose";
            this.butClose.Size = new Size(0x4b, 0x17);
            this.butClose.TabIndex = 5;
            this.butClose.Text = "Đ\x00f3ng";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new EventHandler(this.butClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x110, 0x7c);
            base.Controls.Add(this.butClose);
            base.Controls.Add(this.butOk);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtPass);
            base.Controls.Add(this.txtNguoiDung);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Password";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Password";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
