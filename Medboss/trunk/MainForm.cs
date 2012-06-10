using DevComponents.DotNetBar;
using Nammedia.Medboss.controls;
using Nammedia.Medboss.Favorite;
using Nammedia.Medboss.lib;
using Nammedia.Medboss.Log;
using Nammedia.Medboss.Properties;
using Nammedia.Medboss.report;
using Nammedia.Medboss.report.reports.exchange;
using Nammedia.Medboss.report.reports.finance;
using Nammedia.Medboss.report.reports.general;
using Nammedia.Medboss.report.reports.import;
using Nammedia.Medboss.report.reports.payment;
using Nammedia.Medboss.report.reports.repay;
using Nammedia.Medboss.report.reports.sale;
using Nammedia.Medboss.report.reports.verifier;
using Nammedia.Medboss.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Nammedia.Medboss
{


    public class MainForm : Form
    {
        private int _favNodeIndex;
        private FavoriteTree _favTree;
        private string _favXmlFile;
        private IOperator activeOperator;
        private OperatorFunctionType activeOperatorFunctionType;
        private ToolStripPanel BottomToolStripPanel;
        private IContainer components = null;
        private ToolStripContentPanel ContentPanel;
        private FlowLayoutPanel flpMessage;
        private ImageList imgListMenu;
        private ToolStripPanel LeftToolStripPanel;
        private TabItem menuTab;
        private ArrayList message = new ArrayList();
        private ToolStripLabel mnuKetQua;
        private DataType refreshType;
        private ToolStripPanel RightToolStripPanel;
        private SplitContainer splitContainerMain;
        private DevComponents.DotNetBar.TabControl tabContent;
        private TabControlPanel tabControlPanel5;
        private ToolStrip toolStrip1;
        private ToolStripContainer toolStripContainer1;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripPanel TopToolStripPanel;
        private TreeView trvMenu;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem printPreviewToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem indexToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Dictionary<DataType, BackgroundWorker> workerPool = new Dictionary<DataType, BackgroundWorker>();

        public MainForm()
        {
            this.InitializeComponent();
            this._favXmlFile = ConfigurationManager.AppSettings.GetValues("FavorFile")[0];
            this.LoadFavoriteTree();
        }

        private TabItem AddNewTab(Control ctl, string name)
        {
            foreach (TabItem item in this.tabContent.Tabs)
            {
                if (item.Name == name)
                {
                    this.tabContent.SelectedTab = item;
                    return item;
                }
            }
            TabItem item2 = new TabItem();
            this.tabContent.Tabs.Add(item2);
            TabControlPanel panel = new TabControlPanel();
            this.tabContent.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            item2.AttachedControl = panel;
            panel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            panel.TabItem = item2;
            item2.Name = name;
            this.tabContent.CloseButtonVisible = true;
            return item2;
        }

        private void dataInvalid_event(string msg)
        {
            this.flpMessage.Controls.Clear();
            this.flpMessage.Controls.Add(new ErrorLabel());
            Label control = new Label();
            control.Text = msg;
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void deleteFin(object sender, OperatorArgument args)
        {
            this.refreshWorker(args.DataType);
            this.flpMessage.Controls.Clear();
            Label control = new Label();
            control.Text = "Xo\x00e1 th\x00e0nh c\x00f4ng";
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void deleteUnfin()
        {
            this.flpMessage.Controls.Clear();
            Label control = new Label();
            control.Text = "Lỗi: Xo\x00e1 kh\x00f4ng th\x00e0nh c\x00f4ng";
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void eventBinding(IOperator op)
        {
            op.InsertFinished += new InsertFinishHandler(this.insertFin);
            op.InsertUnfinished += new f0_0(this.insertUnfin);
            op.UpdateFinished += new UpdateFinishHandler(this.updateFin);
            op.UpdateUnfinished += new f0_0(this.updateUnfin);
            op.DeleteFinished += new DeleteFinishHandler(this.deleteFin);
            op.DeleteUnfinished += new f0_0(this.deleteUnfin);
            op.DataInvalid += new ValidateHandler(this.dataInvalid_event);
            op.Unfound += new UnfoundHandler(this.unfoundInfo_event);
            op.SaveFired += new SaveFunc(this.op_SaveFired);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Hóa đơn nhập thuốc", 3, 3);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Trả lại thuốc cho hãng", 3, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Thanh toán hóa đơn", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Báo cáo nhập thuốc", 2, 2);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Báo cáo nhập thuốc chi tiết", 1, 1);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Báo cáo trả lại thuốc cho hãng", 2, 2);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Báo cáo trả lại thuốc cho hãng chi tiết", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Báo cáo trả lại thuốc cho hãng nhóm theo hãng", 1, 1);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Báo cáo nhập thuốc từ nhà cung cấp", 2, 2);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Báo cáo quá trình thanh toán nhập", 2, 2);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Báo cáo quá trình thanh toán nhập chi tiết", 2, 2);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Báo cáo công nợ nhập thuốc", 2, 2);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Quản lý nhập thuốc", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Hóa đơn bán thuốc", 3, 3);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Thu bán hàng", 3, 3);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Nhập thuốc trả lại", 3, 3);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Báo cáo bán thuốc chi tiết", 1, 1);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Báo cáo bán thuốc", 2, 2);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Báo cáo bán thuốc chuẩn", 2, 2);
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Báo cáo nhận lại thuốc từ khách hàng", 2, 2);
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Báo cáo chi tiết nhận lại thuốc từ khách hàng", 1, 1);
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Báo cáo bán thuốc - khách hàng", 2, 2);
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Quản lý bán thuốc", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Nhập thông tin thu chi", 3, 3);
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Nhập loại thu chi", 3, 3);
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Báo cáo quỹ (theo ngày)", 1, 1);
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Báo cáo quỹ tổng hợp", 2, 2);
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Báo cáo quỹ (theo khoản thu chi)", 1, 1);
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Quỹ", new System.Windows.Forms.TreeNode[] {
            treeNode24,
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28});
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Báo cáo tổng hợp nhập bán", 2, 2);
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Báo cáo tổng hợp nhập bán chuẩn", 2, 2);
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Báo cáo", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode30,
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Nhập thuốc vào danh mục", 3, 3);
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Cập nhật giá thuốc", 3, 3);
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Hợp thuốc", 3, 3);
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Sửa danh mục", 3, 3);
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Xoá thuốc", 5, 5);
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Nhóm thuốc", 3, 3);
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Thuốc trong hóa đơn", 4, 4);
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Danh mục thuốc", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode37,
            treeNode38,
            treeNode39});
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("Nhập thông tin kiểm kê", 3, 3);
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Loại kiểm kê", 3, 3);
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("Báo cáo kiểm kê theo tên thuốc", 1, 1);
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("Báo cáo kiểm kê theo trang", 1, 1);
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("Báo cáo kiểm kê mẫu", 1, 1);
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("Thuốc tồn", 3, 3);
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("Kiểm kê", new System.Windows.Forms.TreeNode[] {
            treeNode41,
            treeNode42,
            treeNode43,
            treeNode44,
            treeNode45,
            treeNode46});
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("Nhập thông tin luân chuyển", 3, 3);
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("Báo cáo luân chuyển thuốc", 1, 1);
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("Báo cáo luân chuyển thuốc tổng hợp", 1, 1);
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("Luân chuyển thuốc", new System.Windows.Forms.TreeNode[] {
            treeNode48,
            treeNode49,
            treeNode50});
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Tìm kiếm chi tiết", 4, 4);
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("Tìm kiếm", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode52});
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("Quản lý danh sách nhân viên", 3, 3);
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Nhân viên", new System.Windows.Forms.TreeNode[] {
            treeNode54});
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("Quản lý danh sách khách hàng", 3, 3);
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("Nhóm khách hàng", 3, 3);
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Khách hàng", new System.Windows.Forms.TreeNode[] {
            treeNode56,
            treeNode57});
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("Thông tin về Medboss", 2, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuKetQua = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabContent = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel5 = new DevComponents.DotNetBar.TabControlPanel();
            this.trvMenu = new System.Windows.Forms.TreeView();
            this.imgListMenu = new System.Windows.Forms.ImageList(this.components);
            this.menuTab = new DevComponents.DotNetBar.TabItem(this.components);
            this.flpMessage = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabContent)).BeginInit();
            this.tabContent.SuspendLayout();
            this.tabControlPanel5.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.RightToolStripPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuKetQua,
            this.toolStripSeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 22);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 83);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // mnuKetQua
            // 
            this.mnuKetQua.Image = global::Nammedia.Medboss.Properties.Resources.result;
            this.mnuKetQua.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mnuKetQua.Name = "mnuKetQua";
            this.mnuKetQua.Size = new System.Drawing.Size(22, 63);
            this.mnuKetQua.Text = "Kết quả";
            this.mnuKetQua.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.mnuKetQua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnuKetQua.Click += new System.EventHandler(this.mnuKetQua_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(22, 6);
            this.toolStripSeparator.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(637, 481);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabContent);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.flpMessage);
            this.splitContainerMain.Panel2Collapsed = true;
            this.splitContainerMain.Size = new System.Drawing.Size(658, 481);
            this.splitContainerMain.SplitterDistance = 471;
            this.splitContainerMain.TabIndex = 1;
            // 
            // tabContent
            // 
            this.tabContent.AutoHideSystemBox = false;
            this.tabContent.CanReorderTabs = true;
            this.tabContent.CloseButtonVisible = true;
            this.tabContent.Controls.Add(this.tabControlPanel5);
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Location = new System.Drawing.Point(0, 0);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabContent.SelectedTabIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(654, 477);
            this.tabContent.TabIndex = 0;
            this.tabContent.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabContent.Tabs.Add(this.menuTab);
            this.tabContent.TabItemClose += new DevComponents.DotNetBar.TabStrip.UserActionEventHandler(this.tabContent_TabItemClose);
            // 
            // tabControlPanel5
            // 
            this.tabControlPanel5.Controls.Add(this.trvMenu);
            this.tabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel5.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel5.Name = "tabControlPanel5";
            this.tabControlPanel5.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel5.Size = new System.Drawing.Size(654, 451);
            this.tabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.tabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.tabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel5.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.tabControlPanel5.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel5.Style.GradientAngle = 90;
            this.tabControlPanel5.TabIndex = 5;
            this.tabControlPanel5.TabItem = this.menuTab;
            // 
            // trvMenu
            // 
            this.trvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMenu.ImageIndex = 0;
            this.trvMenu.ImageList = this.imgListMenu;
            this.trvMenu.Location = new System.Drawing.Point(1, 1);
            this.trvMenu.Name = "trvMenu";
            treeNode1.ImageIndex = 3;
            treeNode1.Name = "ndHoaDonNhapThuoc";
            treeNode1.SelectedImageIndex = 3;
            treeNode1.Text = "Hóa đơn nhập thuốc";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "ndTraLaiThuocDaNhap";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Text = "Trả lại thuốc cho hãng";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "ndThanhToanHDN";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "Thanh toán hóa đơn";
            treeNode4.ImageIndex = 2;
            treeNode4.Name = "ndBaoCaoNhapThuoc";
            treeNode4.SelectedImageIndex = 2;
            treeNode4.Text = "Báo cáo nhập thuốc";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "ndBaoCaoNhapThuocChiTiet";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "Báo cáo nhập thuốc chi tiết";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "ndBaoCaoTraLaiThuoc";
            treeNode6.SelectedImageIndex = 2;
            treeNode6.Text = "Báo cáo trả lại thuốc cho hãng";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "ndBaoCaoTraLaiThuocChiTiet";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Báo cáo trả lại thuốc cho hãng chi tiết";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "ndBaoCaoTraLaiThuocTheoHang";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Text = "Báo cáo trả lại thuốc cho hãng nhóm theo hãng";
            treeNode9.ImageIndex = 2;
            treeNode9.Name = "ndBaoCaoNhapThuocKhachHang";
            treeNode9.SelectedImageIndex = 2;
            treeNode9.Text = "Báo cáo nhập thuốc từ nhà cung cấp";
            treeNode10.ImageIndex = 2;
            treeNode10.Name = "ndBaoCaoNhapThuoc_ThanhToanLichSu";
            treeNode10.SelectedImageIndex = 2;
            treeNode10.Text = "Báo cáo quá trình thanh toán nhập";
            treeNode11.ImageIndex = 2;
            treeNode11.Name = "ndBaoCaoNhapThuoc_ThanhToanLichSuChiTiet";
            treeNode11.SelectedImageIndex = 2;
            treeNode11.Text = "Báo cáo quá trình thanh toán nhập chi tiết";
            treeNode12.ImageIndex = 2;
            treeNode12.Name = "ndBaoCaoNhapThuoc_ThanhToanChuaXong";
            treeNode12.SelectedImageIndex = 2;
            treeNode12.Text = "Báo cáo công nợ nhập thuốc";
            treeNode13.ImageIndex = 0;
            treeNode13.Name = "ndThuoc";
            treeNode13.Text = "Quản lý nhập thuốc";
            treeNode14.ImageIndex = 3;
            treeNode14.Name = "ndHoaDonBanThuoc";
            treeNode14.SelectedImageIndex = 3;
            treeNode14.Text = "Hóa đơn bán thuốc";
            treeNode15.ImageIndex = 3;
            treeNode15.Name = "ndThuBanHang";
            treeNode15.SelectedImageIndex = 3;
            treeNode15.Text = "Thu bán hàng";
            treeNode16.ImageIndex = 3;
            treeNode16.Name = "ndNhapThuocTraLai";
            treeNode16.SelectedImageIndex = 3;
            treeNode16.Text = "Nhập thuốc trả lại";
            treeNode17.ImageIndex = 1;
            treeNode17.Name = "ndBaoCaoBanThuocChiTiet";
            treeNode17.SelectedImageIndex = 1;
            treeNode17.Text = "Báo cáo bán thuốc chi tiết";
            treeNode18.ImageIndex = 2;
            treeNode18.Name = "ndBaoCaoBanThuoc";
            treeNode18.SelectedImageIndex = 2;
            treeNode18.Text = "Báo cáo bán thuốc";
            treeNode19.ImageIndex = 2;
            treeNode19.Name = "ndBaoCaoBanThuocChuanDVT";
            treeNode19.SelectedImageIndex = 2;
            treeNode19.Text = "Báo cáo bán thuốc chuẩn";
            treeNode20.ImageIndex = 2;
            treeNode20.Name = "ndBaoCaoNhanThuocTraLai";
            treeNode20.SelectedImageIndex = 2;
            treeNode20.Text = "Báo cáo nhận lại thuốc từ khách hàng";
            treeNode21.ImageIndex = 1;
            treeNode21.Name = "ndBaoCaoNhanThuocTraLaiChiTiet";
            treeNode21.SelectedImageIndex = 1;
            treeNode21.Text = "Báo cáo chi tiết nhận lại thuốc từ khách hàng";
            treeNode22.ImageIndex = 2;
            treeNode22.Name = "ndBaoCaoBanThuocKhachHang";
            treeNode22.SelectedImageIndex = 2;
            treeNode22.Text = "Báo cáo bán thuốc - khách hàng";
            treeNode23.Name = "Node2";
            treeNode23.Text = "Quản lý bán thuốc";
            treeNode24.ImageIndex = 3;
            treeNode24.Name = "ndNhapThongTinThuChi";
            treeNode24.SelectedImageIndex = 3;
            treeNode24.Text = "Nhập thông tin thu chi";
            treeNode25.ImageIndex = 3;
            treeNode25.Name = "ndNhapLoaiThuChi";
            treeNode25.SelectedImageIndex = 3;
            treeNode25.Text = "Nhập loại thu chi";
            treeNode26.ImageIndex = 1;
            treeNode26.Name = "ndBaoCaoQuyTheoNgay";
            treeNode26.SelectedImageIndex = 1;
            treeNode26.Text = "Báo cáo quỹ (theo ngày)";
            treeNode27.ImageIndex = 2;
            treeNode27.Name = "ndBaoCaoQuyTongHop";
            treeNode27.SelectedImageIndex = 2;
            treeNode27.Text = "Báo cáo quỹ tổng hợp";
            treeNode28.ImageIndex = 1;
            treeNode28.Name = "ndBaoCaoQuyTheoKhoanThuChi";
            treeNode28.SelectedImageIndex = 1;
            treeNode28.Text = "Báo cáo quỹ (theo khoản thu chi)";
            treeNode29.Name = "Node5";
            treeNode29.Text = "Quỹ";
            treeNode30.ImageIndex = 2;
            treeNode30.Name = "ndBaoCaoTongHopNhapBan";
            treeNode30.SelectedImageIndex = 2;
            treeNode30.Text = "Báo cáo tổng hợp nhập bán";
            treeNode31.ImageIndex = 2;
            treeNode31.Name = "ndBaoCaoTongHopNhapBanChuan";
            treeNode31.SelectedImageIndex = 2;
            treeNode31.Text = "Báo cáo tổng hợp nhập bán chuẩn";
            treeNode32.ImageIndex = 2;
            treeNode32.Name = "ndBaoCao";
            treeNode32.SelectedImageIndex = 2;
            treeNode32.Text = "Báo cáo";
            treeNode33.ImageIndex = 3;
            treeNode33.Name = "ndNhapThuocVaoDanhMuc";
            treeNode33.SelectedImageIndex = 3;
            treeNode33.Text = "Nhập thuốc vào danh mục";
            treeNode34.ImageIndex = 3;
            treeNode34.Name = "ndCapNhatGiaThuoc";
            treeNode34.SelectedImageIndex = 3;
            treeNode34.Text = "Cập nhật giá thuốc";
            treeNode35.ImageIndex = 3;
            treeNode35.Name = "ndChuyenThuoc";
            treeNode35.SelectedImageIndex = 3;
            treeNode35.Text = "Hợp thuốc";
            treeNode36.ImageIndex = 3;
            treeNode36.Name = "ndSuaDanhMuc";
            treeNode36.SelectedImageIndex = 3;
            treeNode36.Text = "Sửa danh mục";
            treeNode37.ImageIndex = 5;
            treeNode37.Name = "ndXoaThuoc";
            treeNode37.SelectedImageIndex = 5;
            treeNode37.Text = "Xoá thuốc";
            treeNode38.ImageIndex = 3;
            treeNode38.Name = "ndLoaiThuoc";
            treeNode38.SelectedImageIndex = 3;
            treeNode38.Text = "Nhóm thuốc";
            treeNode39.ImageIndex = 4;
            treeNode39.Name = "ndThuocTrongHoaDon";
            treeNode39.SelectedImageIndex = 4;
            treeNode39.Text = "Thuốc trong hóa đơn";
            treeNode40.Name = "ndDanhMucThuoc";
            treeNode40.Text = "Danh mục thuốc";
            treeNode41.ImageIndex = 3;
            treeNode41.Name = "ndNhapThongTinKiemKe";
            treeNode41.SelectedImageIndex = 3;
            treeNode41.Text = "Nhập thông tin kiểm kê";
            treeNode42.ImageIndex = 3;
            treeNode42.Name = "ndLoaiKiemKe";
            treeNode42.SelectedImageIndex = 3;
            treeNode42.Text = "Loại kiểm kê";
            treeNode43.ImageIndex = 1;
            treeNode43.Name = "ndBaoCaoKiemKe";
            treeNode43.SelectedImageIndex = 1;
            treeNode43.Text = "Báo cáo kiểm kê theo tên thuốc";
            treeNode44.ImageIndex = 1;
            treeNode44.Name = "ndBaoCaoKiemKeMaKK";
            treeNode44.SelectedImageIndex = 1;
            treeNode44.Text = "Báo cáo kiểm kê theo trang";
            treeNode45.ImageIndex = 1;
            treeNode45.Name = "ndBaoCaoKiemKeMau";
            treeNode45.SelectedImageIndex = 1;
            treeNode45.Text = "Báo cáo kiểm kê mẫu";
            treeNode46.ImageIndex = 3;
            treeNode46.Name = "ndThuocTon";
            treeNode46.SelectedImageIndex = 3;
            treeNode46.Text = "Thuốc tồn";
            treeNode47.Name = "ndKiemKe";
            treeNode47.Text = "Kiểm kê";
            treeNode48.ImageIndex = 3;
            treeNode48.Name = "ndNhapThongTinChuyenQuay";
            treeNode48.SelectedImageIndex = 3;
            treeNode48.Text = "Nhập thông tin luân chuyển";
            treeNode49.ImageIndex = 1;
            treeNode49.Name = "ndBaoCaoLuanChuyenThuoc";
            treeNode49.SelectedImageIndex = 1;
            treeNode49.Text = "Báo cáo luân chuyển thuốc";
            treeNode50.ImageIndex = 1;
            treeNode50.Name = "ndBaoCaoLuanChuyenThuocTongHop";
            treeNode50.SelectedImageIndex = 1;
            treeNode50.Text = "Báo cáo luân chuyển thuốc tổng hợp";
            treeNode51.Name = "ndChuyenQuay";
            treeNode51.Text = "Luân chuyển thuốc";
            treeNode52.ImageIndex = 4;
            treeNode52.Name = "ndTimKiemChiTiet";
            treeNode52.SelectedImageIndex = 4;
            treeNode52.Text = "Tìm kiếm chi tiết";
            treeNode53.ImageIndex = 4;
            treeNode53.Name = "ndTimKiem";
            treeNode53.SelectedImageIndex = 4;
            treeNode53.Text = "Tìm kiếm";
            treeNode54.ImageIndex = 3;
            treeNode54.Name = "ndQuanLyDanhSachNhanVien";
            treeNode54.SelectedImageIndex = 3;
            treeNode54.Text = "Quản lý danh sách nhân viên";
            treeNode55.Name = "ndNhanVien";
            treeNode55.Text = "Nhân viên";
            treeNode56.ImageIndex = 3;
            treeNode56.Name = "ndQuanLyDanhSachKhachHang";
            treeNode56.SelectedImageIndex = 3;
            treeNode56.Text = "Quản lý danh sách khách hàng";
            treeNode57.ImageIndex = 3;
            treeNode57.Name = "ndNhomKhachHang";
            treeNode57.SelectedImageIndex = 3;
            treeNode57.Text = "Nhóm khách hàng";
            treeNode58.Name = "ndKhachHang";
            treeNode58.Text = "Khách hàng";
            treeNode59.ImageIndex = 2;
            treeNode59.Name = "ndAbout";
            treeNode59.SelectedImageIndex = 2;
            treeNode59.Text = "Thông tin về Medboss";
            this.trvMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode23,
            treeNode29,
            treeNode32,
            treeNode40,
            treeNode47,
            treeNode51,
            treeNode53,
            treeNode55,
            treeNode58,
            treeNode59});
            this.trvMenu.SelectedImageIndex = 0;
            this.trvMenu.Size = new System.Drawing.Size(652, 449);
            this.trvMenu.TabIndex = 0;
            this.trvMenu.DoubleClick += new System.EventHandler(this.menuTree_DoubleClick);
            this.trvMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.menuTree_KeyPress);
            // 
            // imgListMenu
            // 
            this.imgListMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMenu.ImageStream")));
            this.imgListMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListMenu.Images.SetKeyName(0, "48report (1).jpg");
            this.imgListMenu.Images.SetKeyName(1, "reports.png");
            this.imgListMenu.Images.SetKeyName(2, "opcreporticon64.jpg");
            this.imgListMenu.Images.SetKeyName(3, "pencil.png");
            this.imgListMenu.Images.SetKeyName(4, "Search.png");
            this.imgListMenu.Images.SetKeyName(5, "Report.png");
            this.imgListMenu.Images.SetKeyName(6, "report_check.png");
            this.imgListMenu.Images.SetKeyName(7, "task-report-regular.png");
            this.imgListMenu.Images.SetKeyName(8, "Report.png");
            this.imgListMenu.Images.SetKeyName(9, "icon_report.jpg");
            this.imgListMenu.Images.SetKeyName(10, "48report.jpg");
            this.imgListMenu.Images.SetKeyName(11, "Sales report.png");
            // 
            // menuTab
            // 
            this.menuTab.AttachedControl = this.tabControlPanel5;
            this.menuTab.Name = "menuTab";
            this.menuTab.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Default;
            this.menuTab.Text = "Menu";
            // 
            // flpMessage
            // 
            this.flpMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMessage.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMessage.Location = new System.Drawing.Point(0, 0);
            this.flpMessage.Name = "flpMessage";
            this.flpMessage.Size = new System.Drawing.Size(92, 96);
            this.flpMessage.TabIndex = 0;
            this.flpMessage.Leave += new System.EventHandler(this.Right_leave);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(658, 481);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.Size = new System.Drawing.Size(682, 481);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(682, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator4,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(682, 481);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Medboss";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabContent)).EndInit();
            this.tabContent.ResumeLayout(false);
            this.tabControlPanel5.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.RightToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.RightToolStripPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        private ImportOrderUIOperator initImportOrderOperator()
        {
            ImportOrderUIOperator @operator = new ImportOrderUIOperator();
            @operator.InsertFinished += new InsertFinishHandler(this.insertFin);
            @operator.DataInvalid += new ValidateHandler(this.dataInvalid_event);
            @operator.Unfound += new UnfoundHandler(this.unfoundInfo_event);
            @operator.SelectedItemView += new ViewFunc(this.viewDetails);
            return @operator;
        }

        private SellOrderOperator initSellOrderOperator()
        {
            SellOrderOperator @operator = new SellOrderOperator();
            @operator.InsertFinished += new InsertFinishHandler(this.insertFin);
            @operator.DataInvalid += new ValidateHandler(this.dataInvalid_event);
            @operator.Unfound += new UnfoundHandler(this.unfoundInfo_event);
            return @operator;
        }

        private void insertFin(object sender, OperatorArgument args)
        {
            this.refreshWorker(args.DataType);
            this.flpMessage.Controls.Clear();
            Label control = new InsertSuccessfulLabel();
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void insertUnfin()
        {
            this.flpMessage.Controls.Clear();
            Label control = new Label();
            control.Text = "Lỗi: Th\x00eam mới kh\x00f4ng th\x00e0nh c\x00f4ng";
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void LoadFavoriteTree()
        {
            TreeNode node;
            try
            {
                this._favTree = new FavoriteTree(this._favXmlFile);
                node = this._favTree.getTreeNode(7, 1);
                this._favNodeIndex = this.trvMenu.Nodes.Add(node);
            }
            catch (IOException ioexc)
            {
                try
                {
                    this._favTree = new FavoriteTree();
                    this._favTree.WriteXML(this._favXmlFile);
                    node = this._favTree.getTreeNode(7, 1);
                    this._favNodeIndex = this.trvMenu.Nodes.Add(node);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Kh\x00f4ng thể tải:" + this._favXmlFile);
                    LogManager.LogException(exc);
                }
                LogManager.LogException(ioexc);
            }
        }

        private void menuTree_DoubleClick(object sender, EventArgs e)
        {
            this.newTask((TreeView) sender);
        }

        private void menuTree_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.newTask((TreeView) sender);
            }
        }

        private void mnuKetQua_Click(object sender, EventArgs e)
        {
            this.splitContainerMain.Panel2Collapsed = !this.splitContainerMain.Panel2Collapsed;
        }

        private void newTask(TreeView tree)
        {
            TreeNode selectedNode = tree.SelectedNode;
            TabItem item = new TabItem();
            switch (selectedNode.Name)
            {
                case "ndHoaDonBanThuoc":
                    item = this.AddNewTab(this.initSellOrderOperator(), selectedNode.Name);
                    item.Text = "Ho\x00e1 đơn b\x00e1n thuốc";
                    break;

                case "ndHoaDonNhapThuoc":
                    item = this.AddNewTab(this.initImportOrderOperator(), selectedNode.Name);
                    item.Text = "Ho\x00e1 đơn nhập thuốc";
                    break;

                case "ndNhapThongTinThuChi":
                {
                    QuyUIOperator op = new QuyUIOperator();
                    this.eventBinding(op);
                    item = this.AddNewTab(op, selectedNode.Name);
                    break;
                }
                case "ndThuBanHang":
                {
                    ThuBanHangOperator operator2 = new ThuBanHangOperator();
                    this.eventBinding(operator2);
                    item = this.AddNewTab(operator2, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuoc":
                {
                    ReportViewOperator operator3 = new ReportViewOperator("BaoCaoNhapThuoc");
                    operator3.Report = new rptHangNhap();
                    this.eventBinding(operator3);
                    item = this.AddNewTab(operator3, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoBanThuoc":
                {
                    ReportViewOperator operator4 = new ReportViewOperator("BaoCaoBanThuoc");
                    operator4.Report = new rptHangBan();
                    this.eventBinding(operator4);
                    item = this.AddNewTab(operator4, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoBanThuocChuanDVT":
                {
                    ReportViewOperator rvorptHangBanChuan = new ReportViewOperator("BaoCaoBanThuocChuanDVT");
                    rvorptHangBanChuan.Report = new rptHangBanChuan();
                    rvorptHangBanChuan.ParamManager = new BanHangChuanParaManager();
                    this.eventBinding(rvorptHangBanChuan);
                    item = this.AddNewTab(rvorptHangBanChuan, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoBanThuocKhachHang":
                {
                    ReportViewOperator operator5 = new ReportViewOperator("BaoCaoBanThuocKhachHang");
                    operator5.Report = new rptBanThuocKhachHang();
                    operator5.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator5);
                    item = this.AddNewTab(operator5, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuocKhachHang":
                {
                    ReportViewOperator operator6 = new ReportViewOperator("BaoCaoNhapThuocKhachHang");
                    operator6.Report = new rptNhapThuocKhachHang();
                    operator6.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator6);
                    item = this.AddNewTab(operator6, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuoc_ThanhToanLichSu":
                {
                    ReportViewThanhToanCongNoOperator oprpt_TTLS = new ReportViewThanhToanCongNoOperator("BaoCaoNhapThuoc_ThanhToanLichSu");
                    oprpt_TTLS.Report = new rptThanhToan_QuaTrinh();
                    oprpt_TTLS.ParamManager = new ThanhToanParaManager();
                    this.eventBinding(oprpt_TTLS);
                    item = this.AddNewTab(oprpt_TTLS, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuoc_ThanhToanLichSuChiTiet":
                {
                    ReportViewThanhToanCongNoOperator oprpt_TTLSCT = new ReportViewThanhToanCongNoOperator("BaoCaoNhapThuoc_ThanhToanLichSuChiTiet");
                    oprpt_TTLSCT.Report = new rptThanhToan_ChiTiet();
                    oprpt_TTLSCT.ParamManager = new ThanhToanParaManager();
                    this.eventBinding(oprpt_TTLSCT);
                    item = this.AddNewTab(oprpt_TTLSCT, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuoc_ThanhToanChuaXong":
                {
                    ReportViewThanhToanCongNoOperator oprpt_TTCX = new ReportViewThanhToanCongNoOperator("ndBaoCaoNhapThuoc_ThanhToanChuaXong");
                    oprpt_TTCX.Report = new rptThanhToan_CongNo();
                    oprpt_TTCX.ParamManager = new ThanhToanCXParaManager();
                    this.eventBinding(oprpt_TTCX);
                    item = this.AddNewTab(oprpt_TTCX, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoQuyTheoNgay":
                {
                    ReportViewOperator operator7 = new ReportViewOperator("BaoCaoQuy");
                    operator7.Report = new rptQuy_Date();
                    this.eventBinding(operator7);
                    item = this.AddNewTab(operator7, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoQuyTheoKhoanThuChi":
                {
                    ReportViewOperator operator8 = new ReportViewOperator("BaoCaoQuy");
                    operator8.Report = new rptQuy();
                    this.eventBinding(operator8);
                    item = this.AddNewTab(operator8, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoQuyTongHop":
                {
                    ReportViewOperator operator9 = new ReportViewOperator("BaoCaoQuyTongHop");
                    operator9.Report = new rptQuyTongHopModif();
                    this.eventBinding(operator9);
                    item = this.AddNewTab(operator9, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoTongHopNhapBan":
                {
                    ReportViewOperator operator10 = new ReportViewOperator("BaoCaoTongHopNhapBan");
                    operator10.Report = new rptTongHopNhapBan();
                    operator10.ParamManager = new TongHopParaManager();
                    this.eventBinding(operator10);
                    item = this.AddNewTab(operator10, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoTongHopNhapBanChuan":
                {
                    ReportViewOperator operator11 = new ReportViewOperator("BaoCaoTongHopNhapBanChuan");
                    operator11.Report = new rptTongHopNhapBanStandard();
                    operator11.ParamManager = new TongHopParaManager();
                    this.eventBinding(operator11);
                    item = this.AddNewTab(operator11, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoHangTon":
                {
                    ReportViewOperator operator12 = new ReportViewOperator("BaoCaoHangTon");
                    operator12.Report = new rptHangTon();
                    operator12.ParamManager = new TongHopParaManager();
                    this.eventBinding(operator12);
                    item = this.AddNewTab(operator12, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoBanThuocChiTiet":
                {
                    ReportViewOperator operator13 = new ReportViewOperator("BaoCaoBanThuocChiTiet");
                    operator13.Report = new rptBanThuocChiTiet();
                    operator13.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator13);
                    item = this.AddNewTab(operator13, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhapThuocChiTiet":
                {
                    ReportViewOperator operator14 = new ReportViewOperator("BaoCaoNhapThuocChiTiet");
                    operator14.Report = new rptNhapThuocChiTiet();
                    operator14.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator14);
                    item = this.AddNewTab(operator14, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoKiemKe":
                {
                    ReportViewOperator operator15 = new ReportViewOperator("BaoCaoKiemKe");
                    operator15.Report = new rptKiemKe();
                    operator15.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator15);
                    item = this.AddNewTab(operator15, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoKiemKeMau":
                {
                    ReportViewOperator operator15Mau = new ReportViewOperator("BaoCaoKiemKeMau");
                    operator15Mau.Report = new rptKiemKeMau();
                    operator15Mau.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator15Mau);
                    item = this.AddNewTab(operator15Mau, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoKiemKeMaKK":
                {
                    ReportViewOperator operator15OrderByMaKK = new ReportViewOperator("BaoCaoKiemKeMaKK");
                    operator15OrderByMaKK.Report = new rptKiemKeOrderByMaKK();
                    operator15OrderByMaKK.ParamManager = new KiemKeParaManager();
                    this.eventBinding(operator15OrderByMaKK);
                    item = this.AddNewTab(operator15OrderByMaKK, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoTraLaiThuocChiTiet":
                {
                    ReportViewOperator operator16 = new ReportViewOperator("BaoCaoTraLaiThuocChiTiet");
                    operator16.Report = new rptTraLaiChiTiet();
                    operator16.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Thu);
                    this.eventBinding(operator16);
                    item = this.AddNewTab(operator16, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoTraLaiThuocTheoHang":
                {
                    ReportViewOperator operatortlh = new ReportViewOperator("BaoCaoTraLaiThuocTheoHang");
                    operatortlh.Report = new rptTraLaiNhaCungCap();
                    operatortlh.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Thu);
                    this.eventBinding(operatortlh);
                    item = this.AddNewTab(operatortlh, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoTraLaiThuoc":
                {
                    ReportViewOperator operator17 = new ReportViewOperator("BaoCaoTraLaiThuoc");
                    operator17.Report = new rptTraLai();
                    operator17.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Thu);
                    this.eventBinding(operator17);
                    item = this.AddNewTab(operator17, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhanThuocTraLai":
                {
                    ReportViewOperator operator18 = new ReportViewOperator("BaoCaoNhanThuocTraLai");
                    operator18.Report = new rptTraLai();
                    operator18.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Chi);
                    this.eventBinding(operator18);
                    item = this.AddNewTab(operator18, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoNhanThuocTraLaiChiTiet":
                {
                    ReportViewOperator operator19 = new ReportViewOperator("BaoCaoNhanThuocTraLaiChiTiet");
                    operator19.Report = new rptTraLaiChiTiet();
                    operator19.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Chi);
                    this.eventBinding(operator19);
                    item = this.AddNewTab(operator19, selectedNode.Name);
                    break;
                }
                case "ndNhapThuocVaoDanhMuc":
                {
                    MedicineListOperator operator20 = new MedicineListOperator();
                    this.eventBinding(operator20);
                    item = this.AddNewTab(operator20, selectedNode.Name);
                    break;
                }
                case "ndCapNhatGiaThuoc":
                {
                    MedicinePriceOperator operator21 = new MedicinePriceOperator();
                    this.eventBinding(operator21);
                    item = this.AddNewTab(operator21, selectedNode.Name);
                    break;
                }
                case "ndNhapThongTinKiemKe":
                {
                    KiemKeUIOperator operator22 = new KiemKeUIOperator();
                    this.eventBinding(operator22);
                    item = this.AddNewTab(operator22, selectedNode.Name);
                    break;
                }
                case "ndNhapLoaiThuChi":
                {
                    LoaiThuChiUIOperator operator23 = new LoaiThuChiUIOperator();
                    this.eventBinding(operator23);
                    item = this.AddNewTab(operator23, selectedNode.Name);
                    break;
                }
                case "ndTimKiem":
                {
                    Searcher searcher = new Searcher();
                    this.eventBinding(searcher);
                    searcher.SelectedItemView += new ViewFunc(this.viewDetails);
                    item = this.AddNewTab(searcher, selectedNode.Name);
                    break;
                }
                case "ndNhapThongTinChuyenQuay":
                {
                    ChuyenQuayUIOperator operator24 = new ChuyenQuayUIOperator();
                    this.eventBinding(operator24);
                    item = this.AddNewTab(operator24, selectedNode.Name);
                    break;
                }
                case "ndNhapThuocTraLai":
                {
                    TraLaiBanUIOperator operator25 = new TraLaiBanUIOperator(KhoanThuChiInfo.Chi);
                    this.eventBinding(operator25);
                    item = this.AddNewTab(operator25, selectedNode.Name);
                    break;
                }
                case "ndTraLaiThuocDaNhap":
                {
                    TraLaiNhapUIOperator operator26 = new TraLaiNhapUIOperator(KhoanThuChiInfo.Thu);
                    this.eventBinding(operator26);
                    item = this.AddNewTab(operator26, selectedNode.Name);
                    break;
                }
                case "ndThanhToanHDN":
                {
                    ThanhToanListOperator opThanhtoan = new ThanhToanListOperator();
                    this.eventBinding(opThanhtoan);
                    item = this.AddNewTab(opThanhtoan, selectedNode.Name);
                    break;
                }
                case "ndChuyenThuoc":
                {
                    MedicineUnion union = new MedicineUnion();
                    this.eventBinding(union);
                    item = this.AddNewTab(union, selectedNode.Name);
                    break;
                }
                case "ndTimKiemChiTiet":
                {
                    AdvanceSearcher searcher2 = new AdvanceSearcher();
                    this.eventBinding(searcher2);
                    searcher2.SelectedItemView += new ViewFunc(this.viewDetails);
                    item = this.AddNewTab(searcher2, selectedNode.Name);
                    break;
                }
                case "ndSuaDanhMuc":
                {
                    MedicineOperator operator27 = new MedicineOperator();
                    this.eventBinding(operator27);
                    item = this.AddNewTab(operator27, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoLuanChuyenThuoc":
                {
                    ChuyenQuayViewOperator operator28 = new ChuyenQuayViewOperator();
                    operator28.Report = new rptChuyenQuayChiTiet();
                    operator28.ParamManager = new ChuyenQuayParaManager();
                    this.eventBinding(operator28);
                    item = this.AddNewTab(operator28, selectedNode.Name);
                    break;
                }
                case "ndBaoCaoLuanChuyenThuocTongHop":
                {
                    ChuyenQuayViewOperator operator28_b = new ChuyenQuayViewOperator();
                    operator28_b.Report = new rptChuyenQuayTongHop();
                    operator28_b.ParamManager = new ChuyenQuayParaManager();
                    this.eventBinding(operator28_b);
                    item = this.AddNewTab(operator28_b, selectedNode.Name);
                    break;
                }
                case "ndXoaThuoc":
                {
                    MedicineNoneOperator operator29 = new MedicineNoneOperator();
                    this.eventBinding(operator29);
                    item = this.AddNewTab(operator29, selectedNode.Name);
                    break;
                }
                case "ndQuanLyDanhSachNhanVien":
                {
                    NhanVienListOperator operator30 = new NhanVienListOperator();
                    this.eventBinding(operator30);
                    item = this.AddNewTab(operator30, selectedNode.Name);
                    break;
                }
                case "ndQuanLyDanhSachKhachHang":
                {
                    CSListOperator operator31 = new CSListOperator();
                    this.eventBinding(operator31);
                    item = this.AddNewTab(operator31, selectedNode.Name);
                    break;
                }
                case "ndLoaiKiemKe":
                {
                    LoaiKiemKeUIOperator operator32 = new LoaiKiemKeUIOperator();
                    this.eventBinding(operator32);
                    item = this.AddNewTab(operator32, selectedNode.Name);
                    break;
                }
                case "ndLoaiThuoc":
                {
                    LoaiThuocUIOperator operator33 = new LoaiThuocUIOperator();
                    this.eventBinding(operator33);
                    item = this.AddNewTab(operator33, selectedNode.Name);
                    break;
                }
                case "ndAbout":
                    new AboutBox1().ShowDialog();
                    return;

                case "ndNhomKhachHang":
                {
                    LoaiDoiTacUIOperator operator34 = new LoaiDoiTacUIOperator();
                    this.eventBinding(operator34);
                    item = this.AddNewTab(operator34, selectedNode.Name);
                    break;
                }
                case "ndThuocTrongHoaDon":
                {
                    QuickShow ctl = new QuickShow();
                    item = this.AddNewTab(ctl, selectedNode.Name);
                    break;
                }
                case "ndThuocTon":
                {
                    ThuocTonUI nui = new ThuocTonUI();
                    this.eventBinding(nui);
                    item = this.AddNewTab(nui, selectedNode.Name);
                    break;
                }
                default:
                    try
                    {
                        OperatorNode operNode = (OperatorNode) this._favTree.OperatorMap[selectedNode.Name];
                        OperatorBase base2 = OperatorFactory.LoadOperator(operNode);
                        this.eventBinding(base2);
                        item = this.AddNewTab(base2, selectedNode.Name);
                    }
                    catch (Exception exc)
                    {
                        LogManager.LogException(exc);
                        return;
                    }
                    break;
            }
            item.Text = selectedNode.Text;
            this.tabContent.SelectedTab = item;
        }

        private void op_SaveFired(XTreeNode node)
        {
            FavoriteEditor editor = new FavoriteEditor(ref this._favTree, node);
            editor.setImageList(this.imgListMenu);
            editor.setLeafImageIndex(1);
            editor.setFolderImageIndex(7);
            editor.refreshTree();
            if (editor.ShowDialog() == DialogResult.OK)
            {
                this.trvMenu.Nodes.RemoveAt(this._favNodeIndex);
                this._favNodeIndex = this.trvMenu.Nodes.Add(this._favTree.getTreeNode(7, 1));
                this._favTree.WriteXML(this._favXmlFile);
            }
        }

        private void refreshWorker(DataType type)
        {
            this.refreshType = type;
            BackgroundWorker worker = null;
            if (this.workerPool.ContainsKey(type))
            {
                worker = this.workerPool[type];
                if (worker != null)
                {
                    worker.CancelAsync();
                    worker.Dispose();
                }
            }
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
            if (this.workerPool.ContainsKey(type))
            {
                this.workerPool[type] = worker;
            }
            else
            {
                this.workerPool.Add(type, worker);
            }
        }

        private void Right_leave(object sender, EventArgs e)
        {
        }

        private void tabContent_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            if (this.tabContent.SelectedTab != this.menuTab)
            {
                this.tabContent.Tabs.Remove(this.tabContent.SelectedTab);
            }
        }

        private void unfoundInfo_event(object sender, UnknownValueException e)
        {
            DVT dvt;
            this.activeOperator = e.Operator;
            this.activeOperatorFunctionType = e.OperatorFunctionType;
            this.flpMessage.Controls.Clear();
            UnfoundArgs args = e.args;
            Panel panel = this.splitContainerMain.Panel2;
            this.flpMessage.FlowDirection = FlowDirection.TopDown;
            this.flpMessage.Controls.Add(new ErrorLabel());
            Label control = new Label();
            control.Text = e.Message();
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            switch (args.Type)
            {
                case UnfoundType.Thuoc:
                {
                    Label label5 = new Label();
                    label5.Text = "Bạn c\x00f3 thể th\x00eam thuốc mới bằng c\x00e1ch điền c\x00e1c th\x00f4ng tin b\x00ean dưới.\n H\x00e3y chắc chắn rằng đ\x00e2y l\x00e0 loại thuốc mới.";
                    AutoSizeRender.autoSizeControl(label5, 10);
                    this.flpMessage.Controls.Add(label5);
                    MedicineInfo mi = new MedicineInfo();
                    foreach (UnfoundArg arg2 in args.fieldValue)
                    {
                        switch (arg2.Key)
                        {
                            case FieldKey.TenThuoc:
                                mi.TenThuoc = arg2.Value;
                                break;

                            case FieldKey.TenDVT:
                            {
                                dvt = new DVT();
                                dvt.TenDV = arg2.Value;
                                ThuocTraoDoi doi2 = new ThuocTraoDoi();
                                doi2.TiLe = 1;
                                doi2.DVT = dvt;
                                mi.ThuocTraoDois.Add(doi2);
                                break;
                            }
                        }
                    }
                    MedicineOperator op = new MedicineOperator(mi);
                    op.setOperatorState(OperatorState.Add);
                    op.ChangeMode = false;
                    this.eventBinding(op);
                    this.flpMessage.Controls.Add(op);
                    break;
                }
                case UnfoundType.Quay:
                {
                    Label label2 = new Label();
                    label2.Text = "T\x00ean quầy kh\x00f4ng đ\x00fang.";
                    panel.Controls.Add(label2);
                    Label label3 = new Label();
                    label3.Text = "Bạn c\x00f3 thể th\x00eam quầy mới bằng c\x00e1ch điền th\x00f4ng tin ở b\x00ean dưới";
                    AutoSizeRender.autoSizeControl(label3, 10);
                    panel.Controls.Add(label3);
                    QuayUIOperator @operator = new QuayUIOperator(OperatorType.Insert);
                    @operator.quayUI.txtTenQuay.Text = ((UnfoundArg) args.fieldValue[0]).Value;
                    this.eventBinding(@operator);
                    this.flpMessage.Controls.Add(@operator);
                    break;
                }
                case UnfoundType.NV:
                {
                    NhanVienInfo nvi = new NhanVienInfo();
                    nvi.Ten = ((UnfoundArg) args.fieldValue[0]).Value;
                    NhanVienUIOperator operator5 = new NhanVienUIOperator(nvi);
                    this.eventBinding(operator5);
                    this.flpMessage.Controls.Add(operator5);
                    break;
                }
                case UnfoundType.DoiTac:
                {
                    CSInfo csi = new CSInfo();
                    UnfoundArg arg = (UnfoundArg) args.fieldValue[0];
                    csi.Ten = arg.Value;
                    CSInfoUIOperator operator2 = new CSInfoUIOperator(csi);
                    this.eventBinding(operator2);
                    this.flpMessage.Controls.Add(operator2);
                    break;
                }
                case UnfoundType.ThuocDVT:
                {
                    MedicineOperator operator3;
                    MedicineInfo info2;
                    Label label4 = new Label();
                    label4.Text = "Bạn c\x00f3 thể th\x00eam đơn vị mới bằng c\x00e1ch điền c\x00e1c th\x00f4ng tin b\x00ean dưới.\n H\x00e3y ch\x00fa \x00fd đến tỉ lệ giữa c\x00e1c đơn vị t\x00ednh.";
                    AutoSizeRender.autoSizeControl(label4, 10);
                    this.flpMessage.Controls.Add(label4);
                    string tenThuoc = "";
                    ThuocTraoDoi doi = new ThuocTraoDoi();
                    foreach (UnfoundArg arg2 in args.fieldValue)
                    {
                        switch (arg2.Key)
                        {
                            case FieldKey.TenThuoc:
                                tenThuoc = arg2.Value;
                                break;

                            case FieldKey.TenDVT:
                                dvt = new DVT();
                                dvt.TenDV = arg2.Value;
                                doi.TiLe = 1;
                                doi.DVT = dvt;
                                break;
                        }
                    }
                    ArrayList medicine = new MedicineController().GetMedicine(tenThuoc);
                    if (medicine.Count > 0)
                    {
                        info2 = (MedicineInfo) medicine[0];
                        operator3 = new MedicineOperator(info2);
                        info2.ThuocTraoDois.Add(doi);
                        operator3.setOperatorState(OperatorState.Edit);
                    }
                    else
                    {
                        info2 = new MedicineInfo();
                        info2.TenThuoc = tenThuoc;
                        info2.ThuocTraoDois.Add(doi);
                        operator3 = new MedicineOperator(info2);
                        operator3.setOperatorState(OperatorState.Add);
                    }
                    operator3.ChangeMode = false;
                    this.eventBinding(operator3);
                    this.flpMessage.Controls.Add(operator3);
                    break;
                }
            }
            this.flpMessage.Refresh();
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void updateFin(object sender, OperatorArgument args)
        {
            this.refreshWorker(args.DataType);
            this.flpMessage.Controls.Clear();
            Label control = new Label();
            control.Text = "Sửa th\x00e0nh c\x00f4ng";
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void updateUnfin()
        {
            this.flpMessage.Controls.Clear();
            Label control = new Label();
            control.Text = "Lỗi: Sửa kh\x00f4ng th\x00e0nh c\x00f4ng";
            AutoSizeRender.autoSizeControl(control, 10);
            this.flpMessage.Controls.Add(control);
            this.splitContainerMain.Panel2Collapsed = false;
        }

        private void viewDetails(ViewArg args)
        {
            switch (args.Type)
            {
                case DataType.HoaDonNhap:
                {
                    ImportOrderUIOperator ctl = new ImportOrderUIOperator(args.id);
                    TabItem item3 = this.AddNewTab(ctl, "HoaDonNhapThuoc" + args.id.ToString());
                    ctl.SelectedItemView += new ViewFunc(this.viewDetails);
                    this.eventBinding(ctl);
                    item3.Text = "Ho\x00e1 đơn nhập " + args.id.ToString();
                    break;
                }
                case DataType.HoaDonBan:
                {
                    SellOrderOperator @operator = new SellOrderOperator(args.id);
                    TabItem item = this.AddNewTab(@operator, "HoaDonBanThuoc" + args.id.ToString());
                    this.eventBinding(@operator);
                    item.Text = "Ho\x00e1 đơn b\x00e1n " + args.id.ToString();
                    break;
                }
                case DataType.KhoanThuChi:
                {
                    QuyUIOperator operator2 = new QuyUIOperator(args.id);
                    TabItem item2 = this.AddNewTab(operator2, "Quy" + args.id.ToString());
                    this.eventBinding(operator2);
                    item2.Text = "Quỹ " + args.id.ToString();
                    break;
                }
                case DataType.KiemKe:
                {
                    KiemKeUIOperator operator4 = new KiemKeUIOperator(args.id);
                    TabItem item4 = this.AddNewTab(operator4, "KiemKe" + args.id.ToString());
                    this.eventBinding(operator4);
                    item4.Text = "Kiểm k\x00ea " + args.id.ToString();
                    break;
                }
                case DataType.ChuyenQuay:
                {
                    ChuyenQuayUIOperator operator5 = new ChuyenQuayUIOperator(args.id);
                    TabItem item5 = this.AddNewTab(operator5, "ChuyenQuay" + args.id.ToString());
                    this.eventBinding(operator5);
                    item5.Text = "Chuyển quầy " + args.id.ToString();
                    break;
                }
                case DataType.TraLaiThuocNhap:
                {
                    TraLaiNhapUIOperator operator7 = new TraLaiNhapUIOperator(args.id);
                    TabItem item7 = this.AddNewTab(operator7, "TraLai" + args.id.ToString());
                    this.eventBinding(operator7);
                    item7.Text = "Trả lại" + args.id.ToString();
                    break;
                }
                case DataType.NhanThuocTraLai:
                {
                    TraLaiBanUIOperator operator6 = new TraLaiBanUIOperator(args.id);
                    TabItem item6 = this.AddNewTab(operator6, "TraLai" + args.id.ToString());
                    this.eventBinding(operator6);
                    item6.Text = "Trả lại" + args.id.ToString();
                    break;
                }
                case DataType.ThanhToanHoaDon:
                {
                    ThanhToanListOperator ttlo = new ThanhToanListOperator(args.id);
                    this.eventBinding(ttlo);
                    this.AddNewTab(ttlo, "ThanhToan" + args.id).Text = "Thanh to\x00e1n " + args.id.ToString();
                    break;
                }
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (this.refreshType)
            {
                case DataType.Thuoc:
                    Program.ACSource.ActiveSource = CommonSource.Medicine;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.KhachHang:
                    Program.ACSource.ActiveSource = CommonSource.CS;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.NhanVien:
                    Program.ACSource.ActiveSource = CommonSource.NhanVien;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.LoaiThuChi:
                    Program.ACSource.ActiveSource = CommonSource.LoaiThuChi;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.LoaiKiemKe:
                    Program.ACSource.ActiveSource = CommonSource.LoaiKiemKe;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.LoaiThuoc:
                    Program.ACSource.ActiveSource = CommonSource.LoaiThuoc;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.Quay:
                    Program.ACSource.ActiveSource = CommonSource.Quay;
                    Program.ACSource.RefreshSource();
                    break;

                case DataType.LoaiKhachHang:
                    Program.ACSource.ActiveSource = CommonSource.NhomCS;
                    Program.ACSource.RefreshSource();
                    break;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
        }
    }
}
