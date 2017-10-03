using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    public class ControlTools
    {
		#region Attributes & Properties

		public DataGridViewCellStyle BasicStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle HeaderStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle HeaderSelectedStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle ReadOnlyStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle LongStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle DecimalStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle DateStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle DateTimeStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle AbiertoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle EmitidoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle ContabilizadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle AnuladoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle CerradoStyle = new DataGridViewCellStyle();

		#endregion

		#region Factory Methods

		/// <summary>
		/// Única instancia de la clase AppControllerBase (Singleton)
		/// </summary>
		protected static ControlTools _main;

		/// <summary>
		/// Unique Controler Class Instance
		/// </summary>
		public static ControlTools Instance { get { return (_main != null) ? _main : new ControlTools(); } }

		/// <summary>
		/// Contructor 
		/// </summary>
		protected ControlTools()
		{
			// Singleton
			_main = this;

			InitStyles();
		}

		private void InitStyles()
		{
			BasicStyle.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
			BasicStyle.BackColor = Color.White;
			BasicStyle.ForeColor = Color.FromArgb(0, 0, 192);
			BasicStyle.WrapMode = DataGridViewTriState.False;

			CopyBasicStyle(ReadOnlyStyle);
			ReadOnlyStyle.BackColor = Color.Gainsboro;
			ReadOnlyStyle.ForeColor = Color.Black;

			CopyBasicStyle(LongStyle);
			LongStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			LongStyle.Format = "N0";

			CopyBasicStyle(DecimalStyle);
			DecimalStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			DecimalStyle.Format = "N2";

			CopyBasicStyle(DateStyle);
			DateStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DateStyle.Format = "d";
			DateStyle.NullValue = string.Empty;

			CopyBasicStyle(DateTimeStyle);
			DateTimeStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DateTimeStyle.Format = "MM/dd/yyyy HH:mm";
			DateTimeStyle.NullValue = string.Empty;

			HeaderStyle.Font = BasicStyle.Font;
			HeaderStyle.WrapMode = DataGridViewTriState.True;

			HeaderSelectedStyle.Font = new Font("Tahoma", (float)7.10, FontStyle.Bold);
			HeaderSelectedStyle.WrapMode = DataGridViewTriState.True;

			CopyBasicStyle(AbiertoStyle);
			AbiertoStyle.BackColor = Color.White;

			CopyBasicStyle(EmitidoStyle);
			EmitidoStyle.BackColor = Color.FromArgb(252, 245, 211);

			CopyBasicStyle(AnuladoStyle);
			AnuladoStyle.BackColor = Color.White;
			AnuladoStyle.ForeColor = Color.Gray;

			CopyBasicStyle(ContabilizadoStyle);
			ContabilizadoStyle.BackColor = Color.FromArgb(240, 240, 240);
			ContabilizadoStyle.ForeColor = Color.Black;

			CopyBasicStyle(CerradoStyle);
			CerradoStyle.BackColor = Color.FromArgb(225, 205, 205);
			CerradoStyle.ForeColor = Color.Black;
		}

		#endregion

		#region Business Methods

		public void CopyBasicStyle(DataGridViewCellStyle copy)
		{
			copy.Font = BasicStyle.Font;
			copy.BackColor = BasicStyle.BackColor;
			copy.ForeColor = BasicStyle.ForeColor;
			copy.WrapMode = BasicStyle.WrapMode;
		}

		public void ApplyStyle(DataGridViewCell cell, CellStyle style)
		{
			switch (style)
			{
				case CellStyle.ReadOnly:
					cell.Style.BackColor = ReadOnlyStyle.BackColor;
					cell.Style.ForeColor = ReadOnlyStyle.ForeColor;
					break;
			}
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

		public void SetRowColor(DataGridViewRow row, EEstadoItem estado)
		{
			switch (estado)
			{
				case EEstadoItem.Registered:
				case EEstadoItem.Unlock:
					row.DefaultCellStyle = AbiertoStyle;
					break;

				case EEstadoItem.Emitido:
					row.DefaultCellStyle = EmitidoStyle;
					break;

				case EEstadoItem.Contabilizado:
					row.DefaultCellStyle = ContabilizadoStyle;
					break;

				case EEstadoItem.Anulado:
				case EEstadoItem.Baja:
					row.DefaultCellStyle = AnuladoStyle;
					break;
			}
		}

		#endregion
	}

	public enum CellStyle { ReadOnly }
}
