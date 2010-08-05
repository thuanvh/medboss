using Nammedia.Medboss.lib;
using Nammedia.Medboss.Utils;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Nammedia.Medboss.controls
{

    public class LoaiThuChiUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private CheckBox chkDangHoatDong;
        private DataGridViewCheckBoxColumn colDangHoatDong;
        private DataGridViewTextBoxColumn colMaLoaiThuChi;
        private DataGridViewTextBoxColumn colTenLoaiThuChi;
        private IContainer components = null;
        private int dataid;
        private DataGridView dgrLoaiThuChi;
        private int index;
        private Label label1;
        private Label lblLoaiThuChi;
        private TextBox txtTenLoaiThuChi;

        public LoaiThuChiUIOperator()
        {
            this.InitializeComponent();
            this.binding();
            new FormatFactory().Bind(FormatType.Trim, this.txtTenLoaiThuChi);
        }

        private void binding()
        {
            ArrayList list = new LoaiThuChiController().listAll();
            this.dgrLoaiThuChi.DataSource = null;
            this.dgrLoaiThuChi.AutoGenerateColumns = false;
            this.dgrLoaiThuChi.Rows.Clear();
            this.dgrLoaiThuChi.DataSource = list;
            this.colMaLoaiThuChi.Visible = false;
            this.dgrLoaiThuChi.Refresh();
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
            LoaiThuChiController controller = new LoaiThuChiController();
            return (this.dataid = controller.Delete(this.index));
        }

        private void dgrLoaiThuChi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrLoaiThuChi.Rows[e.RowIndex];
            this.txtTenLoaiThuChi.Text = ConvertHelper.getString(row.Cells[this.colTenLoaiThuChi.Index].Value);
            this.index = ConvertHelper.getInt(row.Cells[this.colMaLoaiThuChi.Index].Value);
            this.chkDangHoatDong.Checked = ConvertHelper.getBool(row.Cells[this.colDangHoatDong.Index].Value);
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
            this.txtTenLoaiThuChi = new TextBox();
            this.butThem = new Button();
            this.label1 = new Label();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.dgrLoaiThuChi = new DataGridView();
            this.chkDangHoatDong = new CheckBox();
            this.colMaLoaiThuChi = new DataGridViewTextBoxColumn();
            this.colTenLoaiThuChi = new DataGridViewTextBoxColumn();
            this.colDangHoatDong = new DataGridViewCheckBoxColumn();
            ((ISupportInitialize) this.dgrLoaiThuChi).BeginInit();
            base.SuspendLayout();
            this.lblLoaiThuChi.AutoSize = true;
            this.lblLoaiThuChi.Location = new Point(3, 0x11);
            this.lblLoaiThuChi.Name = "lblLoaiThuChi";
            this.lblLoaiThuChi.Size = new Size(0x3e, 13);
            this.lblLoaiThuChi.TabIndex = 0;
            this.lblLoaiThuChi.Text = "Loại thu chi";
            this.txtTenLoaiThuChi.Location = new Point(0x59, 14);
            this.txtTenLoaiThuChi.Name = "txtTenLoaiThuChi";
            this.txtTenLoaiThuChi.Size = new Size(0xee, 20);
            this.txtTenLoaiThuChi.TabIndex = 1;
            this.butThem.Location = new Point(0x161, 12);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 2;
            this.butThem.Text = "Th\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x4c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "C\x00e1c loại thu chi";
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
            this.dgrLoaiThuChi.AllowUserToAddRows = false;
            this.dgrLoaiThuChi.AllowUserToDeleteRows = false;
            this.dgrLoaiThuChi.AllowUserToResizeColumns = false;
            this.dgrLoaiThuChi.AllowUserToResizeRows = false;
            this.dgrLoaiThuChi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrLoaiThuChi.Columns.AddRange(new DataGridViewColumn[] { this.colMaLoaiThuChi, this.colTenLoaiThuChi, this.colDangHoatDong });
            this.dgrLoaiThuChi.Location = new Point(6, 0x66);
            this.dgrLoaiThuChi.Name = "dgrLoaiThuChi";
            this.dgrLoaiThuChi.ReadOnly = true;
            this.dgrLoaiThuChi.Size = new Size(0x141, 0x121);
            this.dgrLoaiThuChi.TabIndex = 7;
            this.dgrLoaiThuChi.CellEnter += new DataGridViewCellEventHandler(this.dgrLoaiThuChi_CellEnter);
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.Location = new Point(0x59, 0x31);
            this.chkDangHoatDong.Name = "chkDangHoatDong";
            this.chkDangHoatDong.Size = new Size(0x68, 0x11);
            this.chkDangHoatDong.TabIndex = 9;
            this.chkDangHoatDong.Text = "Đang hoạt động";
            this.chkDangHoatDong.UseVisualStyleBackColor = true;
            this.colMaLoaiThuChi.DataPropertyName = "MaLoaiThuChi";
            this.colMaLoaiThuChi.HeaderText = "M\x00e3 Loại Thu Chi";
            this.colMaLoaiThuChi.Name = "colMaLoaiThuChi";
            this.colMaLoaiThuChi.ReadOnly = true;
            this.colMaLoaiThuChi.Visible = false;
            this.colMaLoaiThuChi.Width = 110;
            this.colTenLoaiThuChi.DataPropertyName = "TenLoaiThuChi";
            this.colTenLoaiThuChi.HeaderText = "T\x00ean Loại Thu Chi";
            this.colTenLoaiThuChi.Name = "colTenLoaiThuChi";
            this.colTenLoaiThuChi.ReadOnly = true;
            this.colTenLoaiThuChi.Width = 0x72;
            this.colDangHoatDong.DataPropertyName = "DangHoatDong";
            this.colDangHoatDong.HeaderText = "Đang Hoạt Động";
            this.colDangHoatDong.Name = "colDangHoatDong";
            this.colDangHoatDong.ReadOnly = true;
            this.colDangHoatDong.Width = 0x5e;
            base.Controls.Add(this.chkDangHoatDong);
            base.Controls.Add(this.dgrLoaiThuChi);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.txtTenLoaiThuChi);
            base.Controls.Add(this.lblLoaiThuChi);
            base.Name = "LoaiThuChiUIOperator";
            base.Size = new Size(0x1ba, 0x1a1);
            ((ISupportInitialize) this.dgrLoaiThuChi).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override int insert()
        {
            LoaiThuChiController controller = new LoaiThuChiController();
            LoaiThuChiInfo tci = new LoaiThuChiInfo();
            tci.TenLoaiThuChi = this.txtTenLoaiThuChi.Text;
            tci.DangHoatDong = this.chkDangHoatDong.Checked;
            return (this.dataid = controller.Insert(tci));
        }

        protected override int update()
        {
            LoaiThuChiController controller = new LoaiThuChiController();
            LoaiThuChiInfo tci = new LoaiThuChiInfo();
            tci.TenLoaiThuChi = this.txtTenLoaiThuChi.Text;
            tci.MaLoaiThuChi = this.index;
            tci.DangHoatDong = this.chkDangHoatDong.Checked;
            return (this.dataid = controller.Update(tci));
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
                return Nammedia.Medboss.controls.DataType.LoaiThuChi;
            }
        }
    }
}
