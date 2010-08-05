namespace Nammedia.Medboss.SqlExpression
{
    using DevComponents.DotNetBar;
    using Nammedia.Medboss;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ExpressionBuilder : UserControl
    {
        private FindField[] _Fields;
        private ToolStripButton butAnd;
        private ToolStripButton butOr;
        private IContainer components;
        private FieldExpressionBuilder fieldExpressionBuilder1;
        private DevComponents.DotNetBar.TabControl tabAnd;
        private TabControlPanel tabControlPanel2;
        private TabControlPanel tabControlPanel3;
        private TabItem tabItemAnd;
        private TabItem tabItemOr;
        private DevComponents.DotNetBar.TabControl tabOr;
        private ToolStrip toolStrip1;
        private ToolStripContainer toolStripContainer1;

        public ExpressionBuilder()
        {
            this.components = null;
            this.InitializeComponent();
            this.tabOr.TabItemClose += new TabStrip.UserActionEventHandler(this.tab_close);
        }

        public ExpressionBuilder(FindField[] findFields)
        {
            this.components = null;
            this.InitializeComponent();
            this.tabOr.TabItemClose += new TabStrip.UserActionEventHandler(this.tab_close);
            this.fieldExpressionBuilder1.setFields(findFields);
            this._Fields = new FindField[findFields.Length];
            findFields.CopyTo(this._Fields, 0);
        }

        private TabItem AddNewAndTab(DevComponents.DotNetBar.TabControl tabAndControl)
        {
            tabAndControl.SuspendLayout();
            TabItem item = new TabItem();
            tabAndControl.Tabs.Add(item);
            TabControlPanel panel = new TabControlPanel();
            tabAndControl.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            item.AttachedControl = panel;
            panel.TabItem = item;
            panel.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Right | eBorderSide.Left;
            panel.Style.GradientAngle = 90;
            FieldExpressionBuilder builder = new FieldExpressionBuilder();
            builder.setFields(this._Fields);
            panel.Controls.Add(builder);
            builder.Dock = DockStyle.Fill;
            item.Text = "V\x00e0";
            item.Name = "V\x00e0";
            tabAndControl.SelectedTab = item;
            tabAndControl.CloseButtonVisible = true;
            tabAndControl.ResumeLayout();
            return item;
        }

        private TabItem AddNewOrTab()
        {
            TabItem item = new TabItem();
            this.tabOr.Tabs.Add(item);
            TabControlPanel panel = new TabControlPanel();
            this.tabOr.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            item.AttachedControl = panel;
            panel.TabItem = item;
            DevComponents.DotNetBar.TabControl control = new DevComponents.DotNetBar.TabControl();
            control.AutoHideSystemBox = false;
            control.CloseButtonVisible = true;
            panel.Controls.Add(control);
            control.TabItemClose += new TabStrip.UserActionEventHandler(this.tab_close);
            control.Dock = DockStyle.Fill;
            item.Text = "Hoặc";
            item.Name = "Hoặc";
            this.tabAnd.SelectedTab = item;
            this.tabOr.CloseButtonVisible = true;
            this.tabOr.AutoHideSystemBox = false;
            return item;
        }

        private void butAnd_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabControl control in this.tabOr.SelectedTab.AttachedControl.Controls)
            {
                this.AddNewAndTab(control);
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
        }

        private void butOr_Click(object sender, EventArgs e)
        {
            this.AddNewOrTab();
        }

        public void clear()
        {
            this.tabOr.Tabs.Clear();
            this.AddNewOrTab();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void fieldExpressionBuilder1_Load(object sender, EventArgs e)
        {
        }

        public SqlPrimitiveExpression[][] getExpressions()
        {
            SqlPrimitiveExpression[][] expressionArray = new SqlPrimitiveExpression[this.tabOr.Tabs.Count][];
            int index = 0;
            foreach (TabItem item in this.tabOr.Tabs)
            {
                DevComponents.DotNetBar.TabControl control = (DevComponents.DotNetBar.TabControl) item.AttachedControl.Controls[0];
                expressionArray[index] = new SqlPrimitiveExpression[control.Tabs.Count];
                int num2 = 0;
                foreach (TabItem item2 in control.Tabs)
                {
                    SqlPrimitiveExpression expression = ((FieldExpressionBuilder) item2.AttachedControl.Controls[0]).getSqlPrimitiveExpression();
                    expressionArray[index][num2++] = expression;
                }
                index++;
            }
            return expressionArray;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ExpressionBuilder));
            this.tabAnd = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel2 = new TabControlPanel();
            this.fieldExpressionBuilder1 = new FieldExpressionBuilder();
            this.tabItemAnd = new TabItem(this.components);
            this.tabOr = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel3 = new TabControlPanel();
            this.tabItemOr = new TabItem(this.components);
            this.toolStrip1 = new ToolStrip();
            this.butAnd = new ToolStripButton();
            this.butOr = new ToolStripButton();
            this.toolStripContainer1 = new ToolStripContainer();
            ((ISupportInitialize) this.tabAnd).BeginInit();
            this.tabAnd.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((ISupportInitialize) this.tabOr).BeginInit();
            this.tabOr.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            base.SuspendLayout();
            this.tabAnd.CanReorderTabs = true;
            this.tabAnd.Controls.Add(this.tabControlPanel2);
            this.tabAnd.Dock = DockStyle.Fill;
            this.tabAnd.Location = new Point(1, 1);
            this.tabAnd.Name = "tabAnd";
            this.tabAnd.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabAnd.SelectedTabIndex = 0;
            this.tabAnd.Size = new Size(0x259, 0x9f);
            this.tabAnd.TabIndex = 1;
            this.tabAnd.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabAnd.Tabs.Add(this.tabItemAnd);
            this.tabControlPanel2.Controls.Add(this.fieldExpressionBuilder1);
            this.tabControlPanel2.Dock = DockStyle.Fill;
            this.tabControlPanel2.Location = new Point(0, 0x1a);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new Padding(1);
            this.tabControlPanel2.Size = new Size(0x259, 0x85);
            this.tabControlPanel2.Style.BackColor1.Color = Color.FromArgb(0x90, 0x9c, 0xbb);
            this.tabControlPanel2.Style.BackColor2.Color = Color.FromArgb(230, 0xe9, 240);
            this.tabControlPanel2.Style.Border = eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = SystemColors.ControlDark;
            this.tabControlPanel2.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Right | eBorderSide.Left;
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItemAnd;
            this.fieldExpressionBuilder1.BackColor = SystemColors.Control;
            this.fieldExpressionBuilder1.Dock = DockStyle.Fill;
            this.fieldExpressionBuilder1.Location = new Point(1, 1);
            this.fieldExpressionBuilder1.Name = "fieldExpressionBuilder1";
            this.fieldExpressionBuilder1.Size = new Size(0x257, 0x83);
            this.fieldExpressionBuilder1.TabIndex = 0;
            this.tabItemAnd.AttachedControl = this.tabControlPanel2;
            this.tabItemAnd.Name = "tabItemAnd";
            this.tabItemAnd.PredefinedColor = eTabItemColor.Default;
            this.tabItemAnd.Text = "V\x00e0";
            this.tabOr.CanReorderTabs = true;
            this.tabOr.Controls.Add(this.tabControlPanel3);
            this.tabOr.Dock = DockStyle.Fill;
            this.tabOr.Location = new Point(0, 0);
            this.tabOr.Name = "tabOr";
            this.tabOr.SelectedTabFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            this.tabOr.SelectedTabIndex = 0;
            this.tabOr.Size = new Size(0x275, 0xa1);
            this.tabOr.TabAlignment = eTabStripAlignment.Left;
            this.tabOr.TabIndex = 2;
            this.tabOr.TabLayoutType = eTabLayoutType.FixedWithNavigationBox;
            this.tabOr.Tabs.Add(this.tabItemOr);
            this.tabControlPanel3.Controls.Add(this.tabAnd);
            this.tabControlPanel3.Dock = DockStyle.Fill;
            this.tabControlPanel3.Location = new Point(0x1a, 0);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new Padding(1);
            this.tabControlPanel3.Size = new Size(0x25b, 0xa1);
            this.tabControlPanel3.Style.BackColor1.Color = Color.FromArgb(0x90, 0x9c, 0xbb);
            this.tabControlPanel3.Style.BackColor2.Color = Color.FromArgb(230, 0xe9, 240);
            this.tabControlPanel3.Style.Border = eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = SystemColors.ControlDark;
            this.tabControlPanel3.Style.BorderSide = eBorderSide.Bottom | eBorderSide.Top | eBorderSide.Right;
            this.tabControlPanel3.TabIndex = 1;
            this.tabControlPanel3.TabItem = this.tabItemOr;
            this.tabItemOr.AttachedControl = this.tabControlPanel3;
            this.tabItemOr.Name = "tabItemOr";
            this.tabItemOr.PredefinedColor = eTabItemColor.Default;
            this.tabItemOr.Text = "Hoặc";
            this.toolStrip1.BackColor = SystemColors.Control;
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.butAnd, this.butOr });
            this.toolStrip1.Location = new Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x44, 0x19);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.butAnd.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.butAnd.Image = (Image) manager.GetObject("butAnd.Image");
            this.butAnd.ImageTransparentColor = Color.Magenta;
            this.butAnd.Name = "butAnd";
            this.butAnd.Size = new Size(0x17, 0x16);
            this.butAnd.Text = "V\x00e0";
            this.butAnd.Click += new EventHandler(this.butAnd_Click);
            this.butOr.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.butOr.Image = (Image) manager.GetObject("butOr.Image");
            this.butOr.ImageTransparentColor = Color.Magenta;
            this.butOr.Name = "butOr";
            this.butOr.Size = new Size(0x23, 0x16);
            this.butOr.Text = "Hoặc";
            this.butOr.Click += new EventHandler(this.butOr_Click);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabOr);
            this.toolStripContainer1.ContentPanel.Size = new Size(0x275, 0xa1);
            this.toolStripContainer1.Dock = DockStyle.Fill;
            this.toolStripContainer1.Location = new Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new Size(0x275, 0xba);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.toolStripContainer1);
            base.Name = "ExpressionBuilder";
            base.Size = new Size(0x275, 0xba);
            ((ISupportInitialize) this.tabAnd).EndInit();
            this.tabAnd.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((ISupportInitialize) this.tabOr).EndInit();
            this.tabOr.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void setExpressions(SqlPrimitiveExpression[][] exps)
        {
            this.tabOr.Tabs.Clear();
            foreach (SqlPrimitiveExpression[] expressionArray in exps)
            {
                TabItem item = this.AddNewOrTab();
                foreach (SqlPrimitiveExpression expression in expressionArray)
                {
                    ((FieldExpressionBuilder) this.AddNewAndTab((DevComponents.DotNetBar.TabControl) item.AttachedControl.Controls[0]).AttachedControl.Controls[0]).setSqlPrimitiveExpression(expression);
                }
            }
        }

        public void setFields(FindField[] findFields)
        {
            this._Fields = new FindField[findFields.Length];
            findFields.CopyTo(this._Fields, 0);
        }

        private void tab_close(object sender, TabStripActionEventArgs e)
        {
            DevComponents.DotNetBar.TabControl control = (DevComponents.DotNetBar.TabControl) sender;
            control.Tabs.Remove(control.SelectedTab);
        }
    }
}
