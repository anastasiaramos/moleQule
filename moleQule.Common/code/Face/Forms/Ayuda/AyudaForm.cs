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
    public partial class AyudaForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "AyudaForm";
		public static Type Type { get { return typeof(AyudaForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }
		
        public virtual Ayuda Entity { get { return null; } set { } }
        public virtual AyudaInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public AyudaForm() 
			: this(-1) {}

        public AyudaForm(long oid) 
			: this(oid, true, null) {}

		public AyudaForm(bool is_modal) 
		: this(-1, is_modal, null) {}

        public AyudaForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Style

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Periodos_DGW, cols);
		}

        public override void FormatControls()
        {
			if (Periodos_DGW == null) return;

			MaximizeForm(new Size(this.Width, 0));

			base.FormatControls();

			CuentaContable_TB.Mask = (EntityInfo.CuentaContable != Library.Common.Resources.Defaults.NO_CONTABILIZAR)
							? ModuleController.GetCuentasMask()
							: string.Empty;
        }

		protected virtual void SetGridFormat() {}

		#endregion

		#region Source
	
		#endregion

        #region Print

        //public override void PrintObject()
        //{
        //    AyudaReportMng reportMng = new AyudaReportMng(AppContext.ActiveSchema);
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
			foreach (DataGridViewRow row in Periodos_DGW.Rows)
			{
				SelectObject(row);
			}
		}

		protected virtual void NewPeriodoAction() {}
		protected virtual void DeletePeriodoAction() {}
		protected virtual void SelectEstadoPeriodoAction() {}
		protected virtual void SelectTipoAyudaPeriodoAction() {}

		#endregion

		#region Actions

		protected virtual void SelectAllAction() {}
		protected virtual void SelectItemAction() {}
		protected virtual void SetEstadoItemsAction() { }
		protected virtual void NullItemsAction() {}

		#endregion

		#region Buttons

		private void SelectAll_TI_Click(object sender, EventArgs e) { SelectAllAction(); }
		private void AddPeriodo_TI_Click(object sender, EventArgs e) { NewPeriodoAction(); }
		private void DeletePeriodo_TI_Click(object sender, EventArgs e) { DeletePeriodoAction(); }

		#endregion
		
		#region Events

		private void Periodos_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Periodos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Periodos_DGW.Columns[e.ColumnIndex].DataPropertyName == Estado.DataPropertyName)
			{
				SelectEstadoPeriodoAction();
			}
			else if (Periodos_DGW.Columns[e.ColumnIndex].DataPropertyName == TipoDescuentoLabel.DataPropertyName)
			{
				SelectTipoAyudaPeriodoAction();
			}
		}

		#endregion
	}
}
