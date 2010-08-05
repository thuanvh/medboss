namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class AutoCompleteFactory
    {
        private Hashtable _hash = new Hashtable();
        private AutoCompleter ac;

        public void EnableAutocomplete(object obj, ref ArrayList datasource)
        {
            if (obj is TextBox)
            {
                this.ac = new TextBoxAutocompleter();
                this.ac.EnableAutoCompleted(obj, ref datasource);
            }
            else if (obj is ComboBox)
            {
                this.ac = new ComboBoxAutocompleter();
                this.ac.EnableAutoCompleted(obj, ref datasource);
            }
            else if (obj is DataGridViewTextBoxColumn)
            {
                this.ac = new DataGridViewTextBoxColumnAutoCompleter();
                this.ac.EnableAutoCompleted(obj, ref datasource);
            }
            else if (obj is DataGridViewComboBoxColumn)
            {
                this.ac = new DataGridViewComboBoxColumnAutocompleter();
                this.ac.EnableAutoCompleted(obj, ref datasource);
            }
            else
            {
                this.ac = null;
            }
            if (this.ac != null)
            {
                if (this._hash.Contains(obj))
                {
                    this._hash[obj] = this.ac;
                }
                else
                {
                    this._hash.Add(obj, this.ac);
                }
            }
        }

        public void RefreshAutoCompleteSource(object obj, ref ArrayList datasource)
        {
            if (this._hash.Contains(obj))
            {
                AutoCompleter completer = (AutoCompleter) this._hash[obj];
                if (completer != null)
                {
                    completer.RefreshSource(ref datasource);
                }
            }
        }
    }
}
