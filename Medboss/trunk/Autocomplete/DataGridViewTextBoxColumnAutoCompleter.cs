namespace Nammedia.Medboss.Autocomplete
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class DataGridViewTextBoxColumnAutoCompleter : AutoCompleter
    {
        private DataGridViewTextBoxColumn _col;
        private ArrayList _dataSource;
        private static bool _enable = false;

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == this._col.Index)
            {
                _enable = true;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _enable = false;
        }

        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            AutoCompleter completer;
            DataGridView view = (DataGridView) sender;
            if (view.CurrentCell.ColumnIndex == this._col.Index)
            {
                if (_enable)
                {
                    completer = new TextBoxAutocompleter();
                    completer.AutoCompleteMode = base.AutoCompleteMode;
                    completer.EnableAutoCompleted(e.Control, ref this._dataSource);
                }
            }
            else if (!_enable)
            {
                completer = new TextBoxAutocompleter();
                completer.AutoCompleteMode = base.AutoCompleteMode;
                completer.DisableAutoCompleted(e.Control);
            }
        }

        public override void DisableAutoCompleted(object control)
        {
        }

        public override void EnableAutoCompleted(object column, ref ArrayList dataSource)
        {
            this._col = (DataGridViewTextBoxColumn) column;
            this._dataSource = dataSource;
            this._col.DataGridView.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.DataGridView_CellBeginEdit);
            this._col.DataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGridView_EditingControlShowing);
            this._col.DataGridView.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
        }

        public override void RefreshSource(ref ArrayList Source)
        {
            this._dataSource = Source;
        }
    }
}
