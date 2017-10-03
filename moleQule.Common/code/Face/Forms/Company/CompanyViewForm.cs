using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
	public partial class CompanyViewForm : CompanyForm
	{
        #region Business Methods

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
		private CompanyInfo _entity;

		public override CompanyInfo EntityInfo
        {
            get { return _entity; }
        }

		public override long ContactosActiveOID()
		{
			if (Datos_Contactos.Current != null)
				return ((ContactoEmpresaInfo)(Datos_Contactos.Current)).Oid;
			else
				return -1;
		}

        #endregion

        #region Factory Methods

        public CompanyViewForm(long oid, Form parent)
            : base(oid, null, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
			_entity = CompanyInfo.Get(oid, true);
        }

        #endregion

        #region Style & Source

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
			base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity; 
            Datos_Contactos.DataSource = ContactoEmpresaList.SortList(_entity.Contactos, "Nombre", ListSortDirection.Ascending);
			PgMng.Grow();

			base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion
	}
}

