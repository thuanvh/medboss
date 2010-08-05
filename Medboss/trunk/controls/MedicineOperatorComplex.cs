using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class MedicineOperatorComplex : OperatorBase
    {
        private ComboBox cboThuocList;
        private IContainer components = null;
        private bool init = true;
        private Label label1;
        private MedicineOperator medOper;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;

        public MedicineOperatorComplex()
        {
            this.init = true;
            this.InitializeComponent();
            this.cboThuocList.SelectedIndexChanged += new EventHandler(this.cboThuocList_SelectedIndexChanged);
            this.cboThuocList.DataSource = base._medSource;
            this.loadAC();
            this.init = false;
        }

        private void cboThuocList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.init)
            {
                MedicineInfo mi = (MedicineInfo) this.cboThuocList.SelectedItem;
                this.medOper.setMedicine(mi);
                this.medOper.setOperatorState(OperatorState.Edit);
            }
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
            this.medOper = new MedicineOperator();
            this.cboThuocList = new ComboBox();
            this.label1 = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.medOper.Location = new Point(3, 0x35);
            this.medOper.Name = "medOper";
            this.medOper.Size = new Size(0x143, 0x1a1);
            this.medOper.TabIndex = 0;
            this.cboThuocList.FormattingEnabled = true;
            this.cboThuocList.Location = new Point(0x76, 3);
            this.cboThuocList.Name = "cboThuocList";
            this.cboThuocList.Size = new Size(0xa4, 0x15);
            this.cboThuocList.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh s\x00e1ch thuốc";
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.medOper, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.Size = new Size(0x20b, 0x20d);
            this.tableLayoutPanel1.TabIndex = 3;
            this.panel1.Controls.Add(this.cboThuocList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x205, 0x2c);
            this.panel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "MedicineOperatorComplex";
            base.Size = new Size(0x20b, 0x20d);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboThuocList, ref this._medSource);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboThuocList, ref this._medSource);
        }
    }
}
