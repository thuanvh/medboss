namespace Nammedia.Medboss.controls
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Windows.Forms;
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using Nammedia.Medboss.Favorite;
    using Nammedia.Medboss.report;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class ChuyenQuayViewOperator : OperatorBase, IFavorizable
    {
        private ChuyenQuayParaManager _paramManager = new ChuyenQuayParaManager();
        private Hashtable _reportParamsMap = new Hashtable();
        private Hashtable _reportViewerHash = new Hashtable();
        private ReportClass _rp;
        private Button butSave;
        private Button butXem;
        private IContainer components = null;
        private QuaySelector denQuaySelector;
        public string DocumentType;
        private GroupBox grpQuay;
        private RadioButton radHaiChieu;
        private RadioButton radMotChieu;
        private CrystalReportViewer rpViewer;
        private SplitContainer splitContainer1;
        private DevComponents.DotNetBar.TabControl tabControl;
        private TabControlPanel tabControlPanel1;
        private TabItem tabItem1;
        private TimeParaser timeParaser;
        private QuaySelector tuQuaySelector;

        public ChuyenQuayViewOperator()
        {
            this.InitializeComponent();
            this.binding();
        }

        protected override void _validate()
        {
            base._validate();
            this.Xem();
        }

        private void binding()
        {
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
                leaf.Id = "rptReportViewOperator_" + this.tabControl.SelectedTab.Text;
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
            this.rpViewer.Navigate += new CrystalDecisions.Windows.Forms.NavigateEventHandler(this.rpViewer_Navigate);
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
            this.grpQuay = new GroupBox();
            this.denQuaySelector = new QuaySelector();
            this.tuQuaySelector = new QuaySelector();
            this.radHaiChieu = new RadioButton();
            this.radMotChieu = new RadioButton();
            this.butXem = new Button();
            this.timeParaser = new TimeParaser();
            this.tabControl = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new TabControlPanel();
            this.tabItem1 = new TabItem(this.components);
            this.rpViewer = new CrystalReportViewer();
            this.butSave = new Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpQuay.SuspendLayout();
            ((ISupportInitialize) this.tabControl).BeginInit();
            this.tabControl.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.butSave);
            this.splitContainer1.Panel1.Controls.Add(this.grpQuay);
            this.splitContainer1.Panel1.Controls.Add(this.butXem);
            this.splitContainer1.Panel1.Controls.Add(this.timeParaser);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new Size(0x2c8, 0x1f0);
            this.splitContainer1.SplitterDistance = 0xad;
            this.splitContainer1.TabIndex = 0;
            this.grpQuay.Controls.Add(this.denQuaySelector);
            this.grpQuay.Controls.Add(this.tuQuaySelector);
            this.grpQuay.Controls.Add(this.radHaiChieu);
            this.grpQuay.Controls.Add(this.radMotChieu);
            this.grpQuay.Location = new Point(0x166, 3);
            this.grpQuay.Name = "grpQuay";
            this.grpQuay.Size = new Size(0x103, 0x86);
            this.grpQuay.TabIndex = 7;
            this.grpQuay.TabStop = false;
            this.grpQuay.Text = "Quầy";
            this.denQuaySelector.Location = new Point(0x88, 20);
            this.denQuaySelector.Name = "denQuaySelector";
            this.denQuaySelector.Size = new Size(0x75, 0x57);
            this.denQuaySelector.TabIndex = 9;
            this.denQuaySelector.Title = "Đến ";
            this.tuQuaySelector.Location = new Point(6, 0x13);
            this.tuQuaySelector.Name = "tuQuaySelector";
            this.tuQuaySelector.Size = new Size(0x75, 0x58);
            this.tuQuaySelector.TabIndex = 8;
            this.tuQuaySelector.Title = "Từ";
            this.radHaiChieu.AutoSize = true;
            this.radHaiChieu.Location = new Point(0x54, 0x6f);
            this.radHaiChieu.Name = "radHaiChieu";
            this.radHaiChieu.Size = new Size(70, 0x11);
            this.radHaiChieu.TabIndex = 8;
            this.radHaiChieu.TabStop = true;
            this.radHaiChieu.Text = "Hai chiều";
            this.radHaiChieu.UseVisualStyleBackColor = true;
            this.radMotChieu.AutoSize = true;
            this.radMotChieu.Checked = true;
            this.radMotChieu.Location = new Point(6, 0x6f);
            this.radMotChieu.Name = "radMotChieu";
            this.radMotChieu.Size = new Size(0x48, 0x11);
            this.radMotChieu.TabIndex = 7;
            this.radMotChieu.TabStop = true;
            this.radMotChieu.Text = "Một chiều";
            this.radMotChieu.UseVisualStyleBackColor = true;
            this.butXem.Location = new Point(0x166, 0x8f);
            this.butXem.Name = "butXem";
            this.butXem.Size = new Size(0x4e, 0x15);
            this.butXem.TabIndex = 2;
            this.butXem.Text = "Xem";
            this.butXem.UseVisualStyleBackColor = true;
            this.butXem.Click += new EventHandler(this.butXem_Click);
            this.timeParaser.Location = new Point(0, 3);
            this.timeParaser.Name = "timeParaser";
            this.timeParaser.Size = new Size(0x160, 0x99);
            this.timeParaser.TabIndex = 0;
            this.tabControl.AutoHideSystemBox = false;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.Controls.Add(this.tabControlPanel1);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabControl.SelectedTabIndex = 0;
            this.tabControl.Size = new Size(0x2c8, 0x13f);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabControl.Tabs.Add(this.tabItem1);
            this.tabControl.TabItemClose += new TabStrip.UserActionEventHandler(this.tabControl_TabItemClose);
            this.tabControlPanel1.Dock = DockStyle.Fill;
            this.tabControlPanel1.Location = new Point(0, 0x1a);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new Padding(1);
            this.tabControlPanel1.Size = new Size(0x2c8, 0x125);
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
            this.rpViewer.ActiveViewIndex = -1;
            this.rpViewer.BackColor = SystemColors.Control;
//            this.rpViewer.BorderStyle = BorderStyle.FixedSingle;
            this.rpViewer.Dock = DockStyle.Fill;
            this.rpViewer.Location = new Point(1, 1);
            this.rpViewer.Name = "rpViewer";
            this.rpViewer.SelectionFormula = "";
            this.rpViewer.Size = new Size(710, 0x13d);
            this.rpViewer.TabIndex = 0;
            this.rpViewer.ViewTimeSelectionFormula = "";
            this.rpViewer.ReportRefresh += new CrystalDecisions.Windows.Forms.RefreshEventHandler(this.rpViewer_ReportRefresh);
            this.rpViewer.Load += new EventHandler(this.rpViewer_Load);
            this.butSave.Location = new Point(0x1c7, 0x8f);
            this.butSave.Name = "butSave";
            this.butSave.Size = new Size(0x4b, 0x17);
            this.butSave.TabIndex = 8;
            this.butSave.Text = "Lưu";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Visible = false;
            this.butSave.Click += new EventHandler(this.butSave_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.splitContainer1);
            base.Name = "ChuyenQuayViewOperator";
            base.Size = new Size(0x2c8, 0x1f0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpQuay.ResumeLayout(false);
            this.grpQuay.PerformLayout();
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
                switch (paras[i].name)
                {
                    case "FromDate":
                        this.timeParaser.setTuNgay(DateTime.Parse(paras[i].value, provider));
                        break;

                    case "ToDate":
                        this.timeParaser.setDenNgay(DateTime.Parse(paras[i].value, provider));
                        break;

                    case "TuQuay":
                        if (paras[i].name == "*")
                        {
                            this.tuQuaySelector.setType(QuayParameter.NhieuQuay);
                        }
                        else
                        {
                            this.tuQuaySelector.setType(QuayParameter.MotQuay);
                            this.tuQuaySelector.setQuay(paras[i].value);
                        }
                        break;

                    case "DenQuay":
                        if (paras[i].name == "*")
                        {
                            this.denQuaySelector.setType(QuayParameter.NhieuQuay);
                        }
                        else
                        {
                            this.denQuaySelector.setType(QuayParameter.MotQuay);
                            this.denQuaySelector.setQuay(paras[i].value);
                        }
                        break;

                    case "OneWay":
                        if (bool.TrueString == paras[i].value)
                        {
                            this.radMotChieu.Checked = true;
                        }
                        else
                        {
                            this.radHaiChieu.Checked = true;
                        }
                        break;
                }
            }
            base.Validate();
        }

        private void rpViewer_Load(object sender, EventArgs e)
        {
            this.Xem();
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
            this.rpViewer.ReportRefresh += new CrystalDecisions.Windows.Forms.RefreshEventHandler(this.rpViewer_ReportRefresh);
            DateTime time = this.timeParaser.getTuNgay();
            DateTime time2 = this.timeParaser.getDenNgay();
            string text = this.tuQuaySelector.getQuay();
            string text2 = this.denQuaySelector.getQuay();
            bool flag = this.radMotChieu.Checked;
            ReportParam param = new ReportParam("TuNgay", time);
            ReportParam param2 = new ReportParam("DenNgay", time2);
            ReportParam param3 = new ReportParam("TuQuay", text);
            ReportParam param4 = new ReportParam("DenQuay", text2);
            ReportParam param5 = new ReportParam("MotChieu", flag);
            this.ParamManager.FromDateParam = param;
            this.ParamManager.TuQuayParam = param3;
            this.ParamManager.ToDateParam = param2;
            this.ParamManager.DenQuayParam = param4;
            this.ParamManager.MotChieu = param5;
            this.ParamManager.ReportViewer = this.rpViewer;
            this.ParamManager.setParams();
            string name = time.ToString("dd/MM/yyyy") + "-" + time2.ToString("dd/MM/yyyy") + ";" + text + "-" + text2;
            TabItem key = base.AddNewTab(this.tabControl, this.rpViewer, name, name);
            if (this.Report != null)
            {
                this.Report.Load();
            }
            this.rpViewer.ReportSource = this.Report;
            this._reportViewerHash.Add(key, this.rpViewer);
            ParamNode node = new ParamNode("FromDate", time.ToString("dd/MM/yyyy"));
            ParamNode node2 = new ParamNode("ToDate", time2.ToString("dd/MM/yyyy"));
            ParamNode node3 = new ParamNode("TuQuay", text);
            ParamNode node4 = new ParamNode("DenQuay", text2);
            ParamNode node5 = new ParamNode("MotChieu", flag.ToString());
            ParamNode[] nodeArray = new ParamNode[] { node, node2, node3, node4, node5 };
            this._reportParamsMap.Add(key, nodeArray);
        }

        public ChuyenQuayParaManager ParamManager
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
                this.rpViewer.ReportSource = this._rp;
            }
        }
    }
}
