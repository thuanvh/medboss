namespace Nammedia.Medboss.controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class DateTimePickerVn : UserControl
    {
        private IContainer components = null;

        public DateTimePickerVn()
        {
            this.InitializeComponent();
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
//            base.AutoScaleMode = AutoScaleMode.Font;
        }
    }
}
