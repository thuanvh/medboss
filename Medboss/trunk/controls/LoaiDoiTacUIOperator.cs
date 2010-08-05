namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoaiDoiTacUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private DataGridViewTextBoxColumn colMaLoaiThuChi;
        private DataGridViewTextBoxColumn colTenLoaiThuChi;
        private IContainer components = null;
        private int dataid;
        private DataGridView dgrNhomDoiTac;
        private int index;
        private Label label1;
        private Label lblLoaiThuChi;
        private TextBox txtTenNhomDoiTac;

        public LoaiDoiTacUIOperator()
        {
            this.InitializeComponent();
            this.binding();
            new FormatFactory().Bind(FormatType.Trim, this.txtTenNhomDoiTac);
        }

        private void binding()
        {
            ArrayList aCCSGroups = new CSController().GetACCSGroups();
            this.dgrNhomDoiTac.DataSource = null;
            this.dgrNhomDoiTac.AutoGenerateColumns = false;
            this.dgrNhomDoiTac.Rows.Clear();
            this.dgrNhomDoiTac.DataSource = aCCSGroups;
            this.colMaLoaiThuChi.Visible = false;
            this.dgrNhomDoiTac.Refresh();
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            base.Update();
            this.binding();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
            this.binding();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            base.Delete();
            this.binding();
        }

        protected override int delete()
        {
            CSController controller = new CSController();
            LoaiKhachHang loaiKh = new LoaiKhachHang();
            loaiKh.TenLoai = this.txtTenNhomDoiTac.Text;
            loaiKh.MaLoai = this.index;
            return (this.dataid = controller.DeleteLoaiKhachHang(loaiKh));
        }

        private void dgrLoaiThuChi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrNhomDoiTac.Rows[e.RowIndex];
            this.txtTenNhomDoiTac.Text = ConvertHelper.getString(row.Cells[e.ColumnIndex].Value);
            this.index = ConvertHelper.getInt(row.Cells[this.colMaLoaiThuChi.Index].Value);
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
            this.lblLoaiThuChi = new Label();
            this.txtTenNhomDoiTac = new TextBox();
            this.butThem = new Button();
            this.label1 = new Label();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.dgrNhomDoiTac = new DataGridView();
            this.colMaLoaiThuChi = new DataGridViewTextBoxColumn();
            this.colTenLoaiThuChi = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrNhomDoiTac).BeginInit();
            base.SuspendLayout();
            this.lblLoaiThuChi.AutoSize = true;
            this.lblLoaiThuChi.Location = new Point(3, 0x11);
            this.lblLoaiThuChi.Name = "lblLoaiThuChi";
            this.lblLoaiThuChi.Size = new Size(0x47, 13);
            this.lblLoaiThuChi.TabIndex = 0;
            this.lblLoaiThuChi.Text = "Nh\x00f3m đối t\x00e1c";
            this.txtTenNhomDoiTac.Location = new Point(0x59, 14);
            this.txtTenNhomDoiTac.Name = "txtTenNhomDoiTac";
            this.txtTenNhomDoiTac.Size = new Size(0xee, 20);
            this.txtTenNhomDoiTac.TabIndex = 1;
            this.butThem.Location = new Point(0x161, 12);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 2;
            this.butThem.Text = "Th\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5b, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "C\x00e1c nh\x00f3m đối t\x00e1c";
            this.butSua.Location = new Point(0x161, 0x42);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x4b, 0x17);
            this.butSua.TabIndex = 5;
            this.butSua.Text = "Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.butXoa.Location = new Point(0x161, 0x72);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x4b, 0x17);
            this.butXoa.TabIndex = 6;
            this.butXoa.Text = "Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.dgrNhomDoiTac.AllowUserToAddRows = false;
            this.dgrNhomDoiTac.AllowUserToDeleteRows = false;
            this.dgrNhomDoiTac.AllowUserToResizeColumns = false;
            this.dgrNhomDoiTac.AllowUserToResizeRows = false;
            this.dgrNhomDoiTac.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrNhomDoiTac.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrNhomDoiTac.ColumnHeadersVisible = false;
            this.dgrNhomDoiTac.Columns.AddRange(new DataGridViewColumn[] { this.colMaLoaiThuChi, this.colTenLoaiThuChi });
            this.dgrNhomDoiTac.Location = new Point(0x57, 0x42);
            this.dgrNhomDoiTac.Name = "dgrNhomDoiTac";
            this.dgrNhomDoiTac.ReadOnly = true;
            this.dgrNhomDoiTac.Size = new Size(240, 150);
            this.dgrNhomDoiTac.TabIndex = 7;
            this.dgrNhomDoiTac.CellEnter += new DataGridViewCellEventHandler(this.dgrLoaiThuChi_CellEnter);
            this.colMaLoaiThuChi.DataPropertyName = "MaLoai";
            this.colMaLoaiThuChi.HeaderText = "M\x00e3 Loại Thu Chi";
            this.colMaLoaiThuChi.Name = "colMaLoaiThuChi";
            this.colMaLoaiThuChi.ReadOnly = true;
            this.colMaLoaiThuChi.Visible = false;
            this.colMaLoaiThuChi.Width = 5;
            this.colTenLoaiThuChi.DataPropertyName = "TenLoai";
            this.colTenLoaiThuChi.HeaderText = "T\x00ean Loại Thu Chi";
            this.colTenLoaiThuChi.Name = "colTenLoaiThuChi";
            this.colTenLoaiThuChi.ReadOnly = true;
            this.colTenLoaiThuChi.Width = 5;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.dgrNhomDoiTac);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.txtTenNhomDoiTac);
            base.Controls.Add(this.lblLoaiThuChi);
            base.Name = "LoaiDoiTacUIOperator";
            base.Size = new Size(0x1c4, 0xeb);
            ((ISupportInitialize) this.dgrNhomDoiTac).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override int insert()
        {
            CSController controller = new CSController();
            LoaiKhachHang loaiKK = new LoaiKhachHang();
            loaiKK.TenLoai = this.txtTenNhomDoiTac.Text;
            return (this.dataid = controller.InsertLoaiKhachHang(loaiKK));
        }

        protected override int update()
        {
            CSController controller = new CSController();
            LoaiKhachHang loaiKh = new LoaiKhachHang();
            loaiKh.TenLoai = this.txtTenNhomDoiTac.Text;
            loaiKh.MaLoai = this.index;
            return (this.dataid = controller.UpdateLoaiKhachHang(loaiKh));
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
                return Nammedia.Medboss.controls.DataType.LoaiKhachHang;
            }
        }
    }
}
