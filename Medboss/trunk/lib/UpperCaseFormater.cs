using Nammedia.Medboss.Log;
using System;
using System.Windows.Forms;
namespace Nammedia.Medboss.lib
{


    public class UpperCaseFormater : IFormater
    {
        private object _obj;

        public UpperCaseFormater(object obj)
        {
            ((IFormater) this).bindEvent(obj);
            this._obj = obj;
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn) this._obj;
            if (e.ColumnIndex == column.Index)
            {
                column.DataGridView.Rows[e.RowIndex].Cells[column.Index].Value = this.format(column.DataGridView.Rows[e.RowIndex].Cells[column.Index].Value);
            }
        }

        private string format(object text)
        {
            string text2 = "";
            try
            {
                text2 = (string) text;
                return text2.ToUpper();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
            return text2;
        }

        void IFormater.bindEvent(object ctl)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                TextBox box = (TextBox) ctl;
                box.TextChanged += new EventHandler(this.txt_TextChanged);
            }
            else if (ctl.GetType() == typeof(DataGridViewTextBoxColumn))
            {
                DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn) ctl;
                column.DataGridView.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox) sender;
            box.Text = this.format(box.Text);
        }
    }
}
