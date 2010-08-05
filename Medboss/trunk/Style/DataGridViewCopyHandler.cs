using LumenWorks.Framework.IO.Csv;
using Nammedia.Medboss.Log;
using System;
using System.IO;
using System.Windows.Forms;
namespace Nammedia.Medboss.Style
{


    public class DataGridViewCopyHandler
    {
        public static void addDataGridViewClient(DataGridView dgrview)
        {
            dgrview.KeyDown += new KeyEventHandler(DataGridViewCopyHandler.dgrview_KeyDown);
            dgrview.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
        }

        private static void copyRegion()
        {
            DataObject data = new DataObject();
            data.SetData(DataFormats.CommaSeparatedValue, true, "abc,def");
            Clipboard.SetDataObject(data, true);
        }

        private static void dgrview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.V))
            {
                pasteRegion((DataGridView) sender);
            }
        }

        private static DataGridViewCell getLeftTopCell(DataGridViewSelectedCellCollection collection)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell cell2 in collection)
            {
                if (cell == null)
                {
                    cell = cell2;
                }
                else if ((cell.ColumnIndex > cell2.ColumnIndex) || (cell.RowIndex > cell2.RowIndex))
                {
                    cell = cell2;
                }
            }
            return cell;
        }

        private static void pasteRegion(DataGridView grid)
        {
            try
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if ((dataObject != null) && dataObject.GetDataPresent(DataFormats.CommaSeparatedValue))
                {
                    object data = dataObject.GetData(DataFormats.CommaSeparatedValue);
                    if (data.GetType() == typeof(MemoryStream))
                    {
                        StreamReader reader = new StreamReader((Stream) data);
                        pasteStringCsv(reader.ReadToEnd(), grid);
                        reader.Close();
                    }
                    else if (data.GetType() == typeof(string))
                    {
                        pasteStringCsv((string) data, grid);
                    }
                }
            }
            catch (Exception exception)
            {
                LogManager.LogException(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private static void pasteStringCsv(string data, DataGridView grid)
        {
            if (grid.SelectedCells.Count > 0)
            {
                DataGridViewCell cell = getLeftTopCell(grid.SelectedCells);
                int rowIndex = cell.RowIndex;
                CachedCsvReader reader = new CachedCsvReader(new StringReader(data), true);
                reader.MoveToStart();
                bool hasHeaders = reader.HasHeaders;
                while (reader.ReadNextRecord())
                {
                    DataGridViewRow row = cell.DataGridView.Rows[rowIndex];
                    if (row.IsNewRow)
                    {
                        rowIndex = cell.DataGridView.Rows.Add();
                        row = cell.DataGridView.Rows[rowIndex];
                    }
                    for (int i = 0; i < (reader.FieldCount - 1); i++)
                    {
                        int num3 = cell.ColumnIndex + i;
                        if (num3 >= cell.DataGridView.Columns.Count)
                        {
                            break;
                        }
                        row.Cells[num3].Value = reader[i + 1];
                    }
                    rowIndex++;
                }
            }
        }
    }
}
