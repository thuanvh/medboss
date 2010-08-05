namespace Nammedia.Medboss.lib
{
    using Nammedia.Medboss.Log;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class AutoNumberFormater : IFormater
    {
        private DataGridViewTextBoxColumn _control;
        private int _firstValue;
        private int _step = 1;

        public AutoNumberFormater(object obj)
        {
            ((IFormater) this).bindEvent(obj);
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == this._control.Index) && (e.RowIndex == 0))
            {
                try
                {
                    this._firstValue = ConvertHelper.getInt(this._control.DataGridView.Rows[0].Cells[this._control.Index].Value);
                }
                catch (Exception exc)
                {
                    this._firstValue = 1;
                    LogManager.LogException(exc);
                }
                foreach (DataGridViewRow row in (IEnumerable) this._control.DataGridView.Rows)
                {
                    row.Cells[this._control.Index].Value = this._firstValue + (row.Index * this._step);
                }
            }
        }

        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this._control.DataGridView.Rows[e.RowIndex].Cells[this._control.Index].Value = this._firstValue + (e.RowIndex * this._step);
        }

        void IFormater.bindEvent(object ctl)
        {
            if (ctl.GetType() == typeof(DataGridViewTextBoxColumn))
            {
                DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn) ctl;
                this._control = column;
                this._firstValue = 1;
                column.DataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.DataGridView_RowsAdded);
                column.DataGridView.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
                this._control.ValueType = typeof(int);
            }
        }
    }
}
