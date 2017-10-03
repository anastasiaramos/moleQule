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
using moleQule.Library.Store;

namespace moleQule.Face.Common
{
    public partial class CurrencyExchangeSelectForm : CurrencyExchangeUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CurrencyExchangeSelectForm";
        public new static Type Type { get { return typeof(CurrencyExchangeSelectForm); } }

        private CreditCardList _list;

        #endregion

        #region Factory Methods

		public CurrencyExchangeSelectForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
			DialogResult = DialogResult.Cancel; _view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        protected override void GetFormSourceData()
        {
            _list = CreditCardList.GetList();
        }

        protected override void CloseSession() {}

        #endregion

        #region Style

        public override void FormatControls()
        {
            PanelesV.Panel2Collapsed = true;

            base.FormatControls();
            Datos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _list;
        }

        #endregion

        #region Business Methods

        #endregion

        #region Actions

        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
