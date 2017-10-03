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
    public partial class AyudaUIForm : AyudaForm
    {
        #region Attributes & Properties

		public new const string ID = "AyudaUIForm";
		public new static Type Type { get { return typeof(AyudaUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		/// <summary>
		/// Se trata del objeto actual y que se va a editar.
		/// </summary>
		protected Ayuda _entity;

		public override Ayuda Entity { get { return _entity; } set { _entity = value; } }
		public override AyudaInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected AyudaUIForm() 
			: this(null) { }

        public AyudaUIForm(Form parent) 
			: this(-1, parent) { }

        public AyudaUIForm(long oid) 
			: this(oid, null) { }

		public AyudaUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Ayuda temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            catch (iQValidationException ex)
            {
				PgMng.ShowInfoException(ex);
                return false;
            }
            catch (Exception ex)
            {
				PgMng.ShowErrorException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region Style

		protected override void SetGridFormat()
		{
			foreach (DataGridViewRow row in Periodos_DGW.Rows)
			{
				if (row.IsNewRow) return;

				AyudaPeriodo item = (AyudaPeriodo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			Datos.DataSource = _entity;
			PgMng.Grow();

			Datos_Periodos.DataSource = _entity.Periodos;
			PgMng.Grow();
		}

		#endregion

		#region Business Methods

		protected void ChangeState(DataGridViewRow row, EEstado estado)
		{
			if (row == null) return;
			if (row.DataBoundItem == null) return;

			AyudaPeriodo item = row.DataBoundItem as AyudaPeriodo;

			item.EEstado = estado;
		}

		#endregion

		#region Actions

		protected override void SaveAction()
		{
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
		}

		protected override void DeletePeriodoAction() 
		{
			if (Datos_Periodos.Current == null) return;

			if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				AyudaPeriodo item = (AyudaPeriodo)Datos_Periodos.Current;
				_entity.Periodos.Remove(item);
			}
		}

		protected override void NewPeriodoAction()
		{
			_entity.Periodos.NewItem(_entity);
			ControlsMng.UpdateBinding(Datos_Periodos);
		}

		protected override void SelectEstadoPeriodoAction()
		{
			if (Periodos_DGW.CurrentRow == null) return;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.Active, EEstado.Anulado, EEstado.Baja };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				ChangeState(Periodos_DGW.CurrentRow, (EEstado)estado.Oid);

				Periodos_DGW.CurrentCell.Value = estado.Texto;

				SetGridFormat();
			}
		}

		protected override void SelectTipoAyudaPeriodoAction()
		{
			AyudaPeriodo item = Periodos_DGW.CurrentRow.DataBoundItem as AyudaPeriodo;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<ETipoDescuento>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				item.ETipoDescuento = (ETipoDescuento)tipo.Oid;

				ControlsMng.UpdateBinding(Datos_Periodos);
			}
		}

		#endregion

		#region Buttons

		private void NoContabilizar_BT_Click(object sender, EventArgs e)
		{
			CuentaContable_TB.Mask = string.Empty;
			_entity.CuentaContable = Library.Common.Resources.Defaults.NO_CONTABILIZAR;
		}

		#endregion
	}
}
