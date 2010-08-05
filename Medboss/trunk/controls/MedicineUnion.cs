using Nammedia.Medboss.lib;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nammedia.Medboss.controls
{

    public class MedicineUnion : OperatorBase
    {
        private Button button1;
        private ComboBox cboDVTDes;
        private ComboBox cboDVTSrc;
        private ComboBox cboThuocDes;
        private ComboBox cboThuocSrc;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;

        public MedicineUnion()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void cboThuocDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            MedicineInfo selectedItem = (MedicineInfo) this.cboThuocDes.SelectedItem;
            this.cboDVTDes.DisplayMember = "DVT";
            this.cboDVTDes.DataSource = this.thuoctraodoi2dvt(selectedItem.ThuocTraoDois);
        }

        private void cboThuocSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            MedicineInfo selectedItem = (MedicineInfo) this.cboThuocSrc.SelectedItem;
            this.cboDVTSrc.DisplayMember = "DVT";
            this.cboDVTSrc.DataSource = this.thuoctraodoi2dvt(selectedItem.ThuocTraoDois);
        }

        public void Chuyen()
        {
            MedicineController controller = new MedicineController();
            int maThuocTraoDoiSource = (int) this.cboDVTSrc.SelectedValue;
            int maThuocTraoDoiDes = (int) this.cboDVTDes.SelectedValue;
            controller.medicineUnion(maThuocTraoDoiSource, maThuocTraoDoiDes);
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
            this.cboThuocSrc = new ComboBox();
            this.cboDVTSrc = new ComboBox();
            this.cboThuocDes = new ComboBox();
            this.cboDVTDes = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.label5 = new Label();
            this.button1 = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.cboThuocSrc.Location = new Point(50, 0x19);
            this.cboThuocSrc.Name = "cboThuocSrc";
            this.cboThuocSrc.Size = new Size(230, 0x15);
            this.cboThuocSrc.TabIndex = 0;
            this.cboThuocSrc.SelectedIndexChanged += new EventHandler(this.cboThuocSrc_SelectedIndexChanged);
            this.cboDVTSrc.DisplayMember = "DVT";
            this.cboDVTSrc.FormattingEnabled = true;
            this.cboDVTSrc.Location = new Point(0x158, 0x19);
            this.cboDVTSrc.Name = "cboDVTSrc";
            this.cboDVTSrc.Size = new Size(0x33, 0x15);
            this.cboDVTSrc.TabIndex = 1;
            this.cboThuocDes.FormattingEnabled = true;
            this.cboThuocDes.Location = new Point(50, 0x16);
            this.cboThuocDes.Name = "cboThuocDes";
            this.cboThuocDes.Size = new Size(230, 0x15);
            this.cboThuocDes.TabIndex = 2;
            this.cboThuocDes.SelectedIndexChanged += new EventHandler(this.cboThuocDes_SelectedIndexChanged);
            this.cboDVTDes.DisplayMember = "DVT";
            this.cboDVTDes.FormattingEnabled = true;
            this.cboDVTDes.Location = new Point(0x158, 0x16);
            this.cboDVTDes.Name = "cboDVTDes";
            this.cboDVTDes.Size = new Size(0x33, 0x15);
            this.cboDVTDes.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Thuốc";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(310, 0x1c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ĐVT";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x19);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Thuốc";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(310, 0x19);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "ĐVT";
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboThuocSrc);
            this.groupBox1.Controls.Add(this.cboDVTSrc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1a8, 0x49);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chuyển thuốc";
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboThuocDes);
            this.groupBox2.Controls.Add(this.cboDVTDes);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new Point(3, 0x52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a8, 0x40);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Th\x00e0nh thuốc";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(9, 0x95);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x174, 0x27);
            this.label5.TabIndex = 10;
            this.label5.Text = "Bất cứ việc chuyển một loại thuốc n\x00e0y th\x00e0nh một loại thuốc \n kh\x00e1c \x00e1p dụng cho tất cả loại dữ liệu, v\x00ec thế kh\x00f4ng thể kh\x00f4i phục lại ban đầu,\n bạn n\x00ean c\x00e2n nhắc trước khi thực hiện";
            this.button1.Location = new Point(0x1d0, 0x12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 11;
            this.button1.Text = "&Chuyển";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MedicineUnion";
            base.Size = new Size(0x279, 250);
            base.Load += new EventHandler(this.MedicineUnion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            if ((base._dvtSource != null) && (base._medSource != null))
            {
                ArrayList datasource = (ArrayList) base._dvtSource.Clone();
                ArrayList list2 = (ArrayList) base._medSource.Clone();
                base._acFactory.EnableAutocomplete(this.cboThuocDes, ref this._medSource);
                base._acFactory.EnableAutocomplete(this.cboThuocSrc, ref list2);
            }
        }

        private void MedicineUnion_Load(object sender, EventArgs e)
        {
            this.cboDVTSrc.DisplayMember = "DVT";
            this.cboDVTSrc.ValueMember = "MaThuocTraoDoi";
            this.cboDVTDes.DisplayMember = "DVT";
            this.cboDVTDes.ValueMember = "MaThuocTraoDoi";
            this.loadAC();
        }

        public override void RefreshAC()
        {
            ArrayList datasource = (ArrayList) base._dvtSource.Clone();
            ArrayList list2 = (ArrayList) base._medSource.Clone();
            base._acFactory.RefreshAutoCompleteSource(this.cboDVTDes, ref this._dvtSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboDVTSrc, ref datasource);
            base._acFactory.RefreshAutoCompleteSource(this.cboThuocDes, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboThuocSrc, ref list2);
        }

        private ArrayList thuoctraodoi2dvt(ArrayList thuoctraodoi)
        {
            ArrayList list = new ArrayList();
            foreach (ThuocTraoDoi doi in thuoctraodoi)
            {
                ThuocDVTStruct tdv = new ThuocDVTStruct();
                tdv.DVT = doi.DVT.TenDV;
                tdv.MaDVT = doi.DVT.MaDVT;
                tdv.MaThuocTraoDoi = doi.MaThuocTraoDoi;
                list.Add(tdv);
            }
            return list;
        }

        protected override int update()
        {
            this.Chuyen();
            return 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ThuocDVTStruct
        {
            private string _dvt;
            private int _mathuoctraodoi;
            private int _madvt;
            public string DVT
            {
                get
                {
                    return this._dvt;
                }
                set
                {
                    this._dvt = value;
                }
            }
            public int MaThuocTraoDoi
            {
                get
                {
                    return this._mathuoctraodoi;
                }
                set
                {
                    this._mathuoctraodoi = value;
                }
            }
            public int MaDVT
            {
                get
                {
                    return this._madvt;
                }
                set
                {
                    this._madvt = value;
                }
            }
        }
    }
}
