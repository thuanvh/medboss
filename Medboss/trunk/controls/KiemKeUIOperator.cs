namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class KiemKeUIOperator : OperatorBase
    {
        private Button butLuuKiemKeMoi;
        private Button butSua;
        private Button butTaiHoaDon;
        private Button butThem;
        private Button butXoa;
        private IContainer components;
        private int dataid;
        private GroupBox groupBox1;
        private KiemKeUI kiemkeui;
        private Label lblMaHoaDon;
        private Panel panel1;
        private RadioButton radAdding;
        private RadioButton radEditting;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtMaHoaDon;

        public KiemKeUIOperator()
        {
            this.components = null;
            this.InitializeComponent();
            this.ThemMoiHD();
        }

        public KiemKeUIOperator(int ma)
        {
            this.components = null;
            this.InitializeComponent();
            KiemKeInfo kkInfo = new KiemKeController().GetById(ma);
            this.LoadHD(kkInfo);
        }

        private void butLuuKiemKeMoi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thực sự muốn lưu như kiểm k\x00ea mới? \n H\x00e3y ch\x00fa \x00fd đến ng\x00e0y th\x00e1ng.", "Ch\x00fa \x00fd", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                base.Insert();
            }
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void butTaiHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtMaHoaDon.Text != "")
                {
                    int id = int.Parse(this.txtMaHoaDon.Text);
                    KiemKeInfo kkInfo = new KiemKeController().GetById(id);
                    if (kkInfo != null)
                    {
                        this.LoadHD(kkInfo);
                    }
                    else
                    {
                        MessageBox.Show("Kh\x00f4ng t\x00ecm thấy ho\x00e1 đơn " + this.txtMaHoaDon.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Y\x00eau cầu chỉ r\x00f5 m\x00e3 ho\x00e1 đơn.");
                }
            }
            catch
            {
                MessageBox.Show("Kh\x00f4ng t\x00ecm thấy ho\x00e1 đơn " + this.txtMaHoaDon.Text);
            }
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        public override void Clear()
        {
            this.kiemkeui.clear();
        }

        protected override int delete()
        {
            KiemKeController controller = new KiemKeController();
            KiemKeInfo info = this.kiemkeui.getKiemKeInfo();
            return (this.dataid = controller.Delete(info.MaKiemKe));
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
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.radEditting = new RadioButton();
            this.radAdding = new RadioButton();
            this.butTaiHoaDon = new Button();
            this.lblMaHoaDon = new Label();
            this.txtMaHoaDon = new TextBox();
            this.butLuuKiemKeMoi = new Button();
            this.butXoa = new Button();
            this.butSua = new Button();
            this.butThem = new Button();
            this.kiemkeui = new KiemKeUI();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kiemkeui, 0, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80.04435f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 19.95565f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(760, 0x1c3);
            this.tableLayoutPanel1.TabIndex = 0;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.butLuuKiemKeMoi);
            this.panel1.Controls.Add(this.butXoa);
            this.panel1.Controls.Add(this.butSua);
            this.panel1.Controls.Add(this.butThem);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x16c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2f2, 0x54);
            this.panel1.TabIndex = 0;
            this.groupBox1.Controls.Add(this.radEditting);
            this.groupBox1.Controls.Add(this.radAdding);
            this.groupBox1.Controls.Add(this.butTaiHoaDon);
            this.groupBox1.Controls.Add(this.lblMaHoaDon);
            this.groupBox1.Controls.Add(this.txtMaHoaDon);
            this.groupBox1.Location = new Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(330, 0x4b);
            this.groupBox1.TabIndex = 0x6b;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế độ";
            this.radEditting.AutoSize = true;
            this.radEditting.Location = new Point(20, 0x2e);
            this.radEditting.Name = "radEditting";
            this.radEditting.Size = new Size(0x2c, 0x11);
            this.radEditting.TabIndex = 0x6b;
            this.radEditting.Text = "Sửa";
            this.radEditting.UseVisualStyleBackColor = true;
            this.radEditting.CheckedChanged += new EventHandler(this.radEditting_CheckedChanged);
            this.radAdding.AutoSize = true;
            this.radAdding.Checked = true;
            this.radAdding.Location = new Point(20, 0x13);
            this.radAdding.Name = "radAdding";
            this.radAdding.Size = new Size(0x47, 0x11);
            this.radAdding.TabIndex = 0x6a;
            this.radAdding.TabStop = true;
            this.radAdding.Text = "Th\x00eam mới";
            this.radAdding.UseVisualStyleBackColor = true;
            this.radAdding.CheckedChanged += new EventHandler(this.radAdding_CheckedChanged);
            this.butTaiHoaDon.Enabled = false;
            this.butTaiHoaDon.Location = new Point(0xd9, 0x2e);
            this.butTaiHoaDon.Name = "butTaiHoaDon";
            this.butTaiHoaDon.Size = new Size(0x33, 0x17);
            this.butTaiHoaDon.TabIndex = 0x69;
            this.butTaiHoaDon.Text = "Tải ";
            this.butTaiHoaDon.UseVisualStyleBackColor = true;
            this.butTaiHoaDon.Click += new EventHandler(this.butTaiHoaDon_Click);
            this.lblMaHoaDon.AutoSize = true;
            this.lblMaHoaDon.Enabled = false;
            this.lblMaHoaDon.Location = new Point(0x58, 0x33);
            this.lblMaHoaDon.Name = "lblMaHoaDon";
            this.lblMaHoaDon.Size = new Size(0x41, 13);
            this.lblMaHoaDon.TabIndex = 0x68;
            this.lblMaHoaDon.Text = "M\x00e3 ho\x00e1 đơn";
            this.txtMaHoaDon.Enabled = false;
            this.txtMaHoaDon.Location = new Point(0x9f, 0x30);
            this.txtMaHoaDon.Name = "txtMaHoaDon";
            this.txtMaHoaDon.Size = new Size(0x34, 20);
            this.txtMaHoaDon.TabIndex = 0x67;
            this.butLuuKiemKeMoi.Location = new Point(0x163, 30);
            this.butLuuKiemKeMoi.Name = "butLuuKiemKeMoi";
            this.butLuuKiemKeMoi.Size = new Size(0x90, 0x17);
            this.butLuuKiemKeMoi.TabIndex = 3;
            this.butLuuKiemKeMoi.Text = "Lưu lại như kiểm k\x00ea mới";
            this.butLuuKiemKeMoi.UseVisualStyleBackColor = true;
            this.butLuuKiemKeMoi.Visible = false;
            this.butLuuKiemKeMoi.Click += new EventHandler(this.butLuuKiemKeMoi_Click);
            this.butXoa.Location = new Point(0x29b, 30);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x4b, 0x17);
            this.butXoa.TabIndex = 2;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Visible = false;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.butSua.Location = new Point(0x24a, 30);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x4b, 0x17);
            this.butSua.TabIndex = 1;
            this.butSua.Text = "&Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Visible = false;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.butThem.Location = new Point(0x1f9, 30);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 0;
            this.butThem.Text = "&Th\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            this.kiemkeui.Dock = DockStyle.Fill;
            this.kiemkeui.Location = new Point(3, 3);
            this.kiemkeui.Name = "kiemkeui";
            this.kiemkeui.Size = new Size(0x2f2, 0x163);
            this.kiemkeui.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "KiemKeUIOperator";
            base.Size = new Size(760, 0x1c3);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            KiemKeController controller = new KiemKeController();
            KiemKeInfo ki = this.kiemkeui.getKiemKeInfo();
            return (this.dataid = controller.Insert(ki));
        }

        private void LoadHD(KiemKeInfo kkInfo)
        {
            this.kiemkeui.loadKiemKeInfo(kkInfo);
            this.butSua.Visible = true;
            this.butXoa.Visible = true;
            this.butLuuKiemKeMoi.Visible = true;
            this.butThem.Visible = false;
        }

        private void radAdding_CheckedChanged(object sender, EventArgs e)
        {
            this.ThemMoiHD();
        }

        private void radEditting_CheckedChanged(object sender, EventArgs e)
        {
            this.lblMaHoaDon.Enabled = this.txtMaHoaDon.Enabled = this.butTaiHoaDon.Enabled = this.radEditting.Checked;
        }

        public override void RefreshAC()
        {
            this.kiemkeui.loadAC();
        }

        private void ThemMoiHD()
        {
            this.butSua.Visible = false;
            this.butXoa.Visible = false;
            this.butLuuKiemKeMoi.Visible = false;
            this.butThem.Visible = true;
            this.kiemkeui.clear();
        }

        protected override int update()
        {
            KiemKeController controller = new KiemKeController();
            KiemKeInfo kiemKeInfo = this.kiemkeui.getKiemKeInfo();
            return (this.dataid = controller.Update(kiemKeInfo));
        }

        protected override int DataId
        {
            get
            {
                return this.dataid;
            }
        }

        protected override Nammedia.Medboss.controls.DataType DataType
        {
            get
            {
                return Nammedia.Medboss.controls.DataType.KiemKe;
            }
        }
    }
}
