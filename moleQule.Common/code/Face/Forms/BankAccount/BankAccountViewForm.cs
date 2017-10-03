using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class BankAccountViewForm : BankAccountForm
    {
        #region Attributes & Properties

        public new const string ID = "BankAccountViewForm";
		public new static Type Type { get { return typeof(BankAccountViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private BankAccountInfo _entity;

        public override BankAccountInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public BankAccountViewForm(long oid) 
			: this(oid, null) {}

        public BankAccountViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text += ": " + EntityInfo.Valor.ToUpper();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = BankAccountInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		#endregion
		
		#region Source
		
        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_CuentasAsociadas.DataSource = _entity.CuentasAsociadas;
            Datos_FondosInversion.DataSource = _entity.FondosInversion;
			
            base.RefreshMainData();
        }
		
        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }
}