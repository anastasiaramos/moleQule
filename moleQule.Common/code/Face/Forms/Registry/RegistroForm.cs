using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class RegistroForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "RegistroForm";
		public static Type Type { get { return typeof(RegistroForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }
		
        public virtual Registro Entity { get { return null; } set { } }
        public virtual RegistroInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public RegistroForm() 
			: this(-1) {}

        public RegistroForm(long oid) 
			: this(oid, true, null) {}

		public RegistroForm(bool is_modal) 
		: this(-1, is_modal, null) {}

        public RegistroForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Style

        public override void FormatControls()
        {
			if (LineaRegistros_DGW == null) return;

			MaximizeForm(new Size(1280, 0));

			base.FormatControls();

			switch (EntityInfo.ETipoRegistro)
			{ 
				case ETipoRegistro.Contabilidad:
					IDExportacion.Visible = true;
					break;

				case ETipoRegistro.Fomento:
					IDExportacion.Visible = false;
					break;
			}

			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Descripcion.Tag = 0.6;
			Observaciones.Tag = 0.4;

			cols.Add(Descripcion);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(LineaRegistros_DGW, cols);

			SetGridFormat();
        }

		protected virtual void SetGridFormat() {}

		#endregion

		#region Source

        public override void RefreshSecondaryData()
		{
			Datos_TipoRegistros.DataSource = Library.Common.EnumText<ETipoRegistro>.GetList(false);
			PgMng.Grow();			

			Datos_Estados.DataSource = Library.Common.EnumText<EEstado>.GetList(GetEstados());
			PgMng.Grow();
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    RegistroReportMng reportMng = new RegistroReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

		#region Business Methods

		public void SelectObject(DataGridViewRow row)
		{
			row.Cells[Seleccionar.Index].Value = "True";
		}

		public void SelectAll()
		{
			foreach (DataGridViewRow row in LineaRegistros_DGW.Rows)
			{
				SelectObject(row);
			}
		}

		protected EEstado[] GetEstados()
		{
			switch (EntityInfo.ETipoRegistro)
			{
				case ETipoRegistro.Contabilidad:
					{
						EEstado[] list = { EEstado.Contabilizado, EEstado.Anulado, EEstado.Desestimado };
						return list;
					}

				case ETipoRegistro.Fomento:
					{
						EEstado[] list = { EEstado.Anulado, EEstado.Abierto, EEstado.Exportado, EEstado.EnSolicitud, EEstado.Solicitado, EEstado.Aceptado, EEstado.Desestimado };
						return list;
					}
			}

			return null;
		}
		
		protected void NullItem(DataGridViewRow row)
		{
			LineaRegistro item = row.DataBoundItem as LineaRegistro;
			item.EEstadoEntidad = EEstado.Exportado;
			item.EEstado = EEstado.Anulado;
		}
		protected void NullItems()
		{
			foreach (DataGridViewRow row in LineaRegistros_DGW.Rows)
			{
				if (row == null) return;
				NullItem(row);
			}

			Datos_LineaRegistros.ResetBindings(false);
			SetGridFormat();
		}

		protected virtual void SetEstadoItem() {}
		protected virtual void SetEstadoItems() {}

		#endregion

		#region Actions

		protected virtual void SelectAllAction() {}
		protected virtual void SelectItemAction() {}
		protected virtual void SetEstadoItemsAction() { }
		protected virtual void NullItemsAction() {}

		#endregion

		#region Buttons

		private void SelectAll_TI_Click(object sender, EventArgs e)
		{
			SelectAllAction();
		}
		
		private void Lock_TI_Click(object sender, EventArgs e)
		{
			SetEstadoItemsAction();
		}

		private void Null_TI_Click(object sender, EventArgs e)
		{
			NullItemsAction();
		}

		#endregion
		
		#region Events

		private void RegistroForm_Shown(object sender, EventArgs e)
		{
			
		}

		private void LineaRegistros_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (LineaRegistros_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (LineaRegistros_DGW.Columns[e.ColumnIndex].DataPropertyName == Estado.DataPropertyName)
			{
				SetEstadoItem();
			}
		}

		#endregion
	}
}
