namespace Nammedia.Medboss.SqlExpression
{
    using Nammedia.Medboss;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FieldExpressionBuilder : UserControl
    {
        private ConditionTypeConstants _conds;
        private FindField[] _fields;
        private ComboBox cboCondition;
        private DateTimePicker cboDenNgay;
        private ComboBox cboField;
        private ComboBox cboFieldValue;
        private DateTimePicker cboTuNgay;
        private CheckBox chkNot;
        private IContainer components = null;
        private GroupBox grpValue;
        private Label label1;
        private Label label2;
        private Label lblEndValue;
        private Label lblStartValue;
        private RadioButton radFieldValue;
        private RadioButton radScalarValue;
        private TextBox txtDenValue;
        private TextBox txtScalarValue;

        public FieldExpressionBuilder()
        {
            this.InitializeComponent();
        }

        public void binding()
        {
            FindField[] array = new FindField[this._fields.Length];
            FindField[] fieldArray2 = new FindField[this._fields.Length];
            this._fields.CopyTo(array, 0);
            this._fields.CopyTo(fieldArray2, 0);
            this.cboField.DataSource = array;
            this.cboFieldValue.DataSource = fieldArray2;
            this.cboCondition.DataSource = this._conds.getAllConditionTypeComplex();
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConditionTypeComplex selectedValue = (ConditionTypeComplex) this.cboCondition.SelectedValue;
            FindField field = (FindField) this.cboField.SelectedValue;
            switch (selectedValue.ParameterNumbers)
            {
                case 0:
                    this.grpValue.Enabled = false;
                    break;

                case 1:
                    this.grpValue.Enabled = true;
                    this.cboFieldValue.Enabled = true;
                    this.radFieldValue.Enabled = true;
                    this.cboDenNgay.Visible = false;
                    this.txtDenValue.Visible = false;
                    this.cboTuNgay.Visible = field.Type == typeof(DateTime);
                    this.txtScalarValue.Visible = !this.cboTuNgay.Visible;
                    this.lblEndValue.Visible = false;
                    this.lblStartValue.Visible = false;
                    break;

                case 2:
                    this.grpValue.Enabled = true;
                    this.cboFieldValue.Enabled = false;
                    this.radFieldValue.Checked = false;
                    this.radFieldValue.Enabled = false;
                    this.radScalarValue.Checked = true;
                    this.cboTuNgay.Visible = this.cboDenNgay.Visible = field.Type == typeof(DateTime);
                    this.txtDenValue.Visible = this.txtScalarValue.Visible = !this.cboTuNgay.Visible;
                    this.lblEndValue.Visible = true;
                    this.lblStartValue.Visible = true;
                    break;
            }
        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindField selectedValue = (FindField) this.cboField.SelectedValue;
            this.cboFieldValue.DataSource = this.getFieldsByType(selectedValue.Type);
            if (selectedValue.Type == typeof(DateTime))
            {
                this.txtDenValue.Visible = false;
                this.txtScalarValue.Visible = false;
                this.cboTuNgay.Visible = true;
                this.cboDenNgay.Visible = true;
            }
            else
            {
                this.txtDenValue.Visible = true;
                this.txtScalarValue.Visible = true;
                this.cboTuNgay.Visible = false;
                this.cboDenNgay.Visible = false;
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

        private ArrayList getFieldsByType(System.Type type)
        {
            ArrayList list = new ArrayList();
            foreach (FindField field in this._fields)
            {
                if (field.Type == type)
                {
                    list.Add(field);
                }
            }
            return list;
        }

        public SqlPrimitiveExpression getSqlPrimitiveExpression()
        {
            SqlPrimitiveExpression expression = new SqlPrimitiveExpression();
            expression.IsNot = this.chkNot.Checked;
            expression.ComparedField = this.radFieldValue.Checked;
            expression.Field = (FindField) this.cboField.SelectedValue;
            expression.Condition = (ConditionTypeComplex) this.cboCondition.SelectedValue;
            switch (expression.Condition.ParameterNumbers)
            {
                case 0:
                    return expression;

                case 1:
                    expression.Values = new object[1];
                    if (!expression.ComparedField)
                    {
                        if (expression.Field.Type == typeof(DateTime))
                        {
                            expression.Values[0] = this.cboTuNgay.Value;
                            return expression;
                        }
                        expression.Values[0] = this.txtScalarValue.Text;
                        return expression;
                    }
                    expression.Values[0] = (FindField) this.cboFieldValue.SelectedValue;
                    return expression;

                case 2:
                    expression.Values = new object[2];
                    if (expression.Field.Type == typeof(DateTime))
                    {
                        expression.Values[0] = this.cboTuNgay.Value;
                        expression.Values[1] = this.cboDenNgay.Value;
                        return expression;
                    }
                    expression.Values[0] = this.txtScalarValue.Text;
                    expression.Values[1] = this.txtDenValue.Text;
                    return expression;
            }
            return expression;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboField = new ComboBox();
            this.label2 = new Label();
            this.txtScalarValue = new TextBox();
            this.cboCondition = new ComboBox();
            this.cboFieldValue = new ComboBox();
            this.grpValue = new GroupBox();
            this.txtDenValue = new TextBox();
            this.lblEndValue = new Label();
            this.lblStartValue = new Label();
            this.radFieldValue = new RadioButton();
            this.radScalarValue = new RadioButton();
            this.chkNot = new CheckBox();
            this.cboTuNgay = new DateTimePicker();
            this.cboDenNgay = new DateTimePicker();
            this.grpValue.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trường";
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new Point(0x48, 11);
            this.cboField.Name = "cboField";
            this.cboField.Size = new Size(0x79, 0x15);
            this.cboField.TabIndex = 1;
            this.cboField.SelectedIndexChanged += new EventHandler(this.cboField_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Điều kiện";
            this.txtScalarValue.Location = new Point(90, 40);
            this.txtScalarValue.Name = "txtScalarValue";
            this.txtScalarValue.Size = new Size(0x79, 20);
            this.txtScalarValue.TabIndex = 3;
            this.cboCondition.FormattingEnabled = true;
            this.cboCondition.Location = new Point(0x48, 0x2b);
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Size = new Size(0x79, 0x15);
            this.cboCondition.TabIndex = 4;
            this.cboCondition.SelectedIndexChanged += new EventHandler(this.cboCondition_SelectedIndexChanged);
            this.cboFieldValue.FormattingEnabled = true;
            this.cboFieldValue.Location = new Point(90, 13);
            this.cboFieldValue.Name = "cboFieldValue";
            this.cboFieldValue.Size = new Size(0x79, 0x15);
            this.cboFieldValue.TabIndex = 5;
            this.grpValue.Controls.Add(this.cboDenNgay);
            this.grpValue.Controls.Add(this.cboTuNgay);
            this.grpValue.Controls.Add(this.txtDenValue);
            this.grpValue.Controls.Add(this.lblEndValue);
            this.grpValue.Controls.Add(this.lblStartValue);
            this.grpValue.Controls.Add(this.radFieldValue);
            this.grpValue.Controls.Add(this.radScalarValue);
            this.grpValue.Controls.Add(this.cboFieldValue);
            this.grpValue.Controls.Add(this.txtScalarValue);
            this.grpValue.Location = new Point(0xc7, 3);
            this.grpValue.Name = "grpValue";
            this.grpValue.Size = new Size(0x17e, 0x54);
            this.grpValue.TabIndex = 6;
            this.grpValue.TabStop = false;
            this.grpValue.Text = "Gi\x00e1 trị";
            this.txtDenValue.Location = new Point(250, 40);
            this.txtDenValue.Name = "txtDenValue";
            this.txtDenValue.Size = new Size(0x79, 20);
            this.txtDenValue.TabIndex = 10;
            this.lblEndValue.AutoSize = true;
            this.lblEndValue.Location = new Point(0xd9, 0x2a);
            this.lblEndValue.Name = "lblEndValue";
            this.lblEndValue.Size = new Size(0x1b, 13);
            this.lblEndValue.TabIndex = 9;
            this.lblEndValue.Text = "Đến";
            this.lblStartValue.AutoSize = true;
            this.lblStartValue.Location = new Point(0x40, 0x2a);
            this.lblStartValue.Name = "lblStartValue";
            this.lblStartValue.Size = new Size(20, 13);
            this.lblStartValue.TabIndex = 8;
            this.lblStartValue.Text = "Từ";
            this.radFieldValue.AutoSize = true;
            this.radFieldValue.Location = new Point(6, 15);
            this.radFieldValue.Name = "radFieldValue";
            this.radFieldValue.Size = new Size(0x3b, 0x11);
            this.radFieldValue.TabIndex = 7;
            this.radFieldValue.TabStop = true;
            this.radFieldValue.Text = "Trường";
            this.radFieldValue.UseVisualStyleBackColor = true;
            this.radFieldValue.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.radScalarValue.AutoSize = true;
            this.radScalarValue.Location = new Point(6, 0x29);
            this.radScalarValue.Name = "radScalarValue";
            this.radScalarValue.Size = new Size(0x34, 0x11);
            this.radScalarValue.TabIndex = 6;
            this.radScalarValue.TabStop = true;
            this.radScalarValue.Text = "Gi\x00e1 trị";
            this.radScalarValue.UseVisualStyleBackColor = true;
            this.radScalarValue.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.chkNot.AutoSize = true;
            this.chkNot.Location = new Point(0x48, 70);
            this.chkNot.Name = "chkNot";
            this.chkNot.Size = new Size(0x45, 0x11);
            this.chkNot.TabIndex = 7;
            this.chkNot.Text = "Phủ định";
            this.chkNot.UseVisualStyleBackColor = true;
            this.cboTuNgay.CustomFormat = "dd/MM/yyyy";
            this.cboTuNgay.Format = DateTimePickerFormat.Custom;
            this.cboTuNgay.Location = new Point(0x5b, 0x2a);
            this.cboTuNgay.Name = "cboTuNgay";
            this.cboTuNgay.Size = new Size(120, 20);
            this.cboTuNgay.TabIndex = 11;
            this.cboDenNgay.CustomFormat = "dd/MM/yyyy";
            this.cboDenNgay.Format = DateTimePickerFormat.Custom;
            this.cboDenNgay.Location = new Point(250, 0x29);
            this.cboDenNgay.Name = "cboDenNgay";
            this.cboDenNgay.Size = new Size(120, 20);
            this.cboDenNgay.TabIndex = 12;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkNot);
            base.Controls.Add(this.grpValue);
            base.Controls.Add(this.cboCondition);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboField);
            base.Controls.Add(this.label1);
            base.Name = "FieldExpressionBuilder";
            base.Size = new Size(0x257, 0x65);
            this.grpValue.ResumeLayout(false);
            this.grpValue.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtScalarValue.Enabled = this.radScalarValue.Checked;
            this.cboTuNgay.Enabled = this.radScalarValue.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.cboFieldValue.Enabled = this.radFieldValue.Checked;
        }

        public void setFields(FindField[] fields)
        {
            this._fields = fields;
            this._conds = new ConditionTypeConstants();
            this.binding();
        }

        public void setSqlPrimitiveExpression(SqlPrimitiveExpression exp)
        {
            this.chkNot.Checked = exp.IsNot;
            this.radFieldValue.Checked = exp.ComparedField;
            this.cboField.SelectedValue = exp.Field;
            this.cboCondition.SelectedValue = exp.Condition;
            switch (exp.Condition.ParameterNumbers)
            {
                case 1:
                    if (!exp.ComparedField)
                    {
                        if (exp.Field.Type == typeof(DateTime))
                        {
                            this.txtScalarValue.Text = (string) exp.Values[0];
                        }
                        else
                        {
                            this.cboTuNgay.Value = (DateTime) exp.Values[0];
                        }
                        break;
                    }
                    this.cboFieldValue.SelectedValue = exp.Values[0];
                    break;

                case 2:
                    if (exp.Field.Type == typeof(DateTime))
                    {
                        this.cboTuNgay.Value = (DateTime) exp.Values[0];
                        this.cboDenNgay.Value = (DateTime) exp.Values[1];
                        break;
                    }
                    this.txtScalarValue.Text = (string) exp.Values[0];
                    this.txtDenValue.Text = (string) exp.Values[1];
                    break;
            }
        }
    }
}
