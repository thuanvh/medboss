namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoaiKiemKeUIOperator : OperatorBase
    {
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private CheckBox chkHuy;
        private DataGridViewCheckBoxColumn colHuy;
        private DataGridViewTextBoxColumn colMaLoaiKiemKe;
        private DataGridViewTextBoxColumn colTenLoaiKiemKe;
        private IContainer components = null;
        private int dataid;
        private DataGridView dgrLoaiKiemKe;
        private int index;
        private Label label1;
        private Label lblLoaiThuChi;
        private TextBox txtTenLoaiKiemKe;

        public LoaiKiemKeUIOperator()
        {
            this.InitializeComponent();
            this.binding();
            new FormatFactory().Bind(FormatType.Trim, this.txtTenLoaiKiemKe);
        }

        private void binding()
        {
            ArrayList list = new KiemKeController().ListLoaiKiemKe();
            this.dgrLoaiKiemKe.DataSource = null;
            this.dgrLoaiKiemKe.AutoGenerateColumns = false;
            this.dgrLoaiKiemKe.Rows.Clear();
            this.dgrLoaiKiemKe.DataSource = list;
            this.colMaLoaiKiemKe.Visible = false;
            this.dgrLoaiKiemKe.Refresh();
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
            KiemKeController controller = new KiemKeController();
            LoaiKiemKeInfo lkki = new LoaiKiemKeInfo();
            lkki.MaLoaiKiemKe = this.index;
            return (this.dataid = controller.Delete(lkki));
        }

        private void dgrLoaiThuChi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgrLoaiKiemKe.Rows[e.RowIndex];
            this.txtTenLoaiKiemKe.Text = ConvertHelper.getString(row.Cells[this.colTenLoaiKiemKe.Index].Value);
            this.chkHuy.Checked = (bool) row.Cells[this.colHuy.Index].Value;
            this.index = ConvertHelper.getInt(row.Cells[this.colMaLoaiKiemKe.Index].Value);
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
            this.txtTenLoaiKiemKe = new TextBox();
            this.butThem = new Button();
            this.label1 = new Label();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.dgrLoaiKiemKe = new DataGridView();
            this.chkHuy = new CheckBox();
            this.colMaLoaiKiemKe = new DataGridViewTextBoxColumn();
            this.colTenLoaiKiemKe = new DataGridViewTextBoxColumn();
            this.colHuy = new DataGridViewCheckBoxColumn();
            ((ISupportInitialize) this.dgrLoaiKiemKe).BeginInit();
            base.SuspendLayout();
            this.lblLoaiThuChi.AutoSize = true;
            this.lblLoaiThuChi.Location = new Point(3, 0x11);
            this.lblLoaiThuChi.Name = "lblLoaiThuChi";
            this.lblLoaiThuChi.Size = new Size(0x43, 13);
            this.lblLoaiThuChi.TabIndex = 0;
            this.lblLoaiThuChi.Text = "Loại kiểm k\x00ea";
            this.txtTenLoaiKiemKe.Location = new Point(0x57, 14);
            this.txtTenLoaiKiemKe.Name = "txtTenLoaiKiemKe";
            this.txtTenLoaiKiemKe.Size = new Size(0x9a, 20);
            this.txtTenLoaiKiemKe.TabIndex = 1;
            this.butThem.Location = new Point(370, 12);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x4b, 0x17);
            this.butThem.TabIndex = 2;
            this.butThem.Text = "Th\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x2f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "C\x00e1c loại kiểm k\x00ea";
            this.butSua.Location = new Point(370, 0x42);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x4b, 0x17);
            this.butSua.TabIndex = 5;
            this.butSua.Text = "Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.butXoa.Location = new Point(370, 0x72);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x4b, 0x17);
            this.butXoa.TabIndex = 6;
            this.butXoa.Text = "Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.dgrLoaiKiemKe.AllowUserToAddRows = false;
            this.dgrLoaiKiemKe.AllowUserToDeleteRows = false;
            this.dgrLoaiKiemKe.AllowUserToResizeColumns = false;
            this.dgrLoaiKiemKe.AllowUserToResizeRows = false;
            this.dgrLoaiKiemKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrLoaiKiemKe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrLoaiKiemKe.Columns.AddRange(new DataGridViewColumn[] { this.colMaLoaiKiemKe, this.colTenLoaiKiemKe, this.colHuy });
            this.dgrLoaiKiemKe.Location = new Point(0x57, 0x2f);
            this.dgrLoaiKiemKe.Name = "dgrLoaiKiemKe";
            this.dgrLoaiKiemKe.ReadOnly = true;
            this.dgrLoaiKiemKe.Size = new Size(0x115, 150);
            this.dgrLoaiKiemKe.TabIndex = 7;
            this.dgrLoaiKiemKe.CellEnter += new DataGridViewCellEventHandler(this.dgrLoaiThuChi_CellEnter);
            this.chkHuy.AutoSize = true;
            this.chkHuy.Location = new Point(0xfc, 14);
            this.chkHuy.Name = "chkHuy";
            this.chkHuy.Size = new Size(0x4b, 0x11);
            this.chkHuy.TabIndex = 9;
            this.chkHuy.Text = "Hủy thuốc";
            this.chkHuy.UseVisualStyleBackColor = true;
            this.colMaLoaiKiemKe.DataPropertyName = "MaLoaiKiemKe";
            this.colMaLoaiKiemKe.HeaderText = "M\x00e3 Loại Kiểm K\x00ea";
            this.colMaLoaiKiemKe.Name = "colMaLoaiKiemKe";
            this.colMaLoaiKiemKe.ReadOnly = true;
            this.colMaLoaiKiemKe.Width = 0x5b;
            this.colTenLoaiKiemKe.DataPropertyName = "TenLoaiKiemKe";
            this.colTenLoaiKiemKe.HeaderText = "T\x00ean Loại Kiểm K\x00ea";
            this.colTenLoaiKiemKe.Name = "colTenLoaiKiemKe";
            this.colTenLoaiKiemKe.ReadOnly = true;
            this.colTenLoaiKiemKe.Width = 0x5f;
            this.colHuy.DataPropertyName = "Huy";
            this.colHuy.HeaderText = "Hủy";
            this.colHuy.Name = "colHuy";
            this.colHuy.ReadOnly = true;
            this.colHuy.Resizable = DataGridViewTriState.True;
            this.colHuy.SortMode = DataGridViewColumnSortMode.Automatic;
            this.colHuy.Width = 0x33;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkHuy);
            base.Controls.Add(this.dgrLoaiKiemKe);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.txtTenLoaiKiemKe);
            base.Controls.Add(this.lblLoaiThuChi);
            base.Name = "LoaiKiemKeUIOperator";
            base.Size = new Size(0x1f7, 0xe5);
            ((ISupportInitialize) this.dgrLoaiKiemKe).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override int insert()
        {
            KiemKeController controller = new KiemKeController();
            LoaiKiemKeInfo lkki = new LoaiKiemKeInfo();
            lkki.TenLoaiKiemKe = this.txtTenLoaiKiemKe.Text;
            lkki.Huy = this.chkHuy.Checked;
            return (this.dataid = controller.Insert(lkki));
        }

        protected override int update()
        {
            KiemKeController controller = new KiemKeController();
            LoaiKiemKeInfo lkki = new LoaiKiemKeInfo();
            lkki.TenLoaiKiemKe = this.txtTenLoaiKiemKe.Text;
            lkki.Huy = this.chkHuy.Checked;
            lkki.MaLoaiKiemKe = this.index;
            return (this.dataid = controller.Update(lkki));
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
                return Nammedia.Medboss.controls.DataType.LoaiKiemKe;
            }
        }
    }
}
