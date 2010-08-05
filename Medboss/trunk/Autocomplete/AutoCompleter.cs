namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public abstract class AutoCompleter
    {
        private System.Windows.Forms.AutoCompleteMode _autoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;

        protected AutoCompleter()
        {
        }

        public abstract void DisableAutoCompleted(object control);
        public abstract void EnableAutoCompleted(object control, ref ArrayList datasource);
        public abstract void RefreshSource(ref ArrayList control);

        public System.Windows.Forms.AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return this._autoCompleteMode;
            }
            set
            {
                this._autoCompleteMode = value;
            }
        }
    }
}
