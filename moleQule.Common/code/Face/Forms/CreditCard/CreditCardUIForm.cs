using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class CreditCardUIForm : Skin02.ListMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "CreditCardUIForm";
        public static Type Type { get { return typeof(CreditCardUIForm); } }

        private CreditCards _list;

        public CreditCards List { get { return _list; } set { _list = value; } }

        #endregion

        #region Factory Methods

        public CreditCardUIForm()
            : this(null) {}

        public CreditCardUIForm(Form parent)
			: base(true, parent)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_DGW.DataSource = DatosLocal_BS;
        }

        protected override void GetFormSourceData()
        {
            _list = CreditCards.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false; ;

                // do the save
                try
                {
                    _list.Save();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    RefreshMainData();
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }

        protected virtual void CloseSession()
        {
            if (_list != null) _list.CloseSession();
        }
        
        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 1;

            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Datos_DGW, cols);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _list;
        }

		public override void RefreshSecondaryData()
		{
			Datos_Tipo.DataSource = Library.Common.EnumText<ETipoTarjeta>.GetList(false);
		}

        #endregion

        #region Business Methods

        protected virtual void SetBankAccount()
        {
            CreditCard item = (CreditCard)Datos.Current;

			BankAccountSelectForm form = new BankAccountSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                item.OidCuentaBancaria = cuenta.Oid;
                item.CuentaBancaria = cuenta.Valor;
                Datos_DGW.CurrentCell.Value = cuenta.Valor;
            }
        }

		protected virtual void SetLineType()
		{
            CreditCard item = (CreditCard)Datos.Current;
			item.Tipo = ((ComboBoxSource)Datos_Tipo.Current).Oid;
		}

		protected virtual void SetFormaPago()
		{
            if (!ControlsMng.IsCurrentItemValid(Datos_DGW)) return;
            CreditCard item = ControlsMng.GetCurrentItem(Datos_DGW) as CreditCard;

            EFormaPago[] list;

            if (item.ETipoTarjeta == ETipoTarjeta.Credito)
                list = new EFormaPago[] { EFormaPago.MesVencido, EFormaPago.XDiasMes };
            else
                list = new EFormaPago[] { EFormaPago.Contado };

			SelectEnumInputForm form = new SelectEnumInputForm(true);            
            form.SetDataSource(Library.Common.EnumText<EFormaPago>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource forma_pago = form.Selected as ComboBoxSource;

				item.FormaPago = forma_pago.Oid;
				Datos_DGW.CurrentCell.Value = item.FormaPagoLabel;
			}
		}

        #endregion

        #region Buttons

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void AddAction()
        {
            _list.NewItem();
            Datos.ResetBindings(false);
        }

        protected override void DeleteAction()
        {
            throw new Exception("Comprobar que no hay pagos asociados");
        }

        protected override void CancelAction()
        {
            _list.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void TarjetaCreditoUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           CloseSession();
        }

        private void TarjetaCreditoUIForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Datos_DGW.Name);
        }

        private void Datos_DG_DoubleClick(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Default);
        }

        private void Datos_DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Datos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

            if (Datos_DGW.Columns[e.ColumnIndex].Name == CuentaBancaria.Name)
            {
                SetBankAccount();
            }
			else if (Datos_DGW.Columns[e.ColumnIndex].Name == FormaPagoLabel.Name)
			{
				SetFormaPago();
			}
        }
		
		private void Datos_DG_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (Datos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Datos_DGW.Columns[e.ColumnIndex].Name == TipoTarjetaLabel.Name)
			{
				SetLineType();
			}
		}

        #endregion
    }
}