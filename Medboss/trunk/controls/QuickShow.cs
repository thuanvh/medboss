using Nammedia.Medboss;
using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Nammedia.Medboss.controls
{
    public class QuickShow : MedUIBase
    {
        private DataTable _hdtable = new DataTable();
        private DataTable _kktable = new DataTable();
        private System.Windows.Forms.Timer a = new System.Windows.Forms.Timer();
        private Button butXem;
        private ComboBox cboQuay;
        private ComboBox cboThuoc;
        private IContainer components = null;
        private DataGridView dgrKiemKeInfo;
        private DataGridView dgrQuickShow;
        private Label lblQuay;
        private Label lblthuoc;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private string tenQuay = "";
        private string tenThuoc = "";

        public QuickShow()
        {
            this.InitializeComponent();
            this.loadAC();
            this.a.Tick += new EventHandler(this.a_Tick);
            this.a.Interval = 0x3e8;
            this.a.Start();
        }

        private void a_Tick(object sender, EventArgs e)
        {
            this.dgrQuickShow.DataSource = this._hdtable;
            this.dgrQuickShow.AutoGenerateColumns = true;
            this.dgrKiemKeInfo.DataSource = this._kktable;
            this.dgrKiemKeInfo.AutoGenerateColumns = true;
        }

        private void butXem_Click(object sender, EventArgs e)
        {
            this.tenThuoc = this.cboThuoc.Text;
            this.tenQuay = this.cboQuay.Text;
            new Thread(new ThreadStart(this.LoadShow)).Start();
            new Thread(new ThreadStart(this.LoadKK)).Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        ~QuickShow()
        {
            this.a.Stop();
            this.a.Dispose();
        }

        private void InitializeComponent()
        {
            this.cboQuay = new ComboBox();
            this.lblthuoc = new Label();
            this.cboThuoc = new ComboBox();
            this.lblQuay = new Label();
            this.dgrQuickShow = new DataGridView();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.butXem = new Button();
            this.dgrKiemKeInfo = new DataGridView();
            ((ISupportInitialize) this.dgrQuickShow).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dgrKiemKeInfo).BeginInit();
            base.SuspendLayout();
            this.cboQuay.FormattingEnabled = true;
            this.cboQuay.Location = new Point(0x106, 7);
            this.cboQuay.Name = "cboQuay";
            this.cboQuay.Size = new Size(0x8a, 0x15);
            this.cboQuay.TabIndex = 0;
            this.lblthuoc.AutoSize = true;
            this.lblthuoc.Location = new Point(13, 10);
            this.lblthuoc.Name = "lblthuoc";
            this.lblthuoc.Size = new Size(0x26, 13);
            this.lblthuoc.TabIndex = 1;
            this.lblthuoc.Text = "Thuốc";
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new Point(0x39, 7);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new Size(0x98, 0x15);
            this.cboThuoc.TabIndex = 2;
            this.lblQuay.AutoSize = true;
            this.lblQuay.Location = new Point(0xd7, 10);
            this.lblQuay.Name = "lblQuay";
            this.lblQuay.Size = new Size(0x20, 13);
            this.lblQuay.TabIndex = 3;
            this.lblQuay.Text = "Quầy";
            this.dgrQuickShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrQuickShow.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrQuickShow.Dock = DockStyle.Fill;
            this.dgrQuickShow.Location = new Point(3, 0x35);
            this.dgrQuickShow.Name = "dgrQuickShow";
            this.dgrQuickShow.ReadOnly = true;
            this.dgrQuickShow.Size = new Size(0x22e, 0x121);
            this.dgrQuickShow.TabIndex = 4;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrQuickShow, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgrKiemKeInfo, 0, 2);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.Size = new Size(0x234, 0x1a6);
            this.tableLayoutPanel1.TabIndex = 5;
            this.panel1.Controls.Add(this.butXem);
            this.panel1.Controls.Add(this.cboThuoc);
            this.panel1.Controls.Add(this.lblthuoc);
            this.panel1.Controls.Add(this.lblQuay);
            this.panel1.Controls.Add(this.cboQuay);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x22e, 0x2c);
            this.panel1.TabIndex = 5;
            this.butXem.Location = new Point(0x196, 5);
            this.butXem.Name = "butXem";
            this.butXem.Size = new Size(0x3a, 0x17);
            this.butXem.TabIndex = 4;
            this.butXem.Text = "Xem";
            this.butXem.UseVisualStyleBackColor = true;
            this.butXem.Click += new EventHandler(this.butXem_Click);
            this.dgrKiemKeInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrKiemKeInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrKiemKeInfo.Dock = DockStyle.Fill;
            this.dgrKiemKeInfo.Location = new Point(3, 0x15c);
            this.dgrKiemKeInfo.Name = "dgrKiemKeInfo";
            this.dgrKiemKeInfo.ReadOnly = true;
            this.dgrKiemKeInfo.Size = new Size(0x22e, 0x5e);
            this.dgrKiemKeInfo.TabIndex = 6;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "QuickShow";
            base.Size = new Size(0x234, 0x1a6);
            ((ISupportInitialize) this.dgrQuickShow).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.dgrKiemKeInfo).EndInit();
            base.ResumeLayout(false);
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboThuoc, ref this._medSource);
            base._acFactory.EnableAutocomplete(this.cboQuay, ref this._quaySource);
        }

        private void LoadKK()
        {
            KiemKeController controller = new KiemKeController();
            string condition = "TenQuay='" + this.tenQuay + "' and TenThuoc='" + this.tenThuoc + "'";
            this._kktable = ((IFinder) controller).AdvanceFind(condition);
        }

        private void LoadShow()
        {
            this._hdtable = new MedicineController().getMedicineInHD(this.tenThuoc, this.tenQuay);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboThuoc, ref this._medSource);
            base._acFactory.RefreshAutoCompleteSource(this.cboQuay, ref this._quaySource);
        }
    }
}
