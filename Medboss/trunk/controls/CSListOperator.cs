using Nammedia.Medboss;
using Nammedia.Medboss.lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Nammedia.Medboss.controls
{

    public class CSListOperator : OperatorBase
    {
        private ComboBox cboCSList;
        private IContainer components = null;
        private CSInfoUIOperator csuiOper;
        private Label label1;

        public CSListOperator()
        {
            this.InitializeComponent();
            this.loadAC();
            this.csuiOper.DeleteFinished += new DeleteFinishHandler(this.csuiOper_DeleteFinished);
        }

        private void cboCSList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CSController controller = new CSController();
            CSInfo csi = this.cboCSList.SelectedItem as CSInfo;
            this.csuiOper.loadCSInfo(csi);
        }

        private void csuiOper_DeleteFinished(object sender, OperatorArgument args)
        {
            Program.ACSource.RefreshCSSource();
            this.RefreshAC();
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
            this.cboCSList = new ComboBox();
            this.csuiOper = new CSInfoUIOperator();
            this.label1 = new Label();
            base.SuspendLayout();
            this.cboCSList.FormattingEnabled = true;
            this.cboCSList.Location = new Point(0xa9, 0x16);
            this.cboCSList.Name = "cboCSList";
            this.cboCSList.Size = new Size(0x79, 0x15);
            this.cboCSList.TabIndex = 0;
            this.cboCSList.SelectedIndexChanged += new EventHandler(this.cboCSList_SelectedIndexChanged);
            this.csuiOper.Location = new Point(60, 0x3b);
            this.csuiOper.Name = "csuiOper";
            this.csuiOper.Size = new Size(230, 0xec);
            this.csuiOper.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x44, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh s\x00e1ch đối t\x00e1c";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.csuiOper);
            base.Controls.Add(this.cboCSList);
            base.Name = "CSListOperator";
            base.Size = new Size(0x15b, 0x134);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public override void loadAC()
        {
            base._acFactory.EnableAutocomplete(this.cboCSList, ref this._csSource);
        }

        public override void RefreshAC()
        {
            base._acFactory.RefreshAutoCompleteSource(this.cboCSList, ref this._csSource);
        }
    }
}
