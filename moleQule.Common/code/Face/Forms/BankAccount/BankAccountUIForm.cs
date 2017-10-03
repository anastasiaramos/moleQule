using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class BankAccountUIForm : BankAccountForm
    {
        #region Attributes & Properties

        public new const string ID = "BankAccountUIForm";
		public new static Type Type { get { return typeof(BankAccountUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected BankAccount _entity;

        public override BankAccount Entity { get { return _entity; } set { _entity = value; } }
        public override BankAccountInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected BankAccountUIForm() 
			: this(null) { }

        public BankAccountUIForm(Form parent) 
			: this(-1, parent) { }

        public BankAccountUIForm(long oid) 
			: this(oid, null) { }

        public BankAccountUIForm(long oid, Form parent)
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

            BankAccount temp = _entity.Clone();
			temp.ApplyEdit();

			// do the save
			try
			{
				_entity = temp.Save();
				_entity.ApplyEdit();				

				return true;
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(iQExceptionHandler.GetAllMessages(ex));
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region Style

		protected virtual void SetGridColors(Control grid)
		{
			if (grid == CuentasAsociadas_DGW)
			{
                BankAccount item;

				foreach (DataGridViewRow row in CuentasAsociadas_DGW.Rows)
				{
					if (row.IsNewRow) return;

                    item = row.DataBoundItem as BankAccount;
					if (item == null) continue;

					Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);
				}
			}
		}

		#endregion
		
		#region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_CuentasAsociadas.DataSource = _entity.CuentasAsociadas;
            Datos_FondosInversion.DataSource = _entity.FondosInversion;

            base.RefreshMainData();
        }	
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

        #region Actions

        protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		public override void AddCuentaAsociadaAction() 
		{
			_entity.CuentasAsociadas.NewChildItem(_entity);
		}

		public override void DeleteCuentaAsociadaAction() 
		{
            BankAccount item = (BankAccount)CuentasAsociadas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			if (item.IsNew) _entity.CuentasAsociadas.Remove(item);
			else
				PgMng.ShowInfoException("No es posible eliminar una cuenta asociada que ha sido guardada previamente. Si no desea seguir utilizándola puede desactivarla.");
		}

		public override void SetStateCuentaAsociadaAction() 
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.Active, EEstado.Baja };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
                BankAccount item = (BankAccount)CuentasAsociadas_DGW.CurrentRow.DataBoundItem;
				if (item == null) return;

				ComboBoxSource estado = form.Selected as ComboBoxSource;
				item.Estado = estado.Oid;
				SetGridColors(CuentasAsociadas_DGW);
			}
		}

        public override void RealizarCancelacionAction()
        {
            BankAccount item = (BankAccount)CuentasAsociadas_DGW.CurrentRow.DataBoundItem;
            if (item == null) return;

            if (ProgressInfoMng.ShowQuestion("Para realizar una cancelación debe guardar la cuenta bancaria ahora. ¿Desea continuar?") == DialogResult.Yes)
            {
                if (SaveObject())
                {
                    Assembly assembly = Assembly.Load("moleQule.Face.Invoice");
                    Type type = assembly.GetType("moleQule.Face.Invoice.TraspasoAddForm");

                    ItemMngBaseForm form = (ItemMngBaseForm)type.InvokeMember("TraspasoAddForm", BindingFlags.CreateInstance, null, null, new object[1] { item });
                    form.ShowDialog();

                    Close();
                }
            }
        }

        public override void AddFondoInversionAction()
        {
            _entity.FondosInversion.NewChildItem(_entity, ETipoCuenta.FondoInversion);
        }

        public override void DeleteFondoInversionAction()
        {
            BankAccount item = (BankAccount)FondosInversion_DGW.CurrentRow.DataBoundItem;
            if (item == null) return;

            if (item.IsNew) _entity.CuentasAsociadas.Remove(item);
            else
                PgMng.ShowInfoException("No es posible eliminar una cuenta asociada que ha sido guardada previamente. Si no desea seguir utilizándola puede desactivarla.");
        }

        public override void SetStateFondoInversionAction()
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);

            EEstado[] list = { EEstado.Active, EEstado.Baja };
            form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                BankAccount item = (BankAccount)FondosInversion_DGW.CurrentRow.DataBoundItem;
                if (item == null) return;

                ComboBoxSource estado = form.Selected as ComboBoxSource;
                item.Estado = estado.Oid;
                SetGridColors(FondosInversion_DGW);
            }
        }

        #endregion

		#region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.Active, EEstado.Baja };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
			}
		}

		private void CuentaContable_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContable = Library.Common.Resources.Defaults.NO_CONTABILIZAR;
		}

		private void CuentaContableGastos_BT_Click(object sender, EventArgs e)
		{
			_entity.CuentaContableGastos = Library.Common.Resources.Defaults.NO_CONTABILIZAR;
		}

		private void TipoCuenta_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			ETipoCuenta[] list = { ETipoCuenta.CuentaCorriente, ETipoCuenta.LineaCredito };
			form.SetDataSource(Library.Common.EnumText<ETipoCuenta>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.TipoCuenta = estado.Oid;
			}
		}

		#endregion
				
		#region Events


		#endregion
	}
}