namespace Nammedia.Medboss
{
    using DevComponents.DotNetBar;
    using Nammedia.Medboss.controls;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.SqlExpression;
    using Nammedia.Medboss.Style;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class AdvanceSearcher_2 : OperatorBase
    {
        private string _condition = "";
        private Button butBuild;
        private Button butTim;
        private Button butXem;
        private ComboBox cboLoaiDuLieu;
        private DataGridViewTextBoxColumn colMa;
        private IContainer components = null;
        private Dictionary<DataType, string> dataTypeStr = new Dictionary<DataType, string>();
        private DataGridView dgrDieuKien;
        private DataGridView dgrKetQua;
        private Dictionary<DataType, IFinder> dicFinder = new Dictionary<DataType, IFinder>();
        private FindField[] findFields;
        private Hashtable findFieldsHash;
        private bool init = true;
        private Label lblLoaiDuLieu;
        private Panel panel1;
        private DevComponents.DotNetBar.TabControl tabControl;
        private TabControlPanel tabControlPanel1;
        private TabItem tabItem1;
        private TableLayoutPanel tableLayoutPanel1;

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

        public AdvanceSearcher_2()
        {
            this.InitializeComponent();
            this.initFinderMap();
            this.initStr2DataType();
            this.initCboDataType();
            this.init = false;
            this.dgrDieuKien.CellEndEdit += new DataGridViewCellEventHandler(this.dgrDieuKien_CellEndEdit);
        }

        protected override void _validate()
        {
            DataType selectedValue = (DataType) this.cboLoaiDuLieu.SelectedValue;
            IFinder finder = this.dicFinder[selectedValue];
            DataTable table = finder.AdvanceFind(this._condition);
            this.dgrKetQua = new DataGridView();
            base.AddNewTab(this.tabControl, this.dgrKetQua, this._condition, this._condition);
            this.createResultGrid(finder.getFields());
            this.dgrKetQua.DataSource = table;
        }

        private string autoFormat(System.Type type, string input)
        {
            string[] textArray = new string[] { "like", "between", "and", "(<=|<|>|>=|=)" };
            string text = @"[-]?\s*(\d*[.])?\d+";
            string text3 = "";
            string text4 = input;
            if ((type == typeof(int)) || (type == typeof(double)))
            {
                text3 = text;
                return text4;
            }
            if ((type != typeof(string)) && (type == typeof(DateTime)))
            {
            }
            return text4;
        }

        private void butBuild_Click(object sender, EventArgs e)
        {
            if (this.findFields != null)
            {
                int index;
                FindField[] findFields = new FindField[this.findFields.Length - 1];
                for (index = 1; index < this.findFields.Length; index++)
                {
                    findFields[index - 1] = this.findFields[index];
                }
                SqlExpressionBuilder builder = new SqlExpressionBuilder(findFields);
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    SqlPrimitiveExpression[][] expressionArray = builder.getExpressions();
                    string text = "";
                    for (index = 0; index < expressionArray.Length; index++)
                    {
                        SqlPrimitiveExpression[] expressionArray2 = expressionArray[index];
                        text = text + "(";
                        for (int i = 0; i < expressionArray2.Length; i++)
                        {
                            text = text + "(" + expressionArray2[i].toSqlString() + ")";
                            if (i < (expressionArray2.Length - 1))
                            {
                                text = text + " and ";
                            }
                        }
                        text = text + ")";
                        if (index < (expressionArray.Length - 1))
                        {
                            text = text + " or ";
                        }
                    }
                    this._condition = text;
                }
            }
        }

        private void butTim_Click(object sender, EventArgs e)
        {
            base.Validate();
        }

        private void butXem_Click(object sender, EventArgs e)
        {
            if (this.dgrKetQua.SelectedRows.Count > 0)
            {
                ViewFunc func = (ViewFunc) base.eventTable["SelectedItemView"];
                ViewArg args = new ViewArg((DataType) this.cboLoaiDuLieu.SelectedValue, ConvertHelper.getInt(this.dgrKetQua.SelectedRows[0].Cells[this.colMa.Index].Value));
                if (func != null)
                {
                    func(args);
                }
            }
        }

