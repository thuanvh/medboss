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
            this.components = new Container();
            TreeNode node = new TreeNode("H\x00f3a đơn nhập thuốc", 3, 3);
            TreeNode node2 = new TreeNode("Trả lại thuốc cho h\x00e3ng", 3, 3);
            TreeNode nodeThanhToan = new TreeNode("Thanh to\x00e1n h\x00f3a đơn", 3, 3);
            TreeNode node3 = new TreeNode("B\x00e1o c\x00e1o nhập thuốc", 2, 2);
            TreeNode node5 = new TreeNode("B\x00e1o c\x00e1o nhập thuốc chi tiết", 1, 1);
            TreeNode node4 = new TreeNode("B\x00e1o c\x00e1o trả lại thuốc cho h\x00e3ng", 2, 2);
            TreeNode node6 = new TreeNode("B\x00e1o c\x00e1o trả lại thuốc cho h\x00e3ng chi tiết", 1, 1);
            TreeNode ndtlh = new TreeNode("B\x00e1o c\x00e1o trả lại thuốc cho h\x00e3ng nh\x00f3m theo h\x00e3ng", 1, 1);
            TreeNode node7 = new TreeNode("B\x00e1o c\x00e1o nhập thuốc từ nh\x00e0 cung cấp", 2, 2);
            TreeNode ndttls = new TreeNode("B\x00e1o c\x00e1o qu\x00e1 tr\x00ecnh thanh to\x00e1n nhập", 2, 2);
            TreeNode ndttlsct = new TreeNode("B\x00e1o c\x00e1o qu\x00e1 tr\x00ecnh thanh to\x00e1n nhập chi tiết", 2, 2);
            TreeNode ndttcx = new TreeNode("B\x00e1o c\x00e1o c\x00f4ng nợ nhập thuốc", 2, 2);
            TreeNode node8 = new TreeNode("Quản l\x00fd nhập thuốc", new TreeNode[] { node, node2, nodeThanhToan, node3, node5, node4, node6, ndtlh, node7, ndttls, ndttlsct, ndttcx });
            TreeNode node9 = new TreeNode("H\x00f3a đơn b\x00e1n thuốc", 3, 3);
            TreeNode node10 = new TreeNode("Thu b\x00e1n h\x00e0ng", 3, 3);
            TreeNode node11 = new TreeNode("Nhập thuốc trả lại", 3, 3);
            TreeNode node12 = new TreeNode("B\x00e1o c\x00e1o b\x00e1n thuốc chi tiết", 1, 1);
            TreeNode node13 = new TreeNode("B\x00e1o c\x00e1o b\x00e1n thuốc", 2, 2);
            TreeNode noderptHangBanChuanDVT = new TreeNode("B\x00e1o c\x00e1o b\x00e1n thuốc chuẩn", 2, 2);
            TreeNode node14 = new TreeNode("B\x00e1o c\x00e1o nhận lại thuốc từ kh\x00e1ch h\x00e0ng", 2, 2);
            TreeNode node15 = new TreeNode("B\x00e1o c\x00e1o chi tiết nhận lại thuốc từ kh\x00e1ch h\x00e0ng", 1, 1);
            TreeNode node16 = new TreeNode("B\x00e1o c\x00e1o b\x00e1n thuốc - kh\x00e1ch h\x00e0ng", 2, 2);
            TreeNode node17 = new TreeNode("Quản l\x00fd b\x00e1n thuốc", new TreeNode[] { node9, node10, node11, node12, node13, noderptHangBanChuanDVT, node14, node15, node16 });
            TreeNode node18 = new TreeNode("Nhập th\x00f4ng tin thu chi", 3, 3);
            TreeNode node19 = new TreeNode("Nhập loại thu chi", 3, 3);
            TreeNode node20 = new TreeNode("B\x00e1o c\x00e1o quỹ (theo ng\x00e0y)", 1, 1);
            TreeNode node21 = new TreeNode("B\x00e1o c\x00e1o quỹ tổng hợp", 2, 2);
            TreeNode node22 = new TreeNode("B\x00e1o c\x00e1o quỹ (theo khoản thu chi)", 1, 1);
            TreeNode node23 = new TreeNode("Quỹ", new TreeNode[] { node18, node19, node20, node21, node22 });
            TreeNode node24 = new TreeNode("B\x00e1o c\x00e1o tổng hợp nhập b\x00e1n", 2, 2);
            TreeNode node25 = new TreeNode("B\x00e1o c\x00e1o tổng hợp nhập b\x00e1n chuẩn", 2, 2);
            TreeNode node26 = new TreeNode("B\x00e1o c\x00e1o", 2, 2, new TreeNode[] { node24, node25 });
            TreeNode node27 = new TreeNode("Nhập thuốc v\x00e0o danh mục", 3, 3);
            TreeNode node28 = new TreeNode("Cập nhật gi\x00e1 thuốc", 3, 3);
            TreeNode node29 = new TreeNode("Hợp thuốc", 3, 3);
            TreeNode node30 = new TreeNode("Sửa danh mục", 3, 3);
            TreeNode node31 = new TreeNode("Xo\x00e1 thuốc", 5, 5);
            TreeNode node32 = new TreeNode("Nh\x00f3m thuốc", 3, 3);
            TreeNode node33 = new TreeNode("Thuốc trong h\x00f3a đơn", 4, 4);
            TreeNode node34 = new TreeNode("Danh mục thuốc", new TreeNode[] { node27, node28, node29, node30, node31, node32, node33 });
            TreeNode node35 = new TreeNode("Nhập th\x00f4ng tin kiểm k\x00ea", 3, 3);
            TreeNode node36 = new TreeNode("Loại kiểm k\x00ea", 3, 3);
            TreeNode node37 = new TreeNode("B\x00e1o c\x00e1o kiểm k\x00ea theo tên thuốc", 1, 1);
            TreeNode nodebaocaokiemkebymakk = new TreeNode("B\x00e1o c\x00e1o kiểm k\x00ea theo trang", 1, 1);
            TreeNode nodebaocaokiemkemau = new TreeNode("B\x00e1o c\x00e1o kiểm k\x00ea mẫu", 1, 1);
            TreeNode node38 = new TreeNode("Thuốc tồn", 3, 3);
            TreeNode node39 = new TreeNode("Kiểm k\x00ea", new TreeNode[] { node35, node36, node37,nodebaocaokiemkebymakk,nodebaocaokiemkemau, node38 });
            TreeNode node40 = new TreeNode("Nhập th\x00f4ng tin lu\x00e2n chuyển", 3, 3);
            TreeNode node41 = new TreeNode("B\x00e1o c\x00e1o lu\x00e2n chuyển thuốc", 1, 1);
            TreeNode nodeBaocaoLuanChuyen = new TreeNode("B\x00e1o c\x00e1o lu\x00e2n chuyển thuốc tổng hợp", 1, 1);
            TreeNode node42 = new TreeNode("Lu\x00e2n chuyển thuốc", new TreeNode[] { node40, node41, nodeBaocaoLuanChuyen });
            TreeNode node43 = new TreeNode("T\x00ecm kiếm chi tiết", 4, 4);
            TreeNode node44 = new TreeNode("T\x00ecm kiếm", 4, 4, new TreeNode[] { node43 });
            TreeNode node45 = new TreeNode("Quản l\x00fd danh s\x00e1ch nh\x00e2n vi\x00ean", 3, 3);
            TreeNode node46 = new TreeNode("Nh\x00e2n vi\x00ean", new TreeNode[] { node45 });
            TreeNode node47 = new TreeNode("Quản l\x00fd danh s\x00e1ch kh\x00e1ch h\x00e0ng", 3, 3);
            TreeNode node48 = new TreeNode("Nh\x00f3m kh\x00e1ch h\x00e0ng", 3, 3);
            TreeNode node49 = new TreeNode("Kh\x00e1ch h\x00e0ng", new TreeNode[] { node47, node48 });
            TreeNode node50 = new TreeNode("Th\x00f4ng tin về Medboss", 2, 2);
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MainForm));
            this.BottomToolStripPanel = new ToolStripPanel();
            this.TopToolStripPanel = new ToolStripPanel();
            this.RightToolStripPanel = new ToolStripPanel();
            this.toolStrip1 = new ToolStrip();
            this.mnuKetQua = new ToolStripLabel();
            this.toolStripSeparator = new ToolStripSeparator();
            this.LeftToolStripPanel = new ToolStripPanel();
            this.ContentPanel = new ToolStripContentPanel();
            this.splitContainerMain = new SplitContainer();
            this.tabContent = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel5 = new TabControlPanel();
            this.trvMenu = new TreeView();
            this.imgListMenu = new ImageList(this.components);
            this.menuTab = new TabItem(this.components);
            this.flpMessage = new FlowLayoutPanel();
            this.toolStripContainer1 = new ToolStripContainer();
            this.toolStrip1.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((ISupportInitialize) this.tabContent).BeginInit();
            this.tabContent.SuspendLayout();
            this.tabControlPanel5.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.RightToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            base.SuspendLayout();
            this.BottomToolStripPanel.Location = new Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new Size(0, 0);
            this.TopToolStripPanel.Location = new Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new Size(0, 0);
            this.RightToolStripPanel.Location = new Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new Size(0, 0);
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.mnuKetQua, this.toolStripSeparator });
            this.toolStrip1.Location = new Point(0, 0x16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x18, 0x4e);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.TextDirection = ToolStripTextDirection.Vertical90;
            this.mnuKetQua.Image = Resources.result;
            this.mnuKetQua.ImageAlign = ContentAlignment.TopCenter;
            this.mnuKetQua.Name = "mnuKetQua";
            this.mnuKetQua.Size = new Size(0x16, 60);
            this.mnuKetQua.Text = "Kết quả";
            this.mnuKetQua.TextDirection = ToolStripTextDirection.Vertical90;
            this.mnuKetQua.TextImageRelation = TextImageRelation.ImageAboveText;
            this.mnuKetQua.Click += new EventHandler(this.mnuKetQua_Click);
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new Size(0x16, 6);
            this.toolStripSeparator.TextDirection = ToolStripTextDirection.Vertical90;
            this.LeftToolStripPanel.Location = new Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new Size(0, 0);
            this.ContentPanel.Size = new Size(0x27d, 0x1e1);
            this.splitContainerMain.BackColor = SystemColors.ButtonFace;
            this.splitContainerMain.BorderStyle = BorderStyle.Fixed3D;
            this.splitContainerMain.Dock = DockStyle.Fill;
            this.splitContainerMain.Location = new Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Panel1.Controls.Add(this.tabContent);
            this.splitContainerMain.Panel2.Controls.Add(this.flpMessage);
            this.splitContainerMain.Panel2Collapsed = true;
            this.splitContainerMain.Size = new Size(0x292, 0x1e1);
            this.splitContainerMain.SplitterDistance = 0x1d7;
            this.splitContainerMain.TabIndex = 1;
            this.tabContent.AutoHideSystemBox = false;
            this.tabContent.CanReorderTabs = true;
            this.tabContent.CloseButtonVisible = true;
            this.tabContent.Controls.Add(this.tabControlPanel5);
            this.tabContent.Dock = DockStyle.Fill;
            this.tabContent.Location = new Point(0, 0);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabContent.SelectedTabIndex = 0;
            this.tabContent.Size = new Size(0x28e, 0x1dd);
            this.tabContent.TabIndex = 0;
            this.tabContent.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabContent.Tabs.Add(this.menuTab);
            this.tabContent.TabItemClose += new TabStrip.UserActionEventHandler(this.tabContent_TabItemClose);
            this.tabControlPanel5.Controls.Add(this.trvMenu);
            this.tabControlPanel5.Dock = DockStyle.Fill;
            this.tabControlPanel5.Location = new Point(0, 0x1a);
            this.tabControlPanel5.Name = "tabControlPanel5";
            this.tabControlPanel5.Padding = new Padding(1);
            this.tabControlPanel5.Size = new Size(0x28e, 0x1c3);
            this.tabControlPanel5.Style.BackColor1.Color = Color.FromArgb(0x90, 0x9c, 0xbb);
            this.tabControlPanel5.Style.BackColor2.Color = Color.FromArgb(230, 0xe9, 240);
            this.tabControlPanel5.Style.Border = eBorderType.SingleLine;
            this.tabControlPanel5.Style.BorderColor.Color = SystemColors.ControlDark;
            this.tabControlPanel5.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Right | eBorderSide.Left;
            this.tabControlPanel5.Style.GradientAngle = 90;
            this.tabControlPanel5.TabIndex = 5;
            this.tabControlPanel5.TabItem = this.menuTab;
            this.trvMenu.Dock = DockStyle.Fill;
            this.trvMenu.ImageIndex = 0;
            this.trvMenu.ImageList = this.imgListMenu;
            this.trvMenu.Location = new Point(1, 1);
            this.trvMenu.Name = "trvMenu";
            node.ImageIndex = 3;
            node.Name = "ndHoaDonNhapThuoc";
            node.SelectedImageIndex = 3;
            node2.ImageIndex = 3;
            node2.Name = "ndTraLaiThuocDaNhap";
            node2.SelectedImageIndex = 3;
            nodeThanhToan.ImageIndex = 3;
            nodeThanhToan.Name = "ndThanhToanHDN";
            nodeThanhToan.SelectedImageIndex = 3;
            node3.ImageIndex = 2;
            node3.Name = "ndBaoCaoNhapThuoc";
            node3.SelectedImageIndex = 2;
            node4.ImageIndex = 2;
            node4.Name = "ndBaoCaoTraLaiThuoc";
            node4.SelectedImageIndex = 2;
            node5.ImageIndex = 1;
            node5.Name = "ndBaoCaoNhapThuocChiTiet";
            node5.SelectedImageIndex = 1;
            node6.ImageIndex = 1;
            node6.Name = "ndBaoCaoTraLaiThuocChiTiet";
            node6.SelectedImageIndex = 1;
            ndtlh.ImageIndex = 1;
            ndtlh.Name = "ndBaoCaoTraLaiThuocTheoHang";
            ndtlh.SelectedImageIndex = 1;
            node7.ImageIndex = 2;
            node7.Name = "ndBaoCaoNhapThuocKhachHang";
            node7.SelectedImageIndex = 2;
            ndttls.ImageIndex = 2;
            ndttls.Name = "ndBaoCaoNhapThuoc_ThanhToanLichSu";
            ndttls.SelectedImageIndex = 2;
            ndttlsct.ImageIndex = 2;
            ndttlsct.Name = "ndBaoCaoNhapThuoc_ThanhToanLichSuChiTiet";
            ndttlsct.SelectedImageIndex = 2;
            ndttcx.ImageIndex = 2;
            ndttcx.Name = "ndBaoCaoNhapThuoc_ThanhToanChuaXong";
            ndttcx.SelectedImageIndex = 2;
            node8.ImageIndex = 0;
            node8.Name = "ndThuoc";
            node9.ImageIndex = 3;
            node9.Name = "ndHoaDonBanThuoc";
            node9.SelectedImageIndex = 3;
            node10.ImageIndex = 3;
            node10.Name = "ndThuBanHang";
            node10.SelectedImageIndex = 3;
            node11.ImageIndex = 3;
            node11.Name = "ndNhapThuocTraLai";
            node11.SelectedImageIndex = 3;
            node12.ImageIndex = 1;
            node12.Name = "ndBaoCaoBanThuocChiTiet";
            node12.SelectedImageIndex = 1;
            node13.ImageIndex = 2;
            node13.Name = "ndBaoCaoBanThuoc";
            node13.SelectedImageIndex = 2;
            noderptHangBanChuanDVT.Name = "ndBaoCaoBanThuocChuanDVT";
            noderptHangBanChuanDVT.ImageIndex = 2;
            noderptHangBanChuanDVT.SelectedImageIndex = 2;
            node14.ImageIndex = 2;
            node14.Name = "ndBaoCaoNhanThuocTraLai";
            node14.SelectedImageIndex = 2;
            node15.ImageIndex = 1;
            node15.Name = "ndBaoCaoNhanThuocTraLaiChiTiet";
            node15.SelectedImageIndex = 1;
            node16.ImageIndex = 2;
            node16.Name = "ndBaoCaoBanThuocKhachHang";
            node16.SelectedImageIndex = 2;
            node17.Name = "Node2";
            node18.ImageIndex = 3;
            node18.Name = "ndNhapThongTinThuChi";
            node18.SelectedImageIndex = 3;
            node19.ImageIndex = 3;
            node19.Name = "ndNhapLoaiThuChi";
            node19.SelectedImageIndex = 3;
            node20.ImageIndex = 1;
            node20.Name = "ndBaoCaoQuyTheoNgay";
            node20.SelectedImageIndex = 1;
            node21.ImageIndex = 2;
            node21.Name = "ndBaoCaoQuyTongHop";
            node21.SelectedImageIndex = 2;
            node22.ImageIndex = 1;
            node22.Name = "ndBaoCaoQuyTheoKhoanThuChi";
            node22.SelectedImageIndex = 1;
            node23.Name = "Node5";
            node24.ImageIndex = 2;
            node24.Name = "ndBaoCaoTongHopNhapBan";
            node24.SelectedImageIndex = 2;
            node25.ImageIndex = 2;
            node25.Name = "ndBaoCaoTongHopNhapBanChuan";
            node25.SelectedImageIndex = 2;
            node26.ImageIndex = 2;
            node26.Name = "ndBaoCao";
            node26.SelectedImageIndex = 2;
            node27.ImageIndex = 3;
            node27.Name = "ndNhapThuocVaoDanhMuc";
            node27.SelectedImageIndex = 3;
            node28.ImageIndex = 3;
            node28.Name = "ndCapNhatGiaThuoc";
            node28.SelectedImageIndex = 3;
            node29.ImageIndex = 3;
            node29.Name = "ndChuyenThuoc";
            node29.SelectedImageIndex = 3;
            node30.ImageIndex = 3;
            node30.Name = "ndSuaDanhMuc";
            node30.SelectedImageIndex = 3;
            node31.ImageIndex = 5;
            node31.Name = "ndXoaThuoc";
            node31.SelectedImageIndex = 5;
            node32.ImageIndex = 3;
            node32.Name = "ndLoaiThuoc";
            node32.SelectedImageIndex = 3;
            node33.ImageIndex = 4;
            node33.Name = "ndThuocTrongHoaDon";
            node33.SelectedImageIndex = 4;
            node34.Name = "ndDanhMucThuoc";
            node35.ImageIndex = 3;
            node35.Name = "ndNhapThongTinKiemKe";
            node35.SelectedImageIndex = 3;
            node36.ImageIndex = 3;
            node36.Name = "ndLoaiKiemKe";
            node36.SelectedImageIndex = 3;
            node37.ImageIndex = 1;
            node37.Name = "ndBaoCaoKiemKe";
            node37.SelectedImageIndex = 1;
            nodebaocaokiemkebymakk.ImageIndex = 1;
            nodebaocaokiemkebymakk.Name = "ndBaoCaoKiemKeMaKK";
            nodebaocaokiemkebymakk.SelectedImageIndex = 1;
            nodebaocaokiemkemau.ImageIndex = 1;
            nodebaocaokiemkemau.Name = "ndBaoCaoKiemKeMau";
            nodebaocaokiemkemau.SelectedImageIndex = 1;
            node38.ImageIndex = 3;
            node38.Name = "ndThuocTon";
            node38.SelectedImageIndex = 3;
            node39.Name = "ndKiemKe";
            node40.ImageIndex = 3;
            node40.Name = "ndNhapThongTinChuyenQuay";
            node40.SelectedImageIndex = 3;
            node41.ImageIndex = 1;
            node41.Name = "ndBaoCaoLuanChuyenThuoc";
            node41.SelectedImageIndex = 1;
            nodeBaocaoLuanChuyen.ImageIndex = 1;
            nodeBaocaoLuanChuyen.Name = "ndBaoCaoLuanChuyenThuocTongHop";
            nodeBaocaoLuanChuyen.SelectedImageIndex = 1;
            node42.Name = "ndChuyenQuay";
            node43.ImageIndex = 4;
            node43.Name = "ndTimKiemChiTiet";
            node43.SelectedImageIndex = 4;
            node44.ImageIndex = 4;
            node44.Name = "ndTimKiem";
            node44.SelectedImageIndex = 4;
            node45.ImageIndex = 3;
            node45.Name = "ndQuanLyDanhSachNhanVien";
            node45.SelectedImageIndex = 3;
            node46.Name = "ndNhanVien";
            node47.ImageIndex = 3;
            node47.Name = "ndQuanLyDanhSachKhachHang";
            node47.SelectedImageIndex = 3;
            node48.ImageIndex = 3;
            node48.Name = "ndNhomKhachHang";
            node48.SelectedImageIndex = 3;
            node49.Name = "ndKhachHang";
            node50.ImageIndex = 2;
            node50.Name = "ndAbout";
            node50.SelectedImageIndex = 2;
            this.trvMenu.Nodes.AddRange(new TreeNode[] { node8, node17, node23, node26, node34, node39, node42, node44, node46, node49, node50 });
            this.trvMenu.SelectedImageIndex = 0;
            this.trvMenu.Size = new Size(0x28c, 0x1c1);
            this.trvMenu.TabIndex = 0;
            this.trvMenu.DoubleClick += new EventHandler(this.menuTree_DoubleClick);
            this.trvMenu.KeyPress += new KeyPressEventHandler(this.menuTree_KeyPress);
            this.imgListMenu.ImageStream = (ImageListStreamer) manager.GetObject("imgListMenu.ImageStream");
            this.imgListMenu.TransparentColor = Color.Transparent;
            this.imgListMenu.Images.SetKeyName(0, "icon_Vendors_16px.gif");
            this.imgListMenu.Images.SetKeyName(1, "16doc.gif");
            this.imgListMenu.Images.SetKeyName(2, "16newreport.gif");
            this.imgListMenu.Images.SetKeyName(3, "edit.gif");
            this.imgListMenu.Images.SetKeyName(4, "icon_search_16px.gif");
            this.imgListMenu.Images.SetKeyName(5, "cl-tb-delete.gif");
            this.imgListMenu.Images.SetKeyName(6, "16newfolder.gif");
            this.imgListMenu.Images.SetKeyName(7, "folder.gif");
            this.menuTab.AttachedControl = this.tabControlPanel5;
            this.menuTab.Name = "menuTab";
            this.menuTab.PredefinedColor = eTabItemColor.Default;
            this.menuTab.Text = "Menu";
            this.flpMessage.Dock = DockStyle.Fill;
            this.flpMessage.FlowDirection = FlowDirection.TopDown;
            this.flpMessage.Location = new Point(0, 0);
            this.flpMessage.Name = "flpMessage";
            this.flpMessage.Size = new Size(0x5c, 0x60);
            this.flpMessage.TabIndex = 0;
            this.flpMessage.Leave += new EventHandler(this.Right_leave);
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerMain);
            this.toolStripContainer1.ContentPanel.Size = new Size(0x292, 0x1e1);
            this.toolStripContainer1.Dock = DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.Size = new Size(0x2aa, 0x1e1);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.ClientSize = new Size(0x2aa, 0x1e1);
            base.Controls.Add(this.toolStripContainer1);
            base.Name = "MainForm";
            this.Text = "Medboss";
            base.WindowState = FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            ((ISupportInitialize) this.tabContent).EndInit();
            this.tabContent.ResumeLayout(false);
            this.tabControlPanel5.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.RightToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.RightToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            base.ResumeLayout(false);
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
