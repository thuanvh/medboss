using Nammedia.Medboss.lib;
using Nammedia.Medboss.Utils;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Nammedia.Medboss.controls
{

    public class LoaiThuocUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private DataGridViewTextBoxColumn colMaLoaiThuChi;
        private DataGridViewTextBoxColumn colTenLoaiThuChi;
        private IContainer components = null;
        private int dataid;
        private DataGridView dgrLoaiThuoc;
        private int index;
        private Label label1;
        private Label lblLoaiThuoc;
        private TextBox txtLoaiThuoc;

        public LoaiThuocUIOperator()
        {
            this.InitializeComponent();
            this.binding();
            new FormatFactory().Bind(FormatType.Trim, this.txtLoaiThuoc);
        }

        private void binding()
        {
            ArrayList list = new LoaiThuocController().List();
            this.dgrLoaiThuoc.DataSource = null;
            this.dgrLoaiThuoc.AutoGenerateColumns = false;
            this.dgrLoaiThuoc.Rows.Clear();
            this.dgrLoaiThuoc.DataSource = list;
            this.colMaLoaiThuChi.Visible = false;
            this.dgrLoaiThuoc.Refresh();
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
            LoaiThuocController controller = new LoaiThuocController();
            return (this.dataid = controller.Delete(this.index));
        }

        private void dgrLoaiThuChi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrLoaiThuoc.Rows[e.RowIndex];
            this.txtLoaiThuoc.Text = ConvertHelper.getString(row.Cells[this.colTenLoaiThuChi.Index].Value);
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
            this.lblLoaiThuoc = new Label();
            this.txtLoaiThuoc = new TextBox();
            this.butThem = new Button();
            this.label1 = new Label();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.dgrLoaiThuoc = new DataGridView();
            this.colMaLoaiThuChi = new DataGridViewTextBoxColumn();
            this.colTenLoaiThuChi = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.dgrLoaiThuoc).BeginInit();
            base.SuspendLayout();
            this.lblLoaiThuoc.AutoSize = true;
            this.lblLoaiThuoc.Location = new Point(3, 0x11);
            this.lblLoaiThuoc.Name = "lblLoaiThuoc";
            this.lblLoaiThuoc.Size = new Size(0x39, 13);
            this.lblLoaiThuoc.TabIndex = 0;
            this.lblLoaiThuoc.Text = "Loại thuốc";
            this.txtLoaiThuoc.Location = new Point(0x59, 14);
            this.txtLoaiThuoc.Name = "txtLoaiThuoc";
            this.txtLoaiThuoc.Size = new Size(0xee, 20);
            this.txtLoaiThuoc.TabIndex = 1;
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
            this.dgrLoaiThuoc.AllowUserToAddRows = false;
            this.dgrLoaiThuoc.AllowUserToDeleteRows = false;
            this.dgrLoaiThuoc.AllowUserToResizeColumns = false;
            this.dgrLoaiThuoc.AllowUserToResizeRows = false;
            this.dgrLoaiThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrLoaiThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrLoaiThuoc.ColumnHeadersVisible = false;
            this.dgrLoaiThuoc.Columns.AddRange(new DataGridViewColumn[] { this.colMaLoaiThuChi, this.colTenLoaiThuChi });
            this.dgrLoaiThuoc.Location = new Point(0x57, 0x42);
            this.dgrLoaiThuoc.Name = "dgrLoaiThuoc";
            this.dgrLoaiThuoc.ReadOnly = true;
            this.dgrLoaiThuoc.Size = new Size(240, 0xa2);
            this.dgrLoaiThuoc.TabIndex = 7;
            this.dgrLoaiThuoc.CellEnter += new DataGridViewCellEventHandler(this.dgrLoaiThuChi_CellEnter);
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
            base.Controls.Add(this.dgrLoaiThuoc);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.txtLoaiThuoc);
            base.Controls.Add(this.lblLoaiThuoc);
            base.Name = "LoaiDoiTacUIOperator";
            base.Size = new Size(0x22b, 0x153);
            ((ISupportInitialize) this.dgrLoaiThuoc).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override int insert()
        {
            LoaiThuocController controller = new LoaiThuocController();
            LoaiThuocInfo loaiThuocInfo = new LoaiThuocInfo();
            loaiThuocInfo.TenLoai = this.txtLoaiThuoc.Text;
            return (this.dataid = controller.Insert(loaiThuocInfo));
        }

        protected override int update()
        {
            LoaiThuocController controller = new LoaiThuocController();
            LoaiThuocInfo loaiThuocInfo = new LoaiThuocInfo();
            loaiThuocInfo.TenLoai = this.txtLoaiThuoc.Text;
            loaiThuocInfo.MaLoai = this.index;
            return (this.dataid = controller.Update(loaiThuocInfo));
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
                return Nammedia.Medboss.controls.DataType.LoaiThuoc;
            }
        }
    }
}
