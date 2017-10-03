using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace moleQule.Face
{
	public class ControlsMng
	{
		public static void Center(Control control)
		{
            CenterLeft(control);
            CenterTop(control);
		}
        public static void CenterLeft(Control control)
        {
            control.Left = (control.Parent.Width - control.Width - 1) / 2;
        }
        public static void CenterTop(Control control)
        {
            control.Top = (control.Parent.Height - control.Height - 1) / 2;
        }

		public static void CenterButtons(SplitterPanel panel)
		{
			int botones = 0, espacio = 3, tab, pos = 0, x = 0, y = 0;
			int formWidth = panel.Width;
			int formHeight = panel.Height;
			int totalButtonWidth = 0;

			foreach (Control ctl in panel.Controls)
			{
				if (ctl.GetType().Equals(typeof(Button)) && ctl.Visible)
				{
					botones++;
					totalButtonWidth += ctl.Width;
				}
			}

			// Centrado de los botones
			tab = (formWidth - espacio * (botones - 1) - totalButtonWidth) / 2;
			foreach (Control ctl in panel.Controls)
			{
				if (ctl.GetType().Equals(typeof(Button)) && ctl.Visible)
				{
					x = tab;
					y = (formHeight - ctl.Height - 1) / 2;

					ctl.SetBounds(x, y, ctl.Width, ctl.Height);
					pos++;

					tab = x + espacio + ctl.Width;
				}
			}
		}

        public static bool IsCurrentItemValid(DataGridView grid)
        {
            if (grid.CurrentRow == null) return false;
            if (grid.CurrentRow.Index < 0) return false;
			if (grid.CurrentRow.DataBoundItem == null) return false;
            return true;
        }

        public static object GetCurrentItem(DataGridView grid)
        {
            return grid.CurrentRow.DataBoundItem;
        }

        public static void MarkGridColumn(DataGridView grid, DataGridViewColumn col)
        {
			DataGridViewCellStyle style = new DataGridViewCellStyle();
			style.Font = new Font(grid.ColumnHeadersDefaultCellStyle.Font.FontFamily, grid.ColumnHeadersDefaultCellStyle.Font.Size, FontStyle.Bold);

            MarkGridColumn(grid, col, style);
        }

        public static void MarkGridColumn(DataGridView grid, DataGridViewColumn col, DataGridViewCellStyle style)
        {
			DataGridViewCellStyle header_simple = new DataGridViewCellStyle();

			header_simple = grid.ColumnHeadersDefaultCellStyle;

			foreach (DataGridViewColumn item in grid.Columns)
				item.HeaderCell.Style = (item.Index == col.Index) ? style : header_simple;
        }

        public static DataGridViewColumn GetCurrentColumn(DataGridView grid)
        {
            DataGridViewColumn col;

			if (grid.Columns.Count == 0) return null;

            if (grid.CurrentCell == null)
            {
                if (grid.SortedColumn != null)
                    col = grid.SortedColumn;
                else
                    col = grid.Columns[0];
            }
            else
               col = grid.Columns[grid.CurrentCell.ColumnIndex];
            
            return col;
        }
		public static DataGridViewColumn GetColumn(DataGridView grid, string dataPropertyName)
		{
			if (grid.Columns.Count == 0) return null;
            
			IEnumerable<DataGridViewColumn> results = 
				from DataGridViewColumn item in grid.Columns
				where (item.DataPropertyName == dataPropertyName)
				select item;

			return ((results.Count() == 0) ? null : results.ElementAt(0));			
		}

		public static Control GetControlByProperty(Control.ControlCollection controls, string property)
		{
			Control control = null;

			foreach(Control ctl in controls)
			{
				if (ctl is SplitContainer)
				{
					control = GetControlByProperty(((SplitContainer)ctl).Panel1.Controls, property);
					if (control != null) return control;

					control = GetControlByProperty(((SplitContainer)ctl).Panel2.Controls, property);
					if (control != null) return control;
				}
				else if (ctl is TabControl)
				{
					foreach (TabPage page in ((TabControl)(ctl)).TabPages)
					{
						control = GetControlByProperty(page.Controls, property);
						if (control != null) return control;
					}
				}
				else if (ctl is GroupBox)
				{
					control = GetControlByProperty(((GroupBox)(ctl)).Controls, property);
					if (control != null) return control;
				}
				else if ((ctl is TextBox) || (ctl is RichTextBox))
				{
					try
					{
						if (ctl.DataBindings[0].BindingMemberInfo.BindingField == property) return ctl;
					}
					catch { continue; }
				}
			}

			return null;
		}

        /// <summary>
        /// Establece como Current la columna de ordenación
        /// </summary>
        /// <param name="grid"></param>
        public static DataGridViewCell GetCurrentCell(DataGridView grid)
        {
            if (grid.Rows.Count == 0) return null;
            if (grid.CurrentRow == null) return null;
            return grid.CurrentCell;
        }

		public static List<string> GetPropertiesList(DataGridView grid)
		{
			List<string> list = new List<string>();

			foreach (DataGridViewColumn item in grid.Columns)
				list.Add(item.DataPropertyName);

			return list;
		}

        /// <summary>
        /// Establece como Current la columna de ordenación
        /// </summary>
        /// <param name="grid"></param>
        public static void SetCurrentCell(DataGridView grid) { SetCurrentCell(grid, -1); }
        public static void SetCurrentCell(DataGridView grid, int column_index)
        {
            if (grid.Rows.Count == 0) return;
            if (grid.CurrentRow == null) return;

            if (column_index == -1)
                grid.CurrentCell = grid.Rows[grid.CurrentRow.Index].Cells[grid.SortedColumn.Index];
            else
                grid.CurrentCell = grid.Rows[grid.CurrentRow.Index].Cells[column_index];
        }

        /// <summary>
        /// Establece como current la columna indicada
        /// </summary>
        /// <param name="grid"></param>
        public static void OrderByColumn(DataGridView grid, DataGridViewColumn column, ListSortDirection dir)
        {
            OrderByColumn(grid, column, dir, false);
        }
        public static void OrderByColumn(DataGridView grid, DataGridViewColumn column, ListSortDirection dir, bool respect_current)
        {
            if (column == null) return;
            if (grid.DataSource == null) return;
            if ((grid.Rows.Count == 0) || ((grid.Rows.Count == 1) && (grid.Rows[0].IsNewRow))) return;

            (grid.DataSource as BindingSource).RaiseListChangedEvents = false;
            grid.Sort(column, dir);
            if (!respect_current) SetCurrentCell(grid);
            (grid.DataSource as BindingSource).RaiseListChangedEvents = true;
        }

        public static void ShowDataGridColumns(DataGridView tabla, List<string> campos)
        {
            foreach (DataGridViewColumn col in tabla.Columns)
            {
                col.Visible = false;
                if (campos.Contains(col.Name))
                {
                    col.Visible = true;
                }
            }
        }

        public static void MaximizeColumns(DataGridView grid, List<DataGridViewColumn> maximizable_columns)
        {
			int rowWidth = grid.DisplayRectangle.Width;

			rowWidth -= SystemInformation.VerticalScrollBarWidth + 2;
            rowWidth -= grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.Visible && !maximizable_columns.Contains(col))
                    rowWidth -= col.Width;
            }

            foreach (DataGridViewColumn col in maximizable_columns)
            {
                col.Width = (int)(rowWidth * Convert.ToDecimal(col.Tag));
				col.Width = (col.Width < col.MinimumWidth) ? col.MinimumWidth : col.Width;
            }

			//grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        }

        public static void UpdateBinding(BindingSource source)
        {
            source.RaiseListChangedEvents = false;
            source.ResetBindings(false);
            source.RaiseListChangedEvents = true;
        }
		public static void UpdateBinding(DataGridView grid)
		{
			grid.SuspendLayout();
			BindingSource source = grid.DataSource as BindingSource;
			UpdateBinding(source);
			grid.ResumeLayout();
			grid.Refresh();
		}

		public static string GetIntegerMask(int size)
		{
			string mask = string.Empty;

			try
			{
				for (int i = 1; i <= size; i++)
					mask += "0";
			}
			catch { mask = string.Empty; }

			return mask;
		}
    }
}