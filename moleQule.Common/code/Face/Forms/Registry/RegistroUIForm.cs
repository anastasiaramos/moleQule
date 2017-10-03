using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class RegistroUIForm : RegistroForm
    {
        #region Attributes & Properties
		
        public new const string ID = "RegistroUIForm";
		public new static Type Type { get { return typeof(RegistroUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Registro _entity;

        public override Registro Entity { get { return _entity; } set { _entity = value; } }
        public override RegistroInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected RegistroUIForm() 
			: this(null) { }

        public RegistroUIForm(Form parent) 
			: this(-1, parent) { }

        public RegistroUIForm(long oid) 
			: this(oid, null) { }

        public RegistroUIForm(long oid, Form parent)
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

            Registro temp = _entity.Clone();
            temp.ApplyEdit();
			PgMng.Grow();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();
				PgMng.Grow(string.Empty, "temp.Save()");

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
			foreach (DataGridViewRow row in LineaRegistros_DGW.Rows)
			{
				if (row.IsNewRow) return;

				LineaRegistro item = (LineaRegistro)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

		#endregion

		#region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
			
			Datos_LineaRegistros.DataSource = _entity.LineaRegistros;
            PgMng.Grow();
						
            base.RefreshMainData();
        }
		
        #endregion

		#region Business Methods

		protected override void SetEstadoItem()
		{
			if (LineaRegistros_DGW.CurrentRow == null) return;

			LineaRegistro item = (LineaRegistro)LineaRegistros_DGW.CurrentRow.DataBoundItem;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(GetEstados()));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				if (estado.Oid == ((long)EEstado.Anulado))
					NullItem(LineaRegistros_DGW.CurrentRow);
				else
					ChangeState(LineaRegistros_DGW.CurrentRow, (EEstado)estado.Oid);

				LineaRegistros_DGW.CurrentCell.Value = estado.Texto;

				SetGridFormat();
			}
		}

		#endregion

		#region Actions

		protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void SelectAllAction() { SelectAll(); }

		protected override void SelectItemAction() 
		{
			if (LineaRegistros_DGW.CurrentRow == null) return;
			SelectObject(LineaRegistros_DGW.CurrentRow); 
		}

		protected override void SetEstadoItemsAction() 
		{
			LineaRegistro item = (LineaRegistro)LineaRegistros_DGW.CurrentRow.DataBoundItem;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(GetEstados()));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				if (estado.Oid == ((long)EEstado.Anulado))
					NullItems();
				else
					ChangeStates((EEstado)estado.Oid);

				SetGridFormat();
			}
		}
		
		protected override void NullItemsAction() { NullItems(); }

		protected void ChangeState(DataGridViewRow row, EEstado estado)
		{
			if (row == null) return;
			if (row.DataBoundItem == null) return;

			LineaRegistro item = row.DataBoundItem as LineaRegistro;

			item.EEstadoEntidad = estado;
			item.EEstado = estado;
		}

		protected void ChangeStates(EEstado estado)
		{
			foreach (DataGridViewRow row in LineaRegistros_DGW.Rows)
				ChangeState(row, estado);
		}

        #endregion
    }
}
