namespace Nammedia.Medboss.controls
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Windows.Forms;
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using Nammedia.Medboss.Autocomplete;
    using Nammedia.Medboss.Favorite;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.report;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class ReportViewThanhToanCongNoOperator : OperatorBase, IFavorizable
    {
        private ReportParaManager _paramManager = new ReportParaManager();
        private Hashtable _reportParamsMap = new Hashtable();
        private Hashtable _reportViewerHash = new Hashtable();
        private ReportClass _rp;
        private Button butSave;
        private Button butXem;
        private ComboBox cboNhomCS;
        private IContainer components = null;
        public string DocumentType;
        private Label lblNhomKH;
        private QuaySelector quaySelector;
        private CrystalReportViewer rpViewer;
        private SplitContainer splitContainer1;
        private DevComponents.DotNetBar.TabControl tabControl;
        private TabControlPanel tabControlPanel1;
        private TabItem tabItem1;
        private TimeParaser timeParaser;

        public ReportViewThanhToanCongNoOperator(string documetType)
        {
            this.InitializeComponent();
            this.DocumentType = documetType;
        }

        protected override void _validate()
        {
            base._validate();
            this.Xem();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (this.tabControl.SelectedTab != null)
            {
                ParamNode[] c = ((IFavorizable) this).getParams();
                OperatorNode node = new OperatorNode();
                node.type = this.DocumentType;
                node.paramSequence.AddRange(c);
                XTreeLeaf leaf = new XTreeLeaf();
                leaf.Id = "rptReportViewTTCNOperator_" + this.tabControl.SelectedTab.Text;
                leaf.Name = leaf.Id;
                leaf.Title = leaf.Name;
                leaf.oper = node;
                SaveFunc func = (SaveFunc) base.eventTable["SaveFired"];
                if (func != null)
                {
                    func(leaf);
                }
            }
        }

        private void butXem_Click(object sender, EventArgs e)
        {
            base.Validate();
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
            this.components = new Container();
            this.splitContainer1 = new SplitContainer();
            this.butSave = new Button();
            this.quaySelector = new QuaySelector();
            this.butXem = new Button();
            this.timeParaser = new TimeParaser();
            this.tabControl = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new TabControlPanel();
            this.tabItem1 = new TabItem(this.components);
            this.cboNhomCS = new ComboBox();
            this.lblNhomKH = new Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize) this.tabControl).BeginInit();
            this.tabControl.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.lblNhomKH);
            this.splitContainer1.Panel1.Controls.Add(this.cboNhomCS);
            this.splitContainer1.Panel1.Controls.Add(this.butSave);
            this.splitContainer1.Panel1.Controls.Add(this.quaySelector);
            this.splitContainer1.Panel1.Controls.Add(this.butXem);
            this.splitContainer1.Panel1.Controls.Add(this.timeParaser);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new Size(0x25a, 0x198);
            this.splitContainer1.SplitterDistance = 0xab;
            this.splitContainer1.TabIndex = 0;
            this.butSave.Location = new Point(470, 120);
            this.butSave.Name = "butSave";
            this.butSave.Size = new Size(0x43, 0x17);
            this.butSave.TabIndex = 4;
            this.butSave.Text = "Lưu";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new EventHandler(this.butSave_Click);
            this.quaySelector.Location = new Point(0x184, 3);
            this.quaySelector.Name = "quaySelector";
            this.quaySelector.Size = new Size(0x7b, 0x56);
            this.quaySelector.TabIndex = 3;
            this.quaySelector.Title = "Quầy";
            this.butXem.Location = new Point(0x184, 120);
            this.butXem.Name = "butXem";
            this.butXem.Size = new Size(0x4b, 0x17);
            this.butXem.TabIndex = 2;
            this.butXem.Text = "Xem";
            this.butXem.UseVisualStyleBackColor = true;
            this.butXem.Click += new EventHandler(this.butXem_Click);
            this.timeParaser.Location = new Point(0, 3);
            this.timeParaser.Name = "timeParaser";
            this.timeParaser.Size = new Size(0x176, 0x8a);
            this.timeParaser.TabIndex = 0;
            this.tabControl.AutoHideSystemBox = false;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.Controls.Add(this.tabControlPanel1);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabControl.SelectedTabIndex = 0;
            this.tabControl.Size = new Size(0x25a, 0xe9);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabControl.Tabs.Add(this.tabItem1);
            this.tabControl.TabItemClose += new TabStrip.UserActionEventHandler(this.tabControl_TabItemClose);
            this.tabControlPanel1.Dock = DockStyle.Fill;
            this.tabControlPanel1.Location = new Point(0, 0x1a);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new Padding(1);
            this.tabControlPanel1.Size = new Size(0x25a, 0xcf);
            this.tabControlPanel1.Style.BackColor1.Color = Color.FromArgb(0x90, 0x9c, 0xbb);
            this.tabControlPanel1.Style.BackColor2.Color = Color.FromArgb(230, 0xe9, 240);
            this.tabControlPanel1.Style.Border = eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = SystemColors.ControlDark;
            this.tabControlPanel1.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Right | eBorderSide.Left;
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.cboNhomCS.FormattingEnabled = true;
            this.cboNhomCS.Location = new Point(0x1aa, 0x5d);
            this.cboNhomCS.Name = "cboNhomCS";
            this.cboNhomCS.Size = new Size(0x6f, 0x15);
            this.cboNhomCS.TabIndex = 5;
            this.lblNhomKH.AutoSize = true;
            this.lblNhomKH.Location = new Point(0x181, 0x60);
            this.lblNhomKH.Name = "lblNhomKH";
            this.lblNhomKH.Size = new Size(0x23, 13);
            this.lblNhomKH.TabIndex = 6;
            this.lblNhomKH.Text = "Nh\x00f3m";
            base.Controls.Add(this.splitContainer1);
            base.Name = "ReportViewThanhToanCongNoOperator";
            base.Size = new Size(0x25a, 0x198);
            base.Load += new EventHandler(this.rpViewer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((ISupportInitialize) this.tabControl).EndInit();
            this.tabControl.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        ParamNode[] IFavorizable.getParams()
        {
            return (ParamNode[]) this._reportParamsMap[this.tabControl.SelectedTab];
        }

        void IFavorizable.setParams(ParamNode[] paras)
        {
            DateTimeFormatInfo provider = new CultureInfo("vi-VN", false).DateTimeFormat;
            this.timeParaser.setType(TimeParameter.Duration);
            for (int i = 0; i < paras.Length; i++)
            {
                if (paras[i].name == "FromDate")
                {
                    this.timeParaser.setTuNgay(DateTime.Parse(paras[i].value, provider));
                }
                else if (paras[i].name == "ToDate")
                {
                    this.timeParaser.setDenNgay(DateTime.Parse(paras[i].value, provider));
                }
                else if (paras[i].name == "Quay")
                {
                    if (paras[i].name == "*")
                    {
                        this.quaySelector.setType(QuayParameter.NhieuQuay);
                    }
                    else
                    {
                        this.quaySelector.setType(QuayParameter.MotQuay);
                        this.quaySelector.setQuay(paras[i].value);
                    }
                }
                else if (paras[i].name == "Nhom")
                {
                    this.cboNhomCS.SelectedValue = paras[i].value;
                }
            }
            base.Validate();
        }

        private void rpViewer_Load(object sender, EventArgs e)
        {
            LoaiKhachHang cs = new LoaiKhachHang();
            cs.MaLoai = -1;
            cs.TenLoai = "Tất cả";
            ArrayList list = new ArrayList(base._nhomCS);
            list.Insert(0, cs);
            new AutoCompleteFactory().EnableAutocomplete(this.cboNhomCS, ref list);
        }

        private void rpViewer_Navigate(object source, CrystalDecisions.Windows.Forms.NavigateEventArgs e)
        {
        }

        private void rpViewer_ReportRefresh(object source, ViewerEventArgs e)
        {
            CrystalReportViewer viewer = (CrystalReportViewer) this._reportViewerHash[this.tabControl.SelectedTab];
            PageView view = (PageView) source;
            ReportClass reportSource = (ReportClass) viewer.ReportSource;
            reportSource.Load();
            viewer.ParameterFieldInfo = view.ParameterFieldInfo;
            viewer.ReportSource = reportSource;
            e.Handled = true;
        }

        private void tabControl_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            this.tabControl.Tabs.Remove(this.tabControl.SelectedTab);
        }

        private void Xem()
        {
            this.rpViewer = new CrystalReportViewer();
            DateTime time = this.timeParaser.getTuNgay();
            this.rpViewer.ReportRefresh += new CrystalDecisions.Windows.Forms.RefreshEventHandler(this.rpViewer_ReportRefresh);
            DateTime time2 = this.timeParaser.getDenNgay();
            string text = this.quaySelector.getQuay();
            string nhom = this.cboNhomCS.SelectedValue.ToString();
            if ((this.cboNhomCS.SelectedItem != null) && (this.cboNhomCS.SelectedItem is LoaiKhachHang))
            {
                LoaiKhachHang cs = (LoaiKhachHang) this.cboNhomCS.SelectedItem;
                nhom = (cs.MaLoai == -1) ? "*" : cs.TenLoai;
            }
            ReportParam param = new ReportParam("[FromDate]", time);
            ReportParam param2 = new ReportParam("[ToDate]", time2);
            ReportParam param3 = new ReportParam("Quay", text);
            ReportParam param4 = new ReportParam("Nhom", nhom);
            this.ParamManager.FromDateParam = param;
            this.ParamManager.QuayParam = param3;
            this.ParamManager.ToDateParam = param2;
            this.ParamManager.ReportViewer = this.rpViewer;
            this.ParamManager.NhomCSParam = param4;
            this.ParamManager.setParams();
            string name = time.ToString("dd/MM/yyyy") + "-" + time2.ToString("dd/MM/yyyy") + ";" + text;
            TabItem key = base.AddNewTab(this.tabControl, this.rpViewer, name, name);
            if (this.Report != null)
            {
                this.Report.Load();
            }
            this.rpViewer.ReportSource = this.Report;
            this._reportViewerHash.Add(key, this.rpViewer);
            ParamNode node = new ParamNode("FromDate", time.ToString("dd/MM/yyyy"));
            ParamNode node2 = new ParamNode("ToDate", time2.ToString("dd/MM/yyyy"));
            ParamNode node3 = new ParamNode("Quay", text);
            ParamNode[] nodeArray = new ParamNode[] { node, node2, node3 };
            this._reportParamsMap.Add(key, nodeArray);
        }

        public ReportParaManager ParamManager
        {
            get
            {
                return this._paramManager;
            }
            set
            {
                this._paramManager = value;
            }
        }

        public ReportClass Report
        {
            get
            {
                return this._rp;
            }
            set
            {
                this._rp = value;
            }
        }
    }
}
