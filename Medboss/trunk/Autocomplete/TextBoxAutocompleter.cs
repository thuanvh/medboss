namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class TextBoxAutocompleter : AutoCompleter
    {
        private TextBox textbox;

        public override void DisableAutoCompleted(object control)
        {
            if (control is TextBox)
            {
                this.textbox = (TextBox) control;
                this.textbox.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        public override void EnableAutoCompleted(object control, ref ArrayList datasource)
        {
            this.textbox = (TextBox) control;
            AutoCompleteStringCollection array = new AutoCompleteStringCollection();
            ArrayList list = datasource;
            array.AddRange(Array2StringArray.Array2StrArr(list));
            this.SetACSafe(array);
        }

        public override void RefreshSource(ref ArrayList datasource)
        {
            string text = this.textbox.Text;
            AutoCompleteStringCollection array = new AutoCompleteStringCollection();
            ArrayList list = datasource;
            array.AddRange(Array2StringArray.Array2StrArr(list));
            this.SetACSafe(array);
            this.textbox.Text = text;
        }

        private void SetACSafe(AutoCompleteStringCollection array)
        {
            this.textbox.AutoCompleteMode = base.AutoCompleteMode;
            this.textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textbox.AutoCompleteCustomSource = array;
        }

        private delegate void SetAC(AutoCompleteStringCollection array);
    }
}
