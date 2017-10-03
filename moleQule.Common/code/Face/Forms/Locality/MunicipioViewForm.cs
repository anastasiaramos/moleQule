using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class MunicipioViewForm : MunicipioForm
    {
        #region Attributes & Properties
		
        public new const string ID = "MunicipioViewForm";
		public new static Type Type { get { return typeof(MunicipioViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private MunicipioInfo _entity;

        public override MunicipioInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

		public MunicipioViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            SetFormData();
			_mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = MunicipioInfo.Get(oid);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style & Source

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();			
			
            base.RefreshMainData();
        }
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Asigna formato deseado a los controles del objeto cuando éste es modificado
        /// </summary>
        protected override void FormatData()
        {
        }

        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
			DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Events

        #endregion
    }
}
