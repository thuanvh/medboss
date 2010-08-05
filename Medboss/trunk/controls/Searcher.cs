namespace Nammedia.Medboss.controls
{
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Style;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Searcher : OperatorBase
    {
        private Button butChiTiet;
        private Button butTim;
        private ComboBox cboKhachHang;
        private ComboBox cboLoaiDuLieu;
        private DataGridViewTextBoxColumn colMa;
        private IContainer components = null;
        private Dictionary<DataType, string> dataTypeStr = new Dictionary<DataType, string>();
        private QuaySelector denQuaySelector;
        private Dictionary<DataType, IFinder> dicFinder = new Dictionary<DataType, IFinder>();
        private Dictionary<TabItem, DataGridView> dicGrid = new Dictionary<TabItem, DataGridView>();
        private DataGridView grdSearchResult;
        private GroupBox grpQuay;
        private bool init = true;
        private Label label1;
        private Label lblKhachHang;
        private QuaySelector quaySelector;
        private RadioButton radHaiChieu;
        private RadioButton radMotChieu;
        private SplitContainer splitContainer1;
        private DevComponents.DotNetBar.TabControl tabControl;
        private TabControlPanel tabControlPanel1;
        private TabItem tabItem1;
        private TimeParaser timeParaser;
        private QuaySelector tuQuaySelector;

        public event ViewFunc SelectedItemView
        {
            add
            {
                base.eventTable["SelectedItemView"] = (ViewFunc) Delegate.Combine((ViewFunc) base.eventTable["SelectedItemView"], value);
            }
            remove
            {
                base.eventTable["SelectedItemView"] = (ViewFunc) Delegate.Remove((ViewFunc) base.eventTable["SelectedItemView"], value);
            }
        }

        public Searcher()
        {
            this.InitializeComponent();
            this.grdSearchResult.DefaultCellStyle = new CommonDataGridViewCellStyle(this.grdSearchResult.DefaultCellStyle);
            this.initFinderMap();
            this.initStr2DataType();
            this.initCboDataType();
            this.loadAC();
            this.init = false;
        }

        protected override void _validate()
        {
            base._validate();
            this.Search();
        }

        private void butChiTiet_Click(object sender, EventArgs e)
        {
            DataGridView view = this.dicGrid[this.tabControl.SelectedTab];
            if ((view != null) && (view.SelectedRows.Count > 0))
            {
                ViewFunc func = (ViewFunc) base.eventTable["SelectedItemView"];
                ViewArg args = new ViewArg((DataType) this.cboLoaiDuLieu.SelectedValue, ConvertHelper.getInt(view.SelectedRows[0].Cells[this.colMa.Index].Value));
                if (func != null)
                {
                    func(args);
                }
            }
        }

        private void butXem_Click(object sender, EventArgs e)
        {
            base.Validate();
        }

        private void cboLoaiDuLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.init)
            {
                DataType selectedValue = (DataType) this.cboLoaiDuLieu.SelectedValue;
                if (selectedValue != DataType.ChuyenQuay)
                {
                    this.grpQuay.Visible = false;
                    this.quaySelector.Visible = true;
                }
                this.cboKhachHang.Enabled = false;
                switch (selectedValue)
                {
                    case DataType.HoaDonNhap:
                        this.lblKhachHang.Text = "Nh\x00e0 cung cấp";
                        this.cboKhachHang.Enabled = true;
                        break;

                    case DataType.HoaDonBan:
                        this.lblKhachHang.Text = "Kh\x00e1ch h\x00e0ng";
                        this.cboKhachHang.Enabled = true;
                        break;

                    case DataType.ChuyenQuay:
                        this.grpQuay.Visible = true;
                        this.quaySelector.Visible = false;
                        break;

                    case DataType.ThanhToanHoaDon:
                        this.lblKhachHang.Text = "Nh\x00e0 cung cấp";
                        this.cboKhachHang.Enabled = true;
                        break;
                }
            }
        }

        private void createResultGrid(FindField[] fields)
        {
            this.grdSearchResult.Columns.Clear();
            this.grdSearchResult.AutoGenerateColumns = false;
            this.grdSearchResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.grdSearchResult.ReadOnly = true;
            for (int i = 1; i < fields.Length; i++)
            {
                DataGridViewTextBoxColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
                dataGridViewColumn.DataPropertyName = fields[i].DisplayField;
                dataGridViewColumn.Name = fields[i].Field;
                dataGridViewColumn.HeaderText = fields[i].DisplayField;
                this.grdSearchResult.Columns.Add(dataGridViewColumn);
                if (fields[i].Type == typeof(int))
                {
                    dataGridViewColumn.DefaultCellStyle = new DataGridViewIntCellStyle();
                }
                else if (fields[i].Type == typeof(DateTime))
                {
                    dataGridViewColumn.DefaultCellStyle = new DataGridViewDateTimeCellStyle("dd/MM/yyyy");
                    if (fields[i].Field == "HanDung")
                    {
                        dataGridViewColumn.DefaultCellStyle = new DataGridViewDateTimeCellStyle("MM/yyyy");
                    }
                }
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

        private void initCboDataType()
        {
            DataType[] array = new DataType[this.dataTypeStr.Values.Count];
            this.dataTypeStr.Keys.CopyTo(array, 0);
            DataTable table = new DataTable();
            table.Columns.Add("Value", typeof(DataType));
            table.Columns.Add("Name");
            foreach (DataType type in array)
            {
                DataRow row = table.NewRow();
                row["Value"] = type;
                row["Name"] = this.dataTypeStr[type];
                table.Rows.Add(row);
            }
            this.cboLoaiDuLieu.DataSource = table;
            this.cboLoaiDuLieu.DisplayMember = "Name";
            this.cboLoaiDuLieu.ValueMember = "Value";
        }

        private void initCboKhachHang()
        {
        }

        private void initFinderMap()
        {
            this.dicFinder.Add(DataType.HoaDonBan, new HoaDonBanThuocController());
            this.dicFinder.Add(DataType.KhoanThuChi, new QuyController());
            this.dicFinder.Add(DataType.HoaDonNhap, new HoaDonNhapThuocController());
            this.dicFinder.Add(DataType.KiemKe, new KiemKeController());
            HoaDonTraLaiNhapController controller = new HoaDonTraLaiNhapController();
            HoaDonTraLaiBanController controller2 = new HoaDonTraLaiBanController();
            controller.ThuHayChiSearchParam = KhoanThuChiInfo.Thu;
            controller2.ThuHayChiSearchParam = KhoanThuChiInfo.Chi;
            this.dicFinder.Add(DataType.TraLaiThuocNhap, controller);
            this.dicFinder.Add(DataType.NhanThuocTraLai, controller2);
            this.dicFinder.Add(DataType.ChuyenQuay, new ChuyenQuayController());
            this.dicFinder.Add(DataType.ThanhToanHoaDon, new HoaDonThanhToanController());
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
            this.butChiTiet = new Button();
            this.label1 = new Label();
            this.cboLoaiDuLieu = new ComboBox();
            this.quaySelector = new QuaySelector();
            this.butTim = new Button();
            this.timeParaser = new TimeParaser();
            this.tabControl = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new TabControlPanel();
            this.grdSearchResult = new DataGridView();
            this.colMa = new DataGridViewTextBoxColumn();
            this.tabItem1 = new TabItem(this.components);
            this.cboKhachHang = new ComboBox();
            this.lblKhachHang = new Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpQuay.SuspendLayout();
            ((ISupportInitialize) this.tabControl).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((ISupportInitialize) this.grdSearchResult).BeginInit();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.lblKhachHang);
            this.splitContainer1.Panel1.Controls.Add(this.cboKhachHang);
            this.splitContainer1.Panel1.Controls.Add(this.grpQuay);
            this.splitContainer1.Panel1.Controls.Add(this.butChiTiet);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboLoaiDuLieu);
            this.splitContainer1.Panel1.Controls.Add(this.quaySelector);
            this.splitContainer1.Panel1.Controls.Add(this.butTim);
            this.splitContainer1.Panel1.Controls.Add(this.timeParaser);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new Size(650, 0x1f2);
            this.splitContainer1.SplitterDistance = 0xd0;
            this.splitContainer1.TabIndex = 0;
            this.grpQuay.Controls.Add(this.denQuaySelector);
            this.grpQuay.Controls.Add(this.tuQuaySelector);
            this.grpQuay.Controls.Add(this.radHaiChieu);
            this.grpQuay.Controls.Add(this.radMotChieu);
            this.grpQuay.Location = new Point(0x141, 0x26);
            this.grpQuay.Name = "grpQuay";
            this.grpQuay.Size = new Size(0x103, 140);
            this.grpQuay.TabIndex = 8;
            this.grpQuay.TabStop = false;
            this.grpQuay.Text = "Quầy";
            this.grpQuay.Visible = false;
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
            this.butChiTiet.Location = new Point(0x11c, 11);
            this.butChiTiet.Name = "butChiTiet";
            this.butChiTiet.Size = new Size(50, 0x17);
            this.butChiTiet.TabIndex = 6;
            this.butChiTiet.Text = "Chi tiết";
            this.butChiTiet.UseVisualStyleBackColor = true;
            this.butChiTiet.Click += new EventHandler(this.butChiTiet_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3d, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Loại dữ liệu";
            this.cboLoaiDuLieu.FormattingEnabled = true;
            this.cboLoaiDuLieu.Location = new Point(0x57, 11);
            this.cboLoaiDuLieu.Name = "cboLoaiDuLieu";
            this.cboLoaiDuLieu.Size = new Size(0x83, 0x15);
            this.cboLoaiDuLieu.TabIndex = 4;
            this.cboLoaiDuLieu.SelectedIndexChanged += new EventHandler(this.cboLoaiDuLieu_SelectedIndexChanged);
            this.quaySelector.Location = new Point(0x171, 0x26);
            this.quaySelector.Name = "quaySelector";
            this.quaySelector.Size = new Size(0x76, 0x59);
            this.quaySelector.TabIndex = 3;
            this.quaySelector.Title = "Quầy";
            this.butTim.Location = new Point(0xe0, 11);
            this.butTim.Name = "butTim";
            this.butTim.Size = new Size(0x2d, 0x15);
            this.butTim.TabIndex = 2;
            this.butTim.Text = "T\x00ecm";
            this.butTim.UseVisualStyleBackColor = true;
            this.butTim.Click += new EventHandler(this.butXem_Click);
            this.timeParaser.Location = new Point(3, 0x26);
            this.timeParaser.Name = "timeParaser";
            this.timeParaser.Size = new Size(0x138, 0x71);
            this.timeParaser.TabIndex = 0;
            this.tabControl.AutoHideSystemBox = false;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.Controls.Add(this.tabControlPanel1);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabControl.SelectedTabIndex = 0;
            this.tabControl.Size = new Size(650, 0x11e);
            this.tabControl.TabIndex = 1;
            this.tabControl.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabControl.Tabs.Add(this.tabItem1);
            this.tabControl.TabItemClose += new TabStrip.UserActionEventHandler(this.tabControl_TabItemClose);
            this.tabControlPanel1.Controls.Add(this.grdSearchResult);
            this.tabControlPanel1.Dock = DockStyle.Fill;
            this.tabControlPanel1.Location = new Point(0, 0x1a);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new Padding(1);
            this.tabControlPanel1.Size = new Size(650, 260);
            this.tabControlPanel1.Style.BackColor1.Color = Color.FromArgb(0x8e, 0xb3, 0xe7);
            this.tabControlPanel1.Style.BackColor2.Color = Color.FromArgb(0xdf, 0xed, 0xfe);
            this.tabControlPanel1.Style.Border = eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = Color.FromArgb(0x3b, 0x61, 0x9c);
            this.tabControlPanel1.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Right | eBorderSide.Left;
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            this.grdSearchResult.AllowUserToOrderColumns = true;
            this.grdSearchResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSearchResult.Columns.AddRange(new DataGridViewColumn[] { this.colMa });
            this.grdSearchResult.Dock = DockStyle.Fill;
            this.grdSearchResult.Location = new Point(1, 1);
            this.grdSearchResult.MultiSelect = false;
            this.grdSearchResult.Name = "grdSearchResult";
            this.grdSearchResult.Size = new Size(0x288, 0x102);
            this.grdSearchResult.TabIndex = 0;
            this.colMa.DataPropertyName = "Ma";
            this.colMa.HeaderText = "M\x00e3";
            this.colMa.Name = "colMa";
            this.colMa.Visible = false;
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.PredefinedColor = eTabItemColor.Default;
            this.tabItem1.Text = "tabItem1";
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new Point(0x57, 0x9c);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new Size(0xe4, 0x15);
            this.cboKhachHang.TabIndex = 9;
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new Point(3, 0x9f);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new Size(0x41, 13);
            this.lblKhachHang.TabIndex = 10;
            this.lblKhachHang.Text = "Kh\x00e1ch h\x00e0ng";
            base.Controls.Add(this.splitContainer1);
            base.Name = "Searcher";
            base.Size = new Size(650, 0x1f2);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpQuay.ResumeLayout(false);
            this.grpQuay.PerformLayout();
            ((ISupportInitialize) this.tabControl).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((ISupportInitialize) this.grdSearchResult).EndInit();
            base.ResumeLayout(false);
        }

        private void initStr2DataType()
        {
            this.dataTypeStr.Add(DataType.HoaDonBan, "Ho\x00e1 đơn b\x00e1n");
            this.dataTypeStr.Add(DataType.HoaDonNhap, "Ho\x00e1 đơn nhập");
            this.dataTypeStr.Add(DataType.ThanhToanHoaDon, "Ho\x00e1 đơn thanh to\x00e1n");
            this.dataTypeStr.Add(DataType.KhoanThuChi, "Khoản thu chi");
            this.dataTypeStr.Add(DataType.KiemKe, "Kiểm k\x00ea");
            this.dataTypeStr.Add(DataType.TraLaiThuocNhap, "Trả lại thuốc nhập");
            this.dataTypeStr.Add(DataType.NhanThuocTraLai, "Nhận thuốc trả lại");
            this.dataTypeStr.Add(DataType.ChuyenQuay, "Chuyển quầy");
        }

        public override void loadAC()
        {
            ArrayList arr = (ArrayList) base._csSource.Clone();
            CSInfo cs = new CSInfo();
            cs.MaKhachHang = -1;
            cs.Ten = "Tất cả kh\x00e1ch h\x00e0ng";
            arr.Insert(0, cs);
            base._acFactory.EnableAutocomplete(this.cboKhachHang, ref arr);
        }

        public override void RefreshAC()
        {
            ArrayList arr = (ArrayList) base._csSource.Clone();
            CSInfo cs = new CSInfo();
            cs.MaKhachHang = -1;
            cs.Ten = "Tất cả kh\x00e1ch h\x00e0ng";
            arr.Insert(0, cs);
            base._acFactory.RefreshAutoCompleteSource(this.cboKhachHang, ref arr);
        }

        private void Search()
        {
            FindParam[] findParams;
            FindParam[] allParams;
            FindParam prDoitac = new FindParam();
            bool IsAddDoiTac = false;
            if (this.cboKhachHang.SelectedValue is CSInfo)
            {
                CSInfo selectedKhachHang = (CSInfo) this.cboKhachHang.SelectedValue;
                if (selectedKhachHang.MaKhachHang != -1)
                {
                    IsAddDoiTac = true;
                    prDoitac = new FindParam(FindKeyParam.DoiTac, "TenKhachHang", selectedKhachHang.Ten);
                }
            }
            DataType selectedValue = (DataType) this.cboLoaiDuLieu.SelectedValue;
            IFinder finder = this.dicFinder[selectedValue];
            FindParam param = new FindParam(FindKeyParam.TuNgay, "TuNgay", this.timeParaser.getTuNgay().ToString("MM/dd/yyyy"));
            FindParam param2 = new FindParam(FindKeyParam.ToiNgay, "ToiNgay", this.timeParaser.getDenNgay().ToString("MM/dd/yyyy"));
            string name = string.Concat(new object[] { param.value, "-", param2.value, ";" });
            if (selectedValue != DataType.ChuyenQuay)
            {
                FindParam param3 = new FindParam(FindKeyParam.Quay, "Quay", this.quaySelector.getQuay());
                if (IsAddDoiTac)
                {
                    allParams = new FindParam[] { param3, param, param2, prDoitac };
                    findParams = allParams;
                }
                else
                {
                    allParams = new FindParam[] { param3, param, param2 };
                    findParams = allParams;
                }
                name = name + param3.value;
            }
            else
            {
                FindParam param4 = new FindParam(FindKeyParam.TuQuay, "Quay", this.tuQuaySelector.getQuay());
                FindParam param5 = new FindParam(FindKeyParam.ToiQuay, "Quay", this.denQuaySelector.getQuay());
                FindParam param6 = new FindParam(FindKeyParam.MotChieu, "MotChieu", this.radMotChieu.Checked);
                if (IsAddDoiTac)
                {
                    allParams = new FindParam[] { param4, param, param2, param5, param6, prDoitac };
                    findParams = allParams;
                }
                else
                {
                    allParams = new FindParam[] { param4, param, param2, param5, param6 };
                    findParams = allParams;
                }
                object obj2 = name;
                name = string.Concat(new object[] { obj2, param4.value, "-", param5.value });
            }
            DataTable table = finder.Find(findParams);
            this.grdSearchResult = new DataGridView();
            this.grdSearchResult.DefaultCellStyle = new CommonDataGridViewCellStyle(this.grdSearchResult.DefaultCellStyle);
            TabItem key = base.AddNewTab(this.tabControl, this.grdSearchResult, name, name);
            this.createResultGrid(finder.getFields());
            this.grdSearchResult.DataSource = table;
            this.dicGrid.Add(key, this.grdSearchResult);
        }

        private void tabControl_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            this.tabControl.Tabs.Remove(this.tabControl.SelectedTab);
        }
    }
}
