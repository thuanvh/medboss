using Nammedia.Medboss.Style;
using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace Nammedia.Medboss.controls
{


    public class SpreadSheet : DataGridView
    {
        private IContainer components = null;

        public SpreadSheet()
        {
            this.InitializeComponent();
            DataGridViewCopyHandler.addDataGridViewClient(this);
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
        }
    }
}
