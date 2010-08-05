namespace Nammedia.Medboss.controls
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class MedicineNoneOperator : OperatorBase
    {
        private Button butRefesh;
        private Button butXoa;
        private Button butXoaHet;
        private DataGridViewTextBoxColumn colDVT;
        private DataGridViewTextBoxColumn colMaThuocTraoDoi;
        private DataGridViewTextBoxColumn colTenThuoc;
        private IContainer components = null;
        private DataGridView dgrThuocNone;
        private ArrayList list = new ArrayList();
        private int maThuocTraoDoi;
        private static bool xoahet;

        public MedicineNoneOperator()
        {
            this.InitializeComponent();
            this.dgrThuocNone.RowEnter += new DataGridViewCellEventHandler(this.dgrThuocNone_RowEnter);
        }

        private void butRefesh_Click(object sender, EventArgs e)
        {
            this.loading();
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            xoahet = false;
            base.Delete();
        }

        private void butXoaHet_Click(object sender, EventArgs e)
        {
            xoahet = true;
            base.Delete();
        }

        protected override int delete()
        {
            if (xoahet)
            {
                this.list = new ArrayList();
                foreach (DataGridViewRow row in (IEnumerable) this.dgrThuocNone.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int num = ConvertHelper.getInt(row.Cells[this.colMaThuocTraoDoi.Index].Value);
                        this.list.Add(num);
                    }
                }
                ThuocUnionMassing.Union();
            }
            new Thread(new ThreadStart(this.deleteThreading)).Start();
            return 0;
        }

        private void deleteThreading()
        {
            MedicineController controller = new MedicineController();
            if (xoahet)
            {
                controller.DeleteMedicine(this.list);
            }
            else
            {
                controller.DeleteMedicine(this.maThuocTraoDoi);
            }
            xoahet = false;
            controller.DeleteMedicine();
            Program.ACSource.ActiveSource = CommonSource.All;
            Program.ACSource.RefreshSource();
        }

        private void dgrThuocNone_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.maThuocTraoDoi = ConvertHelper.getInt(this.dgrThuocNone.Rows[e.RowIndex].Cells[this.colMaThuocTraoDoi.Index].Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        ~MedicineNoneOperator()
        {
        }

        private void InitializeComponent()
        {
            this.dgrThuocNone = new DataGridView();
            this.colTenThuoc = new DataGridViewTextBoxColumn();
            this.colDVT = new DataGridViewTextBoxColumn();
            this.colMaThuocTraoDoi = new DataGridViewTextBoxColumn();
            this.butXoa = new Button();
            this.butXoaHet = new Button();
            this.butRefesh = new Button();
            ((ISupportInitialize) this.dgrThuocNone).BeginInit();
            base.SuspendLayout();
            this.dgrThuocNone.AllowUserToAddRows = false;
            this.dgrThuocNone.AllowUserToDeleteRows = false;
            this.dgrThuocNone.AllowUserToOrderColumns = true;
            this.dgrThuocNone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrThuocNone.Columns.AddRange(new DataGridViewColumn[] { this.colTenThuoc, this.colDVT, this.colMaThuocTraoDoi });
            this.dgrThuocNone.Location = new Point(3, 3);
            this.dgrThuocNone.Name = "dgrThuocNone";
            this.dgrThuocNone.ReadOnly = true;
            this.dgrThuocNone.Size = new Size(0x127, 0x178);
            this.dgrThuocNone.TabIndex = 0;
            this.colTenThuoc.DataPropertyName = "TenThuoc";
            this.colTenThuoc.HeaderText = "T\x00ean thuốc";
            this.colTenThuoc.Name = "colTenThuoc";
            this.colTenThuoc.ReadOnly = true;
            this.colDVT.DataPropertyName = "DVT";
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.ReadOnly = true;
            this.colMaThuocTraoDoi.DataPropertyName = "MaThuocTraoDoi";
            this.colMaThuocTraoDoi.HeaderText = "M\x00e3 thuốc trao đổi";
            this.colMaThuocTraoDoi.Name = "colMaThuocTraoDoi";
            this.colMaThuocTraoDoi.ReadOnly = true;
            this.colMaThuocTraoDoi.Visible = false;
            this.butXoa.Location = new Point(0x173, 0x29);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x4b, 0x17);
            this.butXoa.TabIndex = 1;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Click += new EventHandler(this.butXoa_Click);
            this.butXoaHet.Location = new Point(0x173, 0x54);
            this.butXoaHet.Name = "butXoaHet";
            this.butXoaHet.Size = new Size(0x4b, 0x17);
            this.butXoaHet.TabIndex = 2;
            this.butXoaHet.Text = "X\x00f3a hết";
            this.butXoaHet.UseVisualStyleBackColor = true;
            this.butXoaHet.Click += new EventHandler(this.butXoaHet_Click);
            this.butRefesh.BackgroundImageLayout = ImageLayout.None;
            this.butRefesh.Location = new Point(0x173, 0x85);
            this.butRefesh.Name = "butRefesh";
            this.butRefesh.Size = new Size(0x4b, 0x17);
            this.butRefesh.TabIndex = 3;
            this.butRefesh.Text = "Tải lại";
            this.butRefesh.UseVisualStyleBackColor = true;
            this.butRefesh.Click += new EventHandler(this.butRefesh_Click);
            base.Controls.Add(this.butRefesh);
            base.Controls.Add(this.butXoaHet);
            base.Controls.Add(this.dgrThuocNone);
            base.Controls.Add(this.butXoa);
            base.Name = "MedicineNoneOperator";
            base.Size = new Size(0x21e, 0x185);
            ((ISupportInitialize) this.dgrThuocNone).EndInit();
            base.ResumeLayout(false);
        }

        private void loading()
        {
            DataTable table = new MedicineController().getMedicineNone();
            this.dgrThuocNone.AutoGenerateColumns = false;
            this.dgrThuocNone.DataSource = table;
            this.colMaThuocTraoDoi.Visible = false;
            this.dgrThuocNone.Refresh();
        }
    }
}
