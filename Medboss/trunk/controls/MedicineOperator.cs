using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class MedicineOperator : OperatorBase
    {
        private bool _changeMode;
        private Button butSua;
        private Button butThem;
        private Button butXoa;
        private ComboBox cboThuoc;
        private IContainer components;
        private int dataid;
        private GroupBox groupBox1;
        public MedicineUI medUI;
        private RadioButton radioButton2;
        private RadioButton radThem;

        public MedicineOperator()
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
        }

        public MedicineOperator(MedicineInfo mi)
        {
            this.components = null;
            this.InitializeComponent();
            this.loadAC();
            this.medUI.loadData(mi);
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            base.Update();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            base.Insert();
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            MedicineInfo mi = (MedicineInfo) this.cboThuoc.SelectedItem;
            this.setMedicine(mi);
            this.setOperatorState(OperatorState.Edit);
        }

        protected override int delete()
        {
            this.dataid = 0;
            return this.dataid;
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
            this.medUI = new MedicineUI();
            this.butThem = new Button();
            this.butSua = new Button();
            this.butXoa = new Button();
            this.radThem = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.cboThuoc = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.medUI.IsNew = false;
            this.medUI.Location = new Point(9, 0x51);
            this.medUI.Name = "medUI";
            this.medUI.Size = new Size(0xce, 0x1bd);
            this.medUI.TabIndex = 0;
            this.butThem.Location = new Point(0xe7, 0x51);
            this.butThem.Name = "butThem";
            this.butThem.Size = new Size(0x33, 0x17);
            this.butThem.TabIndex = 1;
            this.butThem.Text = "T&h\x00eam";
            this.butThem.UseVisualStyleBackColor = true;
            this.butThem.Click += new EventHandler(this.butThem_Click);
            this.butSua.Location = new Point(0xe7, 110);
            this.butSua.Name = "butSua";
            this.butSua.Size = new Size(0x33, 0x17);
            this.butSua.TabIndex = 2;
            this.butSua.Text = "&Sửa";
            this.butSua.UseVisualStyleBackColor = true;
            this.butSua.Click += new EventHandler(this.butSua_Click);
            this.butXoa.Location = new Point(0xe7, 0x8b);
            this.butXoa.Name = "butXoa";
            this.butXoa.Size = new Size(0x33, 0x17);
            this.butXoa.TabIndex = 3;
            this.butXoa.Text = "&Xo\x00e1";
            this.butXoa.UseVisualStyleBackColor = true;
            this.butXoa.Visible = false;
            this.radThem.AutoSize = true;
            this.radThem.Location = new Point(6, 0x13);
            this.radThem.Name = "radThem";
            this.radThem.Size = new Size(0x47, 0x11);
            this.radThem.TabIndex = 4;
            this.radThem.TabStop = true;
            this.radThem.Text = "Th\x00eam mới";
            this.radThem.UseVisualStyleBackColor = true;
            this.radThem.CheckedChanged += new EventHandler(this.radThem_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(6, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x2c, 0x11);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Sửa";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new Point(0x5c, 0x2a);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new Size(0xbc, 0x15);
            this.cboThuoc.TabIndex = 6;
            this.cboThuoc.SelectedIndexChanged += new EventHandler(this.cboThuoc_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.radThem);
            this.groupBox1.Controls.Add(this.cboThuoc);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x11e, 0x47);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế độ";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.butXoa);
            base.Controls.Add(this.butSua);
            base.Controls.Add(this.butThem);
            base.Controls.Add(this.medUI);
            base.Name = "MedicineOperator";
            base.Size = new Size(0x189, 550);
            base.Load += new EventHandler(this.MedicineOperator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        protected override int insert()
        {
            MedicineController controller = new MedicineController();
            MedicineInfo mi = this.medUI.getMedicine();
            this.dataid = controller.InsertMedicine(mi);
            return this.dataid;
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboThuoc, ref this._medSource);
        }

        private void MedicineOperator_Load(object sender, EventArgs e)
        {
        }

        private void radThem_CheckedChanged(object sender, EventArgs e)
        {
            this.butThem.Enabled = this.radThem.Checked;
            this.butSua.Enabled = !this.radThem.Checked;
            this.medUI.IsNew = this.radThem.Checked;
            this.cboThuoc.Enabled = !this.radThem.Checked;
        }

        public void setMedicine(MedicineInfo mi)
        {
            this.medUI.loadData(mi);
            this.medUI.IsNew = false;
        }

        public void setOperatorState(OperatorState state)
        {
            switch (state)
            {
                case OperatorState.Add:
                    this.butSua.Enabled = false;
                    this.butThem.Enabled = true;
                    this.radThem.Checked = true;
                    this.cboThuoc.Enabled = false;
                    this.medUI.IsNew = true;
                    break;

                case OperatorState.Edit:
                    this.butSua.Enabled = true;
                    this.butThem.Enabled = false;
                    this.radThem.Checked = false;
                    this.cboThuoc.Enabled = true;
                    this.medUI.IsNew = false;
                    break;
            }
        }

        protected override int update()
        {
            MedicineController controller = new MedicineController();
            MedicineInfo mi = this.medUI.getMedicine();
            this.dataid = controller.UpdateMedicine(mi);
            return this.dataid;
        }

        public bool ChangeMode
        {
            get
            {
                return this._changeMode;
            }
            set
            {
                this._changeMode = value;
                this.groupBox1.Enabled = this._changeMode;
            }
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
                return Nammedia.Medboss.controls.DataType.Thuoc;
            }
        }
    }
}
