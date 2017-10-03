using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public class ControlTools
	{
		#region Attributes & Properties

		public DataGridViewCellStyle HeaderSelectedStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle ReadOnlyStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle TransparentStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle AbiertoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle CerradoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle EmitidoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle ExportadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle ContabilizadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle AnuladoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle AceptadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle DesestimadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle PagadoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle PausedStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle PrescribedStyle = new DataGridViewCellStyle();

		public DataGridViewCellStyle AbiertoStyleIM = new DataGridViewCellStyle();
		public DataGridViewCellStyle AnuladoStyleIM = new DataGridViewCellStyle();

		public DataGridViewCellStyle CobradoStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle PendienteStyleA = new DataGridViewCellStyle();
		public DataGridViewCellStyle PendienteStyleB = new DataGridViewCellStyle();
		public DataGridViewCellStyle PendienteStyleC = new DataGridViewCellStyle();
		public DataGridViewCellStyle PendienteStyleD = new DataGridViewCellStyle();
		public DataGridViewCellStyle PendienteStyleE = new DataGridViewCellStyle();

        public DataGridViewCellStyle AuditadoStyle = new DataGridViewCellStyle();

		#endregion

		#region Factory Methods

		/// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
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
			HeaderSelectedStyle = moleQule.Face.ControlTools.Instance.HeaderSelectedStyle;

			Face.ControlTools.Instance.CopyBasicStyle(TransparentStyle);
			TransparentStyle.ForeColor = Color.Transparent;
			TransparentStyle.SelectionForeColor = Color.Transparent;

			//EntityMngForms
			Face.ControlTools.Instance.CopyBasicStyle(AbiertoStyle);
			AbiertoStyle.BackColor = Color.White;

			Face.ControlTools.Instance.CopyBasicStyle(CerradoStyle);
			CerradoStyle.BackColor = Color.FromArgb(240, 240, 240);

			Face.ControlTools.Instance.CopyBasicStyle(EmitidoStyle);
			EmitidoStyle.BackColor = Color.FromArgb(252, 249, 235);

			Face.ControlTools.Instance.CopyBasicStyle(ExportadoStyle);
			ExportadoStyle.BackColor = Color.FromArgb(252, 230, 205);

			Face.ControlTools.Instance.CopyBasicStyle(ContabilizadoStyle);
			//ContabilizadoStyle.BackColor = Color.FromArgb(225, 205, 205);
			ContabilizadoStyle.BackColor = Color.FromArgb(240, 240, 240);
			ContabilizadoStyle.ForeColor = Color.Black;

			Face.ControlTools.Instance.CopyBasicStyle(AnuladoStyle);
			AnuladoStyle.BackColor = Color.White;
			AnuladoStyle.ForeColor = Color.Gray;

			Face.ControlTools.Instance.CopyBasicStyle(DesestimadoStyle);
			DesestimadoStyle.ForeColor = Color.Black;
			DesestimadoStyle.BackColor = Color.FromArgb(240, 240, 240);

			Face.ControlTools.Instance.CopyBasicStyle(AceptadoStyle);
			AceptadoStyle.BackColor = Color.FromArgb(217, 246, 223);
			AceptadoStyle.ForeColor = Color.Black;

			Face.ControlTools.Instance.CopyBasicStyle(DesestimadoStyle);
			PagadoStyle.BackColor = Color.FromArgb(215, 241, 224);

			Face.ControlTools.Instance.CopyBasicStyle(PausedStyle);
			PausedStyle.BackColor = Color.FromArgb(252, 245, 211);

			Face.ControlTools.Instance.CopyBasicStyle(PrescribedStyle);
			PrescribedStyle.BackColor = Color.FromArgb(255, 222, 163);

			//ItemMngForms
			Face.ControlTools.Instance.CopyBasicStyle(AbiertoStyle);
			AbiertoStyleIM.BackColor = Color.White;

			Face.ControlTools.Instance.CopyBasicStyle(AnuladoStyleIM);
			AnuladoStyleIM.ForeColor = Color.Gray;

			CobradoStyle.BackColor = Color.LightGreen;
			CobradoStyle.SelectionBackColor = Color.LightGreen;

			PendienteStyleA.BackColor = Color.FromArgb(255, 255, 192);
			PendienteStyleA.SelectionBackColor = Color.FromArgb(255, 255, 192);
            PendienteStyleA.SelectionForeColor = AbiertoStyle.ForeColor;

			PendienteStyleB.BackColor = Color.FromArgb(255, 192, 128);
			PendienteStyleB.SelectionBackColor = Color.FromArgb(255, 192, 128);

			PendienteStyleC.BackColor = Color.Orange;
			PendienteStyleC.SelectionBackColor = Color.Orange;

			PendienteStyleD.BackColor = Color.OrangeRed;
			PendienteStyleD.SelectionBackColor = Color.OrangeRed;

			PendienteStyleE.BackColor = Color.Red;
            PendienteStyleE.SelectionBackColor = Color.Red;
            
            Face.ControlTools.Instance.CopyBasicStyle(AuditadoStyle);
            AuditadoStyle.BackColor = Color.FromArgb(215, 241, 224);

			ReadOnlyStyle = Face.ControlTools.Instance.ReadOnlyStyle;
		}

		#endregion

		#region Business Methods

		public void SetRowColor(DataGridViewRow row, EEstado estado)
        {
			switch (estado)
			{
				case EEstado.Aceptado:
					row.DefaultCellStyle = AceptadoStyle;
					break;

				case EEstado.Active:
				case EEstado.Abierto:
				case EEstado.Pendiente:
					row.DefaultCellStyle = AbiertoStyle;
					break;

				case EEstado.Anulado:
				case EEstado.Baja:
					row.DefaultCellStyle = AnuladoStyle;
					break;

				case EEstado.Closed:
				case EEstado.Billed:
					row.DefaultCellStyle = CerradoStyle;
					break;

				case EEstado.Contabilizado:
					row.DefaultCellStyle = ContabilizadoStyle;
					break;

				case EEstado.Desestimado:
					row.DefaultCellStyle = DesestimadoStyle;
					break;

				case EEstado.Emitido:
				case EEstado.EnSolicitud:
					row.DefaultCellStyle = EmitidoStyle;
					break;

				case EEstado.Exportado:
				case EEstado.Solicitado:
					row.DefaultCellStyle = ExportadoStyle;
					break;

				case EEstado.Pagado:
					row.DefaultCellStyle = PagadoStyle;
					break;

				case EEstado.Paused:
					row.DefaultCellStyle = PausedStyle;
					break;

				case EEstado.Expired:
                case EEstado.Devuelto:
					row.DefaultCellStyle = PrescribedStyle;
					break;

                case EEstado.Auditado:
                case EEstado.Charged:
                    row.DefaultCellStyle = AuditadoStyle;
                    break;
			}   
        }

		public void SetRowColorIM(DataGridViewRow row, EEstado estado)
		{
			switch (estado)
			{
				case EEstado.Abierto:
					row.DefaultCellStyle.ForeColor = AbiertoStyleIM.ForeColor;
					row.DefaultCellStyle.BackColor = AbiertoStyleIM.BackColor;
					break;

				case EEstado.Anulado:
					row.DefaultCellStyle = AnuladoStyleIM;
					break;
			}

			if (estado != EEstado.Anulado)
			{
				foreach (DataGridViewCell cell in row.Cells)
					if (cell.ReadOnly) Face.ControlTools.Instance.ApplyStyle(cell, CellStyle.ReadOnly);
			}
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

		#endregion
	}
}