        private void cboLoaiDuLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.init)
            {
                DataType selectedValue = (DataType) this.cboLoaiDuLieu.SelectedValue;
                this.findFields = this.dicFinder[selectedValue].getFields();
                this.dgrDieuKien.Columns.Clear();
                this.findFieldsHash = new Hashtable();
                for (int i = 1; i < this.findFields.Length; i++)
                {
                    FindField field = this.findFields[i];
                    int key = this.dgrDieuKien.Columns.Add(field.Field, field.DisplayField);
                    this.findFieldsHash.Add(key, field);
                }
                this.dgrDieuKien.RowCount = 2;
            }
        }

        private void createResultGrid(FindField[] fields)
        {
            this.dgrKetQua.Columns.Clear();
            this.dgrKetQua.AutoGenerateColumns = false;
            for (int i = 1; i < fields.Length; i++)
            {
                DataGridViewTextBoxColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
                dataGridViewColumn.DataPropertyName = fields[i].DisplayField;
                dataGridViewColumn.Name = fields[i].Field;
                dataGridViewColumn.HeaderText = fields[i].DisplayField;
                this.dgrKetQua.Columns.Add(dataGridViewColumn);
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

        private void dgrDieuKien_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            FindField field = (FindField) this.findFieldsHash[e.ColumnIndex];
            DataGridViewCell cell = this.dgrDieuKien.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string input = (string) cell.Value;
            cell.Value = this.autoFormat(field.Type, input);
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
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.dgrDieuKien = new DataGridView();
            this.dgrKetQua = new DataGridView();
            this.colMa = new DataGridViewTextBoxColumn();
            this.cboLoaiDuLieu = new ComboBox();
            this.lblLoaiDuLieu = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.panel1 = new Panel();
            this.butXem = new Button();
            this.butTim = new Button();
            this.tabControl = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new TabControlPanel();
            this.tabItem1 = new TabItem(this.components);
            this.butBuild = new Button();
            ((ISupportInitialize) this.dgrDieuKien).BeginInit();
            ((ISupportInitialize) this.dgrKetQua).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.tabControl).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            base.SuspendLayout();
            this.dgrDieuKien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrDieuKien.Dock = DockStyle.Fill;
            this.dgrDieuKien.Location = new Point(3, 0x21);
            this.dgrDieuKien.Name = "dgrDieuKien";
            this.dgrDieuKien.Size = new Size(0x2ab, 0x90);
            this.dgrDieuKien.TabIndex = 0;
            this.dgrKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrKetQua.Columns.AddRange(new DataGridViewColumn[] { this.colMa });
            this.dgrKetQua.Dock = DockStyle.Fill;
            this.dgrKetQua.Location = new Point(1, 1);
            this.dgrKetQua.Name = "dgrKetQua";
            this.dgrKetQua.Size = new Size(0x2a9, 0xf3);
            this.dgrKetQua.TabIndex = 1;
            this.colMa.DataPropertyName = "Ma";
            this.colMa.HeaderText = "M\x00e3";
            this.colMa.Name = "colMa";
            this.colMa.Visible = false;
            this.cboLoaiDuLieu.FormattingEnabled = true;
            this.cboLoaiDuLieu.Location = new Point(0x52, 0);
            this.cboLoaiDuLieu.Name = "cboLoaiDuLieu";
            this.cboLoaiDuLieu.Size = new Size(0xa1, 0x15);
            this.cboLoaiDuLieu.TabIndex = 2;
            this.cboLoaiDuLieu.SelectedIndexChanged += new EventHandler(this.cboLoaiDuLieu_SelectedIndexChanged);
            this.lblLoaiDuLieu.AutoSize = true;
            this.lblLoaiDuLieu.Location = new Point(3, 3);
            this.lblLoaiDuLieu.Name = "lblLoaiDuLieu";
            this.lblLoaiDuLieu.Size = new Size(0x3d, 13);
            this.lblLoaiDuLieu.TabIndex = 3;
            this.lblLoaiDuLieu.Text = "Loại dữ liệu";
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgrDieuKien, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 2);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 150f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new Size(0x2b1, 0x1c9);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.Paint += new PaintEventHandler(this.tableLayoutPanel1_Paint);
            this.panel1.Controls.Add(this.butBuild);
            this.panel1.Controls.Add(this.butXem);
            this.panel1.Controls.Add(this.butTim);
            this.panel1.Controls.Add(this.cboLoaiDuLieu);
            this.panel1.Controls.Add(this.lblLoaiDuLieu);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2ab, 0x18);
            this.panel1.TabIndex = 2;
            this.butXem.Location = new Point(0x1a5, 1);
            this.butXem.Name = "butXem";
            this.butXem.Size = new Size(0x4b, 0x17);
            this.butXem.TabIndex = 5;
            this.butXem.Text = "&Xem";
            this.butXem.UseVisualStyleBackColor = true;
            this.butXem.Click += new EventHandler(this.butXem_Click);
            this.butTim.Location = new Point(330, 0);
            this.butTim.Name = "butTim";
            this.butTim.Size = new Size(0x4b, 0x17);
            this.butTim.TabIndex = 4;
            this.butTim.Text = "&T\x00ecm";
            this.butTim.UseVisualStyleBackColor = true;
            this.butTim.Click += new EventHandler(this.butTim_Click);
            this.tabControl.AutoHideSystemBox = false;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.Controls.Add(this.tabControlPanel1);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(3, 0xb7);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabControl.SelectedTabIndex = 0;
            this.tabControl.Size = new Size(0x2ab, 0x10f);
            this.tabControl.TabIndex = 3;
            this.tabControl.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabControl.Tabs.Add(this.tabItem1);
            this.tabControl.TabItemClose += new TabStrip.UserActionEventHandler(this.tabControl_TabItemClose);
            this.tabControlPanel1.Controls.Add(this.dgrKetQua);
            this.tabControlPanel1.Dock = DockStyle.Fill;
            this.tabControlPanel1.Location = new Point(0, 0x1a);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new Padding(1);
            this.tabControlPanel1.Size = new Size(0x2ab, 0xf5);
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
            this.butBuild.Location = new Point(0x1f6, 0);
            this.butBuild.Name = "butBuild";
            this.butBuild.Size = new Size(0x4b, 0x17);
            this.butBuild.TabIndex = 6;
            this.butBuild.Text = "Tạ&o";
            this.butBuild.UseVisualStyleBackColor = true;
            this.butBuild.Click += new EventHandler(this.butBuild_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "AdvanceSearcher";
            base.Size = new Size(0x2b1, 0x1c9);
            ((ISupportInitialize) this.dgrDieuKien).EndInit();
            ((ISupportInitialize) this.dgrKetQua).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.tabControl).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void initStr2DataType()
        {
            this.dataTypeStr.Add(DataType.HoaDonBan, "Ho\x00e1 đơn b\x00e1n");
            this.dataTypeStr.Add(DataType.HoaDonNhap, "Ho\x00e1 đơn nhập");
            this.dataTypeStr.Add(DataType.KhoanThuChi, "Khoản thu chi");
            this.dataTypeStr.Add(DataType.KiemKe, "Kiểm k\x00ea");
            this.dataTypeStr.Add(DataType.TraLaiThuocNhap, "Trả lại thuốc nhập");
            this.dataTypeStr.Add(DataType.NhanThuocTraLai, "Nhận thuốc trả lại");
            this.dataTypeStr.Add(DataType.ChuyenQuay, "Chuyển quầy");
        }

        private ParseResult parse(System.Type type, string input)
        {
            string output = (string) input.Clone();
            string text2 = @"[-]?\s*(\d*[.])?\d+";
            string pattern = @"#\d?\d/\d?\d/(\d\d)?\d\d#";
            string text4 = @"'\s*.*\s*'";
            string text5 = "";
            if ((type == typeof(int)) || (type == typeof(double)))
            {
                text5 = text2;
            }
            else if (type == typeof(string))
            {
                text5 = text4;
            }
            else if (type == typeof(DateTime))
            {
                text5 = pattern;
                Match match = new Regex(pattern).Match(input);
                foreach (Capture capture in match.Captures)
                {
                    string oldValue = capture.ToString();
                    char[] separator = new char[] { '#' };
                    string[] textArray = oldValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        string newValue = "#" + ConvertHelper.getTimeFormat(textArray[0]).ToString("MM/dd/yyyy") + "#";
                        output = output.Replace(oldValue, newValue);
                    }
                    catch (InvalidException)
                    {
                        return new ParseResult(Nammedia.Medboss.ConditionType.Value, false, output);
                    }
                }
            }
            string text8 = @"^\s*" + text5 + @"\s*$";
            string text9 = @"^\s*(<=|<|>|>=|=)\s*" + text5 + @"\s*$";
            string text10 = @"^\s*between\s*" + text5 + @"\s*and\s*" + text5 + @"\s*$";
            string text11 = @"^\s*like\s*" + text5 + @"\s*$";
            if (new Regex(text8).Match(output).Success)
            {
                return new ParseResult(Nammedia.Medboss.ConditionType.Value, true, output);
            }
            if (new Regex(text9).Match(output).Success)
            {
                return new ParseResult(Nammedia.Medboss.ConditionType.Compare, true, output);
            }
            if (new Regex(text10).Match(output).Success)
            {
                return new ParseResult(Nammedia.Medboss.ConditionType.Between, true, output);
            }
            if (new Regex(text11).Match(output).Success)
            {
                return new ParseResult(Nammedia.Medboss.ConditionType.Like, true, output);
            }
            return new ParseResult(Nammedia.Medboss.ConditionType.Value, false, output);
        }

        private string parseCondition()
        {
            DataType selectedValue = (DataType) this.cboLoaiDuLieu.SelectedValue;
            IFinder finder = this.dicFinder[selectedValue];
            string text = "";
            foreach (DataGridViewRow row in (IEnumerable) this.dgrDieuKien.Rows)
            {
                string text2 = "";
                for (int i = 0; i < this.dgrDieuKien.Columns.Count; i++)
                {
                    FindField field = (FindField) this.findFieldsHash[i];
                    if (row.Cells[this.dgrDieuKien.Columns[field.Field].Index].Value != null)
                    {
                        string input = (string) row.Cells[this.dgrDieuKien.Columns[field.Field].Index].Value;
                        ParseResult result = this.parse(field.Type, input);
                        if (!result.Success)
                        {
                            throw new InvalidException(field.DisplayField + ": " + input);
                        }
                        if (text2 != "")
                        {
                            text2 = text2 + " and ";
                        }
                        if (result.Type == Nammedia.Medboss.ConditionType.Value)
                        {
                            text2 = text2 + field.Field + "=" + result.Output;
                        }
                        else
                        {
                            text2 = text2 + field.Field + " " + result.Output;
                        }
                    }
                }
                if ((text != "") && (text2 != ""))
                {
                    text = text + " or ";
                }
                if (text2 != "")
                {
                    text = text + "(" + text2 + ")";
                }
            }
            return text;
        }

        private void Search()
        {
        }

        private void tabControl_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            this.tabControl.Tabs.Remove(this.tabControl.SelectedTab);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
