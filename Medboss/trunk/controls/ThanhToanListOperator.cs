namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ThanhToanListOperator : OperatorBase
    {
        private Button butNhap;
        private Button butSua;
        private Button butTaiHoaDon;
        private Button butXoa;
        private IContainer components;
        private int dataid;
        private GroupBox groupBox1;
        private ImportOrderUI iptOrder;
        private Label lblMaHoaDon;
        private int MaHoaDon;
        private Panel panel1;
        private RadioButton radAdding;
        private RadioButton radEditting;
        private TableLayoutPanel tableLayoutPanel1;
        private ThanhToanList thongTinNhapThuoc;
        private TextBox txtMaHoaDon;

        public event ViewFunc SelectedItemView
        {
            add
            {
                base.eventTable["SelectedItemView"] = (ViewFunc) Delegate.Combine((ViewFunc) base.eventTable["SelectedItemView"], value);
            }
            remove
            {
                base.eventTable["SelectedItemView"] = (ViewFunc) Delegate.Remove((ViewFunc) base.eventTable["SelectedItemView"], value);
            }
        }

        public ThanhToanListOperator()
        {
            this.components = null;
            this.InitializeComponent();
            this.ThemMoiHD();
        }

        public ThanhToanListOperator(int maHoaDon)
        {
            this.components = null;
            this.InitializeComponent();
            this.MaHoaDon = maHoaDon;
            HoaDonThanhToanInfo hdnInfo = new HoaDonThanhToanController().GetHoaDonThanhToan(maHoaDon);
            if (hdnInfo != null)
            {
                if (hdnInfo.MaThanhToan > 0)
                {
                    this.LoadHD(hdnInfo);
                }
            }
            else
            {
                MessageBox.Show("Kh\x00f4ng t\x00ecm thấy h\x00f3a đơn " + maHoaDon);
            }
        }

        private void butNhap_Click(object sender, EventArgs e)
        {
            base.Insert();
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
                    HoaDonThanhToanInfo hdnInfo = new HoaDonThanhToanController().GetHoaDonThanhToan(id);
                    if (hdnInfo != null)
                    {
                        this.LoadHD(hdnInfo);
                        this.MaHoaDon = id;
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

        private void butThanhToan_Click(object sender, EventArgs e)
        {
            ViewFunc func = (ViewFunc) base.eventTable["SelectedItemView"];
            ViewArg args = new ViewArg(Nammedia.Medboss.controls.DataType.ThanhToanHoaDon, this.MaHoaDon);
            if (func != null)
            {
                func(args);
            }
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
        }

        public override void Clear()
        {
            this.thongTinNhapThuoc.clear();
        }

        protected override int delete()
        {
            HoaDonThanhToanController controller = new HoaDonThanhToanController();
            return (this.dataid = controller.Delete(this.thongTinNhapThuoc.getHoaDonThanhToan().MaThanhToan));
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
            this.butNhap = new Button();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.thongTinNhapThuoc = new ThanhToanList();
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.radEditting = new RadioButton();
            this.radAdding = new RadioButton();
            this.butTaiHoaDon = new Button();
            this.lblMaHoaDon = new Label();
            this.txtMaHoaDon = new TextBox();
            this.butXoa = new Button();
            this.butSua = new Button();
            this.iptOrder = new ImportOrderUI();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.butNhap.Location = new Point(0x129, 13);
            this.butNhap.Name = "butNhap";
            this.butNhap.Size = new Size(0x4b, 0x17);
            this.butNhap.TabIndex = 100;
            this.butNhap.Text = "&Th\x00eam";
            this.butNhap.UseVisualStyleBackColor = true;
            this.butNhap.Click += new EventHandler(this.butNhap_Click);
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.thongTinNhapThuoc, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 84f));
            this.tableLayoutPanel1.Size = new Size(0x2a6, 0x1c8);
            this.tableLayoutPanel1.TabIndex = 3;
            this.thongTinNhapThuoc.Dock = DockStyle.Fill;
            this.thongTinNhapThuoc.Location = new Point(3, 3);
            this.thongTinNhapThuoc.Name = "thongTinNhapThuoc";
            this.thongTinNhapThuoc.Size = new Size(0x2a0, 0x16e);
            this.thongTinNhapThuoc.TabIndex = 2;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.butXoa);
            this.panel1.Controls.Add(this.butSua);
            this.panel1.Controls.Add(this.butNhap);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2a0, 0x4e);
            this.panel1.TabIndex = 3;
            this.groupBox1.Controls.Add(this.radEditting);
            this.groupBox1.Controls.Add(this.radAdding);
            this.groupBox1.Controls.Add(this.butTaiHoaDon);
            this.groupBox1.Controls.Add(this.lblMaHoaDon);
            this.groupBox1.Controls.Add(this.txtMaHoaDon);
            this.groupBox1.Location = new Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x10a, 0x4b);
            this.groupBox1.TabIndex = 0x6a;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế độ";
            this.radEditting.AutoSize = true;
            this.radEditting.Location = new Point(6, 0x2a);
            this.radEditting.Name = "radEditting";
            this.radEditting.Size = new Size(0x2c, 0x11);
            this.radEditting.TabIndex = 0x6b;
            this.radEditting.Text = "Sửa";
            this.radEditting.UseVisualStyleBackColor = true;
            this.radEditting.CheckedChanged += new EventHandler(this.radEditting_CheckedChanged);
            this.radAdding.AutoSize = true;
            this.radAdding.Checked = true;
            this.radAdding.Location = new Point(6, 0x13);
            this.radAdding.Name = "radAdding";
            this.radAdding.Size = new Size(0x47, 0x11);
            this.radAdding.TabIndex = 0x6a;
            this.radAdding.TabStop = true;
            this.radAdding.Text = "Th\x00eam mới";
            this.radAdding.UseVisualStyleBackColor = true;
            this.radAdding.CheckedChanged += new EventHandler(this.radAdding_CheckedChanged);
            this.butTaiHoaDon.Enabled = false;
            this.butTaiHoaDon.Location = new Point(0xcd, 0x27);
            this.butTaiHoaDon.Name = "butTaiHoaDon";
            this.butTaiHoaDon.Size = new Size(0x33, 0x17);
            this.butTaiHoaDon.TabIndex = 0x69;
            this.butTaiHoaDon.Text = "Tải ";
            this.butTaiHoaDon.UseVisualStyleBackColor = true;
            this.butTaiHoaDon.Click += new EventHandler(this.butTaiHoaDon_Click);
            this.lblMaHoaDon.AutoSize = true;
            this.lblMaHoaDon.Enabled = false;
            this.lblMaHoaDon.Location = new Point(0x4c, 0x2c);
            this.lblMaHoaDon.Name = "lblMaHoaDon";
            this.lblMaHoaDon.Size = new Size(0x41, 13);
            this.lblMaHoaDon.TabIndex = 0x68;
            this.lblMaHoaDon.Text = "M\x00e3 ho\x00e1 đơn";
            this.txtMaHoaDon.Enabled = false;
            this.txtMaHoaDon.Location = new Point(0x93, 0x27);
            this.txtMaHoaDon.Name = "txtMaHoaDon";
            this.txtMaHoaDon.Size = new Size(0x34, 20);
            this.txtMaHoaDon.TabIndex = 0x67;
            this.butXoa.Location = new Point(0x1cb, 13);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x4b, 0x17);
            this.butXoa.TabIndex = 0x66;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.butSua.Location = new Point(0x17a, 13);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x4b, 0x17);
            this.butSua.TabIndex = 0x65;
            this.butSua.Text = "&Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.iptOrder.Location = new Point(0, 0);
            this.iptOrder.Name = "iptOrder";
            this.iptOrder.Size = new Size(640, 0x191);
            this.iptOrder.TabIndex = 0;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ThanhToanListOperator";
            base.Size = new Size(0x2a6, 0x1c8);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            HoaDonThanhToanController controller = new HoaDonThanhToanController();
            return controller.Insert(this.thongTinNhapThuoc.getHoaDonThanhToan());
        }

        private void LoadHD(HoaDonThanhToanInfo hdnInfo)
        {
            this.thongTinNhapThuoc.loadInfo(hdnInfo);
            this.butNhap.Visible = false;
            this.butSua.Visible = true;
            this.butXoa.Visible = true;
        }

        private void radAdding_CheckedChanged(object sender, EventArgs e)
        {
            this.ThemMoiHD();
        }

        private void radEditting_CheckedChanged(object sender, EventArgs e)
        {
            this.lblMaHoaDon.Enabled = this.txtMaHoaDon.Enabled = this.butTaiHoaDon.Enabled = this.radEditting.Checked;
        }

        private void ThemMoiHD()
        {
            this.butNhap.Visible = true;
            this.butSua.Visible = false;
            this.butXoa.Visible = false;
            this.thongTinNhapThuoc.clear();
        }

        protected override int update()
        {
            HoaDonThanhToanController controller = new HoaDonThanhToanController();
            return (this.dataid = controller.Update(this.thongTinNhapThuoc.getHoaDonThanhToan()));
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
                return Nammedia.Medboss.controls.DataType.HoaDonNhap;
            }
        }
    }
}
