namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    internal class ComboBoxAutocompleter : AutoCompleter
    {
        private ComboBox cbo;

        public override void DisableAutoCompleted(object control)
        {
            this.cbo = (ComboBox) control;
            this.cbo.AutoCompleteMode = AutoCompleteMode.None;
        }

        public override void EnableAutoCompleted(object obj, ref ArrayList datasource)
        {
            this.cbo = (ComboBox) obj;
            if (datasource != null)
            {
                this.cbo.DataSource = datasource;
            }
            this.cbo.AutoCompleteMode = base.AutoCompleteMode;
            this.cbo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        public override void RefreshSource(ref ArrayList datasource)
        {
            string text = this.cbo.Text;
            if (datasource != null)
            {
                this.cbo.DataSource = datasource;
            }
            this.cbo.AutoCompleteMode = base.AutoCompleteMode;
            this.cbo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cbo.Text = text;
        }
    }
}
