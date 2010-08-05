namespace Nammedia.Medboss
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class DataGridViewDnDEnabler
    {
        private DataGridView _dataGridView;
        private int _deviderHeight;
        private int _deviderInsertHeight = 1;
        private Rectangle dragBoxFromMouseDown;
        private int rowDeviderIndex;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        public DataGridViewDnDEnabler(DataGridView dataGridView)
        {
            this._dataGridView = dataGridView;
            this._dataGridView.AllowDrop = true;
            this._dataGridView.MouseDown += new MouseEventHandler(this.dataGridView_MouseDown);
            this._dataGridView.MouseMove += new MouseEventHandler(this.dataGridView_MouseMove);
            this._dataGridView.DragOver += new DragEventHandler(this.dataGridView_DragOver);
            this._dataGridView.DragDrop += new DragEventHandler(this.dataGridView_DragDrop);
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            Point point = this._dataGridView.PointToClient(new Point(e.X, e.Y));
            this.rowIndexOfItemUnderMouseToDrop = this._dataGridView.HitTest(point.X, point.Y).RowIndex;
            if (((e.Effect == DragDropEffects.Move) && (this.rowIndexOfItemUnderMouseToDrop != -1)) && (this.rowIndexOfItemUnderMouseToDrop != (this._dataGridView.RowCount - 1)))
            {
                if (this.rowDeviderIndex >= 0)
                {
                    this._dataGridView.Rows[this.rowDeviderIndex].DividerHeight = this._deviderHeight;
                }
                this._dataGridView.ClearSelection();
                this._dataGridView.Rows[this.rowIndexOfItemUnderMouseToDrop].Selected = true;
                DataGridViewRow dataGridViewRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                this._dataGridView.Rows.RemoveAt(this.rowIndexFromMouseDown);
                this._dataGridView.Rows.Insert(this.rowIndexOfItemUnderMouseToDrop, dataGridViewRow);
                foreach (DataGridViewColumn column in this._dataGridView.Columns)
                {
                    if (column.Visible)
                    {
                        this._dataGridView.CurrentCell = this._dataGridView.Rows[this.rowIndexOfItemUnderMouseToDrop].Cells[column.Index];
                        break;
                    }
                }
            }
        }

        private void dataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            Point point = this._dataGridView.PointToClient(new Point(e.X, e.Y));
            int rowIndex = this._dataGridView.HitTest(point.X, point.Y).RowIndex;
            if (rowIndex != -1)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle(this._dataGridView.DefaultCellStyle);
                style.ForeColor = Color.Red;
                DataGridViewAdvancedBorderStyle style2 = new DataGridViewAdvancedBorderStyle();
                style2.Top = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
                if (this.rowDeviderIndex >= 0)
                {
                    this._dataGridView.Rows[this.rowDeviderIndex].DividerHeight = this._deviderHeight;
                }
                this.rowDeviderIndex = rowIndex;
                if (this.rowIndexFromMouseDown > rowIndex)
                {
                    this.rowDeviderIndex--;
                }
                if (this.rowDeviderIndex >= 0)
                {
                    this._dataGridView.Rows[this.rowDeviderIndex].DividerHeight = this._deviderInsertHeight;
                }
            }
        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            this.rowIndexFromMouseDown = -1;
            if (this._dataGridView.SelectedRows.Count == 1)
            {
                this.rowIndexFromMouseDown = this._dataGridView.SelectedRows[0].Index;
            }
            if (this.rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                this.dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) == MouseButtons.Left) && ((this.dragBoxFromMouseDown != Rectangle.Empty) && !this.dragBoxFromMouseDown.Contains(e.X, e.Y)))
            {
                DragDropEffects effects = this._dataGridView.DoDragDrop(this._dataGridView.Rows[this.rowIndexFromMouseDown], DragDropEffects.Move);
            }
        }
    }
}
