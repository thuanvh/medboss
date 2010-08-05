using Nammedia.Medboss.Log;
using Nammedia.Medboss.Utils;
using System;
using System.Windows.Forms;
namespace Nammedia.Medboss.lib
{


    public class DoubleFormater : IFormater
    {
        private object control;

        public DoubleFormater(object obj)
        {
            ((IFormater) this).bindEvent(obj);
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewTextBoxColumn control = (DataGridViewTextBoxColumn) this.control;
            if (e.ColumnIndex == control.Index)
            {
                control.DataGridView.Rows[e.RowIndex].Cells[control.Index].Value = this.format(control.DataGridView.Rows[e.RowIndex].Cells[control.Index].Value);
            }
        }

        private string format(object text)
        {
            if (text == null)
            {
                return "0";
            }
            double a = 0.0;
            if (text.GetType() == typeof(string))
            {
                text = ((string) text).Trim();
            }
            try
            {
                a = ConvertHelper.getDouble(text);
            }
            catch (Exception)
            {
                try
                {
                    a = ConvertHelper.getInt(text);
                }
                catch (Exception exce)
                {
                    LogManager.LogException(exce);
                }
            }
            return ConvertHelper.formatNumber(a);
        }

        void IFormater.bindEvent(object ctl)
        {
            this.control = ctl;
            if (ctl.GetType() == typeof(TextBox))
            {
                TextBox box = (TextBox) ctl;
                box.LostFocus += new EventHandler(this.txt_LostFocus);
                box.TextAlign = HorizontalAlignment.Right;
            }
            else if (ctl.GetType() == typeof(DataGridViewTextBoxColumn))
            {
                DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn) ctl;
                column.DataGridView.CellEndEdit += new DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
                column.ValueType = typeof(double);
            }
        }

        private void txt_LostFocus(object sender, EventArgs e)
        {
            TextBox box = (TextBox) sender;
            box.Text = this.format(box.Text);
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox) sender;
            box.Text = this.format(box.Text);
        }
    }
}
