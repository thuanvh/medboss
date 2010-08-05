using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class NhanVienListOperator : OperatorBase
    {
        private ComboBox cboNhanVien;
        private IContainer components = null;
        private Label label1;
        private NhanVienUIOperator nvuiOper;

        public NhanVienListOperator()
        {
            this.InitializeComponent();
            this.loadAC();
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            NhanVienController controller = new NhanVienController();
            int maNhanVien = controller.getIdByName(this.cboNhanVien.Text);
            NhanVienInfo nvi = controller.GetById(maNhanVien);
            this.nvuiOper.loadNhanVienInfo(nvi);
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
            this.cboNhanVien = new ComboBox();
            this.nvuiOper = new NhanVienUIOperator();
            this.label1 = new Label();
            base.SuspendLayout();
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new Point(0xa8, 0x2d);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new Size(0x79, 0x15);
            this.cboNhanVien.TabIndex = 0;
            this.cboNhanVien.SelectedIndexChanged += new EventHandler(this.cboNhanVien_SelectedIndexChanged);
            this.nvuiOper.Location = new Point(0x2e, 0x59);
            this.nvuiOper.Name = "nvuiOper";
            this.nvuiOper.Size = new Size(0xf3, 0xd6);
            this.nvuiOper.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x35, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6d, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh s\x00e1ch nh\x00e2n vi\x00ean";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.nvuiOper);
            base.Controls.Add(this.cboNhanVien);
            base.Name = "NhanVienListOperator";
            base.Size = new Size(0x174, 330);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboNhanVien, ref this._nhanVienSource);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboNhanVien, ref this._nhanVienSource);
        }
    }
}
