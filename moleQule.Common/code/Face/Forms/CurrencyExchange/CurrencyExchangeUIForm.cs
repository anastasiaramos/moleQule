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
	public partial class CurrencyExchangeUIForm : Skin02.ListMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "CurrencyExchangeUIForm";
        public static Type Type { get { return typeof(CurrencyExchangeUIForm); } }

        private CurrencyExchanges _list;

        public CurrencyExchanges List
        {
            get { return _list; }
            set { _list = value; }
        }

        #endregion

        #region Factory Methods

        public CurrencyExchangeUIForm()
            : this(null) {}

        public CurrencyExchangeUIForm(Form parent)
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
            _list = CurrencyExchanges.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
			this.Datos.RaiseListChangedEvents = false; ;

            // do the save
            try
            {
                _list.Save();
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
            Comments.Tag = 1;

			cols.Add(Comments);

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

		private void SetSourceCurrency()
		{
			SelectInputForm form = new SelectInputForm(Currency.Load());

			if (DialogResult.OK == form.ShowDialog())
			{
				Currency item = form.Selected as Currency;

				CurrencyExchange entity = Datos.Current as CurrencyExchange;
				entity.FromCurrencyIso = item.ISOCode;
			}
		}

		private void SetDestinationCurrency()
		{
			SelectInputForm form = new SelectInputForm(Currency.Load());

			if (DialogResult.OK == form.ShowDialog())
			{
				Currency item = form.Selected as Currency;
				
				CurrencyExchange entity = Datos.Current as CurrencyExchange;
				entity.ToCurrencyIso = item.ISOCode;
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
			_list.Remove(Datos.Current as CurrencyExchange);
        }

        protected override void CancelAction()
        {
            _list.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void CurrencyExchangeUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           CloseSession();
        }

        private void CurrencyExchangeUIForm_Shown(object sender, EventArgs e)
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

            if (Datos_DGW.Columns[e.ColumnIndex].Name == FromCurrency.Name)
            {
                SetSourceCurrency();
            }
			else if (Datos_DGW.Columns[e.ColumnIndex].Name == ToCurrency.Name)
			{
				SetDestinationCurrency();
			}
        }		

        #endregion

    }
}
